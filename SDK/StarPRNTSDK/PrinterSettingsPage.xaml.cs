using StarIO_Extension;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace StarPRNTSDK
{

    public sealed partial class PrinterSettingsPage : Page
    {
        public PrinterSettingsPage()
        {
            this.InitializeComponent();
            this.Loaded += PrinterSettingsPage_Loaded;

            this.SetComboBox();
        }

        void PrinterSettingsPage_Loaded(object sender, RoutedEventArgs e)
        {
            SelectedPrinter _selectedPrinter = new SelectedPrinter();

            PrinterSettings printerSettings = new PrinterSettings();
            string modelName = printerSettings.GetModelTitle(_selectedPrinter.IsSelectedMainPrinter);

            if (modelName != null)
            {
                for (int i = 0; i < PrinterModelComboBox.Items.Count; i++)
                {
                    string value = (string)PrinterModelComboBox.Items[i];

                    if (modelName.Equals(value))
                    {
                        this.PrinterModelComboBox.SelectedIndex = i;
                    }

                }
            }


            this.ReloadUI();

        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            if (e.Parameter != null)
            {
                SelectedPrinter parameter = e.Parameter as SelectedPrinter;

                this.SetSelectedPrinterParameter(parameter);
            }
            else
            {
                this.SetDefaultParameter();
            }
        }

        private void SetSelectedPrinterParameter(SelectedPrinter parameter)
        {
            if (parameter.IsSelectedMainPrinter)
            {
                this.PortNameTextBox.Text = parameter.PortName;
                this.MacAddress = parameter.MacAddress;
                this.ModelName = parameter.ModelName;
            }
            else
            {
                this.PortNameTextBox.Text = parameter.BackupPrinterPortName;
                this.MacAddress = parameter.BackupPrinterMacAddress;
                this.ModelName = parameter.BackupPrinterModelName;
            }
        }

        private void SetDefaultParameter()
        {
            PrinterSettings printerSettings = new PrinterSettings();

            SelectedPrinter selectedPrinter = new SelectedPrinter();

            string portName = printerSettings.GetPortName(selectedPrinter.IsSelectedMainPrinter);

            if (portName != null)
            {
                this.PortNameTextBox.Text = printerSettings.GetPortName(selectedPrinter.IsSelectedMainPrinter);
                this.PortSettingsTextBox.Text = printerSettings.GetPortSettings(selectedPrinter.IsSelectedMainPrinter);
            }
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            await this.ApplySetting();

            SelectedPrinter selectedPrinter = new SelectedPrinter();

            if (selectedPrinter.IsSelectedMainPrinter)
            {
                this.Frame.Navigate(typeof(MainPage), null);
            }
            else
            {
                this.Frame.Navigate(typeof(PrintRedirectionSamplePage), null);
            }
            
        }

        private void PrinterModelComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            this.ReloadUI();
        }

        private void SetComboBox()
        {
            this.SetPrinterModelComboBox();

            this.SetPaperSizeComboBox();
        }

        private void SetPrinterModelComboBox()
        {
            List<string> printerModelList = new List<string>();

            ModelDictionary modelDic = new ModelDictionary();

            var size =  Enum.GetNames(typeof(PrinterModel)).Length;

            for (int i = 0; i < size; i++)
            {
                string simpleModelName = modelDic.GetPrinterInfo(i).SimpleModelName;

                if (simpleModelName.Length != 0)
                {
                    printerModelList.Add(simpleModelName);
                }
            }
                      

            this.PrinterModelListSource.Source = printerModelList;

            this.PrinterModelComboBox.SelectedIndex = 0;

            this.PrinterModelComboBox.SelectionChanged += new SelectionChangedEventHandler(PrinterModelComboBox_SelectionChanged);
        }

        private void SetPaperSizeComboBox()
        {
            List<string> paperSizeList = new List<string>();

            paperSizeList.Add("2\"(384dots)");
            paperSizeList.Add("3\"(576dots)");
            paperSizeList.Add("4\"(832dots)");

            this.PaperSizeListSource.Source = paperSizeList;
        }

        private async Task ApplySetting()
        {
            string simpleModelName = "";
            string portName = "";
            string portSetting = "";
            Emulation emulation  = Emulation.StarLine;
            bool cashDrawerOpenStatus = false;
            string paperSize = "576";

            portName = this.GetPortName();
            portSetting = this.GetPortSetting();

            if (CashDrawerOpenStatusToggleSwitch != null)
            {
                cashDrawerOpenStatus = this.GetCashDrawerOpenStatus();
            }

            if (PrinterModelComboBox != null)
            {
                simpleModelName = this.GetSimpleModelName();

                if (simpleModelName != null)
                {
                    ModelDictionary modelDic = new ModelDictionary();
                    PrinterInfo printerInfo = modelDic.GetPrinterInfo(PrinterModelComboBox.SelectedIndex);

                    emulation = printerInfo.Emulation;
                    paperSize = printerInfo.PaperSize;
                }
            }

            if (this.ModelName == null)
            {
                this.ModelName = simpleModelName;
                this.MacAddress = "";
            }

            if (PaperSizeComboBox != null)
            {
                paperSize = this.GetPaperSize(emulation);
            }

            SelectedPrinter _selectedPrinter = new SelectedPrinter();

            PrinterSettings printerSettings = new PrinterSettings();
            await printerSettings.WriteSetting(this.ModelName, portName, this.MacAddress, portSetting, emulation, cashDrawerOpenStatus, simpleModelName, paperSize, _selectedPrinter.IsSelectedMainPrinter);

            SelectedPrinter selectedPrinter = new SelectedPrinter(this.ModelName, portName, this.MacAddress, _selectedPrinter.IsSelectedMainPrinter);
        }

        private string GetPortName()
        {
            return this.PortNameTextBox.Text;
        }

        private bool GetCashDrawerOpenStatus()
        {
            return CashDrawerOpenStatusToggleSwitch.IsOn;
        }

        private string GetSimpleModelName()
        {
            return (string)PrinterModelComboBox.SelectedItem;
        }

        private string GetPortSetting()
        {
            return this.PortSettingsTextBox.Text;
        }

        private string GetPaperSize(Emulation emulation)
        {
            string paperSize = (string)PaperSizeComboBox.SelectedItem;

            if (emulation == Emulation.EscPos)
            {
                return "512";
            }

            if (emulation == Emulation.StarDotImpact)
            {
                return "210";
            }

            paperSize = paperSize.Substring(3, 3);

            return paperSize;
        }

        public string MacAddress { get; set; }
        public string ModelName { get; set; }

        private void ReloadUI()
        {
            string selectedPrinterModel = this.GetSimpleModelName();

            if (selectedPrinterModel == null)
            {
                return;
            }

            ModelDictionary modelDic = new ModelDictionary();
            PrinterInfo printerInfo = modelDic.GetPrinterInfo(selectedPrinterModel);

            if (printerInfo.ChangeCashDrawerPolarityIsEnabled)
            {
                CashDrawerTextBlock.Visibility = Visibility.Visible;
                CashDrawerOpenStatusToggleSwitch.Visibility = Visibility.Visible;
            }
            else
            {
                CashDrawerTextBlock.Visibility = Visibility.Collapsed;
                CashDrawerOpenStatusToggleSwitch.Visibility = Visibility.Collapsed;
            }

            if (selectedPrinterModel.StartsWith("BSC10") || selectedPrinterModel.StartsWith("SP700"))
            {
                PaperSizeTextBox.Visibility = Visibility.Collapsed;
                PaperSizeComboBox.Visibility = Visibility.Collapsed;
            }
            else
            {
                PaperSizeTextBox.Visibility = Visibility.Visible;
                PaperSizeComboBox.Visibility = Visibility.Visible;
            }

            CashDrawerOpenStatusToggleSwitch.IsOn = true;

            this.PortSettingsTextBox.Text = printerInfo.DefaultPortSettings;


            SelectedPrinter selectedPrinter = new SelectedPrinter();

            if (selectedPrinter.IsSelectedMainPrinter)
            {
                PaperSizeTextBox.Visibility = Visibility.Visible;
                PaperSizeComboBox.Visibility = Visibility.Visible;

                switch (Convert.ToInt32(printerInfo.PaperSize))
                {
                    case (int)PrinterSettings.PAPERSIZE.TWO_INCH:
                        PaperSizeComboBox.SelectedIndex = 0;
                        break;
                    default:
                    case (int)PrinterSettings.PAPERSIZE.THREE_INCH:
                    case (int)PrinterSettings.PAPERSIZE.ESCPOS_THREE_INCH:
                    case (int)PrinterSettings.PAPERSIZE.DOT_THREE_INCH:
                        PaperSizeComboBox.SelectedIndex = 1;
                        break;
                    case (int)PrinterSettings.PAPERSIZE.FOUR_INCH:
                        PaperSizeComboBox.SelectedIndex = 2;
                        break;
                }
            }
            else
            {
                PaperSizeTextBox.Visibility = Visibility.Collapsed;
                PaperSizeComboBox.Visibility = Visibility.Collapsed;
            }


        }
    }
}
