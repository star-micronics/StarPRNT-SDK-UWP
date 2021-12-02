using Windows.UI.Xaml.Controls;

namespace StarPRNTSDK
{
    public sealed partial class SelectBoolContentDialog : ContentDialog
    {


        public SelectBoolContentDialog(string title, string message, string primaryButtonText, string secondaryButtonText)
        {
            this.InitializeComponent();

            this.Title = title;

            this.MessageTextBlock.Text = message;

            this.PrimaryButtonText = primaryButtonText;

            this.SecondaryButtonText = secondaryButtonText;
        }

        private void ContentDialog_PrimaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
        }

        private void ContentDialog_SecondaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
        }
    }
}
