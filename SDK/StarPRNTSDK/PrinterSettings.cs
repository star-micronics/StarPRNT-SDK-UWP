using StarIO_Extension;
using System;
using System.IO;
using System.Runtime.Serialization.Json;
using System.Threading.Tasks;
using Windows.ApplicationModel.Core;
using Windows.Storage;

namespace StarPRNTSDK
{
    [System.Runtime.Serialization.DataContract]
    public class PrinterSettingsInfo
    {
        [System.Runtime.Serialization.DataMember(Name = "PortName", IsRequired = true)]
        internal string PortName { get; set; }

        [System.Runtime.Serialization.DataMember(Name = "MacAddress", IsRequired = true)]
        internal string MacAddress { get; set; }

        [System.Runtime.Serialization.DataMember(Name = "ModelName", IsRequired = true)]
        internal string ModelName { get; set; }

        [System.Runtime.Serialization.DataMember(Name = "PortSettings", IsRequired = true)]
        internal string PortSettings { get; set; }

        [System.Runtime.Serialization.DataMember(Name = "PrinterEmulation", IsRequired = true)]
        internal Emulation PrinterEmulation { get; set; }

        [System.Runtime.Serialization.DataMember(Name = "CashDrawerOpenStatus", IsRequired = true)]
        internal bool CashDrawerOpenStatus { get; set; }

        [System.Runtime.Serialization.DataMember(Name = "ModelTitle", IsRequired = true)]
        internal string ModelTitle { get; set; }

        [System.Runtime.Serialization.DataMember(Name = "PaperSize", IsRequired = true)]
        internal string PaperSize { get; set; }
    }

    class PrinterSettings
    {
        public enum LANGUAGE : int
        {
            ENGLISH = 0,
            JAPANESE,
            FRENCH,
            PORTUGUESE,
            SPANISH,
            GERMAN,
            RUSSIAN,
            SIMPLIFIED_CHINESE,
            TRADITIONAL_CHINESE,
            CJK_UNIFIED_IDEOGRAPH
        }

        public enum PAPERSIZE : int
        {
            TWO_INCH = 384,
            THREE_INCH = 576,
            FOUR_INCH = 832,
            ESCPOS_THREE_INCH = 512,
            DOT_THREE_INCH = 210
        }

        public PrinterSettings()
        {

        }

        public string GetPortName(bool isMainPrinter)
        {
            object portName;

            if (isMainPrinter)
            {
                CoreApplication.Properties.TryGetValue("portName", out portName);
            }
            else
            {
                CoreApplication.Properties.TryGetValue("backupportName", out portName);
            }

            return portName != null ? portName.ToString() : null;

        }

        private void SetPortName(string portName, bool isMainPrinter)
        {
            object _portName;
            object _backupPrinterPortName;

            if (isMainPrinter)
            {
                if (CoreApplication.Properties.TryGetValue("portName", out _portName))
                {
                    CoreApplication.Properties.Remove("portName");
                }

                CoreApplication.Properties.Add("portName", portName);
            }
            else
            {
                if (CoreApplication.Properties.TryGetValue("backupportName", out _backupPrinterPortName))
                {
                    CoreApplication.Properties.Remove("backupportName");
                }

                CoreApplication.Properties.Add("backupportName", portName);
            }

        }

        public string GetModelName(bool isMainPrinter)
        {
            object modelName;

            if (isMainPrinter)
            {
                CoreApplication.Properties.TryGetValue("modelName", out modelName);
            }
            else
            {
                CoreApplication.Properties.TryGetValue("backupmodelName", out modelName);
            }

            return modelName != null ? modelName.ToString() : null;

        }

