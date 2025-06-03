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

//コンテンツ ダイアログ項目テンプレートについては、http://go.microsoft.com/fwlink/?LinkId=234238 を参照してください。

namespace StarPRNTSDK
{
    public sealed partial class LisenceDialog : ContentDialog
    {
        public LisenceDialog()
        {
            this.InitializeComponent();

            Title = "Open Source Lisence";

            TextBlock textBlock = new TextBlock();

            textBlock.Text = "This application contains deliverables distributed under license of Apache License, Version 2.0 (    \n" +
                   "http://www.apache.org/licenses/LICENSE-2.0).\n" +
                   "\n" +
                   "Notices for libraries:\n" +
                   "[ZXing]\n" +
                   "------------------------------\n" +
                   "NOTICES FOR BARCODE 4J\n" +
                   "------------------------------\n" +
                   "Barcode 4J\n" +
                   "Copyright 2002-2010 Jeremias Märki\n" +
                   "Copyright 2005-2006 Dietmar Bürkle\n" +
                   "\n" +
                   "Portions of this software were contributed under section 5 of the\n" +
                   "Apache License. Contributors are listed under:\n" +
                   "http://barcode4j.sourceforge.net/contributors.html\n" +
                   "------------------------------\n" +
                   "NOTICES FOR JCOMMANDER\n" +
                   "------------------------------\n" +
                   "Copyright 2010 Cedric Beust cedric@beust.com";

            this.LisenceStackPanel.Children.Add(textBlock);
        }

        private void ContentDialog_PrimaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
        }

    }
}
