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
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Navigation;


namespace StarPRNTSDK
{

    public sealed partial class PrinterSamplePage : Page
    {

        private struct PrintSampleFunction
        {
            public string title { get; }
            public PrintSampleFunctionType functionType { get; }

            public PrintSampleFunction(string title, PrintSampleFunctionType functionType)
            {
                this.title = title;
                this.functionType = functionType;
            }
        }
        public enum PrintSampleFunctionType
        {
            TextReceipt,
            TextReceiptUTF8,
            RasterReceipt,
            RasterReceiptBothScale,
            RasterReceiptScale,
            RasterCoupon,
            RasterCoupon90,
            TextReceiptExt,
            TextReceiptUTF8Ext,
            RasterReceiptExt,
            RasterReceiptBothScaleExt,
            RasterReceiptScaleExt,
            RasterCouponExt,
            RasterCoupon90Ext,
            PrintPhotoLibrary,
            PrintPhotoCamera
        }

        public PrinterSamplePage()
        {
            this.InitializeComponent();

            this.SetComponents();

            this.SetDisableFunctions();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
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

            this.SetFunctionIsEnabled(this.TextReceitExt,             true == CJKIsEnabled ? printerInfo.CJKIsEnabled : printerInfo.TextReceiptIsEnabled);
            this.SetFunctionIsEnabled(this.TextReceitUTF8Ext,         true == CJKIsEnabled ? printerInfo.CJKIsEnabled : printerInfo.UTF8IsEnabled);
            this.SetFunctionIsEnabled(this.RasterReceiptExt,          true == CJKIsEnabled ? printerInfo.CJKIsEnabled : printerInfo.RasterReceiptIsEnabled);
            this.SetFunctionIsEnabled(this.RasterReceiptBothScaleExt, true == CJKIsEnabled ? printerInfo.CJKIsEnabled : printerInfo.RasterReceiptIsEnabled);
            this.SetFunctionIsEnabled(this.RasterReceiptScaleExt,     true == CJKIsEnabled ? printerInfo.CJKIsEnabled : printerInfo.RasterReceiptIsEnabled);
            this.SetFunctionIsEnabled(this.RasterCouponExt,           true == CJKIsEnabled ? printerInfo.CJKIsEnabled : true);
            this.SetFunctionIsEnabled(this.RasterCouponRotation90Ext, true == CJKIsEnabled ? printerInfo.CJKIsEnabled : true);

            this.SetFunctionIsEnabled(this.PrintPhotofromLibrary,     true == CJKIsEnabled ? false : true);
            this.SetFunctionIsEnabled(this.PrintPhotofromCamera,      true == CJKIsEnabled ? false : true);


            this.DisableFunction(TextReceitStackPanel, CJKIsEnabled);
            this.DisableFunction(RasterReceiptStackPanel, CJKIsEnabled);
            this.DisableFunction(RasterReceiptBothScaleStackPanel, CJKIsEnabled);
            this.DisableFunction(RasterReceiptScaleStackPanel, CJKIsEnabled);
            this.DisableFunction(RasterCouponStackPanel, CJKIsEnabled);
            this.DisableFunction(RasterCouponRotation90StackPanel, CJKIsEnabled);

            this.DisableFunction(TextReceitExtStackPanel, CJKIsEnabled);
            this.DisableFunction(RasterReceiptExtStackPanel, CJKIsEnabled);
            this.DisableFunction(RasterReceiptBothScaleExtStackPanel, CJKIsEnabled);
            this.DisableFunction(RasterReceiptScaleExtStackPanel, CJKIsEnabled);
            this.DisableFunction(RasterCouponExtStackPanel, CJKIsEnabled);
            this.DisableFunction(RasterCouponRotation90ExtStackPanel, CJKIsEnabled);

            
            this.DisableFunction(PrintPhotofromLibraryStackPanel, CJKIsEnabled);
            this.DisableFunction(PrintPhotofromCameraStackPanel, CJKIsEnabled);
        }

        private void DisableFunction(StackPanel stackPanel, bool isEnabled)
        {
            if (isEnabled)
            {
                stackPanel.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
                AppendixBorder.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
            }
            else
            {
                stackPanel.Visibility = Windows.UI.Xaml.Visibility.Visible;
                AppendixBorder.Visibility = Windows.UI.Xaml.Visibility.Visible;
            }
        }

