using Dominio;
using Negocio;
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
                txtNombre.Text = usuario.Nombre;
                txtEmail.Text = usuario.Email;
                txtTelefono.Text = usuario.Telefono;
                lblRol.Text = usuario.Rol;

                if (Session["reciénRegistrado"] != null)
                {
                    lblBienvenida.Text = "¡Gracias por registrarte, " + usuario.NombreUsuario + "!";
                    Session.Remove("reciénRegistrado");
                }
            }
        }

        protected void btnActualizar_Click(object sender, EventArgs e)
        {
            Usuario usuario = (Usuario)Session["usuario"];
            usuario.Nombre = txtNombre.Text;
            usuario.Email = txtEmail.Text;
            usuario.Telefono = txtTelefono.Text;

            UsuarioNegocio negocio = new UsuarioNegocio();
            negocio.Actualizar(usuario); 

            lblResultado.Text = "Datos actualizados correctamente.";
        }

        protected void btnEliminarCuenta_Click(object sender, EventArgs e)
        {
            Usuario usuario = (Usuario)Session["usuario"];
            UsuarioNegocio negocio = new UsuarioNegocio();
            negocio.Eliminar(usuario.Id); 

            Session.Clear();
            Response.Redirect("~/PaginasPublic/Inicio.aspx");
        }

        protected void btnCerrarSesion_Click(object sender, EventArgs e)
        {
            Session.Clear();
            Response.Redirect("~/PaginasPublic/Inicio.aspx");
        }
    }
}