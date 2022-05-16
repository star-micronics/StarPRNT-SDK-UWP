using StarIO_Extension;
using System;
using System.Collections.Generic;
using System.Linq;

namespace StarPRNTSDK
{

    public struct PrinterInfo
    {
        public Emulation Emulation { get; set; }

        public string[] DeviceID { get; set; }

        public string[] BTDeviceNamePrefix { get; set; }

        public string DefaultPortSettings { get; set; }

        public string PaperSize { get; set; }

        public bool ChangeCashDrawerPolarityIsEnabled { get; set; }

        public string SimpleModelName { get; set; }

        public bool TextReceiptIsEnabled { get; set; }

        public bool UTF8IsEnabled { get; set; }

        public bool RasterReceiptIsEnabled { get; set; }

        public bool CJKIsEnabled { get; set; }

        public bool BlackMarkIsEnabled { get; set; }

        public bool BlackMarkDetectionIsEnabled { get; set; }

        public bool PageModeIsEnabled { get; set; }

        public bool PaperPresentStatusIsEnabled { get; set; }

        public bool CashDrawerIsEnabled { get; set; }

        public bool BarcodeReaderIsEnabled { get; set; }

        public bool CustomerDisplayIsEnabled { get; set; }

        public bool MelodySpeakerIsEnabled { get; set; }

        public int SoundNumberDefault { get; set; }

        public int SoundVolumeDefault { get; set; }

        public int SoundVolumeMin { get; set; }

        public int SoundVolumeMax { get; set; }

        public bool ProductSerialNumberIsEnabled { get; set; }
    }

    public enum PrinterModel : int
    {
        MCP20 = 0,
        MCP30,
        POP10,
        FVP10,
        TSP100,
        TSP100IV,
        TSP650II,
        TSP700II,
        TSP800II,
        SP700,
        S210i,
        S220i,
        S230i,
        T300i,
        T400i,
        L200,
        L300,
        BSC10,
        S210i_StarPRNT,
        S220i_StarPRNT,
        S230i_StarPRNT,
        T300i_StarPRNT,
        T400i_StarPRNT,
        Unknown
    }

