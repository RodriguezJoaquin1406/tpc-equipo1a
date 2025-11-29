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

            Usuario usuario = (Usuario)Session["usuario"];
            DireccionNegocio direccionNegocio = new DireccionNegocio();
            List<Direccion> direcciones = direccionNegocio.ListarPorUsuario(usuario.Id);

            if (!IsPostBack)
            {
                // Datos del perfil
                lblNombreUsuario.Text = usuario.NombreUsuario;
                txtNombre.Text = usuario.Nombre;
                txtEmail.Text = usuario.Email;
                txtTelefono.Text = usuario.Telefono;
                lblRol.Text = usuario.Rol;

                // Mensaje de bienvenida si recién se registró
                if (Session["reciénRegistrado"] != null)
                {
                    lblBienvenida.Text = "¡Gracias por registrarte, " + usuario.NombreUsuario + "!";
                    Session.Remove("reciénRegistrado");
                }

                // Cargar direcciones en el DropDownList todo en una sola linea
                if (direcciones != null && direcciones.Count > 0)
                {
                    ddlDirecciones.DataSource = direcciones;
                    ddlDirecciones.DataTextField = "DescripcionCompleta";
                    ddlDirecciones.DataValueField = "Id";
                    ddlDirecciones.DataBind();

                    // Mostrar la primera dirección en los campos individuales
                    Direccion direccion = direcciones[0];
                    txtCalle.Text = direccion.Calle;
                    txtNumero.Text = direccion.Numero;
                    txtCiudad.Text = direccion.Ciudad;
                    txtCodigoPostal.Text = direccion.CodigoPostal;
                    txtProvincia.Text = direccion.Provincia;
                }
            }
        }


        protected void btnActualizar_Click(object sender, EventArgs e)
        {
            // Validar sesión
            if (Session["usuario"] == null)
            {
                Response.Redirect("~/PaginasPublic/Login.aspx");
                return;
            }

            Usuario usuario = (Usuario)Session["usuario"];

            // Actualizar datos del perfil
            usuario.Nombre = txtNombre.Text;
            usuario.Email = txtEmail.Text;
            usuario.Telefono = txtTelefono.Text;

            UsuarioNegocio negocioUsuario = new UsuarioNegocio();
            negocioUsuario.Actualizar(usuario);

            // Validar dirección seleccionada
            
            if (string.IsNullOrEmpty(ddlDirecciones.SelectedValue))
            {
                lblResultado.Text = "Error: no se seleccionó ninguna dirección.";
                return;
            }

            int idDireccion;
            bool conversionOk = int.TryParse(ddlDirecciones.SelectedValue, out idDireccion);
            if (!conversionOk || idDireccion <= 0)
            {
                lblResultado.Text = "Error: dirección seleccionada inválida.";
                return;
            }

            // Crear el objetio
            Direccion direccion = new Direccion
            {
                Id = idDireccion,
                IdUsuario = usuario.Id,
                Calle = txtCalle.Text,
                Numero = txtNumero.Text,
                Ciudad = txtCiudad.Text,
                CodigoPostal = txtCodigoPostal.Text,
                Provincia = txtProvincia.Text
            };


            DireccionNegocio negocioDireccion = new DireccionNegocio();
            negocioDireccion.modificar(direccion);

            lblResultado.Text = "Datos actualizados correctamente.";

            // Refrescar el DropDownList de direcciones porque sino aparece la vieja
            List<Direccion> direccionesActualizadas = negocioDireccion.ListarPorUsuario(usuario.Id);
            ddlDirecciones.DataSource = direccionesActualizadas;
            ddlDirecciones.DataTextField = "DescripcionCompleta";
            ddlDirecciones.DataValueField = "Id";
            ddlDirecciones.DataBind();

            // Seleccionar la dirección actualizada
            ddlDirecciones.SelectedValue = direccion.Id.ToString();

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

        protected void ddlDirecciones_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Tomar el Id seleccionado
            int idSeleccionado = int.Parse(ddlDirecciones.SelectedValue);

            // Traer la dirección y mostrarla
            DireccionNegocio direccionNegocio = new DireccionNegocio();
            Direccion direccion = direccionNegocio.ObtenerPorId(idSeleccionado);

            if (direccion != null)
            {
                txtCalle.Text = direccion.Calle;
                txtNumero.Text = direccion.Numero;
                txtCiudad.Text = direccion.Ciudad;
                txtCodigoPostal.Text = direccion.CodigoPostal;
                txtProvincia.Text = direccion.Provincia;
            }
        }
        protected void btnConfirmarAgregarDireccion_Click(object sender, EventArgs e)
        {
            if (Session["usuario"] == null)
            {
                Response.Redirect("~/PaginasPublic/Login.aspx");
                return;
            }

            Usuario usuario = (Usuario)Session["usuario"];
            Direccion nueva = new Direccion
            {
                IdUsuario = usuario.Id,
                Calle = txtNuevaCalle.Text,
                Numero = txtNuevoNumero.Text,
                Ciudad = txtNuevaCiudad.Text,
                CodigoPostal = txtNuevoCodigoPostal.Text,
                Provincia = txtNuevaProvincia.Text
            };

            DireccionNegocio negocio = new DireccionNegocio();
            negocio.agregar(nueva);

            // Recargar el DropDownList
            List<Direccion> direcciones = negocio.ListarPorUsuario(usuario.Id);
            ddlDirecciones.DataSource = direcciones;
            ddlDirecciones.DataTextField = "DescripcionCompleta";
            ddlDirecciones.DataValueField = "Id";
            ddlDirecciones.DataBind();

            // Mostrar la nueva dirección
            Direccion direccion = direcciones.Last();
            ddlDirecciones.SelectedValue = direccion.Id.ToString();
            txtCalle.Text = direccion.Calle;
            txtNumero.Text = direccion.Numero;
            txtCiudad.Text = direccion.Ciudad;
            txtCodigoPostal.Text = direccion.CodigoPostal;
            txtProvincia.Text = direccion.Provincia;

            lblResultado.Text = "Dirección agregada correctamente.";
        }

        protected void btnEliminarDireccion_Click(object sender, EventArgs e)
        {
            if (Session["usuario"] == null)
            {
                Response.Redirect("~/PaginasPublic/Login.aspx");
                return;
            }

            if (ddlDirecciones.Items.Count == 0)
            {
                lblResultado.Text = "No hay ninguna dirección para eliminar.";
                return;
            }

            int idDireccion;
            if (!int.TryParse(ddlDirecciones.SelectedValue, out idDireccion))
            {
                lblResultado.Text = "Error: dirección seleccionada inválida.";
                return;
            }

            DireccionNegocio negocioDireccion = new DireccionNegocio();
            negocioDireccion.eliminar(idDireccion);

            // Recargar el DropDownList
            Usuario usuario = (Usuario)Session["usuario"];
            List<Direccion> direccionesActualizadas = negocioDireccion.ListarPorUsuario(usuario.Id);

            ddlDirecciones.DataSource = direccionesActualizadas;
            ddlDirecciones.DataTextField = "DescripcionCompleta";
            ddlDirecciones.DataValueField = "Id";
            ddlDirecciones.DataBind();

            // Mostrar la primera dirección o limpiar si no hay ninguna
            if (direccionesActualizadas.Count > 0)
            {
                Direccion direccion = direccionesActualizadas[0];
                ddlDirecciones.SelectedValue = direccion.Id.ToString();
                txtCalle.Text = direccion.Calle;
                txtNumero.Text = direccion.Numero;
                txtCiudad.Text = direccion.Ciudad;
                txtCodigoPostal.Text = direccion.CodigoPostal;
                txtProvincia.Text = direccion.Provincia;
            }
            else
            {
                txtCalle.Text = "";
                txtNumero.Text = "";
                txtCiudad.Text = "";
                txtCodigoPostal.Text = "";
                txtProvincia.Text = "";
            }

            lblResultado.Text = "Dirección eliminada correctamente.";
        }
    }
}