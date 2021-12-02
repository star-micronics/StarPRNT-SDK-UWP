using System;
using Windows.ApplicationModel;
using Windows.ApplicationModel.Activation;
using Windows.Graphics.Display;
using Windows.System.Profile;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace StarPRNTSDK
{

    sealed partial class App : Application
    {
        public bool backButtonEnabled;

        public App()
        {
            this.InitializeComponent();

            this.backButtonEnabled = true;

            this.Suspending += OnSuspending;
        }


        protected override void OnLaunched(LaunchActivatedEventArgs e)
        {

#if DEBUG
            if (System.Diagnostics.Debugger.IsAttached)
            {
                this.DebugSettings.EnableFrameRateCounter = true;
            }
#endif

            Frame rootFrame = Window.Current.Content as Frame;


            if (rootFrame == null)
            {
                rootFrame = new Frame();

                rootFrame.Navigated += OnNavigated;

                rootFrame.Language = Windows.Globalization.ApplicationLanguages.Languages[0];

                rootFrame.NavigationFailed += OnNavigationFailed;

                if (e.PreviousExecutionState == ApplicationExecutionState.Terminated)
                {
                }

                Window.Current.Content = rootFrame;

                SystemNavigationManager.GetForCurrentView().BackRequested += OnBackRequested;
            }

            if (rootFrame.Content == null)
            {
                rootFrame.Navigate(typeof(MainPage), e.Arguments);
            }

            Window.Current.Activate();
        }

        void OnNavigationFailed(object sender, NavigationFailedEventArgs e)
        {
            throw new Exception("Failed to load Page " + e.SourcePageType.FullName);
        }


        private void OnSuspending(object sender, SuspendingEventArgs e)
        {
            var deferral = e.SuspendingOperation.GetDeferral();
            deferral.Complete();
        }

        private void SetDisplayOrientation()
        {
            if (AnalyticsInfo.VersionInfo.DeviceFamily.Equals("Windows.Mobile"))
            {
                DisplayInformation.AutoRotationPreferences = DisplayOrientations.Portrait;
            }
        }

        private void ShowBackButton(Frame frame)
        {
            if (frame.Content is MainPage || !frame.CanGoBack) // Current page is MainPage or Cannot go back page
            {
                SystemNavigationManager.GetForCurrentView().AppViewBackButtonVisibility = AppViewBackButtonVisibility.Collapsed;
            }
            else
            {
                SystemNavigationManager.GetForCurrentView().AppViewBackButtonVisibility = AppViewBackButtonVisibility.Visible;
            }
        }

        private void GoBackPage(BackRequestedEventArgs e)
        {
            Frame rootFrame = Window.Current.Content as Frame;

            if (rootFrame.Content is MainPage)
            {
                return;
            }

            if (rootFrame.CanGoBack)
            {
                rootFrame.GoBack();

                e.Handled = true;
            }
        }

        private void OnNavigated(object sender, NavigationEventArgs e)
        {
            Frame rootFrame = (Frame)sender;

            this.ShowBackButton(rootFrame);

            this.SetDisplayOrientation();
        }

        private void OnBackRequested(object sender, BackRequestedEventArgs e)
        {
            if (backButtonEnabled)
            {
                this.GoBackPage(e);
            }
            else
            {
                e.Handled = true;
            }
        }
    }
}
