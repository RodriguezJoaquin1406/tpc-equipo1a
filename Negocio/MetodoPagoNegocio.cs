using Dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio
{
    public class MetodoPagoNegocio
    {
        public List<MetodoPago> Listar()
        {
            List<MetodoPago> lista = new List<MetodoPago>();
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setConsulta("SELECT Id, Nombre FROM MetodosPago");
                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    MetodoPago metodo = new MetodoPago();
                    metodo.Id = (int)datos.Lector["Id"];
                    metodo.Nombre = datos.Lector["Nombre"].ToString();

                    lista.Add(metodo);
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
