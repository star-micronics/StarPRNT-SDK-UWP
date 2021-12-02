using StarIO_Extension;
using StarPRNTSDK.Functions;
using StarPRNTSDK.Localizereceipts;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Windows.Media.Capture;
using Windows.Storage;
using Windows.Storage.Pickers;
using Windows.Storage.Streams;
using Windows.UI;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Animation;
using Windows.UI.Xaml.Navigation;


namespace StarPRNTSDK
{

    public sealed partial class PrintRedirectionSamplePage : Page
    {

        private struct PrintRedirectionSampleFunction
        {
            public string title { get; }
            public PrintRedirectionSampleFunctionFunctionType functionType { get; }

            public PrintRedirectionSampleFunction(string title, PrintRedirectionSampleFunctionFunctionType functionType)
            {
                this.title = title;
                this.functionType = functionType;
            }
        }
        public enum PrintRedirectionSampleFunctionFunctionType
        {
            TextReceipt,
            TextReceiptUTF8,
            RasterReceipt,
            RasterReceiptBothScale,
            RasterReceiptScale,
            RasterCoupon,
            RasterCoupon90,
        }

        public PrintRedirectionSamplePage()
        {
            this.InitializeComponent();

            this.Loaded += PrintRedirectionSamplePage_Loaded;

            this.SetComponents();
        }

        private async void PrintRedirectionSamplePage_Loaded(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            PrinterSettings printerSettings = new PrinterSettings();
            PrinterSettingsInfo[] settingdata = await printerSettings.LoadSettings();

            this.InitializeUI(printerSettings);


        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            (this.Resources["Storyboard1"] as Storyboard).Stop();
        }

        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            PrinterSettings printerSettings = new PrinterSettings();
            PrinterSettingsInfo[] settingdata = await printerSettings.LoadSettings();

            this.InitializeUI(printerSettings);


            for (var index = Frame.BackStackDepth - 1; index >= 0; index--)
            {
                var entry = Frame.BackStack[index];

                var page = entry.SourcePageType.ToString();

                if (page.Contains("PrinterSettingsPage") ||
                    page.Contains("SearchPortSamplePage") ||
                    page.Contains("PrintRedirectionSamplePage"))
                    Frame.BackStack.RemoveAt(index);
            }
        }

        private void InitializeUI(PrinterSettings printerSettings)
        {
            this.SetSelectedPrinterInfo(printerSettings);
        }

        private void SetSelectedPrinterInfo(PrinterSettings printerSettings)
        {
            if (printerSettings.GetPortName(false) != null)
            {
                this.BackupPrinterNameTextBlock.Text = printerSettings.GetModelName(false);

                this.BackupPrinterPortNameTextBlock.Text = printerSettings.GetPortName(false);

                this.BackupPrinterMacAddressTextBlock.Text = printerSettings.GetMacAddress(false);

                this.BackupPrinterNameTextBlock.Foreground = new SolidColorBrush(Colors.Blue);

                this.SetDisableFunctions();

                (this.Resources["Storyboard1"] as Storyboard).Stop();

            }
            else
            {
                (this.Resources["Storyboard1"] as Storyboard).Begin();

                this.BackupPrinterNameTextBlock.Text = "Unselected State";
                this.BackupPrinterNameTextBlock.Foreground = new SolidColorBrush(Colors.Red);

            }
        }

        private void SetComponents()
        {
            this.SetAllFunctions();

            this.SetAllFunctionsIsEnabled();
        }

