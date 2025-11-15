using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Input;
using Windows.Storage;
using Windows.Storage.Pickers;

namespace WinUI3RisohEditor
{
    /// <summary>
    /// Main window for RisohEditor - A resource editor for Windows executables and resource files
    /// </summary>
    public sealed partial class MainWindow : Window, INotifyPropertyChanged
    {
        private string? _currentFilePath;
        private ResourceFile? _currentFile;
        private ResourceNode? _selectedResource;

        public MainWindow()
        {
            InitializeComponent();
            ExtendsContentIntoTitleBar = true;
            SetTitleBar(null);
        }

        // INotifyPropertyChanged implementation
        public event PropertyChangedEventHandler? PropertyChanged;

        private void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        // Bindable properties
        public bool HasOpenFile => _currentFile != null;
        public bool HasSelectedResource => _selectedResource != null;

        // File menu handlers
        private void OnNewFile(object sender, RoutedEventArgs e)
        {
            _currentFile = new ResourceFile();
            _currentFilePath = null;
            RefreshTreeView();
            UpdateStatus("New resource file created");
            OnPropertyChanged(nameof(HasOpenFile));
        }

        private async void OnOpenFile(object sender, RoutedEventArgs e)
        {
            var picker = new FileOpenPicker
            {
                SuggestedStartLocation = PickerLocationId.DocumentsLibrary
            };
            picker.FileTypeFilter.Add(".rc");
            picker.FileTypeFilter.Add(".res");
            picker.FileTypeFilter.Add(".exe");
            picker.FileTypeFilter.Add(".dll");

            // Get the window handle for the picker
            var hwnd = WinRT.Interop.WindowNative.GetWindowHandle(this);
            WinRT.Interop.InitializeWithWindow.Initialize(picker, hwnd);

            var file = await picker.PickSingleFileAsync();
            if (file != null)
            {
                try
                {
                    _currentFilePath = file.Path;
                    _currentFile = await ResourceFile.LoadAsync(file.Path);
                    RefreshTreeView();
                    UpdateStatus($"Opened: {file.Name}");
                    OnPropertyChanged(nameof(HasOpenFile));
                }
                catch (Exception ex)
                {
                    await ShowErrorDialog("Error opening file", ex.Message);
                }
            }
        }

        private async void OnSaveFile(object sender, RoutedEventArgs e)
        {
            if (_currentFile == null) return;

            if (string.IsNullOrEmpty(_currentFilePath))
            {
                OnSaveAsFile(sender, e);
                return;
            }

            try
            {
                await _currentFile.SaveAsync(_currentFilePath);
                UpdateStatus($"Saved: {Path.GetFileName(_currentFilePath)}");
            }
            catch (Exception ex)
            {
                await ShowErrorDialog("Error saving file", ex.Message);
            }
        }

        private async void OnSaveAsFile(object sender, RoutedEventArgs e)
        {
            if (_currentFile == null) return;

            var picker = new FileSavePicker
            {
                SuggestedStartLocation = PickerLocationId.DocumentsLibrary,
                SuggestedFileName = "resource"
            };
            picker.FileTypeChoices.Add("Resource Script", new List<string> { ".rc" });
            picker.FileTypeChoices.Add("Resource Binary", new List<string> { ".res" });

            var hwnd = WinRT.Interop.WindowNative.GetWindowHandle(this);
            WinRT.Interop.InitializeWithWindow.Initialize(picker, hwnd);

            var file = await picker.PickSaveFileAsync();
            if (file != null)
            {
                try
                {
                    _currentFilePath = file.Path;
                    await _currentFile.SaveAsync(_currentFilePath);
                    UpdateStatus($"Saved: {file.Name}");
                }
                catch (Exception ex)
                {
                    await ShowErrorDialog("Error saving file", ex.Message);
                }
            }
        }

        private void OnExit(object sender, RoutedEventArgs e)
        {
            Close();
        }

        // Edit menu handlers
        private async void OnAddResource(object sender, RoutedEventArgs e)
        {
            if (_currentFile == null) return;

            var dialog = new ContentDialog
            {
                Title = "Add Resource",
                Content = new AddResourceDialog(),
                PrimaryButtonText = "Add",
                CloseButtonText = "Cancel",
                XamlRoot = Content.XamlRoot
            };

            var result = await dialog.ShowAsync();
            if (result == ContentDialogResult.Primary)
            {
                RefreshTreeView();
                UpdateStatus("Resource added");
            }
        }

