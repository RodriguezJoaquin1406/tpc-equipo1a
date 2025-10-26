using Dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio
{
    public class CompraNegocio
    {
        public List<Compra> listar()
        {
            List<Compra> lista = new List<Compra>();
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setConsulta(@"SELECT C.Id, C.Fecha, P.Id AS IdProveedor, P.Nombre AS NombreProveedor, P.Contacto
                                    FROM Compras C
                                    INNER JOIN Proveedores P ON C.IdProveedor = P.Id");
                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    Compra compra = new Compra();
                    compra.Id = (int)datos.Lector["Id"];
                    compra.Fecha = (DateTime)datos.Lector["Fecha"];
                    compra.Proveedor = new Proveedor
                    {
                        Id = (int)datos.Lector["IdProveedor"],
                        Nombre = datos.Lector["NombreProveedor"].ToString(),
                        Contacto = datos.Lector["Contacto"].ToString()
                    };

                    lista.Add(compra);
                }

                return lista;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                datos.cerrarConexion();
            }
        }
    }
}
