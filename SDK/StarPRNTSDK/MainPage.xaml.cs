using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Windows.ApplicationModel;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Animation;
using Windows.UI.Xaml.Navigation;
using static StarPRNTSDK.SelectSettingContentDialog;

namespace StarPRNTSDK
{
    public sealed partial class MainPage : Page
    {
        private struct MainPageFunction
        {
            public Type navigateToClassType { get; }
            public SelectSettingContentDialog selectLanguageDialog { get; }

            public SelectSettingContentDialog selectPaperSizeDialog { get; }

            public MainPageFunction(Type navigateToClassType, SelectSettingContentDialog selectLanguageDialog, SelectSettingContentDialog selectPaperSizeDialog)
            {
                this.navigateToClassType = navigateToClassType;
                this.selectLanguageDialog = selectLanguageDialog;
                this.selectPaperSizeDialog = selectPaperSizeDialog;
            }

            public MainPageFunction(Type navigateToClassType, SelectSettingContentDialog selectLanguageDialog) : this(navigateToClassType, selectLanguageDialog, null) { }

            public MainPageFunction(Type navigateToClassType) : this(navigateToClassType, null, null) {}
        }

        private enum LanguageType
        {
            AllLanguage = 0,
            EnJp,
            AllLanguageWithCJK,
        }

        public MainPage()
        {
            this.InitializeComponent();

            this.AppTitleTextBlock.Text = "StarPRNT SDK Ver " + GetAppVersionString();

            this.Loaded += MainPage_Loaded;

            this.SetAllFunctionEvent();
        }

        async void MainPage_Loaded(object sender, RoutedEventArgs e)
        {
            var rootrame = Window.Current.Content as Frame;
            if (rootrame == null)
            {
                return;
            }
            Page currentPage;
            currentPage = rootrame.Content as Page;


            if (currentPage == null)
            {
                return;
            }

            PrinterSettings printerSettings = new PrinterSettings();
            PrinterSettingsInfo[] settingdata = await printerSettings.LoadSettings();

            this.InitializeUI(printerSettings);
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            (this.Resources["Storyboard1"] as Storyboard).Stop();
        }

        private void SetDisableFunctionsforLANPrinter()
        {
            this.SetFunctionIsEnabled(this.BluetoothSamplePanel, false);
        }

        private void SetDisableFunctionsForUnSelectedPrinter()
        {
            this.SetFunctionIsEnabled(this.PrinterSamplePanel,        false);
            this.SetFunctionIsEnabled(this.BlackMarkSamplePanel,      false);
            this.SetFunctionIsEnabled(this.BlackMarkPasteSamplePanel, false);
            this.SetFunctionIsEnabled(this.PageModeSamplePanel,       false);
            this.SetFunctionIsEnabled(this.PrintRedirectionSamplePanel, false);
            this.SetFunctionIsEnabled(this.HoldPrintSamplePanel, false);
            this.SetFunctionIsEnabled(this.CashDrawerSamplePanel,     false);
            this.SetFunctionIsEnabled(this.BarcodeReaderSamplePanel,  false);
            this.SetFunctionIsEnabled(this.DisplaySamplePanel,        false);
            this.SetFunctionIsEnabled(this.MelodySpeakerSamplePanel, false);
            this.SetFunctionIsEnabled(this.CombinationSamplePanel,    false);
            this.SetFunctionIsEnabled(this.APISamplePanel,            false);
            this.SetFunctionIsEnabled(this.SerialNumberPanel,         false);
            this.SetFunctionIsEnabled(this.DeviceStatusSamplePanel,   false);
            this.SetFunctionIsEnabled(this.BluetoothSamplePanel,      false);
        }

        private void InitializeUI(PrinterSettings printerSettings)
        {
            this.SetSelectedPrinterInfo(printerSettings);

            this.SetAllFunctionEnable();

            if (printerSettings.GetPortName(true) != null)
            {
                this.SetPrinterInfo(printerSettings.GetModelName(true), printerSettings.GetPortName(true), printerSettings.GetMacAddress(true));

                this.SetDisableFunctions(printerSettings.GetModelTitle(true), printerSettings.GetPortName(true), printerSettings.GetModelName(true));
            }
            else
            {
                this.SetDisableFunctionsForUnSelectedPrinter();
            }
        }

