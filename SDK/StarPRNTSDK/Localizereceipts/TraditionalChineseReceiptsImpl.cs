using StarIO_Extension;
using System;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading.Tasks;
using Windows.Graphics.Imaging;
using Windows.Storage;
using Windows.Storage.Streams;
using Windows.UI.Xaml.Media.Imaging;

namespace StarPRNTSDK.Localizereceipts
{
    class TraditionalChineseReceiptsImpl : ILocalizeReceipts
    {
        public TraditionalChineseReceiptsImpl()
        {
            LanguageCode = "zh-TW";

            CharacterCode = CharacterCode.TraditionalChinese;
        }

        public override void Append2inchTextReceiptData(StarIO_Extension.ICommandBuilder builder, bool utf8)
        {
            string encoding;
            if (utf8)
            {
                encoding = "UTF-8";

                builder.AppendCodePage(CodePageType.UTF8);
            }
            else
            {
                encoding = "Big5";
            }

            builder.AppendCharacterSpace(0);

            builder.AppendAlignment(AlignmentPosition.Center);

            builder.AppendEmphasis(true);

            builder.AppendDataWithMultipleHeight(Encoding.GetEncoding(encoding).GetBytes(
                                         "Star Micronics\n").AsBuffer(), 3);

            builder.AppendEmphasis(false);

            builder.AppendData(Encoding.GetEncoding(encoding).GetBytes(
                           "--------------------------------\n").AsBuffer());

            builder.AppendDataWithMultiple(Encoding.GetEncoding(encoding).GetBytes(
                    "電子發票證明聯\n" +
                            "103年01-02月\n" +
                            "EV-99999999\n").AsBuffer(), 2, 2);

            builder.AppendAlignment(AlignmentPosition.Left);

            builder.AppendData(Encoding.GetEncoding(encoding).GetBytes(
                            "2014/01/15 13:00\n" +
                    "隨機碼 : 9999    總計 : 999\n" +
                    "賣方 : 99999999\n" +
                    "\n").AsBuffer());

            builder.AppendAlignment(AlignmentPosition.Center);

            builder.AppendQrCodeData(Encoding.GetEncoding("ASCII").GetBytes("http://www.star-m.jp/eng/index.html").AsBuffer(), QrCodeModel.No2, QrCodeLevel.Q, 5);

            builder.AppendAlignment(AlignmentPosition.Left);

            builder.AppendData(Encoding.GetEncoding(encoding).GetBytes(
                    "\n" +
                            "商品退換請持本聯及銷貨明細表。\n" +
                            "9999999-9999999 999999-999999 9999\n" +
                            "\n").AsBuffer());

            builder.AppendDataWithAlignment(Encoding.GetEncoding(encoding).GetBytes(
                                            "銷貨明細表 　(銷售)\n").AsBuffer(), AlignmentPosition.Center);

            builder.AppendDataWithAlignment(Encoding.GetEncoding(encoding).GetBytes(
                                    "2014-01-15 13:00:02\n").AsBuffer(), AlignmentPosition.Right);

            builder.AppendData(Encoding.GetEncoding(encoding).GetBytes(
                    "\n" +
                            "烏龍袋茶2g20入       55 x2 110TX\n" +
                            "茉莉烏龍茶2g20入     55 x2 110TX\n" +
                            "天仁觀音茶2g*20      55 x2 110TX\n").AsBuffer());

            builder.AppendDataWithEmphasis(Encoding.GetEncoding(encoding).GetBytes(
                    "      小　 計 :              330\n" +
                            "      總   計 :              330\n").AsBuffer());

            builder.AppendData(Encoding.GetEncoding(encoding).GetBytes(
                    "--------------------------------\n" +
                            "現 金                        400\n" +
                            "      找　 零 :               70\n").AsBuffer());

            builder.AppendDataWithEmphasis(Encoding.GetEncoding(encoding).GetBytes(
                                           " 101 發票金額 :              330\n").AsBuffer());

            builder.AppendData(Encoding.GetEncoding(encoding).GetBytes(
                    "2014-01-15 13:00\n" +
                            "\n").AsBuffer());

            builder.AppendAlignment(AlignmentPosition.Center);

            builder.AppendBarcodeData(Encoding.GetEncoding("ASCII").GetBytes("{BStar.").AsBuffer(), BarcodeSymbology.Code128, BarcodeWidth.Mode2, 40, true);

            builder.AppendAlignment(AlignmentPosition.Left);

            builder.AppendData(Encoding.GetEncoding(encoding).GetBytes(
                    "\n" +
                            "商品退換、贈品及停車兌換請持本聯。\n" +
                            "9999999-9999999 999999-999999 9999\n").AsBuffer());

        }

