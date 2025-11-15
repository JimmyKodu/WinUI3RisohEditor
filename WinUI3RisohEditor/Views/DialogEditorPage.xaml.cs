using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using WinUI3RisohEditor.Models;

namespace WinUI3RisohEditor.Views
{
    /// <summary>
    /// Dialog resource editor page
    /// </summary>
    public sealed partial class DialogEditorPage : Page
    {
        private DialogResource? _currentDialog;

        public DialogEditorPage()
        {
            InitializeComponent();
        }

        public void LoadDialog(DialogResource dialog)
        {
            _currentDialog = dialog;
            
            if (_currentDialog != null)
            {
                DialogIdTextBox.Text = _currentDialog.Name;
                DialogCaptionTextBox.Text = _currentDialog.Caption;
                
                // Update canvas size
                DialogCanvas.Width = _currentDialog.Width;
                DialogCanvas.Height = _currentDialog.Height;
            }
        }

        private async void AddControl_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new ContentDialog
            {
                Title = "Add Control",
                Content = "Select control type to add:\n\n" +
                         "• Button\n" +
                         "• Text Box\n" +
                         "• Label\n" +
                         "• Check Box\n" +
                         "• Radio Button\n" +
                         "• List Box\n" +
                         "• Combo Box\n\n" +
                         "Control creation will be implemented.",
                CloseButtonText = "Cancel",
                XamlRoot = XamlRoot
            };
            await dialog.ShowAsync();
        }

        private void DeleteControl_Click(object sender, RoutedEventArgs e)
        {
            // TODO: Delete selected control
        }

        private void AlignLeft_Click(object sender, RoutedEventArgs e)
        {
            // TODO: Align selected controls to left
        }

        private void AlignCenter_Click(object sender, RoutedEventArgs e)
        {
            // TODO: Align selected controls to center
        }

        private void AlignRight_Click(object sender, RoutedEventArgs e)
        {
            // TODO: Align selected controls to right
        }

        private async void TestDialog_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new ContentDialog
            {
                Title = _currentDialog?.Caption ?? "Test Dialog",
                Content = "This would show a preview of the dialog as it would appear in a real application.\n\n" +
                         "Dialog preview functionality will be implemented.",
                CloseButtonText = "Close",
                XamlRoot = XamlRoot
            };
            await dialog.ShowAsync();
        }
    }
}
