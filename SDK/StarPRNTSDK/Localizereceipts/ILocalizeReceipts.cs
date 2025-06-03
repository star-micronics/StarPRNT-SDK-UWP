using StarIO_Extension;
using System;
using System.Text;
using System.Threading.Tasks;
using Windows.Graphics.Imaging;
using Windows.Storage;
using Windows.Storage.Streams;
using Windows.UI;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using System.Runtime.InteropServices.WindowsRuntime;

namespace StarPRNTSDK.Localizereceipts
{
    abstract class ILocalizeReceipts
    {
        public string LanguageCode { get; protected set; }
        public string PaperSizeStr { get; private set; }
        public string ScalePaperSizeStr { get; private set; }
        public CharacterCode CharacterCode { get; protected set; }
        public int PaperSize { get; set; }

        public const string RASTER_IMAGE_FILE_NAME = "RasterImage.png";

        public static ILocalizeReceipts createLocalizeReceipts(int language, int paperSize, PrinterSettings printerSettings)
        {
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);

            ILocalizeReceipts localizeReceipts = null;


            ModelDictionary modelDic = new ModelDictionary();
            PrinterInfo printerInfo = modelDic.GetPrinterInfo(printerSettings.GetModelTitle(true));

            switch (language)
            {
                case (int)PrinterSettings.LANGUAGE.ENGLISH:
                    localizeReceipts = new EnglishReceiptsImpl(); break;
                case (int)PrinterSettings.LANGUAGE.JAPANESE:
                    localizeReceipts = new JapaneseReceiptsImpl(); break;
                case (int)PrinterSettings.LANGUAGE.FRENCH:
                    localizeReceipts = new FrenchReceiptsImpl(); break;
                case (int)PrinterSettings.LANGUAGE.PORTUGUESE:
                    localizeReceipts = new PortugueseReceiptsImpl(); break;
                case (int)PrinterSettings.LANGUAGE.SPANISH:
                    localizeReceipts = new SpanishReceiptsImpl(); break;
                case (int)PrinterSettings.LANGUAGE.GERMAN:
                    localizeReceipts = new GermanReceiptsImpl(); break;
                case (int)PrinterSettings.LANGUAGE.RUSSIAN:
                    localizeReceipts = new RussianReceiptsImpl(); break;
                case (int)PrinterSettings.LANGUAGE.SIMPLIFIED_CHINESE:
                    localizeReceipts = new SimplifiedChineseReceiptsImpl(); break;
  //            case (int)PrinterSettings.LANGUAGE.TRADITIONAL_CHINESE:
                default:
                    localizeReceipts = new TraditionalChineseReceiptsImpl(); break;
                case (int)PrinterSettings.LANGUAGE.CJK_UNIFIED_IDEOGRAPH:
                    localizeReceipts = new UTF8MultiLanguageReceiptsImple(); break;
            }

            if (paperSize == 1)
            {
                paperSize = Convert.ToInt32(printerInfo.PaperSize);
            }

            switch (paperSize)
            {
                case (int)PrinterSettings.PAPERSIZE.TWO_INCH:

                    localizeReceipts.PaperSizeStr = "2\"";
                    localizeReceipts.ScalePaperSizeStr = "3\""; // 3inch -> 2inch
                    
                    break;
                case (int)PrinterSettings.PAPERSIZE.THREE_INCH:
                case (int)PrinterSettings.PAPERSIZE.ESCPOS_THREE_INCH:
                case (int)PrinterSettings.PAPERSIZE.DOT_THREE_INCH:

                    localizeReceipts.PaperSizeStr = "3\"";
                    localizeReceipts.ScalePaperSizeStr = "4\"";// 4inch -> 3inch

                    break;
//            case (int)PrinterSettings.PAPERSIZE._FOUR_INCH :
                default:
                    localizeReceipts.PaperSizeStr = "4\"";
                    localizeReceipts.ScalePaperSizeStr = "3\"";// 3inch -> 4inch

                    break;
            }

            localizeReceipts.PaperSize = paperSize;
            

            return localizeReceipts;
        }

        public void AppendTextReceiptData(ICommandBuilder builder, bool utf8)
        {
            switch (PaperSize)
            {
                case (int)PrinterSettings.PAPERSIZE.TWO_INCH:
                    Append2inchTextReceiptData(builder, utf8);
                    break;
                case (int)PrinterSettings.PAPERSIZE.THREE_INCH:
                    Append3inchTextReceiptData(builder, utf8);
                    break;
                case (int)PrinterSettings.PAPERSIZE.FOUR_INCH:
                    Append4inchTextReceiptData(builder, utf8);
                    break;
                case (int)PrinterSettings.PAPERSIZE.ESCPOS_THREE_INCH:
                    AppendEscPos3inchTextReceiptData(builder, utf8);
                    break;
  //            case (int)PrinterSettings.PAPERSIZE.DOT_THREE_INCH:
                default:
                    AppendDotImpact3inchTextReceiptData(builder, utf8);
                    break;
            }
        }

        public async Task<BitmapDecoder> CreateRasterReceiptImage()
        {
            BitmapDecoder decoder;

            switch (PaperSize)
            {
                case (int)PrinterSettings.PAPERSIZE.TWO_INCH:
                    decoder = await Create2inchRasterImageAsync();
                    break;
                case (int)PrinterSettings.PAPERSIZE.THREE_INCH:
                    decoder = await Create3inchRasterImageAsync();
                    break;
                case (int)PrinterSettings.PAPERSIZE.FOUR_INCH:
                    decoder = await Create4inchRasterImageAsync();
                    break;
                case (int)PrinterSettings.PAPERSIZE.ESCPOS_THREE_INCH:
                    decoder = await CreateEscPos3inchRasterImageAsync();
                    break;
  //            case (int)PrinterSettings.PAPERSIZE.DOT_THREE_INCH:
                default:
                    decoder = await CreateCouponImageAsync();
                    break;
            }

            return decoder;
        }

