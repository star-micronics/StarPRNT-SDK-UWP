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
    class JapaneseReceiptsImpl : ILocalizeReceipts
    {
        public JapaneseReceiptsImpl()
        {
            LanguageCode = "Ja";

            CharacterCode = CharacterCode.Japanese;
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
                encoding = "Shift_JIS";

                builder.AppendCodePage(CodePageType.CP932);
            }

            builder.AppendInternational(InternationalType.Japan);

            builder.AppendCharacterSpace(0);

            builder.AppendAlignment(AlignmentPosition.Center);

            builder.AppendEmphasis(true);

            builder.AppendDataWithMultipleHeight(Encoding.GetEncoding(encoding).GetBytes("スター電機\n").AsBuffer(), 3);

            builder.AppendDataWithMultipleHeight(Encoding.GetEncoding(encoding).GetBytes("修理報告書　兼領収書\n").AsBuffer(), 2);

            builder.AppendEmphasis(false);

            builder.AppendAlignment(AlignmentPosition.Left);

            builder.AppendData(Encoding.GetEncoding(encoding).GetBytes(
                    "--------------------------------\n" +
                            "発行日時：YYYY年MM月DD日HH時MM分\n" +
                            "TEL：054-347-XXXX\n" +
                            "\n" +
                            "         ｲｹﾆｼ  ｼｽﾞｺ   ｻﾏ\n" +
                            "お名前：池西　静子　様\n" +
                            "御住所：静岡市清水区七ツ新屋\n" +
                            "　　　　５３６番地\n" +
                            "伝票番号：No.12345-67890\n" +
                            "\n" +
                            "　この度は修理をご用命頂き有難うございます。\n" +
                            " 今後も故障など発生した場合はお気軽にご連絡ください。\n" +
                            "\n" +
                            "品名／型名　数量　金額　備考\n" +
                            "--------------------------------\n" +
                            "制御基板　　   1 10,000  配達\n" +
                            "操作スイッチ   1  3,800  配達\n" +
                            "パネル　　　   1  2,000  配達\n" +
                            "技術料　　　   1 15,000\n" +
                            "出張費用　　   1  5,000\n" +
                            "--------------------------------\n" +
                            "\n" +
                            "             小計      \\ 35,800\n" +
                            "             内税      \\  1,790\n" +
                            "             合計      \\ 37,590\n" +
                            "\n" +
                            "　お問合わせ番号　12345-67890\n" +
                            "\n").AsBuffer());

            builder.AppendAlignment(AlignmentPosition.Center);

            builder.AppendBarcodeData(Encoding.GetEncoding("ASCII").GetBytes("{BStar.").AsBuffer(), BarcodeSymbology.Code128, BarcodeWidth.Mode2, 40, true);
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
                encoding = "Shift_JIS";

                builder.AppendCodePage(CodePageType.CP932);
            }

            builder.AppendInternational(InternationalType.Japan);

            builder.AppendCharacterSpace(0);

            builder.AppendAlignment(AlignmentPosition.Center);

            builder.AppendEmphasis(true);

            builder.AppendDataWithMultipleHeight(Encoding.GetEncoding(encoding).GetBytes("スター電機\n").AsBuffer(), 3);

            builder.AppendDataWithMultipleHeight(Encoding.GetEncoding(encoding).GetBytes("修理報告書　兼領収書\n").AsBuffer(), 2);

            builder.AppendEmphasis(false);

            builder.AppendAlignment(AlignmentPosition.Left);

            builder.AppendData(Encoding.GetEncoding(encoding).GetBytes(
                    "------------------------------------------------\n" +
                            "発行日時：YYYY年MM月DD日HH時MM分\n" +
                            "TEL：054-347-XXXX\n" +
                            "\n" +
                            "           ｲｹﾆｼ  ｼｽﾞｺ   ｻﾏ\n" +
                            "　お名前：池西　静子　様\n" +
                            "　御住所：静岡市清水区七ツ新屋\n" +
                            "　　　　　５３６番地\n" +
                            "　伝票番号：No.12345-67890\n" +
                            "\n" +
                            "　この度は修理をご用命頂き有難うございます。\n" +
                            " 今後も故障など発生した場合はお気軽にご連絡ください。\n" +
                            "\n" +
                            "品名／型名　          数量      金額　   備考\n" +
                            "------------------------------------------------\n" +
                            "制御基板　          　  1      10,000     配達\n" +
                            "操作スイッチ            1       3,800     配達\n" +
                            "パネル　　          　  1       2,000     配達\n" +
                            "技術料　          　　  1      15,000\n" +
                            "出張費用　　            1       5,000\n" +
                            "------------------------------------------------\n" +
                            "\n" +
                            "                            小計       \\ 35,800\n" +
                            "                            内税       \\  1,790\n" +
                            "                            合計       \\ 37,590\n" +
                            "\n" +
                            "　お問合わせ番号　　12345-67890\n" +
                            "\n").AsBuffer());

            builder.AppendAlignment(AlignmentPosition.Center);

            builder.AppendBarcodeData(Encoding.GetEncoding("ASCII").GetBytes("{BStar.").AsBuffer(), BarcodeSymbology.Code128, BarcodeWidth.Mode2, 40, true);

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
                encoding = "Shift_JIS";

                builder.AppendCodePage(CodePageType.CP932);
            }

            builder.AppendInternational(InternationalType.Japan);

            builder.AppendCharacterSpace(0);

            builder.AppendAlignment(AlignmentPosition.Center);

            builder.AppendEmphasis(true);

            //IBuffer otherData = System.Text.Encoding.UTF8.GetBytes("スター電機\n").AsBuffer();

            builder.AppendDataWithMultipleHeight(Encoding.GetEncoding(encoding).GetBytes("スター電機\n").AsBuffer(), 3);

            builder.AppendDataWithMultipleHeight(Encoding.GetEncoding(encoding).GetBytes("修理報告書　兼領収書\n").AsBuffer(), 2);

            builder.AppendEmphasis(false);

            builder.AppendAlignment(AlignmentPosition.Left);

            builder.AppendData(Encoding.GetEncoding(encoding).GetBytes(
                    "---------------------------------------------------------------------\n" +
                            "発行日時：YYYY年MM月DD日HH時MM分\n" +
                            "TEL：054-347-XXXX\n" +
                            "\n" +
                            "           ｲｹﾆｼ  ｼｽﾞｺ   ｻﾏ\n" +
                            "　お名前：池西　静子　様\n" +
                            "　御住所：静岡市清水区七ツ新屋\n" +
                            "　　　　　５３６番地\n" +
                            "　伝票番号：No.12345-67890\n" +
                            "\n" +
                            "この度は修理をご用命頂き有難うございます。\n" +
                            " 今後も故障など発生した場合はお気軽にご連絡ください。\n" +
                            "\n" +
                            "品名／型名　                 数量             金額　          備考\n" +
                            "---------------------------------------------------------------------\n" +
                            "制御基板　　                   1             10,000            配達\n" +
                            "操作スイッチ                   1              3,800            配達\n" +
                            "パネル　　　                   1              2,000            配達\n" +
                            "技術料　　　                   1             15,000\n" +
                            "出張費用　　                   1              5,000\n" +
                            "---------------------------------------------------------------------\n" +
                            "\n" +
                            "                                                 小計       \\ 35,800\n" +
                            "                                                 内税       \\  1,790\n" +
                            "                                                 合計       \\ 37,590\n" +
                            "\n" +
                            "　お問合わせ番号　　12345-67890\n" +
                            "\n" +
                            "\n").AsBuffer());

            builder.AppendAlignment(AlignmentPosition.Center);

            builder.AppendBarcodeData(Encoding.GetEncoding("ASCII").GetBytes("{BStar.").AsBuffer(), BarcodeSymbology.Code128, BarcodeWidth.Mode2, 40, true);

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
                encoding = "Shift_JIS";

                builder.AppendCodePage(CodePageType.CP932);
            }

            builder.AppendInternational(InternationalType.Japan);

            builder.AppendCharacterSpace(0);

            builder.AppendAlignment(AlignmentPosition.Center);

            builder.AppendEmphasis(true);

            builder.AppendDataWithMultipleHeight(Encoding.GetEncoding(encoding).GetBytes("スター電機\n").AsBuffer(), 3);

            builder.AppendDataWithMultipleHeight(Encoding.GetEncoding(encoding).GetBytes("修理報告書　兼領収書\n").AsBuffer(), 2);

            builder.AppendEmphasis(false);

            builder.AppendAlignment(AlignmentPosition.Left);

            builder.AppendData(Encoding.GetEncoding(encoding).GetBytes(
                    "------------------------------------------\n" +
                            "発行日時：YYYY年MM月DD日HH時MM分\n" +
                            "TEL：054-347-XXXX\n" +
                            "\n" +
                            "           ｲｹﾆｼ  ｼｽﾞｺ   ｻﾏ\n" +
                            "　お名前：池西　静子　様\n" +
                            "　御住所：静岡市清水区七ツ新屋\n" +
                            "　　　　　５３６番地\n" +
                            "　伝票番号：No.12345-67890\n" +
                            "\n" +
                            "　この度は修理をご用命頂き有難うございます。\n" +
                            " 今後も故障など発生した場合はお気軽にご連絡ください。\n" +
                            "\n" +
                            "品名／型名        数量      金額　   備考\n" +
                            "------------------------------------------\n" +
                            "制御基板　          1     10,000     配達\n" +
                            "操作スイッチ        1      3,800     配達\n" +
                            "パネル　　          1      2,000     配達\n" +
                            "技術料　            1     15,000\n" +
                            "出張費用　　        1      5,000\n" +
                            "------------------------------------------\n" +
                            "\n" +
                            "                      小計       \\ 35,800\n" +
                            "                      内税       \\  1,790\n" +
                            "                      合計       \\ 37,590\n" +
                            "\n" +
                            "　お問合わせ番号　　12345-67890\n" +
                            "\n").AsBuffer());

            builder.AppendAlignment(AlignmentPosition.Center);

            builder.AppendBarcodeData(Encoding.GetEncoding("ASCII").GetBytes("{BStar.").AsBuffer(), BarcodeSymbology.Code128, BarcodeWidth.Mode2, 40, true);

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
                encoding = "Shift_JIS";

                builder.AppendCodePage(CodePageType.CP932);
            }

            builder.AppendInternational(InternationalType.Japan);

            builder.AppendAlignment(AlignmentPosition.Center);

            builder.AppendEmphasis(true);

            builder.AppendDataWithMultipleHeight(Encoding.GetEncoding(encoding).GetBytes("スター電機\n修理報告書　兼領収書\n").AsBuffer(), 2);

            builder.AppendEmphasis(false);

            builder.AppendAlignment(AlignmentPosition.Left);

            builder.AppendData(Encoding.GetEncoding(encoding).GetBytes(
                    "------------------------------------------\n" +
                            "発行日時：YYYY年MM月DD日HH時MM分\n" +
                            "TEL：054-347-XXXX\n" +
                            "\n" +
                            "        ｲｹﾆｼ  ｼｽﾞｺ  ｻﾏ\n" +
                            "　お名前：池西  静子　様\n" +
                            "　御住所：静岡市清水区七ツ新屋\n" +
                            "　　　　　５３６番地\n" +
                            "　伝票番号：No.12345-67890\n" +
                            "\n" +
                            "　この度は修理をご用命頂き有難うございます。\n" +
                            " 今後も故障など発生した場合はお気軽にご連絡ください。\n" +
                            "\n" +
                            "品名／型名　     数量      金額　     備考\n" +
                            "------------------------------------------\n" +
                            "制御基板　　       1      10,000     配達\n" +
                            "操作スイッチ       1       3,800     配達\n" +
                            "パネル　　　       1       2,000     配達\n" +
                            "技術料　　　       1      15,000\n" +
                            "出張費用　　       1       5,000\n" +
                            "------------------------------------------\n" +
                            "\n" +
                            "                       小計       \\ 35,800\n" +
                            "                       内税       \\  1,790\n" +
                            "                       合計       \\ 37,590\n" +
                            "\n" +
                            "　お問合わせ番号　　12345-67890\n").AsBuffer());
        }

        public async override Task<BitmapDecoder> CreateCouponImageAsync()
        {
            BitmapDecoder logoData = null;

            StorageFile logoFile = await StorageFile.GetFileFromApplicationUriAsync(new Uri("ms-appx:///Assets/coupon_image_japanese.png"));
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
                "　　　　　　スター電機\n" +
                "　　　　修理報告書　兼領収書\n" +
                "----------------------------------------\n" +
                "発行日時：\n" +
                "         YYYY年MM月DD日HH時MM分\n" +
                "TEL：054-347-XXXX\n" +
                "\n" +
                "　　　　　ｲｹﾆｼ  ｼｽﾞｺ  ｻﾏ\n" +
                "　お名前  : 池西　静子　様\n" +
                "　御住所 : 静岡市清水区七ツ新屋\n" +
                "　　　　　５３６番地\n" +
                "　伝票番号：No.12345-67890\n" +
                "\n" +
                "　この度は修理をご用命頂き有難うござい\n" +
                "　ます。\n" +
                " 今後も故障など発生した場合はお気軽に\n" +
                " ご連絡ください。\n" +
                "\n" +
                "品名／型名\t\t数量\t\t金額\n" +
                "----------------------------------------\n" +
                "制御基板\t\t1\t\t10,000\n" +
                "操作スイッチ\t\t1\t\t  3,000\n" +
                "パネル\t\t\t1\t\t  2,000\n" +
                "技術料\t\t1\t\t15,000\n" +
                "出張費用\t\t1\t\t  5,000\n" +
                "----------------------------------------\n" +
                "\n" +
                "　　　　　　\t    小計　¥  35,800\n" +
                "　　　　　　\t    内税　¥    1,790\n" +
                "　　　　　　\t    合計　¥  37,590\n" +
                "\n" +
                "　お問合わせ番号　　12345-67890\n";

            return textToPrint;
        }

        public override string Create3inchRasterReceiptText()
        {
            String textToPrint =
                "                        スター電機\n" +
                "               修理報告書  兼領収書\n" +
                "------------------------------------------\n" +
                "発行日時 : \n" +
                "        YYYY年MM月DD日HH時MM分\n" +
                "TEL :054-347-XXXX\n" +
                "\n" +
                "　　　　　ｲｹﾆｼ  ｼｽﾞｺ  ｻﾏ\n" +
                "　お名前：池西　静子　様\n" +
                "　御住所：静岡市清水区七ツ新屋\n" +
                "　　　　　５３６番地\n" +
                "　伝票番号：No.12345-67890\n" +
                "\n" +
                "　この度は修理をご用命頂き有難うござい\n" +
                "　ます。\n" +
                " 今後も故障など発生した場合はお気軽に\n" +
                " ご連絡ください。\n" +
                "\n" +
                "品名／型名\t数量\t金額\t   備考\n" +
                "------------------------------------------\n" +
                "制御基板\t\t1\t10,000\t配達\n" +
                "操作スイッチ\t1\t  3,800\t配達\n" +
                "パネル\t\t1\t  2,000\t配達\n" +
                "技術料\t\t1\t15,000\n" +
                "出張費用\t\t1\t  5,000\n" +
                "------------------------------------------\n" +
                "\n" +
                "           \t\t\t小計　¥ 35,800\n" +
                "           \t\t\t内税　¥   1,790\n" +
                "           \t\t\t合計　¥ 37,590\n" +
                "\n" +
                "　お問合わせ番号　　12345-67890\n";

            return textToPrint;
        }

        public override string Create4inchRasterReceiptText()
        {
            String textToPrint =
                "                                                スター電機\n" +
                "                                        修理報告書　兼領収書\n" +
                "---------------------------------------------------------------------------\n" +
                "発行日時：YYYY年MM月DD日HH時MM分\n" +
                "TEL：054-347-XXXX\n" +
                "\n" +
                "\t\t          　　　　　ｲｹﾆｼ  ｼｽﾞｺ  ｻﾏ\n" +
                "\t\t          　お名前：池西　静子　様\n" +
                "\t\t          　御住所：静岡市清水区七ツ新屋\n" +
                "\t\t          　　　　　５３６番地\n" +
                "\t\t          　伝票番号：No.12345-67890\n" +
                "\n" +
                "\t\t         　この度は修理をご用命頂き有難うございます。\n" +
                "\t\t       今後も故障など発生した場合はお気軽にご連絡ください。\n" +
                "\n" +
                "品名／型名\t\t数量\t\t\t金額\t\t備考\n" +
                "---------------------------------------------------------------------------\n" +
                "制御基板\t\t\t1\t\t\t10,000\t配達\n" +
                "操作スイッチ\t\t1\t\t\t  3,800\t配達\n" +
                "パネル\t\t\t1\t\t\t  2,000\t配達\n" +
                "技術料\t\t\t1\t\t\t15,000\n" +
                "出張費用\t\t\t1\t\t\t  5,000\n" +
                "---------------------------------------------------------------------------\n" +
                "\n" +
                "　　　　　　　　　　　　　　　　　　　　　　小計　¥ 35,800\n" +
                "　　　　　　　　　　　　　　　　　　　　　　内税　¥   1,790\n" +
                "　　　　　　　　　　　　　　　　　　　　　　合計　¥ 37,590\n" +
                "\n" +
                "　お問合わせ番号　　12345-67890\n";

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
                encoding = "Shift_JIS";

                builder.AppendCodePage(CodePageType.CP932);

            }

            builder.AppendInternational(InternationalType.Japan);

            builder.AppendCharacterSpace(0);

            builder.AppendUnitFeed(20 * 2);

            builder.AppendMultipleHeight(2);

            builder.AppendData(Encoding.GetEncoding(encoding).GetBytes("〒422-8654").AsBuffer());

            builder.AppendUnitFeed(64);

            builder.AppendData(Encoding.GetEncoding(encoding).GetBytes("静岡県静岡市駿河区中吉田20番10号").AsBuffer());

            builder.AppendUnitFeed(64);

            builder.AppendData(Encoding.GetEncoding(encoding).GetBytes("スター精密株式会社").AsBuffer());

            builder.AppendUnitFeed(64);

            builder.AppendMultipleHeight(1);
        }

        public override string CreatePasteTextLabelString()
        {
            return "〒422-8654\n" +
                   "静岡県静岡市駿河区中吉田20番10号\n" +
                   "スター精密株式会社";
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
                encoding = "Shift_JIS";

                builder.AppendCodePage(CodePageType.CP932);

            }

            builder.AppendInternational(InternationalType.Japan);

            builder.AppendCharacterSpace(0);

            builder.AppendData(Encoding.GetEncoding(encoding).GetBytes(pasteText).AsBuffer());

        }
    }
}
