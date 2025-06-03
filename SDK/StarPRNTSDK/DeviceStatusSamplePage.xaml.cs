using Windows.UI;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using System;

namespace StarPRNTSDK
{

    public sealed partial class DeviceStatusSamplePage : Page
    {
        public DeviceStatusSamplePage()
        {
            this.InitializeComponent();

            Title.Visibility   = Windows.UI.Xaml.Visibility.Collapsed;
            ContentView.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
        }

        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            await GetDeviceStatus();


        }

        private async System.Threading.Tasks.Task GetDeviceStatus()
        {
            
            ProgressRing progressRing = new ProgressRing();
            string message = null;
            FirmwareModelNameTile.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
            ModelName.Visibility             = Windows.UI.Xaml.Visibility.Collapsed;
            FirmwareVersionTile.Visibility   = Windows.UI.Xaml.Visibility.Collapsed;
            FirmwareVersion.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
            FirmwareInfoGetError.Visibility = Windows.UI.Xaml.Visibility.Collapsed;

            try
            {
                progressRing.Message = "Communicating...";
                progressRing.Show();

                PrinterSettings printerSettings = new PrinterSettings();
                PrinterSettingsInfo[] settingsData = await printerSettings.LoadSettings();
                string portName = printerSettings.GetPortName(true);
                string portSettings = printerSettings.GetPortSettings(true);
                bool cashDrawerOpenActiveHigh = printerSettings.GetCashDrawerOpenActiveHigh(true);

                StarIOPort.Status status = await Communication.RetrieveStatus(portName, portSettings, 30000);

                if (status == null || status.RawStatus.Length == 0)
                {
                    message = "Fail to Open Port.";
                }
                else
                {
                    ParsedStatus(status, cashDrawerOpenActiveHigh);

                    if (!status.Offline)
                    {
                        StarIOPort.FirmwareInformation firmwareInfo = await Communication.GetFirmwareVersion(portName, portSettings, 30000);

                        if (firmwareInfo != null)
                        {
                            ModelName.Text = firmwareInfo.ModelName;
                            ModelName.Foreground = new SolidColorBrush(Colors.Blue);

                            FirmwareVersion.Text = firmwareInfo.FirmwareVersion;
                            FirmwareVersion.Foreground = new SolidColorBrush(Colors.Blue);

                            FirmwareModelNameTile.Visibility = Windows.UI.Xaml.Visibility.Visible;
                            ModelName.Visibility = Windows.UI.Xaml.Visibility.Visible;
                            FirmwareVersionTile.Visibility = Windows.UI.Xaml.Visibility.Visible;
                            FirmwareVersion.Visibility = Windows.UI.Xaml.Visibility.Visible;
                        }
                    }
                    else
                    {
                        FirmwareInfoGetError.Visibility = Windows.UI.Xaml.Visibility.Visible;
                    }
                }

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

        private void ParsedStatus(StarIOPort.Status status, bool cashDrawerOpenActiveHigh)
        {
            ETBCounterTitle.Visibility = Windows.UI.Xaml.Visibility.Visible;
            ETBConter.Visibility       = Windows.UI.Xaml.Visibility.Visible;

            if (status == null || status.RawStatus.Length == 0)
            {
                return;
            }

            if (status.Offline)
            {
                OnlineStatus.Text = "Offline";
                OnlineStatus.Foreground = new SolidColorBrush(Colors.Red);
            }
            else
            {
                OnlineStatus.Text = "Online";
                OnlineStatus.Foreground = new SolidColorBrush(Colors.Blue);
            }

            if (status.CoverOpen)
            {
                CoverStatus.Text = "Open";
                CoverStatus.Foreground = new SolidColorBrush(Colors.Red);
            }
            else
            {
                CoverStatus.Text = "Closed";
                CoverStatus.Foreground = new SolidColorBrush(Colors.Blue);
            }

            if (status.ReceiptPaperEmpty)
            {
                PaperStatus.Text = "Empty";
                PaperStatus.Foreground = new SolidColorBrush(Colors.Red);
            }
            else if (status.ReceiptPaperNearEmptyInner || status.ReceiptPaperNearEmptyOuter)
            {
                PaperStatus.Text = "Near Empty";
                PaperStatus.Foreground = new SolidColorBrush(Colors.Orange);
            }
            else
            {
                PaperStatus.Text = "Ready";
                PaperStatus.Foreground = new SolidColorBrush(Colors.Blue);
            }

            if (cashDrawerOpenActiveHigh)
            {
                if (status.CompulsionSwitch)
                {
                    CashDrawerStatus.Text = "Open";
                    CashDrawerStatus.Foreground = new SolidColorBrush(Colors.Red);
                }
                else
                {
                    CashDrawerStatus.Text = "Closed";
                    CashDrawerStatus.Foreground = new SolidColorBrush(Colors.Blue);
                }
            }
            else
            {
                if (status.CompulsionSwitch)
                {
                    CashDrawerStatus.Text = "Closed";
                    CashDrawerStatus.Foreground = new SolidColorBrush(Colors.Blue);
                }
                else
                {
                    CashDrawerStatus.Text = "Open";
                    CashDrawerStatus.Foreground = new SolidColorBrush(Colors.Red);
                }
            }

            if (status.OverTemp)
            {
                HeadTempertureStatus.Text = "High";
                HeadTempertureStatus.Foreground = new SolidColorBrush(Colors.Red);
            }
            else
            {
                HeadTempertureStatus.Text = "Normal";
                HeadTempertureStatus.Foreground = new SolidColorBrush(Colors.Blue);
            }

            if (status.UnrecoverableError)
            {
                NonRecoverableErrorStatus.Text = "Error";
                NonRecoverableErrorStatus.Foreground = new SolidColorBrush(Colors.Red);
            }
            else
            {
                NonRecoverableErrorStatus.Text = "Ready";
                NonRecoverableErrorStatus.Foreground = new SolidColorBrush(Colors.Blue);
            }

            if (status.CutterError)
            {
                PaperCutterStatus.Text = "Error";
                PaperCutterStatus.Foreground = new SolidColorBrush(Colors.Red);
            }
            else
            {
                PaperCutterStatus.Text = "Ready";
                PaperCutterStatus.Foreground = new SolidColorBrush(Colors.Blue);
            }

            if (status.HeadThermistorError)
            {
                HeadThermistorStatus.Text = "Abnormal";
                HeadThermistorStatus.Foreground = new SolidColorBrush(Colors.Red);
            }
            else
            {
                HeadThermistorStatus.Text = "Normal";
                HeadThermistorStatus.Foreground = new SolidColorBrush(Colors.Blue);
            }

            if (status.VoltageError)
            {
                VoltageStatus.Text = "Abnormal";
                VoltageStatus.Foreground = new SolidColorBrush(Colors.Red);
            }
            else
            {
                VoltageStatus.Text = "Normal";
                VoltageStatus.Foreground = new SolidColorBrush(Colors.Blue);
            }

            if (status.ETBAvailable)
            {
                ETBConter.Text = status.ETBCounter.ToString();
            }
            else
            {
                ETBCounterTitle.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
                ETBConter.Visibility       = Windows.UI.Xaml.Visibility.Collapsed;
            }
            

            Title.Visibility   = Windows.UI.Xaml.Visibility.Visible;
            ContentView.Visibility = Windows.UI.Xaml.Visibility.Visible;
        }

        private async void RefreshButton_Tapped(object sender, TappedRoutedEventArgs e)
        {
            await GetDeviceStatus();
        }
    }


}
