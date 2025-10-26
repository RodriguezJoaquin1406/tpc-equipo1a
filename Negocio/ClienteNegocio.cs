using Dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio
{
    public class ClienteNegocio
    {
        public List<Cliente> listar()
        {
            List<Cliente> lista = new List<Cliente>();
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setConsulta("SELECT Id, Nombre, Email, Telefono FROM Clientes");
                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    Cliente cli = new Cliente();
                    cli.Id = (int)datos.Lector["Id"];
                    cli.Nombre = datos.Lector["Nombre"].ToString();
                    cli.Email = datos.Lector["Email"].ToString();
                    cli.Telefono = datos.Lector["Telefono"].ToString();

                    lista.Add(cli);
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

