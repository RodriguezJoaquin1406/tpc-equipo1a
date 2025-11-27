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
                        lblStock.Text = $"Stock disponible: {producto.StockActual}";
                        lblTalleDisponible.Text = producto.Talle;
                        lblColorDisponible.Text = "Único"; // o dinámico si tenés color real
                        lblMaterial.Text = "Algodón"; // si tenés campo real, reemplazalo

                        ddlTalle.Items.Clear();
                        ddlTalle.Items.Add(new ListItem(producto.Talle));

                        ddlColor.Items.Clear();
                        ddlColor.Items.Add(new ListItem("Único")); // o dinámico si tenés color

                        txtCantidad.Text = "1";


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
            if (Session["usuario"] == null)
            {
                Response.Redirect("~/PaginasPublic/Login.aspx?origen=carrito", false);
                return;
            }

            int idProducto = int.Parse(Request.QueryString["Id"]);
            int cantidad = int.Parse(txtCantidad.Text);
            string talleSeleccionado = ddlTalle.SelectedValue;

            ProductoNegocio productoNegocio = new ProductoNegocio();
            Producto producto = productoNegocio.buscarPorId(idProducto);

            if (producto == null)
            {
                lblMensaje.Text = "El producto no existe.";
                pnlMensaje.Visible = true;
                return;
            }

            if (cantidad > producto.StockActual)
            {
                lblMensaje.Text = $"No hay suficiente stock. Disponible: {producto.StockActual}";
                pnlMensaje.Visible = true;
                return;
            }

            Usuario usuario = (Usuario)Session["usuario"];
            ItemCarritoNegocio itemNegocio = new ItemCarritoNegocio();
            itemNegocio.agregarAlCarrito(usuario.Id, idProducto, cantidad, talleSeleccionado);

            lblMensaje.Text = "Producto añadido al carrito.";
            pnlMensaje.Visible = true;
        }


        protected void cargarTalles()
        {
            List<string> talles = new List<string>() { "S", "M", "L", "XL" };
            ddlTalle.DataSource = talles;
            ddlTalle.DataBind();
        }

        
    }
}