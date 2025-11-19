using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Dominio;
using Negocio;

namespace Tp_Cuatrimestral_Equipo1A.PaginasAdministrador
{
    public partial class AdministradorCategorias : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
			// Validacion Administrador
			try
			{
				CategoriaNegocio negocio = new CategoriaNegocio();
				dgvCategorias.DataSource = negocio.listar();
				dgvCategorias.DataBind();
            }
			catch (Exception ex)
			{

				throw ex;
			}

        }
    }
}