        public override void Append3inchTextReceiptData(StarIO_Extension.ICommandBuilder builder, bool utf8)
        {
            string encoding;
            if (utf8)
            {
                encoding = "UTF-8";

                builder.AppendCodePage(CodePageType.UTF8);
            }
            else
            {
                encoding = "Big5";
            }

            builder.AppendCharacterSpace(0);

            builder.AppendAlignment(AlignmentPosition.Center);

            builder.AppendEmphasis(true);

            builder.AppendDataWithMultipleHeight(Encoding.GetEncoding(encoding).GetBytes(
                                         "Star Micronics\n").AsBuffer(), 3);

            builder.AppendEmphasis(false);

            builder.AppendData(Encoding.GetEncoding(encoding).GetBytes(
                           "------------------------------------------------\n").AsBuffer());

            builder.AppendDataWithMultiple(Encoding.GetEncoding(encoding).GetBytes(
                    "電子發票證明聯\n" +
                            "103年01-02月\n" +
                            "EV-99999999\n").AsBuffer(), 2, 2);

            builder.AppendAlignment(AlignmentPosition.Left);

            builder.AppendData(Encoding.GetEncoding(encoding).GetBytes(
                            "2014/01/15 13:00\n" +
                    "隨機碼 : 9999    總計 : 999\n" +
                    "賣方 : 99999999\n" +
                    "\n").AsBuffer());

            builder.AppendAlignment(AlignmentPosition.Center);

            builder.AppendQrCodeData(Encoding.GetEncoding("ASCII").GetBytes("http://www.star-m.jp/eng/index.html").AsBuffer(), QrCodeModel.No2, QrCodeLevel.Q, 5);

            builder.AppendAlignment(AlignmentPosition.Left);

            builder.AppendData(Encoding.GetEncoding(encoding).GetBytes(
                    "\n" +
                            "商品退換請持本聯及銷貨明細表。\n" +
                            "9999999-9999999 999999-999999 9999\n" +
                            "\n").AsBuffer());

            builder.AppendDataWithAlignment(Encoding.GetEncoding(encoding).GetBytes(
                                    "銷貨明細表 　(銷售)\n").AsBuffer(), AlignmentPosition.Center);

            builder.AppendDataWithAlignment(Encoding.GetEncoding(encoding).GetBytes(
                                    "2014-01-15 13:00:02\n").AsBuffer(), AlignmentPosition.Right);

            builder.AppendData(Encoding.GetEncoding(encoding).GetBytes(
                    "\n" +
                            "烏龍袋茶2g20入                55 x2 110TX\n" +
                            "茉莉烏龍茶2g20入              55 x2 110TX\n" +
                            "天仁觀音茶2g*20               55 x2 110TX\n").AsBuffer());

            builder.AppendDataWithEmphasis(Encoding.GetEncoding(encoding).GetBytes(
                    "      小　 計 :                330\n" +
                            "      總   計 :                330\n").AsBuffer());

            builder.AppendData(Encoding.GetEncoding(encoding).GetBytes(
                    "------------------------------------------------\n" +
                            "現 金                          400\n" +
                            "      找　 零 :                 70\n").AsBuffer());

            builder.AppendDataWithEmphasis(Encoding.GetEncoding(encoding).GetBytes(
                                   " 101 發票金額 :                330\n").AsBuffer());

            builder.AppendData(Encoding.GetEncoding(encoding).GetBytes(
                    "2014-01-15 13:00\n" +
                            "\n").AsBuffer());

            builder.AppendAlignment(AlignmentPosition.Center);

            builder.AppendBarcodeData(Encoding.GetEncoding("ASCII").GetBytes("{BStar.").AsBuffer(), BarcodeSymbology.Code128, BarcodeWidth.Mode2, 40, true);

            builder.AppendAlignment(AlignmentPosition.Left);

            builder.AppendData(Encoding.GetEncoding(encoding).GetBytes(
                    "\n" +
                            "商品退換、贈品及停車兌換請持本聯。\n" +
                            "9999999-9999999 999999-999999 9999\n").AsBuffer());
        }