        private void SetDisableFunctions(string modelTitle,string portName, string modelName)
        {
            ModelDictionary modelDic = new ModelDictionary();
            PrinterInfo printerInfo = modelDic.GetPrinterInfo(modelTitle);

            this.SetFunctionIsEnabled(this.BlackMarkSamplePanel,      printerInfo.BlackMarkIsEnabled);
            this.SetFunctionIsEnabled(this.BlackMarkPasteSamplePanel, printerInfo.BlackMarkIsEnabled);
            this.SetFunctionIsEnabled(this.PageModeSamplePanel,       printerInfo.PageModeIsEnabled);
            this.SetFunctionIsEnabled(this.PrintRedirectionSamplePanel, true == (PrinterNameTextBlock.Text.ToUpper().Equals("UNSELECTED STATE")) ? false : true);
            this.SetFunctionIsEnabled(this.HoldPrintSamplePanel, printerInfo.PaperPresentStatusIsEnabled);
            this.SetFunctionIsEnabled(this.CashDrawerSamplePanel,     printerInfo.CashDrawerIsEnabled);
            this.SetFunctionIsEnabled(this.BarcodeReaderSamplePanel,  printerInfo.BarcodeReaderIsEnabled);
            this.SetFunctionIsEnabled(this.DisplaySamplePanel,        printerInfo.CustomerDisplayIsEnabled);
            this.SetFunctionIsEnabled(this.MelodySpeakerSamplePanel,  printerInfo.MelodySpeakerIsEnabled);
            this.SetFunctionIsEnabled(this.CombinationSamplePanel,    printerInfo.BarcodeReaderIsEnabled);
            this.SetFunctionIsEnabled(this.SerialNumberPanel,         true == (modelName.ToUpper().Equals("TSP143 (STR_T-001)") || modelName.ToUpper().Equals("TSP113 (STR_T-001)") )? false : printerInfo.ProductSerialNumberIsEnabled);
            this.SetFunctionIsEnabled(this.BluetoothSamplePanel,      portName.ToUpper().StartsWith("BT"));
        }

        private void SetSelectedPrinterInfo(PrinterSettings printerSettings)
        {
            if (printerSettings.GetPortName(true) != null)
            {
                this.PrinterNameTextBlock.Text = printerSettings.GetModelName(true);

                this.PortNameTextBlock.Text = printerSettings.GetPortName(true);

                this.MacAddressTextBlock.Text = printerSettings.GetMacAddress(true);

                this.PrinterNameTextBlock.Foreground = new SolidColorBrush(Colors.Blue);

            }
            else
            {
                (this.Resources["Storyboard1"] as Storyboard).Begin();

                this.PrinterNameTextBlock.Text = "Unselected State";
                this.PrinterNameTextBlock.Foreground = new SolidColorBrush(Colors.Red);

            }
        }

        private void SetPrinterInfo(string modelName, string portName, string macAddress)
        {
            this.PrinterNameTextBlock.Text = modelName;

            this.PortNameTextBlock.Text = portName;

            this.MacAddressTextBlock.Text = macAddress;

            this.PrinterNameTextBlock.Foreground = new SolidColorBrush(Colors.Blue);

        }

        private void SetAllFunctionEnable()
        {
            foreach (KeyValuePair<Panel, MainPageFunction> pair in this.GetAllFunctions())
            {
                Panel panel = pair.Key;

                this.SetFunctionIsEnabled(panel, true);
            }
        }

        private void SetFunctionIsEnabled(Panel panel, bool isEnabled)
        {
            double opacity = 1;

            if (!isEnabled)
            {
                opacity = 0.2;
            }

            panel.Opacity = opacity;

            panel.IsTapEnabled = isEnabled;
        }

        private void SetAllFunctionEvent()
        {
            foreach (KeyValuePair<Panel, MainPageFunction> pair in this.GetAllFunctions())
            {
                Panel panel = pair.Key;
                MainPageFunction function = pair.Value;

                panel.Tapped += (sender, e) => this.CallFunction(sender, e, function);
            }
        }

