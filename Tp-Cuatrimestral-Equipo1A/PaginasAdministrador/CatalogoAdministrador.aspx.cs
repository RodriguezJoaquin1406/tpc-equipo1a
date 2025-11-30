using Dominio;
using Negocio;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Tp_Cuatrimestral_Equipo1A.Helpers;

namespace Tp_Cuatrimestral_Equipo1A.PaginasAdministrador
{
    public partial class CatalogoAdministrador : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["usuario"] == null || !((Dominio.Usuario)Session["usuario"]).EsAdmin)
                {
                    Response.Redirect("../PaginasPublic/Inicio.aspx");
                }
                cargarFiltros();
                cargarTabla();
            }
        }

        protected void cargarTabla()
        {
            try
            {
                string categoria = ddlCategoria.SelectedValue;
                string talle = ddlTalle.SelectedValue;
                string busqueda = txtBusqueda.Text.quitarAcentos();

                ProductoNegocio negocio = new ProductoNegocio();
                List<Producto> listaProductos;

                // Si ambos filtros están en "Todos", usar el método listar normal
                if (categoria == "Todos" && talle == "Todos" && string.IsNullOrWhiteSpace(busqueda))
                {
                    listaProductos = negocio.listar();
                }
                else
                {
                    // Usar el método filtrado
                    listaProductos = negocio.listarFiltrado(categoria, talle);

                    if (!string.IsNullOrWhiteSpace(busqueda))
                    {
                        listaProductos = listaProductos.Where(p =>
                        p.Nombre.quitarAcentos().Contains(busqueda) ||
                        p.Descripcion.quitarAcentos().Contains(busqueda)
                        ).ToList();
                    }
                }

                dgvProductos.DataSource = listaProductos;
                dgvProductos.DataBind();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void cargarFiltros()
        {
            try
            {
                // Cargar categorías
                CategoriaNegocio catNegocio = new CategoriaNegocio();
                ddlCategoria.DataSource = catNegocio.listar();
                ddlCategoria.DataTextField = "Nombre";
                ddlCategoria.DataValueField = "Nombre";
                ddlCategoria.DataBind();
                ddlCategoria.Items.Insert(0, "Todos");

                // Cargar talles únicos
                ProductoNegocio prodNegocio = new ProductoNegocio();
                var talles = prodNegocio.listar()
                    .Select(p => p.Talle)
                    .Distinct()
                    .OrderBy(t => t)
                    .ToList();

                ddlTalle.DataSource = talles;
                ddlTalle.DataBind();
                ddlTalle.Items.Insert(0, "Todos");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void ddlCategoria_SelectedIndexChanged(object sender, EventArgs e)
        {
            cargarTabla();
        }

        protected void ddlTalle_SelectedIndexChanged(object sender, EventArgs e)
        {
            cargarTabla();
        }
        protected void txtBusqueda_TextChanged(object sender, EventArgs e)
        {
            cargarTabla();
        }

        protected void btnLimpiarFiltros_Click(object sender, EventArgs e)
        {
            ddlCategoria.SelectedIndex = 0; // "Todos"
            ddlTalle.SelectedIndex = 0;     // "Todos"
            txtBusqueda.Text = "";
            cargarTabla();
        }
    }
}