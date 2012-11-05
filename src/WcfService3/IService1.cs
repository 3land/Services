using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace WcfService3
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService1" in both code and config file together.
    [ServiceContract]
    public interface IService1
    {
        [OperationContract]
        void agregarUsuario(Usuario usuario);

        [OperationContract]
        List<Mercado> listadomer();

        [OperationContract]
        List<Usuario> listado();

        [OperationContract]
        List<Usuario> listad(string usuario);

        [OperationContract]
        List<Usuario> listad2(string correo);

        [OperationContract]
        List<Usuario> listad3(string pass);

        [OperationContract]
        void nuevaBD(int idusu);

        [OperationContract]
        void agregarProducto(Mercado mercado);

        [OperationContract]
        void eliminarProducto(int idPro);

        [OperationContract]
        List<Mercado> productos(int idUs);

        [OperationContract]
        List<Usuario> retCorreo(string correo);
    }
}