        private Dictionary<Panel, MainPageFunction> GetAllFunctions()
        {
            Dictionary<Panel, MainPageFunction> allFunctionsList = new Dictionary<Panel, MainPageFunction>();


            allFunctionsList.Add(this.PrinterNamePanel,
                                 new MainPageFunction(typeof(SearchPortSamplePage)));


            allFunctionsList.Add(this.PrinterSamplePanel,
                                 new MainPageFunction(typeof(PrinterSamplePage),
                                 this.CreateSelectLanguageDialog(LanguageType.AllLanguageWithCJK)));

            allFunctionsList.Add(this.BlackMarkSamplePanel,
                                 new MainPageFunction(typeof(BlackMarkSamplePage),
                                 this.CreateSelectLanguageDialog(LanguageType.EnJp)));

            allFunctionsList.Add(this.BlackMarkPasteSamplePanel,
                                 new MainPageFunction(typeof(BlackMarkPasteSamplePage),
                                 this.CreateSelectLanguageDialog(LanguageType.EnJp)));

            allFunctionsList.Add(this.PageModeSamplePanel,
                                 new MainPageFunction(typeof(PageModeSamplePage),
                                 this.CreateSelectLanguageDialog(LanguageType.EnJp)));

            allFunctionsList.Add(this.PrintRedirectionSamplePanel,
                                 new MainPageFunction(typeof(PrintRedirectionSamplePage),
                                 this.CreateSelectLanguageDialog(LanguageType.AllLanguage)));

            allFunctionsList.Add(this.HoldPrintSamplePanel,
                                 new MainPageFunction(typeof(HoldPrintSamplePage)));

            allFunctionsList.Add(this.CashDrawerSamplePanel,
                                 new MainPageFunction(typeof(CashDrawerSamplePage)));

            allFunctionsList.Add(this.BarcodeReaderSamplePanel,
                                 new MainPageFunction(typeof(BarcodeReaderSamplePage)));

            allFunctionsList.Add(this.DisplaySamplePanel,
                                 new MainPageFunction(typeof(DisplaySamplePage)));

            allFunctionsList.Add(this.MelodySpeakerSamplePanel,
                                 new MainPageFunction(typeof(MelodySpeakerSamplePage)));

            allFunctionsList.Add(this.CombinationSamplePanel,
                                 new MainPageFunction(typeof(CombinationSamplePage),
                                 this.CreateSelectLanguageDialog(LanguageType.AllLanguage)));

            allFunctionsList.Add(this.APISamplePanel,
                                 new MainPageFunction(typeof(APISamplePage)));

            allFunctionsList.Add(this.DeviceStatusSamplePanel,
                                 new MainPageFunction(typeof(DeviceStatusSamplePage)));

            allFunctionsList.Add(this.BluetoothSamplePanel,
                                 new MainPageFunction(typeof(BluetoothSettingsSample)));

            allFunctionsList.Add(this.SerialNumberPanel,
                                 new MainPageFunction(null));

            return allFunctionsList;
        }

        private SelectSettingContentDialog CreateSelectLanguageDialog(LanguageType languageType)
        {
            return new SelectSettingContentDialog("Select language.", this.CreateLanguageList(languageType));
        }

        private SelectSettingContentDialog CreateSelectPaperSizeDialog()
        {
            return new SelectSettingContentDialog("Select paper size.", new List<string>() { "2\" (384dots)", "3\" (576dots)", "4\" (832dots)" });
        }

        private async void CallFunction(object sender, TappedRoutedEventArgs e, MainPageFunction function)
        {
            PrinterSettings printerSettings   = new PrinterSettings();
            PrinterSettingsInfo[] settingdata   = await printerSettings.LoadSettings();

            Panel panel = (Panel)sender;

            if (!panel.IsTapEnabled)
            {
                return;
            }


            int selectedLanguageIndex = 0;
            if (function.selectLanguageDialog != null)
            {
                SelectSettingContentDialogResult selectLanguageResult = await this.ShowSelectSettingDialog(function.selectLanguageDialog);

                if(selectLanguageResult.isCanceled)
                {
                    return;
                }

                selectedLanguageIndex = selectLanguageResult.selectedIndex;
            }


            if (function.selectLanguageDialog != null)
            {
                this.ApplySetting(selectedLanguageIndex);
            }

            if(function.navigateToClassType != null)
            {
                this.Frame.Navigate(function.navigateToClassType, panel.Name);
            }

            if (panel.Name.Contains("SerialNumberPanel"))
            {
                CheckSerialNumber();
            }
        }

