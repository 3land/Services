using CentrosCo.ServiceReference1;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Networking.Connectivity;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Basic Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234237

namespace CentrosCo
{
    /// <summary>
    /// A basic page that provides characteristics common to most applications.
    /// </summary>
    public sealed partial class CinePage : CentrosCo.Common.LayoutAwarePage
    {
        string cine;
        public CinePage()
        {
            this.InitializeComponent();
        }
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            if (e.Parameter != null)
            {
                cine = ((String)e.Parameter);
                pageTitle.Text = ((String)e.Parameter);
                listaCine(cine);

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

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            
        }
        private void listaCine(string cine)
        {
            if (HayInternet)
            {
                Service1Client proxy = new Service1Client();
                var lista = proxy.listaCineAsync(cine).Result;

                List<CentrosComerciale> lis = new List<CentrosComerciale>();
                CentrosComerciale l = new CentrosComerciale();

                int i = lista.Count;
                for (int j = 0; j < i; j++)
                {
                    l = new CentrosComerciale();
                    l.nombreCentro = lista[j].nombreCentro.ToString();
                    l.direccion = lista[j].direccion.ToString();
                    l.cine = lista[j].cine.ToString();
                    l.tiendas = lista[j].tiendas;
                    l.url = lista[j].url.ToString();
                    lis.Add(l);
                }

                lstData.ItemsSource = lis;
            }
        }

        /// <summary>
        /// Populates the page with content passed during navigation.  Any saved state is also
        /// provided when recreating a page from a prior session.
        /// </summary>
        /// <param name="navigationParameter">The parameter value passed to
        /// <see cref="Frame.Navigate(Type, Object)"/> when this page was initially requested.
        /// </param>
        /// <param name="pageState">A dictionary of state preserved by this page during an earlier
        /// session.  This will be null the first time a page is visited.</param>
        protected override void LoadState(Object navigationParameter, Dictionary<String, Object> pageState)
        {
        }

        /// <summary>
        /// Preserves state associated with this page in case the application is suspended or the
        /// page is discarded from the navigation cache.  Values must conform to the serialization
        /// requirements of <see cref="SuspensionManager.SessionState"/>.
        /// </summary>
        /// <param name="pageState">An empty dictionary to be populated with serializable state.</param>
        protected override void SaveState(Dictionary<String, Object> pageState)
        {
        }
    }
}
