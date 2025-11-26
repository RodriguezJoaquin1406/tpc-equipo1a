using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Dominio;
using Negocio;

namespace Tp_Cuatrimestral_Equipo1A.PaginasPublic
{
    public partial class Catalogo : System.Web.UI.Page
    {
        public List<Producto> listaProductos { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                cargarFiltros();      // llena los DropDownList
                filtrarCatalogo();    // carga los productos
            }
        }

        protected void RepeaterProductos_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                var producto = (Producto)e.Item.DataItem;
                var repeaterImagenes = (Repeater)e.Item.FindControl("RepeaterImagenes");

                if (repeaterImagenes != null)
                {
                    repeaterImagenes.DataSource = producto.Imagenes;
                    repeaterImagenes.DataBind();
                }
            }
        }

        private void filtrarCatalogo()
        {
            string categoria = category.SelectedValue;
            string talle = size.SelectedValue;

            ProductoNegocio negocio = new ProductoNegocio();
            List<Producto> lista = negocio.listarFiltrado(categoria, talle);

            if (lista != null && lista.Count > 0)
            {
                RepeaterProductos.DataSource = lista;
                RepeaterProductos.DataBind();
            }
            else
            {
                RepeaterProductos.DataSource = null;
                RepeaterProductos.DataBind();
                //  mostrar mensaje "No se encontraron productos"???
            }
        }

        protected void ddlCategoria_SelectedIndexChanged(object sender, EventArgs e)
        {
            filtrarCatalogo();
        }

        protected void ddlTalle_SelectedIndexChanged(object sender, EventArgs e)
        {
            filtrarCatalogo();
        }

        private void cargarFiltros()
        {
            CategoriaNegocio catNegocio = new CategoriaNegocio();
            category.DataSource = catNegocio.listar(); // devuelve List<Categoria>
            category.DataTextField = "Nombre";
            category.DataValueField = "Nombre";
            category.DataBind();
            category.Items.Insert(0, "Todos");

            ProductoNegocio prodNegocio = new ProductoNegocio();
            var talles = prodNegocio.listar()
                .Select(p => p.Talle)
                .Distinct()
                .ToList();

            size.DataSource = talles;
            size.DataBind();
            size.Items.Insert(0, "Todos");
        }

    }
}