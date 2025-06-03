using StarIO_Extension;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage.Streams;

namespace StarPRNTSDK.Functions
{
    class CashDrawerFunctions
    {
        public static IBuffer CreateData(Emulation emulation, PeripheralChannel channel)
        {
            ICommandBuilder builder = StarIO_Extension.StarIoExt.CreateCommandBuilder(emulation);

            builder.BeginDocument();

            builder.AppendPeripheral(channel);

            builder.EndDocument();

            return builder.GetCommands();
        }
    }
}
