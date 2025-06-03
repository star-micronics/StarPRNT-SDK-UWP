using StarIO_Extension;
using System;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading.Tasks;
using Windows.Graphics.Imaging;
using Windows.Storage;
using Windows.Storage.Streams;

namespace StarPRNTSDK.Localizereceipts
{
    class EnglishReceiptsImpl : ILocalizeReceipts
    {

        public EnglishReceiptsImpl()
        {
            LanguageCode = "En";

            CharacterCode = CharacterCode.Standard;
        }

        public override void Append2inchTextReceiptData(ICommandBuilder builder, bool utf8)
        {
            string encoding;
            if (utf8)
            {
                encoding = "UTF-8";

                builder.AppendCodePage(CodePageType.UTF8);
            }
            else
            {
                encoding = "ASCII";

                builder.AppendCodePage(CodePageType.CP998);
            }

            builder.AppendInternational(InternationalType.USA);

            builder.AppendCharacterSpace(0);

            builder.AppendAlignment(AlignmentPosition.Center);

            builder.AppendData(Encoding.GetEncoding(encoding).GetBytes("[Sample receipt]\n").AsBuffer());

            builder.AppendData(Encoding.GetEncoding(encoding).GetBytes(
                            "**** Boutique\n" +
                            "123 Star Road\n" +
                            "City, State 12345\n" +
                            "\n").AsBuffer());

            builder.AppendAlignment(AlignmentPosition.Left);

            builder.AppendData(Encoding.GetEncoding(encoding).GetBytes(
                            "Date:MM/DD/YYYY    Time:HH:MM PM\n" +
                            "--------------------------------\n" +
                            "\n").AsBuffer());

            builder.AppendData(Encoding.GetEncoding(encoding).GetBytes(
                    "SKU         Description    Total\n" +
                    "300678566   PLAIN T-SHIRT  10.99\n" +
                    "300692003   BLACK DENIM    29.99\n" +
                    "300651148   BLUE DENIM     29.99\n" +
                    "300642980   STRIPED DRESS  49.99\n" +
                    "300638471   BLACK BOOTS    35.99\n" +
                    "\n" +
                    "Subtotal                  156.95\n" +
                    "Tax                         0.00\n" +
                    "--------------------------------\n").AsBuffer());

            builder.AppendData(Encoding.GetEncoding(encoding).GetBytes("Total     ").AsBuffer());

            builder.AppendDataWithMultiple(Encoding.GetEncoding(encoding).GetBytes("   $156.95\n").AsBuffer(), 2, 2);

            builder.AppendData(Encoding.GetEncoding(encoding).GetBytes(
                    "--------------------------------\n" +
                    "\n" +
                    "Charge\n" +
                    "156.95\n" +
                    "Visa XXXX-XXXX-XXXX-0123\n" +
                    "\n").AsBuffer());

            builder.AppendDataWithInvert(Encoding.GetEncoding(encoding).GetBytes("Refunds and Exchanges\n").AsBuffer());

            builder.AppendData(Encoding.GetEncoding(encoding).GetBytes("Within ").AsBuffer());

            builder.AppendDataWithUnderLine(Encoding.GetEncoding(encoding).GetBytes("30 days").AsBuffer());

            builder.AppendData(Encoding.GetEncoding(encoding).GetBytes(" with receipt\n").AsBuffer());

            builder.AppendData(Encoding.GetEncoding(encoding).GetBytes(
                    "And tags attached\n" +
                    "\n").AsBuffer());

            builder.AppendAlignment(AlignmentPosition.Center);

            builder.AppendBarcodeData(Encoding.GetEncoding("ASCII").GetBytes("{BStar.").AsBuffer(), BarcodeSymbology.Code128, BarcodeWidth.Mode2, 40, true);
        }

