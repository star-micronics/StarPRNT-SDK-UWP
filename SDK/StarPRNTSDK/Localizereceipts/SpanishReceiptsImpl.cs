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
    class SpanishReceiptsImpl : ILocalizeReceipts
    {
        public SpanishReceiptsImpl()
        {
            LanguageCode = "Es";

            CharacterCode = CharacterCode.Standard;
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
                encoding = "Windows-1252";

                builder.AppendCodePage(CodePageType.CP1252);
            }

            builder.AppendInternational(InternationalType.Spain);

            builder.AppendCharacterSpace(0);

            builder.AppendAlignment(AlignmentPosition.Center);

            builder.AppendDataWithMultiple(Encoding.GetEncoding(encoding).GetBytes(
                                    "BAR RESTAURANT\n" +
                    "EL POZO\n").AsBuffer(), 2, 2);

            builder.AppendData(Encoding.GetEncoding(encoding).GetBytes(
                    "C/.ROCAFORT 187\n" +
                            "08029 BARCELONA\n" +
                            "\n" +
                            "NIF :X-3856907Z\n" +
                            "TEL :934199465\n").AsBuffer());

            builder.AppendAlignment(AlignmentPosition.Left);

            builder.AppendData(Encoding.GetEncoding(encoding).GetBytes(
                    "--------------------------------\n" +
                            "MESA: 100 P: - FECHA: YYYY-MM-DD\n" +
                            "CAN P/U DESCRIPCION  SUMA\n" +
                            "--------------------------------\n" +
                            " 4  3,00  JARRA  CERVEZA   12,00\n" +
                            " 1  1,60  COPA DE CERVEZA   1,60\n" +
                            "--------------------------------\n" +
                            "               SUB TOTAL : 13,60\n").AsBuffer());

            builder.AppendAlignment(AlignmentPosition.Right);

            builder.AppendDataWithMultipleHeight(Encoding.GetEncoding(encoding).GetBytes(
                                         "TOTAL:     13,60 EUROS\n").AsBuffer(), 2);

            builder.AppendAlignment(AlignmentPosition.Left);

            builder.AppendData(Encoding.GetEncoding(encoding).GetBytes(
                    "NO: 000018851     IVA INCLUIDO\n" +
                            "--------------------------------\n").AsBuffer());

            builder.AppendAlignment(AlignmentPosition.Center);

            builder.AppendData(Encoding.GetEncoding(encoding).GetBytes(
                            "**** GRACIAS POR SU VISITA! ****\n" +
                    "\n").AsBuffer());

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
                encoding = "Windows-1252";

                builder.AppendCodePage(CodePageType.CP1252);
            }

            builder.AppendInternational(InternationalType.Spain);

            builder.AppendCharacterSpace(0);

            builder.AppendAlignment(AlignmentPosition.Center);

            builder.AppendDataWithMultiple(Encoding.GetEncoding(encoding).GetBytes(
                                   "BAR RESTAURANT EL POZO\n").AsBuffer(), 2, 2);

            builder.AppendData(Encoding.GetEncoding(encoding).GetBytes(
                    "C/.ROCAFORT 187 08029 BARCELONA\n" +
                            "NIF :X-3856907Z  TEL :934199465\n").AsBuffer());

            builder.AppendAlignment(AlignmentPosition.Left);

            builder.AppendData(Encoding.GetEncoding(encoding).GetBytes(
                    "------------------------------------------------\n" +
                            "MESA: 100 P: - FECHA: YYYY-MM-DD\n" +
                            "CAN P/U DESCRIPCION  SUMA\n" +
                            "------------------------------------------------\n" +
                            " 4     3,00      JARRA  CERVEZA            12,00\n" +
                            " 1     1,60      COPA DE CERVEZA            1,60\n" +
                            "------------------------------------------------\n" +
                            "                           SUB TOTAL :     13,60\n").AsBuffer());

            builder.AppendAlignment(AlignmentPosition.Right);

            builder.AppendDataWithMultipleHeight(Encoding.GetEncoding(encoding).GetBytes(
                                         "TOTAL:     13,60 EUROS\n").AsBuffer(), 2);

            builder.AppendAlignment(AlignmentPosition.Left);

            builder.AppendData(Encoding.GetEncoding(encoding).GetBytes(
                    "NO: 000018851  IVA INCLUIDO\n" +
                            "------------------------------------------------\n").AsBuffer());

            builder.AppendAlignment(AlignmentPosition.Center);

            builder.AppendData(Encoding.GetEncoding(encoding).GetBytes(
                            "**** GRACIAS POR SU VISITA! ****\n" +
                    "\n").AsBuffer());

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
                encoding = "Windows-1252";

                builder.AppendCodePage(CodePageType.CP1252);
            }

            builder.AppendInternational(InternationalType.Spain);

            builder.AppendCharacterSpace(0);

            builder.AppendAlignment(AlignmentPosition.Center);

            builder.AppendDataWithMultiple(Encoding.GetEncoding(encoding).GetBytes(
                                   "BAR RESTAURANT EL POZO\n").AsBuffer(), 2, 2);

            builder.AppendData(Encoding.GetEncoding(encoding).GetBytes(
                    "C/.ROCAFORT 187 08029 BARCELONA\n" +
                            "NIF :X-3856907Z  TEL :934199465\n").AsBuffer());

            builder.AppendAlignment(AlignmentPosition.Left);

            builder.AppendData(Encoding.GetEncoding(encoding).GetBytes(
                    "---------------------------------------------------------------------\n" +
                            "MESA: 100 P: - FECHA: YYYY-MM-DD\n" +
                            "CAN P/U DESCRIPCION  SUMA\n" +
                            "---------------------------------------------------------------------\n" +
                            " 4     3,00          JARRA  CERVEZA                             12,00\n" +
                            " 1     1,60          COPA DE CERVEZA                             1,60\n" +
                            "---------------------------------------------------------------------\n" +
                            "                                         SUB TOTAL :            13,60\n").AsBuffer());

            builder.AppendAlignment(AlignmentPosition.Right);

            builder.AppendDataWithMultipleHeight(Encoding.GetEncoding(encoding).GetBytes(
                                         "TOTAL:     13,60 EUROS\n").AsBuffer(), 2);

            builder.AppendAlignment(AlignmentPosition.Left);

            builder.AppendData(Encoding.GetEncoding(encoding).GetBytes(
                    "NO: 000018851  IVA INCLUIDO\n" +
                            "---------------------------------------------------------------------\n").AsBuffer());

            builder.AppendAlignment(AlignmentPosition.Center);

            builder.AppendData(Encoding.GetEncoding(encoding).GetBytes(
                            "**** GRACIAS POR SU VISITA! ****\n" +
                    "\n").AsBuffer());

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
                encoding = "Windows-1252";

                builder.AppendCodePage(CodePageType.CP1252);
            }

            builder.AppendInternational(InternationalType.Spain);

            builder.AppendCharacterSpace(0);

            builder.AppendAlignment(AlignmentPosition.Center);

            builder.AppendData(Encoding.GetEncoding(encoding).GetBytes(
                           "\n").AsBuffer());

            builder.AppendDataWithMultipleHeight(Encoding.GetEncoding(encoding).GetBytes(
                                         "BAR RESTAURANT EL POZO\n").AsBuffer(), 2);

            builder.AppendData(Encoding.GetEncoding(encoding).GetBytes(
                    "C/.ROCAFORT 187 08029 BARCELONA\n" +
                            "NIF :X-3856907Z  TEL :934199465\n").AsBuffer());

            builder.AppendAlignment(AlignmentPosition.Left);

            builder.AppendData(Encoding.GetEncoding(encoding).GetBytes(
                    "------------------------------------------\n" +
                            "MESA: 100 P: - FECHA: YYYY-MM-DD\n" +
                            "CAN P/U DESCRIPCION  SUMA\n" +
                            "------------------------------------------\n" +
                            " 4    3,00    JARRA  CERVEZA         12,00\n" +
                            " 1    1,60    COPA DE CERVEZA         1,60\n" +
                            "------------------------------------------\n" +
                            "                     SUB TOTAL :     13,60\n").AsBuffer());

            builder.AppendAlignment(AlignmentPosition.Right);

            builder.AppendDataWithMultipleHeight(Encoding.GetEncoding(encoding).GetBytes(
                                         "TOTAL:     13,60 EUROS\n").AsBuffer(), 2);

            builder.AppendAlignment(AlignmentPosition.Left);

            builder.AppendData(Encoding.GetEncoding(encoding).GetBytes(
                    "NO: 000018851  IVA INCLUIDO\n" +
                            "------------------------------------------\n").AsBuffer());

            builder.AppendAlignment(AlignmentPosition.Center);

            builder.AppendData(Encoding.GetEncoding(encoding).GetBytes(
                            "**** GRACIAS POR SU VISITA! ****\n" +
                    "\n").AsBuffer());

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
                encoding = "Windows-1252";

                builder.AppendCodePage(CodePageType.CP1252);
            }

            builder.AppendInternational(InternationalType.Spain);

            builder.AppendAlignment(AlignmentPosition.Center);

            builder.AppendDataWithMultipleHeight(Encoding.GetEncoding(encoding).GetBytes(
                                         "BAR RESTAURANT EL POZO\n").AsBuffer(), 2);

            builder.AppendData(Encoding.GetEncoding(encoding).GetBytes(
                    "C/.ROCAFORT 187 08029 BARCELONA\n" +
                            "NIF :X-3856907Z  TEL :934199465\n").AsBuffer());

            builder.AppendAlignment(AlignmentPosition.Left);

            builder.AppendData(Encoding.GetEncoding(encoding).GetBytes(
                    "------------------------------------------\n" +
                            "MESA: 100 P: - FECHA: YYYY-MM-DD\n" +
                            "CAN P/U DESCRIPCION  SUMA\n" +
                            "------------------------------------------\n" +
                            " 4 3,00 JARRA  CERVEZA               12,00\n" +
                            " 1 1,60 COPA DE CERVEZA               1,60\n" +
                            "------------------------------------------\n" +
                            " SUB TOTAL :                         13,60\n" +
                            "                     TOTAL:    13,60 EUROS\n" +
                            "NO: 000018851  IVA INCLUIDO\n" +
                            "------------------------------------------\n").AsBuffer());

            builder.AppendAlignment(AlignmentPosition.Center);

            builder.AppendData(Encoding.GetEncoding(encoding).GetBytes(
                           "**** GRACIAS POR SU VISITA! ****\n").AsBuffer());
        }

        public override async Task<BitmapDecoder> CreateCouponImageAsync()
        {
            BitmapDecoder logoData = null;

            StorageFile logoFile = await StorageFile.GetFileFromApplicationUriAsync(new Uri("ms-appx:///Assets/coupon_image_spanish.png"));
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
                "               BAR RESTAURANT\n" +
                "                                           EL POZO\n" +
                "C/.ROCAFORT 187\n" +
                "08029 BARCELONA\n" +
                "NIF :X-3856907Z\n" +
                "TEL :934199465\n" +
                "----------------------------------------\n" +
                "MESA: 100 P: -\n" +
                "    FECHA: YYYY-MM-DD\n" +
                "CAN P/U DESCRIPCION  SUMA\n" +
                "----------------------------------------\n" +
                "3,00 \tJARRA CERVEZA\t\t12,00\n" +
                "1,60 \tCOPA DE CERVEZA\t  1,60\n" +
                "----------------------------------------\n" +
                "                  SUB TOTAL :\t13,60\n" +
                "TOTAL:                     13,60 \tEUROS\n" +
                " NO:000018851 IVA INCLUIDO\n" +
                "\n" +
                "----------------------------------------\n" +
                "     **GRACIAS POR SU VISITA!**\n";

            return textToPrint;
        }

        public override string Create3inchRasterReceiptText()
        {
            String textToPrint =
                "                BAR RESTAURANT\n" +
                "\t                               EL POZO\n" +
                "C/.ROCAFORT 187\n" +
                "08029 BARCELONA\n" +
                "NIF :X-3856907Z\n" +
                "TEL :934199465\n" +
                "------------------------------------------\n" +
                "MESA: 100 P: - FECHA: YYYY-MM-DD\n" +
                "CAN P/U DESCRIPCION  SUMA\n" +
                "------------------------------------------\n" +
                "4 3,00 JARRA  CERVEZA   \t\t12,00\n" +
                "1 1,60 COPA DE CERVEZA  \t\t  1,60\n" +
                "------------------------------------------\n" +
                "                     SUB TOTAL : \t\t13,60\n" +
                "TOTAL:               \t13,60\t\tEUROS\n" +
                "NO: 000018851 IVA INCLUIDO\n" +
                "\n" +
                "------------------------------------------\n" +
                "      **GRACIAS POR SU VISITA!**\n";

            return textToPrint;
        }

        public override string Create4inchRasterReceiptText()
        {
            String textToPrint =
                "                                   BAR RESTAURANT EL POZO\n" +
                "                          C/.ROCAFORT 187 08029 BARCELONA\n" +
                "                          NIF :X-3856907Z  TEL :934199465\n" +
                "---------------------------------------------------------------------------\n" +
                "MESA: 100 P: - FECHA: YYYY-MM-DD\n" +
                "CAN P/U DESCRIPCION  SUMA\n" +
                "---------------------------------------------------------------------------\n" +
                "4\t\t    3,00\t\t    JARRA  CERVEZA\t                  12,00\n" +
                "1\t\t    1,60\t\t    COPA DE CERVEZA\t                    1,60\n" +
                "---------------------------------------------------------------------------\n" +
                "\t\t\t\t                                  SUB TOTAL :       \t13,60\n" +
                "\t\t\t\t                                  TOTAL :      \t13,60\tEUROS\n" +
                "NO: 000018851 IVA INCLUIDO\n" +
                "\n" +
                "---------------------------------------------------------------------------\n" +
                "                             ***GRACIAS POR SU VISITA!***\n";

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
