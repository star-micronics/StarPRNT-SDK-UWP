using StarIO_Extension;
using StarIOPort;
using StarPRNTSDK.Functions;
using StarPRNTSDK.Localizereceipts;
using System;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Storage.Streams;
using Windows.UI;
using Windows.UI.Popups;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Animation;
using Windows.UI.Xaml.Navigation;


namespace StarPRNTSDK
{

    public sealed partial class CombinationExtSamplePage : Page
    {
        private StarIoExtManager starIoExtManager = null;
        private string selectedName;

        private bool isNeedScroll;
        private bool IsConnected { get; set; } = false;


        public CombinationExtSamplePage()
        {
            this.InitializeComponent();

            this.SetBarcodeDataListView();
        }

        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            selectedName = e.Parameter as string;

            (this.Resources["Storyboard1"] as Storyboard).Begin();

            PrinterSettings printerSettings = new PrinterSettings();
            PrinterSettingsInfo[] settingsData        = await printerSettings.LoadSettings();
            string portName                 = printerSettings.GetPortName(true);
            string portSettings             = printerSettings.GetPortSettings(true);
            bool cashDrawerOpenActiveHigh   = printerSettings.GetCashDrawerOpenActiveHigh(true);

            starIoExtManager = new StarIoExtManager(StarIoExtManagerType.WithBarcodeReader, portName.ToString(), portSettings.ToString(), 30000);

            starIoExtManager.CashDrawerOpenActiveHigh = cashDrawerOpenActiveHigh;

            starIoExtManager.PrinterImpossibleEvent       += starIoExtManager_PrinterImpossibleEvent;
            starIoExtManager.PrinterOnlineEvent           += starIoExtManager_PrinterOnlineEvent;
            starIoExtManager.PrinterPaperNearEmptyEvent   += starIoExtManager_PrinterPaperNearEmptyEvent;
            starIoExtManager.PrinterPaperEmptyEvent       += starIoExtManager_PrinterPaperEmptyEvent;
            starIoExtManager.PrinterCoverOpenEvent        += starIoExtManager_PrinterCoverOpenEvent;

            starIoExtManager.CashDrawerOpenEvent          += starIoExtManager_CashDrawerOpenEvent;
            starIoExtManager.CashDrawerCloseEvent         += starIoExtManager_CashDrawerCloseEvent;

            starIoExtManager.BarcodeDataReceivedEvent     += starIoExtManager_BarcodeDataReceivedEvent;
            starIoExtManager.BarcodeReaderImpossibleEvent += starIoExtManager_BarcodeReaderImpossibleEvent;
            starIoExtManager.BarcodeReaderConnectEvent    += starIoExtManager_BarcodeReaderConnectEvent;
            starIoExtManager.BarcodeReaderDisconnectEvent += starIoExtManager_BarcodeReaderDisconnectEvent;
            starIoExtManager.AccessoryConnectSuccessEvent += starIoExtManager_AccessoryConnectSuccessEvent;
            starIoExtManager.AccessoryConnectFailureEvent += starIoExtManager_AccessoryConnectFailureEvent;
            starIoExtManager.AccessoryDisconnectEvent     += starIoExtManager_AccessoryDisconnectEvent;

            starIoExtManager.CashDrawerOpenActiveHigh = cashDrawerOpenActiveHigh;

            await Connect();
        }

        protected override async void OnNavigatedFrom(NavigationEventArgs e)
        {
            (this.Resources["Storyboard1"] as Storyboard).Stop();

            await starIoExtManager.DisconnectAsync();
        }

        private void SetBarcodeDataListView()
        {
            this.isNeedScroll = false;

            this.BarcodeDataListView.LayoutUpdated += OnBarcodeDataListViewUpdated;
        }

        private void OnBarcodeDataListViewUpdated(object sender, object e)
        {
            this.ScrollBarcodeDataListViewToBottom();
        }

        private void ScrollBarcodeDataListViewToBottom()
        {
            if (this.BarcodeDataListView.Items != null && this.BarcodeDataListView.Items.Count != 0 && this.isNeedScroll)
            {
                Border border = (Border)VisualTreeHelper.GetChild(this.BarcodeDataListView, 0);
                ScrollViewer scrollViewer = (ScrollViewer)VisualTreeHelper.GetChild(border, 0);
                scrollViewer.ChangeView(0, scrollViewer.ScrollableHeight, 1);

                this.isNeedScroll = false;
            }
        }

