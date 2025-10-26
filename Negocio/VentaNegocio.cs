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
        public List<Venta> listar()
        {
            List<Venta> lista = new List<Venta>();
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setConsulta(@"SELECT V.Id, V.Fecha, V.NumeroFactura,
                                           C.Id AS IdCliente, C.Nombre AS NombreCliente, C.Email, C.Telefono
                                    FROM Ventas V
                                    INNER JOIN Clientes C ON V.IdCliente = C.Id");
                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    Venta venta = new Venta();
                    venta.Id = (int)datos.Lector["Id"];
                    venta.Fecha = (DateTime)datos.Lector["Fecha"];
                    venta.NumeroFactura = datos.Lector["NumeroFactura"].ToString();
                    venta.Cliente = new Cliente
                    {
                        Id = (int)datos.Lector["IdCliente"],
                        Nombre = datos.Lector["NombreCliente"].ToString(),
                        Email = datos.Lector["Email"].ToString(),
                        Telefono = datos.Lector["Telefono"].ToString()
                    };

                    lista.Add(venta);
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