        private void SetAllFunctions()
        {
            foreach (KeyValuePair<TextBlock, PrintSampleFunction> pair in this.GetAllFunctions())
            {
                TextBlock textBlock = pair.Key;
                PrintSampleFunction function = pair.Value;

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
        private void SetAllFunctionsIsEnabled()
        {

            foreach (KeyValuePair<TextBlock, PrintSampleFunction> pair in this.GetAllFunctions())
            {
                TextBlock textBlock = pair.Key;

                this.SetFunctionIsEnabled(textBlock, true);
            }

        }

        private void CallFunction(object sender, TappedRoutedEventArgs e, PrintSampleFunction function)
        {
            if (this.IsExtFunction(function.functionType))
            {
                this.Frame.Navigate(typeof(PrinterExtSamplePage), function.functionType);
            }
            else
            {
                this.PrintReceipt(function.functionType);
            }
        }

        private bool IsExtFunction(PrintSampleFunctionType functionType)
        {
            return (functionType == PrintSampleFunctionType.TextReceiptExt) ||
                   (functionType == PrintSampleFunctionType.TextReceiptUTF8Ext) ||
                   (functionType == PrintSampleFunctionType.RasterReceiptExt) ||
                   (functionType == PrintSampleFunctionType.RasterReceiptBothScaleExt) ||
                   (functionType == PrintSampleFunctionType.RasterReceiptScaleExt) ||
                   (functionType == PrintSampleFunctionType.RasterCouponExt) ||
                   (functionType == PrintSampleFunctionType.RasterCoupon90Ext);
        }

        private Dictionary<TextBlock, PrintSampleFunction> GetAllFunctions()
        {
            PrinterSettings printerSettings = new PrinterSettings();

            ILocalizeReceipts localizeReceipts = ILocalizeReceipts.createLocalizeReceipts(printerSettings.GetLanguage(), printerSettings.GetPaperSize(true), printerSettings);

            Dictionary<TextBlock, PrintSampleFunction> allFunctionsList = new Dictionary<TextBlock, PrintSampleFunction>();

            allFunctionsList.Add(this.TextReceit,
                                 new PrintSampleFunction(localizeReceipts.LanguageCode + " " + localizeReceipts.PaperSizeStr + " " + "Text Receipt",
                                 PrintSampleFunctionType.TextReceipt));

            allFunctionsList.Add(this.TextReceitUTF8,
                                 new PrintSampleFunction(localizeReceipts.LanguageCode + " " + localizeReceipts.PaperSizeStr + " " + "Text Receipt(UTF8)",
                                 PrintSampleFunctionType.TextReceiptUTF8));

            allFunctionsList.Add(this.RasterReceipt,
                                 new PrintSampleFunction(localizeReceipts.LanguageCode + " " + localizeReceipts.PaperSizeStr + " " + "Raster Receipt",
                                 PrintSampleFunctionType.RasterReceipt));

            allFunctionsList.Add(this.RasterReceiptBothScale,
                                 new PrintSampleFunction(localizeReceipts.LanguageCode + " " + localizeReceipts.ScalePaperSizeStr + " " + "Raster Receipt(Both Scale)",
                                 PrintSampleFunctionType.RasterReceiptBothScale));

            allFunctionsList.Add(this.RasterReceiptScale,
                                 new PrintSampleFunction(localizeReceipts.LanguageCode + " " + localizeReceipts.ScalePaperSizeStr + " " + "Raster Receipt(Scale)",
                                 PrintSampleFunctionType.RasterReceiptScale));

            allFunctionsList.Add(this.RasterCoupon,
                                 new PrintSampleFunction(localizeReceipts.LanguageCode + " " + "Raster Coupon",
                                 PrintSampleFunctionType.RasterCoupon));

            allFunctionsList.Add(this.RasterCouponRotation90,
                                 new PrintSampleFunction(localizeReceipts.LanguageCode + " " + "Raster Coupon(Rotation90)",
                                 PrintSampleFunctionType.RasterCoupon90));

            allFunctionsList.Add(this.TextReceitExt,
                                 new PrintSampleFunction(localizeReceipts.LanguageCode + " " + localizeReceipts.PaperSizeStr + " " + "Text Receipt",
                                 PrintSampleFunctionType.TextReceiptExt));

            allFunctionsList.Add(this.TextReceitUTF8Ext,
                                 new PrintSampleFunction(localizeReceipts.LanguageCode + " " + localizeReceipts.PaperSizeStr + " " + "Text Receipt(UTF8)",
                                 PrintSampleFunctionType.TextReceiptUTF8Ext));

            allFunctionsList.Add(this.RasterReceiptExt,
                                 new PrintSampleFunction(localizeReceipts.LanguageCode + " " + localizeReceipts.PaperSizeStr + " " + "Raster Receipt",
                                 PrintSampleFunctionType.RasterReceiptExt));

            allFunctionsList.Add(this.RasterReceiptBothScaleExt,
                                 new PrintSampleFunction(localizeReceipts.LanguageCode + " " + localizeReceipts.ScalePaperSizeStr + " " + "Raster Receipt(Both Scale)",
                                 PrintSampleFunctionType.RasterReceiptBothScaleExt));

            allFunctionsList.Add(this.RasterReceiptScaleExt,
                                 new PrintSampleFunction(localizeReceipts.LanguageCode + " " + localizeReceipts.ScalePaperSizeStr + " " + "Raster Receipt(Scale)",
                                 PrintSampleFunctionType.RasterReceiptScaleExt));

            allFunctionsList.Add(this.RasterCouponExt,
                                 new PrintSampleFunction(localizeReceipts.LanguageCode + " " + "Raster Coupon",
                                 PrintSampleFunctionType.RasterCouponExt));

            allFunctionsList.Add(this.RasterCouponRotation90Ext,
                                 new PrintSampleFunction(localizeReceipts.LanguageCode + " " + "Raster Coupon(Rotation90)",
                                 PrintSampleFunctionType.RasterCoupon90Ext));

            allFunctionsList.Add(this.PrintPhotofromLibrary,
                                 new PrintSampleFunction("Print Photo from Library",
                                 PrintSampleFunctionType.PrintPhotoLibrary));

            allFunctionsList.Add(this.PrintPhotofromCamera,
                                 new PrintSampleFunction("Print Photo by Camera",
                                 PrintSampleFunctionType.PrintPhotoCamera));

            return allFunctionsList;
        }


        private async void PrintReceipt(PrintSampleFunctionType functionType)
        {
            var progressRing = new ProgressRing();
            CommunicationResult result;
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

                result = await Communication.SendCommands(commands, printerSettings.GetPortName(true), printerSettings.GetPortSettings(true), 30000);

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

        private async Task<IBuffer> CreateCommands(PrintSampleFunctionType functionType, Emulation emulation, ILocalizeReceipts localizeReceipts, uint paperSize)
        {
            IBuffer commands = null;

            switch (functionType)
            {
                default:
                case PrintSampleFunctionType.TextReceipt:
                    commands = PrinterFunctions.CreateTextReceiptData(emulation, localizeReceipts, false); break;

                case PrintSampleFunctionType.TextReceiptUTF8:
                    commands = PrinterFunctions.CreateTextReceiptData(emulation, localizeReceipts, true); break;

                case PrintSampleFunctionType.RasterReceipt:
                    commands = await PrinterFunctions.CreateRasterReceiptData(emulation, localizeReceipts);
                    break;

                case PrintSampleFunctionType.RasterReceiptBothScale:
                    commands = await PrinterFunctions.CreateScaleRasterReceiptData(emulation, localizeReceipts, (uint)paperSize, true);
                    break;

                case PrintSampleFunctionType.RasterReceiptScale:
                    commands = await PrinterFunctions.CreateScaleRasterReceiptData(emulation, localizeReceipts, (uint)paperSize, false);
                    break;

                case PrintSampleFunctionType.RasterCoupon:
                    commands = await PrinterFunctions.CreateCouponData(emulation, localizeReceipts, (uint)paperSize, BitmapConverterRotation.Normal);
                    break;

                case PrintSampleFunctionType.RasterCoupon90:
                    commands = await PrinterFunctions.CreateCouponData(emulation, localizeReceipts, (uint)paperSize, BitmapConverterRotation.Right90);
                    break;

                case PrintSampleFunctionType.PrintPhotoLibrary:
                    commands = await this.CreateCommandsFromLibrary(emulation, (uint)paperSize);
                    break;

                case PrintSampleFunctionType.PrintPhotoCamera:
                    commands = await this.CreateCommandsFromCamera(emulation, (uint)paperSize);
                    break;
            }

            return commands;
        }

        private async Task<IBuffer> CreateCommandsFromCamera(Emulation emulation, uint paperSize)
        {
            IBuffer commands = null;

            var dialog = new CameraCaptureUI();

            dialog.PhotoSettings.Format = CameraCaptureUIPhotoFormat.Png;
            var file = await dialog.CaptureFileAsync(CameraCaptureUIMode.Photo);

            if (file != null)
            {
                commands = await PrinterFunctions.CreateFileOpenData(emulation, file, paperSize);
            }

            return commands;
        }

        private async Task<IBuffer> CreateCommandsFromLibrary(Emulation emulation, uint paperSize)
        {
            IBuffer commands = null;

            var picker = new FileOpenPicker();
            picker.ViewMode = PickerViewMode.Thumbnail;
            picker.SuggestedStartLocation = PickerLocationId.PicturesLibrary;
            picker.FileTypeFilter.Add(".jpg");
            picker.FileTypeFilter.Add(".jpeg");
            picker.FileTypeFilter.Add(".png");
            picker.FileTypeFilter.Add(".bmp");
            picker.FileTypeFilter.Add(".tiff");
            picker.FileTypeFilter.Add(".gif");


            StorageFile storageFile = await picker.PickSingleFileAsync();

            if (storageFile != null)
            {
                commands = await PrinterFunctions.CreateFileOpenData(emulation, storageFile, paperSize);
            }

            return commands;
        }
    }
}
