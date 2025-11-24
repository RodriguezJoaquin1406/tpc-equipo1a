using Dominio;
using Negocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI.WebControls;

namespace Tp_Cuatrimestral_Equipo1A.PaginasPublic
{
    public partial class DetalleProducto : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                try
                {
                    if (!int.TryParse(Request.QueryString["Id"], out int id))
                    {
                        Response.Redirect("Catalogo.aspx", false);
                        return;
                    }

                    Negocio.ProductoNegocio productoNegocio = new Negocio.ProductoNegocio();
                    Producto producto = productoNegocio.buscarPorId(id);

                    if (producto != null)   
                    {
                        lblNombreProducto.Text = producto.Nombre;
                        lblCategoria.Text = producto.Categoria?.Nombre;
                        lblPrecio.Text = producto.PrecioBase.ToString("C");
                        lblDescripcion.Text = producto.Descripcion;

                        if (producto.Imagenes != null && producto.Imagenes.Any())
                        {
                            RepeaterImagenes.DataSource = producto.Imagenes;
                            RepeaterImagenes.DataBind();
                        }
                        
                    }
                    else
                    {
                        Response.Redirect("Catalogo.aspx", false);
                    }
                }
                catch (Exception)
                {
                    throw;
                }
            }
        }

        public void btnAgregarCarrito_Click(object sender, EventArgs e)
        {
            // Add to cart logic
            ItemCarritoNegocio itemNegocio = new ItemCarritoNegocio();
            // ejemplo agregarAlCarrito(int idUsuario, int idProducto, int cantidad);   
            Usuario usuario = (Usuario)Session["usuario"];
            int cantidad = int.Parse(txtCantidad.Text);
            if (usuario != null && cantidad >0)
            {
                itemNegocio.agregarAlCarrito(usuario.Id, int.Parse(Request.QueryString["Id"]), cantidad);
            }
        }

        

        protected void cargarTalles()
        {
            List<string> talles = new List<string>() { "S", "M", "L", "XL" };
            ddlTalle.DataSource = talles;
            ddlTalle.DataBind();
        }

        
    }
}