        private void SetDisableFunctions()
        {
            PrinterSettings printerSettings = new PrinterSettings();
            string modelTitle = printerSettings.GetModelTitle(true);
            ILocalizeReceipts localizeReceipts = ILocalizeReceipts.createLocalizeReceipts(printerSettings.GetLanguage(), printerSettings.GetPaperSize(true), printerSettings);


            ModelDictionary modelDic = new ModelDictionary();
            PrinterInfo printerInfo = modelDic.GetPrinterInfo(modelTitle);

            bool CJKIsEnabled = localizeReceipts.LanguageCode.Contains("CJK");


            this.SetFunctionIsEnabled(this.TextReceit,                true == CJKIsEnabled ? printerInfo.CJKIsEnabled : printerInfo.TextReceiptIsEnabled);
            this.SetFunctionIsEnabled(this.TextReceitUTF8,            true == CJKIsEnabled ? printerInfo.CJKIsEnabled : printerInfo.UTF8IsEnabled);
            this.SetFunctionIsEnabled(this.RasterReceipt,             true == CJKIsEnabled ? printerInfo.CJKIsEnabled : printerInfo.RasterReceiptIsEnabled);
            this.SetFunctionIsEnabled(this.RasterReceiptBothScale,    true == CJKIsEnabled ? printerInfo.CJKIsEnabled : printerInfo.RasterReceiptIsEnabled);
            this.SetFunctionIsEnabled(this.RasterReceiptScale,        true == CJKIsEnabled ? printerInfo.CJKIsEnabled : printerInfo.RasterReceiptIsEnabled);
            this.SetFunctionIsEnabled(this.RasterCoupon,              true == CJKIsEnabled ? printerInfo.CJKIsEnabled : true);
            this.SetFunctionIsEnabled(this.RasterCouponRotation90,    true == CJKIsEnabled ? printerInfo.CJKIsEnabled : true);



            this.DisableFunction(TextReceitStackPanel, CJKIsEnabled);
            this.DisableFunction(RasterReceiptStackPanel, CJKIsEnabled);
            this.DisableFunction(RasterReceiptBothScaleStackPanel, CJKIsEnabled);
            this.DisableFunction(RasterReceiptScaleStackPanel, CJKIsEnabled);
            this.DisableFunction(RasterCouponStackPanel, CJKIsEnabled);
            this.DisableFunction(RasterCouponRotation90StackPanel, CJKIsEnabled);

        }

        private void DisableFunction(StackPanel stackPanel, bool isEnabled)
        {
            if (isEnabled)
            {
                stackPanel.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
            }
            else
            {
                stackPanel.Visibility = Windows.UI.Xaml.Visibility.Visible;
            }
        }

        private void SetAllFunctions()
        {
            foreach (KeyValuePair<TextBlock, PrintRedirectionSampleFunction> pair in this.GetAllFunctions())
            {
                TextBlock textBlock = pair.Key;
                PrintRedirectionSampleFunction function = pair.Value;

                textBlock.Text = function.title;

                textBlock.Tapped += (sender, e) => this.CallFunction(sender, e, function);
            }
        }

        private void SetFunctionIsEnabled(TextBlock textBlock, bool isEnabled)
        {
            double opacity = 1;

            if (!isEnabled)
            {
                opacity = 0.2;
            }

            textBlock.Opacity = opacity;

            textBlock.IsTapEnabled = isEnabled;
        }
        private async void SetAllFunctionsIsEnabled()
        {
            bool isBackupPrinter = false;
            PrinterSettings printerSettings = new PrinterSettings();
            PrinterSettingsInfo[] settingdata = await printerSettings.LoadSettings();

            if (printerSettings.GetPortName(false) != null)
            {
                isBackupPrinter = true;
            }

            foreach (KeyValuePair<TextBlock, PrintRedirectionSampleFunction> pair in this.GetAllFunctions())
            {
                TextBlock textBlock = pair.Key;

                this.SetFunctionIsEnabled(textBlock, isBackupPrinter);
            }

            if (printerSettings.GetPortName(false) != null)
            {
                this.SetDisableFunctions();
            }


        }

        private void CallFunction(object sender, TappedRoutedEventArgs e, PrintRedirectionSampleFunction function)
        {
            this.PrintReceipt(function.functionType);
        }