        public override void Append3inchTextReceiptData(ICommandBuilder builder, bool utf8)
        {
            string encoding;
            if (utf8)
            {
                encoding = "UTF-8";

                builder.AppendCodePage(CodePageType.UTF8);
            }
            else
            {
                encoding = "ASCII";

                builder.AppendCodePage(CodePageType.CP998);
            }

            builder.AppendInternational(InternationalType.USA);

            builder.AppendCharacterSpace(0);

            builder.AppendAlignment(AlignmentPosition.Center);

            builder.AppendData(Encoding.GetEncoding(encoding).GetBytes("[Sample receipt]\n").AsBuffer());

            builder.AppendData(Encoding.GetEncoding(encoding).GetBytes(
                            "**** Boutique\n" +
                            "123 Star Road\n" +
                            "City, State 12345\n" +
                            "\n").AsBuffer());

            builder.AppendAlignment(AlignmentPosition.Left);

            builder.AppendData(Encoding.GetEncoding(encoding).GetBytes(
                            "Date:MM/DD/YYYY                    Time:HH:MM PM\n" +
                            "------------------------------------------------\n" +
                            "\n").AsBuffer());

            builder.AppendDataWithEmphasis(Encoding.GetEncoding(encoding).GetBytes("SALE\n").AsBuffer());

            builder.AppendData(Encoding.GetEncoding(encoding).GetBytes(
                            "SKU               Description              Total\n" +
                            "300678566         PLAIN T-SHIRT            10.99\n" +
                            "300692003         BLACK DENIM              29.99\n" +
                            "300651148         BLUE DENIM               29.99\n" +
                            "300642980         STRIPED DRESS            49.99\n" +
                            "300638471         BLACK BOOTS              35.99\n" +
                            "\n" +
                            "Subtotal                                  156.95\n" +
                            "Tax                                         0.00\n" +
                            "------------------------------------------------\n").AsBuffer());

            builder.AppendData(Encoding.GetEncoding(encoding).GetBytes(
                           "Total                       ").AsBuffer());

            builder.AppendDataWithMultiple(Encoding.GetEncoding(encoding).GetBytes("   $156.95\n").AsBuffer(), 2, 2);

            builder.AppendData(Encoding.GetEncoding(encoding).GetBytes(
                            "------------------------------------------------\n" +
                            "\n" +
                            "Charge\n" +
                            "156.95\n" +
                            "Visa XXXX-XXXX-XXXX-0123\n" +
                            "\n").AsBuffer());

            builder.AppendDataWithInvert(Encoding.GetEncoding(encoding).GetBytes("Refunds and Exchanges\n").AsBuffer());

            builder.AppendData(Encoding.GetEncoding(encoding).GetBytes("Within ").AsBuffer());

            builder.AppendDataWithUnderLine(Encoding.GetEncoding(encoding).GetBytes("30 days").AsBuffer());

            builder.AppendData(Encoding.GetEncoding(encoding).GetBytes(" with receipt\n").AsBuffer());

            builder.AppendData(Encoding.GetEncoding(encoding).GetBytes(
                    "And tags attached\n" +
                            "\n").AsBuffer());

            builder.AppendAlignment(AlignmentPosition.Center);

            builder.AppendBarcodeData(Encoding.GetEncoding("ASCII").GetBytes("{BStar.").AsBuffer(), BarcodeSymbology.Code128, BarcodeWidth.Mode2, 40, true);
        }

