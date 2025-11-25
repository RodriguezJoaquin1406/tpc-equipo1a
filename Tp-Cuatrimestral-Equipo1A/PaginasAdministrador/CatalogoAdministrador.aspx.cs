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
    public partial class CatalogoAdministrador : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //  Validacion de administrador
            //if(!IsPostBack)
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
                ProductoNegocio negocio = new ProductoNegocio();
                dgvProductos.DataSource = negocio.listar();
                dgvProductos.DataBind();
            }
            catch (Exception)
            {
                throw ;
            }
        }
    }
}