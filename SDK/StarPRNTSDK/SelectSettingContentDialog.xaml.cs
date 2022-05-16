using System.Collections.Generic;
using Windows.UI.Xaml.Controls;

namespace StarPRNTSDK
{
    public sealed partial class SelectSettingContentDialog : ContentDialog
    {
        public struct SelectSettingContentDialogResult
        {
            public int selectedIndex { get; }
            public bool isCanceled { get; }

            public SelectSettingContentDialogResult(int selectedIndex, bool isCanceled)
            {
                this.selectedIndex = selectedIndex;
                this.isCanceled = isCanceled;
            }
        }

        private int selectedIndex;
        private List<RadioButton> radioButtonList;

        public SelectSettingContentDialog(string title, List<string> settingsList)
        {
            this.InitializeComponent();

            this.Title = title;

            this.selectedIndex = 0;

            this.SetSettingsList(settingsList);
        }

        private void SetSettingsList(List<string> settingsList)
        {
            this.SetRadioButton(settingsList);
        }

        private void SetRadioButton(List<string> settingsList)
        {
            this.radioButtonList = new List<RadioButton>();

            int index = 0;

            foreach (string setting in settingsList)
            {
                RadioButton radioButton = new RadioButton();

                radioButton.Content = setting;

                if (index == 0)
                {
                    radioButton.IsChecked = true;
                }

                this.SettingStackPanel.Children.Add(radioButton);

                this.radioButtonList.Add(radioButton);

                index++;
            }
        }

        public void SetRadioButtonCheckedIndex(int index)
        {
            RadioButton radioButton = this.radioButtonList[index];

            radioButton.IsChecked = true;
        }

        public int GetSelectedIndex()
        {
            return this.selectedIndex;
        }

        private void SetSelectedRadioButtonIndex()
        {
            int selectedIndex = 0;

            foreach(RadioButton radioButton in this.radioButtonList)
            {
                if(radioButton.IsChecked != null)
                {
                    bool isChecked = (bool)radioButton.IsChecked;

                    if (isChecked)
                    {
                        this.selectedIndex = selectedIndex;

                        break;
                    }
                }

                selectedIndex++;
            }
        }

        private void ContentDialog_PrimaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
            this.SetSelectedRadioButtonIndex();
        }

        private void ContentDialog_SecondaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
        }
    }
}
