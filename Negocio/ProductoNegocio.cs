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
            SELECT P.Id, P.Nombre, P.Descripcion, P.Talle, P.PrecioBase, P.StockActual, P.StockMinimo,
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
                        Talle = datos.Lector["Talle"].ToString(), // Campo agregado
                        PrecioBase = (decimal)datos.Lector["PrecioBase"],
                        StockActual = (int)datos.Lector["StockActual"],
                        StockMinimo = (int)datos.Lector["StockMinimo"],
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

        public Producto buscarPorId(int id)
        {
            var datos = new AccesoDatos();
            try
            {
                string consulta = @"
            SELECT P.Id, P.Nombre, P.Descripcion, P.Talle, P.PrecioBase, P.StockActual, P.StockMinimo,
                   C.Id AS IdCategoria, C.Nombre AS NombreCategoria
            FROM Productos P
            INNER JOIN Categorias C ON P.IdCategoria = C.Id
            WHERE P.Id = @id";

                datos.setConsulta(consulta);
                datos.setearParametro("@id", id);
                datos.ejecutarLectura();

                Producto prod = null;

                if (datos.Lector.Read())
                {
                    prod = new Producto
                    {
                        Id = (int)datos.Lector["Id"],
                        Nombre = datos.Lector["Nombre"].ToString(),
                        Descripcion = datos.Lector["Descripcion"].ToString(),
                        Talle = datos.Lector["Talle"].ToString(), // Campo agregado
                        PrecioBase = (decimal)datos.Lector["PrecioBase"],
                        StockActual = (int)datos.Lector["StockActual"],
                        StockMinimo = (int)datos.Lector["StockMinimo"], // Nuevo campo
                        Categoria = new Categoria
                        {
                            Id = (int)datos.Lector["IdCategoria"],
                            Nombre = datos.Lector["NombreCategoria"].ToString()
                        }
                    };
                }

                datos.cerrarConexion();

                // cargado imagenes
                if (prod != null)
                {
                    var datosImg = new AccesoDatos();
                    try
                    {
                        datosImg.setConsulta("SELECT UrlImagen FROM Imagenes WHERE IdProducto = @id");
                        datosImg.setearParametro("@id", prod.Id);
                        datosImg.ejecutarLectura();

                        while (datosImg.Lector.Read())
                            prod.Imagenes.Add(datosImg.Lector["UrlImagen"].ToString());
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                    finally
                    {
                        datosImg.cerrarConexion();
                    }
                }

                return prod; // null si no existe
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                try 
                { 
                    datos.cerrarConexion();
                } 
                catch 
                {
                    Console.WriteLine("error cerrando conexion bd");
                }
            }
        }

        public void agregar(Producto nuevo)
        {
            var datos = new AccesoDatos();
            try
            {
                datos.setConsulta(@"INSERT INTO Productos (Nombre, Descripcion, Talle, PrecioBase, StockActual, StockMinimo, IdCategoria)
                            VALUES (@Nombre, @Descripcion, @Talle, @PrecioBase, @StockActual, @StockMinimo, @IdCategoria)");
                datos.setearParametro("@Nombre", nuevo.Nombre);
                datos.setearParametro("@Descripcion", nuevo.Descripcion);
                datos.setearParametro("@Talle", nuevo.Talle); // Agregar este parámetro
                datos.setearParametro("@PrecioBase", nuevo.PrecioBase);
                datos.setearParametro("@StockActual", nuevo.StockActual);
                datos.setearParametro("@StockMinimo", nuevo.StockMinimo); // Nuevo campo
                datos.setearParametro("@IdCategoria", nuevo.Categoria.Id);

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

        public void modificar(Producto producto)
        {
            var datos = new AccesoDatos();
            try
            {
                datos.setConsulta(@"UPDATE Productos SET Nombre = @Nombre, Descripcion = @Descripcion, Talle = @Talle,
                            PrecioBase = @PrecioBase, StockActual = @StockActual, StockMinimo = @StockMinimo, IdCategoria = @IdCategoria
                            WHERE Id = @Id");
                datos.setearParametro("@Nombre", producto.Nombre);
                datos.setearParametro("@Descripcion", producto.Descripcion);
                datos.setearParametro("@Talle", producto.Talle); // Agregar este parámetro
                datos.setearParametro("@PrecioBase", producto.PrecioBase);
                datos.setearParametro("@StockActual", producto.StockActual);
                datos.setearParametro("@StockMinimo", producto.StockMinimo); // Nuevo campo
                datos.setearParametro("@IdCategoria", producto.Categoria.Id);
                datos.setearParametro("@Id", producto.Id);

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

        public void eliminar(int id)
        {
            var datos = new AccesoDatos();
            try
            {
                datos.setConsulta("DELETE FROM Productos WHERE Id = @Id");
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

        public List<Producto> listarFiltrado(string categoria, string talle)
        {
            List<Producto> lista = new List<Producto>();
            AccesoDatos datos = new AccesoDatos();

            try
            {
                string consulta = "SELECT p.Id, p.Nombre, p.Descripcion, p.Talle, p.PrecioBase, c.Nombre AS CategoriaNombre " +
                                  "FROM Productos p INNER JOIN Categorias c ON p.IdCategoria = c.Id WHERE 1=1";

                if (categoria != "Todos")
                    consulta += " AND c.Nombre = @Categoria";

                if (talle != "Todos")
                    consulta += " AND p.Talle = @Talle";

                datos.setConsulta(consulta);

                if (categoria != "Todos")
                    datos.setearParametro("@Categoria", categoria);

                if (talle != "Todos")
                    datos.setearParametro("@Talle", talle);

                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    Producto p = new Producto();
                    p.Id = (int)datos.Lector["Id"];
                    p.Nombre = datos.Lector["Nombre"].ToString();
                    p.Descripcion = datos.Lector["Descripcion"].ToString();
                    p.Talle = datos.Lector["Talle"].ToString();
                    p.PrecioBase = (decimal)datos.Lector["PrecioBase"];
                    p.Categoria = new Categoria { Nombre = datos.Lector["CategoriaNombre"].ToString() };

                    // Cargar imágenes
                    p.Imagenes = new List<string>();
                    var datosImg = new AccesoDatos();
                    datosImg.setConsulta("SELECT UrlImagen FROM Imagenes WHERE IdProducto = @id");
                    datosImg.setearParametro("@id", p.Id);
                    datosImg.ejecutarLectura();

                    while (datosImg.Lector.Read())
                    {
                        p.Imagenes.Add(datosImg.Lector["UrlImagen"].ToString());
                    }

                    datosImg.cerrarConexion();

                    lista.Add(p);
                }

                return lista;
            }
            finally
            {
                datos.cerrarConexion();
            }
        }

        public void descontarStock(int idProducto, int cantidad)
        {
            var datos = new AccesoDatos();
            try
            {
                datos.setConsulta("UPDATE Productos SET StockActual = StockActual - @Cantidad WHERE Id = @IdProducto");
                datos.setearParametro("@Cantidad", cantidad);
                datos.setearParametro("@IdProducto", idProducto);
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