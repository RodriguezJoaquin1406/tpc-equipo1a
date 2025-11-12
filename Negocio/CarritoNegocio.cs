using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dominio;

namespace Negocio
{
    public class CarritoNegocio
    {
        public List<Producto> listarCarritoUsuario(int id)
        {

            // primero consulta a carrito para buscar id de productos en el carrito del usuario
            var datos = new AccesoDatos();

            try
            {
                string consulta = "SELECT DISTINCT(IdProducto), Cantidad FROM Carrito WHERE IdUsuario = @id;";
                datos.setConsulta(consulta);
                datos.setearParametro("@id", id);
                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    List<int> idsProductos = new List<int>();
                    idsProductos.Add((int)datos.Lector["IdProducto"]);
                    // producto.Cantidad = (int)datos.Lector["Cantidad"];
                }
            }
            catch (Exception)
            {

                throw;
            }

            // ya con los id de productos, hacer otra consulta para traer los productos completos como en el catalogo pero filtrados por esos id

            var lista = new List<Producto>();


            return lista;
        }
    }
}
