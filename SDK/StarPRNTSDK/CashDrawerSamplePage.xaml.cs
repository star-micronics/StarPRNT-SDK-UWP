using StarIO_Extension;
using StarPRNTSDK.Functions;
using System;
using Windows.Storage.Streams;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;

namespace StarPRNTSDK
{

    public sealed partial class CashDrawerSamplePage : Page
    {
        public CashDrawerSamplePage()
        {
            this.InitializeComponent();
        }

        private void CashDrawerExt_Tapped(object sender, TappedRoutedEventArgs e)
        {

            var selectedName = (sender as TextBlock).Name;

            this.Frame.Navigate(typeof(CashDrawerExtSamplePage), selectedName);
        }

        private async void CashDrawer_Tapped(object sender, TappedRoutedEventArgs e)
        {
            var selectedName = (sender as TextBlock).Name;


            var progressRing = new ProgressRing();
            CommunicationResult result;
            string msg = null;

            try
            {
                PrinterSettings printerSettings = new PrinterSettings();
                PrinterSettingsInfo[] settingsData        = await printerSettings.LoadSettings();
                string portName                 = printerSettings.GetPortName(true);
                string portSettings             = printerSettings.GetPortSettings(true);
                Emulation emulation             = printerSettings.GetEmulation(true);

                IBuffer buffer = null;
                bool DoCheckCondition = true;
                switch (selectedName)
                {
                    case "Channel1":
                        buffer = CashDrawerFunctions.CreateData(emulation, PeripheralChannel.No1);
                        DoCheckCondition = true;
                        break;
                    case "Channel1DoNotCheckCondition":
                        buffer = CashDrawerFunctions.CreateData(emulation, PeripheralChannel.No1);
                        DoCheckCondition = false;
                        break;
                    case "Channel2":
                        buffer = CashDrawerFunctions.CreateData(emulation, PeripheralChannel.No2);
                        DoCheckCondition = true;
                        break;
                    case "Channel2DoNotCheckCondition":
                        buffer = CashDrawerFunctions.CreateData(emulation, PeripheralChannel.No2);
                        DoCheckCondition = false;
                        break;

                }

                progressRing.Message = "Communicating...";
                progressRing.Show();


                if (DoCheckCondition)
                {
                    result = await Communication.SendCommands(buffer, portName, portSettings, 30000);
                }
                else
                {
                    result = await Communication.SendCommandsDoNotCheckCondition(buffer, portName, portSettings, 30000);
                }

                msg = Communication.GetCommunicationResultMessage(result);
            }
            catch (Exception )
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

    }
}
