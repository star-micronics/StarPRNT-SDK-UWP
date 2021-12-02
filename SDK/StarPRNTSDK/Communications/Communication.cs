using StarIO_Extension;
using StarIOPort;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage.Streams;

namespace StarPRNTSDK
{
    public class CommunicationResult
    {
        public Communication.Result Result { get; set; } = Communication.Result.ErrorUnknown;

        public int Code { get; set; } = StarResultCode.ErrorFailed;
    }

    public class SerialNumberResult
    {
        public string Title { get; set; }
        public string Message { get; set; }

        public CommunicationResult CommunicationResult { get; set; }
    }

    public class Communication
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

        public enum Connect
        {
            Connect,
            Disconnect,
        }

        public static async Task<CommunicationResult> SendCommands(IBuffer buffer, StarIOPort.Port port)
        {
            Result result = Result.ErrorUnknown;
            int code = StarResultCode.ErrorFailed;

            try
            {
                result = Result.ErrorOpenPort;

                if (port == null)
                {
                    return new CommunicationResult()
                    {
                        Result = result,
                        Code = code
                    };
                }

                Status status;

                result = Result.ErrorBeginCheckedBlock;

                status = await port.BeginCheckedBlockAsync();

                if (status.Offline == true)
                {
                    return new CommunicationResult()
                    {
                        Result = result,
                        Code = code
                    };
                }

                result = Result.ErrorWritePort;


                if (await port.WriteAsync(buffer) != buffer.Length)
                {
                    return new CommunicationResult()
                    {
                        Result = result,
                        Code = code
                    };
                }

                result = Result.ErrorEndCheckedBlock;

                status = await port.EndCheckedBlockAsync();

                if (status.Offline == true)
                {
                    return new CommunicationResult()
                    {
                        Result = result,
                        Code = code
                    };
                }

                result = Result.Success;
                code = StarResultCode.Succeeded;
            }
            catch (Exception ex)
            {
                code = ex.HResult;
            }

            return new CommunicationResult()
            {
                Result = result,
                Code = code
            };
        }

        public static async Task<CommunicationResult> SendCommandsDoNotCheckCondition(IBuffer buffer, StarIOPort.Port port)
        {
            Result result = Result.ErrorUnknown;
            int code = StarResultCode.ErrorFailed;

            try
            {
                result = Result.ErrorOpenPort;

                if (port == null)
                {
                    return new CommunicationResult()
                    {
                        Result = result,
                        Code = code
                    };
                }

                StarIOPort.Status status;

                result = Result.ErrorWritePort;
                status = await port.GetParsedStatusAsync();

                if (status.RawStatus.Length == 0)
                {
                    return new CommunicationResult()
                    {
                        Result = result,
                        Code = code
                    };
                }

                result = Result.ErrorWritePort;
                if (await port.WriteAsync(buffer) != buffer.Length)
                {
                    return new CommunicationResult()
                    {
                        Result = result,
                        Code = code
                    };
                }


                result = Result.ErrorWritePort;
                status = await port.GetParsedStatusAsync();

                if (status.RawStatus.Length == 0)
                {
                    return new CommunicationResult()
                    {
                        Result = result,
                        Code = code
                    };
                }

                result = Result.Success;
                code = StarResultCode.Succeeded;
            }
            catch (Exception ex)
            {
                code = ex.HResult;
            }

            return new CommunicationResult()
            {
                Result = result,
                Code = code
            };
        }


        public static async Task<CommunicationResult> SendCommands(IBuffer buffer, string portName, string portSettings, int timeout)
        {
            Result result = Result.ErrorUnknown;
            int code = StarResultCode.ErrorFailed;

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
                        return new CommunicationResult()
                        {
                            Result = result,
                            Code = code
                        };
                    }


                    result = Result.ErrorWritePort;
                    if (await port.WriteAsync(buffer) != buffer.Length)
                    {
                        return new CommunicationResult()
                        {
                            Result = result,
                            Code = code
                        };
                    }

                    result = Result.ErrorEndCheckedBlock;
                    status = await port.EndCheckedBlockAsync();

                    if (status.Offline == true)
                    {
                        return new CommunicationResult()
                        {
                            Result = result,
                            Code = code
                        };
                    }