        private async void Refresh()
        {
            await Disconnect();

            await Connect();
        }

        private async Task Connect()
        {
            ProgressRing progressRing = new ProgressRing() { Message = "Communicating..." };
            string reason = "";

            try
            {
                progressRing.Show();

                IAsyncOperation<bool> connectOperation = starIoExtManager.ConnectAsync();
                IsConnected = await connectOperation;

                if (!IsConnected)
                {
                    if (connectOperation.ErrorCode != null)
                    {
                        if (connectOperation.ErrorCode.HResult == StarResultCode.ErrorFailed)
                        {
                            reason = "Power and Bluetooth pairing";
                        }
                        else if (connectOperation.ErrorCode.HResult == StarResultCode.ErrorInUse)
                        {
                            reason = "In use";
                        }
                        else if (connectOperation.ErrorCode.HResult == StarResultCode.ErrorInProcess)
                        {
                            reason = "In process";
                        }
                        else
                        {
                            reason = "Power and Bluetooth pairing";
                        }
                    }
                    else
                    {
                        reason = "Unknown";
                    }
                }

            }
            finally
            {
                progressRing.Hide();
            }

            if (!IsConnected)
            {
                await Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal, () =>
                {
                    StatusText.Text = "Check the device. (" + reason + ")\nThen touch up the Refresh button.";
                    StatusText.Foreground = new SolidColorBrush(Colors.Red);
                });

                MessageDialog dialog = new MessageDialog("Fail to openPort", "Communication Result");
                await dialog.ShowAsync();
            }
        }

        private async Task Disconnect()
        {
            ProgressRing progressRing = new ProgressRing() { Message = "Communicating..." };

            try
            {
                progressRing.Show();

                await starIoExtManager.DisconnectAsync();
            }
            finally
            {
                progressRing.Hide();
            }
        }

        private async Task Print()
        {
            ProgressRing progressRing = new ProgressRing() { Message = "Communicating..." };
            CommunicationResult result;
            string msg = null;

            try
            {
                progressRing.Show();

                PrinterSettings printerSettings = new PrinterSettings();
                Emulation emulation = printerSettings.GetEmulation(true);
                int paperSize = printerSettings.GetPaperSize(true);
                int languageIndex = printerSettings.GetLanguage();

                ILocalizeReceipts localizeReceipts = ILocalizeReceipts.createLocalizeReceipts(languageIndex, paperSize, printerSettings);

                IBuffer buffer = null;

                switch (selectedName)
                {
                    case "TextReceitExt":
                        buffer = CombinationFunctions.CreateTextReceiptData(emulation, localizeReceipts, false);
                        break;
                    case "TextReceitUTF8Ext":
                        buffer = CombinationFunctions.CreateTextReceiptData(emulation, localizeReceipts, true);
                        break;
                    case "RasterReceiptExt":
                        buffer = await CombinationFunctions.CreateRasterReceiptData(emulation, localizeReceipts);
                        break;
                    case "RasterReceiptBothScaleExt":
                        buffer = await CombinationFunctions.CreateScaleRasterReceiptData(emulation, localizeReceipts, (uint)paperSize, true);
                        break;
                    case "RasterReceiptScaleExt":
                        buffer = await CombinationFunctions.CreateScaleRasterReceiptData(emulation, localizeReceipts, (uint)paperSize, false);
                        break;
                    case "RasterCouponExt":
                        buffer = await CombinationFunctions.CreateCouponData(emulation, localizeReceipts, (uint)paperSize, BitmapConverterRotation.Normal);
                        break;
                    case "RasterCouponRotation90Ext":
                        buffer = await CombinationFunctions.CreateCouponData(emulation, localizeReceipts, (uint)paperSize, BitmapConverterRotation.Right90);
                        break;
                }

                using (await starIoExtManager.LockAsync())
                {
                    result = await Communication.SendCommands(buffer, starIoExtManager.Port);
                }

                msg = Communication.GetCommunicationResultMessage(result);
            }
            catch (Exception)
            {

            }
            finally
            {
                progressRing.Hide();

                (this.Resources["Storyboard1"] as Storyboard).Begin();
            }

            if (msg != null)
            {
                Windows.UI.Popups.MessageDialog dialog = new Windows.UI.Popups.MessageDialog(msg, "Communication Result");

                await dialog.ShowAsync();
            }
        }

        async void starIoExtManager_PrinterImpossibleEvent(object sender, object e)
        {
            await Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal, () =>
            {
                StatusText.Text = "Printer Impossible.";
                StatusText.Foreground = new SolidColorBrush(Colors.Red);
            });

        }

        async void starIoExtManager_PrinterOnlineEvent(object sender, object e)
        {
            await Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal, () =>
            {
                StatusText.Text = "Printer Online.";
                StatusText.Foreground = new SolidColorBrush(Colors.Blue);
            });
        }

        async void starIoExtManager_PrinterPaperNearEmptyEvent(object sender, object e)
        {
            await Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal, () =>
            {
                StatusText.Text = "Printer Paper Near Empty.";
                StatusText.Foreground = new SolidColorBrush(Colors.Orange);
            });

        }

        async void starIoExtManager_PrinterPaperEmptyEvent(object sender, object e)
        {
            await Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal, () =>
            {
                StatusText.Text = "Printer Paper Empty.";
                StatusText.Foreground = new SolidColorBrush(Colors.Red);
            });
        }

        async void starIoExtManager_PrinterCoverOpenEvent(object sender, object e)
        {
            await Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal, () =>
            {
                StatusText.Text = "Printer Cover Open.";
                StatusText.Foreground = new SolidColorBrush(Colors.Red);
            });
        }

        async void starIoExtManager_CashDrawerOpenEvent(object sender, object e)
        {
            await Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal, () =>
            {
                StatusText.Text = "Cash Drawer Open.";
                StatusText.Foreground = new SolidColorBrush(Colors.Magenta);
            });
        }

        async void starIoExtManager_CashDrawerCloseEvent(object sender, object e)
        {
            await Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal, () =>
            {
                StatusText.Text = "Cash Drawer Close.";
                StatusText.Foreground = new SolidColorBrush(Colors.Blue);
            });
        }

        async void starIoExtManager_BarcodeDataReceivedEvent(object sender, object e)
        {
            //An object that contains the event data. 
            //Cast object to byte[].
            byte[] data = e as byte[];

            try
            {
                string text = System.Text.Encoding.UTF8.GetString(data, 0, data.Length);

                string[] stArrayData = text.Split('\n');


                int index = 0;
                foreach (var item in stArrayData)
                {
                    if (item != "")
                    {
                        string barcode = item;

                        index = item.IndexOf("\r");

                        if (index != -1)
                        {
                            barcode = barcode.Substring(0, index);
                        }

                        await Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal, () =>
                        {
                            this.BarcodeDataListView.Items.Add(barcode);

                            this.isNeedScroll = true;
                        });

                    }
                }
            }
            catch (Exception)
            {

            }

        }

        async void starIoExtManager_BarcodeReaderImpossibleEvent(object sender, object e)
        {
            await Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal, () =>
            {
                StatusText.Text = "Barcode Reader Impossible.";
                StatusText.Foreground = new SolidColorBrush(Colors.Red);

            });

        }

        async void starIoExtManager_BarcodeReaderConnectEvent(object sender, object e)
        {
            await Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal, () =>
            {
                StatusText.Text = "Barcode Reader Connect.";
                StatusText.Foreground = new SolidColorBrush(Colors.Blue);

            });

        }

        async void starIoExtManager_BarcodeReaderDisconnectEvent(object sender, object e)
        {
            await Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal, () =>
            {
                StatusText.Text = "Barcode Reader Disconnect.";
                StatusText.Foreground = new SolidColorBrush(Colors.Red);

            });
        }


        async void starIoExtManager_AccessoryConnectSuccessEvent(object sender, object e)
        {
            await Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal, () =>
            {
                StatusText.Text = "Accessory Connect Success.";
                StatusText.Foreground = new SolidColorBrush(Colors.Blue);
            });
        }

        async void starIoExtManager_AccessoryConnectFailureEvent(object sender, object e)
        {
            await Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal, () =>
            {
                StatusText.Text = "Accessory Connect Failure.";
                StatusText.Foreground = new SolidColorBrush(Colors.Red);
            });

        }

        async void starIoExtManager_AccessoryDisconnectEvent(object sender, object e)
        {
            await Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal, () =>
            {
                StatusText.Text = "Accessory Disconnect.";
                StatusText.Foreground = new SolidColorBrush(Colors.Red);
            });

        }

        private void RefreshButton_Tapped(object sender, TappedRoutedEventArgs e)
        {
            Refresh();
        }

        private async void PrintButton_Tapped(object sender, TappedRoutedEventArgs e)
        {
            await Print();
        }
    }
}