        public override void Append4inchTextReceiptData(StarIO_Extension.ICommandBuilder builder, bool utf8)
        {
            string encoding;
            if (utf8)
            {
                encoding = "UTF-8";

                builder.AppendCodePage(CodePageType.UTF8);
            }
            else
            {
                encoding = "Big5";
            }

            builder.AppendCharacterSpace(0);

            builder.AppendAlignment(AlignmentPosition.Center);

            builder.AppendEmphasis(true);

            builder.AppendDataWithMultipleHeight(Encoding.GetEncoding(encoding).GetBytes(
                                         "Star Micronics\n").AsBuffer(), 3);

            builder.AppendEmphasis(false);

            builder.AppendData(Encoding.GetEncoding(encoding).GetBytes(
                           "------------------------------------------------\n").AsBuffer());

            builder.AppendDataWithMultiple(Encoding.GetEncoding(encoding).GetBytes(
                    "電子發票證明聯\n" +
                            "103年01-02月\n" +
                            "EV-99999999\n").AsBuffer(), 2, 2);

            builder.AppendAlignment(AlignmentPosition.Left);

            builder.AppendData(Encoding.GetEncoding(encoding).GetBytes(
                            "2014/01/15 13:00\n" +
                    "隨機碼 : 9999    總計 : 999\n" +
                    "賣方 : 99999999\n").AsBuffer());

            builder.AppendAlignment(AlignmentPosition.Center);

            builder.AppendQrCodeData(Encoding.GetEncoding("ASCII").GetBytes("http://www.star-m.jp/eng/index.html").AsBuffer(), QrCodeModel.No2, QrCodeLevel.Q, 5);

            builder.AppendAlignment(AlignmentPosition.Left);

            builder.AppendData(Encoding.GetEncoding(encoding).GetBytes(
                    "\n" +
                            "商品退換請持本聯及銷貨明細表。\n" +
                            "9999999-9999999 999999-999999 9999\n" +
                            "\n").AsBuffer());

            builder.AppendDataWithAlignment(Encoding.GetEncoding(encoding).GetBytes(
                                    "銷貨明細表 　(銷售)\n").AsBuffer(), AlignmentPosition.Center);

            builder.AppendDataWithAlignment(Encoding.GetEncoding(encoding).GetBytes(
                                    "2014-01-15 13:00:02\n").AsBuffer(), AlignmentPosition.Right);

            builder.AppendData(Encoding.GetEncoding(encoding).GetBytes(
                    "\n" +
                            "烏龍袋茶2g20入                                    55 x2 110TX\n" +
                            "茉莉烏龍茶2g20入                                  55 x2 110TX\n" +
                            "天仁觀音茶2g*20                                   55 x2 110TX\n").AsBuffer());

            builder.AppendDataWithEmphasis(Encoding.GetEncoding(encoding).GetBytes(
                    "      小　 計 :                                    330\n" +
                            "      總   計 :                                    330\n").AsBuffer());

            builder.AppendData(Encoding.GetEncoding(encoding).GetBytes(
                    "---------------------------------------------------------------------\n" +
                            "現 金                                              400\n" +
                            "      找　 零 :                                     70\n").AsBuffer());

            builder.AppendDataWithEmphasis(Encoding.GetEncoding(encoding).GetBytes(
                                   " 101 發票金額 :                                    330\n").AsBuffer());

            builder.AppendData(Encoding.GetEncoding(encoding).GetBytes(
                    "2014-01-15 13:00\n" +
                            "\n").AsBuffer());

            builder.AppendAlignment(AlignmentPosition.Center);

            builder.AppendBarcodeData(Encoding.GetEncoding("ASCII").GetBytes("{BStar.").AsBuffer(), BarcodeSymbology.Code128, BarcodeWidth.Mode2, 40, true);

            builder.AppendAlignment(AlignmentPosition.Left);

            builder.AppendData(Encoding.GetEncoding(encoding).GetBytes(
                    "\n" +
                            "商品退換、贈品及停車兌換請持本聯。\n" +
                            "9999999-9999999 999999-999999 9999\n").AsBuffer());
        }

