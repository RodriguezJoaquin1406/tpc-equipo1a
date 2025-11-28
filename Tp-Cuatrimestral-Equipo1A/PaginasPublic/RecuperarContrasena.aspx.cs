using Dominio;
using Microsoft.Ajax.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Dominio;
using Negocio;
using System.Web.WebSockets;



namespace Tp_Cuatrimestral_Equipo1A.PaginasPublic
{
    public partial class RecuperarContrasena : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            limpiarLabels();


        }

        protected void btnReiniciar_Click(object sender, EventArgs e)
        {
            // limpiar 
            

            string mail = txtEmail.Text.IsNullOrWhiteSpace() ? "" : txtEmail.Text;

            if (EsEmailValido(mail))
            {
                // reiniciar contraseña
                UsuarioNegocio negocio = new UsuarioNegocio();
                Usuario usuario = negocio.BuscarPorMail(mail);
                if (usuario != null)
                {
                    negocio.ReiniciarContraseña(usuario.Id);


                    lblMensaje.Text = "Se ha reemplazado su contraseña con su telefono registrado.";
                    lblMensaje.Visible = true;
                }
                else
                {
                    lblError.Text = "No se encontró ningún usuario con ese correo electrónico.";
                    lblError.Visible = true;
                }
            }
            else
            {
                lblError.Text = "Por favor, ingrese un correo electrónico válido.";
                lblError.Visible = true;
            }

        }


        private bool EsEmailValido(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }

        private void limpiarLabels()
        {
            lblError.Text = "";
            lblMensaje.Text = "";

            lblError.Visible = false;
            lblMensaje.Visible = false;

        }
    }
}