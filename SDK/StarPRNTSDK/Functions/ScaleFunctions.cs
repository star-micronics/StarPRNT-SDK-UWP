using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage.Streams;
using StarIO_Extension;

namespace StarPRNTSDK.Functions
{
    class ScaleFunctions
    {
        public static void AppendZeroClear(IScaleCommandBuilder builder)
        {
            builder.AppendZeroClear();
        }

        public static void AppendUnitChange(IScaleCommandBuilder builder)
        {
            builder.AppendUnitChange();
        }
    }
}
