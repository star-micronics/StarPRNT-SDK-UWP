using StarPRNTSDK.Localizereceipts;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Navigation;

namespace StarPRNTSDK
{

    public sealed partial class CombinationSamplePage : Page
    {
        private ILocalizeReceipts localizeReceipts;

        public CombinationSamplePage()
        {
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            PrinterSettings printerSettings = new PrinterSettings();
            int paperSize                   = printerSettings.GetPaperSize(true);
            int languageIndex               = printerSettings.GetLanguage();

            localizeReceipts = ILocalizeReceipts.createLocalizeReceipts(languageIndex, paperSize, printerSettings);

            TextReceitExt.Text             = localizeReceipts.LanguageCode + " " + localizeReceipts.PaperSizeStr      + " " + "Text Receipt";
            TextReceitUTF8Ext.Text         = localizeReceipts.LanguageCode + " " + localizeReceipts.PaperSizeStr      + " " + "Text Receipt(UTF8)";

            RasterReceiptExt.Text          = localizeReceipts.LanguageCode + " " + localizeReceipts.PaperSizeStr      + " " + "Raster Receipt";
            RasterReceiptBothScaleExt.Text = localizeReceipts.LanguageCode + " " + localizeReceipts.ScalePaperSizeStr + " " + "Raster Receipt(Both Scale)";
            RasterReceiptScaleExt.Text     = localizeReceipts.LanguageCode + " " + localizeReceipts.ScalePaperSizeStr + " " + "Raster Receipt(Scale)";

            RasterCouponExt.Text           = localizeReceipts.LanguageCode + " " + "Raster Coupon";
            RasterCouponRotation90Ext.Text = localizeReceipts.LanguageCode + " " + "Raster Coupon(Rotation90)";

       }

        private void Receit_Tapped(object sender, TappedRoutedEventArgs e)
        {
            var selectedName = (sender as TextBlock).Name;

            this.Frame.Navigate(typeof(CombinationExtSamplePage), selectedName);
        }
    }
}
