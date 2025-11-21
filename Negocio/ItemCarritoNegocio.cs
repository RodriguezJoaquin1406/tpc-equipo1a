using Domio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio
{
    public class ItemCarritoNegocio
    {
        public List<ItemCarrito> listarCarrito(int idUsuario)
        {
            List<ItemCarrito> lista = new List<ItemCarrito>();
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setConsulta(@"

SELECT 
    P.Id, 
    P.Nombre, 
    P.PrecioBase AS Precio, 
    (SELECT TOP 1 UrlImagen FROm Imagenes WHERE IdProducto = P.Id) AS Imagen, 
    C.Cantidad

FROM 
    Carrito C
INNER JOIN 
    Productos P ON C.IdProducto = P.Id
WHERE 
    C.IdUsuario = @idUsuario

");

                datos.setearParametro("@idUsuario", idUsuario);
                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {

                    ItemCarrito item = new ItemCarrito();

                    item.IdProducto = (int)datos.Lector["Id"];
                    item.nombre = datos.Lector["Nombre"].ToString();
                    item.precio = (decimal)datos.Lector["Precio"];
                    item.imagen = datos.Lector["Imagen"].ToString();
                    item.cantidad = (int)datos.Lector["Cantidad"];
                    lista.Add(item);
                }
                return lista;
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                datos.cerrarConexion();
            }
        }

        public void eliminarDelCarrito(int idUsuario, int idProducto)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setConsulta("DELETE FROM Carrito WHERE IdUsuario = @idUsuario AND IdProducto = @idProducto");
                datos.setearParametro("@idUsuario", idUsuario);
                datos.setearParametro("@idProducto", idProducto);
                datos.ejecutarAccion();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                datos.cerrarConexion();
            }
        }

        public void agregarAlCarrito(int idUsuario, int idProducto, int cantidad)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setConsulta("INSERT INTO Carrito (IdUsuario, IdProducto, Cantidad) VALUES (@idUsuario, @idProducto, @cantidad)");
                datos.setearParametro("@idUsuario", idUsuario);
                datos.setearParametro("@idProducto", idProducto);
                datos.setearParametro("@cantidad", cantidad);
                datos.ejecutarAccion();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                datos.cerrarConexion();
            }
        }

        public void actualizarCantidad(int idUsuario, int idProducto, int nuevaCantidad)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setConsulta("UPDATE Carrito SET Cantidad = @nuevaCantidad WHERE IdUsuario = @idUsuario AND IdProducto = @idProducto");
                datos.setearParametro("@nuevaCantidad", nuevaCantidad);
                datos.setearParametro("@idUsuario", idUsuario);
                datos.setearParametro("@idProducto", idProducto);
                datos.ejecutarAccion();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                datos.cerrarConexion();
            }
        }
    }
}
