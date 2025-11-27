using Dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio
{
    public class DireccionNegocio
    {
        public List<Direccion> ListarPorUsuario(int idUsuario)
        {
            List<Direccion> lista = new List<Direccion>();
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setConsulta(@"SELECT Id, IdUsuario, Calle, Ciudad, CodigoPostal, Provincia
                                FROM Direcciones
                                WHERE IdUsuario = @idUsuario");
                datos.setearParametro("@idUsuario", idUsuario);
                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    Direccion dir = new Direccion();
                    dir.Id = (int)datos.Lector["Id"];
                    dir.IdUsuario = (int)datos.Lector["IdUsuario"];
                    dir.Calle = datos.Lector["Calle"].ToString();
                    dir.Ciudad = datos.Lector["Ciudad"].ToString();
                    dir.CodigoPostal = datos.Lector["CodigoPostal"].ToString();
                    dir.Provincia = datos.Lector["Provincia"].ToString();

                    lista.Add(dir);
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
        public void agregar(Direccion nueva)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setConsulta(@"INSERT INTO Direcciones (IdUsuario, Calle, Numero, Ciudad, Provincia, CodigoPostal)
                            VALUES (@IdUsuario, @Calle, @Numero, @Ciudad, @Provincia, @CodigoPostal)");

                datos.setearParametro("@IdUsuario", nueva.IdUsuario);
                datos.setearParametro("@Calle", nueva.Calle);
                datos.setearParametro("@Numero", nueva.Numero);
                datos.setearParametro("@Ciudad", nueva.Ciudad);
                datos.setearParametro("@Provincia", nueva.Provincia);
                datos.setearParametro("@CodigoPostal", nueva.CodigoPostal);

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

        public void modificar(Direccion direccion)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setConsulta(@"UPDATE Direcciones SET Calle = @Calle, Numero = @Numero,
                                Ciudad = @Ciudad, Provincia = @Provincia, CodigoPostal = @CodigoPostal
                                WHERE Id = @Id");
                datos.setearParametro("@Calle", direccion.Calle);
                datos.setearParametro("@Numero", direccion.Numero);
                datos.setearParametro("@Ciudad", direccion.Ciudad);
                datos.setearParametro("@Provincia", direccion.Provincia);
                datos.setearParametro("@CodigoPostal", direccion.CodigoPostal);
                datos.setearParametro("@Id", direccion.Id);

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
                datos.setConsulta("DELETE FROM Direcciones WHERE Id = @Id");
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

    }
}
