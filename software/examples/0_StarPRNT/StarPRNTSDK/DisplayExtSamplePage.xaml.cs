using StarIO_Extension;
using StarPRNTSDK.Functions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading;
using System.Threading.Tasks;
using Windows.Storage.Streams;
using Windows.UI;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Animation;
using Windows.UI.Xaml.Navigation;


namespace StarPRNTSDK
{
    public enum DisplayStatus : int
    {
        DisplayStatusInvalid = 0,
        DisplayStatusImpossible,
        DisplayStatusConnect,
        DisplayStatusDisconnect
    }

    public sealed partial class DisplayExtSamplePage : Page
    {
        

        private StarIOPort.Port port = null;
        private DisplayStatus dispalyStatus = DisplayStatus.DisplayStatusInvalid;

        private Task watchDisplayTask = null;
        private CancellationTokenSource watchDisplayCancellationToken = null;
        private SemaphoreSlim semaphore = null;

        private string selectedFunctionName = null;
        private int internationalIndex = 0;
        private int codePageIndex = 0;
        private bool portValid;

        public DisplayExtSamplePage()
        {
            this.InitializeComponent();
        }

        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            semaphore = new SemaphoreSlim(1, 1);
            watchDisplayCancellationToken = new CancellationTokenSource();
            (this.Resources["Storyboard1"] as Storyboard).Begin();

            var progressRing = new ProgressRing();
            progressRing.Message = "Communicating...";
            progressRing.Show();
            if (await Connect())
            {
                progressRing.Hide();
                watchDisplayTask = Task.Factory.StartNew(this.WatchDisplayAsync, watchDisplayCancellationToken.Token);
            }
            else
            {
                progressRing.Hide();
                Windows.UI.Popups.MessageDialog dialog = new Windows.UI.Popups.MessageDialog("Failed to Open Port", "Communication Result");

                await dialog.ShowAsync();
            }

        }


        protected override  void OnNavigatedFrom(NavigationEventArgs e)
        {
            if (watchDisplayCancellationToken != null)
            {
                watchDisplayCancellationToken.Cancel();
            }

            (this.Resources["Storyboard1"] as Storyboard).Stop();

            Disconnect();
        }

        private DisplayCursorMode GetCursorMode(int cursorIndex)
        {
            DisplayCursorMode cursorMode = DisplayCursorMode.Off;

            switch (cursorIndex)
            {
                case 0: cursorMode = DisplayCursorMode.Off; break;
                case 1: cursorMode = DisplayCursorMode.Blink; break;
                case 2: cursorMode = DisplayCursorMode.On; break;
            }

            return cursorMode;
        }

        private DisplayContrastMode GetContrastMode(int contrastIndex)
        {
            DisplayContrastMode contrastMode = DisplayContrastMode.Default;

            switch (contrastIndex)
            {
                case 0: contrastMode = DisplayContrastMode.Minus3; break;
                case 1: contrastMode = DisplayContrastMode.Minus2; break;
                case 2: contrastMode = DisplayContrastMode.Minus1; break;
                case 3: contrastMode = DisplayContrastMode.Default; break;
                case 4: contrastMode = DisplayContrastMode.Plus1; break;
                case 5: contrastMode = DisplayContrastMode.Plus2; break;
                case 6: contrastMode = DisplayContrastMode.Plus3; break;

            }

            return contrastMode;
        }

        private DisplayInternationalType GetInternationalType(int internationalIndex)
        {
            DisplayInternationalType internationalType = DisplayInternationalType.USA;

            switch (internationalIndex)
            {
                case 0: internationalType  = DisplayInternationalType.USA;          break;
                case 1: internationalType  = DisplayInternationalType.France;       break;
                case 2: internationalType  = DisplayInternationalType.Germany;      break;
                case 3: internationalType  = DisplayInternationalType.UK;           break;
                case 4: internationalType  = DisplayInternationalType.Denmark;      break;
                case 5: internationalType  = DisplayInternationalType.Sweden;       break;
                case 6: internationalType  = DisplayInternationalType.Italy;        break;
                case 7: internationalType  = DisplayInternationalType.Spain;        break;
                case 8: internationalType  = DisplayInternationalType.Japan;        break;
                case 9: internationalType  = DisplayInternationalType.Norway;       break;
                case 10: internationalType = DisplayInternationalType.Denmark2;     break;
                case 11: internationalType = DisplayInternationalType.Spain2;       break;
                case 12: internationalType = DisplayInternationalType.LatinAmerica; break;
                case 13: internationalType = DisplayInternationalType.Korea;        break;
            }

            return internationalType;
        }

