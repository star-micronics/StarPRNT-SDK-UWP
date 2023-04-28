using StarIO_Extension;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Windows.UI.Popups;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Navigation;
using static StarPRNTSDK.SelectSettingContentDialog;

namespace StarPRNTSDK
{
    public sealed partial class SearchPortSamplePage : Page
    {
        private string selectedPanelName;
        private  bool isMainPrinter;

        public SearchPortSamplePage()
        {
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            selectedPanelName = e.Parameter as string;
            SelectedPrinter selectedPrinter = new SelectedPrinter();

            isMainPrinter = false;
            if (selectedPanelName.Equals("PrinterNamePanel"))
            {
                isMainPrinter = true;
                selectedPrinter.IsSelectedMainPrinter = true;
            }
            else
            {
                isMainPrinter = false;
                selectedPrinter.IsSelectedMainPrinter = false;
            }

            base.OnNavigatedTo(e);
        }

        private void ProductListBox_Tapped(object sender, TappedRoutedEventArgs e)
        {
            ProductList info = (ProductList)((ListView)ProductListBox).SelectedItem;

            ModelDictionary modelDic = new ModelDictionary();

            string simpleModelName = modelDic.GetPrinterInfo(info).SimpleModelName; ;


            if (simpleModelName.Length != 0)
            {
                this.CallProcessForKnownProduct(info);
            }
            else
            {
                this.CallProcessForUnknownProduct(info);
            }
        }

        private async void CallProcessForKnownProduct(ProductList info)
        {

            SelectedPrinter selectedPrinter = new SelectedPrinter(info.ModelName, info.PortName, info.MacAddress, isMainPrinter);


            ModelDictionary modelDic = new ModelDictionary();

            string portSettings       = modelDic.GetPrinterInfo(info).DefaultPortSettings;
            Emulation emulation       = modelDic.GetPrinterInfo(info).Emulation;
            bool cashDrawerIsEnabled  = modelDic.GetPrinterInfo(info).CashDrawerIsEnabled;
            bool cashDrawerOpenStatus = true;
            string paperSize          = modelDic.GetPrinterInfo(info).PaperSize;
            string simpleModelName    = modelDic.GetPrinterInfo(info).SimpleModelName;
            int paperSizeIndex = 0;
            int cashDrawerActiveHighIndex = 0;
            PrinterSettings printerSettings = new PrinterSettings();

            SelectBoolContentDialog confirmPrinterContentDialog = this.CreateConfirmPrinterContentDialog(simpleModelName);

            bool isTapYes = await this.ShowSelectBoolContentDialog(confirmPrinterContentDialog);


            if (isTapYes)
            {
                if ((simpleModelName.StartsWith("BSC10") || simpleModelName.StartsWith("SP700")) == false)
                {
                    if (isMainPrinter)
                    {
                        SelectSettingContentDialog selectPaperSizeDialog = CreateSelectPaperSizeDialog();
                        paperSizeIndex = await this.ShowSelectSettingContentDialog(selectPaperSizeDialog);
                        if (paperSizeIndex == -1)
                        {
                            return;
                        }
                    }
                }

                if (cashDrawerIsEnabled && this.ChangeCashDrawerPolarityIsEnabled(simpleModelName))
                {
                    SelectSettingContentDialog selectCashDrawerActiveHighContentDialog = CreateSelectCashDrawerActiveHighDialog();

                    cashDrawerActiveHighIndex = await this.ShowSelectSettingContentDialog(selectCashDrawerActiveHighContentDialog);
                    if (cashDrawerActiveHighIndex == -1)
                    {
                        return;
                    }
                    if (cashDrawerActiveHighIndex == 0)
                    {
                        //High when Open
                        cashDrawerOpenStatus = true;
                    }
                    else
                    {
                        //Low when Open
                        cashDrawerOpenStatus = false;
                    }

                }

                await printerSettings.WriteSetting(info.ModelName, info.PortName, info.MacAddress, portSettings, emulation, cashDrawerOpenStatus, simpleModelName, GetPaperSize(paperSizeIndex, emulation), isMainPrinter);

                this.Frame.GoBack();
            }
            else
            {
                this.Frame.Navigate(typeof(PrinterSettingsPage), selectedPrinter);
            }
        }

