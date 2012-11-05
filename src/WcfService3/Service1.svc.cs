using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace WcfService3
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Service1.svc or Service1.svc.cs at the Solution Explorer and start debugging.
    public class Service1 : IService1
    {
        wappsEntities db = new wappsEntities();
        public void agregarUsuario(Usuario usuario)
        {
            db.Usuarios.Add(usuario);
            db.SaveChanges();
        }

        public void agregarProducto(Mercado mercado)
        {
            db.Mercadoes.Add(mercado);
            db.SaveChanges();
        }

        public List<Usuario> listado()
        {
            return (from c in db.Usuarios select c).ToList();
        }

        public List<Mercado> listadomer()
        {
            return (from c in db.Mercadoes select c).ToList();
        }

        public List<Usuario> listad(string usuario)
        {
            return (from c in db.Usuarios where c.NomUsuario == usuario select c).ToList();
        }

        public List<Usuario> listad2(string correo)
        {
            return (from c in db.Usuarios where c.Correo == correo select c).ToList();
        }

        public List<Usuario> listad3(string pass)
        {
            return (from c in db.Usuarios where c.PassUsuario == pass select c).ToList();
        }

        //revisar
        public void nuevaBD(int idusu)
        {
            SqlConnection connS = new SqlConnection();
            connS.ConnectionString = ConfigurationManager.ConnectionStrings["jovez"].ToString();
            string queryString = "DELETE FROM Mercado WHERE idUser = " + idusu;

            using (connS)
            {
                connS.Open();

                SqlCommand command = new SqlCommand(queryString, connS);
                command.ExecuteNonQuery();

            }
            connS.Close();
        }

        public void eliminarProducto(int idPro)
        {
            SqlConnection connS = new SqlConnection();
            connS.ConnectionString = ConfigurationManager.ConnectionStrings["jovez"].ToString();
            string queryString = "DELETE FROM Mercado WHERE idProducto = " + idPro;

            using (connS)
            {
                connS.Open();

                SqlCommand command = new SqlCommand(queryString, connS);
                command.ExecuteNonQuery();

            }
            connS.Close();
        }

        public List<Mercado> productos(int hey)
        {
            return (from c in db.Mercadoes where c.IdUser == hey select c).ToList();
        }

        public List<Usuario> retCorreo(string correo)
        {
            return (from c in db.Usuarios where c.Correo == correo select c).ToList();
        }
    }
}