        public override void AppendEscPos3inchTextReceiptData(StarIO_Extension.ICommandBuilder builder, bool utf8)
        {
            string encoding;
            if (utf8)
            {
                encoding = "UTF-8";

                builder.AppendCodePage(CodePageType.UTF8);
            }
            else
            {
                encoding = "Big5";
            }

            builder.AppendCharacterSpace(0);

            builder.AppendAlignment(AlignmentPosition.Center);

            builder.AppendEmphasis(true);

            builder.AppendData(Encoding.GetEncoding(encoding).GetBytes(
                           "\n").AsBuffer());

            builder.AppendDataWithMultipleHeight(Encoding.GetEncoding(encoding).GetBytes(
                                         "Star Micronics\n").AsBuffer(), 3);

            builder.AppendEmphasis(false);

            builder.AppendData(Encoding.GetEncoding(encoding).GetBytes(
                           "------------------------------------------\n").AsBuffer());

            builder.AppendDataWithMultiple(Encoding.GetEncoding(encoding).GetBytes(
                    "電子發票證明聯\n" +
                            "103年01-02月\n" +
                            "EV-99999999\n").AsBuffer(), 2, 2);

            builder.AppendAlignment(AlignmentPosition.Left);

            builder.AppendData(Encoding.GetEncoding(encoding).GetBytes(
                            "2014/01/15 13:00\n" +
                    "隨機碼 : 9999    總計 : 999\n" +
                    "賣方 : 99999999\n" +
                    "\n").AsBuffer());

            builder.AppendAlignment(AlignmentPosition.Center);

            builder.AppendQrCodeData(Encoding.GetEncoding("ASCII").GetBytes("http://www.star-m.jp/eng/index.html").AsBuffer(), QrCodeModel.No2, QrCodeLevel.Q, 5);

            builder.AppendAlignment(AlignmentPosition.Left);

            builder.AppendData(Encoding.GetEncoding(encoding).GetBytes(
                    "\n" +
                            "商品退換請持本聯及銷貨明細表。\n" +
                            "9999999-9999999 999999-999999 9999\n" +
                            "\n").AsBuffer());

            builder.AppendDataWithAlignment(Encoding.GetEncoding(encoding).GetBytes(
                                    "銷貨明細表 　(銷售)\n").AsBuffer(), AlignmentPosition.Center);

            builder.AppendDataWithAlignment(Encoding.GetEncoding(encoding).GetBytes(
                                    "2014-01-15 13:00:02\n").AsBuffer(), AlignmentPosition.Right);

            builder.AppendData(Encoding.GetEncoding(encoding).GetBytes(
                    "\n" +
                            "烏龍袋茶2g20入                55 x2 110TX\n" +
                            "茉莉烏龍茶2g20入              55 x2 110TX\n" +
                            "天仁觀音茶2g*20               55 x2 110TX\n").AsBuffer());

            builder.AppendDataWithEmphasis(Encoding.GetEncoding(encoding).GetBytes(
                    "      小　 計 :                330\n" +
                            "      總   計 :                330\n").AsBuffer());

            builder.AppendData(Encoding.GetEncoding(encoding).GetBytes(
                    "------------------------------------------\n" +
                            "現 金                          400\n" +
                            "      找　 零 :                 70\n").AsBuffer());

            builder.AppendDataWithEmphasis(Encoding.GetEncoding(encoding).GetBytes(
                                   " 101 發票金額 :                330\n").AsBuffer());

            builder.AppendData(Encoding.GetEncoding(encoding).GetBytes(
                    "2014-01-15 13:00\n" +
                            "\n").AsBuffer());

            builder.AppendAlignment(AlignmentPosition.Center);

            builder.AppendBarcodeData(Encoding.GetEncoding("ASCII").GetBytes("{BStar.").AsBuffer(), BarcodeSymbology.Code128, BarcodeWidth.Mode2, 40, true);

            builder.AppendAlignment(AlignmentPosition.Left);

            builder.AppendData(Encoding.GetEncoding(encoding).GetBytes(
                    "\n" +
                            "商品退換、贈品及停車兌換請持本聯。\n" +
                            "9999999-9999999 999999-999999 9999\n").AsBuffer());
        }