        private DisplayCodePageType GetCodePageType(int codePageIndex)
        {
            DisplayCodePageType codePageType = DisplayCodePageType.CP437;

            switch (codePageIndex)
            {
                case 0: codePageType = DisplayCodePageType.CP437; break;
                case 1: codePageType = DisplayCodePageType.Katakana; break;
                case 2: codePageType = DisplayCodePageType.CP850; break;
                case 3: codePageType = DisplayCodePageType.CP860; break;
                case 4: codePageType = DisplayCodePageType.CP863; break;
                case 5: codePageType = DisplayCodePageType.CP865; break;
                case 6: codePageType = DisplayCodePageType.CP1252; break;
                case 7: codePageType = DisplayCodePageType.CP866; break;
                case 8: codePageType = DisplayCodePageType.CP852; break;
                case 9: codePageType = DisplayCodePageType.CP858; break;
                case 10: codePageType = DisplayCodePageType.Japanese; break;
                case 11: codePageType = DisplayCodePageType.SimplifiedChinese; break;
                case 12: codePageType = DisplayCodePageType.TraditionalChinese; break;
                case 13: codePageType = DisplayCodePageType.Hangul; break;
            }

            return codePageType;

        }

        private void TextBlock_Tapped(object sender, TappedRoutedEventArgs e)
        {
            selectedFunctionName = (sender as TextBlock).Name;

            this.SetComboBox();

            this.DoFunction();
        }

        private void SetComboBox()
        {
            this.ClearComboBox();

            switch (selectedFunctionName)
            {
                case "DisplayText":
                    this.SetComboBoxForDisplayText();
                    break;

                case "DisplayGraphic":
                    this.SetComboBoxForDisplayGraphic();
                    break;

                case "DisplayTurnOnOff":
                    this.SetComboBoxForDisplayTurnOnOff();
                    break;

                case "DisplayCursor":
                    this.SetComboBoxForDisplayCursor();
                    break;

                case "DisplayContrast":
                    this.SetComboBoxForDisplayContrast();
                    break;

                case "DisplayCharacterSet":
                    this.SetComboBoxForDisplayCharacterSet();
                    break;

                case "UserDefinedCharacter":
                    this.SetComboBoxForUserDefinedCharacter();
                    break;
            }

            pattern1ComboBox.SelectionChanged += pattern1ComboBox_SelectionChanged;
            pattern2ComboBox.SelectionChanged += pattern2ComboBox_SelectionChanged;
        }

        private void ClearComboBox()
        {
            pattern1ComboBox.SelectionChanged -= pattern1ComboBox_SelectionChanged;
            pattern2ComboBox.SelectionChanged -= pattern2ComboBox_SelectionChanged;

            Pattern1ListSource.Source = new List<string>();
            Pattern2ListSource.Source = new List<string>();
        }

        private void SetComboBoxForDisplayText()
        {
            List<string> patter1List = new List<string>();

            patter1List.Add("Pattern1");
            patter1List.Add("Pattern2");
            patter1List.Add("Pattern3");
            patter1List.Add("Pattern4");
            patter1List.Add("Pattern5");
            patter1List.Add("Pattern6");

            Pattern1ListSource.Source = patter1List;

            pattern1ComboBox.SelectedIndex = 0;

            pattern1ComboBox.Visibility = Windows.UI.Xaml.Visibility.Visible;
            pattern2ComboBox.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
        }

        private void SetComboBoxForDisplayGraphic()
        {
            List<string> patter1List = new List<string>();

            patter1List.Add("Pattern1");
            patter1List.Add("Pattern2");

            Pattern1ListSource.Source = patter1List;

            pattern1ComboBox.SelectedIndex = 0;

            pattern1ComboBox.Visibility = Windows.UI.Xaml.Visibility.Visible;
            pattern2ComboBox.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
        }

        private void SetComboBoxForDisplayTurnOnOff()
        {
            List<string> patter1List = new List<string>();

            patter1List.Add("Turn On");
            patter1List.Add("Turn Off");

            Pattern1ListSource.Source = patter1List;

            pattern1ComboBox.SelectedIndex = 0;

            pattern1ComboBox.Visibility = Windows.UI.Xaml.Visibility.Visible;
            pattern2ComboBox.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
        }

