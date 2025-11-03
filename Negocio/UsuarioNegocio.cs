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
                datos.setConsulta(@"SELECT Id, NombreUsuario, Contrasena, Rol, Nombre, Email, Telefono FROM Usuarios");
                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    Usuario user = new Usuario();
                    user.Id = (int)datos.Lector["Id"];
                    user.NombreUsuario = datos.Lector["NombreUsuario"].ToString();
                    user.Contrasena = datos.Lector["Contrasena"].ToString();
                    user.Rol = datos.Lector["Rol"].ToString();
                    user.Nombre = datos.Lector["Nombre"].ToString();
                    user.Email = datos.Lector["Email"].ToString();
                    user.Telefono = datos.Lector["Telefono"].ToString();

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
