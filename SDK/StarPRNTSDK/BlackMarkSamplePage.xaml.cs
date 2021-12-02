using StarIO_Extension;
using StarPRNTSDK.Functions;
using StarPRNTSDK.Localizereceipts;
using System;
using Windows.Storage.Streams;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Navigation;

namespace StarPRNTSDK
{

    public sealed partial class BlackMarkSamplePage : Page
    {
        private enum FunctionType
        {
            TextLabel
        }

        public BlackMarkSamplePage()
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

            this.SetAllFunctionList(localizeReceipts.LanguageCode);
        }

        private void SetAllFunctionList(string languageCode)
        {
            this.SetFunction(this.TextLabelTextBlock, languageCode + " " + "Text Label", FunctionType.TextLabel);
        }

        private void SetFunction(TextBlock functionTextBlock, string functionTitle, FunctionType functionType)
        {
            functionTextBlock.Text = functionTitle;

            functionTextBlock.Tapped += (sender, e) => this.CallFunction(sender, e, functionType);
        }

        private bool IsPrintReceiptFunction(FunctionType functionType)
        {
            return (functionType == FunctionType.TextLabel);
        }

        private void CallFunction(object sender, TappedRoutedEventArgs e, FunctionType functionType)
        {
            if(this.IsPrintReceiptFunction(functionType))
            {
                this.PrintReceipt(functionType);
            }
        }

        private async void PrintReceipt(FunctionType functionType)
        {
            var progressRing = new ProgressRing();
            CommunicationResult result;
            string message = null;

            try
            {
                PrinterSettings printerSettings = new PrinterSettings();
                ILocalizeReceipts localizeReceipts = ILocalizeReceipts.createLocalizeReceipts(printerSettings.GetLanguage(), printerSettings.GetPaperSize(true), printerSettings);

                IBuffer commands = this.CreateCommands(functionType, localizeReceipts, printerSettings.GetEmulation(true));

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

        private IBuffer CreateCommands(FunctionType functionType, ILocalizeReceipts localizeReceipts, Emulation emulation)
        {
            IBuffer commands = null;

            switch (functionType)
            {
                case FunctionType.TextLabel:
                    commands = PrinterFunctions.CreateTextBlackMarkData(emulation, localizeReceipts, this.GetBlackMarkType(), false);
                    break;
            }

            return commands;
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