    public class ModelDictionary
    {
        public static Dictionary<PrinterModel, PrinterInfo> ModelInformationDictionary
        {
            get
            {
                return new Dictionary<PrinterModel, PrinterInfo>()
                {
                    { PrinterModel.L200,
                      new PrinterInfo()
                      {
                          Emulation = Emulation.StarPRNT,
                          DeviceID = new string[] {  "SM-L200 (STAR_T-001)" },
                          BTDeviceNamePrefix = new string[] {  "STAR L200-", "STAR L204-" },
                          DefaultPortSettings = "portable",
                          PaperSize = "384",
                          ChangeCashDrawerPolarityIsEnabled = false,
                          SimpleModelName = "SM-L200",
                          TextReceiptIsEnabled = true,
                          UTF8IsEnabled = false,
                          RasterReceiptIsEnabled = true,
                          CJKIsEnabled = false,
                          BlackMarkIsEnabled = true,
                          BlackMarkDetectionIsEnabled = true,
                          PageModeIsEnabled = true,
                          PaperPresentStatusIsEnabled = false,
                          CashDrawerIsEnabled = false,
                          BarcodeReaderIsEnabled = false,
                          CustomerDisplayIsEnabled = false,
                          MelodySpeakerIsEnabled = false,
                          SoundNumberDefault = -1,
                          SoundVolumeDefault = -1,
                          SoundVolumeMax = -1,
                          SoundVolumeMin = -1,
                          ProductSerialNumberIsEnabled = false
                      }
                    },

                    { PrinterModel.L300,
                      new PrinterInfo()
                      {
                          Emulation = Emulation.StarPRNT,
                          DeviceID = new string[] {  "SM-L300 (STAR_T-001)" },
                          BTDeviceNamePrefix = new string[] {  "STAR L300-", "STAR L304-" },
                          DefaultPortSettings = "portable",
                          PaperSize = "576",
                          ChangeCashDrawerPolarityIsEnabled = false,
                          SimpleModelName = "SM-L300",
                          TextReceiptIsEnabled = true,
                          UTF8IsEnabled = false,
                          RasterReceiptIsEnabled = true,
                          CJKIsEnabled = false,
                          BlackMarkIsEnabled = true,
                          BlackMarkDetectionIsEnabled = false,
                          PageModeIsEnabled = true,
                          PaperPresentStatusIsEnabled = false,
                          CashDrawerIsEnabled = false,
                          BarcodeReaderIsEnabled = false,
                          CustomerDisplayIsEnabled = false,
                          MelodySpeakerIsEnabled = false,
                          SoundNumberDefault = -1,
                          SoundVolumeDefault = -1,
                          SoundVolumeMax = -1,
                          SoundVolumeMin = -1,
                          ProductSerialNumberIsEnabled = false
                      }
                    },

                    { PrinterModel.S210i,
                      new PrinterInfo()
                      {
                          Emulation = Emulation.EscPosMobile,
                          DeviceID = (string[])Enumerable.Empty<string>(),
                          BTDeviceNamePrefix = (string[])Enumerable.Empty<string>(),
                          DefaultPortSettings = "mini",
                          PaperSize = "384",
                          ChangeCashDrawerPolarityIsEnabled = false,
                          SimpleModelName = "SM-S210i",
                          TextReceiptIsEnabled = true,
                          UTF8IsEnabled = false,
                          RasterReceiptIsEnabled = true,
                          CJKIsEnabled = false,
                          BlackMarkIsEnabled = false,
                          BlackMarkDetectionIsEnabled = false,
                          PageModeIsEnabled = true,
                          PaperPresentStatusIsEnabled = false,
                          CashDrawerIsEnabled = false,
                          BarcodeReaderIsEnabled = false,
                          CustomerDisplayIsEnabled = false,
                          MelodySpeakerIsEnabled = false,
                          SoundNumberDefault = -1,
                          SoundVolumeDefault = -1,
                          SoundVolumeMax = -1,
                          SoundVolumeMin = -1,
                          ProductSerialNumberIsEnabled = false
                      }
                    },

                    { PrinterModel.S220i,
                      new PrinterInfo()
                      {
                          Emulation = Emulation.EscPosMobile,
                          DeviceID = (string[])Enumerable.Empty<string>(),
                          BTDeviceNamePrefix = (string[])Enumerable.Empty<string>(),
                          DefaultPortSettings = "mini",
                          PaperSize = "384",
                          ChangeCashDrawerPolarityIsEnabled = false,
                          SimpleModelName = "SM-S220i",
                          TextReceiptIsEnabled = true,
                          UTF8IsEnabled = false,
                          RasterReceiptIsEnabled = true,
                          CJKIsEnabled = false,
                          BlackMarkIsEnabled = false,
                          BlackMarkDetectionIsEnabled = false,
                          PageModeIsEnabled = true,
                          PaperPresentStatusIsEnabled = false,
                          CashDrawerIsEnabled = false,
                          BarcodeReaderIsEnabled = false,
                          CustomerDisplayIsEnabled = false,
                          MelodySpeakerIsEnabled = false,
                          SoundNumberDefault = -1,
                          SoundVolumeDefault = -1,
                          SoundVolumeMax = -1,
                          SoundVolumeMin = -1,
                          ProductSerialNumberIsEnabled = false
                      }
                    },

                    { PrinterModel.S230i,
                      new PrinterInfo()
                      {
                          Emulation = Emulation.EscPosMobile,
                          DeviceID = new string[] {  "SM-S230i (STAR_T-001)" },
                          BTDeviceNamePrefix = (string[])Enumerable.Empty<string>(),
                          DefaultPortSettings = "mini",
                          PaperSize = "384",
                          ChangeCashDrawerPolarityIsEnabled = false,
                          SimpleModelName = "SM-S230i",
                          TextReceiptIsEnabled = true,
                          UTF8IsEnabled = false,
                          RasterReceiptIsEnabled = true,
                          CJKIsEnabled = false,
                          BlackMarkIsEnabled = false,
                          BlackMarkDetectionIsEnabled = false,
                          PageModeIsEnabled = true,
                          PaperPresentStatusIsEnabled = false,
                          CashDrawerIsEnabled = false,
                          BarcodeReaderIsEnabled = false,
                          CustomerDisplayIsEnabled = false,
                          MelodySpeakerIsEnabled = false,
                          SoundNumberDefault = -1,
                          SoundVolumeDefault = -1,
                          SoundVolumeMax = -1,
                          SoundVolumeMin = -1,
                          ProductSerialNumberIsEnabled = false
                      }
                    },

                    { PrinterModel.T300i,
                      new PrinterInfo()
                      {
                          Emulation = Emulation.EscPosMobile,
                          DeviceID = (string[])Enumerable.Empty<string>(),
                          BTDeviceNamePrefix = (string[])Enumerable.Empty<string>(),
                          DefaultPortSettings = "mini",
                          PaperSize = "576",
                          ChangeCashDrawerPolarityIsEnabled = false,
                          SimpleModelName = "SM-T300i",
                          TextReceiptIsEnabled = true,
                          UTF8IsEnabled = false,
                          RasterReceiptIsEnabled = true,
                          CJKIsEnabled = false,
                          BlackMarkIsEnabled = true,
                          BlackMarkDetectionIsEnabled = false,
                          PageModeIsEnabled = true,
                          PaperPresentStatusIsEnabled = false,
                          CashDrawerIsEnabled = false,
                          BarcodeReaderIsEnabled = false,
                          CustomerDisplayIsEnabled = false,
                          MelodySpeakerIsEnabled = false,
                          SoundNumberDefault = -1,
                          SoundVolumeDefault = -1,
                          SoundVolumeMax = -1,
                          SoundVolumeMin = -1,
                          ProductSerialNumberIsEnabled = false
                      }
                    },

                    { PrinterModel.T400i,
                      new PrinterInfo()
                      {
                          Emulation = Emulation.EscPosMobile,
                          DeviceID = (string[])Enumerable.Empty<string>(),
                          BTDeviceNamePrefix = (string[])Enumerable.Empty<string>(),
                          DefaultPortSettings = "mini",
                          PaperSize = "832",
                          ChangeCashDrawerPolarityIsEnabled = false,
                          SimpleModelName = "SM-T400i",
                          TextReceiptIsEnabled = true,
                          UTF8IsEnabled = false,
                          RasterReceiptIsEnabled = true,
                          CJKIsEnabled = false,
                          BlackMarkIsEnabled = true,
                          BlackMarkDetectionIsEnabled = false,
                          PageModeIsEnabled = true,
                          PaperPresentStatusIsEnabled = false,
                          CashDrawerIsEnabled = false,
                          BarcodeReaderIsEnabled = false,
                          CustomerDisplayIsEnabled = false,
                          MelodySpeakerIsEnabled = false,
                          SoundNumberDefault = -1,
                          SoundVolumeDefault = -1,
                          SoundVolumeMax = -1,
                          SoundVolumeMin = -1,
                          ProductSerialNumberIsEnabled = false
                      }
                    },

                    { PrinterModel.S210i_StarPRNT,
                      new PrinterInfo()
                      {
                          Emulation = Emulation.StarPRNT,
                          DeviceID = (string[])Enumerable.Empty<string>(),
                          BTDeviceNamePrefix = (string[])Enumerable.Empty<string>(),
                          DefaultPortSettings = "portable",
                          PaperSize = "384",
                          ChangeCashDrawerPolarityIsEnabled = false,
                          SimpleModelName = "SM-S210i StarPRNT",
                          TextReceiptIsEnabled = true,
                          UTF8IsEnabled = false,
                          RasterReceiptIsEnabled = true,
                          CJKIsEnabled = false,
                          BlackMarkIsEnabled = false,
                          BlackMarkDetectionIsEnabled = false,
                          PageModeIsEnabled = true,
                          PaperPresentStatusIsEnabled = false,
                          CashDrawerIsEnabled = false,
                          BarcodeReaderIsEnabled = false,
                          CustomerDisplayIsEnabled = false,
                          MelodySpeakerIsEnabled = false,
                          SoundNumberDefault = -1,
                          SoundVolumeDefault = -1,
                          SoundVolumeMax = -1,
                          SoundVolumeMin = -1,
                          ProductSerialNumberIsEnabled = false
                      }
                    },

                    { PrinterModel.S220i_StarPRNT,
                      new PrinterInfo()
                      {
                          Emulation = Emulation.StarPRNT,
                          DeviceID = (string[])Enumerable.Empty<string>(),
                          BTDeviceNamePrefix = (string[])Enumerable.Empty<string>(),
                          DefaultPortSettings = "portable",
                          PaperSize = "384",
                          ChangeCashDrawerPolarityIsEnabled = false,
                          SimpleModelName = "SM-S220i StarPRNT",
                          TextReceiptIsEnabled = true,
                          UTF8IsEnabled = false,
                          RasterReceiptIsEnabled = true,
                          CJKIsEnabled = false,
                          BlackMarkIsEnabled = false,
                          BlackMarkDetectionIsEnabled = false,
                          PageModeIsEnabled = true,
                          PaperPresentStatusIsEnabled = false,
                          CashDrawerIsEnabled = false,
                          BarcodeReaderIsEnabled = false,
                          CustomerDisplayIsEnabled = false,
                          MelodySpeakerIsEnabled = false,
                          SoundNumberDefault = -1,
                          SoundVolumeDefault = -1,
                          SoundVolumeMax = -1,
                          SoundVolumeMin = -1,
                          ProductSerialNumberIsEnabled = false
                      }
                    },

                    { PrinterModel.S230i_StarPRNT,
                      new PrinterInfo()
                      {
                          Emulation = Emulation.StarPRNT,
                          DeviceID = (string[])Enumerable.Empty<string>(),
                          BTDeviceNamePrefix = (string[])Enumerable.Empty<string>(),
                          DefaultPortSettings = "portable",
                          PaperSize = "384",
                          ChangeCashDrawerPolarityIsEnabled = false,
                          SimpleModelName = "SM-S230i StarPRNT",
                          TextReceiptIsEnabled = true,
                          UTF8IsEnabled = false,
                          RasterReceiptIsEnabled = true,
                          CJKIsEnabled = false,
                          BlackMarkIsEnabled = false,
                          BlackMarkDetectionIsEnabled = false,
                          PageModeIsEnabled = true,
                          PaperPresentStatusIsEnabled = false,
                          CashDrawerIsEnabled = false,
                          BarcodeReaderIsEnabled = false,
                          CustomerDisplayIsEnabled = false,
                          MelodySpeakerIsEnabled = false,
                          SoundNumberDefault = -1,
                          SoundVolumeDefault = -1,
                          SoundVolumeMax = -1,
                          SoundVolumeMin = -1,
                          ProductSerialNumberIsEnabled = false
                      }
                    },

                    { PrinterModel.T300i_StarPRNT,
                      new PrinterInfo()
                      {
                          Emulation = Emulation.StarPRNT,
                          DeviceID = (string[])Enumerable.Empty<string>(),
                          BTDeviceNamePrefix = (string[])Enumerable.Empty<string>(),
                          DefaultPortSettings = "portable",
                          PaperSize = "576",
                          ChangeCashDrawerPolarityIsEnabled = false,
                          SimpleModelName = "SM-T300i StarPRNT",
                          TextReceiptIsEnabled = true,
                          UTF8IsEnabled = false,
                          RasterReceiptIsEnabled = true,
                          CJKIsEnabled = false,
                          BlackMarkIsEnabled = true,
                          BlackMarkDetectionIsEnabled = true,
                          PageModeIsEnabled = true,
                          PaperPresentStatusIsEnabled = false,
                          CashDrawerIsEnabled = false,
                          BarcodeReaderIsEnabled = false,
                          CustomerDisplayIsEnabled = false,
                          MelodySpeakerIsEnabled = false,
                          SoundNumberDefault = -1,
                          SoundVolumeDefault = -1,
                          SoundVolumeMax = -1,
                          SoundVolumeMin = -1,
                          ProductSerialNumberIsEnabled = false
                      }
                    },

                    { PrinterModel.T400i_StarPRNT,
                      new PrinterInfo()
                      {
                          Emulation = Emulation.StarPRNT,
                          DeviceID = (string[])Enumerable.Empty<string>(),
                          BTDeviceNamePrefix = (string[])Enumerable.Empty<string>(),
                          DefaultPortSettings = "portable",
                          PaperSize = "832",
                          ChangeCashDrawerPolarityIsEnabled = false,
                          SimpleModelName = "SM-T400i StarPRNT",
                          TextReceiptIsEnabled = true,
                          UTF8IsEnabled = false,
                          RasterReceiptIsEnabled = true,
                          CJKIsEnabled = false,
                          BlackMarkIsEnabled = true,
                          BlackMarkDetectionIsEnabled = true,
                          PageModeIsEnabled = true,
                          PaperPresentStatusIsEnabled = false,
                          CashDrawerIsEnabled = false,
                          BarcodeReaderIsEnabled = false,
                          CustomerDisplayIsEnabled = false,
                          MelodySpeakerIsEnabled = false,
                          SoundNumberDefault = -1,
                          SoundVolumeDefault = -1,
                          SoundVolumeMax = -1,
                          SoundVolumeMin = -1,
                          ProductSerialNumberIsEnabled = false
                      }
                    },

                    { PrinterModel.BSC10,
                      new PrinterInfo()
                      {
                          Emulation = Emulation.EscPos,
                          DeviceID = new string[] {  "BSC10 (ESP-001)" },
                          BTDeviceNamePrefix = (string[])Enumerable.Empty<string>(),
                          DefaultPortSettings = "escpos",
                          PaperSize = "512",
                          ChangeCashDrawerPolarityIsEnabled = true,
                          SimpleModelName = "BSC10",
                          TextReceiptIsEnabled = true,
                          UTF8IsEnabled = false,
                          RasterReceiptIsEnabled = true,
                          CJKIsEnabled = false,
                          BlackMarkIsEnabled = false,
                          BlackMarkDetectionIsEnabled = false,
                          PageModeIsEnabled = true,
                          PaperPresentStatusIsEnabled = false,
                          CashDrawerIsEnabled = true,
                          BarcodeReaderIsEnabled = false,
                          CustomerDisplayIsEnabled = false,
                          MelodySpeakerIsEnabled = false,
                          SoundNumberDefault = -1,
                          SoundVolumeDefault = -1,
                          SoundVolumeMax = -1,
                          SoundVolumeMin = -1,
                          ProductSerialNumberIsEnabled = false
                      }
                    },

                    { PrinterModel.FVP10,
                      new PrinterInfo()
                      {
                          Emulation = Emulation.StarLine,
                          DeviceID = new string[] {"FVP10 (STR_T-001)" },
                          BTDeviceNamePrefix = (string[])Enumerable.Empty<string>(),
                          DefaultPortSettings = "",
                          PaperSize = "576",
                          ChangeCashDrawerPolarityIsEnabled = true,
                          SimpleModelName = "FVP10",
                          TextReceiptIsEnabled = true,
                          UTF8IsEnabled = true,
                          RasterReceiptIsEnabled = true,
                          CJKIsEnabled = false,
                          BlackMarkIsEnabled = true,
                          BlackMarkDetectionIsEnabled = true,
                          PageModeIsEnabled = true,
                          PaperPresentStatusIsEnabled = false,
                          CashDrawerIsEnabled = true,
                          BarcodeReaderIsEnabled = false,
                          CustomerDisplayIsEnabled = false,
                          MelodySpeakerIsEnabled = true,
                          SoundNumberDefault = 1,
                          SoundVolumeDefault = -1,
                          SoundVolumeMax = -1,
                          SoundVolumeMin = -1,
                          ProductSerialNumberIsEnabled = false
                      }
                    },

                    { PrinterModel.SP700,
                      new PrinterInfo()
                      {
                          Emulation = Emulation.StarDotImpact,
                          DeviceID = new string[] {  "SP717 (STR-001)", "SP747 (STR-001)", "SP712 (STR-001)", "SP742 (STR-001)"},
                          BTDeviceNamePrefix = (string[])Enumerable.Empty<string>(),
                          DefaultPortSettings = "",
                          PaperSize = "210",
                          ChangeCashDrawerPolarityIsEnabled = true,
                          SimpleModelName = "SP700",
                          TextReceiptIsEnabled = true,
                          UTF8IsEnabled = true,
                          RasterReceiptIsEnabled = false,
                          CJKIsEnabled = false,
                          BlackMarkIsEnabled = true,
                          BlackMarkDetectionIsEnabled = true,
                          PageModeIsEnabled = false,
                          PaperPresentStatusIsEnabled = false,
                          CashDrawerIsEnabled = true,
                          BarcodeReaderIsEnabled = false,
                          CustomerDisplayIsEnabled = false,
                          MelodySpeakerIsEnabled = false,
                          SoundNumberDefault = -1,
                          SoundVolumeDefault = -1,
                          SoundVolumeMax = -1,
                          SoundVolumeMin = -1,
                          ProductSerialNumberIsEnabled = false
                      }
                    },

                    { PrinterModel.TSP650II,
                      new PrinterInfo()
                      {
                          Emulation = Emulation.StarLine,
                          DeviceID = new string[] {  "TSP654 (STR_T-001)", "TSP651 (STR_T-001)" },
                          BTDeviceNamePrefix = (string[])Enumerable.Empty<string>(),
                          DefaultPortSettings = "",
                          PaperSize = "576",
                          ChangeCashDrawerPolarityIsEnabled = true,
                          SimpleModelName = "TSP650II",
                          TextReceiptIsEnabled = true,
                          UTF8IsEnabled = true,
                          RasterReceiptIsEnabled = true,
                          CJKIsEnabled = true,
                          BlackMarkIsEnabled = false,
                          BlackMarkDetectionIsEnabled = false,
                          PageModeIsEnabled = true,
                          PaperPresentStatusIsEnabled = true,
                          CashDrawerIsEnabled = true,
                          BarcodeReaderIsEnabled = false,
                          CustomerDisplayIsEnabled = false,
                          MelodySpeakerIsEnabled = false,
                          SoundNumberDefault = -1,
                          SoundVolumeDefault = -1,
                          SoundVolumeMax = -1,
                          SoundVolumeMin = -1,
                          ProductSerialNumberIsEnabled = false
                      }
                    },

                    { PrinterModel.TSP700II,
                      new PrinterInfo()
                      {
                          Emulation = Emulation.StarLine,
                          DeviceID = new string[] {  "TSP743II (STR_T-001)"},
                          BTDeviceNamePrefix = (string[])Enumerable.Empty<string>(),
                          DefaultPortSettings = "",
                          PaperSize = "576",
                          ChangeCashDrawerPolarityIsEnabled = true,
                          SimpleModelName = "TSP700II",
                          TextReceiptIsEnabled = true,
                          UTF8IsEnabled = true,
                          RasterReceiptIsEnabled = true,
                          CJKIsEnabled = false,
                          BlackMarkIsEnabled = true,
                          BlackMarkDetectionIsEnabled = true,
                          PageModeIsEnabled = true,
                          PaperPresentStatusIsEnabled = false,
                          CashDrawerIsEnabled = true,
                          BarcodeReaderIsEnabled = false,
                          CustomerDisplayIsEnabled = false,
                          MelodySpeakerIsEnabled = false,
                          SoundNumberDefault = -1,
                          SoundVolumeDefault = -1,
                          SoundVolumeMax = -1,
                          SoundVolumeMin = -1,
                          ProductSerialNumberIsEnabled = false
                      }
                    },

                    { PrinterModel.TSP800II,
                      new PrinterInfo()
                      {
                          Emulation = Emulation.StarLine,
                          DeviceID = new string[] { "TSP847II (STR_T-001)"},
                          BTDeviceNamePrefix = (string[])Enumerable.Empty<string>(),
                          DefaultPortSettings = "",
                          PaperSize = "832",
                          ChangeCashDrawerPolarityIsEnabled = true,
                          SimpleModelName = "TSP800II",
                          TextReceiptIsEnabled = true,
                          UTF8IsEnabled = true,
                          RasterReceiptIsEnabled = true,
                          CJKIsEnabled = false,
                          BlackMarkIsEnabled = true,
                          BlackMarkDetectionIsEnabled = true,
                          PageModeIsEnabled = false,
                          PaperPresentStatusIsEnabled = false,
                          CashDrawerIsEnabled = true,
                          BarcodeReaderIsEnabled = false,
                          CustomerDisplayIsEnabled = false,
                          MelodySpeakerIsEnabled = false,
                          SoundNumberDefault = -1,
                          SoundVolumeDefault = -1,
                          SoundVolumeMax = -1,
                          SoundVolumeMin = -1,
                          ProductSerialNumberIsEnabled = false
                      }
                    },

                    { PrinterModel.POP10,
                      new PrinterInfo()
                      {
                          Emulation = Emulation.StarPRNT,
                          DeviceID = new string[] {  "POP10 (STR-001)" },
                          BTDeviceNamePrefix = new string[] {  "STAR mPOP-" },
                          DefaultPortSettings = "",
                          PaperSize = "384",
                          ChangeCashDrawerPolarityIsEnabled = false,
                          SimpleModelName = "mPOP",
                          TextReceiptIsEnabled = true,
                          UTF8IsEnabled = true,
                          RasterReceiptIsEnabled = true,
                          CJKIsEnabled = false,
                          BlackMarkIsEnabled = false,
                          BlackMarkDetectionIsEnabled = false,
                          PageModeIsEnabled = true,
                          PaperPresentStatusIsEnabled = false,
                          CashDrawerIsEnabled = true,
                          BarcodeReaderIsEnabled = true,
                          CustomerDisplayIsEnabled = true,
                          MelodySpeakerIsEnabled = false,
                          SoundNumberDefault = -1,
                          SoundVolumeDefault = -1,
                          SoundVolumeMax = -1,
                          SoundVolumeMin = -1,
                          ProductSerialNumberIsEnabled = true
                      }
                    },

                    { PrinterModel.MCP20,
                      new PrinterInfo()
                      {
                          Emulation = Emulation.StarPRNT,
                          DeviceID = new string[] {  "MCP20 (STR-001)", "MCP21 (STR-001)" },
                          BTDeviceNamePrefix = new string[] {  "mC-Print2-" },
                          DefaultPortSettings = "",
                          PaperSize = "384",
                          ChangeCashDrawerPolarityIsEnabled = true,
                          SimpleModelName = "mC-Print2",
                          TextReceiptIsEnabled = true,
                          UTF8IsEnabled = true,
                          RasterReceiptIsEnabled = true,
                          CJKIsEnabled = true,
                          BlackMarkIsEnabled = false,
                          BlackMarkDetectionIsEnabled = false,
                          PageModeIsEnabled = true,
                          PaperPresentStatusIsEnabled = false,
                          CashDrawerIsEnabled = true,
                          BarcodeReaderIsEnabled = true,
                          CustomerDisplayIsEnabled = true,
                          MelodySpeakerIsEnabled = false,
                          SoundNumberDefault = -1,
                          SoundVolumeDefault = -1,
                          SoundVolumeMax = -1,
                          SoundVolumeMin = -1,
                          ProductSerialNumberIsEnabled = true
                      }
                    },

                    { PrinterModel.MCP30,
                      new PrinterInfo()
                      {
                          Emulation = Emulation.StarPRNT,
                          DeviceID = new string[] {  "MCP31 (STR-001)" },
                          BTDeviceNamePrefix = new string[] {  "mC-Print3-" },
                          DefaultPortSettings = "",
                          PaperSize = "576",
                          ChangeCashDrawerPolarityIsEnabled = true,
                          SimpleModelName = "mC-Print3",
                          TextReceiptIsEnabled = true,
                          UTF8IsEnabled = true,
                          RasterReceiptIsEnabled = true,
                          CJKIsEnabled = true,
                          BlackMarkIsEnabled = false,
                          BlackMarkDetectionIsEnabled = false,
                          PageModeIsEnabled = true,
                          PaperPresentStatusIsEnabled = false,
                          CashDrawerIsEnabled = true,
                          BarcodeReaderIsEnabled = true,
                          CustomerDisplayIsEnabled = true,
                          MelodySpeakerIsEnabled = true,
                          SoundNumberDefault = 0,
                          SoundVolumeDefault = 12,
                          SoundVolumeMax = 15,
                          SoundVolumeMin = 0,
                          ProductSerialNumberIsEnabled = true
                      }
                    },

                    { PrinterModel.TSP100,
                      new PrinterInfo()
                      {
                          Emulation = Emulation.StarGraphic,
                          DeviceID = new string[] {  "TSP143 (STR_T-001)", "TSP113 (STR_T-001)", "TSP143IIIW (STR_T-001)", "TSP143IIILAN (STR_T-001)" },
                          BTDeviceNamePrefix = new string[] {  "TSP100-"},
                          DefaultPortSettings = "",
                          PaperSize = "576",
                          ChangeCashDrawerPolarityIsEnabled = true,
                          SimpleModelName = "TSP100",
                          TextReceiptIsEnabled = false,
                          UTF8IsEnabled = false,
                          RasterReceiptIsEnabled = true,
                          CJKIsEnabled = false,
                          BlackMarkIsEnabled = false,
                          BlackMarkDetectionIsEnabled = false,
                          PageModeIsEnabled = false,
                          PaperPresentStatusIsEnabled = false,
                          CashDrawerIsEnabled = true,
                          BarcodeReaderIsEnabled = false,
                          CustomerDisplayIsEnabled = false,
                          MelodySpeakerIsEnabled = false,
                          SoundNumberDefault = -1,
                          SoundVolumeDefault = -1,
                          SoundVolumeMax = -1,
                          SoundVolumeMin = -1,
                          ProductSerialNumberIsEnabled = true
                      }
                    },

                    { PrinterModel.TSP100IV,
                      new PrinterInfo()
                      {
                          Emulation = Emulation.StarPRNT,
                          DeviceID = new string[] {  "TSP143IV (STR-001)" },
                          BTDeviceNamePrefix = (string[])Enumerable.Empty<string>(),
                          DefaultPortSettings = "",
                          PaperSize = "576",
                          ChangeCashDrawerPolarityIsEnabled = true,
                          SimpleModelName = "TSP100IV",
                          TextReceiptIsEnabled = true,
                          UTF8IsEnabled = true,
                          RasterReceiptIsEnabled = true,
                          CJKIsEnabled = true,
                          BlackMarkIsEnabled = false,
                          BlackMarkDetectionIsEnabled = false,
                          PageModeIsEnabled = true,
                          PaperPresentStatusIsEnabled = false,
                          CashDrawerIsEnabled = true,
                          BarcodeReaderIsEnabled = true,
                          CustomerDisplayIsEnabled = true,
                          MelodySpeakerIsEnabled = true,
                          SoundNumberDefault = 0,
                          SoundVolumeDefault = 12,
                          SoundVolumeMax = 15,
                          SoundVolumeMin = 0,
                          ProductSerialNumberIsEnabled = true
                      }
                    },

                     { PrinterModel.Unknown,
                      new PrinterInfo()
                      {
                          Emulation = Emulation.None,
                          DeviceID = (string[])Enumerable.Empty<string>(),
                          BTDeviceNamePrefix = (string[])Enumerable.Empty<string>(),
                          DefaultPortSettings = "",
                          PaperSize = "",
                          ChangeCashDrawerPolarityIsEnabled = false,
                          SimpleModelName = "",
                          TextReceiptIsEnabled = false,
                          UTF8IsEnabled = false,
                          RasterReceiptIsEnabled = false,
                          CJKIsEnabled = false,
                          BlackMarkIsEnabled = false,
                          BlackMarkDetectionIsEnabled = false,
                          PageModeIsEnabled = false,
                          PaperPresentStatusIsEnabled = false,
                          CashDrawerIsEnabled = false,
                          BarcodeReaderIsEnabled = false,
                          CustomerDisplayIsEnabled = false,
                          MelodySpeakerIsEnabled = false,
                          SoundNumberDefault = -1,
                          SoundVolumeDefault = -1,
                          SoundVolumeMax = -1,
                          SoundVolumeMin = -1,
                          ProductSerialNumberIsEnabled = false
                      }
                    },
                };
            }
        }