        private void SetComboBoxForDisplayCursor()
        {
            List<string> patter1List = new List<string>();

            patter1List.Add("Off");
            patter1List.Add("Blink");
            patter1List.Add("On");

            Pattern1ListSource.Source = patter1List;

            pattern1ComboBox.SelectedIndex = 0;

            pattern1ComboBox.Visibility = Windows.UI.Xaml.Visibility.Visible;
            pattern2ComboBox.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
        }

        private void SetComboBoxForDisplayContrast()
        {
            List<string> patter1List = new List<string>();

            patter1List.Add("Contrast -3");
            patter1List.Add("Contrast -2");
            patter1List.Add("Contrast -1");
            patter1List.Add("Default");
            patter1List.Add("Contrast +1");
            patter1List.Add("Contrast +2");
            patter1List.Add("Contrast +3");

            Pattern1ListSource.Source = patter1List;

            pattern1ComboBox.SelectedIndex = 3;

            pattern1ComboBox.Visibility = Windows.UI.Xaml.Visibility.Visible;
            pattern2ComboBox.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
        }

        private void SetComboBoxForDisplayCharacterSet()
        {
            List<string> patter1List = new List<string>();

            patter1List.Add("USA");
            patter1List.Add("France");
            patter1List.Add("Germany");
            patter1List.Add("UK");
            patter1List.Add("Denmark");
            patter1List.Add("Sweden");
            patter1List.Add("Italy");
            patter1List.Add("Spain");
            patter1List.Add("Japan");
            patter1List.Add("Norway");
            patter1List.Add("Denmark 2");
            patter1List.Add("Spain 2");
            patter1List.Add("Latin America");
            patter1List.Add("Korea");

            Pattern1ListSource.Source = patter1List;

            pattern1ComboBox.SelectedIndex = 0;

            List<string> patter2List = new List<string>();

            patter2List.Add("Code Page 437");
            patter2List.Add("Katakana");
            patter2List.Add("Code Page 850");
            patter2List.Add("Code Page 860");
            patter2List.Add("Code Page 863");
            patter2List.Add("Code Page 865");
            patter2List.Add("Code Page 1252");
            patter2List.Add("Code Page 866");
            patter2List.Add("Code Page 852");
            patter2List.Add("Code Page 858");
            patter2List.Add("Japanese");
            patter2List.Add("Simplified Chinese");
            patter2List.Add("Traditional Chinese");
            patter2List.Add("Hangul");

            Pattern2ListSource.Source = patter2List;

            pattern2ComboBox.SelectedIndex = 0;

            pattern1ComboBox.Visibility = Windows.UI.Xaml.Visibility.Visible;
            pattern2ComboBox.Visibility = Windows.UI.Xaml.Visibility.Visible;
        }

        private void SetComboBoxForUserDefinedCharacter()
        {
            List<string> patter1List = new List<string>();

            patter1List.Add("Set");
            patter1List.Add("Reset");

            Pattern1ListSource.Source = patter1List;

            pattern1ComboBox.SelectedIndex = 0;

            pattern1ComboBox.Visibility = Windows.UI.Xaml.Visibility.Visible;
            pattern2ComboBox.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
        }

