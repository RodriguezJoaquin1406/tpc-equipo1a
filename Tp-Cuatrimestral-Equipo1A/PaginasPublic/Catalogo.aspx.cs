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
                var negocio = new ProductoNegocio();
                var productos = negocio.listar();
                RepeaterProductos.DataSource = productos;
                RepeaterProductos.DataBind();
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

    }
}