using Dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio
{
    public class DetallePedidoNegocio
    {
        public List<DetallePedido> ListarPorPedido(int idPedido)
        {
            List<DetallePedido> lista = new List<DetallePedido>();
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setConsulta(@"SELECT DP.Id, DP.IdPedido, DP.IdProducto, DP.Cantidad, DP.PrecioUnitario,
                                       P.Nombre, P.Descripcion, P.PrecioBase
                                FROM DetallePedido DP
                                INNER JOIN Productos P ON DP.IdProducto = P.Id
                                WHERE DP.IdPedido = @idPedido");
                datos.setearParametro("@idPedido", idPedido);
                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    DetallePedido detalle = new DetallePedido();
                    detalle.Id = (int)datos.Lector["Id"];
                    detalle.IdPedido = (int)datos.Lector["IdPedido"];
                    detalle.IdProducto = (int)datos.Lector["IdProducto"];
                    detalle.Cantidad = (int)datos.Lector["Cantidad"];
                    detalle.PrecioUnitario = (decimal)datos.Lector["PrecioUnitario"];

                    detalle.Producto = new Producto
                    {
                        Id = detalle.IdProducto,
                        Nombre = datos.Lector["Nombre"].ToString(),
                        Descripcion = datos.Lector["Descripcion"].ToString(),
                        PrecioBase = (decimal)datos.Lector["PrecioBase"]
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