        public override void Append4inchTextReceiptData(ICommandBuilder builder, bool utf8)
        {
            string encoding;
            if (utf8)
            {
                encoding = "UTF-8";

                builder.AppendCodePage(CodePageType.UTF8);
            }
            else
            {
                encoding = "ASCII";

                builder.AppendCodePage(CodePageType.CP998);
            }

            builder.AppendInternational(InternationalType.USA);

            builder.AppendCharacterSpace(0);

            builder.AppendAlignment(AlignmentPosition.Center);

            builder.AppendData(Encoding.GetEncoding(encoding).GetBytes("[Sample receipt]\n").AsBuffer());

            builder.AppendData(Encoding.GetEncoding(encoding).GetBytes(
                            "**** Boutique\n" +
                            "123 Star Road\n" +
                            "City, State 12345\n" +
                            "\n").AsBuffer());

            builder.AppendAlignment(AlignmentPosition.Left);

            builder.AppendData(Encoding.GetEncoding(encoding).GetBytes(
                            "Date:MM/DD/YYYY                                         Time:HH:MM PM\n" +
                            "---------------------------------------------------------------------\n" +
                            "\n").AsBuffer());

            builder.AppendDataWithEmphasis(Encoding.GetEncoding(encoding).GetBytes("SALE\n").AsBuffer());

            builder.AppendData(Encoding.GetEncoding(encoding).GetBytes(
                            "SKU                        Description                          Total\n" +
                            "300678566                  PLAIN T-SHIRT                        10.99\n" +
                            "300692003                  BLACK DENIM                          29.99\n" +
                            "300651148                  BLUE DENIM                           29.99\n" +
                            "300642980                  STRIPED DRESS                        49.99\n" +
                            "300638471                  BLACK BOOTS                          35.99\n" +
                            "\n" +
                            "Subtotal                                                       156.95\n" +
                            "Tax                                                              0.00\n" +
                            "---------------------------------------------------------------------\n").AsBuffer());

            builder.AppendData(Encoding.GetEncoding(encoding).GetBytes(
                           "Total                                            ").AsBuffer());

            builder.AppendDataWithMultiple(Encoding.GetEncoding(encoding).GetBytes(
                                    "   $156.95\n").AsBuffer(), 2, 2);

            builder.AppendData(Encoding.GetEncoding(encoding).GetBytes(
                            "---------------------------------------------------------------------\n" +
                            "\n" +
                            "Charge\n" +
                            "156.95\n" +
                            "Visa XXXX-XXXX-XXXX-0123\n" +
                            "\n").AsBuffer());

            builder.AppendDataWithInvert(Encoding.GetEncoding(encoding).GetBytes(
                                  "Refunds and Exchanges\n").AsBuffer());

            builder.AppendData(Encoding.GetEncoding(encoding).GetBytes(
                            "Within ").AsBuffer());

            builder.AppendDataWithUnderLine(Encoding.GetEncoding(encoding).GetBytes(
                                     "30 days").AsBuffer());

            builder.AppendData(Encoding.GetEncoding(encoding).GetBytes(
                            " with receipt\n").AsBuffer());

            builder.AppendData(Encoding.GetEncoding(encoding).GetBytes(
                    "And tags attached\n" +
                            "\n").AsBuffer());

            builder.AppendAlignment(AlignmentPosition.Center);

            builder.AppendBarcodeData(Encoding.GetEncoding("ASCII").GetBytes("{BStar.").AsBuffer(), BarcodeSymbology.Code128, BarcodeWidth.Mode2, 40, true);

        }

        public override void AppendEscPos3inchTextReceiptData(ICommandBuilder builder, bool utf8)
        {
            string encoding;
            if (utf8)
            {
                encoding = "UTF-8";

                builder.AppendCodePage(CodePageType.UTF8);
            }
            else
            {
                encoding = "ASCII";

                builder.AppendCodePage(CodePageType.CP998);
            }

            builder.AppendInternational(InternationalType.USA);

            builder.AppendCharacterSpace(0);

            builder.AppendAlignment(AlignmentPosition.Center);

            builder.AppendData(Encoding.GetEncoding(encoding).GetBytes("[Sample receipt]\n").AsBuffer());

            builder.AppendData(Encoding.GetEncoding(encoding).GetBytes(
                            "**** Boutique\n" +
                            "123 Star Road\n" +
                            "City, State 12345\n" +
                            "\n").AsBuffer());

            builder.AppendAlignment(AlignmentPosition.Left);

            builder.AppendData(Encoding.GetEncoding(encoding).GetBytes(
                    "Date:MM/DD/YYYY              Time:HH:MM PM\n" +
                    "------------------------------------------\n" +
                    "\n").AsBuffer());

            builder.AppendDataWithEmphasis(Encoding.GetEncoding(encoding).GetBytes(
                                    "SALE \n").AsBuffer());

            builder.AppendData(Encoding.GetEncoding(encoding).GetBytes(
                            "SKU            Description           Total\n" +
                            "300678566      PLAIN T-SHIRT         10.99\n" +
                            "300692003      BLACK DENIM           29.99\n" +
                            "300651148      BLUE DENIM            29.99\n" +
                            "300642980      STRIPED DRESS         49.99\n" +
                            "300638471      BLACK BOOTS           35.99\n" +
                            "\n" +
                            "Subtotal                            156.95\n" +
                            "Tax                                   0.00\n" +
                            "------------------------------------------\n").AsBuffer());

            builder.AppendData(Encoding.GetEncoding(encoding).GetBytes(
                            "Total                 ").AsBuffer());

            builder.AppendDataWithMultiple(Encoding.GetEncoding(encoding).GetBytes(
                                    "   $156.95\n").AsBuffer(), 2, 2);

            builder.AppendData(Encoding.GetEncoding(encoding).GetBytes(
                            "------------------------------------------\n" +
                            "\n" +
                            "Charge\n" +
                            "156.95\n" +
                            "Visa XXXX-XXXX-XXXX-0123\n" +
                            "\n").AsBuffer());

            builder.AppendDataWithInvert(Encoding.GetEncoding(encoding).GetBytes(
                                  "Refunds and Exchanges\n").AsBuffer());

            builder.AppendData(Encoding.GetEncoding(encoding).GetBytes(
                            "Within ").AsBuffer());

            builder.AppendDataWithUnderLine(Encoding.GetEncoding(encoding).GetBytes(
                                     "30 days").AsBuffer());

            builder.AppendData(Encoding.GetEncoding(encoding).GetBytes(
                            " with receipt\n").AsBuffer());

            builder.AppendData(Encoding.GetEncoding(encoding).GetBytes(
                    "And tags attached\n" +
                            "\n").AsBuffer());

            builder.AppendAlignment(AlignmentPosition.Center);

            builder.AppendBarcodeData(Encoding.GetEncoding("ASCII").GetBytes("{BStar.").AsBuffer(), BarcodeSymbology.Code128, BarcodeWidth.Mode2, 40, true);

        }

