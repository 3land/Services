using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace MiServicio
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Service1.svc or Service1.svc.cs at the Solution Explorer and start debugging.
    public class Service1 : IService1
    {
        wappsEntities1 db = new wappsEntities1();
        public List<CentrosComerciale> local(string local)
        {
            return (from c in db.CentrosComerciales where c.localidad == local select c).ToList();
        }
        public List<CentrosComerciale> listaCine(string cine)
        {
            return (from c in db.CentrosComerciales where c.cine == cine select c).ToList();
        }
    }
}
