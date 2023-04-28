using System;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;

namespace StarPRNTSDK
{
    public sealed partial class ProgressRing : UserControl
    {
        private Page currentPage;
        private Popup popup = new Popup();

        public static readonly DependencyProperty MessageProperty
            = DependencyProperty.Register(
            "Message",
            typeof(string),
            typeof(ProgressRing),
            new PropertyMetadata(
                null,
                (s, e) =>
                {
                    var control = s as ProgressRing;
                    if (control != null)
                    {
                        control.OnMessageChanged();
                    }
                }));
        private void OnMessageChanged()
        {
            this.Message_Part.Text = this.Message;
        }

        public string Message
        {
            get { return (string)this.GetValue(MessageProperty); }
            set { this.SetValue(MessageProperty, value); }
        }


        public ProgressRing()
        {
            this.InitializeComponent();

            this.Width = Window.Current.Bounds.Width;
            this.Height = Window.Current.Bounds.Height;

            this.Loaded += this.OnLoaded;
            this.Unloaded += this.OnUnloaded;
        }

        private async void OnLoaded(object sender, RoutedEventArgs e)
        {
            await this.Dispatcher.RunAsync(
                CoreDispatcherPriority.Normal,
                () =>
                {
                    var rootrame = Window.Current.Content as Frame;
                    if (rootrame == null)
                    {
                        return;
                    }
                    this.currentPage = rootrame.Content as Page;
                    if (this.currentPage == null)
                    {
                        return;
                    }
                });
        }

        public void Show()
        {
            this.Show(true);
        }

        public void Show(bool showProgressRing)
        {
            if(showProgressRing)
            {
                ring.IsActive = true;
            }
            else
            {
                ring.IsActive = false;
            }

            this.ShowInternal();
        }

        private void ShowInternal()
        {
            (Application.Current as App).backButtonEnabled = false;

            this.popup.Child = this;
            this.popup.IsOpen = true;
            this.popup.Closed += this.OnClosed;
            Window.Current.SizeChanged += this.OnWindowSizeChanged;

            if (this.currentPage == null)
            {
                return;
            }
        }

        private async void OnWindowSizeChanged(object sender, WindowSizeChangedEventArgs e)
        {
            await this.Dispatcher.RunAsync(
                            CoreDispatcherPriority.Normal,
                            () =>
                            {
                                this.Width = Window.Current.Bounds.Width;
                                this.Height = Window.Current.Bounds.Height;
                            });
        }

        public void Hide()
        {
            (Application.Current as App).backButtonEnabled = true;

            if (this.popup != null)
            {
                this.popup.IsOpen = false;

                if (this.currentPage == null)
                {
                    return;
                }

                this.currentPage.IsEnabled = true;
                this.currentPage.IsHitTestVisible = true;
            }

        }

        private void OnClosed(object sender, object e)
        {
            this.popup.Child = null;
            this.popup = null;
        }

        private async void OnUnloaded(object sender, RoutedEventArgs e)
        {
            this.Unloaded -= this.OnUnloaded;
            this.Loaded   -= this.OnLoaded;

            this.popup.Closed -= this.OnClosed;
            this.popup.Child = null;
            this.popup = null;
            Window.Current.SizeChanged -= this.OnWindowSizeChanged;

            await this.Dispatcher.RunAsync(
                CoreDispatcherPriority.Normal,
                () =>
                {
                    if (this.currentPage == null)
                    {
                        return;
                    }

                    this.currentPage = null;
                });
        }

    }
}
