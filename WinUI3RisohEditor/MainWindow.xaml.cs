using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Windows.Storage;
using Windows.Storage.Pickers;

namespace WinUI3RisohEditor
{
    /// <summary>
    /// Main window for the Risoh Editor application
    /// </summary>
    public sealed partial class MainWindow : Window
    {
        private string? _currentFilePath;
        private ObservableCollection<ResourceTreeItem> _resourceItems;

        public MainWindow()
        {
            InitializeComponent();
            Title = "Risoh Editor - WinUI3";
            _resourceItems = new ObservableCollection<ResourceTreeItem>();
            InitializeResourceTree();
        }

        private void InitializeResourceTree()
        {
            // Initialize with default resource categories
            _resourceItems.Clear();
            _resourceItems.Add(new ResourceTreeItem("Accelerators", "\uE765"));
            _resourceItems.Add(new ResourceTreeItem("Bitmaps", "\uE91B"));
            _resourceItems.Add(new ResourceTreeItem("Cursors", "\uE962"));
            _resourceItems.Add(new ResourceTreeItem("Dialogs", "\uE8A9"));
            _resourceItems.Add(new ResourceTreeItem("Icons", "\uE745"));
            _resourceItems.Add(new ResourceTreeItem("Menus", "\uE700"));
            _resourceItems.Add(new ResourceTreeItem("String Tables", "\uE8C8"));
            _resourceItems.Add(new ResourceTreeItem("Version Info", "\uE946"));
            _resourceItems.Add(new ResourceTreeItem("Custom Resources", "\uE8B7"));

            ResourceTreeView.ItemsSource = _resourceItems;
        }

        // File Menu Handlers
        private void NewFile_Click(object sender, RoutedEventArgs e)
        {
            _currentFilePath = null;
            InitializeResourceTree();
            FilePathText.Text = "New File";
            StatusText.Text = "New resource file created";
            ResourceTitleText.Text = "No resource selected";
            ResourceContent.Children.Clear();
            ResourceContent.Children.Add(new TextBlock 
            { 
                Text = "Select a resource from the tree to view or edit it.",
                TextWrapping = TextWrapping.Wrap
            });
        }

        private async void OpenFile_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var picker = new FileOpenPicker();
                var hwnd = WinRT.Interop.WindowNative.GetWindowHandle(this);
                WinRT.Interop.InitializeWithWindow.Initialize(picker, hwnd);
                
                picker.FileTypeFilter.Add(".rc");
                picker.FileTypeFilter.Add(".res");
                picker.FileTypeFilter.Add(".exe");
                picker.FileTypeFilter.Add(".dll");
                picker.SuggestedStartLocation = PickerLocationId.DocumentsLibrary;

