using StarIO_Extension;
using StarPRNTSDK.Functions;
using System;
using System.Collections.Generic;
using Windows.Storage.Streams;
using Windows.UI.Core;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace StarPRNTSDK
{
    public sealed partial class HoldPrintSamplePage : Page
    {
        public HoldPrintSamplePage()
        {
            this.InitializeComponent();
        }

        private async void Print()
        {
            // Your printer PortName and PortSettings.
            PrinterSettings printerSettings = new PrinterSettings();
            string portName = printerSettings.GetPortName(true);
            string portSettings = printerSettings.GetPortSettings(true);

            // Your printer emulation.
            Emulation emulation = printerSettings.GetEmulation(true);

            bool[] isHoldArray;
            if (holdingControlComboBox.SelectedItem == alwaysComboBoxItem) // Always hold
            {
                isHoldArray = new bool[] { true, true, true };
            }
            else if (holdingControlComboBox.SelectedItem == lastPageComboBoxItem) // Hold before printing the last page
            {
                isHoldArray = new bool[] { false, true, false };
            }
            else // Do not Hold
            {
                isHoldArray = new bool[] { false, false, false };
            }

            List<IBuffer> commandList = PrinterFunctions.CreateHoldPrintData(emulation, isHoldArray);

            ProgressRing progressRing = new ProgressRing();
            progressRing.Message = "Communicating...";
            progressRing.Show();

            CommunicationResult sendCommandsResult = await Communication.SendCommandsMultiplePages(commandList, portName, portSettings, 30000, 10000, null, async (index) =>
            {
                if (!isHoldArray[index])
                {
                    return;
                }

                await Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
                {
                    progressRing.Message = "Please remove paper from printer.";
                });
            });

            progressRing.Hide();

            MessageDialog dialog = new MessageDialog(Communication.GetCommunicationResultMessage(sendCommandsResult), "Communication Result");
            await dialog.ShowAsync();
        }

        private void PlayButton_Click(object sender, RoutedEventArgs e)
        {
            Print();
        }
    }
}