        private void pattern1ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            this.DoFunction();
        }

        private void pattern2ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            this.DoFunction();
        }

        private void DoFunction()
        {
            int patternIndex = pattern1ComboBox.SelectedIndex;

            this.SendCommands(selectedFunctionName, patternIndex);
        }

        private async void SendCommands(string selectedFunctionName, int patternIndex)
        {
            var progressRing = new ProgressRing();
            CommunicationResult result = new CommunicationResult();
            string message = null;
            string title = "Communication Result";

            try
            {
                await semaphore.WaitAsync();

                PrinterSettings printerSettings = new PrinterSettings();
                PrinterSettingsInfo[] settingsData = await printerSettings.LoadSettings();
                Emulation emulation = printerSettings.GetEmulation(true);

                IBuffer commands = await CreateCommands(selectedFunctionName, patternIndex);

                progressRing.Message = "Communicating...";
                progressRing.Show();

                if (dispalyStatus == DisplayStatus.DisplayStatusConnect)
                {
                    result = await Communication.SendCommandsDoNotCheckCondition(commands, port);

                    if (result.Result != Communication.Result.Success)
                    {
                        title = "Failure";
                    }

                    message = Communication.GetCommunicationResultMessage(result);
                }
                else
                {
                    message = "Display Disconnect.";
                }
            }
            catch (Exception)
            {

            }
            finally
            {
                progressRing.Hide();
                semaphore.Release();
            }

            if (result.Result != Communication.Result.Success)
            {
                Windows.UI.Popups.MessageDialog dialog = new Windows.UI.Popups.MessageDialog(message, title);

                await dialog.ShowAsync();
            }
        }

        private async Task<IBuffer> CreateCommands(string selectedFunctionName, int patternIndex)
        {
            IBuffer commands = null;
            List<byte> commandsList = new List<byte>();

            switch (selectedFunctionName)
            {
                case "DisplayText":
                    commands = this.CreateCommandsForDisplayText(patternIndex, this.internationalIndex, this.codePageIndex);
                    break;

                case "DisplayGraphic":
                    commands = await CreateCommandsForDisplayGraphic(patternIndex);
                    break;

                case "DisplayTurnOnOff":
                    commands = this.CreateCommandsForDisplayTurnOnOff(patternIndex);
                    break;

                case "DisplayCursor":
                    commands = this.CreateCommandsForDisplayCursor(patternIndex);
                    break;

                case "DisplayContrast":
                    commands = this.CreateCommandsForDisplayContrast(patternIndex);
                    break;

                case "DisplayCharacterSet":
                    this.internationalIndex = patternIndex;
                    this.codePageIndex = pattern2ComboBox.SelectedIndex;
                    commands = this.CreateCommandsForDisplayCharacterSet(this.internationalIndex, this.codePageIndex);
                    break;

                case "UserDefinedCharacter":
                    commands = this.CreateCommandsForUserDefinedCharacter(patternIndex);
                    break;
            }

            return commands;
        }

        private IBuffer CreateCommandsForDisplayText(int patternIndex, int internationalIndex, int codePageIndex)
        {
            IDisplayCommandBuilder builder = StarIoExt.CreateDisplayCommandBuilder(DisplayModel.SCD222);

            DisplayFunction.AppendClearScreen(builder);
            DisplayFunction.AppendCharacterSet(builder, GetInternationalType(internationalIndex), GetCodePageType(codePageIndex));
            DisplayFunction.AppendTextPattern(builder, patternIndex);

            return builder.PassThrouhCommands;
        }

        private async Task<IBuffer> CreateCommandsForDisplayGraphic(int patternIndex)
        {
            IDisplayCommandBuilder builder = StarIoExt.CreateDisplayCommandBuilder(DisplayModel.SCD222);

            DisplayFunction.AppendClearScreen(builder);
            await DisplayFunction.ApppendGraphicPattern(builder, patternIndex);

            return builder.PassThrouhCommands;
        }

        private IBuffer CreateCommandsForDisplayTurnOnOff(int patternIndex)
        {
            IDisplayCommandBuilder builder = StarIoExt.CreateDisplayCommandBuilder(DisplayModel.SCD222);

            switch (patternIndex)
            {
                default: DisplayFunction.AppendTurnOn(builder, true);  break;
                case 1:  DisplayFunction.AppendTurnOn(builder, false); break;
            }

            return builder.PassThrouhCommands;
        }

        private IBuffer CreateCommandsForDisplayCursor(int patternIndex)
        {
            IDisplayCommandBuilder builder = StarIoExt.CreateDisplayCommandBuilder(DisplayModel.SCD222);

            DisplayFunction.AppendClearScreen(builder);
            DisplayFunction.AppendCursorMode(builder, GetCursorMode(patternIndex));

            return builder.PassThrouhCommands;
        }

        private IBuffer CreateCommandsForDisplayContrast(int patternIndex)
        {
            IDisplayCommandBuilder builder = StarIoExt.CreateDisplayCommandBuilder(DisplayModel.SCD222);

            DisplayFunction.AppendContrastMode(builder, GetContrastMode(patternIndex));

            return builder.PassThrouhCommands;
        }

        private IBuffer CreateCommandsForDisplayCharacterSet(int patternIndex, int codePageIndex)
        {
            IDisplayCommandBuilder builder = StarIoExt.CreateDisplayCommandBuilder(DisplayModel.SCD222);

            DisplayFunction.AppendClearScreen(builder);
            DisplayFunction.AppendCharacterSet(builder, GetInternationalType(patternIndex), GetCodePageType(codePageIndex));

            return builder.PassThrouhCommands;
        }

        private IBuffer CreateCommandsForUserDefinedCharacter(int patternIndex)
        {
            IDisplayCommandBuilder builder = StarIoExt.CreateDisplayCommandBuilder(DisplayModel.SCD222);

            DisplayFunction.AppendClearScreen(builder);

            switch (patternIndex)
            {
                default: DisplayFunction.AppendUserDefinedCharacter(builder, true);  break;
                case 1:  DisplayFunction.AppendUserDefinedCharacter(builder, false); break;
            }

            return builder.PassThrouhCommands;
        }

        private async Task<bool> Connect()
        {
            bool result = false;
            try
            {
                PrinterSettings printerSettings = new PrinterSettings();
                PrinterSettingsInfo[] settingsData = await printerSettings.LoadSettings();
                string portName = printerSettings.GetPortName(true);
                string portSettings = printerSettings.GetPortSettings(true);

                if (port == null)
                {
                    port = new StarIOPort.Port();

                    await port.ConnectAsync(portName, portSettings);

                    StarIOPort.Status status = await port.GetParsedStatusAsync();

                    result = true;
                    portValid = true;
                }

            }
            catch (Exception)
            {
                Disconnect();
            }

            return result;
        }

        private bool Disconnect()
        {
            bool result = false;
            if (port != null)
            {
                port.Close();
                port = null;

                result = true;
                portValid = false;
                dispalyStatus = DisplayStatus.DisplayStatusInvalid;

            }

            return result;
        }

        private async void WatchDisplayAsync()
        {
            while (true)
            {
                await semaphore.WaitAsync();

                try
                {
                    if (watchDisplayCancellationToken.IsCancellationRequested)
                    {
                        watchDisplayCancellationToken.Token.ThrowIfCancellationRequested();
                    }


                    if (portValid == true)
                    {
                        IPeripheralConnectParser parser = StarIoExt.CreateDisplayConnectParser(DisplayModel.SCD222);
                        var result = await Communication.ParseDoNotCheckCondition(parser, port);


                        if (result.Result == Communication.Result.Success)
                        {
                            if (parser.IsConnected)
                            {
                                if (dispalyStatus != DisplayStatus.DisplayStatusConnect)
                                {
                                    dispalyStatus = DisplayStatus.DisplayStatusConnect;
                                    await Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal, () =>
                                    {
                                        StatusText.Text = "";
                                        //StatusText.Foreground = new SolidColorBrush(Colors.Red);
                                    });
                                }
                            }
                            else
                            {
                                if (dispalyStatus != DisplayStatus.DisplayStatusDisconnect)
                                {
                                    dispalyStatus = DisplayStatus.DisplayStatusDisconnect;
                                    await Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal, () =>
                                    {
                                        StatusText.Text = "Display Disconnect";
                                        StatusText.Foreground = new SolidColorBrush(Colors.Red);
                                    });
                                }
                            }

                        }
                        else
                        {
                            if (dispalyStatus != DisplayStatus.DisplayStatusDisconnect)
                            {
                                dispalyStatus = DisplayStatus.DisplayStatusDisconnect;
                                await Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal, () =>
                                {
                                    StatusText.Text = "Printer Impossible";
                                    StatusText.Foreground = new SolidColorBrush(Colors.Red);
                                });
                            }
                        }
                    }
                    else
                    {
                        if (dispalyStatus != DisplayStatus.DisplayStatusImpossible)
                        {
                            dispalyStatus = DisplayStatus.DisplayStatusImpossible;
                            await Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal, () =>
                            {
                                StatusText.Text = "Printer Impossible.";
                                StatusText.Foreground = new SolidColorBrush(Colors.Red);
                            });
                        }
                    }

                }
                catch (OperationCanceledException)
                {
                    if (watchDisplayCancellationToken != null)
                    {
                        watchDisplayCancellationToken.Dispose();
                        watchDisplayCancellationToken = null;
                    }

                    break;
                }
                catch (Exception)
                {

                }
                finally
                {
                    semaphore.Release();
                }

                await Task.Delay(1000);
            }
        }
        
    }
}
