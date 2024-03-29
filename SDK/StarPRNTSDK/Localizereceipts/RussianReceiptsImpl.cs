﻿using StarIO_Extension;
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
    class RussianReceiptsImpl : ILocalizeReceipts
    {
        public RussianReceiptsImpl()
        {
            LanguageCode = "Ru";

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
                encoding = "Windows-1251";

                builder.AppendCodePage(CodePageType.CP1251);
            }

            builder.AppendCharacterSpace(0);

            builder.AppendAlignment(AlignmentPosition.Center);

            builder.AppendDataWithMultiple(Encoding.GetEncoding(encoding).GetBytes(
                                   "Р Е Л А К С\n").AsBuffer(), 2, 2);

            builder.AppendData(Encoding.GetEncoding(encoding).GetBytes(
                            "ООО “РЕЛАКС”\n" +
                    "СПб., Малая Балканская, д. 38, лит. А\n" +
                    "тел. 307-07-12\n").AsBuffer());

            builder.AppendAlignment(AlignmentPosition.Left);


            builder.AppendData(Encoding.GetEncoding(encoding).GetBytes(
                            "РЕГ №322736     ИНН:123321\n" +
                    "01 Белякова И.А.КАССА: 0020 ОТД.01\n").AsBuffer());

            builder.AppendDataWithAlignment(Encoding.GetEncoding(encoding).GetBytes(
                   "ЧЕК НА ПРОДАЖУ  No 84373\n").AsBuffer(), AlignmentPosition.Center);

            builder.AppendData(Encoding.GetEncoding(encoding).GetBytes(
                    "--------------------------------\n" +
                            " 1. Яблоки Айдаред, кг    144.50\n" +
                            " 2. Соус соевый Sen So     36.40\n" +
                            " 3. Соус томатный Клас     19.90\n" +
                            " 4. Ребра свиные в.к м     78.20\n" +
                            " 5. Масло подсол раф д    114.00\n" +
                            " 6. Блокнот 10х14см сп    164.00\n" +
                            " 7. Морс Северная Ягод     99.90\n" +
                            " 8. Активия Биойогурт      43.40\n" +
                            " 9. Бублики Украинские     26.90\n" +
                            "10. Активия Биойогурт      43.40\n" +
                            "11. Сахар-песок 1кг        58.40\n" +
                            "12. Хлопья овсяные Ясн     38.40\n" +
                            "13. Кинза 50г              39.90\n" +
                            "14. Пемза “Сердечко” .Т    37.90\n" +
                            "15. Приправа Santa Mar     47.90\n" +
                            "16. Томаты слива Выбор    162.00\n" +
                            "17. Бонд Стрит Ред Сел     56.90\n" +
                            "--------------------------------\n" +
                            "--------------------------------\n" +
                            "ДИСКОНТНАЯ КАРТА\n" +
                            "                No:2440012489765\n" +
                            "--------------------------------\n").AsBuffer());

            builder.AppendAlignment(AlignmentPosition.Right);

            builder.AppendData(Encoding.GetEncoding(encoding).GetBytes(
                    "ИТОГО К ОПЛАТЕ = 1212.00\n" +
                            "НАЛИЧНЫЕ = 1212.00\n" +
                            "ВАША СКИДКА : 0.41\n" +
                            "\n").AsBuffer());

            builder.AppendDataWithAlignment(Encoding.GetEncoding(encoding).GetBytes(
                    "ЦЕНЫ УКАЗАНЫ С УЧЕТОМ СКИДКИ\n" +
                            "\n").AsBuffer(), AlignmentPosition.Center);

            builder.AppendData(Encoding.GetEncoding(encoding).GetBytes(
                    "08-02-2015 09:49  0254.0130604\n" +
                            "00083213 #060127\n").AsBuffer());

            builder.AppendAlignment(AlignmentPosition.Center);

            builder.AppendData(Encoding.GetEncoding(encoding).GetBytes(
                    "СПАСИБО ЗА ПОКУПКУ !\n" +
                            "МЫ  ОТКРЫТЫ ЕЖЕДНЕВНО С 9 ДО 23\n" +
                            "СОХРАНЯЙТЕ, ПОЖАЛУЙСТА , ЧЕК\n" +
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
                encoding = "Windows-1251";

                builder.AppendCodePage(CodePageType.CP1251);
            }

            builder.AppendCharacterSpace(0);

            builder.AppendAlignment(AlignmentPosition.Center);

            builder.AppendDataWithMultiple(Encoding.GetEncoding(encoding).GetBytes(
                                   "Р Е Л А К С\n").AsBuffer(), 2, 2);

            builder.AppendData(Encoding.GetEncoding(encoding).GetBytes(
                            "ООО “РЕЛАКС”\n" +
                    "СПб., Малая Балканская, д. 38, лит. А\n" +
                    "тел. 307-07-12\n").AsBuffer());

            builder.AppendAlignment(AlignmentPosition.Left);

            builder.AppendData(Encoding.GetEncoding(encoding).GetBytes(
                            "РЕГ №322736 ИНН : 123321\n" +
                    "01  Белякова И.А.  КАССА: 0020 ОТД.01\n").AsBuffer());

            builder.AppendDataWithAlignment(Encoding.GetEncoding(encoding).GetBytes(
                                    "ЧЕК НА ПРОДАЖУ  No 84373\n").AsBuffer(), AlignmentPosition.Center);

            builder.AppendData(Encoding.GetEncoding(encoding).GetBytes(
                    "------------------------------------------------\n" +
                            "1.     Яблоки Айдаред, кг                 144.50\n" +
                            "2.     Соус соевый Sen So                  36.40\n" +
                            "3.     Соус томатный Клас                  19.90\n" +
                            "4.     Ребра свиные в.к м                  78.20\n" +
                            "5.     Масло подсол раф д                 114.00\n" +
                            "6.     Блокнот 10х14см сп                 164.00\n" +
                            "7.     Морс Северная Ягод                  99.90\n" +
                            "8.     Активия Биойогурт                   43.40\n" +
                            "9.     Бублики Украинские                  26.90\n" +
                            "10.    Активия Биойогурт                   43.40\n" +
                            "11.    Сахар-песок 1кг                     58.40\n" +
                            "12.    Хлопья овсяные Ясн                  38.40\n" +
                            "13.    Кинза 50г                           39.90\n" +
                            "14.    Пемза “Сердечко” .Т                 37.90\n" +
                            "15.    Приправа Santa Mar                  47.90\n" +
                            "16.    Томаты слива Выбор                 162.00\n" +
                            "17.    Бонд Стрит Ред Сел                  56.90\n" +
                            "------------------------------------------------\n" +
                            "------------------------------------------------\n" +
                            "ДИСКОНТНАЯ КАРТА  No: 2440012489765\n" +
                            "------------------------------------------------\n").AsBuffer());

            builder.AppendAlignment(AlignmentPosition.Right);

            builder.AppendData(Encoding.GetEncoding(encoding).GetBytes(
                    "ИТОГО  К  ОПЛАТЕ     = 1212.00\n" +
                            "НАЛИЧНЫЕ             = 1212.00\n" +
                            "ВАША СКИДКА : 0.41\n" +
                            "\n").AsBuffer());

            builder.AppendDataWithAlignment(Encoding.GetEncoding(encoding).GetBytes(
                    "ЦЕНЫ УКАЗАНЫ С УЧЕТОМ СКИДКИ\n" +
                            "\n").AsBuffer(), AlignmentPosition.Center);

            builder.AppendData(Encoding.GetEncoding(encoding).GetBytes(
                    "08-02-2015 09:49  0254.0130604\n" +
                            "00083213 #060127\n").AsBuffer());

            builder.AppendAlignment(AlignmentPosition.Center);

            builder.AppendData(Encoding.GetEncoding(encoding).GetBytes(
                    "СПАСИБО ЗА ПОКУПКУ !\n" +
                            "МЫ  ОТКРЫТЫ ЕЖЕДНЕВНО С 9 ДО 23\n" +
                            "СОХРАНЯЙТЕ, ПОЖАЛУЙСТА , ЧЕК\n" +
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
                encoding = "Windows-1251";

                builder.AppendCodePage(CodePageType.CP1251);
            }

            builder.AppendCharacterSpace(0);

            builder.AppendAlignment(AlignmentPosition.Center);

            builder.AppendDataWithMultiple(Encoding.GetEncoding(encoding).GetBytes(
                                   "Р Е Л А К С\n").AsBuffer(), 2, 2);

            builder.AppendData(Encoding.GetEncoding(encoding).GetBytes(
                            "ООО “РЕЛАКС”\n" +
                    "СПб., Малая Балканская, д. 38, лит. А\n" +
                    "тел. 307-07-12\n").AsBuffer());

            builder.AppendAlignment(AlignmentPosition.Left);

            builder.AppendData(Encoding.GetEncoding(encoding).GetBytes(
                            "РЕГ №322736 ИНН : 123321\n" +
                    "01  Белякова И.А.  КАССА: 0020 ОТД.01\n").AsBuffer());

            builder.AppendDataWithAlignment(Encoding.GetEncoding(encoding).GetBytes(
                                    "ЧЕК НА ПРОДАЖУ  No 84373\n").AsBuffer(), AlignmentPosition.Center);

            builder.AppendData(Encoding.GetEncoding(encoding).GetBytes(
                    "---------------------------------------------------------------------\n" +
                            "1.     Яблоки Айдаред, кг                                      144.50\n" +
                            "2.     Соус соевый Sen So                                       36.40\n" +
                            "3.     Соус томатный Клас                                       19.90\n" +
                            "4.     Ребра свиные в.к м                                       78.20\n" +
                            "5.     Масло подсол раф д                                      114.00\n" +
                            "6.     Блокнот 10х14см сп                                      164.00\n" +
                            "7.     Морс Северная Ягод                                       99.90\n" +
                            "8.     Активия Биойогурт                                        43.40\n" +
                            "9.     Бублики Украинские                                       26.90\n" +
                            "10.    Активия Биойогурт                                        43.40\n" +
                            "11.    Сахар-песок 1кг                                          58.40\n" +
                            "12.    Хлопья овсяные Ясн                                       38.40\n" +
                            "13.    Кинза 50г                                                39.90\n" +
                            "14.    Пемза “Сердечко” .Т                                      37.90\n" +
                            "15.    Приправа Santa Mar                                       47.90\n" +
                            "16.    Томаты слива Выбор                                      162.00\n" +
                            "17.    Бонд Стрит Ред Сел                                       56.90\n" +
                            "---------------------------------------------------------------------\n" +
                            "---------------------------------------------------------------------\n" +
                            "ДИСКОНТНАЯ КАРТА  No: 2440012489765\n" +
                            "---------------------------------------------------------------------\n").AsBuffer());

            builder.AppendAlignment(AlignmentPosition.Right);

            builder.AppendData(Encoding.GetEncoding(encoding).GetBytes(
                    "ИТОГО  К  ОПЛАТЕ           = 1212.00\n" +
                            "НАЛИЧНЫЕ                   = 1212.00\n" +
                            "ВАША СКИДКА : 0.41\n" +
                            "\n").AsBuffer());

            builder.AppendDataWithAlignment(Encoding.GetEncoding(encoding).GetBytes(
                    "ЦЕНЫ УКАЗАНЫ С УЧЕТОМ СКИДКИ\n" +
                            "\n").AsBuffer(), AlignmentPosition.Center);

            builder.AppendData(Encoding.GetEncoding(encoding).GetBytes(
                    "08-02-2015 09:49  0254.0130604\n" +
                            "00083213 #060127\n").AsBuffer());

            builder.AppendAlignment(AlignmentPosition.Center);

            builder.AppendData(Encoding.GetEncoding(encoding).GetBytes(
                    "СПАСИБО ЗА ПОКУПКУ !\n" +
                            "МЫ  ОТКРЫТЫ ЕЖЕДНЕВНО С 9 ДО 23\n" +
                            "СОХРАНЯЙТЕ, ПОЖАЛУЙСТА , ЧЕК\n" +
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
                encoding = "Windows-1251";

                builder.AppendCodePage(CodePageType.CP1251);
            }

            builder.AppendCharacterSpace(0);

            builder.AppendAlignment(AlignmentPosition.Center);

            builder.AppendData(Encoding.GetEncoding(encoding).GetBytes(
                           "\n").AsBuffer());

            builder.AppendDataWithMultiple(Encoding.GetEncoding(encoding).GetBytes(
                                   "Р Е Л А К С\n").AsBuffer(), 2, 2);

            builder.AppendData(Encoding.GetEncoding(encoding).GetBytes(
                            "ООО “РЕЛАКС”\n" +
                    "СПб., Малая Балканская, д. 38, лит. А\n" +
                    "тел. 307-07-12\n").AsBuffer());

            builder.AppendAlignment(AlignmentPosition.Left);

            builder.AppendData(Encoding.GetEncoding(encoding).GetBytes(
                            "РЕГ №322736 ИНН : 123321\n" +
                    "01  Белякова И.А.  КАССА: 0020 ОТД.01\n").AsBuffer());

            builder.AppendDataWithAlignment(Encoding.GetEncoding(encoding).GetBytes(
                                    "ЧЕК НА ПРОДАЖУ  No 84373\n").AsBuffer(), AlignmentPosition.Center);

            builder.AppendData(Encoding.GetEncoding(encoding).GetBytes(
                    "------------------------------------------\n" +
                            "1.     Яблоки Айдаред, кг           144.50\n" +
                            "2.     Соус соевый Sen So            36.40\n" +
                            "3.     Соус томатный Клас            19.90\n" +
                            "4.     Ребра свиные в.к м            78.20\n" +
                            "5.     Масло подсол раф д           114.00\n" +
                            "6.     Блокнот 10х14см сп           164.00\n" +
                            "7.     Морс Северная Ягод            99.90\n" +
                            "8.     Активия Биойогурт             43.40\n" +
                            "9.     Бублики Украинские            26.90\n" +
                            "10.    Активия Биойогурт             43.40\n" +
                            "11.    Сахар-песок 1кг               58.40\n" +
                            "12.    Хлопья овсяные Ясн            38.40\n" +
                            "13.    Кинза 50г                     39.90\n" +
                            "14.    Пемза “Сердечко” .Т           37.90\n" +
                            "15.    Приправа Santa Mar            47.90\n" +
                            "16.    Томаты слива Выбор           162.00\n" +
                            "17.    Бонд Стрит Ред Сел            56.90\n" +
                            "------------------------------------------\n" +
                            "------------------------------------------\n" +
                            "ДИСКОНТНАЯ КАРТА  No: 2440012489765\n" +
                            "------------------------------------------\n").AsBuffer());

            builder.AppendAlignment(AlignmentPosition.Right);

            builder.AppendData(Encoding.GetEncoding(encoding).GetBytes(
                    "ИТОГО  К  ОПЛАТЕ     = 1212.00\n" +
                            "НАЛИЧНЫЕ             = 1212.00\n" +
                            "ВАША СКИДКА : 0.41\n" +
                            "\n").AsBuffer());

            builder.AppendDataWithAlignment(Encoding.GetEncoding(encoding).GetBytes(
                    "ЦЕНЫ УКАЗАНЫ С УЧЕТОМ СКИДКИ\n" +
                            "\n").AsBuffer(), AlignmentPosition.Center);

            builder.AppendData(Encoding.GetEncoding(encoding).GetBytes(
                    "08-02-2015 09:49  0254.0130604\n" +
                            "00083213 #060127\n").AsBuffer());

            builder.AppendAlignment(AlignmentPosition.Center);

            builder.AppendData(Encoding.GetEncoding(encoding).GetBytes(
                    "СПАСИБО ЗА ПОКУПКУ !\n" +
                            "МЫ  ОТКРЫТЫ ЕЖЕДНЕВНО С 9 ДО 23\n" +
                            "СОХРАНЯЙТЕ, ПОЖАЛУЙСТА , ЧЕК\n" +
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
                encoding = "Windows-1251";

                builder.AppendCodePage(CodePageType.CP1251);
            }

            builder.AppendAlignment(AlignmentPosition.Center);

            builder.AppendDataWithMultiple(Encoding.GetEncoding(encoding).GetBytes(
                                   "Р Е Л А К С\n").AsBuffer(), 2, 2);

            builder.AppendData(Encoding.GetEncoding(encoding).GetBytes(
                            "ООО “РЕЛАКС”\n" +
                    "СПб., Малая Балканская, д. 38, лит. А\n" +
                    "тел. 307-07-12\n").AsBuffer());

            builder.AppendAlignment(AlignmentPosition.Left);

            builder.AppendData(Encoding.GetEncoding(encoding).GetBytes(
                            "РЕГ №322736 ИНН : 123321\n" +
                    "01  Белякова И.А.  КАССА: 0020 ОТД.01\n").AsBuffer());

            builder.AppendDataWithAlignment(Encoding.GetEncoding(encoding).GetBytes(
                                    "ЧЕК НА ПРОДАЖУ  No 84373\n").AsBuffer(), AlignmentPosition.Center);

            builder.AppendData(Encoding.GetEncoding(encoding).GetBytes(
                    "------------------------------------------\n" +
                            "1.     Яблоки Айдаред, кг           144.50\n" +
                            "2.     Соус соевый Sen So            36.40\n" +
                            "3.     Соус томатный Клас            19.90\n" +
                            "4.     Ребра свиные в.к м            78.20\n" +
                            "5.     Масло подсол раф д           114.00\n" +
                            "6.     Блокнот 10х14см сп           164.00\n" +
                            "7.     Морс Северная Ягод            99.90\n" +
                            "8.     Активия Биойогурт             43.40\n" +
                            "9.     Бублики Украинские            26.90\n" +
                            "10.    Активия Биойогурт             43.40\n" +
                            "11.    Сахар-песок 1кг               58.40\n" +
                            "12.    Хлопья овсяные Ясн            38.40\n" +
                            "13.    Кинза 50г                     39.90\n" +
                            "14.    Пемза “Сердечко” .Т           37.90\n" +
                            "15.    Приправа Santa Mar            47.90\n" +
                            "16.    Томаты слива Выбор           162.00\n" +
                            "17.    Бонд Стрит Ред Сел            56.90\n" +
                            "------------------------------------------\n" +
                            "------------------------------------------\n" +
                            "ДИСКОНТНАЯ КАРТА  No: 2440012489765\n" +
                            "------------------------------------------\n").AsBuffer());

            builder.AppendAlignment(AlignmentPosition.Right);

            builder.AppendData(Encoding.GetEncoding(encoding).GetBytes(
                    "ИТОГО  К  ОПЛАТЕ  = 1212.00\n" +
                            "НАЛИЧНЫЕ          = 1212.00\n" +
                            "ВАША СКИДКА : 0.41\n" +
                            "\n").AsBuffer());

            builder.AppendDataWithAlignment(Encoding.GetEncoding(encoding).GetBytes(
                    "ЦЕНЫ УКАЗАНЫ С УЧЕТОМ СКИДКИ\n" +
                            "\n").AsBuffer(), AlignmentPosition.Center);

            builder.AppendData(Encoding.GetEncoding(encoding).GetBytes(
                    "08-02-2015 09:49  0254.0130604\n" +
                            "00083213 #060127\n").AsBuffer());

            builder.AppendAlignment(AlignmentPosition.Center);

            builder.AppendData(Encoding.GetEncoding(encoding).GetBytes(
                    "СПАСИБО ЗА ПОКУПКУ !\n" +
                            "МЫ  ОТКРЫТЫ ЕЖЕДНЕВНО С 9 ДО 23\n" +
                            "СОХРАНЯЙТЕ, ПОЖАЛУЙСТА , ЧЕК\n").AsBuffer());
        }

        public override async Task<BitmapDecoder> CreateCouponImageAsync()
        {
            BitmapDecoder logoData = null;

            StorageFile logoFile = await StorageFile.GetFileFromApplicationUriAsync(new Uri("ms-appx:///Assets/coupon_image_russian.png"));
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
                "                    Р Е Л А К С\n" +
                "                    ООО “РЕЛАКС”\n" +
                "СПб., Малая Балканская, д.\n" +
                "38, лит. А\n" +
                "\n" +
                "тел. 307-07-12\n" +
                "РЕГ №322736     ИНН:123321\n" +
                "01 Белякова И.А. КАССА:0020\n" +
                "ОТД.01\n" +
                "ЧЕК НА ПРОДАЖУ  No 84373\n" +
                "----------------------------------------\n" +
                "  1.Яблоки Айдаред, кг\t 144.50\n" +
                "  2.Соус соевый Sen So\t   36.40\n" +
                "  3.Соус томатный Клас\t   19.90\n" +
                "  4.Ребра свиные в.к м\t   78.20\n" +
                "  5.Масло подсол раф д\t 114.00\n" +
                "  6.Блокнот 10х14см сп\t 164.00\n" +
                "  7.Морс Северная Ягод\t   99.90\n" +
                "  8.Активия Биойогурт\t   43.40\n" +
                "  9.Бублики Украинские\t   26.90\n" +
                "10.Активия Биойогурт\t   43.40\n" +
                "11.Сахар-песок 1кг\t   58.40\n" +
                "12.Хлопья овсяные Ясн\t   38.40\n" +
                "13.Кинза 50г\t\t   39.90\n" +
                "14.Пемза “Сердечко” .Т\t   37.90\n" +
                "15.Приправа Santa Mar\t   47.90\n" +
                "16.Томаты слива Выбор 162.00\n" +
                "17.Бонд Стрит Ред Сел\t   56.90\n" +
                "----------------------------------------\n" +
                "----------------------------------------\n" +
                "ДИСКОНТНАЯ КАРТА\n" +
                "            No:2440012489765\n" +
                "----------------------------------------\n" +
                "ИТОГО К ОПЛАТЕ = 1212.00\n" +
                "НАЛИЧНЫЕ = 1212.00\n" +
                "ВАША СКИДКА : 0.41\n" +
                "ЦЕНЫ УКАЗАНЫ С УЧЕТОМ\n" +
                "СКИДКИ\n" +
                "\n" +
                "08-02-2015 09:49\n" +
                "0254.013060400083213 #060127\n" +
                "СПАСИБО ЗА ПОКУПКУ !\n" +
                "\n" +
                "МЫ  ОТКРЫТЫ ЕЖЕДНЕВНО\n" +
                "С 9 ДО23 СОХРАНЯЙТЕ,\n" +
                "ПОЖАЛУЙСТА ,ЧЕК\n";

            return textToPrint;
        }

        public override string Create3inchRasterReceiptText()
        {
            String textToPrint =
                "           Р Е Л А К С   ООО “РЕЛАКС”\n" +
                " СПб., Малая Балканская, д. 38, лит. А\n" +
                "\n" +
                "тел. 307-07-12\n" +
                "РЕГ №322736     ИНН:123321\n" +
                "01 Белякова И.А. КАССА: 0020 ОТД.01\n" +
                "ЧЕК НА ПРОДАЖУ  No 84373\n" +
                "------------------------------------------\n" +
                "  1. Яблоки Айдаред, кг\t\t144.50\n" +
                "  2. Соус соевый Sen So\t\t  36.40\n" +
                "  3. Соус томатный Клас\t\t  19.90\n" +
                "  4. Ребра свиные в.к м\t\t  78.20\n" +
                "  5. Масло подсол раф д\t\t114.00\n" +
                "  6. Блокнот 10х14см сп\t\t164.00\n" +
                "  7. Морс Северная Ягод\t\t  99.90\n" +
                "  8. Активия Биойогурт\t\t  43.40\n" +
                "  9. Бублики Украинские\t\t  26.90\n" +
                "10. Активия Биойогурт\t\t  43.40\n" +
                "11. Сахар-песок 1кг\t\t\t  58.40\n" +
                "12. Хлопья овсяные Ясн\t\t  38.40\n" +
                "13. Кинза 50г\t\t\t\t  39.90\n" +
                "14. Пемза “Сердечко” .Т\t\t  37.90\n" +
                "15. Приправа Santa Mar\t\t  47.90\n" +
                "16. Томаты слива Выбор\t\t162.00\n" +
                "17. Бонд Стрит Ред Сел\t\t  56.90\n" +
                "------------------------------------------\n" +
                "------------------------------------------\n" +
                "ДИСКОНТНАЯ КАРТА\n" +
                "No:2440012489765\n" +
                "------------------------------------------\n" +
                "ИТОГО К ОПЛАТЕ = 1212.00\n" +
                "НАЛИЧНЫЕ = 1212.00\n" +
                "ВАША СКИДКА : 0.41\n" +
                "ЦЕНЫ УКАЗАНЫ С УЧЕТОМ СКИДКИ\n" +
                "\n" +
                "08-02-2015 09:49  0254.0130604\n" +
                "00083213 #060127\n" +
                "               СПАСИБО ЗА ПОКУПКУ !\n" +
                "\n" +
                "МЫ ОТКРЫТЫ ЕЖЕДНЕВНО С 9 ДО\n" +
                "23 СОХРАНЯЙТЕ, ПОЖАЛУЙСТА ,\n"+
                "ЧЕК\n";

            return textToPrint;
        }

        public override string Create4inchRasterReceiptText()
        {
            String textToPrint =
                "                                   Р Е Л А К С   ООО “РЕЛАКС”\n" +
                "                              СПб., Малая Балканская, д. 38, лит. А\n" +
                "\n" +
                "тел. 307-07-12\n" +
                "РЕГ №322736     ИНН:123321\n" +
                "01 Белякова И.А. КАССА: 0020 ОТД.01\n" +
                "ЧЕК НА ПРОДАЖУ  No 84373\n" +
                "---------------------------------------------------------------------------\n" +
                "  1.      \t\tЯблоки Айдаред, кг\t\t\t\t\t144.50\n" +
                "  2.      \t\tСоус соевый Sen So\t\t\t\t\t  36.40\n" +
                "  3.      \t\tСоус томатный Клас\t\t\t\t\t  19.90\n" +
                "  4.      \t\tРебра свиные в.к м\t\t\t\t\t  78.20\n" +
                "  5.      \t\tМасло подсол раф д\t\t\t\t\t114.00\n" +
                "  6.      \t\tБлокнот 10х14см сп\t\t\t\t\t164.00\n" +
                "  7.      \t\tМорс Северная Ягод\t\t\t\t\t  99.90\n" +
                "  8.      \t\tАктивия Биойогурт\t\t\t\t\t  43.40\n" +
                "  9.      \t\tБублики Украинские\t\t\t\t\t  26.90\n" +
                "10.      \t\tАктивия Биойогурт\t\t\t\t\t  43.40\n" +
                "11.      \t\tСахар-песок 1кг\t\t\t\t\t  58.40\n" +
                "12.      \t\tХлопья овсяные Ясн\t\t\t\t\t  38.40\n" +
                "13.      \t\tКинза 50г\t\t\t\t\t\t\t  39.90\n" +
                "14.      \t\tПемза “Сердечко” .Т\t\t\t\t\t  37.90\n" +
                "15.      \t\tПриправа Santa Mar\t\t\t\t\t  47.90\n" +
                "16.      \t\tТоматы слива Выбор\t\t\t\t162.00\n" +
                "17.      \t\tБонд Стрит Ред Сел\t\t\t\t\t  56.90\n" +
                "---------------------------------------------------------------------------\n" +
                "---------------------------------------------------------------------------\n" +
                "ДИСКОНТНАЯ КАРТА                     \t\tNo:2440012489765\n" +
                "---------------------------------------------------------------------------\n" +
                "ИТОГО К ОПЛАТЕ = 1212.00\n" +
                "НАЛИЧНЫЕ = 1212.00\n" +
                "ВАША СКИДКА : 0.41\n" +
                "ЦЕНЫ УКАЗАНЫ С УЧЕТОМ СКИДКИ\n" +
                "\n" +
                "08-02-2015 09:49  0254.0130604\n" +
                "00083213 #060127\n" +
                "                                 СПАСИБО ЗА ПОКУПКУ !\n" +
                "\n" +
                "                      МЫ  ОТКРЫТЫ ЕЖЕДНЕВНО С 9 ДО 23\n" +
                "                         СОХРАНЯЙТЕ, ПОЖАЛУЙСТА , ЧЕК\n";

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
