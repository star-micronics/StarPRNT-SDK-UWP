using StarIO_Extension;
using StarPRNTSDK.Localizereceipts;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading.Tasks;
using Windows.Graphics.Imaging;
using Windows.Storage;
using Windows.Storage.Streams;

namespace StarPRNTSDK.Functions
{
    class PrinterFunctions
    {
        public static IBuffer CreateTextReceiptData(Emulation emulation, ILocalizeReceipts localizeReceipts, bool utf8)
        {
            ICommandBuilder builder = StarIO_Extension.StarIoExt.CreateCommandBuilder(emulation);

            builder.BeginDocument();

            localizeReceipts.AppendTextReceiptData(builder, utf8);

            builder.AppendCutPaper(CutPaperAction.PartialCutWithFeed);

            builder.EndDocument();

            return builder.GetCommands();
        }

        public async static Task<IBuffer> CreateRasterReceiptData(Emulation emulation, ILocalizeReceipts localizeReceipts)
        {
            ICommandBuilder builder = StarIO_Extension.StarIoExt.CreateCommandBuilder(emulation);

            builder.BeginDocument();

            BitmapDecoder image = null;

            image = await localizeReceipts.CreateRasterReceiptImage();

            await builder.AppendBitmapAsync(image, false);

            builder.AppendCutPaper(CutPaperAction.PartialCutWithFeed);

            builder.EndDocument();

            return builder.GetCommands();
        }

        public async static Task<IBuffer> CreateScaleRasterReceiptData(Emulation emulation, ILocalizeReceipts localizeReceipts, uint width, bool bothScale)
        {
            ICommandBuilder builder = StarIO_Extension.StarIoExt.CreateCommandBuilder(emulation);

            builder.BeginDocument();

            BitmapDecoder image = null;

            image = await localizeReceipts.CreateScaleRasterReceiptImage();

            await builder.AppendBitmapAsync(image, false, width, bothScale);

            builder.AppendCutPaper(CutPaperAction.PartialCutWithFeed);

            builder.EndDocument();

            return builder.GetCommands();
        }


        public static async Task<IBuffer> CreateCouponData(Emulation emulation, ILocalizeReceipts localizeReceipts, uint width, BitmapConverterRotation rotation)
        {
            ICommandBuilder builder = StarIO_Extension.StarIoExt.CreateCommandBuilder(emulation);

            builder.BeginDocument();

            BitmapDecoder image = await localizeReceipts.CreateCouponImageAsync();

            await builder.AppendBitmapAsync(image, false, width, true, rotation);

            builder.AppendCutPaper(CutPaperAction.PartialCutWithFeed);

            builder.EndDocument();

            return builder.GetCommands();
        }

        public static IBuffer CreateTextBlackMarkData(Emulation emulation, ILocalizeReceipts localizeReceipts, BlackMarkType type, bool utf8)
        {
            ICommandBuilder builder = StarIO_Extension.StarIoExt.CreateCommandBuilder(emulation);

            builder.BeginDocument();

            builder.AppendBlackMark(type);

            localizeReceipts.AppendTextLabelData(builder, utf8);

            builder.AppendCutPaper(CutPaperAction.PartialCutWithFeed);

            builder.EndDocument();

            return builder.GetCommands();
        }

        public static IBuffer CreatePasteTextBlackMarkData(Emulation emulation, ILocalizeReceipts localizeReceipts, string pasteText, bool doubleHeight, BlackMarkType type, bool utf8)
        {
            ICommandBuilder builder = StarIO_Extension.StarIoExt.CreateCommandBuilder(emulation);

            builder.BeginDocument();

            builder.AppendBlackMark(type);

            if (doubleHeight)
            {
                builder.AppendMultipleHeight(2);

                localizeReceipts.AppendPasteTextLabelData(builder, pasteText, utf8);

                builder.AppendMultipleHeight(1);
            }
            else
            {
                localizeReceipts.AppendPasteTextLabelData(builder, pasteText, utf8);
            }

            builder.AppendCutPaper(CutPaperAction.PartialCutWithFeed);

            builder.EndDocument();

            return builder.GetCommands();
        }

        public static IBuffer CreateTextPageModeData(Emulation emulation, ILocalizeReceipts localizeReceipts, PageModePrintRegion printRegion, BitmapConverterRotation rotation, bool utf8)
        {
            ICommandBuilder builder = StarIO_Extension.StarIoExt.CreateCommandBuilder(emulation);

            builder.BeginDocument();

            builder.BeginPageMode(printRegion, rotation);

            localizeReceipts.AppendTextLabelData(builder, utf8);

            builder.EndPageMode();

            builder.AppendCutPaper(CutPaperAction.PartialCutWithFeed);

            builder.EndDocument();

            return builder.GetCommands();
        }

        public async static Task<IBuffer> CreateFileOpenData(Emulation emulation, StorageFile storageFile, uint paperSize)
        {
            ICommandBuilder builder = StarIO_Extension.StarIoExt.CreateCommandBuilder(emulation);

            builder.BeginDocument();

            var logoStream = await storageFile.OpenAsync(Windows.Storage.FileAccessMode.Read);

            BitmapDecoder image = await BitmapDecoder.CreateAsync(logoStream);
            logoStream.Dispose();
            logoStream = null;
            storageFile = null;

            await builder.AppendBitmapAsync(image, true, paperSize, true);

            builder.AppendCutPaper(CutPaperAction.PartialCutWithFeed);

            builder.EndDocument();

            return builder.GetCommands();
        }

        public static List<IBuffer> CreateHoldPrintData(Emulation emulation, bool[] isHoldArray)
        {
            List<IBuffer> commandList = new List<IBuffer>();

            for (int i = 0; i < isHoldArray.Length; i++)
            {
                ICommandBuilder builder = StarIoExt.CreateCommandBuilder(emulation);

                builder.BeginDocument();

                // Disable hold print controlled by printer firmware.
                builder.AppendHoldPrint(HoldPrintType.Invalid);

                if (isHoldArray[i])
                {
                    // Enable paper present status if wait paper removal before next printing.
                    builder.AppendPaperPresentStatus(PaperPresentStatusType.Valid);
                }
                else
                {
                    // Disable paper present status if do not wait paper removal before next printing.
                    builder.AppendPaperPresentStatus(PaperPresentStatusType.Invalid);
                }

                // Create commands for printing.
                builder.AppendAlignment(AlignmentPosition.Center);

                builder.AppendData(Encoding.ASCII.GetBytes("\n------------------------------------\n\n\n\n\n\n").AsBuffer());

                builder.AppendMultiple(3, 3);

                builder.AppendData(Encoding.ASCII.GetBytes("Page ").AsBuffer());
                builder.AppendData(Encoding.ASCII.GetBytes((i + 1).ToString()).AsBuffer());

                builder.AppendMultiple(1, 1);

                builder.AppendData(Encoding.ASCII.GetBytes("\n\n\n\n\n----------------------------------\n").AsBuffer());

                builder.AppendCutPaper(CutPaperAction.PartialCutWithFeed);

                builder.EndDocument();

                commandList.Add(builder.GetCommands());
            }

            return commandList;
        }
    }
}
