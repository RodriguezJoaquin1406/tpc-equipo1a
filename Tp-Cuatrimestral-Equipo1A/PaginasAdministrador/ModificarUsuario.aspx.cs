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

                if (Request.QueryString["id"] != null && Request.QueryString["id"] != "")
                {
                    int id = Request.QueryString["id"] != null ? int.Parse(Request.QueryString["id"]) : 0;
                    UsuarioNegocio negocio = new UsuarioNegocio();
                    if (id == 0)
                    {
                        userError = true;
                        Response.Redirect("../PaginasPublic/Inicio.aspx");
                    }
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
                    Response.Redirect("../PaginasPublic/Inicio.aspx");

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
                Usuario antesModificacion = negocio.BuscarPorID(modificado.Id);
                if (antesModificacion.Email != modificado.Email)
                {
                    if (negocio.ExisteEmail(modificado.Email))
                    {
                        userError = true;
                        return;
                    }
                    
                }

                if (antesModificacion.NombreUsuario != modificado.NombreUsuario)
                {
                    if (negocio.ExisteNombreUsuario(modificado.NombreUsuario))
                    {
                        userError = true;
                        return;
                    }

                }

                negocio.Actualizar(modificado);
                userSuccess = true;
                Response.Redirect("../PaginasAdministrador/administradorUsuarios.aspx", false);
                Context.ApplicationInstance.CompleteRequest();
            }
            catch (System.Threading.ThreadAbortException)
            {
                // Esto ocurre debido a Response.Redirect, se puede ignorar
            }
            catch (Exception)
            {
                userError = true;
            }
        }

        protected void btnEliminar_Click(object sender, EventArgs e)
        {
            if (Request.QueryString["id"] != null)
            {
                int idAEliminar = int.Parse(Request.QueryString["id"]);

                // Verificar que el usuario a eliminar no sea el usuario actual
                if (Session["Usuario"] != null)
                {
                    Usuario usuarioActual = (Usuario)Session["Usuario"];

                    if (usuarioActual.Id == idAEliminar)
                    {
                        userError = true;
                        // Aquí puedes mostrar un mensaje como "No puedes eliminarte a ti mismo"
                        return;
                    }
                }

                UsuarioNegocio negocio = new UsuarioNegocio();
                try
                {
                    negocio.Eliminar(idAEliminar);
                    Response.Redirect("AdministradorUsuarios.aspx", false);
                    Context.ApplicationInstance.CompleteRequest();
                }
               
                catch (Exception ex)
                {
                    userError = true;
                    // Manejo de errores de eliminación
                }
            }
        }


    }
}