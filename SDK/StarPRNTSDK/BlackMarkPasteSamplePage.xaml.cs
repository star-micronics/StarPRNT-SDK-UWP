using StarIO_Extension;
using StarPRNTSDK.Functions;
using StarPRNTSDK.Localizereceipts;
using System;
using Windows.Storage.Streams;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace StarPRNTSDK
{
    public sealed partial class BlackMarkPasteSamplePage : Page
    {
        public BlackMarkPasteSamplePage()
        {
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            PrinterSettings printerSettings    = new PrinterSettings();
            ILocalizeReceipts localizeReceipts = ILocalizeReceipts.createLocalizeReceipts(printerSettings.GetLanguage(), printerSettings.GetPaperSize(true), printerSettings);

            ModelDictionary modelDic             = new ModelDictionary();
            PrinterInfo info                     = modelDic.GetPrinterInfo(printerSettings.GetModelTitle(true));
            this.DetectionToggleSwitch.IsEnabled = info.BlackMarkDetectionIsEnabled;

            this.SetAllButton();

            this.PasteTextTextBox.Text = localizeReceipts.CreatePasteTextLabelString();
        }

        private void SetAllButton()
        {
            this.ClearButton.Tapped += ClearButtonTapped;

            this.PrintButton.Tapped += PrintButtonTapped;
        }

        private void ClearButtonTapped(object sender, RoutedEventArgs e)
        {
            this.ClearPasteText();
        }

        private void PrintButtonTapped(object sender, RoutedEventArgs e)
        {
            this.PrintPasteText();
        }

        private void ClearPasteText()
        {
            this.PasteTextTextBox.Text = "";
        }

        private async void PrintPasteText()
        {
            var progressRing = new ProgressRing();
            CommunicationResult result;
            string message = null;

            try
            {
                PrinterSettings printerSettings = new PrinterSettings();
                ILocalizeReceipts localizeReceipts = ILocalizeReceipts.createLocalizeReceipts(printerSettings.GetLanguage(), printerSettings.GetPaperSize(true), printerSettings);
                string pasteText = this.PasteTextTextBox.Text.Replace("\r", "\n");
                bool doubleHeight = this.DoubleHeightToggleSwitch.IsOn;

                IBuffer commands = PrinterFunctions.CreatePasteTextBlackMarkData(printerSettings.GetEmulation(true), localizeReceipts, pasteText, doubleHeight, this.GetBlackMarkType(), false);

                progressRing.Message = "Communicating...";
                progressRing.Show();

                if (commands != null)
                {
                    string portSettings = printerSettings.GetPortSettings(true);

                    result = await Communication.SendCommands(commands, printerSettings.GetPortName(true), portSettings, 30000);     // 30000mS!!!

                    message = Communication.GetCommunicationResultMessage(result);
                }
            }
            catch (Exception)
            {

            }
            finally
            {
                progressRing.Hide();
            }

            if (message != null)
            {
                Windows.UI.Popups.MessageDialog dialog = new Windows.UI.Popups.MessageDialog(message, "Communication Result");

                await dialog.ShowAsync();
            }
        }

        private BlackMarkType GetBlackMarkType()
        {
            BlackMarkType type;

            if (this.DetectionToggleSwitch.IsOn)
            {
                type = BlackMarkType.ValidWithDetection;
            }
            else
            {
                type = BlackMarkType.Valid;
            }

            return type;
        }
    }
}
