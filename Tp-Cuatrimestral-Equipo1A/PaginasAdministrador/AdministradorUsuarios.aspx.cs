using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Dominio;
using Negocio;

namespace Tp_Cuatrimestral_Equipo1A.PaginasAdministrador
{
    public partial class AdministradorUsuarios : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
			try
			{
				UsuarioNegocio negocio = new UsuarioNegocio();
                dgvUsuarios.DataSource = negocio.listar();
                dgvUsuarios.DataBind();
            }
			catch (Exception ex)
			{

				throw ex;
			}
        }
    }
}