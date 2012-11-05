using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Windows.Devices.Geolocation;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using CentrosCo.ServiceReference1;
using Windows.Networking.Connectivity;


// The Basic Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234237

namespace CentrosCo
{
    /// <summary>
    /// A basic page that provides characteristics common to most applications.
    /// </summary>
    public sealed partial class Cercano : CentrosCo.Common.LayoutAwarePage
    {
        Geolocator geo = null;
        double miLatitud, miLongitud, distancia;
        public Cercano()
        {
            this.InitializeComponent();
            localiza();
        }

        public async void localiza()
        {
            if (HayInternet)
            {
                if (geo == null)
                {
                    geo = new Geolocator();
                }

                Geoposition pos = await geo.GetGeopositionAsync();
                miLatitud = pos.Coordinate.Latitude;
                miLongitud = pos.Coordinate.Longitude;
                calculo(); 
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

        public void calculo() 
        {
            Service1Client proxy = new Service1Client();
            var lista = proxy.cercaAsync().Result;
            int count = lista.Count;
            double lat, lon;
            List<CentrosComerciale> lis = new List<CentrosComerciale>();
            CentrosComerciale l = new CentrosComerciale();
            List<double> prov = new List<double>();
            for (int i = 0; i < count; i++)
            {
                string lat1, lon1;
                lat1 = lista[i].latitud.ToString();
                lat = (Convert.ToDouble (lat1) * Math.PI) / 180;
                lon1 = lista[i].longitud.ToString();
                lon = (Convert.ToDouble(lon1) * Math.PI) / 180;
                double earthRadius = 6378;
                double resLat = (miLatitud * Math.PI) / 180;
                double resLon = (miLongitud * Math.PI) / 180;
                double dislon = resLon - lon;
                double dislat = resLat - lat;
                double res1 = Math.Sin(dislat / 2) * Math.Sin(dislat / 2) + Math.Cos(lat) * Math.Cos(resLat) * Math.Sin(dislon / 2) * Math.Sin(dislon / 2);
                distancia = 2 * Math.Atan2(Math.Sqrt(res1), Math.Sqrt(1 - res1))*earthRadius;
                prov.Add(distancia);

                /*l = new CentrosComerciale();
                l.nombreCentro = lista[i].nombreCentro.ToString() +" "+prov[i];
                l.direccion = lista[i].direccion.ToString();
                l.cine = lista[i].cine.ToString();
                l.tiendas = lista[i].tiendas;
                l.url = lista[i].url.ToString();
                lis.Add(l);*/
            }

           // lstData.ItemsSource =lis;
            double a = prov.Min();
            int pos = prov.IndexOf(prov.Min())+1;
            var lista2 = proxy.minAsync(pos).Result;
            l = new CentrosComerciale();
            l.nombreCentro = lista2[0].nombreCentro.ToString();
            l.direccion = lista2[0].direccion.ToString();
            l.cine = lista2[0].cine.ToString();
            l.tiendas = lista2[0].tiendas;
            l.url = lista2[0].url.ToString();
            lis.Add(l);
            lstData.ItemsSource =lis;
            

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
