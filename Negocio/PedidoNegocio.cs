using Dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio
{
    public class PedidoNegocio
    {
        public List<Pedido> Listar()
        {
            List<Pedido> lista = new List<Pedido>();
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setConsulta(@"
            SELECT P.Id, P.Fecha, P.Estado, P.Total,
                   U.Id AS IdUsuario, U.NombreUsuario,
                   P.DireccionCalle, P.DireccionNumero, P.DireccionCiudad,
                   P.DireccionCodigoPostal, P.DireccionProvincia,
                   MP.Id AS IdMetodoPago, MP.Nombre AS Metodo
            FROM Pedidos P
            INNER JOIN Usuarios U ON P.IdUsuario = U.Id
            INNER JOIN MetodosPago MP ON P.IdMetodoPago = MP.Id
        ");

                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    Pedido pedido = new Pedido();
                    pedido.Id = (int)datos.Lector["Id"];
                    pedido.Fecha = (DateTime)datos.Lector["Fecha"];
                    pedido.Estado = datos.Lector["Estado"].ToString();
                    pedido.Total = (decimal)datos.Lector["Total"];

                    pedido.Usuario = new Usuario
                    {
                        Id = (int)datos.Lector["IdUsuario"],
                        NombreUsuario = datos.Lector["NombreUsuario"].ToString()
                    };

                    pedido.Direccion = new Direccion
                    {
                        Calle = datos.Lector["DireccionCalle"].ToString(),
                        Numero = datos.Lector["DireccionNumero"].ToString(),
                        Ciudad = datos.Lector["DireccionCiudad"].ToString(),
                        CodigoPostal = datos.Lector["DireccionCodigoPostal"].ToString(),
                        Provincia = datos.Lector["DireccionProvincia"].ToString()
                    };

                    pedido.MetodoPago = new MetodoPago
                    {
                        Id = (int)datos.Lector["IdMetodoPago"],
                        Nombre = datos.Lector["Metodo"].ToString()
                    };

                    lista.Add(pedido);
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

        public Pedido obtenerPedido(int idPedido)
        {
            AccesoDatos datos = new AccesoDatos();
            Pedido pedido = new Pedido();

            try
            {
                datos.setConsulta(@"
            SELECT 
                P.Id, P.IdUsuario, P.Fecha, P.Estado, P.IdDireccion, P.IdMetodoPago, P.Total,
                U.NombreUsuario, U.Email, U.Telefono,
                P.DireccionCalle, P.DireccionNumero, P.DireccionCiudad,
                P.DireccionCodigoPostal, P.DireccionProvincia,
                MP.Nombre AS Metodo
            FROM Pedidos P
            INNER JOIN Usuarios U ON P.IdUsuario = U.Id
            INNER JOIN MetodosPago MP ON P.IdMetodoPago = MP.Id
            WHERE P.Id = @idPedido
        ");

                datos.setearParametro("@idPedido", idPedido);
                datos.ejecutarLectura();

                if (datos.Lector.Read())
                {
                    pedido.Id = (int)datos.Lector["Id"];
                    pedido.IdUsuario = (int)datos.Lector["IdUsuario"];
                    pedido.Fecha = (DateTime)datos.Lector["Fecha"];
                    pedido.Estado = datos.Lector["Estado"].ToString();
                    pedido.IdDireccion = datos.Lector["IdDireccion"] != DBNull.Value ? (int)datos.Lector["IdDireccion"] : 0;
                    pedido.IdMetodoPago = (int)datos.Lector["IdMetodoPago"];
                    pedido.Total = Convert.ToDecimal(datos.Lector["Total"]);

                    // Usuario
                    pedido.Usuario = new Usuario
                    {
                        Id = pedido.IdUsuario,
                        NombreUsuario = datos.Lector["NombreUsuario"].ToString(),
                        Email = datos.Lector["Email"].ToString(),
                        Telefono = datos.Lector["Telefono"].ToString()
                    };

                    // Dirección copiada
                    pedido.Direccion = new Direccion
                    {
                        Calle = datos.Lector["DireccionCalle"].ToString(),
                        Numero = datos.Lector["DireccionNumero"].ToString(),
                        Ciudad = datos.Lector["DireccionCiudad"].ToString(),
                        CodigoPostal = datos.Lector["DireccionCodigoPostal"].ToString(),
                        Provincia = datos.Lector["DireccionProvincia"].ToString()
                    };

                    // Método de pago
                    pedido.MetodoPago = new MetodoPago
                    {
                        Id = pedido.IdMetodoPago,
                        Nombre = datos.Lector["Metodo"].ToString()
                    };
                }

                return pedido;
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

        public int crearPedido(Pedido pedido)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setConsulta(@"
            INSERT INTO Pedidos (
                IdUsuario, Fecha, IdDireccion, IdMetodoPago, Total, Estado,
                DireccionCalle, DireccionNumero, DireccionCiudad, DireccionCodigoPostal, DireccionProvincia
            )
            OUTPUT INSERTED.Id
            VALUES (
                @IdUsuario, @Fecha, @IdDireccion, @IdMetodoPago, @Total, @Estado,
                @DireccionCalle, @DireccionNumero, @DireccionCiudad, @DireccionCodigoPostal, @DireccionProvincia
            )
        ");

                datos.setearParametro("@IdUsuario", pedido.IdUsuario);
                datos.setearParametro("@Fecha", pedido.Fecha);
                datos.setearParametro("@IdDireccion", pedido.IdDireccion);
                datos.setearParametro("@IdMetodoPago", pedido.IdMetodoPago);
                datos.setearParametro("@Total", pedido.Total);
                datos.setearParametro("@Estado", pedido.Estado);

                // Copia de datos de dirección
                datos.setearParametro("@DireccionCalle", pedido.Direccion.Calle);
                datos.setearParametro("@DireccionNumero", pedido.Direccion.Numero);
                datos.setearParametro("@DireccionCiudad", pedido.Direccion.Ciudad);
                datos.setearParametro("@DireccionCodigoPostal", pedido.Direccion.CodigoPostal);
                datos.setearParametro("@DireccionProvincia", pedido.Direccion.Provincia);

                return (int)datos.ejecutarEscalar();
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

        public List<Pedido> listarPorUsuario(int idUsuario)
        {
            List<Pedido> lista = new List<Pedido>();
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setConsulta(@"
            SELECT P.Id, P.Fecha, P.Estado, P.Total
            FROM Pedidos P
            WHERE P.IdUsuario = @IdUsuario
            ORDER BY P.Fecha DESC
        ");

                datos.setearParametro("@IdUsuario", idUsuario);
                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    Pedido pedido = new Pedido
                    {
                        Id = (int)datos.Lector["Id"],
                        Fecha = (DateTime)datos.Lector["Fecha"],
                        Estado = datos.Lector["Estado"].ToString(),
                        Total = (decimal)datos.Lector["Total"],
                        IdUsuario = idUsuario
                    };

                    lista.Add(pedido);
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

        public void actualizarEstado(int idPedido, string nuevoEstado)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setConsulta("UPDATE Pedidos SET Estado = @Estado WHERE Id = @IdPedido");
                datos.setearParametro("@Estado", nuevoEstado);
                datos.setearParametro("@IdPedido", idPedido);
                datos.ejecutarAccion();
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

        public List<DetallePedido> listarDetalles(int idPedido)
        {
            List<DetallePedido> lista = new List<DetallePedido>();
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setConsulta(@"
            SELECT DP.Id, DP.IdPedido, DP.IdProducto, DP.Talle, DP.Cantidad, DP.PrecioUnitario,
                   PR.Nombre AS NombreProducto
            FROM DetallePedido DP
            INNER JOIN Productos PR ON DP.IdProducto = PR.Id
            WHERE DP.IdPedido = @IdPedido
        ");

                datos.setearParametro("@IdPedido", idPedido);
                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    DetallePedido detalle = new DetallePedido
                    {
                        Id = (int)datos.Lector["Id"],
                        IdPedido = (int)datos.Lector["IdPedido"],
                        IdProducto = (int)datos.Lector["IdProducto"],
                        Talle = datos.Lector["Talle"].ToString(),
                        Cantidad = (int)datos.Lector["Cantidad"],
                        PrecioUnitario = (decimal)datos.Lector["PrecioUnitario"],
                        Producto = new Producto
                        {
                            Nombre = datos.Lector["NombreProducto"].ToString()
                        }
                    };

                    lista.Add(detalle);
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