        public override void AppendDotImpact3inchTextReceiptData(StarIO_Extension.ICommandBuilder builder, bool utf8)
        {
            string encoding;
            if (utf8)
            {
                encoding = "UTF-8";

                builder.AppendCodePage(CodePageType.UTF8);
            }
            else
            {
                encoding = "Big5";
            }

            builder.AppendCharacterSpace(0);

            builder.AppendAlignment(AlignmentPosition.Center);

            builder.AppendEmphasis(true);

            builder.AppendDataWithMultipleHeight(Encoding.GetEncoding(encoding).GetBytes(
                                         "Star Micronics\n").AsBuffer(), 3);

            builder.AppendEmphasis(false);

            builder.AppendData(Encoding.GetEncoding(encoding).GetBytes(
                           "------------------------------------------\n").AsBuffer());

            builder.AppendDataWithMultiple(Encoding.GetEncoding(encoding).GetBytes(
                    "電子發票證明聯\n" +
                            "103年01-02月\n" +
                            "EV-99999999\n").AsBuffer(), 2, 2);

            builder.AppendAlignment(AlignmentPosition.Left);

            builder.AppendData(Encoding.GetEncoding(encoding).GetBytes(
                            "2014/01/15 13:00\n" +
                    "隨機碼 : 9999    總計 : 999\n" +
                    "賣方 : 99999999\n" +
                    "\n" +
                    "商品退換請持本聯及銷貨明細表。\n" +
                    "9999999-9999999 999999-999999 9999\n" +
                    "\n").AsBuffer());

            builder.AppendDataWithAlignment(Encoding.GetEncoding(encoding).GetBytes(
                                    "銷貨明細表 　(銷售)\n").AsBuffer(), AlignmentPosition.Center);

            builder.AppendDataWithAlignment(Encoding.GetEncoding(encoding).GetBytes(
                                    "2014-01-15 13:00:02\n").AsBuffer(), AlignmentPosition.Right);

            builder.AppendData(Encoding.GetEncoding(encoding).GetBytes(
                    "\n" +
                            "烏龍袋茶2g20入             55 x2 110TX\n" +
                            "茉莉烏龍茶2g20入           55 x2 110TX\n" +
                            "天仁觀音茶2g*20            55 x2 110TX\n").AsBuffer());

            builder.AppendDataWithEmphasis(Encoding.GetEncoding(encoding).GetBytes(
                    "      小　 計 :             330\n" +
                            "      總   計 :             330\n").AsBuffer());

            builder.AppendData(Encoding.GetEncoding(encoding).GetBytes(
                    "------------------------------------------\n" +
                            "現 金                       400\n" +
                            "      找　 零 :              70\n").AsBuffer());

            builder.AppendDataWithEmphasis(Encoding.GetEncoding(encoding).GetBytes(
                                   " 101 發票金額 :             330\n").AsBuffer());

            builder.AppendData(Encoding.GetEncoding(encoding).GetBytes(
                    "2014-01-15 13:00\n" +
                            "\n" +
                            "商品退換、贈品及停車兌換請持本聯。\n" +
                            "9999999-9999999 999999-999999 9999\n").AsBuffer());
        }

        public override async Task<BitmapDecoder> CreateCouponImageAsync()
        {
            BitmapDecoder logoData = null;

            StorageFile logoFile = await StorageFile.GetFileFromApplicationUriAsync(new Uri("ms-appx:///Assets/coupon_image_traditional_chinese.png"));
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
                "　　　　      Star Micronics\n" +
                "----------------------------------------\n" +
                "                  電子發票證明聯\n" +
                "                  103年01-02月\n" +
                "                  EV-99999999\n" +
                "2014/01/15 13:00\n" +
                "隨機碼 : 9999      總計 : 999\n" +
                "賣　方 : 99999999\n" +
                "\n" +
                "商品退換請持本聯及銷貨明細表。\n" +
                "9999999-9999999 999999-999999\n" +
                "9999\n" +
                "\n" +
                "\n" +
                "                 銷貨明細表 　(銷售)\n" +
                "              2014-01-15 13:00:02\n" +
                "\n" +
                "烏龍袋茶2g20入\t 55 x2\t110TX\n" +
                "茉莉烏龍茶2g20入 55 x2\t110TX\n" +
                "天仁觀音茶2g*20\t 55 x2\t110TX\n" +
                "        小　　計 :　　             330\n" +
                "        總　　計 :　　             330\n" +
                "----------------------------------------\n" +
                "現　金　　　                \t    400\n" +
                "        找　　零 :　　              70\n" +
                " 101 發票金額 :　　             330\n" +
                "2014-01-15 13:00\n" +
                "\n" +
                "商品退換、贈品及停車兌換請持本聯。\n" +
                "9999999-9999999 999999-999999\n" +
                "9999\n";

            return textToPrint;
        }

