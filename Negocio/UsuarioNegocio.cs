using Dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio
{
    public class UsuarioNegocio
    {
        public List<Usuario> listar()
        {
            List<Usuario> lista = new List<Usuario>();
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setConsulta("SELECT Id, NombreUsuario, Contraseña, Rol FROM Usuarios");
                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    Usuario user = new Usuario();
                    user.Id = (int)datos.Lector["Id"];
                    user.NombreUsuario = datos.Lector["NombreUsuario"].ToString();
                    user.Contraseña = datos.Lector["Contraseña"].ToString();
                    user.Rol = datos.Lector["Rol"].ToString();

                    lista.Add(user);
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

