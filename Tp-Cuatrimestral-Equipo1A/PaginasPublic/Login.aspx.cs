using Dominio;
using Negocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Tp_Cuatrimestral_Equipo1A.PaginasPublic
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.QueryString["origen"] == "carrito")
                {
                    pnlAlerta.Visible = true; 
                }
            }
        }
        protected void btnLogin_Click(object sender, EventArgs e)
        {
            UsuarioNegocio negocio = new UsuarioNegocio();
            Usuario usuario = negocio.Login(txtEmail.Text, txtPassword.Text);

            try
            {
                if (usuario != null)
                {
                    Session["usuario"] = usuario;
                    Session.Add("usuario", usuario);
                    Response.Redirect("~/PaginasPublic/Inicio.aspx");
                    //Response.Redirect("MenuLogin1Ejemplo.aspx", false);
                }
                if (usuario == null)
                {
                    lblEmail.ForeColor = System.Drawing.Color.Red;
                    lblEmail.Text = "Usuario o contraseña incorrectos.";
                    Session.Add("error", "user o pass incorrectos");
                    //Response.Redirect("../Error.aspx", false);
                }
            }
            catch (Exception ex)
            {
                lblEmail.ForeColor = System.Drawing.Color.Red;
                lblEmail.Text = "Ocurrió un error durante el inicio de sesión. Por favor, inténtelo de nuevo más tarde.";
                Session.Add("error", ex.ToString());
                //Response.Redirect("../Error.aspx");
                // Aquí podrías registrar el error en un log si es necesario
            }
        }

    }
}