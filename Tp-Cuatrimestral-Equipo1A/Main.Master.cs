using Dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Tp_Cuatrimestral_Equipo1A
{
    public partial class Main : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["usuario"] != null)
                {
                    Usuario usuario = (Usuario)Session["usuario"];
                    lblSaludoMaster.Text = "Hola, " + usuario.NombreUsuario + " 👋";
                    hlLoginMaster.Visible = false;
                    hlRegistroMaster.Visible = false;
                    hlPerfilMaster.Visible = true;
                    btnCerrarSesionMaster.Visible = true;
                    hlMisComprasMaster.Visible = true;
                }
                else
                {
                    lblSaludoMaster.Text = "";
                    hlLoginMaster.Visible = true;
                    hlRegistroMaster.Visible = true;
                    hlPerfilMaster.Visible = false;
                    btnCerrarSesionMaster.Visible = false;
                    hlMisComprasMaster.Visible = false;
                }
            }
        }

        protected void btnCerrarSesionMaster_Click(object sender, EventArgs e)
        {
            Session.Clear();
            Response.Redirect("~/PaginasPublic/Inicio.aspx");
        }
    }
}