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
            // Falta Validacion administrador y que venga con un id en parametro

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

            Usuario modificado = new Usuario
            {
                Id = int.Parse(Request.QueryString["id"]),
                NombreUsuario = txtNombreUsuario.Text,
                Nombre = txtNombre.Text,
                Email = txtEmail.Text,
                Telefono = txtTelefono.Text,
                Rol = ddlRol.SelectedValue
            };

            

            try
            {
                negocio.Actualizar(modificado);
                userSuccess = true;
                Response.Redirect("AdministradorUsuarios.aspx");
            }
            catch (Exception)
            {
                throw;
            }
        }

        protected void btnEliminar_Click(object sender, EventArgs e)
        {
            if (Request.QueryString["id"] != null)
            {
                UsuarioNegocio negocio = new UsuarioNegocio();
                try
                {
                    negocio.Eliminar(int.Parse(Request.QueryString["id"]));
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