        public async Task<BitmapDecoder> CreateScaleRasterReceiptImage()
        {
            BitmapDecoder decoder;

            switch (PaperSize)
            {
                case (int)PrinterSettings.PAPERSIZE.TWO_INCH:
                    decoder = await Create3inchRasterImageAsync();
                    break;
                case (int)PrinterSettings.PAPERSIZE.THREE_INCH:
                case (int)PrinterSettings.PAPERSIZE.ESCPOS_THREE_INCH:
                    decoder = await Create4inchRasterImageAsync();
                    break;
                case (int)PrinterSettings.PAPERSIZE.FOUR_INCH:
                    decoder = await Create3inchRasterImageAsync();
                    break;
  //            case (int)PrinterSettings.PAPERSIZE.DOT_THREE_INCH:
                default:
                    decoder = await CreateCouponImageAsync();
                    break;
            }

            return decoder;
        }

        public abstract void Append2inchTextReceiptData(ICommandBuilder builder, bool utf8);

        public abstract void Append3inchTextReceiptData(ICommandBuilder builder, bool utf8);

        public abstract void Append4inchTextReceiptData(ICommandBuilder builder, bool utf8);

        public abstract string Create2inchRasterReceiptText();

        public abstract string Create3inchRasterReceiptText();

        public abstract string Create4inchRasterReceiptText();

        public abstract Task<BitmapDecoder> CreateCouponImageAsync();

        public abstract Task<BitmapDecoder> Create2inchRasterImageAsync();

        public abstract Task<BitmapDecoder> Create3inchRasterImageAsync();

        public abstract Task<BitmapDecoder> Create4inchRasterImageAsync();

        public abstract Task<BitmapDecoder> CreateEscPos3inchRasterImageAsync();

        public abstract void AppendEscPos3inchTextReceiptData(ICommandBuilder builder, bool utf8);

        public abstract void AppendDotImpact3inchTextReceiptData(ICommandBuilder builder, bool utf8);

        public abstract void AppendTextLabelData(ICommandBuilder builder, bool utf8);

        public abstract string CreatePasteTextLabelString();

        public abstract void AppendPasteTextLabelData(ICommandBuilder builder, string pasteText, bool utf8);

        public async Task<bool> CreateImageAsync(string text, int fontSize, uint width, string fileName)
        {
            Grid grid = new Grid();

            RowDefinition LoginGridRow1 = new RowDefinition();
            grid.RowDefinitions.Add(LoginGridRow1);

            TextBlock textdata = new TextBlock();
            textdata.Text = text;
            textdata.FontSize = fontSize;

            Grid.SetRow(textdata, 0);

            grid.Children.Add(textdata);

            StackPanel panel = new StackPanel();
            panel.Background = new SolidColorBrush(Colors.White);
            panel.VerticalAlignment = Windows.UI.Xaml.VerticalAlignment.Top;
            panel.Orientation = Orientation.Horizontal;
            panel.Children.Add(grid);

            Popup popup1 = new Popup();

            //To avoid popup being displayed on the screen, display it outside the screen.
            var h = Windows.UI.Xaml.Window.Current.Bounds.Height;
            var w = Windows.UI.Xaml.Window.Current.Bounds.Width;
            popup1.HorizontalOffset = -h * 2;
            popup1.VerticalOffset = -w * 2;
            popup1.Child = panel;
            popup1.IsOpen = true;

            try
            {
                var renderTargetBitmap = new RenderTargetBitmap();
                await renderTargetBitmap.RenderAsync(panel);
                popup1.IsOpen = false;
                var pixelBuffer = await renderTargetBitmap.GetPixelsAsync();

                var file = await ApplicationData.Current.LocalFolder.CreateFileAsync(fileName, CreationCollisionOption.ReplaceExisting);

                byte[] pixels = pixelBuffer.ToArray();

                using (var stream = await file.OpenAsync(FileAccessMode.ReadWrite))
                {
                    var encoder = await BitmapEncoder.CreateAsync(BitmapEncoder.PngEncoderId, stream);
                    encoder.SetPixelData(
                        BitmapPixelFormat.Bgra8,
                        BitmapAlphaMode.Ignore,
                        (uint)renderTargetBitmap.PixelWidth,
                        (uint)renderTargetBitmap.PixelHeight,
                        72,
                        72,
                        pixels);

                    encoder.BitmapTransform.ScaledWidth = width;
                    encoder.BitmapTransform.ScaledHeight = (uint)((encoder.BitmapTransform.ScaledWidth * renderTargetBitmap.PixelHeight) / renderTargetBitmap.PixelWidth);
                    encoder.BitmapTransform.Rotation = BitmapRotation.None;
                    encoder.BitmapTransform.InterpolationMode = BitmapInterpolationMode.Cubic;
                    await encoder.FlushAsync();
                }

                return true;
            }
            catch (Exception)
            {
                popup1.IsOpen = false;
                return false;
            }
            finally
            {
                popup1.IsOpen = false;
            }
        }

        public async Task<BitmapDecoder> GetRasterImage(string fileName)
        {
            BitmapDecoder logoData = null;
            StorageFile logoFile = null;
            try
            {
                logoFile = await ApplicationData.Current.LocalFolder.GetFileAsync(fileName);

                using (IRandomAccessStreamWithContentType logoStream = await logoFile.OpenReadAsync())
                {
                    logoData = await BitmapDecoder.CreateAsync(logoStream);
                }
                logoFile = null;
            }
            catch
            {

            }

            return logoData;
        }

    }
}