        private void ApplySetting(int selectedLanguageIndex)
        {
            PrinterSettings printerSettings = new PrinterSettings();

            printerSettings.SetLanguage(selectedLanguageIndex);
        }

        private async Task<bool> ShowSelectBoolContentDialog(SelectBoolContentDialog selectBoolContentDialog)
        {
            ContentDialogResult result = await selectBoolContentDialog.ShowAsync();

            if (result == ContentDialogResult.Primary)
            {
                return true;
            }

            return false;
        }


        private async Task<SelectSettingContentDialogResult> ShowSelectSettingDialog(SelectSettingContentDialog selectSettingDialog)
        {
            selectSettingDialog.SetRadioButtonCheckedIndex(0);
            ContentDialogResult result = await selectSettingDialog.ShowAsync();

            int selectedIndex = 0;
            bool isCanceled = true;

            if (result == ContentDialogResult.Primary)
            {
                isCanceled = false;

                selectedIndex = selectSettingDialog.GetSelectedIndex();
            }

            return new SelectSettingContentDialogResult(selectedIndex, isCanceled);
        }

        private List<string> CreateLanguageList(LanguageType languageType)
        {
            List<string> languageList = new List<string>();

            switch (languageType)
            {
                case LanguageType.EnJp:
                    languageList.Add("English");
                    languageList.Add("Japanese");
                    break;
                case LanguageType.AllLanguageWithCJK:
                    languageList.Add("English");
                    languageList.Add("Japanese");
                    languageList.Add("French");
                    languageList.Add("Portuguese");
                    languageList.Add("Spanish");
                    languageList.Add("German");
                    languageList.Add("Russian");
                    languageList.Add("Simplified Chinese");
                    languageList.Add("Traditional Chinese");
                    languageList.Add("UTF8 Multi language");
                    break;
                default:
                case LanguageType.AllLanguage:
                    languageList.Add("English");
                    languageList.Add("Japanese");
                    languageList.Add("French");
                    languageList.Add("Portuguese");
                    languageList.Add("Spanish");
                    languageList.Add("German");
                    languageList.Add("Russian");
                    languageList.Add("Simplified Chinese");
                    languageList.Add("Traditional Chinese");
                    break;
            }

            return languageList;
        }


        private async void CheckSerialNumber()
        {

            var progressRing = new ProgressRing();

            SerialNumberResult serialNumberResult = new SerialNumberResult();
            try
            {
                PrinterSettings printerSettings = new PrinterSettings();
                PrinterSettingsInfo[] settingsData = await printerSettings.LoadSettings();
                string portName = printerSettings.GetPortName(true);
                string portSettings = printerSettings.GetPortSettings(true);

                progressRing.Message = "Communicating...";
                progressRing.Show();

                serialNumberResult = await Communication.ConfirmSerialNumber(portName, portSettings, 30000);     // 30000mS!!!

            }
            catch (Exception)
            {

            }
            finally
            {
                progressRing.Hide();
            }

            Windows.UI.Popups.MessageDialog dialog = new Windows.UI.Popups.MessageDialog(serialNumberResult.Message, serialNumberResult.Title);

            await dialog.ShowAsync();
        }

        private async void TextBlock_Tapped(object sender, TappedRoutedEventArgs e)
        {
            string Version = "StarIO\t\t : " + StarIOPort.Util.GetStarIOVersion() + "\n" +
                             "StarIOExtension\t : " + StarIO_Extension.StarIoExt.GetStarIOExtensionVersion();

            await new SelectBoolContentDialog("Library Version", Version, "OK", "").ShowAsync();

        }

        private async void OpenSourceLicense_Tapped(object sender, TappedRoutedEventArgs e)
        {
            LisenceDialog dig = new LisenceDialog();
            await dig.ShowAsync();
        }

        private string GetAppVersionString()
        {
            var version = Package.Current.Id.Version;
            return string.Format("{0}.{1}.{2}", version.Major, version.Minor, version.Build);
        }
    }

}
