using StarIO_Extension;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading.Tasks;
using Windows.Graphics.Imaging;
using Windows.Storage;
using Windows.Storage.Streams;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;

namespace StarPRNTSDK.Localizereceipts
{
    class UTF8MultiLanguageReceiptsImple : ILocalizeReceipts
    {

        public UTF8MultiLanguageReceiptsImple()
        {
            LanguageCode = "CJK";

            CharacterCode = CharacterCode.Standard;
        }

        public override void Append2inchTextReceiptData(StarIO_Extension.ICommandBuilder builder, bool utf8)
        {
            string encoding = "UTF-8";

            builder.AppendCodePage(CodePageType.UTF8);

            builder.AppendCharacterSpace(0);

            builder.AppendAlignment(AlignmentPosition.Center);

            builder.AppendData(Encoding.GetEncoding(encoding).GetBytes("2017 / 5 / 15 AM 10:00\n").AsBuffer());

            builder.AppendMultiple(2, 2);

            // This function is supported by TSP650II(JP2/TW models only) with F/W version 4.0 or later and and mC-Print2/3.
            // Switch Kanji/Hangul font by specifying the font for Unicode CJK Unified Ideographs before each word.

            builder.AppendCjkUnifiedIdeographFont(CjkUnifiedIdeographFont.Japanese);
            builder.AppendData(Encoding.GetEncoding(encoding).GetBytes("受付票 ").AsBuffer());

            builder.AppendCjkUnifiedIdeographFont(CjkUnifiedIdeographFont.TraditionalChinese);
            builder.AppendData(Encoding.GetEncoding(encoding).GetBytes("排號單\n").AsBuffer());

            builder.AppendCjkUnifiedIdeographFont(CjkUnifiedIdeographFont.SimplifiedChinese);
            builder.AppendData(Encoding.GetEncoding(encoding).GetBytes("排号单 ").AsBuffer());

            builder.AppendCjkUnifiedIdeographFont(CjkUnifiedIdeographFont.Hangul);
            builder.AppendData(Encoding.GetEncoding(encoding).GetBytes("접수표\n\n").AsBuffer());

            builder.AppendMultiple(1, 1);

            builder.AppendCjkUnifiedIdeographFont();
            builder.AppendDataWithMultiple(Encoding.GetEncoding(encoding).GetBytes("1\n").AsBuffer(), 6, 6);
            builder.AppendData(Encoding.GetEncoding(encoding).GetBytes("--------------------------------\n").AsBuffer());

            builder.AppendCjkUnifiedIdeographFont(CjkUnifiedIdeographFont.Japanese);
            builder.AppendData(Encoding.GetEncoding(encoding).GetBytes("ご本人がお持ちください。\n").AsBuffer());
            builder.AppendData(Encoding.GetEncoding(encoding).GetBytes("※紛失しないように\n").AsBuffer());
            builder.AppendData(Encoding.GetEncoding(encoding).GetBytes("ご注意ください。\n").AsBuffer());
        }

        public override void Append3inchTextReceiptData(StarIO_Extension.ICommandBuilder builder, bool utf8)
        {
            string encoding = "UTF-8";

            builder.AppendCodePage(CodePageType.UTF8);

            builder.AppendCharacterSpace(0);

            builder.AppendAlignment(AlignmentPosition.Center);

            builder.AppendData(Encoding.GetEncoding(encoding).GetBytes("2017 / 5 / 15 AM 10:00\n").AsBuffer());

            builder.AppendMultiple(2, 2);

            // This function is supported by TSP650II(JP2/TW models only) with F/W version 4.0 or later and and mC-Print2/3.
            // Switch Kanji/Hangul font by specifying the font for Unicode CJK Unified Ideographs before each word.

            builder.AppendCjkUnifiedIdeographFont(CjkUnifiedIdeographFont.Japanese);
            builder.AppendData(Encoding.GetEncoding(encoding).GetBytes("受付票 ").AsBuffer());

            builder.AppendCjkUnifiedIdeographFont(CjkUnifiedIdeographFont.TraditionalChinese);
            builder.AppendData(Encoding.GetEncoding(encoding).GetBytes("排號單\n").AsBuffer());

            builder.AppendCjkUnifiedIdeographFont(CjkUnifiedIdeographFont.SimplifiedChinese);
            builder.AppendData(Encoding.GetEncoding(encoding).GetBytes("排号单 ").AsBuffer());

            builder.AppendCjkUnifiedIdeographFont(CjkUnifiedIdeographFont.Hangul);
            builder.AppendData(Encoding.GetEncoding(encoding).GetBytes("접수표\n\n").AsBuffer());

            builder.AppendMultiple(1, 1);

            builder.AppendCjkUnifiedIdeographFont();
            builder.AppendDataWithMultiple(Encoding.GetEncoding(encoding).GetBytes("1\n").AsBuffer(), 6, 6);
            builder.AppendData(Encoding.GetEncoding(encoding).GetBytes("------------------------------------------\n").AsBuffer());

            builder.AppendCjkUnifiedIdeographFont(CjkUnifiedIdeographFont.Japanese);
            builder.AppendData(Encoding.GetEncoding(encoding).GetBytes("ご本人がお持ちください。\n").AsBuffer());
            builder.AppendData(Encoding.GetEncoding(encoding).GetBytes("※紛失しないようにご注意ください。\n").AsBuffer());
        }

        public override void Append4inchTextReceiptData(StarIO_Extension.ICommandBuilder builder, bool utf8)
        {
            // not implemented
        }

        public override void AppendEscPos3inchTextReceiptData(StarIO_Extension.ICommandBuilder builder, bool utf8)
        {
            // not implemented
        }

        public override void AppendDotImpact3inchTextReceiptData(StarIO_Extension.ICommandBuilder builder, bool utf8)
        {
            // not implemented
        }

        public override string Create2inchRasterReceiptText()
        {
            // not implemented
            return null;
        }

        public override string Create3inchRasterReceiptText()
        {
            // not implemented
            return null;
        }

        public override string Create4inchRasterReceiptText()
        {
            // not implemented
            return null;
        }


        public override Task<BitmapDecoder> Create2inchRasterImageAsync()
        {
            // not implemented
            return null;
        }


        public override Task<BitmapDecoder> Create3inchRasterImageAsync()
        {
            // not implemented
            return null;
        }

        public override Task<BitmapDecoder> CreateEscPos3inchRasterImageAsync()
        {
            // not implemented
            return null;
        }

        public override Task<BitmapDecoder> Create4inchRasterImageAsync()
        {
            // not implemented
            return null;
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

        public override Task<BitmapDecoder> CreateCouponImageAsync()
        {
            // not implemented
            return null;
        }
    }
}