        public static Dictionary<int, PrinterModel> ModelTypeDictionary
        {
            get
            {
                return new Dictionary<int, PrinterModel>()
                {
                    { 0,  PrinterModel.MCP20 },
                    { 1,  PrinterModel.MCP30 },
                    { 2,  PrinterModel.POP10},
                    { 3,  PrinterModel.FVP10 },
                    { 4,  PrinterModel.TSP100 },
                    { 5,  PrinterModel.TSP100IV },
                    { 6,  PrinterModel.TSP650II},
                    { 7,  PrinterModel.TSP700II },
                    { 8,  PrinterModel.TSP800II },
                    { 9,  PrinterModel.SP700 },
                    { 10,  PrinterModel.S210i },
                    { 11, PrinterModel.S220i },
                    { 12, PrinterModel.S230i },
                    { 13, PrinterModel.T300i},
                    { 14, PrinterModel.T400i },
                    { 15, PrinterModel.L200 },
                    { 16, PrinterModel.L300 },
                    { 17, PrinterModel.BSC10 },
                    { 18, PrinterModel.S210i_StarPRNT },
                    { 19, PrinterModel.S220i_StarPRNT},
                    { 20, PrinterModel.S230i_StarPRNT},
                    { 21, PrinterModel.T300i_StarPRNT },
                    { 22, PrinterModel.T400i_StarPRNT },
                    { 23, PrinterModel.Unknown },
                };
            }
        }