        private void SetModelName(string modelName, bool isMainPrinter)
        {
            object _modelName;
            object _backupmodelName;
            if (isMainPrinter)
            {
                if (CoreApplication.Properties.TryGetValue("modelName", out _modelName))
                {
                    CoreApplication.Properties.Remove("modelName");
                }

                CoreApplication.Properties.Add("modelName", modelName);
            }
            else
            {
                if (CoreApplication.Properties.TryGetValue("backupmodelName", out _backupmodelName))
                {
                    CoreApplication.Properties.Remove("backupmodelName");
                }

                CoreApplication.Properties.Add("backupmodelName", modelName);
            }

        }

        public string GetDevicelName(bool isMainPrinter)
        {
            object deviceName;

            if (isMainPrinter)
            {
                CoreApplication.Properties.TryGetValue("deviceName", out deviceName);
            }
            else
            {
                CoreApplication.Properties.TryGetValue("backupdeviceName", out deviceName);
            }

            return deviceName != null ? deviceName.ToString() : null;
        }

        private void SetDeviceName(string deviceName, bool isMainPrinter)
        {
            object _deviceName;
            object _backupdeviceName;
            if (isMainPrinter)
            {
                if (CoreApplication.Properties.TryGetValue("deviceName", out _deviceName))
                {
                    CoreApplication.Properties.Remove("deviceName");
                }

                CoreApplication.Properties.Add("deviceName", deviceName);
            }
            else
            {
                if (CoreApplication.Properties.TryGetValue("backupdeviceName", out _backupdeviceName))
                {
                    CoreApplication.Properties.Remove("backupdeviceName");
                }

                CoreApplication.Properties.Add("backupdeviceName", deviceName);
            }

        }

        public string GetMacAddress(bool isMainPrinter)
        {
            object macAddress;

            if (isMainPrinter)
            {
                CoreApplication.Properties.TryGetValue("macAddress", out macAddress);
            }
            else
            {
                CoreApplication.Properties.TryGetValue("backupmacAddress", out macAddress);
            }

            return macAddress != null ? macAddress.ToString() : null;
        }

        private void SetMacAddress(string macAddress, bool isMainPrinter)
        {
            object _macAddress;
            object _backupmacAddress;

            if (isMainPrinter)
            {
                if (CoreApplication.Properties.TryGetValue("macAddress", out _macAddress))
                {
                    CoreApplication.Properties.Remove("macAddress");
                }

                CoreApplication.Properties.Add("macAddress", macAddress);
            }
            else
            {
                if (CoreApplication.Properties.TryGetValue("backupmacAddress", out _backupmacAddress))
                {
                    CoreApplication.Properties.Remove("backupmacAddress");
                }

                CoreApplication.Properties.Add("backupmacAddress", macAddress);
            }

        }

        public string GetPortSettings(bool isMainPrinter)
        {
            object portSettings;

            if (isMainPrinter)
            {
                CoreApplication.Properties.TryGetValue("portSettings", out portSettings);
            }
            else
            {
                CoreApplication.Properties.TryGetValue("backupportSettings", out portSettings);
            }

            return portSettings != null ? portSettings.ToString() : null;
        }

        private void SetPortSettings(string portSettings, bool isMainPrinter)
        {
            object _portSettings;
            object _backupportSettings;

            if (isMainPrinter)
            {
                if (CoreApplication.Properties.TryGetValue("portSettings", out _portSettings))
                {
                    CoreApplication.Properties.Remove("portSettings");
                }

                CoreApplication.Properties.Add("portSettings", portSettings);
            }
            else
            {
                if (CoreApplication.Properties.TryGetValue("backupportSettings", out _backupportSettings))
                {
                    CoreApplication.Properties.Remove("backupportSettings");
                }

                CoreApplication.Properties.Add("backupportSettings", portSettings);
            }

        }

        public StarIO_Extension.Emulation GetEmulation(bool isMainPrinter)
        {
            object emulation;

            if (isMainPrinter)
            {
                CoreApplication.Properties.TryGetValue("emulation", out emulation);
            }
            else
            {
                CoreApplication.Properties.TryGetValue("backupemulation", out emulation);
            }

            return emulation != null ? (StarIO_Extension.Emulation)emulation : StarIO_Extension.Emulation.None;
        }

