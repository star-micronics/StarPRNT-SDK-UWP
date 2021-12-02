using StarIOPort;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage.Streams;
using System.Runtime.InteropServices.WindowsRuntime;
using StarPRNTSDK.Functions;
using StarIO_Extension;

namespace StarPRNTSDK
{

    class ScaleCommunication 
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

        public static async Task<Result> ParseDoNotCheckCondition(IPeripheralCommandParser parser, StarIOPort.Port port)
        {
            Result result = Result.ErrorUnknown;

            IBuffer requestWeightToScaleCommand = parser.CreateSendCommands();
            IBuffer receiveWeightFromPrinterCommand = parser.CreateReceiveCommands();

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
                    throw new Exception("Unable to communicate with printer.");
                }

                UInt32 start = (UInt32)Environment.TickCount;

                bool isScaleDataReceived = false;

                result = Result.ErrorWritePort;

                while (!isScaleDataReceived)
                {
                    if (await port.WriteAsync(requestWeightToScaleCommand) != requestWeightToScaleCommand.Length)
                    {
                        throw new Exception("WriteAsync failed.");
                    }

                    UInt32 startRequestWeight = (UInt32)Environment.TickCount;

                    while (!isScaleDataReceived)
                    {
                        await Task.Delay(50);

                        List<byte> readData = new List<byte>();

                        if (await port.WriteAsync(receiveWeightFromPrinterCommand) != receiveWeightFromPrinterCommand.Length)
                        {
                            throw new Exception("WriteAsync failed.");
                        }

                        UInt32 startReceiveWeight = (UInt32)Environment.TickCount;

                        ParseResult completionResult = ParseResult.Invalid;

                        while (true)
                        {
                            await Task.Delay(10);

                            if (500 < ((UInt32)Environment.TickCount - startReceiveWeight))
                            {
                                break;
                            }

                            IBuffer buffer = await port.ReadAsync();

                            if (buffer.Length == 0)
                            {
                                continue;
                            }

                            readData.AddRange(buffer.ToArray());

                            byte[] readBytes = readData.ToArray();

                            completionResult = parser.Parse(readBytes.AsBuffer());

                            if (completionResult == ParseResult.Success)
                            {
                                result = Result.Success;
                                isScaleDataReceived = true;
                                break;
                            }
                            else if (completionResult == ParseResult.Failure)
                            {
                                break;
                            }

                        }

                        if (500 < ((UInt32)Environment.TickCount - startRequestWeight))
                        {
                            break;
                        }

                    }

                    if (1000 < ((UInt32)Environment.TickCount - start))
                    {
                        result = Result.ErrorReadPort;
                        break;
                    }

                }

            }
            catch (Exception)
            {
            }

            return result;

        }

        public static string GetCommunicationResultMessage(Result result)
        {
            string message = null;

            switch (result)
            {
                case ScaleCommunication.Result.Success:
                    message = "Success!";
                    break;
                case ScaleCommunication.Result.ErrorOpenPort:
                    message = "Fail to openPort";
                    break;
                case ScaleCommunication.Result.ErrorBeginCheckedBlock:
                    message = "Printer is offline (beginCheckedBlock)";
                    break;
                case ScaleCommunication.Result.ErrorEndCheckedBlock:
                    message = "Printer is offline (endCheckedBlock)";
                    break;
                case ScaleCommunication.Result.ErrorReadPort:
                    message = "Read port error (readPort)";
                    break;
                case ScaleCommunication.Result.ErrorWritePort:
                    message = "Write port error (writePort)";
                    break;
                default:
                    message = "Unknown error";
                    break;
            }

            return message;
        }


    }
}
