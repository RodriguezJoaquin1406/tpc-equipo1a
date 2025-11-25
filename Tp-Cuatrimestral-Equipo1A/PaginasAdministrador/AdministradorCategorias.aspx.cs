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

namespace Tp_Cuatrimestral_Equipo1A.PaginasAdministrador
{
    public partial class AdministradorCategorias : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // Validacion Administrador
            if (!IsPostBack)
            {
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
            string script = "$('#modalEdicion').modal('show');";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "ShowModal", script, true);

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

                // Cerrar modal y limpiar backdrop
                System.Text.StringBuilder scr = new System.Text.StringBuilder();
                scr.Append("var modal = bootstrap.Modal.getInstance(document.getElementById('modalEdicion'));");
                scr.Append("if (modal) { modal.hide(); }");
                scr.Append("var backdrops = document.getElementsByClassName('modal-backdrop');");
                scr.Append("while(backdrops[0]) { backdrops[0].parentNode.removeChild(backdrops[0]); }");
                scr.Append("document.body.classList.remove('modal-open');");
                scr.Append("document.body.style = '';");
                ScriptManager.RegisterStartupScript(this, this.GetType(), "HideModalFix", scr.ToString(), true);
            }
            catch (Exception ex)
            {
                // Mostrar el mensaje en el Label dentro del modal
                lblErrorNuevo.Text = ex.Message;

                // Script para limpiar backdrop y reabrir el modal
                System.Text.StringBuilder scr = new System.Text.StringBuilder();
                scr.Append("var backdrops = document.getElementsByClassName('modal-backdrop');");
                scr.Append("while(backdrops[0]) { backdrops[0].parentNode.removeChild(backdrops[0]); }");
                scr.Append("document.body.classList.remove('modal-open');");
                scr.Append("document.body.style = '';");

                // Reabrir el modal
                scr.Append("$('#modalNuevo').modal('show');");

                ScriptManager.RegisterStartupScript(this, this.GetType(), "ErrorNuevo", scr.ToString(), true);
            }
        }

        protected void btnEliminar_Click(object sender, EventArgs e)
        {
            // A. Recuperar el ID del botón
            Button btn = (Button)sender;
            int idCategoria = int.Parse(btn.CommandArgument);

            hfIdEliminar.Value = idCategoria.ToString();

            CategoriaNegocio negocio = new CategoriaNegocio();
            List<Categoria> lista = negocio.listar();
            Categoria seleccionada = lista.Find(x => x.Id == idCategoria);

            lblMensajeEliminar.Text = "¿Estás seguro que deseas eliminar la categoría <b>" + seleccionada.Nombre + "</b>?";

            string script = "$('#modalConfirmaEliminar').modal('show');";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "ShowModalEliminar", script, true);
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

                // Cerrar modal y limpiar 
                System.Text.StringBuilder scr = new System.Text.StringBuilder();
                scr.Append("var modal = bootstrap.Modal.getInstance(document.getElementById('modalConfirmaEliminar'));");
                scr.Append("if (modal) { modal.hide(); }");
                scr.Append("var backdrops = document.getElementsByClassName('modal-backdrop');");
                scr.Append("while(backdrops[0]) { backdrops[0].parentNode.removeChild(backdrops[0]); }");
                scr.Append("document.body.classList.remove('modal-open');");
                scr.Append("document.body.style = '';");
                ScriptManager.RegisterStartupScript(this, this.GetType(), "HideModalEliminar", scr.ToString(), true);
            }
            catch (Exception ex)
            {
                // Mostrar el mensaje en el Label dentro del modal
                lblErrorNuevo.Text = ex.Message;

                // Script para limpiar backdrop y reabrir el modal
                System.Text.StringBuilder scr = new System.Text.StringBuilder();
                scr.Append("var backdrops = document.getElementsByClassName('modal-backdrop');");
                scr.Append("while(backdrops[0]) { backdrops[0].parentNode.removeChild(backdrops[0]); }");
                scr.Append("document.body.classList.remove('modal-open');");
                scr.Append("document.body.style = '';");

                // Reabrir el modal
                scr.Append("$('#modalNuevo').modal('show');");

                ScriptManager.RegisterStartupScript(this, this.GetType(), "ErrorNuevo", scr.ToString(), true);
            }
        }


        protected void btnNuevaCategoria_Click(object sender, EventArgs e)
        {
            txtNombreNuevo.Text = string.Empty;

            string script = "$('#modalNuevo').modal('show');";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "ShowmodalNuevo", script, true);
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

                // Cerrar modal y limpiar backdrop
                System.Text.StringBuilder scr = new System.Text.StringBuilder();
                scr.Append("var modal = bootstrap.Modal.getInstance(document.getElementById('modalNuevo'));");
                scr.Append("if (modal) { modal.hide(); }");
                scr.Append("var backdrops = document.getElementsByClassName('modal-backdrop');");
                scr.Append("while(backdrops[0]) { backdrops[0].parentNode.removeChild(backdrops[0]); }");
                scr.Append("document.body.classList.remove('modal-open');");
                scr.Append("document.body.style = '';");
                ScriptManager.RegisterStartupScript(this, this.GetType(), "HideModalNuevo", scr.ToString(), true);
            }
            catch (Exception ex)
            {
                // Mostrar el mensaje en el Label dentro del modal
                lblErrorNuevo.Text = ex.Message;

                // Script para limpiar backdrop y reabrir el modal
                System.Text.StringBuilder scr = new System.Text.StringBuilder();
                scr.Append("var backdrops = document.getElementsByClassName('modal-backdrop');");
                scr.Append("while(backdrops[0]) { backdrops[0].parentNode.removeChild(backdrops[0]); }");
                scr.Append("document.body.classList.remove('modal-open');");
                scr.Append("document.body.style = '';");

                // Reabrir el modal
                scr.Append("$('#modalNuevo').modal('show');");

                ScriptManager.RegisterStartupScript(this, this.GetType(), "ErrorNuevo", scr.ToString(), true);
            }
        }
    }
}