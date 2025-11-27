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
                datos.setConsulta(@"SELECT P.Id, P.Fecha, P.Estado, P.Total,
                                       U.Id AS IdUsuario, U.NombreUsuario,
                                       D.Id AS IdDireccion, D.Calle, D.Ciudad, D.CodigoPostal, D.Provincia,
                                       MP.Id AS IdMetodoPago, MP.Nombre AS Metodo
                                FROM Pedidos P
                                INNER JOIN Usuarios U ON P.IdUsuario = U.Id
                                LEFT JOIN Direcciones D ON P.IdDireccion = D.Id
                                LEFT JOIN MetodosPago MP ON P.IdMetodoPago = MP.Id");

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
                        Id = (int)datos.Lector["IdDireccion"],
                        Calle = datos.Lector["Calle"].ToString(),
                        Ciudad = datos.Lector["Ciudad"].ToString(),
                        CodigoPostal = datos.Lector["CodigoPostal"].ToString(),
                        Provincia = datos.Lector["Provincia"].ToString()
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
                U.Nombre AS NombreUsuario, U.Email, U.Telefono,
                D.Calle, D.Numero, D.Ciudad, D.CodigoPostal, D.Provincia,
                MP.Nombre AS Metodo
            FROM Pedido P
            INNER JOIN Usuario U ON P.IdUsuario = U.Id
            INNER JOIN Direccion D ON P.IdDireccion = D.Id
            INNER JOIN MetodoPago MP ON P.IdMetodoPago = MP.Id
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
                    pedido.IdDireccion = (int)datos.Lector["IdDireccion"];
                    pedido.IdMetodoPago = (int)datos.Lector["IdMetodoPago"];
                    pedido.Total = Convert.ToDecimal(datos.Lector["Total"]);

                    // Composición Usuario
                    pedido.Usuario = new Usuario
                    {
                        Id = pedido.IdUsuario,
                        Nombre = datos.Lector["NombreUsuario"].ToString(),
                        Email = datos.Lector["Email"].ToString(),
                        Telefono = datos.Lector["Telefono"].ToString()
                    };

                    // Composición Dirección
                    pedido.Direccion = new Direccion
                    {
                        Id = pedido.IdDireccion,
                        Calle = datos.Lector["Calle"].ToString(),
                        Numero = datos.Lector["Numero"].ToString(),
                        Ciudad = datos.Lector["Ciudad"].ToString(),
                        CodigoPostal = datos.Lector["CodigoPostal"].ToString(),
                        Provincia = datos.Lector["Provincia"].ToString()
                    };

                    // Composición Método de pago
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

    }

}

