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
            List<Producto> lista = new List<Producto>();
            AccesoDatos datos = new AccesoDatos();

            try
            {
                string consulta = @"SELECT P.Id, P.Nombre, P.StockActual, P.StockMinimo, P.PorcentajeGanancia,
                                           M.Id AS IdMarca, M.Nombre AS NombreMarca,
                                           C.Id AS IdCategoria, C.Nombre AS NombreCategoria
                                    FROM Productos P
                                    INNER JOIN Marcas M ON P.IdMarca = M.Id
                                    INNER JOIN Categorias C ON P.IdCategoria = C.Id";

                datos.setConsulta(consulta);
                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    Producto prod = new Producto();
                    prod.Id = (int)datos.Lector["Id"];
                    prod.Nombre = datos.Lector["Nombre"].ToString();
                    prod.StockActual = (int)datos.Lector["StockActual"];
                    prod.StockMinimo = (int)datos.Lector["StockMinimo"];
                    prod.PorcentajeGanancia = (decimal)datos.Lector["PorcentajeGanancia"];

                    prod.Marca = new Marca
                    {
                        Id = (int)datos.Lector["IdMarca"],
                        Nombre = datos.Lector["NombreMarca"].ToString()
                    };

                    prod.Categoria = new Categoria
                    {
                        Id = (int)datos.Lector["IdCategoria"],
                        Nombre = datos.Lector["NombreCategoria"].ToString()
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

