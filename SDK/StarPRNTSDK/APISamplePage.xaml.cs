using StarIO_Extension;
using StarPRNTSDK.Functions;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Windows.Storage.Streams;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Navigation;


namespace StarPRNTSDK
{

    public sealed partial class APISamplePage : Page
    {
        private enum APIType
        {
            Generic,
            FontStyle,
            Initialization,
            CodePage,
            International,
            Feed,
            CharacterSpace,
            LineSpace,
            TopMargin,
            Emphasis,
            Invert,
            UnderLine,
            Multiple,
            AbsolutePosition,
            Alignment,
            HorizontalTabPosition,
            Logo,
            CutPaper,
            Peripheral,
            Sound,
            Bitmap,
            Barcode,
            PDF417,
            QRCode,
            BlackMark,
            PageMode,
            PrintableArea
        }

        private struct SelectDialogResult
        {
            public int selectedIndex { get; }
            public bool isCanceled { get; }

            public SelectDialogResult(int selectedIndex, bool isCanceled)
            {
                this.selectedIndex = selectedIndex;
                this.isCanceled = isCanceled;
            }
        }

        public APISamplePage()
        {
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            PrinterSettings printerSettings = new PrinterSettings();

            this.SetAllAPIList();
        }

        private void SetAllAPIList()
        {
            this.SetAPI(this.GenericTextBlock, APIType.Generic);

            this.SetAPI(this.FontStyleTextBlock, APIType.FontStyle);

            this.SetAPI(this.InitializationTextBlock, APIType.Initialization);

            this.SetAPI(this.CodePageTextBlock, APIType.CodePage);

            this.SetAPI(this.InternationalTextBlock, APIType.International);

            this.SetAPI(this.FeedTextBlock, APIType.Feed);

            this.SetAPI(this.CharacterSpaceTextBlock, APIType.CharacterSpace);

            this.SetAPI(this.LineSpaceTextBlock, APIType.LineSpace);

            this.SetAPI(this.TopMarginTextBlock, APIType.TopMargin);

            this.SetAPI(this.EmphasisTextBlock, APIType.Emphasis);

            this.SetAPI(this.InvertTextBlock, APIType.Invert);

            this.SetAPI(this.UnderLineTextBlock, APIType.UnderLine);

            this.SetAPI(this.MultipleTextBlock, APIType.Multiple);

            this.SetAPI(this.AbsolutePositionTextBlock, APIType.AbsolutePosition);

            this.SetAPI(this.AlignmentTextBlock, APIType.Alignment);

            this.SetAPI(this.HorizontalTabPositionTextBlock, APIType.HorizontalTabPosition);

            this.SetAPI(this.LogoTextBlock, APIType.Logo);

            this.SetAPI(this.CutPaperTextBlock, APIType.CutPaper);

            this.SetAPI(this.PeripheralTextBlock, APIType.Peripheral);

            this.SetAPI(this.SoundTextBlock, APIType.Sound);

            this.SetAPI(this.BitmapTextBlock, APIType.Bitmap);

            this.SetAPI(this.BarcodeTextBlock, APIType.Barcode);

            this.SetAPI(this.PDF417TextBlock, APIType.PDF417);

            this.SetAPI(this.QRCodeTextBlock, APIType.QRCode);

            this.SetAPI(this.BlackMarkTextBlock, APIType.BlackMark);

            this.SetAPI(this.PageModeTextBlock, APIType.PageMode);

            this.SetAPI(this.PrintableAreaTextBlock, APIType.PrintableArea);
        }

        private void SetAPI(TextBlock apiTextBlock, APIType apiType)
        {
            apiTextBlock.Tapped += (sender, e) => this.CallAPI(sender, e, apiType);
        }

        private async void CallAPI(object sender, TappedRoutedEventArgs e, APIType apiType)
        {
            if(apiType == APIType.BlackMark)
            {
                List<string> blackMarkTypeList = new List<string>();

                blackMarkTypeList.Add("Invalid");
                blackMarkTypeList.Add("Valid");
                blackMarkTypeList.Add("Valid with Detection");

                SelectDialogResult result = await this.ShowSelectDialog("Select black mark type.", blackMarkTypeList);

                if (result.isCanceled == true)
                {
                    return;
                }

                BlackMarkType blackMarkType = BlackMarkType.Invalid;

                switch (result.selectedIndex)
                {
                    default:
                    case 0: blackMarkType = BlackMarkType.Invalid;            break;
                    case 1: blackMarkType = BlackMarkType.Valid;              break;
                    case 2: blackMarkType = BlackMarkType.ValidWithDetection; break;
                }

                this.SendAPICommands(apiType, blackMarkType, PrintableAreaType.Standard);
            }
            else if (apiType == APIType.PrintableArea)
            {
                List<string> printableAreaTypeList = new List<string>();

                printableAreaTypeList.Add("Standard");
                printableAreaTypeList.Add("Type1");
                printableAreaTypeList.Add("Type2");
                printableAreaTypeList.Add("Type3");
                printableAreaTypeList.Add("Type4");

                SelectDialogResult result = await this.ShowSelectDialog("Select printable type.", printableAreaTypeList);

                if (result.isCanceled == true)
                {
                    return;
                }

                PrintableAreaType printableAreaType = PrintableAreaType.Standard;

                switch (result.selectedIndex)
                {
                    default:
                    case 0: printableAreaType = PrintableAreaType.Standard; break;
                    case 1: printableAreaType = PrintableAreaType.Type1;    break;
                    case 2: printableAreaType = PrintableAreaType.Type2;    break;
                    case 3: printableAreaType = PrintableAreaType.Type3;    break;
                    case 4: printableAreaType = PrintableAreaType.Type4;    break;
                }

                this.SendAPICommands(apiType, BlackMarkType.Invalid, printableAreaType);
            }
            else
            {
                this.SendAPICommands(apiType);
            }
        }