        private void SetEmulation(StarIO_Extension.Emulation emulation, bool isMainPrinter)
        {
            object _emulation;
            object _backupemulation;

            if (isMainPrinter)
            {
                if (CoreApplication.Properties.TryGetValue("emulation", out _emulation))
                {
                    CoreApplication.Properties.Remove("emulation");
                }

                CoreApplication.Properties.Add("emulation", emulation);
            }
            else
            {
                if (CoreApplication.Properties.TryGetValue("backupemulation", out _backupemulation))
                {
                    CoreApplication.Properties.Remove("backupemulation");
                }

                CoreApplication.Properties.Add("backupemulation", emulation);
            }


        }

        public string GetModelTitle(bool isMainPrinter)
        {
            object modelTitle;

            if (isMainPrinter)
            {
                CoreApplication.Properties.TryGetValue("modelTitle", out modelTitle);
            }
            else
            {
                CoreApplication.Properties.TryGetValue("backupmodelTitle", out modelTitle);
            }

            return modelTitle != null ? modelTitle.ToString() : null;
        }

        private void SetModelTitle(string modelTitle, bool isMainPrinter)
        {
            object _modelTitle;
            object _backupmodelTitle;

            if (isMainPrinter)
            {
                if (CoreApplication.Properties.TryGetValue("modelTitle", out _modelTitle))
                {
                    CoreApplication.Properties.Remove("modelTitle");
                }

                CoreApplication.Properties.Add("modelTitle", modelTitle);
            }
            else
            {
                if (CoreApplication.Properties.TryGetValue("backupmodelTitle", out _backupmodelTitle))
                {
                    CoreApplication.Properties.Remove("backupmodelTitle");
                }

                CoreApplication.Properties.Add("backupmodelTitle", modelTitle);
            }


        }

        public bool GetCashDrawerOpenActiveHigh(bool isMainPrinter)
        {
            object cashDrawerOpenActiveHigh;

            if (isMainPrinter)
            {
                CoreApplication.Properties.TryGetValue("cashDrawerOpenActiveHigh", out cashDrawerOpenActiveHigh);
            }
            else
            {
                CoreApplication.Properties.TryGetValue("backupcashDrawerOpenActiveHigh", out cashDrawerOpenActiveHigh);
            }

            return cashDrawerOpenActiveHigh != null ? (bool)cashDrawerOpenActiveHigh : false;
        }

        private void SetCashDrawerOpenActiveHigh(bool cashDrawerOpenActiveHigh, bool isMainPrinter)
        {
            object _cashDrawerOpenActiveHigh;
            object _backupcashDrawerOpenActiveHigh;

            if (isMainPrinter)
            {
                if (CoreApplication.Properties.TryGetValue("cashDrawerOpenActiveHigh", out _cashDrawerOpenActiveHigh))
                {
                    CoreApplication.Properties.Remove("cashDrawerOpenActiveHigh");
                }

                CoreApplication.Properties.Add("cashDrawerOpenActiveHigh", cashDrawerOpenActiveHigh);
            }
            else
            {
                if (CoreApplication.Properties.TryGetValue("backupcashDrawerOpenActiveHigh", out _backupcashDrawerOpenActiveHigh))
                {
                    CoreApplication.Properties.Remove("backupcashDrawerOpenActiveHigh");
                }

                CoreApplication.Properties.Add("backupcashDrawerOpenActiveHigh", cashDrawerOpenActiveHigh);
            }


        }


        public int GetLanguage()
        {
            object language;
            CoreApplication.Properties.TryGetValue("language", out language);

            return language != null ? (int)language : 0;
        }

        public void SetLanguage(int language)
        {
            object _language;
            if (CoreApplication.Properties.TryGetValue("language", out _language))
            {
                CoreApplication.Properties.Remove("language");
            }

            CoreApplication.Properties.Add("language", language);
        }

