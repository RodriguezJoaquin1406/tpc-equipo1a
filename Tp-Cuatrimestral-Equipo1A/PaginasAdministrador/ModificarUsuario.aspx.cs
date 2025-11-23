using Dominio;
using Negocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Tp_Cuatrimestral_Equipo1A.PaginasAdministrador
{
    public partial class ModificarUsuario : System.Web.UI.Page
    {
        protected Usuario usuario;
        protected bool userError;
        protected bool userSuccess;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                cargarRoles();

                if(Request.QueryString["id"] != null)
                {
                    int id = int.Parse(Request.QueryString["id"]);
                    UsuarioNegocio negocio = new UsuarioNegocio();
                    try
                    {
                        usuario = negocio.BuscarPorID(id);
                        if (usuario != null)
                        {
                            txtNombreUsuario.Text = usuario.NombreUsuario;
                            txtNombre.Text = usuario.Nombre;
                            txtEmail.Text = usuario.Email;
                            txtTelefono.Text = usuario.Telefono;
                            ddlRol.SelectedValue = usuario.Rol;
                        }
                        else
                        {
                            userError = true;
                        }
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                }
                else
                {
                    userError = true;
                }
            }   
        }

        protected void cargarRoles()
        {
            List<string> roles = new List<string>() { "Administrador", "Vendedor", "Cliente" };
            ddlRol.DataSource = roles;
            ddlRol.DataBind();
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            Response.Redirect("AdministradorUsuarios.aspx");
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            UsuarioNegocio negocio = new UsuarioNegocio();

            // Validaciones

            if (string.IsNullOrWhiteSpace(txtNombreUsuario.Text) ||
                string.IsNullOrWhiteSpace(txtNombre.Text) ||
                string.IsNullOrWhiteSpace(txtEmail.Text) ||
                string.IsNullOrWhiteSpace(txtTelefono.Text))
            {
                userError = true;
                return;
            }

            // Si las validaciones son correctas, se procede a actualizar el usuario
            usuario.NombreUsuario = txtNombreUsuario.Text;
            usuario.Nombre = txtNombre.Text;
            usuario.Email = txtEmail.Text;
            usuario.Telefono = txtTelefono.Text;
            usuario.Rol = ddlRol.SelectedValue;

            try
            {
                negocio.Actualizar(usuario);
                userSuccess = true;
            }
            catch (Exception)
            {
                throw;
            }
        }

        protected void btnEliminar_Click(object sender, EventArgs e)
        {
            
            if (usuario != null)
            {
                UsuarioNegocio negocio = new UsuarioNegocio();
                try
                {
                    negocio.Eliminar(usuario.Id);
                    Response.Redirect("AdministradorUsuarios.aspx");
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        
    }
}