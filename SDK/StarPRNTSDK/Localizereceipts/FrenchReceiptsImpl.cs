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
    class FrenchReceiptsImpl : ILocalizeReceipts
    {

        public FrenchReceiptsImpl()
        {
            LanguageCode = "Fr";

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

            builder.AppendInternational(InternationalType.France);

            builder.AppendCharacterSpace(0);

            builder.AppendAlignment(AlignmentPosition.Center);

            builder.AppendDataWithMultipleHeight(Encoding.GetEncoding(encoding).GetBytes(
                                   "Star Micronics Communications\n").AsBuffer(), 2);

            builder.AppendData(Encoding.GetEncoding(encoding).GetBytes(
                "AVENUE LA MOTTE PICQUET\n" +
                        "\n").AsBuffer());

            builder.AppendAlignment(AlignmentPosition.Left);

            builder.AppendData(Encoding.GetEncoding(encoding).GetBytes(
                "--------------------------------\n" +
                        "Date   : MM/DD/YYYY\n" +
                        "Heure  : HH:MM\n" +
                        "Boutique: OLUA23    Caisse: 0001\n" +
                        "Conseiller: 002970  Ticket: 3881\n" +
                        "--------------------------------\n" +
                        "\n" +
                        "Vous avez été servi par : Souad\n" +
                        "\n" +
                        "CAC IPHONE\n" +
                        "3700615033581 1 X 19.99€  19.99€\n" +
                        "\n" +
                        "dont contribution\n" +
                        " environnementale :\n" +
                        "CAC IPHONE                 0.01€\n" +
                        "--------------------------------\n" +
                        "1 Piéce(s) Total :        19.99€\n" +
                        "Mastercard Visa  :        19.99€\n" +
                        "\n" +
                        "Taux TVA    Montant H.T.   T.V.A\n" +
                        "  20%          16.66€      3.33€\n" +
                        "\n").AsBuffer());


            builder.AppendAlignment(AlignmentPosition.Center);

            builder.AppendData(Encoding.GetEncoding(encoding).GetBytes(
                "Merci de votre visite et.\n" +
                        "à bientôt.\n" +
                        "Conservez votre ticket il\n" +
                        "vous sera demandé pour\n" +
                        "tout échange.\n" +
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

            builder.AppendInternational(InternationalType.France);

            builder.AppendCharacterSpace(0);

            builder.AppendAlignment(AlignmentPosition.Center);

            builder.AppendDataWithMultipleHeight(Encoding.GetEncoding(encoding).GetBytes(
                                   "Star Micronics Communications\n").AsBuffer(), 2);

            builder.AppendData(Encoding.GetEncoding(encoding).GetBytes(
                "AVENUE LA MOTTE PICQUET\n" +
                        "\n").AsBuffer());

            builder.AppendAlignment(AlignmentPosition.Left);
        
            builder.AppendData(Encoding.GetEncoding(encoding).GetBytes(
                "------------------------------------------------\n" +
                        "Date: MM/DD/YYYY    Heure: HH:MM\n" +
                        "Boutique: OLUA23    Caisse: 0001\n" +
                        "Conseiller: 002970  Ticket: 3881\n" +
                        "------------------------------------------------\n" +
                        "\n" +
                        "Vous avez été servi par : Souad\n" +
                        "\n" +
                        "CAC IPHONE\n" +
                        "3700615033581   1    X     19.99€         19.99€\n" +
                        "\n" +
                        "dont contribution environnementale :\n" +
                        "CAC IPHONE                                 0.01€\n" +
                        "------------------------------------------------\n" +
                        "1 Piéce(s) Total :                        19.99€\n" +
                        "Mastercard Visa  :                        19.99€\n" +
                        "\n" +
                        "Taux TVA    Montant H.T.   T.V.A\n" +
                        "  20%          16.66€      3.33€\n" +
                        "\n").AsBuffer());

            builder.AppendAlignment(AlignmentPosition.Center);
        
       
            builder.AppendData(Encoding.GetEncoding(encoding).GetBytes(
                "Merci de votre visite et. à bientôt.\n" +
                        "Conservez votre ticket il\n" +
                        "vous sera demandé pour tout échange.\n").AsBuffer());

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

            builder.AppendInternational(InternationalType.France);

            builder.AppendCharacterSpace(0);

            builder.AppendAlignment(AlignmentPosition.Center);

            builder.AppendDataWithMultipleHeight(Encoding.GetEncoding(encoding).GetBytes(
                                   "Star Micronics Communications\n").AsBuffer(), 2);

            builder.AppendData(Encoding.GetEncoding(encoding).GetBytes(
                    "AVENUE LA MOTTE PICQUET\n" +
                            "\n").AsBuffer());

            builder.AppendAlignment(AlignmentPosition.Left);

            builder.AppendData(Encoding.GetEncoding(encoding).GetBytes(
                    "---------------------------------------------------------------------\n" +
                            "Date: MM/DD/YYYY    Heure: HH:MM\n" +
                            "Boutique: OLUA23    Caisse: 0001\n" +
                            "Conseiller: 002970  Ticket: 3881\n" +
                            "---------------------------------------------------------------------\n" +
                            "\n" +
                            "Vous avez été servi par : Souad\n" +
                            "\n" +
                            "CAC IPHONE\n" +
                            "3700615033581   1    X     19.99€                              19.99€\n" +
                            "\n" +
                            "dont contribution environnementale :\n" +
                            "CAC IPHONE                                                      0.01€\n" +
                            "---------------------------------------------------------------------\n" +
                            "1 Piéce(s) Total :                                             19.99€\n" +
                            "Mastercard Visa  :                                             19.99€\n" +
                            "\n" +
                            "Taux TVA    Montant H.T.   T.V.A\n" +
                            "  20%          16.66€      3.33€\n" +
                            "\n").AsBuffer());

            builder.AppendAlignment(AlignmentPosition.Center);

            builder.AppendData(Encoding.GetEncoding(encoding).GetBytes(
                    "Merci de votre visite et. à bientôt.\n" +
                            "Conservez votre ticket il\n" +
                            "vous sera demandé pour tout échange.\n").AsBuffer());

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

            builder.AppendInternational(InternationalType.France);

            builder.AppendCharacterSpace(0);

            builder.AppendAlignment(AlignmentPosition.Center);
       
            builder.AppendData(Encoding.GetEncoding(encoding).GetBytes("\n").AsBuffer());

            builder.AppendDataWithMultipleHeight(Encoding.GetEncoding(encoding).GetBytes(
                                   "Star Micronics Communications\n").AsBuffer(), 2);

            builder.AppendData(Encoding.GetEncoding(encoding).GetBytes(
                "AVENUE LA MOTTE PICQUET\n" +
                        "\n").AsBuffer());

            builder.AppendAlignment(AlignmentPosition.Left);
        
            builder.AppendData(Encoding.GetEncoding(encoding).GetBytes(
                "------------------------------------------\n" +
                        "Date: MM/DD/YYYY    Heure: HH:MM\n" +
                        "Boutique: OLUA23    Caisse: 0001\n" +
                        "Conseiller: 002970  Ticket: 3881\n" +
                        "------------------------------------------\n" +
                        "\n" +
                        "Vous avez été servi par : Souad\n" +
                        "\n" +
                        "CAC IPHONE\n" +
                        "3700615033581   1    X   19.99€     19.99€\n" +
                        "\n" +
                        "dont contribution environnementale :\n" +
                        "CAC IPHONE                           0.01€\n" +
                        "------------------------------------------\n" +
                        "1 Piéce(s) Total :                  19.99€\n" +
                        "Mastercard Visa  :                  19.99€\n" +
                        "\n" +
                        "Taux TVA    Montant H.T.   T.V.A\n" +
                        "  20%          16.66€      3.33€\n" +
                        "\n").AsBuffer());

        
            builder.AppendAlignment(AlignmentPosition.Center);

        
            builder.AppendData(Encoding.GetEncoding(encoding).GetBytes(
                "Merci de votre visite et. à bientôt.\n" +
                        "Conservez votre ticket il\n" +
                        "vous sera demandé pour tout échange.\n").AsBuffer());

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

            builder.AppendInternational(InternationalType.France);

            builder.AppendAlignment(AlignmentPosition.Center);

            builder.AppendDataWithMultipleHeight(Encoding.GetEncoding(encoding).GetBytes(
                                   "Star Micronics Communications\n").AsBuffer(), 2);

            builder.AppendData(Encoding.GetEncoding(encoding).GetBytes(
                    "AVENUE LA MOTTE PICQUET\n" +
                            "\n").AsBuffer());

            builder.AppendAlignment(AlignmentPosition.Left);

            builder.AppendData(Encoding.GetEncoding(encoding).GetBytes(
                    "------------------------------------------\n" +
                            "Date: MM/DD/YYYY    Heure: HH:MM\n" +
                            "Boutique: OLUA23    Caisse: 0001\n" +
                            "Conseiller: 002970  Ticket: 3881\n" +
                            "------------------------------------------\n" +
                            "\n" +
                            "Vous avez été servi par : Souad\n" +
                            "\n" +
                            "CAC IPHONE\n" +
                            "3700615033581 1 X 19.99€            19.99€\n" +
                            "\n" +
                            "dont contribution environnementale :\n" +
                            "CAC IPHONE                           0.01€\n" +
                            "------------------------------------------\n" +
                            "1 Piéce(s) Total :                  19.99€\n" +
                            "Mastercard Visa  :                  19.99€\n" +
                            "\n" +
                            "Taux TVA    Montant H.T.   T.V.A\n" +
                            "  20%          16.66€      3.33€\n" +
                            "\n").AsBuffer());

            builder.AppendAlignment(AlignmentPosition.Center);

            builder.AppendData(Encoding.GetEncoding(encoding).GetBytes(
                    "Merci de votre visite et. à bientôt.\n" +
                            "Conservez votre ticket il\n" +
                            "vous sera demandé pour tout échange.\n").AsBuffer());


        }

        public override async Task<BitmapDecoder> CreateCouponImageAsync()
        {
            BitmapDecoder logoData = null;

            StorageFile logoFile = await StorageFile.GetFileFromApplicationUriAsync(new Uri("ms-appx:///Assets/coupon_image_french.png"));
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
                "                 Star Micronics\n" +
                "                 Communications\n"+
                "             AVENUE LA MOTTE\n" +
                "           PICQUET City, State 12345\n" +
                "\n" +
                "----------------------------------------\n" +
                "Date\t\t: MM/DD/YYYY\n" +
                "Time\t\t: HH:MM PM\n" +
                "Boutique\t: OLUA23\n" +
                "Caisse\t: 0001\n" +
                "Conseiller\t: 002970\n" +
                "Ticket\t: 3881\n" +
                "----------------------------------------\n" +
                "Vous avez été servi par :\n" +
                "\t\t\t\tSouad\n" +
                "CAC IPHONE\n" +
                "3700615033581 1 X\t19.99€\n" +
                "\t\t\t\t19.99€\n" +
                "dont contribution\n" +
                " environnementale :\n" +
                "CAC IPHONE\t\t  0.01€\n" +
                "----------------------------------------\n" +
                " 1 Piéce(s) Total :\t\t19.99€\n" +
                "\n" +
                "  Mastercard Visa :\t\t19.99€\n" +
                "Taux TVA Montant H.T.\n" +
                "     20%           16.66€\n" +
                "T.V.A\n" +
                "3.33€\n" +
                "Merci de votre visite et.\n" +
                "à bientôt.\n" +
                "Conservez votre ticket il\n" +
                "vous sera demandé pour\n" +
                "tout échange.\n";

            return textToPrint;
        }

        public override string Create3inchRasterReceiptText()
        {
            String textToPrint =
                "                 Star Micronics\n" +
                "                 Communications\n" +
                "                 AVENUE LA MOTTE\n" +
                "             PICQUET City, State 12345\n" +
                "\n" +
                "------------------------------------------\n" +
                "Date: MM/DD/YYYY Time:HH:MM PM\n" +
                "Boutique\t: OLUA23 Caisse\t: 0001\n" +
                "Conseiller\t: 002970  Ticket\t: 3881\n" +
                "------------------------------------------\n" +
                "Vous avez été servi par : Souad\n" +
                "CAC IPHONE\n" +
                "3700615033581   1 X 19.99€    19.99€\n" +
                "dont contribution environnementale :\n" +
                "CAC IPHONE\t\t                0.01€\n" +
                "------------------------------------------\n" +
                "\t  1 Piéce(s)    Total :\t     19.99€\n" +
                "\n" +
                "\t     Mastercard Visa  :\t     19.99€\n" +
                "\tTaux TVA  Montant H.T. T.V.A\n" +
                "\t          20%    16.66€\t       3.33€\n" +
                "Merci de votre visite et. à bientôt.\n" +
                "Conservez votre ticket il vous sera\n" +
                "demandé pour tout échange.\n";

            return textToPrint;
        }

        public override string Create4inchRasterReceiptText()
        {
            String textToPrint =
                "                                         Star Micronics\n" +
                "                                         Communications\n" +
                "                  AVENUE LA MOTTE PICQUET City, State 12345\n" +
                "\n" +
                "---------------------------------------------------------------------------\n" +
                "                      Date\t : MM/DD/YYYY\tTime\t : HH:MM PM\n" +
                "                  Boutique\t : OLUA23\t\tCaisse\t : 0001\n" +
                "                Conseiller\t : 002970\t\tTicket\t : 3881\n" +
                "---------------------------------------------------------------------------\n" +
                "Vous avez été servi par : Souad\n" +
                "CAC IPHONE\n" +
                "3700615033581      \t\t1  X  19.99€                  \t       19.99€\n" +
                "dont contribution environnementale :\n" +
                "CAC IPHONE                                        \t\t\t\t         0.01€\n" +
                "---------------------------------------------------------------------------\n" +
                "\t\t\t\t\t        1 Piéce(s)    Total :\t       19.99€\n" +
                "\n" +
                "\t\t\t\t\t        Mastercard Visa  :\t       19.99€\n" +
                "\t\t\t\t                        Taux TVA  Montant H.T. T.V.A\n" +
                "\t\t\t\t                              20%         16.66€\t3.33€\n" +
                "\t          Merci de votre visite et. à bientôt.\n" +
                "\t          Conservez votre ticket il vous sera demandé pour\n" +
                "\t          tout échange.\n";

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
