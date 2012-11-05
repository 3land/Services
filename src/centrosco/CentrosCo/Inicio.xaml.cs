using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Networking.Connectivity;
using Windows.UI.ApplicationSettings;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Animation;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace CentrosCo
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Inicio : Page
    {
        private bool isEventRegistered;
        private Rect windowBounds;
        private double settingsWidth = 400;
        private Popup settingsPopup;
        public Inicio()
        {
            this.InitializeComponent();
            windowBounds = Window.Current.Bounds;
            Window.Current.SizeChanged += OnWindowSizeChanged;
            if (!this.isEventRegistered)
            {
                SettingsPane.GetForCurrentView().CommandsRequested += onCommandsRequested;
                this.isEventRegistered = true;
            }
        }
        private void onCommandsRequested(SettingsPane sender, SettingsPaneCommandsRequestedEventArgs args)
        {
            UICommandInvokedHandler handler = new UICommandInvokedHandler(onSettingsCommand);
            SettingsCommand generalCommand = new SettingsCommand("DefaultsId", "Politica de privacidad", handler);
            args.Request.ApplicationCommands.Add(generalCommand);
        }
        private void onSettingsCommand(IUICommand command)
        {
            settingsPopup = new Popup(); settingsPopup.Closed += OnPopupClosed;
            Window.Current.Activated += OnWindowActivated;
            settingsPopup.IsLightDismissEnabled = true;
            settingsPopup.Width = settingsWidth;
            settingsPopup.Height = windowBounds.Height;
            settingsPopup.ChildTransitions = new TransitionCollection();
            settingsPopup.ChildTransitions.Add(new PaneThemeTransition()
            {
                Edge = (SettingsPane.Edge == SettingsEdgeLocation.Right) ? EdgeTransitionLocation.Right : EdgeTransitionLocation.Left
            });
            SettingsFlyout mypane = new SettingsFlyout();
            mypane.Width = settingsWidth;
            mypane.Height = windowBounds.Height;
            settingsPopup.Child = mypane;
            settingsPopup.SetValue(Canvas.LeftProperty, SettingsPane.Edge == SettingsEdgeLocation.Right ? (windowBounds.Width - settingsWidth) : 0);
            settingsPopup.SetValue(Canvas.TopProperty, 0);
            settingsPopup.IsOpen = true;
        }
        private void OnWindowActivated(object sender, Windows.UI.Core.WindowActivatedEventArgs e)
        {
            if (e.WindowActivationState == Windows.UI.Core.CoreWindowActivationState.Deactivated)
            {
                settingsPopup.IsOpen = false;
            }
        }

        private void OnPopupClosed(object sender, object e)
        {
            Window.Current.Activated -= OnWindowActivated;
        }

        private void OnWindowSizeChanged(object sender, Windows.UI.Core.WindowSizeChangedEventArgs e)
        {
            windowBounds = Window.Current.Bounds;
        }

        /// <summary>
        /// Invoked when this page is about to be displayed in a Frame.
        /// </summary>
        /// <param name="e">Event data that describes how this page was reached.  The Parameter
        /// property is typically used to configure the page.</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
        }

        private void Tapped_1(object sender, TappedRoutedEventArgs e)
        {
            var control = sender as Image;
            String envia = control.Tag.ToString();
            buscar(envia);
        }

        private void buscar(string envia)
        {
            if (HayInternet)
            {
                if (envia == "sector")
                {
                    this.Frame.Navigate(typeof(BasicPage1));
                }
                else if(envia == "cine")
                {
                    this.Frame.Navigate(typeof(Cine));
                }
                else if (envia == "cercano")
                {
                    this.Frame.Navigate(typeof(Cercano));
                }
            }
            else
            {
                MessageDialog msg = new MessageDialog("Esta aplicación requiere acceso a Internet");
                msg.ShowAsync();
            }
        }
        public bool HayInternet
        {
            get
            {
                var prof = NetworkInformation.GetInternetConnectionProfile();
                return prof != null && prof.GetNetworkConnectivityLevel() == NetworkConnectivityLevel.InternetAccess;
            }
        }
    }
}
