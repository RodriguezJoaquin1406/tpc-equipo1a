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

        // --------------------------------------- ABM ----------------------------------------
        public void Registrar(Usuario nuevo)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setConsulta(@"INSERT INTO Usuarios (NombreUsuario, Contrasena, Rol, Nombre, Email, Telefono)
                            VALUES (@usuario, @clave, @rol, @nombre, @email, @telefono)");
                datos.setearParametro("@usuario", nuevo.NombreUsuario);
                datos.setearParametro("@clave", nuevo.Contrasena);
                datos.setearParametro("@rol", nuevo.Rol);
                datos.setearParametro("@nombre", nuevo.Nombre);
                datos.setearParametro("@email", nuevo.Email);
                datos.setearParametro("@telefono", nuevo.Telefono);

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

        public void Actualizar(Usuario usuario)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setConsulta("UPDATE Usuarios SET NombreUsuario = @NombreUsuario, Nombre = @Nombre, Rol = @Rol, Email = @Email, Telefono = @Telefono WHERE Id = @Id");
                datos.setearParametro("@NombreUsuario", usuario.NombreUsuario);
                datos.setearParametro("@Nombre", usuario.Nombre);
                datos.setearParametro("@Rol", usuario.Rol);
                datos.setearParametro("@Email", usuario.Email);
                datos.setearParametro("@Telefono", usuario.Telefono);
                datos.setearParametro("@Id", usuario.Id);

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

        public void Eliminar(int id)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setConsulta("DELETE FROM Usuarios WHERE Id = @Id");
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


        // --------------------------------------- LOGIN ----------------------------------------
        public Usuario Login(string email, string contrasena)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setConsulta(@"SELECT Id, NombreUsuario, Rol, Nombre, Email, Telefono
                            FROM Usuarios
                            WHERE Email = @email AND Contrasena = @clave");
                datos.setearParametro("@email", email);
                datos.setearParametro("@clave", contrasena);
                datos.ejecutarLectura();

                if (datos.Lector.Read())
                {
                    Usuario usuario = new Usuario();
                    usuario.Id = (int)datos.Lector["Id"];
                    usuario.NombreUsuario = datos.Lector["NombreUsuario"].ToString();
                    usuario.Rol = datos.Lector["Rol"].ToString();
                    usuario.Nombre = datos.Lector["Nombre"].ToString();
                    usuario.Email = datos.Lector["Email"].ToString();
                    usuario.Telefono = datos.Lector["Telefono"].ToString();

                    return usuario;
                }

                return null;
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

        // --------------------------------------- VALIDACIONES ----------------------------------------
        public bool ExisteEmail(string email)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setConsulta("SELECT COUNT(*) FROM Usuarios WHERE Email = @email");
                datos.setearParametro("@email", email);
                datos.ejecutarLectura();

                if (datos.Lector.Read())
                {
                    int cantidad = (int)datos.Lector[0];
                    return cantidad > 0;
                }

                return false;
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

        public bool ExisteNombreUsuario(string nombreUsuario)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setConsulta("SELECT COUNT(*) FROM Usuarios WHERE NombreUsuario = @usuario");
                datos.setearParametro("@usuario", nombreUsuario);
                datos.ejecutarLectura();

                if (datos.Lector.Read())
                {
                    int cantidad = (int)datos.Lector[0];
                    return cantidad > 0;
                }

                return false;
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

        public Usuario BuscarPorID(int id) 
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setConsulta("SELECT * FROM Usuarios WHERE Id = @id");
                datos.setearParametro("@id", id);
                datos.ejecutarLectura();

                if (datos.Lector.Read())
                {
                    Usuario usuario = new Usuario();
                    usuario.Id = (int)datos.Lector["Id"];
                    usuario.NombreUsuario = datos.Lector["NombreUsuario"].ToString();
                    usuario.Rol = datos.Lector["Rol"].ToString();
                    usuario.Nombre = datos.Lector["Nombre"].ToString();
                    usuario.Email = datos.Lector["Email"].ToString();
                    usuario.Telefono = datos.Lector["Telefono"].ToString();

                    return usuario;
                }

                return null;
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                datos.cerrarConexion();
            }
        }


        public Usuario BuscarPorMail(string mail)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setConsulta("SELECT * FROM Usuarios WHERE Email = @mail");
                datos.setearParametro("@mail", mail);
                datos.ejecutarLectura();

                if (datos.Lector.Read())
                {
                    Usuario usuario = new Usuario();
                    usuario.Id = (int)datos.Lector["Id"];
                    usuario.NombreUsuario = datos.Lector["NombreUsuario"].ToString();
                    usuario.Rol = datos.Lector["Rol"].ToString();
                    usuario.Nombre = datos.Lector["Nombre"].ToString();
                    usuario.Email = datos.Lector["Email"].ToString();
                    usuario.Telefono = datos.Lector["Telefono"].ToString();

                    return usuario;
                }

                return null;
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                datos.cerrarConexion();
            }
        
        }

        public void ReiniciarContraseña(int id) 
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                // Primero obtenemos el teléfono del usuario
                datos.setConsulta("SELECT Telefono FROM Usuarios WHERE Id = @id");
                datos.setearParametro("@id", id);
                datos.ejecutarLectura();

                string telefono = "";
                if (datos.Lector.Read())
                {
                    telefono = datos.Lector["Telefono"].ToString();
                }
                datos.cerrarConexion();

                // Validamos que el usuario tenga teléfono registrado
                if (string.IsNullOrEmpty(telefono))
                {
                    throw new Exception("El usuario no tiene un teléfono registrado.");
                }

                // Actualizamos la contraseña con el teléfono
                datos = new AccesoDatos();
                datos.setConsulta("UPDATE Usuarios SET Contrasena = @telefono WHERE Id = @id");
                datos.setearParametro("@telefono", telefono);
                datos.setearParametro("@id", id);
                datos.ejecutarAccion();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                datos.cerrarConexion();
            }
        }
    }
}