        private void CallProcessForUnknownProduct(ProductList info)
        {
            SelectedPrinter selectedPrinter = new SelectedPrinter(info.ModelName, info.PortName, info.MacAddress, isMainPrinter);

            this.Frame.Navigate(typeof(PrinterSettingsPage), selectedPrinter);
        }

        private SelectBoolContentDialog CreateConfirmPrinterContentDialog(string simpleModelName)
        {
            return new SelectBoolContentDialog("Confirm.", "Is your printer " + simpleModelName + "?", "YES", "NO");
        }

        private SelectBoolContentDialog CreateConfirmCashDrawerActiveHighContentDialog()
        {
            return new SelectBoolContentDialog("Confirm.", "Select CashDrawer Open Status.", "High when Open", "Low when Open");
        }

        private SelectSettingContentDialog CreateSelectCashDrawerActiveHighDialog()
        {
            return new SelectSettingContentDialog("Select CashDrawer Open Status.", new List<string>() { "High when Open", "Low when Open" });
        }

        private SelectSettingContentDialog CreateSelectPaperSizeDialog()
        {
            return new SelectSettingContentDialog("Select paper size.", new List<string>() { "2\" (384dots)", "3\" (576dots)", "4\" (832dots)" });
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
        private async Task<int> ShowSelectSettingContentDialog(SelectSettingContentDialog selectSettingContentDialog)
        {
            ContentDialogResult result = await selectSettingContentDialog.ShowAsync();

            int selectedIndex = 0;

            if (result == ContentDialogResult.Primary)
            {
                selectedIndex = selectSettingContentDialog.GetSelectedIndex();

            }
            else//cancel
            {
                selectedIndex = -1;
            }

            return selectedIndex;
        }

        private string GetPaperSize(int selectedPaperSizeIndex, Emulation emulation)
        {
            string paperSize = "576";

            if (emulation == Emulation.EscPos)
            {
                return "512";
            }

            if (emulation == Emulation.StarDotImpact)
            {
                return "210";
            }

            switch (selectedPaperSizeIndex)
            {
                default:
                case 0: paperSize = "384"; break;
                case 1: paperSize = "576"; break;
                case 2: paperSize = "832"; break;
            }
            return paperSize;
        }

        private bool ChangeCashDrawerPolarityIsEnabled(string simpleModelName)
        {
            ModelDictionary modelDic = new ModelDictionary();
            PrinterInfo printerInfo = modelDic.GetPrinterInfo(simpleModelName);

            return printerInfo.ChangeCashDrawerPolarityIsEnabled;
        }

        private async void ListBoxItem_Tapped(object sender, TappedRoutedEventArgs e)
        {
            var progressRing = new ProgressRing();
            StarIOPort.ProductInformationCollection productCollection = null;

            try
            {
                string selectInterface = (string)((sender as ListViewItem).Content);
                progressRing.Message = "Communicating...";
                progressRing.Show();

                switch (selectInterface)
                {
                    case "LAN":
                        productCollection = await StarIOPort.ProductInformation.FindAllAsync(StarIOPort.PrinterInterfaceType.LAN);
                        break;
                    case "Bluetooth":
                        productCollection = await StarIOPort.ProductInformation.FindAllAsync(StarIOPort.PrinterInterfaceType.Bluetooth);
                        break;
                    case "All":
                        productCollection = await StarIOPort.ProductInformation.FindAllAsync();
                        break;
                    case "Manual":
                        progressRing.Hide();
                        this.Frame.Navigate(typeof(PrinterSettingsPage), null);
                        return;
                }

                ObservableCollection<ProductList> collection = new ObservableCollection<ProductList>();

                foreach (var item in productCollection)
                {
                    collection.Add(new ProductList(item));
                }

                ProductListSource.Source = collection;

            }
            catch (Exception)
            {

            }
            finally
            {
                progressRing.Hide();
            }
 
        }


    }
}
