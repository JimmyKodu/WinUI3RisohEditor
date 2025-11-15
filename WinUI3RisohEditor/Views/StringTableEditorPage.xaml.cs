using System.Collections.ObjectModel;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using WinUI3RisohEditor.Models;

namespace WinUI3RisohEditor.Views
{
    /// <summary>
    /// String table resource editor page
    /// </summary>
    public sealed partial class StringTableEditorPage : Page
    {
        private StringTableResource? _currentStringTable;
        private ObservableCollection<StringEntry> _strings;

        public StringTableEditorPage()
        {
            InitializeComponent();
            _strings = new ObservableCollection<StringEntry>();
            StringsListView.ItemsSource = _strings;
        }

        public void LoadStringTable(StringTableResource stringTable)
        {
            _currentStringTable = stringTable;
            _strings.Clear();

            if (_currentStringTable != null)
            {
                foreach (var kvp in _currentStringTable.Strings)
                {
                    _strings.Add(new StringEntry { Id = kvp.Key, Value = kvp.Value });
                }
            }
        }

        private async void AddString_Click(object sender, RoutedEventArgs e)
        {
            var idBox = new NumberBox
            {
                Header = "String ID",
                Value = 1,
                SpinButtonPlacementMode = NumberBoxSpinButtonPlacementMode.Compact,
                Minimum = 1,
                Maximum = 65535
            };

            var valueBox = new TextBox
            {
                Header = "String Value",
                PlaceholderText = "Enter string value",
                TextWrapping = TextWrapping.Wrap,
                AcceptsReturn = true,
                Height = 100
            };

            var panel = new StackPanel { Spacing = 8 };
            panel.Children.Add(idBox);
            panel.Children.Add(valueBox);

            var dialog = new ContentDialog
            {
                Title = "Add String",
                Content = panel,
                PrimaryButtonText = "Add",
                CloseButtonText = "Cancel",
                DefaultButton = ContentDialogButton.Primary,
                XamlRoot = XamlRoot
            };

            var result = await dialog.ShowAsync();
            if (result == ContentDialogResult.Primary)
            {
                var id = (int)idBox.Value;
                var value = valueBox.Text;

                if (!string.IsNullOrWhiteSpace(value))
                {
                    _strings.Add(new StringEntry { Id = id, Value = value });
                    
                    if (_currentStringTable != null)
                    {
                        _currentStringTable.Strings[id] = value;
                    }
                }
            }
        }

        private async void DeleteString_Click(object sender, RoutedEventArgs e)
        {
            if (StringsListView.SelectedItem is StringEntry entry)
            {
                var dialog = new ContentDialog
                {
                    Title = "Delete String",
                    Content = $"Are you sure you want to delete string ID {entry.Id}?",
                    PrimaryButtonText = "Delete",
                    CloseButtonText = "Cancel",
                    DefaultButton = ContentDialogButton.Close,
                    XamlRoot = XamlRoot
                };

                var result = await dialog.ShowAsync();
                if (result == ContentDialogResult.Primary)
                {
                    _strings.Remove(entry);
                    
                    if (_currentStringTable != null)
                    {
                        _currentStringTable.Strings.Remove(entry.Id);
                    }
                }
            }
        }

        private async void SaveChanges_Click(object sender, RoutedEventArgs e)
        {
            // Update the string table resource
            if (_currentStringTable != null)
            {
                _currentStringTable.Strings.Clear();
                foreach (var entry in _strings)
                {
                    _currentStringTable.Strings[entry.Id] = entry.Value;
                }
            }

            var dialog = new ContentDialog
            {
                Title = "Changes Saved",
                Content = "String table changes have been saved.",
                CloseButtonText = "OK",
                XamlRoot = XamlRoot
            };
            await dialog.ShowAsync();
        }

        public class StringEntry
        {
            public int Id { get; set; }
            public string Value { get; set; } = string.Empty;
        }
    }
}
