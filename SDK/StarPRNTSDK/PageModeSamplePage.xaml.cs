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
    public sealed partial class PageModeSamplePage : Page
    {
        private enum FunctionType
        {
            TextLabelRotation0,
            TextLabelRotation90,
            TextLabelRotation180,
            TextLabelRotation270
        }

        public PageModeSamplePage()
        {
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            PrinterSettings printerSettings = new PrinterSettings();
            ILocalizeReceipts localizeReceipts = ILocalizeReceipts.createLocalizeReceipts(printerSettings.GetLanguage(), printerSettings.GetPaperSize(true), printerSettings);

            this.SetAllFunctionList(localizeReceipts.LanguageCode, localizeReceipts.PaperSizeStr);
        }

        private void SetAllFunctionList(string languageCode, string paperSizeStr)
        {
            this.SetFunction(this.TextLabelRotation0TextBlock, languageCode + " " + paperSizeStr + "" + "Text Label (Rotation0)", FunctionType.TextLabelRotation0);
            this.SetFunction(this.TextLabelRotation90TextBlock, languageCode + " " + paperSizeStr + "" + "Text Label (Rotation90)", FunctionType.TextLabelRotation90);
            this.SetFunction(this.TextLabelRotation180TextBlock, languageCode + " " + paperSizeStr + "" + "Text Label (Rotation180)", FunctionType.TextLabelRotation180);
            this.SetFunction(this.TextLabelRotation270TextBlock, languageCode + " " + paperSizeStr + "" + "Text Label (Rotation270)", FunctionType.TextLabelRotation270);
        }

        private void SetFunction(TextBlock functionTextBlock, string functionTitle, FunctionType functionType)
        {
            functionTextBlock.Text = functionTitle;

            functionTextBlock.Tapped += (sender, e) => this.CallFunction(sender, e, functionType);
        }

        private bool IsPrintReceiptFunction(FunctionType functionType)
        {
            return (functionType == FunctionType.TextLabelRotation0) ||
                   (functionType == FunctionType.TextLabelRotation90) ||
                   (functionType == FunctionType.TextLabelRotation180) ||
                   (functionType == FunctionType.TextLabelRotation270);
        }

        private void CallFunction(object sender, TappedRoutedEventArgs e, FunctionType functionType)
        {
            if (this.IsPrintReceiptFunction(functionType))
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
                    result = await Communication.SendCommands(commands, printerSettings.GetPortName(true), printerSettings.GetPortSettings(true), 30000);     // 30000mS!!!

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

            PageModePrintRegion printRegion;
            int height = 30 * 8;
            int width = localizeReceipts.PaperSize;
            BitmapConverterRotation rotation;

            switch (functionType)
            {
                default:
                case FunctionType.TextLabelRotation0:
                    printRegion = new PageModePrintRegion(0, 0, width, height);
                    rotation = BitmapConverterRotation.Normal;
                    break;

                case FunctionType.TextLabelRotation90:
                    printRegion = new PageModePrintRegion(0, 0, width, width);
                    rotation = BitmapConverterRotation.Right90;
                    break;

                case FunctionType.TextLabelRotation180:
                    printRegion = new PageModePrintRegion(0, 0, width, height);
                    rotation = BitmapConverterRotation.Rotate180;
                    break;

                case FunctionType.TextLabelRotation270:
                    printRegion = new PageModePrintRegion(0, 0, height, width);
                    rotation = BitmapConverterRotation.Left90;
                    break;
            }

            commands = PrinterFunctions.CreateTextPageModeData(emulation, localizeReceipts, printRegion, rotation, false);

            return commands;
        }
    }
}
