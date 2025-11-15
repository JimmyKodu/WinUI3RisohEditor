using System.Collections.ObjectModel;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using WinUI3RisohEditor.Models;

namespace WinUI3RisohEditor.Views
{
    /// <summary>
    /// Menu resource editor page
    /// </summary>
    public sealed partial class MenuEditorPage : Page
    {
        private MenuResource? _currentMenu;
        private ObservableCollection<MenuItemViewModel> _menuItems;

        public MenuEditorPage()
        {
            InitializeComponent();
            _menuItems = new ObservableCollection<MenuItemViewModel>();
            MenuTreeView.ItemsSource = _menuItems;
        }

        public void LoadMenu(MenuResource menu)
        {
            _currentMenu = menu;
            _menuItems.Clear();

            if (_currentMenu != null)
            {
                foreach (var item in _currentMenu.Items)
                {
                    _menuItems.Add(ConvertToViewModel(item));
                }
            }
        }

        private MenuItemViewModel ConvertToViewModel(MenuItem item)
        {
            var viewModel = new MenuItemViewModel
            {
                Text = item.Text,
                Id = item.Id,
                IdText = item.Id > 0 ? $"(ID: {item.Id})" : string.Empty,
                Flags = item.Flags
            };

            foreach (var subItem in item.SubItems)
            {
                viewModel.SubItems.Add(ConvertToViewModel(subItem));
            }

            return viewModel;
        }

        private async void AddMenuItem_Click(object sender, RoutedEventArgs e)
        {
            var textBox = new TextBox
            {
                Header = "Menu Item Text",
                PlaceholderText = "Enter menu item text"
            };

            var idBox = new TextBox
            {
                Header = "Menu Item ID",
                PlaceholderText = "IDM_ITEM"
            };

            var panel = new StackPanel { Spacing = 8 };
            panel.Children.Add(textBox);
            panel.Children.Add(idBox);

            var dialog = new ContentDialog
            {
                Title = "Add Menu Item",
                Content = panel,
                PrimaryButtonText = "Add",
                CloseButtonText = "Cancel",
                DefaultButton = ContentDialogButton.Primary,
                XamlRoot = XamlRoot
            };

            var result = await dialog.ShowAsync();
            if (result == ContentDialogResult.Primary)
            {
                var newItem = new MenuItemViewModel
                {
                    Text = textBox.Text,
                    IdText = idBox.Text
                };

                // Add to selected parent or root
                if (MenuTreeView.SelectedItem is MenuItemViewModel parent && parent.SubItems.Count > 0)
                {
                    parent.SubItems.Add(newItem);
                }
                else
                {
                    _menuItems.Add(newItem);
                }
            }
        }

        private async void AddSubmenu_Click(object sender, RoutedEventArgs e)
        {
            var textBox = new TextBox
            {
                Header = "Submenu Text",
                PlaceholderText = "Enter submenu text"
            };

            var dialog = new ContentDialog
            {
                Title = "Add Submenu",
                Content = textBox,
                PrimaryButtonText = "Add",
                CloseButtonText = "Cancel",
                DefaultButton = ContentDialogButton.Primary,
                XamlRoot = XamlRoot
            };

            var result = await dialog.ShowAsync();
            if (result == ContentDialogResult.Primary)
            {
                var newSubmenu = new MenuItemViewModel
                {
                    Text = textBox.Text,
                    Id = 0,
                    IdText = "(Submenu)"
                };

                if (MenuTreeView.SelectedItem is MenuItemViewModel parent)
                {
                    parent.SubItems.Add(newSubmenu);
                }
                else
                {
                    _menuItems.Add(newSubmenu);
                }
            }
        }

        private async void DeleteMenuItem_Click(object sender, RoutedEventArgs e)
        {
            if (MenuTreeView.SelectedItem is MenuItemViewModel item)
            {
                var dialog = new ContentDialog
                {
                    Title = "Delete Menu Item",
                    Content = $"Are you sure you want to delete '{item.Text}'?",
                    PrimaryButtonText = "Delete",
                    CloseButtonText = "Cancel",
                    DefaultButton = ContentDialogButton.Close,
                    XamlRoot = XamlRoot
                };

                var result = await dialog.ShowAsync();
                if (result == ContentDialogResult.Primary)
                {
                    // Find and remove the item
                    RemoveMenuItem(_menuItems, item);
                }
            }
        }

        private bool RemoveMenuItem(ObservableCollection<MenuItemViewModel> items, MenuItemViewModel target)
        {
            if (items.Remove(target))
                return true;

            foreach (var item in items)
            {
                if (RemoveMenuItem(item.SubItems, target))
                    return true;
            }

            return false;
        }

        private void MoveUp_Click(object sender, RoutedEventArgs e)
        {
            // TODO: Implement move up functionality
        }

        private void MoveDown_Click(object sender, RoutedEventArgs e)
        {
            // TODO: Implement move down functionality
        }

        private async void TestMenu_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new ContentDialog
            {
                Title = "Test Menu",
                Content = "This would show a preview of the menu as it would appear in a real application.\n\n" +
                         "Menu preview functionality will be implemented.",
                CloseButtonText = "Close",
                XamlRoot = XamlRoot
            };
            await dialog.ShowAsync();
        }

        public class MenuItemViewModel
        {
            public string Text { get; set; } = string.Empty;
            public int Id { get; set; }
            public string IdText { get; set; } = string.Empty;
            public uint Flags { get; set; }
            public ObservableCollection<MenuItemViewModel> SubItems { get; set; } = new();
        }
    }
}