                var file = await picker.PickSingleFileAsync();
                if (file != null)
                {
                    _currentFilePath = file.Path;
                    FilePathText.Text = file.Path;
                    StatusText.Text = $"Opened: {file.Name}";
                    
                    // TODO: Load resource file
                    await ShowInfoDialogAsync("Open File", $"File opened: {file.Name}\n\nResource loading functionality will be implemented.");
                }
            }
            catch (Exception ex)
            {
                await ShowErrorDialogAsync("Error Opening File", ex.Message);
            }
        }

        private async void SaveFile_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(_currentFilePath))
            {
                SaveFileAs_Click(sender, e);
                return;
            }

            try
            {
                // TODO: Implement save functionality
                StatusText.Text = $"Saved: {Path.GetFileName(_currentFilePath)}";
                await ShowInfoDialogAsync("Save File", "Save functionality will be implemented.");
            }
            catch (Exception ex)
            {
                await ShowErrorDialogAsync("Error Saving File", ex.Message);
            }
        }

        private async void SaveFileAs_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var picker = new FileSavePicker();
                var hwnd = WinRT.Interop.WindowNative.GetWindowHandle(this);
                WinRT.Interop.InitializeWithWindow.Initialize(picker, hwnd);
                
                picker.FileTypeChoices.Add("Resource Script", new List<string> { ".rc" });
                picker.FileTypeChoices.Add("Resource File", new List<string> { ".res" });
                picker.SuggestedStartLocation = PickerLocationId.DocumentsLibrary;
                picker.SuggestedFileName = "NewResource";

                var file = await picker.PickSaveFileAsync();
                if (file != null)
                {
                    _currentFilePath = file.Path;
                    FilePathText.Text = file.Path;
                    StatusText.Text = $"Saved as: {file.Name}";
                    
                    // TODO: Implement save functionality
                    await ShowInfoDialogAsync("Save File", $"File will be saved as: {file.Name}\n\nSave functionality will be implemented.");
                }
            }
            catch (Exception ex)
            {
                await ShowErrorDialogAsync("Error Saving File", ex.Message);
            }
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Exit();
        }

        // Edit Menu Handlers
        private async void AddResource_Click(object sender, RoutedEventArgs e)
        {
            await ShowInfoDialogAsync("Add Resource", "Add resource functionality will be implemented.\n\nYou will be able to add new resources like dialogs, menus, icons, etc.");
            StatusText.Text = "Add Resource clicked";
        }

        private async void DeleteResource_Click(object sender, RoutedEventArgs e)
        {
            var selectedItem = ResourceTreeView.SelectedItem as ResourceTreeItem;
            if (selectedItem != null && selectedItem.Parent != null)
            {
                var dialog = new ContentDialog
                {
                    Title = "Delete Resource",
                    Content = $"Are you sure you want to delete '{selectedItem.Name}'?",
                    PrimaryButtonText = "Delete",
                    CloseButtonText = "Cancel",
                    DefaultButton = ContentDialogButton.Close,
                    XamlRoot = Content.XamlRoot
                };

                var result = await dialog.ShowAsync();
                if (result == ContentDialogResult.Primary)
                {
                    // TODO: Implement delete functionality
                    StatusText.Text = $"Deleted: {selectedItem.Name}";
                }
            }
            else
            {
                await ShowInfoDialogAsync("Delete Resource", "Please select a specific resource to delete (not a category).");
            }
        }

        private async void EditResource_Click(object sender, RoutedEventArgs e)
        {
            var selectedItem = ResourceTreeView.SelectedItem as ResourceTreeItem;
            if (selectedItem != null && selectedItem.Parent != null)
            {
                await ShowInfoDialogAsync("Edit Resource", $"Editing '{selectedItem.Name}'\n\nResource editor will be implemented.");
                StatusText.Text = $"Editing: {selectedItem.Name}";
            }
            else
            {
                await ShowInfoDialogAsync("Edit Resource", "Please select a specific resource to edit (not a category).");
            }
        }

        // View Menu Handlers
        private void Refresh_Click(object sender, RoutedEventArgs e)
        {
            InitializeResourceTree();
            StatusText.Text = "Resource tree refreshed";
        }

        // Help Menu Handlers
        private async void About_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new ContentDialog
            {
                Title = "About Risoh Editor",
                Content = "Risoh Editor - WinUI3 Edition\n\n" +
                         "A modern resource editor for Windows development.\n\n" +
                         "This is a WinUI3 replication of the original RisohEditor by katahiromz.\n" +
                         "Original project: https://github.com/katahiromz/RisohEditor\n\n" +
                         "Version: 1.0.0 (Preview)",
                CloseButtonText = "Close",
                XamlRoot = Content.XamlRoot
            };
            await dialog.ShowAsync();
        }

        // Event Handlers
        private void ResourceTreeView_SelectionChanged(TreeView sender, TreeViewSelectionChangedEventArgs args)
        {
            if (args.AddedItems.Count > 0)
            {
                var selectedItem = args.AddedItems[0] as ResourceTreeItem;
                if (selectedItem != null)
                {
                    ResourceTitleText.Text = selectedItem.Name;
                    ResourceContent.Children.Clear();
                    
                    if (selectedItem.Parent == null)
                    {
                        // Category selected
                        ResourceContent.Children.Add(new TextBlock 
                        { 
                            Text = $"Category: {selectedItem.Name}\n\nSelect a specific resource to view or edit it.",
                            TextWrapping = TextWrapping.Wrap
                        });
                    }
                    else
                    {
                        // Specific resource selected
                        ResourceContent.Children.Add(new TextBlock 
                        { 
                            Text = $"Resource: {selectedItem.Name}\n\nResource editor will be implemented here.",
                            TextWrapping = TextWrapping.Wrap
                        });
                    }
                    
                    StatusText.Text = $"Selected: {selectedItem.Name}";
                }
            }
        }

        // Helper Methods
        private async System.Threading.Tasks.Task ShowInfoDialogAsync(string title, string content)
        {
            var dialog = new ContentDialog
            {
                Title = title,
                Content = content,
                CloseButtonText = "OK",
                XamlRoot = Content.XamlRoot
            };
            await dialog.ShowAsync();
        }

        private async System.Threading.Tasks.Task ShowErrorDialogAsync(string title, string content)
        {
            var dialog = new ContentDialog
            {
                Title = title,
                Content = content,
                CloseButtonText = "OK",
                XamlRoot = Content.XamlRoot
            };
            await dialog.ShowAsync();
        }
    }

    // Helper class for resource tree items
    public class ResourceTreeItem
    {
        public string Name { get; set; }
        public string Icon { get; set; }
        public ResourceTreeItem? Parent { get; set; }
        public ObservableCollection<ResourceTreeItem> Children { get; set; }

        public ResourceTreeItem(string name, string icon)
        {
            Name = name;
            Icon = icon;
            Children = new ObservableCollection<ResourceTreeItem>();
        }

        public void AddChild(ResourceTreeItem child)
        {
            child.Parent = this;
            Children.Add(child);
        }
    }
}
