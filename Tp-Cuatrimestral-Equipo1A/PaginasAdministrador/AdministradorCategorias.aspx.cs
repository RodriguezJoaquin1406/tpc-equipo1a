using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing.Printing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Dominio;
using Negocio;
using Tp_Cuatrimestral_Equipo1A.Helpers;

namespace Tp_Cuatrimestral_Equipo1A.PaginasAdministrador
{
    public partial class AdministradorCategorias : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // Validacion Administrador
            if (!IsPostBack)
            {
                if (Session["usuario"] == null || !((Dominio.Usuario)Session["usuario"]).EsAdmin)
                {
                    Response.Redirect("../PaginasPublic/Inicio.aspx");
                }
                cargarTabla();
            }
        }

        private void cargarTabla()
        {
            try
            {
                CategoriaNegocio negocio = new CategoriaNegocio();
                dgvCategorias.DataSource = negocio.listar();
                dgvCategorias.DataBind();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void btnEditar_Click(object sender, EventArgs e)
        {
            // Recuperamos el ID del botón que fue presionado
            Button btn = (Button)sender;
            int idCategoria = int.Parse(btn.CommandArgument);

            // Buscamos los datos originales en la BD
            CategoriaNegocio negocio = new CategoriaNegocio();
            // Usamos Find para buscar en memoria o creas un método "ObtenerPorId(id)"
            List<Categoria> lista = negocio.listar();
            Categoria seleccionada = lista.Find(x => x.Id == idCategoria);

            // Pre-cargamos los datos en el Modal (TextBox y HiddenField)
            txtNombreEditar.Text = seleccionada.Nombre;
            hfIdCategoria.Value = idCategoria.ToString();

            // Este script se ejecuta apenas termina el postback parcial
            //string script = "$('#modalEdicion').modal('show');";
            //ScriptManager.RegisterStartupScript(this, this.GetType(), "ShowModal", script, true);

            ModalHelper.MostrarModal(this, "modalEdicion");

        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                // Recuperamos los datos del modal
                Categoria categoriaModificada = new Categoria();
                categoriaModificada.Id = int.Parse(hfIdCategoria.Value);
                categoriaModificada.Nombre = txtNombreEditar.Text.Trim();

                // actualizar (lanza excepciones si hay error)
                CategoriaNegocio negocio = new CategoriaNegocio();
                negocio.modificar(categoriaModificada);

                // Recargar tabla
                cargarTabla();

                ModalHelper.CerrarModal(this, "modalEdicion");
                cargarTabla();
            }
            catch (Exception ex)
            {
                // Mostrar el mensaje en el Label dentro del modal
                lblErrorNuevo.Text = ex.Message;

                // Limpiar backdrop y reabrir el modal con error
                ModalHelper.LimpiarBackdropYMostrarModal(this, "modalEdicion");
            }
        }

        protected void btnEliminar_Click(object sender, EventArgs e)
        {
            // Recuperar el ID del botón
            Button btn = (Button)sender;
            int idCategoria = int.Parse(btn.CommandArgument);

            hfIdEliminar.Value = idCategoria.ToString();

            CategoriaNegocio negocio = new CategoriaNegocio();
            List<Categoria> lista = negocio.listar();
            Categoria seleccionada = lista.Find(x => x.Id == idCategoria);

            lblMensajeEliminar.Text = "¿Estás seguro que deseas eliminar la categoría <b>" + seleccionada.Nombre + "</b>?";

            ModalHelper.MostrarModal(this, "modalConfirmaEliminar");
        }

        protected void btnConfirmaEliminar_Click(object sender, EventArgs e)
        {
            try
            {
                // Recuperar el ID guardado
                int idCategoria = int.Parse(hfIdEliminar.Value);

                CategoriaNegocio negocio = new CategoriaNegocio();
                negocio.eliminar(idCategoria); // valida si tiene productos asociados

                // Recargar la grilla
                cargarTabla();
                ModalHelper.CerrarModal(this, "modalConfirmaEliminar");
            }
            catch (Exception ex)
            {
                // Mostrar el mensaje en el Label dentro del modal
                lblErrorNuevo.Text = ex.Message;

                // Limpiar backdrop y mostrar modal de error
                ModalHelper.LimpiarBackdropYMostrarModal(this, "modalNuevo");
            }
        }


        protected void btnNuevaCategoria_Click(object sender, EventArgs e)
        {
            txtNombreNuevo.Text = string.Empty;

            ModalHelper.MostrarModal(this,"modalNuevo");
        }

        protected void btnGuardarNuevo_Click(object sender, EventArgs e)
        {
            try
            {
                Categoria nueva = new Categoria();
                nueva.Nombre = txtNombreNuevo.Text.Trim();

                CategoriaNegocio negocio = new CategoriaNegocio();
                negocio.agregar(nueva);

                cargarTabla();

                ModalHelper.CerrarModal(this, "modalNuevo");
            }
            catch (Exception ex)
            {
                // Mostrar el mensaje en el Label dentro del modal
                lblErrorNuevo.Text = ex.Message;

                // Script para limpiar backdrop y reabrir el modal
                ModalHelper.LimpiarBackdropYMostrarModal(this,"modalNuevo");
            }
        }
    }
}