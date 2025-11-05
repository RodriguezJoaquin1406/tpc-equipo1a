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
    public partial class Registro : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnRegistrar_Click(object sender, EventArgs e)
        {
            UsuarioNegocio negocio = new UsuarioNegocio();

            if (negocio.ExisteEmail(txtEmail.Text))
            {
                lblError.Text = "Ya existe una cuenta con ese correo electrónico.";
                lblError.Visible = true;
                return;
            }

            if (negocio.ExisteNombreUsuario(txtUsuario.Text))
            {
                lblError.Text = "El nombre de usuario ya está en uso. Elegí otro.";
                lblError.Visible = true;
                return;
            }

            Usuario nuevo = new Usuario
            {
                NombreUsuario = txtUsuario.Text,
                Contrasena = txtContrasena.Text,
                Nombre = txtNombre.Text,
                Email = txtEmail.Text,
                Telefono = txtTelefono.Text,
                Rol = "Cliente"
            };

            try
            {
                Session["usuario"] = nuevo;
                Session["reciénRegistrado"] = true;
                Response.Redirect("~/PaginasPrivadas/Home.aspx");


            }
            catch (Exception ex)
            {
                lblError.Text = "Error al registrar: " + ex.Message;
                lblError.Visible = true;
            }
        }


    }
}