        public int GetPaperSize(bool isMainPrinter)
        {

            object paperSize;

            if (isMainPrinter)
            {
                CoreApplication.Properties.TryGetValue("paperSize", out paperSize);
            }
            else
            {
                CoreApplication.Properties.TryGetValue("backuppaperSize", out paperSize);
            }

            return paperSize != null ? (int)paperSize : 1;
        }

        public void SetPaperSize(int paperSize, bool isMainPrinter)
        {
            object _paperSize;
            object _backuppaperSize;

            if (isMainPrinter)
            {
                if (CoreApplication.Properties.TryGetValue("paperSize", out _paperSize))
                {
                    CoreApplication.Properties.Remove("paperSize");
                }

                CoreApplication.Properties.Add("paperSize", paperSize);
            }
            else
            {
                if (CoreApplication.Properties.TryGetValue("backuppaperSize", out _backuppaperSize))
                {
                    CoreApplication.Properties.Remove("backuppaperSize");
                }

                CoreApplication.Properties.Add("backuppaperSize", paperSize);
            }

        }

        public async Task WriteSetting(string modelName, string portName, string macAddress, string portSettings, Emulation emulation, bool cashDrawerOpenStatus, string modelTitle, string paperSize, bool isMainPrinter)
        {
            try
            {
                PrinterSettings printerSettings = new PrinterSettings();
                PrinterSettingsInfo[] settingdata = await printerSettings.LoadSettings();
                PrinterSettingsInfo[] items;
                if (settingdata == null)
                {
                    items = new PrinterSettingsInfo[2];
                }
                else
                {
                    items = settingdata;
                }


                DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(PrinterSettingsInfo[]));

                if (isMainPrinter)
                {
                    items[0] = new PrinterSettingsInfo();
                    items[0].PortName = portName;
                    items[0].MacAddress = macAddress;
                    items[0].ModelName = modelName;
                    items[0].PortSettings = portSettings;
                    items[0].PrinterEmulation = emulation;
                    items[0].CashDrawerOpenStatus = cashDrawerOpenStatus;
                    items[0].ModelTitle = modelTitle;
                    items[0].PaperSize = paperSize;


                    SetPortName(portName, true);
                    SetMacAddress(macAddress, true);
                    SetModelName(modelName, true);
                    SetPortSettings(portSettings, true);
                    SetEmulation(emulation, true);
                    SetCashDrawerOpenActiveHigh(cashDrawerOpenStatus, true);
                    SetModelTitle(modelTitle, true);
                    SetPaperSize(Convert.ToInt32(paperSize), true);
                }
                else
                {
                    //Load Main Printer Info
                    PrinterSettingsInfo[] settingsData = await printerSettings.LoadSettings();

                    //Set main printer info
                    items[0] = new PrinterSettingsInfo();
                    items[0].PortName             = settingsData[0].PortName;
                    items[0].MacAddress           = settingsData[0].MacAddress;
                    items[0].ModelName            = settingsData[0].ModelName;
                    items[0].PortSettings         = settingsData[0].PortSettings;
                    items[0].PrinterEmulation     = settingsData[0].PrinterEmulation;
                    items[0].CashDrawerOpenStatus = settingsData[0].CashDrawerOpenStatus;
                    items[0].ModelTitle           = settingsData[0].ModelTitle;
                    items[0].PaperSize            = settingsData[0].PaperSize;

                    SetPortName(settingsData[0].PortName, true);
                    SetMacAddress(settingsData[0].MacAddress, true);
                    SetModelName(settingsData[0].ModelName, true);
                    SetPortSettings(settingsData[0].PortSettings, true);
                    SetEmulation(settingsData[0].PrinterEmulation, true);
                    SetCashDrawerOpenActiveHigh(settingsData[0].CashDrawerOpenStatus, true);
                    SetModelTitle(settingsData[0].ModelTitle, true);
                    SetPaperSize(Convert.ToInt32(settingsData[0].PaperSize), true);


                    //Set backup printer info
                    items[1] = new PrinterSettingsInfo();
                    items[1].PortName             = portName;
                    items[1].MacAddress           = macAddress;
                    items[1].ModelName            = modelName;
                    items[1].PortSettings         = portSettings;
                    items[1].PrinterEmulation     = emulation;
                    items[1].CashDrawerOpenStatus = cashDrawerOpenStatus;
                    items[1].ModelTitle           = modelTitle;
                    items[1].PaperSize            = paperSize;

                    SetPortName(portName, false);
                    SetMacAddress(macAddress, false);
                    SetModelName(modelName, false);
                    SetPortSettings(portSettings, false);
                    SetEmulation(emulation, false);
                    SetCashDrawerOpenActiveHigh(cashDrawerOpenStatus, false);
                    SetModelTitle(modelTitle, false);
                    SetPaperSize(Convert.ToInt32(paperSize), false);
                }




                using (var ms = new MemoryStream())
                {
                    serializer.WriteObject(ms, items);

                    StorageFile file = await ApplicationData.Current.LocalFolder.CreateFileAsync("PrinterSettings.json", CreationCollisionOption.ReplaceExisting);

                    byte[] array = ms.ToArray();
                    await FileIO.WriteBytesAsync(file, array);
                }

            }
            catch (Exception)
            {

            }
        }