        public override void AppendDotImpact3inchTextReceiptData(ICommandBuilder builder, bool utf8)
        {
            string encoding;
            if (utf8)
            {
                encoding = "UTF-8";

                builder.AppendCodePage(CodePageType.UTF8);
            }
            else
            {
                encoding = "ASCII";

                builder.AppendCodePage(CodePageType.CP998);
            }

            builder.AppendInternational(InternationalType.USA);

            builder.AppendCharacterSpace(0);

            builder.AppendAlignment(AlignmentPosition.Center);

            builder.AppendData(Encoding.GetEncoding(encoding).GetBytes("[Sample receipt]\n").AsBuffer());

            builder.AppendData(Encoding.GetEncoding(encoding).GetBytes(
                            "**** Boutique\n" +
                            "123 Star Road\n" +
                            "City, State 12345\n" +
                            "\n").AsBuffer());

            builder.AppendAlignment(AlignmentPosition.Left);

            builder.AppendData(Encoding.GetEncoding(encoding).GetBytes(
                            "Date:MM/DD/YYYY              Time:HH:MM PM\n" +
                            "------------------------------------------\n" +
                            "\n").AsBuffer());

            builder.AppendDataWithEmphasis(Encoding.GetEncoding(encoding).GetBytes(
                                    "SALE \n").AsBuffer());

            builder.AppendData(Encoding.GetEncoding(encoding).GetBytes(
                            "SKU             Description          Total\n" +
                            "300678566       PLAIN T-SHIRT        10.99\n" +
                            "300692003       BLACK DENIM          29.99\n" +
                            "300651148       BLUE DENIM           29.99\n" +
                            "300642980       STRIPED DRESS        49.99\n" +
                            "300638471       BLACK BOOTS          35.99\n" +
                            "\n" +
                            "Subtotal                            156.95\n" +
                            "Tax                                   0.00\n" +
                            "------------------------------------------\n" +
                            "Total                              $156.95\n" +
                            "------------------------------------------\n" +
                            "\n" +
                            "Charge\n" +
                            "156.95\n" +
                            "Visa XXXX-XXXX-XXXX-0123\n" +
                            "\n").AsBuffer());

            builder.AppendDataWithInvert(Encoding.GetEncoding(encoding).GetBytes(
                                  "Refunds and Exchanges\n").AsBuffer());

            builder.AppendData(Encoding.GetEncoding(encoding).GetBytes(
                            "Within ").AsBuffer());

            builder.AppendDataWithUnderLine(Encoding.GetEncoding(encoding).GetBytes(
                                     "30 days").AsBuffer());

            builder.AppendData(Encoding.GetEncoding(encoding).GetBytes(
                            " with receipt\n").AsBuffer());
        }

