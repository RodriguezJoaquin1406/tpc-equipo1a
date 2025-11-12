using Dominio;
using System;
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
                        // Bind data to all controls
                        lblNombreProducto.Text = producto.Nombre;
                        lblCategoria.Text = producto.Categoria?.Nombre;
                        lblPrecio.Text = producto.PrecioBase.ToString("C");
                        lblDescripcion.Text = producto.Descripcion;

                        // Bind images to the carousel repeater
                        if (producto.Imagenes != null && producto.Imagenes.Any())
                        {
                            RepeaterImagenes.DataSource = producto.Imagenes;
                            RepeaterImagenes.DataBind();
                        }
                        
                        // You can continue binding other controls here...
                        // ddlTalle.DataSource = ...
                        // ddlColor.DataSource = ...
                    }
                    else
                    {
                        Response.Redirect("Catalogo.aspx", false);
                    }
                }
                catch (Exception ex)
                {
                    // It's a good practice to log the exception
                    // Log.Error(ex);
                    Response.Redirect("Error.aspx", false); // Redirect to a generic error page
                }
            }
        }

        public void btnAgregarCarrito_Click(object sender, EventArgs e)
        {
            // Add to cart logic
        }
    }
}