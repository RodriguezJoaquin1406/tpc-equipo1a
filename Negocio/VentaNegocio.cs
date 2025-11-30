using Dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio
{
    public class VentaNegocio
    {
        public List<Venta> Listar()
        {
            List<Venta> lista = new List<Venta>();
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setConsulta(@"SELECT V.Id, V.IdCliente, V.Fecha, V.NumeroFactura,
                                       U.Id AS IdUsuario, U.NombreUsuario, U.Nombre AS NombreCliente, U.Email
                                FROM Ventas V
                                INNER JOIN Usuarios U ON V.IdCliente = U.Id
                                ORDER BY V.Fecha DESC");

                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    Venta venta = new Venta();
                    venta.Id = (int)datos.Lector["Id"];
                    venta.IdCliente = (int)datos.Lector["IdCliente"];
                    venta.Fecha = (DateTime)datos.Lector["Fecha"];
                    venta.NumeroFactura = datos.Lector["NumeroFactura"].ToString();

                    venta.Cliente = new Usuario
                    {
                        Id = (int)datos.Lector["IdUsuario"],
                        NombreUsuario = datos.Lector["NombreUsuario"].ToString(),
                        Nombre = datos.Lector["NombreCliente"].ToString(),
                        Email = datos.Lector["Email"].ToString()
                    };

                    lista.Add(venta);
                }

                // Cargar detalles para calcular total
                foreach (var venta in lista)
                {
                    venta.Detalles = listarDetalles(venta.Id);
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

        public Venta buscarPorId(int id)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setConsulta(@"SELECT V.Id, V.IdCliente, V.Fecha, V.NumeroFactura,
                                       U.Id AS IdUsuario, U.NombreUsuario, U.Nombre AS NombreCliente, U.Email, U.Telefono
                                FROM Ventas V
                                INNER JOIN Usuarios U ON V.IdCliente = U.Id
                                WHERE V.Id = @id");

                datos.setearParametro("@id", id);
                datos.ejecutarLectura();

                Venta venta = null;

                if (datos.Lector.Read())
                {
                    venta = new Venta();
                    venta.Id = (int)datos.Lector["Id"];
                    venta.IdCliente = (int)datos.Lector["IdCliente"];
                    venta.Fecha = (DateTime)datos.Lector["Fecha"];
                    venta.NumeroFactura = datos.Lector["NumeroFactura"].ToString();

                    venta.Cliente = new Usuario
                    {
                        Id = (int)datos.Lector["IdUsuario"],
                        NombreUsuario = datos.Lector["NombreUsuario"].ToString(),
                        Nombre = datos.Lector["NombreCliente"].ToString(),
                        Email = datos.Lector["Email"].ToString(),
                        Telefono = datos.Lector["Telefono"].ToString()
                    };

                    // Cargar detalles
                    venta.Detalles = listarDetalles(venta.Id);
                }

                return venta;
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

        public List<VentaDetalle> listarDetalles(int idVenta)
        {
            List<VentaDetalle> lista = new List<VentaDetalle>();
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setConsulta(@"SELECT VD.Id, VD.IdVenta, VD.IdProducto, VD.Cantidad, VD.PrecioUnitario,
                                       P.Nombre AS NombreProducto, P.Descripcion
                                FROM VentaDetalle VD
                                INNER JOIN Productos P ON VD.IdProducto = P.Id
                                WHERE VD.IdVenta = @IdVenta");

                datos.setearParametro("@IdVenta", idVenta);
                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    VentaDetalle detalle = new VentaDetalle
                    {
                        Id = (int)datos.Lector["Id"],
                        IdVenta = (int)datos.Lector["IdVenta"],
                        IdProducto = (int)datos.Lector["IdProducto"],
                        Cantidad = (int)datos.Lector["Cantidad"],
                        PrecioUnitario = (decimal)datos.Lector["PrecioUnitario"],
                        Producto = new Producto
                        {
                            Id = (int)datos.Lector["IdProducto"],
                            Nombre = datos.Lector["NombreProducto"].ToString(),
                            Descripcion = datos.Lector["Descripcion"].ToString()
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

        public int crearVenta(Venta venta)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setConsulta(@"INSERT INTO Ventas (IdCliente, Fecha, NumeroFactura)
                                OUTPUT INSERTED.Id
                                VALUES (@IdCliente, @Fecha, @NumeroFactura)");

                datos.setearParametro("@IdCliente", venta.IdCliente);
                datos.setearParametro("@Fecha", venta.Fecha);
                datos.setearParametro("@NumeroFactura", venta.NumeroFactura);

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

        public void eliminarVenta(int id)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                // Primero eliminar detalles
                datos.setConsulta("DELETE FROM VentaDetalle WHERE IdVenta = @IdVenta");
                datos.setearParametro("@IdVenta", id);
                datos.ejecutarAccion();
                datos.cerrarConexion();

                // Luego eliminar la venta
                datos = new AccesoDatos();
                datos.setConsulta("DELETE FROM Ventas WHERE Id = @Id");
                datos.setearParametro("@Id", id);
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

        public string generarNumeroFactura()
        {
            return "F-" + DateTime.Now.ToString("yyyyMMdd") + "-" + DateTime.Now.Ticks.ToString().Substring(10);
        }

        public void agregarDetalleVenta(VentaDetalle detalle)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setConsulta(@"INSERT INTO VentaDetalle (IdVenta, IdProducto, Cantidad, PrecioUnitario)
                                VALUES (@IdVenta, @IdProducto, @Cantidad, @PrecioUnitario)");
                datos.setearParametro("@IdVenta", detalle.IdVenta);
                datos.setearParametro("@IdProducto", detalle.IdProducto);
                datos.setearParametro("@Cantidad", detalle.Cantidad);
                datos.setearParametro("@PrecioUnitario", detalle.PrecioUnitario);
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
    }
}