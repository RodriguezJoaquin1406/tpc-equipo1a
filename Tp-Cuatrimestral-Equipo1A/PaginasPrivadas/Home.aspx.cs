using Dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Tp_Cuatrimestral_Equipo1A.PaginasPrivadas

{
    public partial class Home : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["usuario"] == null)
            {
                Response.Redirect("~/PaginasPublic/Login.aspx");
                return;
            }

            if (!IsPostBack)
            {
                Usuario usuario = (Usuario)Session["usuario"];
                lblNombreUsuario.Text = usuario.NombreUsuario;
                lblNombre.Text = usuario.Nombre;
                lblEmail.Text = usuario.Email;
                lblTelefono.Text = usuario.Telefono;
                lblRol.Text = usuario.Rol;

                if (Session["reciénRegistrado"] != null)
                {
                    lblBienvenida.Text = "¡Gracias por registrarte, " + usuario.NombreUsuario + "!";
                    Session.Remove("reciénRegistrado");
                }
            }
        }


        protected void btnCerrarSesion_Click(object sender, EventArgs e)
        {
            Session.Clear();
            Response.Redirect("~/PaginasPublic/Inicio.aspx");
        }
    }
}