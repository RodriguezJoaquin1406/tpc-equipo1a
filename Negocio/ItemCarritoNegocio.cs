using Dominio;
using Domio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio
{
    internal class ItemCarritoNegocio
    {
        public List<ItemCarrito> listarCarrito (int idUsuario)
        {
            List<ItemCarrito> lista = new List<ItemCarrito>();
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setConsulta(@"SELECT P.Id AS IdProducto, P.Nombre, P.Precio, P.Imagen, IC.Cantidad
                                    FROM ItemCarrito IC
                                    JOIN Productos P ON IC.IdProducto = P.Id
                                    WHERE IC.IdUsuario = @idUsuario");
                datos.setearParametro("@idUsuario", idUsuario);
                datos.ejecutarLectura();
                while (datos.Lector.Read())
                {
                    ItemCarrito item = new ItemCarrito();
                    item.IdProducto = (int)datos.Lector["IdProducto"];
                    item.nombre = datos.Lector["Nombre"].ToString();
                    item.precio = (decimal)datos.Lector["Precio"];
                    item.imagen = datos.Lector["Imagen"].ToString();
                    item.cantidad = (int)datos.Lector["Cantidad"];
                    lista.Add(item);
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
