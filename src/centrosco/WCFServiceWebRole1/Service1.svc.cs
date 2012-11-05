using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.ServiceModel;
using System.ServiceModel.Activation;

namespace WCFServiceWebRole1
{
    [ServiceContract(Namespace = "")]
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]

    public class Service1 : IService1
    {
       [OperationContract]
        public System.Collections.Generic.List<CentroC> CentroCList()
        {
            string nwConn = System.Configuration.ConfigurationManager.ConnectionStrings["nataliaCS"].ConnectionString;
            var custList = new List<CentroC>();
            using (SqlConnection conn = new SqlConnection(nwConn))
            {
                const string sql = @"SELECT  Nombre, Direccion, Localidad FROM CentroCom";
                conn.Open();
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    SqlDataReader dr = cmd.ExecuteReader(
                        CommandBehavior.CloseConnection);
                    if (dr != null)
                        while (dr.Read())
                        {
                            var cust = new CentroC
                            {
                                Nombre = dr.GetString(0),
                                Direccion = dr.GetString(1),
                                Localidad = dr.GetString(2)
                            };
                            custList.Add(cust);
                        }
                    return custList;
                }
            }
        }
    }
}
