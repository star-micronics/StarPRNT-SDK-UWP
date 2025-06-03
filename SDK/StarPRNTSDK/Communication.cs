using StarIOPort;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage.Streams;

namespace StarPRNTSDK
{
    class Communication
    {
        public enum Result
        {
            Success,
            ErrorUnknown,
            ErrorOpenPort,
            ErrorBeginCheckedBlock,
            ErrorEndCheckedBlock,
            ErrorWritePort,
            ErrorReadPort,
        }

        public static async Task<Result> SendCommands(IBuffer buffer, StarIOPort.Port port)
        {
            Result result = Result.ErrorUnknown;
            try
            {
                if (port == null)
                {
                    result = Result.ErrorOpenPort;
                    return result;
                }

                StarIOPort.Status status;

                result = Result.ErrorBeginCheckedBlock;

                status = await port.BeginCheckedBlockAsync();

               

                if (status.Offline == true)
                {
                    throw new Exception("Printer is offline.");
                }

                result = Result.ErrorWritePort;


                if (await port.WriteAsync(buffer) != buffer.Length)
                {
                    throw new Exception("WriteAsync failed.");
                }

                result = Result.ErrorEndCheckedBlock;

                status = await port.EndCheckedBlockAsync();

                if (status.Offline == true)
                {
                    String message = "Printer is offline.";

                    if (status.ReceiptPaperEmpty == true)
                    {
                        message += "\nReceipt paper is empty.";
                    }
                    if (status.CoverOpen == true)
                    {
                        message += "\nPrinter cover is open.";
                    }
                    throw new Exception(message);
                }

                result = Result.Success;

            }
            catch (Exception)
            {

            }

            return result;
        }

        public static async Task<Result> SendCommandsDoNotCheckCondition(IBuffer buffer, StarIOPort.Port port)
        {
            Result result = Result.ErrorUnknown;

            try
            {
                if (port == null)
                {
                    result = Result.ErrorOpenPort;
                    return result;
                }

                StarIOPort.Status status;

                result = Result.ErrorWritePort;
                status = await port.GetParsedStatusAsync();

                if (status.RawStatus.Length == 0)
                {
                    throw new Exception("Printer is offline.");
                }

                result = Result.ErrorWritePort;
                if (await port.WriteAsync(buffer) != buffer.Length)
                {
                    throw new Exception("WriteAsync failed.");
                }


                result = Result.ErrorWritePort;
                status = await port.GetParsedStatusAsync();
                if (status.RawStatus.Length == 0)
                {
                    throw new Exception("Printer is offline.");
                }

                result = Result.Success;

            }
            catch (Exception)
            {
            }

            return result;

        }


        public static async Task<Result> SendCommands(IBuffer buffer, string portName, string portSettings, int timeout)
        {
            Result result = Result.ErrorUnknown;
            
            try
            {              
                using (StarIOPort.Port port = new StarIOPort.Port(timeout))
                {
                    result = Result.ErrorOpenPort;
                    await port.ConnectAsync(portName, portSettings);

                    StarIOPort.Status status;

                    result = Result.ErrorBeginCheckedBlock;
                    status = await port.BeginCheckedBlockAsync();

                    if (status.Offline == true)
                    {
                        string message = "Printer is Offline.";

                        if (status.ReceiptPaperEmpty == true)
                        {
                            message += "\nPaper is Empty.";
                        }
                        if (status.CoverOpen == true)
                        {
                            message += "\nCover is Open.";
                        }
                        throw new Exception(message);
                    }


                    result = Result.ErrorWritePort;
                    if (await port.WriteAsync(buffer) != buffer.Length)
                    {
                        throw new Exception("WriteAsync failed.");
                    }

                    result = Result.ErrorEndCheckedBlock;
                    status = await port.EndCheckedBlockAsync();

                    if (status.Offline == true)
                    {
                        string message = "Printer is Offline.";

                        if (status.ReceiptPaperEmpty == true)
                        {
                            message += "\nPaper is Empty.";
                        }
                        if (status.CoverOpen == true)
                        {
                            message += "\nCover is Open.";
                        }
                        throw new Exception(message);
                    }

                    result = Result.Success;
                }
            }
            catch (Exception )
            {
                
            }


            return result;
        }

        public static async Task<Result> SendCommandsDoNotCheckCondition(IBuffer buffer, string portName, string portSettings, int timeout)
        {
            Result result = Result.ErrorUnknown;
            try
            {
                using (StarIOPort.Port port = new StarIOPort.Port(timeout))
                {
                    result = Result.ErrorOpenPort;
                    await port.ConnectAsync(portName, portSettings);

                    StarIOPort.Status status;


                    result = Result.ErrorWritePort;
                    status = await port.GetParsedStatusAsync();

                    if (status.RawStatus.Length == 0)
                    {
                        throw new Exception("Printer is offline.");
                    }


                    result = Result.ErrorWritePort;
                    if (await port.WriteAsync(buffer) != buffer.Length)
                    {
                        throw new Exception("WriteAsync failed.");
                    }

                    result = Result.ErrorWritePort;
                    status = await port.GetParsedStatusAsync();

                    if (status.RawStatus.Length == 0)
                    {
                        throw new Exception("Printer is offline.");
                    }

                    result = Result.Success;
                }
            }
            catch (Exception)
            {

            }

            return result;
        }


        public static async Task<Status> RetrieveStatus(string portName, string portSettings, int timeout)
        {
            Status status = null;

            try
            {
                using (StarIOPort.Port port = new StarIOPort.Port(timeout))
                {
                    await port.ConnectAsync(portName, portSettings);

                    status = await port.GetParsedStatusAsync();

                    if (status.RawStatus.Length == 0)
                    {
                        string message = "Printer is Offline.";
                        throw new Exception(message);
                    }

                }
            }
            catch (Exception)
            {

            }

            return status;
        }
    }
}