        private async void OnDeleteResource(object sender, RoutedEventArgs e)
        {
            if (_selectedResource == null) return;

            var dialog = new ContentDialog
            {
                Title = "Delete Resource",
                Content = $"Are you sure you want to delete '{_selectedResource.Name}'?",
                PrimaryButtonText = "Delete",
                CloseButtonText = "Cancel",
                XamlRoot = Content.XamlRoot
            };

            var result = await dialog.ShowAsync();
            if (result == ContentDialogResult.Primary)
            {
                _currentFile?.Resources.Remove(_selectedResource);
                RefreshTreeView();
                UpdateStatus("Resource deleted");
            }
        }

        private async void OnImportResource(object sender, RoutedEventArgs e)
        {
            await ShowInfoDialog("Import Resource", "Import functionality will be implemented here.");
        }

        private async void OnExportResource(object sender, RoutedEventArgs e)
        {
            await ShowInfoDialog("Export Resource", "Export functionality will be implemented here.");
        }

        // View menu handlers
        private void OnExpandAll(object sender, RoutedEventArgs e)
        {
            ExpandCollapseAll(ResourceTreeView, true);
        }

        private void OnCollapseAll(object sender, RoutedEventArgs e)
        {
            ExpandCollapseAll(ResourceTreeView, false);
        }

        private void ExpandCollapseAll(TreeView treeView, bool expand)
        {
            // TreeView expansion logic would go here
            UpdateStatus(expand ? "Expanded all nodes" : "Collapsed all nodes");
        }

        // Help menu handlers
        private async void OnAbout(object sender, RoutedEventArgs e)
        {
            var dialog = new ContentDialog
            {
                Title = "About RisohEditor",
                Content = "RisohEditor - WinUI3 Edition\n\nA Windows resource editor\nBased on RisohEditor by katahiromz\n\nVersion 1.0",
                CloseButtonText = "OK",
                XamlRoot = Content.XamlRoot
            };

            await dialog.ShowAsync();
        }

        // Resource tree handlers
        private void OnResourceSelectionChanged(object sender, TreeViewSelectionChangedEventArgs e)
        {
            if (e.AddedItems.Count > 0 && e.AddedItems[0] is ResourceNode node)
            {
                _selectedResource = node;
                DisplayResource(node);
                OnPropertyChanged(nameof(HasSelectedResource));
            }
        }

        // Splitter handlers
        private void OnSplitterPointerEntered(object sender, PointerRoutedEventArgs e)
        {
            ProtectedCursor = InputSystemCursor.Create(InputSystemCursorShape.SizeWestEast);
        }

        private void OnSplitterPointerExited(object sender, PointerRoutedEventArgs e)
        {
            ProtectedCursor = InputSystemCursor.Create(InputSystemCursorShape.Arrow);
        }

        // Resource editor handlers
        private void OnSaveResourceChanges(object sender, RoutedEventArgs e)
        {
            if (_selectedResource != null)
            {
                _selectedResource.Content = ResourceContentEditor.Text;
                UpdateStatus("Resource changes saved");
            }
        }

        private void OnDiscardResourceChanges(object sender, RoutedEventArgs e)
        {
            if (_selectedResource != null)
            {
                DisplayResource(_selectedResource);
                UpdateStatus("Changes discarded");
            }
        }

        // Helper methods
        private void RefreshTreeView()
        {
            if (_currentFile == null)
            {
                ResourceTreeView.RootNodes.Clear();
                EmptyStatePanel.Visibility = Visibility.Visible;
                ResourceEditorPanel.Visibility = Visibility.Collapsed;
                return;
            }

            EmptyStatePanel.Visibility = Visibility.Collapsed;
            
            // Group resources by type
            var groupedResources = _currentFile.Resources
                .GroupBy(r => r.Type)
                .OrderBy(g => g.Key);

            ResourceTreeView.RootNodes.Clear();
            foreach (var group in groupedResources)
            {
                var groupNode = new TreeViewNode
                {
                    Content = group.Key,
                    IsExpanded = true
                };

                foreach (var resource in group)
                {
                    var resourceNode = new TreeViewNode
                    {
                        Content = resource
                    };
                    groupNode.Children.Add(resourceNode);
                }

                ResourceTreeView.RootNodes.Add(groupNode);
            }
        }

