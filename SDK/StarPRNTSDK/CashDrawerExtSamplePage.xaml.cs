using StarIO_Extension;
using StarIOPort;
using StarPRNTSDK.Functions;
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

    public sealed partial class CashDrawerExtSamplePage : Page
    {
        private StarIoExtManager starIoExtManager = null;
        private string selectedName;
        private bool IsConnected { get; set; } = false;

        public CashDrawerExtSamplePage()
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
            selectedName = e.Parameter as string;


            (this.Resources["Storyboard1"] as Storyboard).Begin();

            try
            {
                PrinterSettings printerSettings = new PrinterSettings();
                PrinterSettingsInfo[] settingsData        = await printerSettings.LoadSettings();
                string portName                 = printerSettings.GetPortName(true);
                string portSettings             = printerSettings.GetPortSettings(true);
                bool cashDrawerOpenActiveHigh   = printerSettings.GetCashDrawerOpenActiveHigh(true);

                starIoExtManager = new StarIoExtManager(StarIoExtManagerType.Standard, portName, portSettings, 30000);
                starIoExtManager.CashDrawerOpenActiveHigh = cashDrawerOpenActiveHigh;

                starIoExtManager.PrinterImpossibleEvent += starIoExtManager_PrinterImpossibleEvent;
                starIoExtManager.CashDrawerOpenEvent += starIoExtManager_CashDrawerOpenEvent;
                starIoExtManager.CashDrawerCloseEvent += starIoExtManager_CashDrawerCloseEvent;

                starIoExtManager.AccessoryConnectSuccessEvent += starIoExtManager_AccessoryConnectSuccessEvent;
                starIoExtManager.AccessoryConnectFailureEvent += starIoExtManager_AccessoryConnectFailureEvent;
                starIoExtManager.AccessoryDisconnectEvent += starIoExtManager_AccessoryDisconnectEvent;

                await Connect();

                if (IsConnected)
                {
                    await OpenDrawer();
                }
            }
            catch (Exception )
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

        private async Task OpenDrawer()
        {
            ProgressRing progressRing = new ProgressRing() { Message = "Communicating..." };
            CommunicationResult result;
            string message = null;

            try
            {
                progressRing.Show();

                PrinterSettings printerSettings = new PrinterSettings();
                PrinterSettingsInfo[] settingsData = await printerSettings.LoadSettings();
                Emulation emulation = printerSettings.GetEmulation(true);
                IBuffer buffer = null;
                bool DoCheckCondition = true;

                switch (selectedName)
                {
                    case "Channel1Ext":
                        buffer = CashDrawerFunctions.CreateData(emulation, PeripheralChannel.No1);
                        DoCheckCondition = true;
                        break;
                    case "Channel1ExtDoNotCheckCondition":
                        buffer = CashDrawerFunctions.CreateData(emulation, PeripheralChannel.No1);
                        DoCheckCondition = false;
                        break;
                    case "Channel2Ext":
                        buffer = CashDrawerFunctions.CreateData(emulation, PeripheralChannel.No2);
                        DoCheckCondition = true;
                        break;
                    case "Channel2ExtDoNotCheckCondition":
                        buffer = CashDrawerFunctions.CreateData(emulation, PeripheralChannel.No2);
                        DoCheckCondition = false;
                        break; ;
                }

                using (await starIoExtManager.LockAsync())
                {
                    if (DoCheckCondition)
                    {
                        result = await Communication.SendCommands(buffer, starIoExtManager.Port);
                    }
                    else
                    {
                        result = await Communication.SendCommandsDoNotCheckCondition(buffer, starIoExtManager.Port);
                    }
                }

                message = Communication.GetCommunicationResultMessage(result);
            }
            catch (Exception)
            {

            }
            finally
            {
                progressRing.Hide();
            }

            if (message != null)
            {
                Windows.UI.Popups.MessageDialog dialog = new Windows.UI.Popups.MessageDialog(message, "Communication Result");

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

        private async void OpenButton_Tapped(object sender, TappedRoutedEventArgs e)
        {
            await OpenDrawer();
        }

        private async void RefreshButton_Tapped(object sender, TappedRoutedEventArgs e)
        {
            await Refresh();
        }
    }
}
