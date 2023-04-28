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
using static StarPRNTSDK.PrinterSamplePage;

namespace StarPRNTSDK
{

    public sealed partial class PrinterExtSamplePage : Page
    {
        private StarIoExtManager starIoExtManager = null;
        private PrintSampleFunctionType selectedFunctionType;

        private bool IsConnected { get; set; } = false;

        public PrinterExtSamplePage()
        {
            this.InitializeComponent();
        }

        protected override async void OnNavigatedFrom(NavigationEventArgs e)
        {
            (this.Resources["Storyboard1"] as Storyboard).Stop();

            await starIoExtManager.DisconnectAsync();
        }


        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            (this.Resources["Storyboard1"] as Storyboard).Begin();

            selectedFunctionType = (PrintSampleFunctionType)e.Parameter;

            try
            {
                PrinterSettings printerSettings = new PrinterSettings();
                PrinterSettingsInfo[] settingsData = await printerSettings.LoadSettings();
                string portName = printerSettings.GetPortName(true);
                string portSettings = printerSettings.GetPortSettings(true);

                starIoExtManager = new StarIoExtManager(StarIoExtManagerType.Standard, portName, portSettings, 30000);

                starIoExtManager.PrinterImpossibleEvent += starIoExtManager_PrinterImpossibleEvent;
                starIoExtManager.PrinterOnlineEvent += starIoExtManager_PrinterOnlineEvent;
                starIoExtManager.PrinterPaperNearEmptyEvent += starIoExtManager_PrinterPaperNearEmptyEvent;
                starIoExtManager.PrinterPaperEmptyEvent += starIoExtManager_PrinterPaperEmptyEvent;
                starIoExtManager.PrinterCoverOpenEvent += starIoExtManager_PrinterCoverOpenEvent;

                starIoExtManager.AccessoryConnectSuccessEvent += starIoExtManager_AccessoryConnectSuccessEvent;
                starIoExtManager.AccessoryConnectFailureEvent += starIoExtManager_AccessoryConnectFailureEvent;
                starIoExtManager.AccessoryDisconnectEvent += starIoExtManager_AccessoryDisconnectEvent;
                starIoExtManager.StatusUpdatedEvent += starIoExtManager_StatusUpdatedEvent;

                await Connect();

                if(IsConnected)
                {
                    await Print();
                }
            }
            catch (Exception)
            {
            }
        }

        private async Task Refresh()
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
                progressRing = new ProgressRing() { Message = "Communicating..." };
                progressRing.Show();

                PrinterSettings printerSettings = new PrinterSettings();
                PrinterSettingsInfo[] settingsData = await printerSettings.LoadSettings();
                Emulation emulation = printerSettings.GetEmulation(true);
                int paperSize = printerSettings.GetPaperSize(true);
                int languageIndex = printerSettings.GetLanguage();

                ILocalizeReceipts localizeReceipts = ILocalizeReceipts.createLocalizeReceipts(languageIndex, paperSize, printerSettings);

                IBuffer buffer = null;

                switch (selectedFunctionType)
                {
                    case PrintSampleFunctionType.TextReceiptExt:
                        buffer = PrinterFunctions.CreateTextReceiptData(emulation, localizeReceipts, false);
                        break;
                    case PrintSampleFunctionType.TextReceiptUTF8Ext:
                        buffer = PrinterFunctions.CreateTextReceiptData(emulation, localizeReceipts, true);
                        break;
                    case PrintSampleFunctionType.RasterReceiptExt:
                        buffer = await PrinterFunctions.CreateRasterReceiptData(emulation, localizeReceipts);
                        break;
                    case PrintSampleFunctionType.RasterReceiptBothScaleExt:
                        buffer = await PrinterFunctions.CreateScaleRasterReceiptData(emulation, localizeReceipts, (uint)paperSize, true);
                        break;
                    case PrintSampleFunctionType.RasterReceiptScaleExt:
                        buffer = await PrinterFunctions.CreateScaleRasterReceiptData(emulation, localizeReceipts, (uint)paperSize, false);
                        break;
                    case PrintSampleFunctionType.RasterCouponExt:
                        buffer = await PrinterFunctions.CreateCouponData(emulation, localizeReceipts, (uint)paperSize, BitmapConverterRotation.Normal);
                        break;
                    case PrintSampleFunctionType.RasterCoupon90Ext:
                        buffer = await PrinterFunctions.CreateCouponData(emulation, localizeReceipts, (uint)paperSize, BitmapConverterRotation.Right90);
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

        async void starIoExtManager_StatusUpdatedEvent(object sender, object e)
        {
            await Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal, () =>
            {

            });
        }

        private async void RefreshButton_Tapped(object sender, TappedRoutedEventArgs e)
        {
            await Refresh();
        }

        private async void PrintButton_Tapped(object sender, TappedRoutedEventArgs e)
        {
            await Print();
        }
    }
}
