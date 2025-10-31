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
                string consultaProductos = @"
            SELECT P.Id, P.Nombre, P.Descripcion, P.PrecioBase, P.StockActual,
                   C.Id AS IdCategoria, C.Nombre AS NombreCategoria
            FROM Productos P
            INNER JOIN Categorias C ON P.IdCategoria = C.Id";

                datos.setConsulta(consultaProductos);
                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    var prod = new Producto
                    {
                        Id = (int)datos.Lector["Id"],
                        Nombre = datos.Lector["Nombre"].ToString(),
                        Descripcion = datos.Lector["Descripcion"].ToString(),
                        PrecioBase = (decimal)datos.Lector["PrecioBase"],
                        StockActual = (int)datos.Lector["StockActual"],
                        Categoria = new Categoria
                        {
                            Id = (int)datos.Lector["IdCategoria"],
                            Nombre = datos.Lector["NombreCategoria"].ToString()
                        }
                    };

                    lista.Add(prod);
                }

                datos.cerrarConexion();

                // Cargar imágenes por producto
                foreach (var producto in lista)
                {
                    var datosImg = new AccesoDatos();
                    datosImg.setConsulta("SELECT UrlImagen FROM Imagenes WHERE IdProducto = @id");
                    datosImg.setearParametro("@id", producto.Id);
                    datosImg.ejecutarLectura();

                    while (datosImg.Lector.Read())
                    {
                        producto.Imagenes.Add(datosImg.Lector["UrlImagen"].ToString());
                    }

                    datosImg.cerrarConexion();
                }

                return lista;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}