        private void DisplayResource(ResourceNode resource)
        {
            ResourceEditorPanel.Visibility = Visibility.Visible;
            ResourceTypeText.Text = $"Type: {resource.Type}";
            ResourceNameText.Text = $"Name: {resource.Name}";
            ResourceContentEditor.Text = resource.Content ?? string.Empty;
        }

        private void UpdateStatus(string message)
        {
            StatusText.Text = message;
        }

        private async System.Threading.Tasks.Task ShowErrorDialog(string title, string message)
        {
            var dialog = new ContentDialog
            {
                Title = title,
                Content = message,
                CloseButtonText = "OK",
                XamlRoot = Content.XamlRoot
            };
            await dialog.ShowAsync();
        }

        private async System.Threading.Tasks.Task ShowInfoDialog(string title, string message)
        {
            var dialog = new ContentDialog
            {
                Title = title,
                Content = message,
                CloseButtonText = "OK",
                XamlRoot = Content.XamlRoot
            };
            await dialog.ShowAsync();
        }
    }

    // Resource data model
    public class ResourceNode
    {
        public string Type { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string? Content { get; set; }
        public byte[]? BinaryData { get; set; }
    }

    public class ResourceFile
    {
        public ObservableCollection<ResourceNode> Resources { get; set; } = new();

        public static async System.Threading.Tasks.Task<ResourceFile> LoadAsync(string path)
        {
            // Placeholder implementation - actual loading would parse RC/RES/EXE/DLL files
            var file = new ResourceFile();
            
            // Add sample resources for demonstration
            file.Resources.Add(new ResourceNode
            {
                Type = "DIALOG",
                Name = "IDD_ABOUTBOX",
                Content = "DIALOG 0, 0, 200, 100\nCAPTION \"About\"\n{\n    // Dialog content\n}"
            });
            
            file.Resources.Add(new ResourceNode
            {
                Type = "MENU",
                Name = "IDR_MAINMENU",
                Content = "MENU\n{\n    POPUP \"&File\"\n    {\n        MENUITEM \"&Exit\", ID_FILE_EXIT\n    }\n}"
            });
            
            file.Resources.Add(new ResourceNode
            {
                Type = "STRING",
                Name = "IDS_APP_TITLE",
                Content = "\"Application Title\""
            });

            await System.Threading.Tasks.Task.CompletedTask;
            return file;
        }

        public async System.Threading.Tasks.Task SaveAsync(string path)
        {
            // Placeholder implementation - actual saving would generate RC/RES files
            var extension = Path.GetExtension(path).ToLowerInvariant();
            
            if (extension == ".rc")
            {
                // Generate RC file format
                var content = GenerateRCContent();
                await File.WriteAllTextAsync(path, content);
            }
            else
            {
                throw new NotSupportedException($"File format '{extension}' is not yet supported for saving.");
            }
        }

        private string GenerateRCContent()
        {
            var sb = new System.Text.StringBuilder();
            sb.AppendLine("// Resource script generated by RisohEditor");
            sb.AppendLine();

            foreach (var resource in Resources.GroupBy(r => r.Type))
            {
                sb.AppendLine($"// {resource.Key} resources");
                foreach (var item in resource)
                {
                    sb.AppendLine(item.Content ?? string.Empty);
                    sb.AppendLine();
                }
            }

            return sb.ToString();
        }
    }

    // Add Resource Dialog placeholder
    public class AddResourceDialog : StackPanel
    {
        public AddResourceDialog()
        {
            Spacing = 12;
            Children.Add(new ComboBox
            {
                Header = "Resource Type",
                PlaceholderText = "Select type",
                Items = { "DIALOG", "MENU", "STRING", "ICON", "BITMAP", "ACCELERATOR" }
            });
            Children.Add(new TextBox { Header = "Resource Name", PlaceholderText = "Enter name" });
        }
    }
}
