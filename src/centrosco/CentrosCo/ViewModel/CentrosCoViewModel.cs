using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Networking.Connectivity;
using Windows.Web.Syndication;

namespace CentrosCo.ViewModel
{
    class CentrosCoViewModel
    {
        private const string INTERNET_REQUIRED = "This Application Requires Internet Acess to Work Properly";
        private BasicPage2 _traerDatos = new BasicPage2();

        public BasicPage2 TraerDatos
        {
            get { return _traerDatos; }
            set { _traerDatos = value; }
        }
        public bool HayInternet
        {
            get
            {
                var prof = NetworkInformation.GetInternetConnectionProfile();
                return prof != null && prof.GetNetworkConnectivityLevel() == NetworkConnectivityLevel.InternetAccess;
            }
        }
        public async Task Initialize()
        {
            if (HayInternet)
            {
                var syndicationClient = new SyndicationClient();
            }
            else
            {

            }

        }

    }
}
