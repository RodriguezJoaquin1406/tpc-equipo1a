using Dominio;
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
    P.Id AS IdProducto,  
    P.Nombre,  
    P.PrecioBase AS Precio,  
    (SELECT TOP 1 UrlImagen FROM Imagenes WHERE IdProducto = P.Id) AS Imagen,  
    C.Cantidad,  
    C.Talle

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

                    item.IdProducto = (int)datos.Lector["IdProducto"];
                    item.nombre = datos.Lector["Nombre"].ToString();
                    item.precio = (decimal)datos.Lector["Precio"];
                    item.imagen = datos.Lector["Imagen"].ToString();
                    item.cantidad = (int)datos.Lector["Cantidad"];
                    item.talle = datos.Lector["Talle"].ToString(); // lo agregue para el carrito

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

        public void agregarAlCarrito(int idUsuario, int idProducto, int cantidad, string talle)
        {
            AccesoDatos datos = new AccesoDatos();

            // Verificar si ya existe ese producto con ese talle en el carrito
            datos.setConsulta(@"SELECT Cantidad FROM Carrito WHERE IdUsuario = @idUsuario AND IdProducto = @idProducto AND Talle = @talle");
            datos.setearParametro("@idUsuario", idUsuario);
            datos.setearParametro("@idProducto", idProducto);
            datos.setearParametro("@talle", talle);
            datos.ejecutarLectura();

            if (datos.Lector.Read())
            {
                // Ya existe  actualizar cantidad
                int cantidadActual = (int)datos.Lector["Cantidad"];
                int nuevaCantidad = cantidadActual + cantidad;

                datos.cerrarConexion();
                datos = new AccesoDatos();
                datos.setConsulta(@"UPDATE Carrito SET Cantidad = @cantidad WHERE IdUsuario = @idUsuario AND IdProducto = @idProducto AND Talle = @talle");
                datos.setearParametro("@cantidad", nuevaCantidad);
                datos.setearParametro("@idUsuario", idUsuario);
                datos.setearParametro("@idProducto", idProducto);
                datos.setearParametro("@talle", talle);
                datos.ejecutarAccion();
            }
            else
            {
                // No existe, insertar nuevo
                datos.cerrarConexion();
                datos = new AccesoDatos();
                datos.setConsulta(@"INSERT INTO Carrito (IdUsuario, IdProducto, Cantidad, Talle) VALUES (@idUsuario, @idProducto, @cantidad, @talle)");
                datos.setearParametro("@idUsuario", idUsuario);
                datos.setearParametro("@idProducto", idProducto);
                datos.setearParametro("@cantidad", cantidad);
                datos.setearParametro("@talle", talle);
                datos.ejecutarAccion();
            }

            datos.cerrarConexion();
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
