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
            // validar si es admin
            //if (!IsPostBack)
            //{
            //    if (Session["usuario"] == null || !((Dominio.Usuario)Session["usuario"]).EsAdmin)
            //    {
            //        Response.Redirect("../PaginasPublic/Inicio.aspx");
            //    }
            //    cargarTabla();
            //}
            cargarTabla();
        }

        protected void cargarTabla()
        {
            try
            {
                UsuarioNegocio negocio = new UsuarioNegocio();
                dgvUsuarios.DataSource = negocio.listar();
                dgvUsuarios.DataBind();
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}