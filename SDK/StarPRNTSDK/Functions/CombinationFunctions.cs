using StarIO_Extension;
using StarPRNTSDK.Localizereceipts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Graphics.Imaging;
using Windows.Storage;
using Windows.Storage.Streams;

namespace StarPRNTSDK.Functions
{
    class CombinationFunctions
    {
        public static IBuffer CreateTextReceiptData(Emulation emulation, ILocalizeReceipts localizeReceipts, bool utf8)
        {
            ICommandBuilder builder = StarIO_Extension.StarIoExt.CreateCommandBuilder(emulation);

            builder.BeginDocument();

            localizeReceipts.AppendTextReceiptData(builder, utf8);

            builder.AppendCutPaper(CutPaperAction.PartialCutWithFeed);

            builder.AppendPeripheral(PeripheralChannel.No1);

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

            builder.AppendPeripheral(PeripheralChannel.No1);

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

            builder.AppendPeripheral(PeripheralChannel.No1);

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

            builder.AppendPeripheral(PeripheralChannel.No1);

            builder.EndDocument();

            return builder.GetCommands();
        }

    }
}
