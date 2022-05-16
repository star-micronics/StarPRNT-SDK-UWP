using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;


namespace StarPRNTSDK
{
    public sealed partial class ListViewContentDialog : ContentDialog
    {
        public ListViewContentDialog(ListViewDataCollection sampleCollection, string title)
        {
            this.InitializeComponent();

            this.Title = title;

            this.listView1.ItemsSource = sampleCollection;

            var h = Windows.UI.Xaml.Window.Current.Bounds.Height;
            ScrollViewerList.Height = Windows.UI.Xaml.Window.Current.Bounds.Height - 200;
        }

        public int SelectedListViewIndex { get; private set; }

        private void ContentDialog_PrimaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {

        }

        private void ContentDialog_SecondaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
        }

        private void listView1_Tapped(object sender, TappedRoutedEventArgs e)
        {
            SelectedListViewIndex = (listView1).SelectedIndex;

            this.Hide();

        }
    }
}
