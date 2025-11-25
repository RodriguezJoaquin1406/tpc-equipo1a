using Dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio
{
    public class CategoriaNegocio
    {
        public List<Categoria> listar()
        {
            List<Categoria> lista = new List<Categoria>();
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setConsulta("SELECT Id, Nombre FROM Categorias");
                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    Categoria cat = new Categoria();
                    cat.Id = (int)datos.Lector["Id"];
                    cat.Nombre = datos.Lector["Nombre"].ToString();

                    lista.Add(cat);
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

        public void agregar(Categoria nueva)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                //  longitud mínima
                if (string.IsNullOrWhiteSpace(nueva.Nombre) || nueva.Nombre.Length < 4)
                    throw new Exception("El nombre de la categoría debe tener al menos 4 caracteres.");

                //  duplicados
                if (ExisteCategoria(nueva.Nombre))
                    throw new Exception("La categoría ya existe.");

                // Si pasa las validaciones , agregar a la base de datos
                datos.setConsulta("INSERT INTO Categorias (Nombre) VALUES (@Nombre)");
                datos.setearParametro("@Nombre", nueva.Nombre);
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


        public void modificar(Categoria categoria)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                // longitud mínima
                if (string.IsNullOrWhiteSpace(categoria.Nombre) || categoria.Nombre.Length < 4)
                    throw new Exception("El nombre de la categoría debe tener al menos 4 caracteres.");

                // duplicados (excepto si es la misma categoría)
                if (ExisteCategoria(categoria.Nombre, categoria.Id))
                    throw new Exception("Ya existe otra categoría con ese nombre.");

                // Si pasa las validaciones, actualiza
                datos.setConsulta("UPDATE Categorias SET Nombre = @Nombre WHERE Id = @Id");
                datos.setearParametro("@Nombre", categoria.Nombre);
                datos.setearParametro("@Id", categoria.Id);
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
            AccesoDatos datos = new AccesoDatos();
            try
            {
                // Validación: no permitir eliminar si hay productos asociados
                if (TieneProductos(id))
                    throw new Exception("No se puede eliminar la categoría porque tiene productos asociados.");

                datos.setConsulta("DELETE FROM Categorias WHERE Id = @Id");
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


        // ------------------------------  validaciones 

        public bool ExisteCategoria(string nombre, int? idActual = null)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setConsulta("SELECT Id FROM Categorias WHERE Nombre = @Nombre");
                datos.setearParametro("@Nombre", nombre);
                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    int idEncontrado = (int)datos.Lector["Id"];
                    if (idActual == null || idEncontrado != idActual.Value)
                        return true; // existe duplicado
                }
                return false;
            }
            finally
            {
                datos.cerrarConexion();
            }
        }

        public bool TieneProductos(int idCategoria)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setConsulta("SELECT COUNT(*) FROM Productos WHERE IdCategoria = @IdCategoria");
                datos.setearParametro("@IdCategoria", idCategoria);
                datos.ejecutarLectura();
                datos.Lector.Read();
                return (int)datos.Lector[0] > 0;
            }
            finally
            {
                datos.cerrarConexion();
            }
        }

    }
}