        public override string Create3inchRasterReceiptText()
        {
            String textToPrint =
                "\t       Star Micronics\n" +
                "------------------------------------------\n" +
                "\t       電子發票證明聯\n" +
                "\t       103年01-02月\n" +
                "\t       EV-99999999\n" +
                "2014/01/15 13:00\n" +
                "隨機碼 : 9999      總計 : 999\n" +
                "賣　方 : 99999999\n" +
                "\n" +
                "商品退換請持本聯及銷貨明細表。\n" +
                "9999999-9999999 999999-999999\n" +
                "9999\n" +
                "\n" +
                "\n" +
                "\t\t       銷貨明細表 　(銷售)\n" +
                "\t\t       2014-01-15 13:00:02\n" +
                "\n" +
                "烏龍袋茶2g20入\t55 x2\t\t110TX\n" +
                "茉莉烏龍茶2g20入\t55 x2\t\t110TX\n" +
                "天仁觀音茶2g*20\t55 x2\t\t110TX\n" +
                " \t\t\t小　　計 :\t\t330\n" +
                " \t\t\t總　　計 :\t\t330\n" +
                "------------------------------------------\n" +
                "\t\t\t現　金\t\t400\n" +
                "\t\t找　　零\t :\t\t  70\n" +
                "\t\t101 發票金額 :\t\t330\n" +
                "2014-01-15 13:00\n" +
                "\n" +
                "商品退換、贈品及停車兌換請持本聯。\n" +
                "9999999-9999999 999999-999999\n" +
                "9999\n";

            return textToPrint;
        }

        public override string Create4inchRasterReceiptText()
        {
            String textToPrint =
                "\t\t\t\t        Star Micronics\n" +
                "---------------------------------------------------------------------------\n" +
                "\t\t\t\t        電子發票證明聯\n" +
                "\t\t\t\t        103年01-02月\n" +
                "\t\t\t\t        EV-99999999\n" +
                "2014/01/15 13:00\n" +
                "隨機碼 : 9999      總計 : 999\n" +
                "賣　方 : 99999999\n" +
                "\n" +
                "商品退換請持本聯及銷貨明細表。\n" +
                "9999999-9999999 999999-999999 9999\n" +
                "\n" +
                "\n" +
                "\t\t\t\t        銷貨明細表 　(銷售)\n" +
                "\t\t\t\t\t\t        2014-01-15 13:00:02\n" +
                "\n" +
                "烏龍袋茶2g20入\t\t\t\t55 x2\t\t\t   110TX\n" +
                "茉莉烏龍茶2g20入\t\t\t\t55 x2\t\t\t   110TX\n" +
                "天仁觀音茶2g*20\t\t\t\t55 x2\t\t\t   110TX\n" +
                "\t\t\t小　　計 :\t\t\t\t　　                  330\n" +
                "\t\t\t總　　計 :\t\t\t\t　　                  330\n" +
                "---------------------------------------------------------------------------\n" +
                "現　金\t\t\t\t\t\t\t　　                  400\n" +
                "\t\t\t找　　零 :\t\t\t\t　　                    70\n" +
                "\t\t\t101 發票金額 :\t\t\t　　                  330\n" +
                "2014-01-15 13:00\n" +
                "\n" +
                "商品退換、贈品及停車兌換請持本聯。\n" +
                "9999999-9999999 999999-999999 9999\n";

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
            // not implemented
        }

        public override string CreatePasteTextLabelString()
        {
            // not implemented
            return null;
        }

        public override void AppendPasteTextLabelData(ICommandBuilder builder, string pasteText, bool utf8)
        {
            // not implemented
        }
    }
}