        private Dictionary<TextBlock, PrintRedirectionSampleFunction> GetAllFunctions()
        {
            PrinterSettings printerSettings = new PrinterSettings();

            ILocalizeReceipts localizeReceipts = ILocalizeReceipts.createLocalizeReceipts(printerSettings.GetLanguage(), printerSettings.GetPaperSize(true), printerSettings);

            Dictionary<TextBlock, PrintRedirectionSampleFunction> allFunctionsList = new Dictionary<TextBlock, PrintRedirectionSampleFunction>();

            allFunctionsList.Add(this.TextReceit,
                                 new PrintRedirectionSampleFunction(localizeReceipts.LanguageCode + " " + localizeReceipts.PaperSizeStr + " " + "Text Receipt",
                                 PrintRedirectionSampleFunctionFunctionType.TextReceipt));

            allFunctionsList.Add(this.TextReceitUTF8,
                                 new PrintRedirectionSampleFunction(localizeReceipts.LanguageCode + " " + localizeReceipts.PaperSizeStr + " " + "Text Receipt(UTF8)",
                                 PrintRedirectionSampleFunctionFunctionType.TextReceiptUTF8));

            allFunctionsList.Add(this.RasterReceipt,
                                 new PrintRedirectionSampleFunction(localizeReceipts.LanguageCode + " " + localizeReceipts.PaperSizeStr + " " + "Raster Receipt",
                                 PrintRedirectionSampleFunctionFunctionType.RasterReceipt));

            allFunctionsList.Add(this.RasterReceiptBothScale,
                                 new PrintRedirectionSampleFunction(localizeReceipts.LanguageCode + " " + localizeReceipts.ScalePaperSizeStr + " " + "Raster Receipt(Both Scale)",
                                 PrintRedirectionSampleFunctionFunctionType.RasterReceiptBothScale));

            allFunctionsList.Add(this.RasterReceiptScale,
                                 new PrintRedirectionSampleFunction(localizeReceipts.LanguageCode + " " + localizeReceipts.ScalePaperSizeStr + " " + "Raster Receipt(Scale)",
                                 PrintRedirectionSampleFunctionFunctionType.RasterReceiptScale));

            allFunctionsList.Add(this.RasterCoupon,
                                 new PrintRedirectionSampleFunction(localizeReceipts.LanguageCode + " " + "Raster Coupon",
                                 PrintRedirectionSampleFunctionFunctionType.RasterCoupon));

            allFunctionsList.Add(this.RasterCouponRotation90,
                                 new PrintRedirectionSampleFunction(localizeReceipts.LanguageCode + " " + "Raster Coupon(Rotation90)",
                                 PrintRedirectionSampleFunctionFunctionType.RasterCoupon90));



            return allFunctionsList;
        }


        private async void PrintReceipt(PrintRedirectionSampleFunctionFunctionType functionType)
        {
            var progressRing = new ProgressRing();
            Dictionary<string, CommunicationResult> result;

            string message = null;

            try
            {
                progressRing.Message = "Communicating...";

                progressRing.Show();

                PrinterSettings printerSettings = new PrinterSettings();
                uint paperSize = (uint)printerSettings.GetPaperSize(true);
                int languageIndex = printerSettings.GetLanguage();
                ILocalizeReceipts localizeReceipts = ILocalizeReceipts.createLocalizeReceipts(languageIndex, (int)paperSize, printerSettings);

                IBuffer commands = await this.CreateCommands(functionType, printerSettings.GetEmulation(true), localizeReceipts, paperSize);


                PrinterSettingsInfo[] printerSettingInfo = await printerSettings.LoadSettings();

                result = await Communication.SendCommandsForRedirection(commands, printerSettingInfo, 10000);

                message = Communication.GetCommunicationResultMessage(result);

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

        private async Task<IBuffer> CreateCommands(PrintRedirectionSampleFunctionFunctionType functionType, Emulation emulation, ILocalizeReceipts localizeReceipts, uint paperSize)
        {
            IBuffer commands = null;

            switch (functionType)
            {
                default:
                case PrintRedirectionSampleFunctionFunctionType.TextReceipt:
                    commands = PrinterFunctions.CreateTextReceiptData(emulation, localizeReceipts, false); break;

                case PrintRedirectionSampleFunctionFunctionType.TextReceiptUTF8:
                    commands = PrinterFunctions.CreateTextReceiptData(emulation, localizeReceipts, true); break;

                case PrintRedirectionSampleFunctionFunctionType.RasterReceipt:
                    commands = await PrinterFunctions.CreateRasterReceiptData(emulation, localizeReceipts);
                    break;

                case PrintRedirectionSampleFunctionFunctionType.RasterReceiptBothScale:
                    commands = await PrinterFunctions.CreateScaleRasterReceiptData(emulation, localizeReceipts, (uint)paperSize, true);
                    break;

                case PrintRedirectionSampleFunctionFunctionType.RasterReceiptScale:
                    commands = await PrinterFunctions.CreateScaleRasterReceiptData(emulation, localizeReceipts, (uint)paperSize, false);
                    break;

                case PrintRedirectionSampleFunctionFunctionType.RasterCoupon:
                    commands = await PrinterFunctions.CreateCouponData(emulation, localizeReceipts, (uint)paperSize, BitmapConverterRotation.Normal);
                    break;

                case PrintRedirectionSampleFunctionFunctionType.RasterCoupon90:
                    commands = await PrinterFunctions.CreateCouponData(emulation, localizeReceipts, (uint)paperSize, BitmapConverterRotation.Right90);
                    break;

            }

            return commands;
        }

        private void SearchPrinter(object sender, TappedRoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(SearchPortSamplePage), "BackupPrinterNamePanel");
        }
    }
}
