using StarIO_Extension;
using StarPRNTSDK.Functions;
using System;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Navigation;
using Windows.Storage.Streams;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace StarPRNTSDK
{

    public sealed partial class DisplaySamplePage : Page
    {
        DisplayInternationalType internationalType = DisplayInternationalType.USA;
        DisplayCodePageType codePageType = DisplayCodePageType.CP437;
        private ListViewDataCollection listViewItemCollection;
        private ContentDialogResult contentDialogResult;
        private int number;

        public ListViewContentDialog dlg { get; private set; }

        public DisplaySamplePage()
        {
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
        }

        private void DisplaySampleExt_Tapped(object sender, TappedRoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(DisplayExtSamplePage), null);
        }

        private async void DisplayCheckStatusSample_Tapped(object sender, TappedRoutedEventArgs e)
        {
            var selectedName = (sender as TextBlock).Name;


            var progressRing = new ProgressRing();
            string msg = null;
            string title = "Communication Result";

            try
            {
                PrinterSettings printerSettings = new PrinterSettings();
                PrinterSettingsInfo[] settingsData        = await printerSettings.LoadSettings();
                string portName                 = printerSettings.GetPortName(true);
                string portSettings             = printerSettings.GetPortSettings(true);
                Emulation emulation             = printerSettings.GetEmulation(true);

                progressRing.Message = "Communicating...";
                progressRing.Show();

                using (StarIOPort.Port port = new StarIOPort.Port(30000))
                {
                    msg = "Fail to openPort";
                    await port.ConnectAsync(portName, portSettings);


                    IPeripheralConnectParser parser = StarIoExt.CreateDisplayConnectParser(DisplayModel.SCD222);

                    msg = null;
                    var result = await Communication.ParseDoNotCheckCondition(parser, port);

                    if (result.Result == Communication.Result.Success)
                    {
                        if (parser.IsConnected)
                        {
                            title = "Check Status";
                            msg = "Display Connect.";
                        }
                        else
                        {
                            title = "Check Status";
                            msg = "Display Disconnect.";
                        }
                    }
                    else
                    {
                        title = "Failure";
                        msg = "Printer Impossible";
                    }
                }
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
                Windows.UI.Popups.MessageDialog dialog = new Windows.UI.Popups.MessageDialog(msg, title);

                await dialog.ShowAsync();
            }

        }



        private async void DisplaySample_Tapped(object sender, TappedRoutedEventArgs e)
        {
            var selectedFunctionName = (sender as TextBlock).Name;

            IDisplayCommandBuilder builder = StarIoExt.CreateDisplayCommandBuilder(DisplayModel.SCD222);
            switch (selectedFunctionName)
            {
                case "DisplayText":
                    //Display Dialoag
                    listViewItemCollection = new ListViewDataCollection();
                    listViewItemCollection.Add(new ListViewItemData() { Item = "Pattern 1" });
                    listViewItemCollection.Add(new ListViewItemData() { Item = "Pattern 2" });
                    listViewItemCollection.Add(new ListViewItemData() { Item = "Pattern 3" });
                    listViewItemCollection.Add(new ListViewItemData() { Item = "Pattern 4" });
                    listViewItemCollection.Add(new ListViewItemData() { Item = "Pattern 5" });
                    listViewItemCollection.Add(new ListViewItemData() { Item = "Pattern 6" });

                    dlg = new ListViewContentDialog(listViewItemCollection, "Select Text");
                    contentDialogResult = await dlg.ShowAsync();
                    if (contentDialogResult == ContentDialogResult.Secondary)
                    {
                        return;
                    }
                    number = dlg.SelectedListViewIndex;

                    DisplayFunction.AppendClearScreen(builder);

                    DisplayFunction.AppendCharacterSet(builder, internationalType, codePageType);

                    DisplayFunction.AppendTextPattern(builder, number);

                    break;
                case "DisplayGraphic":
                    //Display Dialoag
                    listViewItemCollection = new ListViewDataCollection();
                    listViewItemCollection.Add(new ListViewItemData() { Item = "Pattern 1" });
                    listViewItemCollection.Add(new ListViewItemData() { Item = "Pattern 2" });

                    dlg = new ListViewContentDialog(listViewItemCollection, "Select Graphic");
                    contentDialogResult = await dlg.ShowAsync();
                    if (contentDialogResult == ContentDialogResult.Secondary)
                    {
                        return;
                    }
                    number = dlg.SelectedListViewIndex;

                    DisplayFunction.AppendClearScreen(builder);

                    await DisplayFunction.ApppendGraphicPattern(builder, number);

                    break;
                case "DisplayTurnOnOff":
                    listViewItemCollection = new ListViewDataCollection();
                    listViewItemCollection.Add(new ListViewItemData() { Item = "Turn  On" });
                    listViewItemCollection.Add(new ListViewItemData() { Item = "Turn Off" });

                    dlg = new ListViewContentDialog(listViewItemCollection, "Select Tutn On/Off");
                    contentDialogResult = await dlg.ShowAsync();
                    if (contentDialogResult == ContentDialogResult.Secondary)
                    {
                        return;
                    }
                    number = dlg.SelectedListViewIndex;

                    //DisplayFunction.AppendClearScreen(builder);

                    switch (number)
                    {
                        default : DisplayFunction.AppendTurnOn(builder, true);  break;     // 0
                        case 1  : DisplayFunction.AppendTurnOn(builder, false); break;
                    }


                    break;
                case "DisplayCursor":
                    //Display Dialoag
                    listViewItemCollection = new ListViewDataCollection();
                    listViewItemCollection.Add(new ListViewItemData() { Item = "Off" });
                    listViewItemCollection.Add(new ListViewItemData() { Item = "Blink" });
                    listViewItemCollection.Add(new ListViewItemData() { Item = "On" });

                    dlg = new ListViewContentDialog(listViewItemCollection, "Select Cursor");
                    contentDialogResult = await dlg.ShowAsync();
                    if (contentDialogResult == ContentDialogResult.Secondary)
                    {
                        return;
                    }
                    number = dlg.SelectedListViewIndex;

                    DisplayFunction.AppendClearScreen(builder);

                    switch (number)
                    {
                        default: DisplayFunction.AppendCursorMode(builder, DisplayCursorMode.Off);   break;
                        case 1:  DisplayFunction.AppendCursorMode(builder, DisplayCursorMode.Blink); break;
                        case 2:  DisplayFunction.AppendCursorMode(builder, DisplayCursorMode.On);    break;
                    }

                    break;
                case "DisplayContrast":
                    //Display Dialoag
                    listViewItemCollection = new ListViewDataCollection();
                    listViewItemCollection.Add(new ListViewItemData() { Item = "Contrast -3" });
                    listViewItemCollection.Add(new ListViewItemData() { Item = "Contrast -2" });
                    listViewItemCollection.Add(new ListViewItemData() { Item = "Contrast -1" });
                    listViewItemCollection.Add(new ListViewItemData() { Item = "Default" });
                    listViewItemCollection.Add(new ListViewItemData() { Item = "Contrast +1" });
                    listViewItemCollection.Add(new ListViewItemData() { Item = "Contrast +2" });
                    listViewItemCollection.Add(new ListViewItemData() { Item = "Contrast +3" });

                    dlg = new ListViewContentDialog(listViewItemCollection, "Select Contrast");
                    contentDialogResult = await dlg.ShowAsync();
                    if (contentDialogResult == ContentDialogResult.Secondary)
                    {
                        return;
                    }
                    number = dlg.SelectedListViewIndex;

                    //DisplayFunction.AppendClearScreen(builder);

                    switch (number)
                    {
                        case 0:  DisplayFunction.AppendContrastMode(builder, DisplayContrastMode.Minus3);  break;
                        case 1:  DisplayFunction.AppendContrastMode(builder, DisplayContrastMode.Minus2);  break;
                        case 2:  DisplayFunction.AppendContrastMode(builder, DisplayContrastMode.Minus1);  break;
                        case 3:
                        default: DisplayFunction.AppendContrastMode(builder, DisplayContrastMode.Default); break;
                        case 4:  DisplayFunction.AppendContrastMode(builder, DisplayContrastMode.Plus1);   break;
                        case 5:  DisplayFunction.AppendContrastMode(builder, DisplayContrastMode.Plus2);   break;
                        case 6:  DisplayFunction.AppendContrastMode(builder, DisplayContrastMode.Plus3);   break;
                    }
                    break;
                case "DisplayCharacterSetInternational":
                    //Display Dialoag
                    listViewItemCollection = new ListViewDataCollection();
                    listViewItemCollection.Add(new ListViewItemData() { Item = "USA" });
                    listViewItemCollection.Add(new ListViewItemData() { Item = "France" });
                    listViewItemCollection.Add(new ListViewItemData() { Item = "Germary" });
                    listViewItemCollection.Add(new ListViewItemData() { Item = "UK" });
                    listViewItemCollection.Add(new ListViewItemData() { Item = "Denmark" });
                    listViewItemCollection.Add(new ListViewItemData() { Item = "Sweden" });
                    listViewItemCollection.Add(new ListViewItemData() { Item = "Italy" });
                    listViewItemCollection.Add(new ListViewItemData() { Item = "Spain" });
                    listViewItemCollection.Add(new ListViewItemData() { Item = "Japan" });
                    listViewItemCollection.Add(new ListViewItemData() { Item = "Norway" });
                    listViewItemCollection.Add(new ListViewItemData() { Item = "Denmark 2" });
                    listViewItemCollection.Add(new ListViewItemData() { Item = "Spain 2" });
                    listViewItemCollection.Add(new ListViewItemData() { Item = "Latin America" });
                    listViewItemCollection.Add(new ListViewItemData() { Item = "Korea" });
                    dlg = new ListViewContentDialog(listViewItemCollection, "Select Character Set(International)");
                    contentDialogResult = await dlg.ShowAsync();
                    if (contentDialogResult == ContentDialogResult.Secondary)
                    {
                        return;
                    }
                    number = dlg.SelectedListViewIndex;

                    switch (number)
                    {
                        case 0x00:
                        default:   internationalType = DisplayInternationalType.USA;          break;
                        case 0x01: internationalType = DisplayInternationalType.France;       break;
                        case 0x02: internationalType = DisplayInternationalType.Germany;      break;
                        case 0x03: internationalType = DisplayInternationalType.UK;           break;
                        case 0x04: internationalType = DisplayInternationalType.Denmark;      break;
                        case 0x05: internationalType = DisplayInternationalType.Sweden;       break;
                        case 0x06: internationalType = DisplayInternationalType.Italy;        break;
                        case 0x07: internationalType = DisplayInternationalType.Spain;        break;
                        case 0x08: internationalType = DisplayInternationalType.Japan;        break;
                        case 0x09: internationalType = DisplayInternationalType.Norway;       break;
                        case 0x0a: internationalType = DisplayInternationalType.Denmark2;     break;
                        case 0x0b: internationalType = DisplayInternationalType.Spain2;       break;
                        case 0x0c: internationalType = DisplayInternationalType.LatinAmerica; break;
                        case 0x0d: internationalType = DisplayInternationalType.Korea;        break;
                    }

                    DisplayFunction.AppendClearScreen(builder);
                    DisplayFunction.AppendCharacterSet(builder, internationalType, codePageType);

                    break;
                case "DisplayCharacterSetCodePage":
                    //Display Dialoag
                    listViewItemCollection = new ListViewDataCollection();
                    listViewItemCollection.Add(new ListViewItemData() { Item = "Code Page 437" });
                    listViewItemCollection.Add(new ListViewItemData() { Item = "Katakana" });
                    listViewItemCollection.Add(new ListViewItemData() { Item = "Code Page 850" });
                    listViewItemCollection.Add(new ListViewItemData() { Item = "Code Page 860" });
                    listViewItemCollection.Add(new ListViewItemData() { Item = "Code Page 863" });
                    listViewItemCollection.Add(new ListViewItemData() { Item = "Code Page 865" });
                    listViewItemCollection.Add(new ListViewItemData() { Item = "Code Page 1252" });
                    listViewItemCollection.Add(new ListViewItemData() { Item = "Code Page 866" });
                    listViewItemCollection.Add(new ListViewItemData() { Item = "Code Page 852" });
                    listViewItemCollection.Add(new ListViewItemData() { Item = "Code Page 858" });
                    listViewItemCollection.Add(new ListViewItemData() { Item = "Japanese" });
                    listViewItemCollection.Add(new ListViewItemData() { Item = "Simplified Chinese" });
                    listViewItemCollection.Add(new ListViewItemData() { Item = "Traditional Chinese" });
                    listViewItemCollection.Add(new ListViewItemData() { Item = "Hangul" });
                    dlg = new ListViewContentDialog(listViewItemCollection, "Select Character Set(Code Page)");
                    contentDialogResult = await dlg.ShowAsync();
                    if (contentDialogResult == ContentDialogResult.Secondary)
                    {
                        return;
                    }
                    number = dlg.SelectedListViewIndex;

                    switch (number)
                    {
                        default:
                        case 0:  codePageType = DisplayCodePageType.CP437;              break;
                        case 1:  codePageType = DisplayCodePageType.Katakana;           break;
                        case 2:  codePageType = DisplayCodePageType.CP850;              break;
                        case 3:  codePageType = DisplayCodePageType.CP860;              break;
                        case 4:  codePageType = DisplayCodePageType.CP863;              break;
                        case 5:  codePageType = DisplayCodePageType.CP865;              break;
                        case 6:  codePageType = DisplayCodePageType.CP1252;             break;
                        case 7:  codePageType = DisplayCodePageType.CP866;              break;
                        case 8:  codePageType = DisplayCodePageType.CP852;              break;
                        case 9:  codePageType = DisplayCodePageType.CP858;              break;
                        case 10: codePageType = DisplayCodePageType.Japanese;           break;
                        case 11: codePageType = DisplayCodePageType.SimplifiedChinese;  break;
                        case 12: codePageType = DisplayCodePageType.TraditionalChinese; break;
                        case 13: codePageType = DisplayCodePageType.Hangul;             break;
                                            }

                    DisplayFunction.AppendClearScreen(builder);
                    DisplayFunction.AppendCharacterSet(builder, internationalType, codePageType);

                    break;
                case "UserDefinedCharacter":
                    //Display Dialoag
                    listViewItemCollection = new ListViewDataCollection();
                    listViewItemCollection.Add(new ListViewItemData() { Item = "Set" });
                    listViewItemCollection.Add(new ListViewItemData() { Item = "Reset" });

                    dlg = new ListViewContentDialog(listViewItemCollection, "Select User Defined Character");
                    contentDialogResult = await dlg.ShowAsync();
                    if (contentDialogResult == ContentDialogResult.Secondary)
                    {
                        return;
                    }
                    number = dlg.SelectedListViewIndex;

                    DisplayFunction.AppendClearScreen(builder);

                    switch (number)
                    {
                        default: DisplayFunction.AppendUserDefinedCharacter(builder, true);  break;
                        case 1 : DisplayFunction.AppendUserDefinedCharacter(builder, false); break;
                    }

                    break;
            }

            var progressRing = new ProgressRing();
            string msg = null;
            string title = "Communication Result";

            try
            {
                PrinterSettings printerSettings = new PrinterSettings();
                PrinterSettingsInfo[] settingsData = await printerSettings.LoadSettings();
                string portName = printerSettings.GetPortName(true);
                string portSettings = printerSettings.GetPortSettings(true);
                Emulation emulation = printerSettings.GetEmulation(true);

                progressRing.Message = "Communicating...";
                progressRing.Show();

                using (StarIOPort.Port port = new StarIOPort.Port(30000))
                {
                    msg = "Fail to openPort";
                    await port.ConnectAsync(portName, portSettings);

                    IPeripheralConnectParser parser = StarIoExt.CreateDisplayConnectParser(DisplayModel.SCD222);

                    msg = null;
                    var result = await Communication.ParseDoNotCheckCondition(parser, port);

                    if (result.Result == Communication.Result.Success)
                    {
                        if (parser.IsConnected)
                        {
                            result = await Communication.SendCommandsDoNotCheckCondition(builder.PassThrouhCommands, port);

                            if (result.Result != Communication.Result.Success)
                            {
                                title = "Failure";
                                msg = "";
                            }
                        }
                        else
                        {
                            title = "Failure";
                            msg = "Display Disconnect.";
                        }
                    }
                    else
                    {
                        title = "Failure";
                        msg = "Printer Impossible";
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

            if (msg != null)
            {
                Windows.UI.Popups.MessageDialog dialog = new Windows.UI.Popups.MessageDialog(msg, title);

                await dialog.ShowAsync();
            }


        }


    }
}
