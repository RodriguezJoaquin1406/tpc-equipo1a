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
    }
}
