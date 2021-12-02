using StarIO_Extension;
using System;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using Windows.Storage;
using Windows.Storage.Pickers;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace StarPRNTSDK
{
    public sealed partial class MelodySpeakerSamplePage : Page
    {
        private MelodySpeakerModel SpeakerModel { get; set; } = MelodySpeakerModel.None;
        private StorageFile SelectedSoundFile { get; set; }

        private async void PlayRegisteredSound()
        {
            // Your printer PortName and PortSettings.
            PrinterSettings printerSettings = new PrinterSettings();
            string portName = printerSettings.GetPortName(true);
            string portSettings = printerSettings.GetPortSettings(true);

            // Your speaker model.
            MelodySpeakerModel speakerModel = SpeakerModel;

            MessageDialog dialog;
            ProgressRing progressRing = new ProgressRing();
            progressRing.Message = "Communicating...";
            progressRing.Show();

            // Check MCS10 connection status.
            if (speakerModel == MelodySpeakerModel.MCS10)
            {
                IPeripheralConnectParser parser = StarIoExt.CreateMelodySpeakerConnectParser(speakerModel);

                CommunicationResult parseResult = await Communication.ParseDoNotCheckCondition(parser, portName, portSettings, 30000);

                if (parseResult.Result == Communication.Result.Success)
                {
                    if (!parser.IsConnected)
                    {
                        progressRing.Hide();
                        dialog = new MessageDialog("MelodySpeaker Disconnect.", "Check Status");
                        await dialog.ShowAsync();
                        return;
                    }
                }
                else
                {
                    progressRing.Hide();
                    dialog = new MessageDialog("Printer Impossible", "Failure");
                    await dialog.ShowAsync();
                    return;
                }
            }

            // Set sound storage area and number settings.
            bool specifySound = false;
            MelodySpeakerSoundStorageArea soundStorageArea = MelodySpeakerSoundStorageArea.Area1;
            int soundNumber = 0;

            switch (soundStorageAreaComboBox.SelectedIndex)
            {
                default:
                case 0: // Default
                    // Not specify sound storage area and sound number
                    break;

                case 1: // Area1
                    specifySound = true;
                    soundStorageArea = MelodySpeakerSoundStorageArea.Area1;
                    soundNumber = InputSoundNumber;
                    break;

                case 2: // Area2
                    specifySound = true;
                    soundStorageArea = MelodySpeakerSoundStorageArea.Area2;
                    soundNumber = InputSoundNumber;
                    break;
            }

            // Set volume setting.
            bool specifyVolume = false;
            int volume = 0;

            if (speakerModel != MelodySpeakerModel.FVP10) // FVP10 not supported volume setting.
            {
                switch (volumeTypeRegisteredComboBox.SelectedIndex)
                {
                    default:
                    case 0: // Default
                        // Not specify volume
                        break;

                    case 1: // Off
                        specifyVolume = true;
                        volume = SoundSetting.VolumeOff;
                        break;

                    case 2: // Min
                        specifyVolume = true;
                        volume = SoundSetting.VolumeMin;
                        break;

                    case 3: // Max
                        specifyVolume = true;
                        volume = SoundSetting.VolumeMax;
                        break;

                    case 4: // Manual
                        specifyVolume = true;
                        volume = (int)volumeRegisteredSlider.Value;
                        break;
                }
            }

            // Create melody speaker commands.
            byte[] commands = null;
            try
            {
                commands = MelodySpeakerFunctions.CreatePlayingRegisteredSoundData(speakerModel, specifySound, soundStorageArea, soundNumber, specifyVolume, volume);
            }
            catch (ArgumentOutOfRangeException ex) // Specified parameter is out of range.
            {
                progressRing.Hide();
                dialog = new MessageDialog(ex.GetType().Name + "\n" + ex.Message, "Error");
                await dialog.ShowAsync();
                return;
            }
            catch (ArgumentException ex) // Not allowed operation. (Ex. Specify not supported parameter)
            {
                progressRing.Hide();
                dialog = new MessageDialog(ex.GetType().Name + "\n" + ex.Message, "Error");
                await dialog.ShowAsync();
                return;
            }
            catch (Exception ex) // Other error.
            {
                progressRing.Hide();
                dialog = new MessageDialog(ex.GetType().Name + "\n" + ex.Message, "Error");
                await dialog.ShowAsync();
                return;
            }

            // Send melody speaker commands.
            CommunicationResult sendCommandsResult = await Communication.SendCommands(commands.AsBuffer(), portName, portSettings, 30000);

            progressRing.Hide();

            dialog = new MessageDialog(Communication.GetCommunicationResultMessage(sendCommandsResult), "Communication Result");
            await dialog.ShowAsync();
        }

        private async void PlaySoundData()
        {
            // Your printer PortName and PortSettings.
            PrinterSettings printerSettings = new PrinterSettings();
            string portName = printerSettings.GetPortName(true);
            string portSettings = printerSettings.GetPortSettings(true);

            // Your speaker model.
            MelodySpeakerModel speakerModel = SpeakerModel;

            MessageDialog dialog;
            ProgressRing progressRing = new ProgressRing();
            progressRing.Message = "Communicating...";
            progressRing.Show();

            // Check MCS10 connection status.
            if (speakerModel == MelodySpeakerModel.MCS10)
            {
                IPeripheralConnectParser parser = StarIoExt.CreateMelodySpeakerConnectParser(speakerModel);

                CommunicationResult parseResult = await Communication.ParseDoNotCheckCondition(parser, portName, portSettings, 30000);

                if (parseResult.Result == Communication.Result.Success)
                {
                    if (!parser.IsConnected)
                    {
                        progressRing.Hide();
                        dialog = new MessageDialog("MelodySpeaker Disconnect.", "Check Status");
                        await dialog.ShowAsync();
                        return;
                    }
                }
                else
                {
                    progressRing.Hide();
                    dialog = new MessageDialog("Printer Impossible", "Failure");
                    await dialog.ShowAsync();
                    return;
                }
            }

            // Your sound File
            StorageFile file = SelectedSoundFile;

            // Set volume setting.
            bool specifyVolume = false;
            int volume = 0;

            if (speakerModel != MelodySpeakerModel.FVP10) // FVP10 not supported volume setting.
            {
                switch (volumeTypeReceivedComboBox.SelectedIndex)
                {
                    default:
                    case 0: // Default
                        // Not specify volume
                        break;

                    case 1: // Off
                        specifyVolume = true;
                        volume = SoundSetting.VolumeOff;
                        break;

                    case 2: // Min
                        specifyVolume = true;
                        volume = SoundSetting.VolumeMin;
                        break;

                    case 3: // Max
                        specifyVolume = true;
                        volume = SoundSetting.VolumeMax;
                        break;

                    case 4: // Manual
                        specifyVolume = true;
                        volume = (int)volumeReceivedSlider.Value; ;
                        break;
                }
            }

            // Create melody speaker commands.
            byte[] commands = null;
            try
            {
                commands = MelodySpeakerFunctions.CreatePlayingSoundData(speakerModel, file, specifyVolume, volume);
            }
            catch (FormatException ex) // Unsupported sound format.
            {
                progressRing.Hide();
                dialog = new MessageDialog(ex.GetType().Name + "\n" + ex.Message, "Error");
                await dialog.ShowAsync();
                return;
            }
            catch (ArgumentOutOfRangeException ex) // Specified parameter is out of range.
            {
                progressRing.Hide();
                dialog = new MessageDialog(ex.GetType().Name + "\n" + ex.Message, "Error");
                await dialog.ShowAsync();
                return;
            }
            catch (ArgumentException ex) // Not allowed operation. (Ex. Specify not supported parameter)
            {
                progressRing.Hide();
                dialog = new MessageDialog(ex.GetType().Name + "\n" + ex.Message, "Error");
                await dialog.ShowAsync();
                return;
            }
            catch (InvalidOperationException ex) // Not allowed operation. (Ex. Unsupported model is specified.)
            {
                progressRing.Hide();
                dialog = new MessageDialog(ex.GetType().Name + "\n" + ex.Message, "Error");
                await dialog.ShowAsync();
                return;
            }
            catch (Exception ex) // Other error.
            {
                progressRing.Hide();
                dialog = new MessageDialog(ex.GetType().Name + "\n" + ex.Message, "Error");
                await dialog.ShowAsync();
                return;
            }

            // Send melody speaker commands.
            CommunicationResult sendCommandsResult = await Communication.SendCommands(commands.AsBuffer(), portName, portSettings, 30000);

            progressRing.Hide();

            dialog = new MessageDialog(Communication.GetCommunicationResultMessage(sendCommandsResult), "Communication Result");
            await dialog.ShowAsync();
        }

        private async void SelectSoundFile()
        {
            FileOpenPicker picker = new FileOpenPicker();
            picker.ViewMode = PickerViewMode.Thumbnail;
            picker.SuggestedStartLocation = PickerLocationId.MusicLibrary;
            picker.FileTypeFilter.Add(".wav");

            StorageFile file = await picker.PickSingleFileAsync();

            if (file != null)
            {
                SelectedSoundFile = file;
                filePathTextBox.Text = file.Path;
            }
        }

        public MelodySpeakerSamplePage()
        {
            this.InitializeComponent();

            Initialize();
        }

        private void PlayRegisteredButton_Click(object sender, RoutedEventArgs e)
        {
            PlayRegisteredSound();
        }

        private async void PlayReceivedButton_Click(object sender, RoutedEventArgs e)
        {
            if (SelectedSoundFile == null)
            {
                MessageDialog dialog = new MessageDialog("Sound file is not selected.", "Error");
                await dialog.ShowAsync();

                return;
            }

            PlaySoundData();
        }

        private void SelectFileButton_Click(object sender, RoutedEventArgs e)
        {
            SelectSoundFile();
        }

        private void SoundNumberInputTextBox_TextChanging(TextBox sender, TextBoxTextChangingEventArgs args)
        {
            int selection = sender.SelectionStart;
            int diff = 0;

            StringBuilder builder = new StringBuilder();

            foreach (char ch in sender.Text)
            {
                if ('0' <= ch && ch <= '9')
                {
                    builder.Append(ch);
                }
                else
                {
                    diff++;
                }
            }

            sender.Text = builder.ToString();
            sender.SelectionStart = selection - diff;
        }

        private int InputSoundNumber
        {
            get
            {
                int soundNumber = -1;

                Int32.TryParse(soundNumberInputTextBox.Text, out soundNumber);

                return soundNumber;
            }
        }

        private async void Initialize()
        {
            PrinterSettings settings = new PrinterSettings();
            await settings.LoadSettings();

            ModelDictionary modelDic = new ModelDictionary();
            PrinterModel model = modelDic.GetModel(settings.GetModelTitle(true));
            PrinterInfo printerInfo = modelDic.GetPrinterInfo(settings.GetModelTitle(true));

            switch (model)
            {
                default:
                    break;

                case PrinterModel.MCP30:
                case PrinterModel.TSP100IV:
                    SpeakerModel = MelodySpeakerModel.MCS10;
                    break;

                case PrinterModel.FVP10:
                    SpeakerModel = MelodySpeakerModel.FVP10;
                    break;
            }

            soundNumberInputTextBox.Text = printerInfo.SoundNumberDefault.ToString();

            if (printerInfo.SoundVolumeMin >= 0 &&
                printerInfo.SoundVolumeMax >= 0 &&
                printerInfo.SoundVolumeDefault >= 0)
            {
                volumeRegisteredSlider.Minimum = printerInfo.SoundVolumeMin;
                volumeRegisteredSlider.Maximum = printerInfo.SoundVolumeMax;
                volumeRegisteredSlider.Value = printerInfo.SoundVolumeDefault;

                volumeReceivedSlider.Minimum = printerInfo.SoundVolumeMin;
                volumeReceivedSlider.Maximum = printerInfo.SoundVolumeMax;
                volumeReceivedSlider.Value = printerInfo.SoundVolumeDefault;
            }
            else
            {
                volumeTypeRegisteredComboBox.IsEnabled = false;
                volumeTypeReceivedComboBox.IsEnabled = false;
            }
        }

        private void SoundStorageAreaComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (soundNumberInputTextBox == null)
            {
                return;
            }

            bool isDefaultSelected = (soundStorageAreaComboBox.SelectedIndex == 0);

            soundNumberInputTextBox.IsEnabled = !isDefaultSelected;
        }

        private void VolumeTypeRegisteredComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (volumeRegisteredSlider == null)
            {
                return;
            }

            bool isManualSelected = ((sender as ComboBox).SelectedIndex == 4);

            volumeRegisteredSlider.IsEnabled = isManualSelected;
        }

        private void VolumeTypeReceivedComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (volumeReceivedSlider == null)
            {
                return;
            }

            bool isManualSelected = ((sender as ComboBox).SelectedIndex == 4);

            volumeReceivedSlider.IsEnabled = isManualSelected;
        }
    }
}