        private async Task<SelectDialogResult> ShowSelectDialog(string message , List<string> list)
        {
            SelectSettingContentDialog dialog = new SelectSettingContentDialog(message, list);

            ContentDialogResult result = await dialog.ShowAsync();

            bool isCanceled = true;
            int selectedIndex = 0;
            if (result == ContentDialogResult.Primary)
            {
                isCanceled = false;

                selectedIndex = dialog.GetSelectedIndex();
            }

            return new SelectDialogResult(selectedIndex, isCanceled);
        }

        private async void SendAPICommands(APIType apiType, BlackMarkType blackMarkType, PrintableAreaType printableAreaType)
        {
            var progressRing = new ProgressRing();
            CommunicationResult result;
            string message = null;

            try
            {
                PrinterSettings printerSettings = new PrinterSettings();

                IBuffer commands = await this.CreateCommands(apiType, printerSettings.GetEmulation(true), printerSettings.GetPaperSize(true), blackMarkType, printableAreaType);

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

        private void SendAPICommands(APIType apiType)
        {
            this.SendAPICommands(apiType, BlackMarkType.Invalid, PrintableAreaType.Standard);
        }

        private async Task<IBuffer> CreateCommands(APIType apiType, Emulation emulation, int paperSize, BlackMarkType blackMarkType, PrintableAreaType printableAreaType)
        {
            IBuffer commands = null;

            switch (apiType)
            {
                default:
                case APIType.Generic:
                    commands = APIFunctions.CreateGenericData(emulation);
                    break;

                case APIType.FontStyle:
                    commands = APIFunctions.CreateFontStyleData(emulation);
                    break;

                case APIType.Initialization:
                    commands = APIFunctions.CreateInitializationData(emulation);
                    break;

                case APIType.CodePage:
                    commands = APIFunctions.CreateCodePageData(emulation);
                    break;

                case APIType.International:
                    commands = APIFunctions.CreateInternationalData(emulation);
                    break;

                case APIType.Feed:
                    commands = APIFunctions.CreateFeedData(emulation);
                    break;

                case APIType.CharacterSpace:
                    commands = APIFunctions.CreateCharacterSpaceData(emulation);
                    break;
                case APIType.LineSpace:
                    commands = APIFunctions.CreateLineSpaceData(emulation);
                    break;

                case APIType.TopMargin:
                    commands = APIFunctions.CreateTopMarginData(emulation);
                    break;

                case APIType.Emphasis:
                    commands = APIFunctions.CreateEmphasisData(emulation);
                    break;
                case APIType.Invert:
                    commands = APIFunctions.CreateInvertData(emulation);
                    break;

                case APIType.UnderLine:
                    commands = APIFunctions.CreateUnderLineData(emulation);
                    break;

                case APIType.Multiple:
                    commands = APIFunctions.CreateMultipleData(emulation);
                    break;

                case APIType.AbsolutePosition:
                    commands = APIFunctions.CreateAbsolutePositionData(emulation);
                    break;

                case APIType.HorizontalTabPosition:
                    commands = APIFunctions.CreateHorizontalTabData(emulation);
                    break;

                case APIType.Alignment:
                    commands = APIFunctions.CreateAlignmentData(emulation);
                    break;

                case APIType.Logo:
                    commands = APIFunctions.CreateLogoData(emulation);
                    break;

                case APIType.CutPaper:
                    commands = APIFunctions.CreateCutPaperData(emulation);
                    break;

                case APIType.Peripheral:
                    commands = APIFunctions.CreatePeripheralData(emulation);
                    break;

                case APIType.Sound:
                    commands = APIFunctions.CreateSoundData(emulation);
                    break;

                case APIType.Bitmap:
                    commands = await APIFunctions.CreateBitmapDataAsync(emulation, (uint)paperSize);
                    break;

                case APIType.Barcode:
                    commands = APIFunctions.CreateBarcodeData(emulation);
                    break;

                case APIType.PDF417:
                    commands = APIFunctions.CreatePdf417Data(emulation);
                    break;

                case APIType.QRCode:
                    commands = APIFunctions.CreateQrCodeData(emulation);
                    break;

                case APIType.BlackMark:
                    commands = APIFunctions.CreateBlackMarkData(emulation, blackMarkType);
                    break;

                case APIType.PageMode:
                    commands = await APIFunctions.CreatePageModeData(emulation, paperSize);
                    break;

                case APIType.PrintableArea:
                    commands = await APIFunctions.CreatePrintableAreaData(emulation, printableAreaType);
                    break;

            }

            return commands;
        }      
    }
}