                    result = Result.Success;
                    code = StarResultCode.Succeeded;
                }
            }
            catch (Exception ex)
            {
                code = ex.HResult;
            }

            return new CommunicationResult()
            {
                Result = result,
                Code = code
            };
        }

        public static async Task<CommunicationResult> SendCommandsMultiplePages(List<IBuffer> bufferList, string portName, string portSettings, int timeout, int holdPrintTimeout, Action<int> startAction, Action<int> finishAction)
        {
            Result result = Result.ErrorUnknown;
            int code = StarResultCode.ErrorFailed;

            try
            {
                using (StarIOPort.Port port = new StarIOPort.Port(timeout))
                {
                    result = Result.ErrorOpenPort;
                    await port.ConnectAsync(portName, portSettings);

                    for(int i = 0; i < bufferList.Count; i++)
                    {
                        IBuffer buffer = bufferList[i];

                        StarIOPort.Status status;

                        result = Result.ErrorBeginCheckedBlock;

                        port.HoldPrintTimeout = holdPrintTimeout;

                        status = await port.BeginCheckedBlockAsync();

                        if (status.Offline == true)
                        {
                            return new CommunicationResult()
                            {
                                Result = result,
                                Code = code
                            };
                        }

                        startAction?.Invoke(i);

                        result = Result.ErrorWritePort;
                        if (await port.WriteAsync(buffer) != buffer.Length)
                        {
                            return new CommunicationResult()
                            {
                                Result = result,
                                Code = code
                            };
                        }

                        result = Result.ErrorEndCheckedBlock;
                        status = await port.EndCheckedBlockAsync();

                        if (status.Offline == true)
                        {
                            return new CommunicationResult()
                            {
                                Result = result,
                                Code = code
                            };
                        }

                        finishAction.Invoke(i);

                        if (i == bufferList.Count - 1)
                        {
                            result = Result.Success;
                            code = StarResultCode.Succeeded;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                code = ex.HResult;
            }

            return new CommunicationResult()
            {
                Result = result,
                Code = code
            };
        }

        public static async Task<Dictionary<string, CommunicationResult>> SendCommandsForRedirection(IBuffer buffer, PrinterSettingsInfo[] printerSettingInfo, int timeout)
        {
            Dictionary<string, CommunicationResult> dicResult = new Dictionary<string, CommunicationResult>();


            foreach (var settingInfo in printerSettingInfo)
            {
                Result result = Result.ErrorUnknown;
                int code = StarResultCode.ErrorFailed;

                try
                {
                    using (StarIOPort.Port port = new StarIOPort.Port(timeout))
                    {
                        result = Result.ErrorOpenPort;
                        await port.ConnectAsync(settingInfo.PortName, settingInfo.PortSettings);

                        StarIOPort.Status status;

                        result = Result.ErrorBeginCheckedBlock;
                        status = await port.BeginCheckedBlockAsync();

                        if (status.Offline == true)
                        {
                            continue;
                        }


                        result = Result.ErrorWritePort;
                        if (await port.WriteAsync(buffer) != buffer.Length)
                        {
                            continue;
                        }

                        result = Result.ErrorEndCheckedBlock;
                        status = await port.EndCheckedBlockAsync();

                        if (status.Offline == true)
                        {
                            continue;
                        }

                        result = Result.Success;
                        code = StarResultCode.Succeeded;
                    }

                    if (result == Result.Success)
                    {
                        break;
                    }
                }
                catch (Exception ex)
                {
                    code = ex.HResult;
                }
                finally
                {
                    dicResult.Add(settingInfo.PortName, new CommunicationResult()
                    {
                        Result = result,
                        Code = code
                    });
                }
            }

            return dicResult;
        }

        public static async Task<CommunicationResult> SendCommandsDoNotCheckCondition(IBuffer buffer, string portName, string portSettings, int timeout)
        {
            Result result = Result.ErrorUnknown;
            int code = StarResultCode.ErrorFailed;

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
                        return new CommunicationResult()
                        {
                            Result = result,
                            Code = code
                        };
                    }


                    result = Result.ErrorWritePort;
                    if (await port.WriteAsync(buffer) != buffer.Length)
                    {
                        return new CommunicationResult()
                        {
                            Result = result,
                            Code = code
                        };
                    }

                    result = Result.ErrorWritePort;
                    status = await port.GetParsedStatusAsync();

                    if (status.RawStatus.Length == 0)
                    {
                        return new CommunicationResult()
                        {
                            Result = result,
                            Code = code
                        };
                    }

                    result = Result.Success;
                    code = StarResultCode.Succeeded;
                }
            }
            catch (Exception ex)
            {
                code = ex.HResult;
            }

            return new CommunicationResult()
            {
                Result = result,
                Code = code
            };
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
        public static async Task<FirmwareInformation> GetFirmwareVersion(string portName, string portSettings, int timeout)
        {
            FirmwareInformation firmwareinfo = null;

            try
            {
                using (StarIOPort.Port port = new StarIOPort.Port(timeout))
                {
                    await port.ConnectAsync(portName, portSettings);

                    firmwareinfo = await port.GetFirmwareInformationAsync();

                }
            }
            catch (Exception)
            {

            }

            return firmwareinfo;
        }

        public static async Task<CommunicationResult> ParseDoNotCheckCondition(IPeripheralCommandParser parser, string portName, string portSettings, int timeout)
        {
            Result result = Result.ErrorUnknown;
            int code = StarResultCode.ErrorFailed;

            List<byte> readData = new List<byte>();

            IBuffer buffer = parser.CreateSendCommands();

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
                        return new CommunicationResult()
                        {
                            Result = result,
                            Code = code
                        };
                    }

                    result = Result.ErrorWritePort;
                    if (await port.WriteAsync(buffer) != buffer.Length)
                    {
                        return new CommunicationResult()
                        {
                            Result = result,
                            Code = code
                        };
                    }

                    UInt32 startime = (UInt32)Environment.TickCount;

                    while (true)
                    {
                        await Task.Delay(10);

                        if (10000 < ((UInt32)Environment.TickCount - startime))
                        {
                            result = Result.ErrorReadPort;
                            break;
                        }

                        buffer = await port.ReadAsync();

                        if (buffer.Length == 0)
                        {
                            continue;
                        }

                        readData.AddRange(buffer.ToArray());

                        byte[] readBytes = readData.ToArray();

                        ParseResult completionResult = parser.Parse(readBytes.AsBuffer());

                        if (completionResult == ParseResult.Success)
                        {
                            result = Result.Success;
                            code = StarResultCode.Succeeded;
                            break;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                code = ex.HResult;
            }

            return new CommunicationResult()
            {
                Result = result,
                Code = code
            };

        }

        public static async Task<CommunicationResult> ParseDoNotCheckCondition(IPeripheralCommandParser parser, StarIOPort.Port port)
        {
            Result result = Result.ErrorUnknown;
            int code = StarResultCode.ErrorFailed;
            List<byte> readData = new List<byte>();

            IBuffer buffer = parser.CreateSendCommands();

            try
            {
                result = Result.ErrorOpenPort;

                if (port == null)
                {
                    return new CommunicationResult()
                    {
                        Result = result,
                        Code = code
                    };
                }

                StarIOPort.Status status;

                result = Result.ErrorWritePort;
                status = await port.GetParsedStatusAsync();

                if (status.RawStatus.Length == 0)
                {
                    return new CommunicationResult()
                    {
                        Result = result,
                        Code = code
                    };
                }


                result = Result.ErrorWritePort;
                if (await port.WriteAsync(buffer) != buffer.Length)
                {
                    return new CommunicationResult()
                    {
                        Result = result,
                        Code = code
                    };
                }

                UInt32 startime = (UInt32)Environment.TickCount;

                while (true)
                {
                    await Task.Delay(10);

                    if (10000 < ((UInt32)Environment.TickCount - startime))
                    {
                        result = Result.ErrorReadPort;
                        break;
                    }

                    buffer = await port.ReadAsync();

                    if (buffer.Length == 0)
                    {
                        continue;
                    }

                    readData.AddRange(buffer.ToArray());

                    byte[] readBytes = readData.ToArray();

                    ParseResult completionResult = parser.Parse(readBytes.AsBuffer());

                    if (completionResult == ParseResult.Success)
                    {
                        result = Result.Success;
                        code = StarResultCode.Succeeded;
                        break;
                    }
                }
            }
            catch (Exception ex)
            {
                code = ex.HResult;
            }

            return new CommunicationResult()
            {
                Result = result,
                Code = code
            };
        }

        public static async Task<SerialNumberResult> ConfirmSerialNumber(string portName, string portSettings, int timeout)
        {
            SerialNumberResult serialNumberResult = new SerialNumberResult();
            serialNumberResult.Title = null;
            serialNumberResult.Message = null;
            bool parseResult = false;
            string information = "";
            int amountRead = 0;
            IBuffer readBuffer;
            //            byte[] readBytes = new byte[1024];

            Result result = Result.ErrorUnknown;
            int code = StarResultCode.ErrorFailed;
            List<byte> readData = new List<byte>();

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
                        serialNumberResult.Title = "Printer Error";
                        serialNumberResult.Message = GetCommunicationResultMessage(serialNumberResult.CommunicationResult);

                        serialNumberResult.CommunicationResult = new CommunicationResult()
                        {
                            Result = result,
                            Code = code
                        };

                        return serialNumberResult;
                    }


                    result = Result.ErrorWritePort;
                    byte[] getInformationCommand = { 0x1b, 0x1d, (byte)')', (byte)'I', 0x01, 0x00, 49 };     // <ESC><GS>')''I'pLpHfn
                    if (await port.WriteAsync(getInformationCommand.AsBuffer()) != getInformationCommand.Length)
                    {
                        serialNumberResult.Title = "Printer Error";
                        serialNumberResult.Message = GetCommunicationResultMessage(serialNumberResult.CommunicationResult);

                        serialNumberResult.CommunicationResult = new CommunicationResult()
                        {
                            Result = result,
                            Code = code
                        };

                        return serialNumberResult;
                    }

                    UInt32 startime = (UInt32)Environment.TickCount;
                    while (true)
                    {
                        if (((UInt32)Environment.TickCount - startime) > 3000)//3000msec
                        {
                            result = Result.ErrorReadPort;
                            break;
                        }


                        result = Result.ErrorReadPort;

                        readBuffer = await port.ReadAsync();

                        if (readBuffer.Length == 0)
                        {
                            continue;
                        }

                        readData.AddRange(readBuffer.ToArray());

                        byte[] readBytes = readData.ToArray();

                        //System.Array.Copy(readBuffer.ToArray(), 0, readBytes, amountRead, (int)readBuffer.Length);
                        amountRead += (int)readBuffer.Length;


                        if (amountRead >= 2)
                        {
                            for (int i = 0; i < amountRead; i++)
                            {
                                if (readBytes[i + 0] == 0x0a && readBytes[i + 1] == 0x00)
                                {
                                    for (int j = 0; j < amountRead - 9; j++)
                                    {
                                        if (readBytes[j + 0] == 0x1b &&
                                            readBytes[j + 1] == 0x1d &&
                                            readBytes[j + 2] == (byte)')' &&
                                            readBytes[j + 3] == (byte)'I' &&
                                            // readBytes[j + 4] == 0x01      &&
                                            // readBytes[j + 5] == 0x00      &&
                                            readBytes[j + 6] == 0x31)
                                        {
                                            information = System.Text.Encoding.UTF8.GetString(readBytes, j + 7, (amountRead - 9));
                                            result = Result.Success;
                                            code = StarResultCode.Succeeded;
                                            parseResult = true;
                                            break;
                                        }
                                    }
                                    break;
                                }
                            }
                        }

                        if (parseResult)
                        {
                            break;
                        }

                    }


                    serialNumberResult.Title = "Printer Error";
                    serialNumberResult.Message = "Can not receive tag";

                    foreach (var item in information.Split(','))
                    {
                        if (item.StartsWith("PrSrN="))
                        {
                            serialNumberResult.Title = "Product Serial Number";
                            serialNumberResult.Message = item.Substring(6, item.Length - 6);
                        }
                    }

                }
            }
            catch (Exception ex)
            {
                code = ex.HResult;
                serialNumberResult.Title = "Printer Error";
                serialNumberResult.Message = GetCommunicationResultMessage(serialNumberResult.CommunicationResult);
            }

            serialNumberResult.CommunicationResult = new CommunicationResult()
            {
                Result = result,
                Code = code
            };

            return serialNumberResult;
        }

        public static string GetCommunicationResultMessage(CommunicationResult result)
        {
            StringBuilder builder = new StringBuilder();

            switch (result.Result)
            {
                case Result.Success:
                    builder.Append("Success!");
                    break;
                case Result.ErrorOpenPort:
                    builder.Append("Fail to openPort");
                    break;
                case Result.ErrorBeginCheckedBlock:
                    builder.Append("Printer is offline (BeginCheckedBlock)");
                    break;
                case Result.ErrorEndCheckedBlock:
                    builder.Append("Printer is offline (EndCheckedBlock)");
                    break;
                case Result.ErrorReadPort:
                    builder.Append("Read port error (ReadPort)");
                    break;
                case Result.ErrorWritePort:
                    builder.Append("Write port error (WritePort)");
                    break;
                default:
                case Result.ErrorUnknown:
                    builder.Append("Unknown error");
                    break;
            }

            if (result.Result != Result.Success)
            {
                builder.Append(Environment.NewLine);
                builder.Append(Environment.NewLine);
                builder.Append("Error code: ");
                builder.Append(result.Code.ToString());

                if (result.Code == StarResultCode.ErrorFailed)
                {
                    builder.Append(" (Failed)");
                }
                else if (result.Code == StarResultCode.ErrorInUse)
                {
                    builder.Append(" (In use)");
                }
                else if (result.Code == StarResultCode.ErrorPaperPresent)
                {
                    builder.Append(" (Paper present)");
                }
            }

            return builder.ToString();
        }

        public static string GetCommunicationResultMessage(Dictionary<string, CommunicationResult> result)
        {
            StringBuilder builder = new StringBuilder();
            int index = 0;

            foreach (KeyValuePair<string, CommunicationResult> item in result)
            {

                if (index == 0)
                {
                    builder.Append("[Destination]");
                }
                else
                {
                    builder.Append("[Backup]");
                }

                builder.Append(Environment.NewLine);
                builder.Append("Port Name: ");
                builder.Append(item.Key.ToString());
                builder.Append(" ->");
                builder.Append(Environment.NewLine);
                builder.Append(GetCommunicationResultMessage(item.Value));

                if(index == 0)
                {
                    builder.Append(Environment.NewLine);
                    builder.Append(Environment.NewLine);
                }

                index++;
            }

            return builder.ToString();
        }
    }
}
