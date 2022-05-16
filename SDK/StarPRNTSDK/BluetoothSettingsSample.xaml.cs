using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
using System;
using System.ComponentModel;
using StarIOPort;
using StarIO_Extension;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Text;
using System.Threading.Tasks;
using Windows.UI;
using Windows.UI.Xaml.Media;

namespace StarPRNTSDK
{

    public sealed partial class BluetoothSettingsSample : Page
    {
        private StarIOPort.StarBluetoothManager starBluetoothManager = null;

        public bool isSMLSeries { get; private set; }

        public BluetoothSettingsSample()
        {
            this.InitializeComponent();           
        }

        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            await LoadSettingAsync();
        }

        private async void RefreshButton_Tapped(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            await LoadSettingAsync();
        }

        private async System.Threading.Tasks.Task LoadSettingAsync()
        {
            ProgressRing progressRing = new ProgressRing();
            progressRing.Message = "Communicating...";
            string message = "";
            bool result = false;
            try
            {
                ApplyButton.IsEnabled = false;

                progressRing.Show();

                PrinterSettings printerSettings = new PrinterSettings();
                PrinterSettingsInfo[] settingsData = await printerSettings.LoadSettings();
                string portName = printerSettings.GetPortName(true);
                string portSettings = printerSettings.GetPortSettings(true);
                Emulation emulation = printerSettings.GetEmulation(true);

                
                var checkResult = await CheckSupportModelFirmwareVersion(portName, portSettings);
                result = checkResult.Item1;
                if (result)
                {
                    starBluetoothManager = StarBluetoothManagerFactory.GetManager(portName, portSettings, 30000, emulation);

                    message = "Fail to Open Port.";
                    result = await starBluetoothManager.ConnectAsync();

                    if (result)
                    {
                        message = "Fail to load setting.";
                        result = await starBluetoothManager.LoadSettingAsync();

                        if (result)
                        {
                            UpdateUI();

                            await starBluetoothManager.DisconnectAsync();

                            ApplyButton.IsEnabled = true;
                        }
                    }
                }
                else
                {
                    //check firmware Error
                    message = checkResult.Item2;
                }

            }
            catch (Exception )
            {

            }
            finally
            {
                progressRing.Hide();
    
            }

            if (result == false)
            {
                Windows.UI.Popups.MessageDialog dialog = new Windows.UI.Popups.MessageDialog(message, "Communication Result");

                await dialog.ShowAsync();
            }

        }

        private async void ApplyButton_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            string message = null;
            bool result = false;
            ProgressRing progressRing = new ProgressRing();
            progressRing.Message = "Communicating...";

            if (await ConfirmSettingsAsync() == false)
            {
                return;
            }

            try
            {
                progressRing.Show();
                message = "Fail to Open port.";
                result = await starBluetoothManager.ConnectAsync();

                if (result)
                {
                    SetBluetoothInfo();

                    message = "Fail to Apply setting.";
                    result = await starBluetoothManager.ApplyAsync();

                }

            }
            catch (Exception)
            {
                result = false;
            }
            finally
            {
                if (starBluetoothManager.IsOpened)
                {
                    try
                    {
                        await starBluetoothManager.DisconnectAsync();
                    }
                    catch (Exception)
                    {

                    }
                }

                progressRing.Hide();

            }