        public PrinterInfo GetPrinterInfo(int index)
        {
            return ModelInformationDictionary[ModelTypeDictionary[index]];
        }

        public PrinterInfo GetPrinterInfo(string simpleModelName)
        {
            var size = Enum.GetNames(typeof(PrinterModel)).Length;

            PrinterInfo printerInfo = ModelInformationDictionary[PrinterModel.Unknown];

            for (int i = 0; i < size; i++)
            {
                printerInfo = ModelInformationDictionary[ModelTypeDictionary[i]];

                if (printerInfo.SimpleModelName.StartsWith(simpleModelName))
                {
                    return printerInfo;
                }
            }

            return printerInfo;
        }

        public PrinterInfo GetPrinterInfo(ProductList info)
        {
            var size = Enum.GetNames(typeof(PrinterModel)).Length;

            PrinterInfo printerInfo = ModelInformationDictionary[PrinterModel.Unknown];

            for (int i = 0; i < size; i++)
            {
                printerInfo = ModelInformationDictionary[ModelTypeDictionary[i]];

                //LAN
                foreach (var item in printerInfo.DeviceID)
                {
                    if (info.ModelName.StartsWith(item))
                    {
                        return printerInfo;
                    }
                }

                //BT
                foreach (var item in printerInfo.BTDeviceNamePrefix)
                {
                    if (info.ModelName.StartsWith(item))
                    {
                        return printerInfo;
                    }
                }
            }

            return printerInfo;
        }

        public PrinterModel GetModel(string simpleModelName)
        {
            var size = Enum.GetNames(typeof(PrinterModel)).Length;

            PrinterInfo printerInfo = ModelInformationDictionary[PrinterModel.Unknown];

            for (int i = 0; i < size; i++)
            {
                PrinterModel model = ModelTypeDictionary[i];
                printerInfo = ModelInformationDictionary[model];

                if (printerInfo.SimpleModelName.StartsWith(simpleModelName))
                {
                    return model;
                }
            }

            return PrinterModel.Unknown;
        }
    }

}