        public override async Task<BitmapDecoder> CreateCouponImageAsync()
        {
            BitmapDecoder logoData = null;

            StorageFile logoFile = await StorageFile.GetFileFromApplicationUriAsync(new Uri("ms-appx:///Assets/coupon_image.png"));
            using (IRandomAccessStreamWithContentType logoStream = await logoFile.OpenReadAsync())
            {
                logoData = await BitmapDecoder.CreateAsync(logoStream);
            }
            logoFile = null;

            return logoData;
        }

        public override string Create2inchRasterReceiptText()
        {
            String textToPrint =
                "                 [Sample receipt]\n" +
                "                  **** Boutique\n" +
                "                  123 Star Road\n" +
                "                City, State 12345\n" +
                "\n" +
                "Date:MM/DD/YYYY Time:HH:MM PM\n" +
                "----------------------------------------\n" +
                "SALE\n" +
                "SKU\t\tDescription\t\t    Total\n" +
                "300678566\tPLAIN T-SHIRT\t    10.99\n" +
                "300692003\tBLACK DENIM\t    29.99\n" +
                "300651148\tBLUE DENIM\t    29.99\n" +
                "300642980\tSTRIPED DRESS\t    49.99\n" +
                "300638471  BLACK BOOTS\t    35.99\n" +
                "\n" +
                "Subtotal\t\t\t\t  156.95\n" +
                "Tax\t\t\t\t\t      0.00\n" +
                "----------------------------------------\n" +
                "Total\t\t\t\t\t$156.95\n" +
                "----------------------------------------\n" +
                "\n" +
                "Charge\n" +
                "156.95\n" +
                "Visa XXXX-XXXX-XXXX-0123\n" +
                "Refunds and Exchanges\n" +
                "Within 30 days with receipt\n" +
                "And tags attached\n";

            return textToPrint;
        }

        public override string Create3inchRasterReceiptText()
        {
            String textToPrint =
                    "                   [Sample receipt]\n" +
                    "                    **** Boutique\n" +
                    "                    123 Star Road\n" +
                    "                  City, State 12345\n" +
                    "\n" +
                    "Date:MM/DD/YYYY  Time:HH:MM PM\n" +
                    "------------------------------------------\n" +
                    "SALE\n" +
                    "SKU       \t\tDescription \tTotal\n" +
                    "300678566\tPLAIN T-SHIRT \t10.99\n" +
                    "300692003\tBLACK DENIM   \t29.99\n" +
                    "300651148\tBLUE DENIM    \t29.99\n" +
                    "300642980\tSTRIPED DRESS \t49.99\n" +
                    "300638471\tBLACK BOOTS   \t35.99\n" +
                    "\n" +
                    "Subtotal\t\t\t\t       156.95\n" +
                    "Tax\t\t\t\t\t           0.00\n" +
                    "------------------------------------------\n" +
                    "Total\t\t\t\t              $156.95\n" +
                    "------------------------------------------\n" +
                    "\n" +
                    "Charge\n" +
                    "156.95\n" +
                    "Visa XXXX-XXXX-XXXX-0123\n" +
                    "Refunds and Exchanges\n" +
                    "Within 30 days with receipt\n" +
                    "And tags attached\n";

            return textToPrint;
        }