        public async Task<PrinterSettingsInfo[]> LoadSettings()
        {
            PrinterSettingsInfo[] info = null;
            try
            {
                byte[] byteArray = null;

                StorageFile storageFile = await ApplicationData.Current.LocalFolder.GetFileAsync("PrinterSettings.json");

                using (Stream stream = await storageFile.OpenStreamForReadAsync())
                using (BinaryReader binaryReader = new BinaryReader(stream))
                {
                    int count = (int)binaryReader.BaseStream.Length;
                    byteArray = binaryReader.ReadBytes(count);
                }

                DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(PrinterSettingsInfo[]));

                using (System.IO.MemoryStream stream = new System.IO.MemoryStream(byteArray))
                {
                    info = (PrinterSettingsInfo[])serializer.ReadObject(stream);

                    for (int i = 0; i < info.Length; i++)
                    {
                        bool IsMainPrinter = false;
                        if (i == 0)
                        {
                            IsMainPrinter = true;
                        }

                        if (info[i] == null)
                        {
                            continue;
                        }
                        if (info[i].PortName != null)     SetPortName(info[i].PortName, IsMainPrinter);
                        if (info[i].MacAddress != null)   SetMacAddress(info[i].MacAddress, IsMainPrinter);
                        if (info[i].ModelName != null)    SetModelName(info[i].ModelName, IsMainPrinter);
                        if (info[i].PortSettings != null) SetPortSettings(info[i].PortSettings, IsMainPrinter);
                                                          SetEmulation(info[i].PrinterEmulation, IsMainPrinter);
                                                          SetCashDrawerOpenActiveHigh(info[i].CashDrawerOpenStatus, IsMainPrinter);
                        if (info[i].ModelTitle != null)   SetModelTitle(info[i].ModelTitle, IsMainPrinter);
                        if (info[i].PaperSize != null)    SetPaperSize(Convert.ToInt32(info[i].PaperSize), IsMainPrinter);



                        if (i == 0)//Main Printer info
                        {
                            SelectedPrinter selectedPrinter = new SelectedPrinter(info[i].ModelName, info[i].PortName, info[i].MacAddress, true);
                        }
                        else
                        {
                            SelectedPrinter selectedPrinter = new SelectedPrinter(info[i].ModelName, info[i].PortName, info[i].MacAddress, false);
                        }


                    }


                }

                if (info.Length == 0)
                {
                    return null;
                }

                return info;

            }
            catch (Exception)
            {
                return info;
            }

        }
    }
}
