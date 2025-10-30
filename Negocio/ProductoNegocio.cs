using Dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio
{
    public class ProductoNegocio
    {
        public List<Producto> listar()
        {
            var lista = new List<Producto>();
            var datos = new AccesoDatos();

            try
            {
                string consulta = @"
                    SELECT 
                        P.Id,
                        P.Nombre,
                        P.Descripcion,
                        P.PrecioBase,
                        P.StockActual,
                        C.Id  AS IdCategoria,
                        C.Nombre AS NombreCategoria,
                        Img.UrlImagen AS UrlImagen
                    FROM Productos P
                    INNER JOIN Categorias C ON P.IdCategoria = C.Id
                    OUTER APPLY (
                        SELECT TOP 1 I.UrlImagen
                        FROM Imagenes I
                        WHERE I.IdProducto = P.Id
                        ORDER BY I.IdImagen
                    ) Img";

                datos.setConsulta(consulta);
                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    var prod = new Producto
                    {
                        Id = (int)datos.Lector["Id"],
                        Nombre = datos.Lector["Nombre"].ToString(),
                        Descripcion = datos.Lector["Descripcion"].ToString(),
                        // MONEY -> decimal in .NET
                        PrecioBase = (decimal)datos.Lector["PrecioBase"],
                        StockActual = (int)datos.Lector["StockActual"],
                        Categoria = new Categoria
                        {
                            Id = (int)datos.Lector["IdCategoria"],
                            Nombre = datos.Lector["NombreCategoria"].ToString()
                        },
                        UrlImagen = datos.Lector["UrlImagen"] == DBNull.Value ? null : datos.Lector["UrlImagen"].ToString()
                    };

                    lista.Add(prod);
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