        public override string Create4inchRasterReceiptText()
        {
            String textToPrint =
                    "                                          [Sample receipt]\n" +
                    "                                           **** Boutique\n" +
                    "                                           123 Star Road\n" +
                    "                                         City, State 12345\n" +
                    "\n" +
                    "Date:MM/DD/YYYY                                                 Time:HH:MM PM\n" +
                    "---------------------------------------------------------------------------\n" +
                    "SALE\n" +
                    "SKU\t\t                          Description\t\t               Total\n" +
                    "300678566                          PLAIN T-SHIRT\t\t               10.99\n" +
                    "300692003                          BLACK DENIM\t\t               29.99\n" +
                    "300651148                          BLUE DENIM\t\t               29.99\n" +
                    "300642980                          STRIPED DRESS\t\t               49.99\n" +
                    "300638471                          BLACK BOOTS\t\t               35.99\n" +
                    "\n" +
                    "Subtotal\t\t\t\t\t                                       156.95\n" +
                    "Tax\t\t\t\t\t                                                    0.00\n" +
                    "---------------------------------------------------------------------------\n" +
                    "Total\t\t\t\t\t                                              $156.95\n" +
                    "---------------------------------------------------------------------------\n" +
                    "\n" +
                    "Charge\n" +
                    "156.95\n" +
                    "Visa XXXX-XXXX-XXXX-0123\n" +
                    "Refunds and Exchanges\n" +
                    "Within 30 days with receipt\n" +
                    "And tags attached\n";

            return textToPrint;
        }


        public override async Task<BitmapDecoder> Create2inchRasterImageAsync()
        {
            string text = Create2inchRasterReceiptText();

            await CreateImageAsync(text, 22, 384, RASTER_IMAGE_FILE_NAME); // 2inch(384dots)

            return await GetRasterImage(RASTER_IMAGE_FILE_NAME);
        }


        public override async Task<BitmapDecoder> Create3inchRasterImageAsync()
        {
            string text = Create3inchRasterReceiptText();

            await CreateImageAsync(text, 25, 576, RASTER_IMAGE_FILE_NAME); // 3inch(576dots)

            return await GetRasterImage(RASTER_IMAGE_FILE_NAME);
        }

        public override async Task<BitmapDecoder> CreateEscPos3inchRasterImageAsync()
        {
            string text = Create3inchRasterReceiptText();

            await CreateImageAsync(text, 25, 512, RASTER_IMAGE_FILE_NAME); // EscPos3inch(512dots)

            return await GetRasterImage(RASTER_IMAGE_FILE_NAME);
        }

        public override async Task<BitmapDecoder> Create4inchRasterImageAsync()
        {
            string text = Create4inchRasterReceiptText();

            await CreateImageAsync(text, 25, 832, RASTER_IMAGE_FILE_NAME); // 4inch(832dots)

            return await GetRasterImage(RASTER_IMAGE_FILE_NAME);
        }

        public override void AppendTextLabelData(ICommandBuilder builder, bool utf8)
        {
            string encoding;
            if (utf8)
            {
                encoding = "UTF-8";

                builder.AppendCodePage(CodePageType.UTF8);
            }
            else
            {
                encoding = "ASCII";

                builder.AppendCodePage(CodePageType.CP998);
            }

            builder.AppendInternational(InternationalType.USA);

            builder.AppendCharacterSpace(0);

            builder.AppendUnitFeed(20 * 2);

            builder.AppendMultipleHeight(2);

            builder.AppendData(Encoding.GetEncoding(encoding).GetBytes(
                        "Star Micronics America, Inc.").AsBuffer());

            builder.AppendUnitFeed(64);

            builder.AppendData(Encoding.GetEncoding(encoding).GetBytes(
            "65 Clyde Road Suite G").AsBuffer());

            builder.AppendUnitFeed(64);

            builder.AppendData(Encoding.GetEncoding(encoding).GetBytes(
            "Somerset, NJ 08873-3485 U.S.A").AsBuffer());

            builder.AppendUnitFeed(64);

            builder.AppendMultipleHeight(1);
        }

        public override string CreatePasteTextLabelString()
        {
            return "Star Micronics America, Inc.\n" +
                   "65 Clyde Road Suite G\n" +
                   "Somerset, NJ 08873-3485 U.S.A";
        }

        public override void AppendPasteTextLabelData(ICommandBuilder builder, string pasteText, bool utf8)
        {
            string encoding;
            if (utf8)
            {
                encoding = "UTF-8";

                builder.AppendCodePage(CodePageType.UTF8);
            }
            else
            {
                encoding = "ASCII";

                builder.AppendCodePage(CodePageType.CP998);
            }

            builder.AppendInternational(InternationalType.USA);

            builder.AppendCharacterSpace(0);

            builder.AppendData(Encoding.GetEncoding(encoding).GetBytes(pasteText).AsBuffer());
        }
    }
}
