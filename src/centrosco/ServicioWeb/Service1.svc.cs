using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Activation;
using System.ServiceModel.Web;
using System.Text;


namespace ServicioWeb
{
    public class Service1 : IService1
    {
      
       public List<Centro> Centro()
        {
            string nwConn = System.Configuration.ConfigurationManager.ConnectionStrings["nataliaCS"].ConnectionString;
            var custList = new List<Centro>();
            using (SqlConnection conn = new SqlConnection(nwConn))
            {
                const string sql = "SELECT  Nombre FROM CentrosCom where nombre= 'Atlantis'";
                conn.Open();
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    SqlDataReader dr = cmd.ExecuteReader();
                    if (dr != null)
                        while (dr.Read())
                        {
                            var cust = new Centro
                            {
                                Nombre = dr.GetString(0),
                                Direccion = dr.GetString(1),
                                Localidad = dr.GetString(2)
                            };
                            custList.Add(cust);
                        }
                    dr.Close(); 
                    return custList;
                }
            }
        }

       public string CentroC()
       {
           return "Hola MAD";
           //string nwConn = System.Configuration.ConfigurationManager.ConnectionStrings["nataliaCS"].ConnectionString;
           ////var custList = new List<Centro>();
           //using (SqlConnection conn = new SqlConnection(nwConn))
           //{
           //    const string sql = "SELECT  Nombre FROM CentrosCom where nombre= 'Atlantis'";
           //    conn.Open();
           //    using (SqlCommand cmd = new SqlCommand(sql, conn))
           //    {
           //        SqlDataReader dr = cmd.ExecuteReader();
           //        if (dr != null)
           //        {
           //            dr.Close();
           //            return dr[0].ToString();
                       
           //        }
           //        else { dr.Close(); return null; }
                  
                   
                   
           //    }
           //}
       }
    }
}