            if (result)
            {
                message = "To apply settings, please turn the device power OFF and ON, and establish pairing again.";
                Windows.UI.Popups.MessageDialog dialog = new Windows.UI.Popups.MessageDialog(message, "Complete");

                await dialog.ShowAsync();
            }
            else
            {
                Windows.UI.Popups.MessageDialog dialog = new Windows.UI.Popups.MessageDialog(message, "Communication Result");

                await dialog.ShowAsync();
            }
        }

        private void UpdateUI()
        {
            //BluetoothDeviceName
            if (starBluetoothManager.BluetoothDeviceNameCapability == StarBluetoothSettingCapability.SUPPORT)
            {
                DeviceName.Text = starBluetoothManager.BluetoothDeviceName;
            }
            else
            {
                DeviceName.Text = "N/A";
            }

            //iOSPortName
            if (starBluetoothManager.iOSPortNameCapability == StarBluetoothSettingCapability.SUPPORT)
            {
                iOSPortName.Text = starBluetoothManager.iOSPortName;
            }
            else
            {
                iOSPortName.Text = "N/A";
            }

            //AutoConnect
            if (starBluetoothManager.AutoConnectCapability == StarBluetoothSettingCapability.SUPPORT)
            {
                AutoConnectionToggleSwitch.IsEnabled = true;
                AutoConnectionToggleSwitch.IsOn = starBluetoothManager.AutoConnect;
            }
            else
            {
                AutoConnectionToggleSwitch.IsEnabled = false;
                AutoConnectionToggleSwitch.IsOn = false;
            }


            //SecurityType
            if (starBluetoothManager.SecurityTypeCapability == StarBluetoothSettingCapability.SUPPORT)
            {

                if (starBluetoothManager.SecurityType == StarBluetoothSecurity.SSP)
                {
                    SecurityTypeText.Text = "SSP";
                }
                else if (starBluetoothManager.SecurityType == StarBluetoothSecurity.PINCODE)
                {
                    SecurityTypeText.Text = "PIN Code";
                }
                else //StarBluetoothSecurity.DISABLE
                {
                    SecurityTypeText.Text = "Disable";
                }

            }
            else
            {
                SecurityTypeText.Text = "N/A";
                SecurityTypeText.Foreground = new SolidColorBrush(Colors.Gray);

            }

            ChangePinCodeToggleSwitch.IsOn = false;
            //PinCode
            if (starBluetoothManager.PinCodeCapability == StarBluetoothSettingCapability.SUPPORT)
            {
                ChangePinCodeText.Text = "";
                ChangePinCodeToggleSwitch.IsEnabled = true;
                NewPinCode.Text = "";
                NewPinCode.IsReadOnly = true;
            }
            else
            {
                ChangePinCodeText.Text = "N/A";
                ChangePinCodeToggleSwitch.IsEnabled = false;
                NewPinCode.Text = "N/A";
            }
        }



        private void SetBluetoothInfo()
        {
            if (starBluetoothManager.BluetoothDeviceNameCapability == StarBluetoothSettingCapability.SUPPORT)
            {
                starBluetoothManager.BluetoothDeviceName = DeviceName.Text;
            }

            if (starBluetoothManager.iOSPortNameCapability == StarBluetoothSettingCapability.SUPPORT)
            {
                starBluetoothManager.iOSPortName = iOSPortName.Text;
            }

            if (starBluetoothManager.AutoConnectCapability == StarBluetoothSettingCapability.SUPPORT)
            {
                starBluetoothManager.AutoConnect = AutoConnectionToggleSwitch.IsOn;
            }

            if (starBluetoothManager.SecurityTypeCapability == StarBluetoothSettingCapability.SUPPORT &&
                starBluetoothManager.SecurityType == StarBluetoothSecurity.PINCODE &&
                ChangePinCodeToggleSwitch.IsOn)
            {
                starBluetoothManager.PinCode = NewPinCode.Text;
            }
        }

        private async void SecurityTypeText_Tapped(object sender, Windows.UI.Xaml.Input.TappedRoutedEventArgs e)
        {
            if (starBluetoothManager == null)
            {
                return;
            }
            if (starBluetoothManager.SecurityTypeCapability == StarBluetoothSettingCapability.NOSUPPORT)
            {
                return;
            }

            StarBluetoothSecurity[] securityType;
            string[] security;
            if (starBluetoothManager.DeviceType == StarDeviceType.StarDeviceTypeDesktopPrinter)
            {
                securityType = new StarBluetoothSecurity[] { StarBluetoothSecurity.SSP, StarBluetoothSecurity.PINCODE };
                security = new string[]  { "SSP", "PIN Code" };
            }
            else
            {
                securityType = new StarBluetoothSecurity[] { StarBluetoothSecurity.PINCODE, StarBluetoothSecurity.DISABLE };
                security = new string[] { "PIN Code" , "Disable"};
            }


            SelectBoolContentDialog selectBoolContentDialog  = new SelectBoolContentDialog("Select security type", "", security[0], security[1]);

            ContentDialogResult result = await selectBoolContentDialog.ShowAsync();

            if (result == ContentDialogResult.Primary)
            {
                SecurityTypeText.Text = security[0];
                starBluetoothManager.SecurityType = securityType[0];
            }
            else
            {
                SecurityTypeText.Text = security[1];
                starBluetoothManager.SecurityType = securityType[1];
            }
            SecurityTypeText.Foreground = new SolidColorBrush(Colors.Black);
        }


        private async Task<Tuple<bool, string>> CheckSupportModelFirmwareVersion(string portName, string portSettings)
        {
            bool result = false;
            string message = "";
            try
            {
                FirmwareInformation firmwareInfo = await Communication.GetFirmwareVersion(portName, portSettings, 30000);
                if (firmwareInfo == null)
                {
                    result = false;
                    message = "Fail to Open port.";
                }
                else
                {
                    // Bluetooth setting feature is supported from Ver3.0 or later (SM-S210i, SM-S220i, SM-T300i/T300 and SM-T400i).
                    // Other models support from Ver1.0.
                    if (firmwareInfo.ModelName.StartsWith("SM-S21") || firmwareInfo.ModelName.StartsWith("SM-S22") ||
                        firmwareInfo.ModelName.StartsWith("SM-T30") || firmwareInfo.ModelName.StartsWith("SM-T40"))
                    {
                        if (float.Parse(firmwareInfo.FirmwareVersion) < 3.0F)
                        {
                            result = false;
                            message = "This firmware version(" + firmwareInfo.FirmwareVersion + ") of the device does not support bluetooth setting feature.";
                            return new Tuple<bool, string>(false, message);
                        }
                    }

                    if (firmwareInfo.ModelName.StartsWith("SM-L"))
                    {
                        //SM-L200 and SM-L300 > character:[0-9] length:4
                        NewPinCode.MaxLength = 4;
                        isSMLSeries = true;
                    }
                    else
                    {
                        //Other models > character:[0-9a-zA-z] length:16
                        NewPinCode.MaxLength = 16;
                        isSMLSeries = false;
                    }

                    result = true;
                    message = "success";
                }

            }
            catch (Exception ex)
            {
                result = false;
                message = ex.Message;

            }

            return new Tuple<bool, string>(result, message);

        }

        private async Task<bool> ConfirmSettingsAsync()
        {
            if (starBluetoothManager.DeviceType == StarDeviceType.StarDeviceTypeDesktopPrinter &&
                AutoConnectionToggleSwitch.IsOn &&
                starBluetoothManager.SecurityType == StarBluetoothSecurity.PINCODE)
            {
                Windows.UI.Popups.MessageDialog dialog = new Windows.UI.Popups.MessageDialog("Auto Connection function is available only when the security setting is \"SSP\"", "Error");
                await dialog.ShowAsync();

                return false;
            }

            return true;
        }


        private bool ValidateStringSettings()
        {
            bool isValidDeviceName = true;

            if (starBluetoothManager == null)
            {
                return false;
            }

            if (starBluetoothManager.BluetoothDeviceNameCapability == StarBluetoothSettingCapability.SUPPORT)
            {
                isValidDeviceName = DeviceName.Text != null && ValidateNameLength(DeviceName.Text);
            }

            bool isValidateiOSPortName = true;

            if (starBluetoothManager.iOSPortNameCapability == StarBluetoothSettingCapability.SUPPORT)
            {
                isValidateiOSPortName = iOSPortName.Text != null && ValidateNameLength(iOSPortName.Text);
            }

            bool isValidatePinCode = true;

            if (starBluetoothManager.PinCodeCapability == StarBluetoothSettingCapability.SUPPORT && ChangePinCodeToggleSwitch.IsOn)
            {
                if (isSMLSeries)
                {
                    isValidatePinCode = NewPinCode.Text != null && ValidateSMLSeriesPinCodeLength(NewPinCode.Text);
                }
                else
                {
                    isValidatePinCode = NewPinCode.Text != null && ValidatePinCodeLength(NewPinCode.Text);
                }
            }

            return isValidDeviceName && isValidateiOSPortName && isValidatePinCode;

        }


        private bool ValidateNameLength(string text)
        {
            return text != null && text.Length >= 1 && text.Length <= 16;
        }

        private bool ValidateSMLSeriesPinCodeLength(string text)
        {
            return text != null && text.Length == 4;
        }

        private bool ValidatePinCodeLength(string text)
        {
            return text != null && text.Length >= 4 && text.Length <= 16;
        }


        private void ValidateInput(TextBox input, string text, bool paste)
        {
            string pattern;
            int length = input.Text.Length;

            int i = input.SelectionStart;
            string tempText = input.Text;


            string selecctedtext = input.SelectedText;
            int selectionLength = input.SelectionLength;


            if (paste)
            {
                if (selectionLength > 0)
                {
                    tempText = input.Text.Remove(i, selectionLength);
                    tempText = tempText.Insert(i, text);
                    length = tempText.Length;
                }
                else
                {
                    length += text.Length;
                    tempText = input.Text.Insert(i, text);
                }

                input.SelectionStart = i;
            }


            if (input.Name == "NewPinCode")
            {
                if (isSMLSeries)
                {
                    pattern = "^[0-9]+$";

                    if (length < 4 || length > 16)
                    {
                        return;
                    }
                }
                else
                {
                    pattern = "^[0-9a-zA-Z]+$";

                    if (length > 16)
                    {
                        return;
                    }
                }


            }
            else//DeviceName, iOSPortName
            {
                pattern = "^[0-9a-zA-Z;:!?#$%&,.@_\\-= \\\\/\\*\\+~\\^\\[\\{\\(\\]\\}\\)\\|]+$";

                if (length > 16)
                {
                    return;
                }
            }

            if (text == "N/A")
            {
                input.Foreground = new SolidColorBrush(Colors.Gray);
                input.IsReadOnly = true;
                return;
            }
            else
            {
                input.Foreground = new SolidColorBrush(Colors.Black);
                input.IsReadOnly = false;
            }

            if (paste)
            {
                if (!Regex.IsMatch(tempText, pattern))
                {
                    return;
                }
            }
            StringBuilder builder = new StringBuilder();

            for (int index = 0; index < length; index++)
            {
                string stringtext = tempText.Substring(index, 1);
                if (Regex.IsMatch(stringtext, pattern))
                {
                    builder.Append(stringtext);
                }
            }
            input.Text = builder.ToString();
            input.SelectionStart = i;
        }


        private void ChangePinCodeToggleSwitch_Toggled(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            if (ChangePinCodeToggleSwitch.IsOn == true)
            {
                NewPinCode.IsReadOnly = false;
            }
            else
            {
                NewPinCode.Text = "";
                NewPinCode.IsReadOnly = true;
            }

            ApplyButton.IsEnabled = ValidateStringSettings();
        }

        private void AutoConnectionToggleSwitch_Toggled(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            ApplyButton.IsEnabled = ValidateStringSettings();
        }

        private void TextChanging(TextBox sender, TextBoxTextChangingEventArgs args)
        {
            TextBox input = sender as TextBox;
            string text = input.Text;
            ValidateInput(input, text, false);

            ApplyButton.IsEnabled = ValidateStringSettings();
        }

        private async void Paste(object sender, TextControlPasteEventArgs e)
        {
            //Paste not supported.
            TextBox input = sender as TextBox;
            e.Handled = true;

            var dataPackageView = Windows.ApplicationModel.DataTransfer.Clipboard.GetContent();
            if (dataPackageView.Contains(Windows.ApplicationModel.DataTransfer.StandardDataFormats.Text))
            {
                try
                {
                    var text = await dataPackageView.GetTextAsync();
                    ValidateInput(input, text, true);
                }
                catch (Exception)
                {

                }
            }
        }


    }

    public class BoolConverter : Windows.UI.Xaml.Data.IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            return !(value is bool && (bool)value);
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            return !(value is bool && (bool)value);
        }

    }
}
