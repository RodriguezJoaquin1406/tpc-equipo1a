using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Dominio;
using Domio;
using Negocio;

namespace Tp_Cuatrimestral_Equipo1A.PaginasAdministrador
{
    public partial class AdministradorProducto : System.Web.UI.Page
    {
        protected Producto productoModificar;
        protected bool prodError;
        protected bool prodSuccess;


        protected int ImagenCount
        {
            get { return (int)(ViewState["ImagenCount"] ?? 0); }
            set { ViewState["ImagenCount"] = value; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {

                if (Session["usuario"] == null || !((Dominio.Usuario)Session["usuario"]).EsAdmin)
                {
                    Response.Redirect("../PaginasPublic/Inicio.aspx");
                }

                CargarCategorias();

                if (Request.QueryString["Id"] == null)
                {
                    ImagenCount = 1;
                }
                else
                {
                    int id;
                    if (int.TryParse(Request.QueryString["Id"], out id))
                    {
                        var negocio = new ProductoNegocio();
                        productoModificar = negocio.buscarPorId(id);

                        if (productoModificar != null)
                        {
                            var imgs = productoModificar.Imagenes ?? new List<string>();
                            ViewState["InitialImages"] = imgs;
                            ImagenCount = imgs.Count > 0 ? imgs.Count : 1;
                        }
                        else
                        {
                            ImagenCount = 1;
                            prodError = true;
                            lblError.Text = "No se pudo encontrar el producto en la base de datos";
                        }
                    }
                    else
                    {
                        ImagenCount = 1;
                    }
                }
            }

            CreateImageTextBoxes();


            prodError = false;
            prodSuccess = false;

            if (!IsPostBack)
            {
                if (productoModificar != null)
                {
                    txtId.Text = productoModificar.Id.ToString();
                    txtNombre.Text = productoModificar.Nombre;
                    txtDesc.Text = productoModificar.Descripcion;
                    txtPrecio.Text = productoModificar.PrecioBase.ToString(System.Globalization.CultureInfo.InvariantCulture);
                    txtStock.Text = productoModificar.StockActual.ToString();
                    txtStockMinimo.Text = productoModificar.StockMinimo.ToString();
                    ddlTalle.SelectedValue = productoModificar.Talle;

                    if (productoModificar.Imagenes != null && productoModificar.Imagenes.Count > 0)
                    {
                        txtImagenes.Text = string.Join(",", productoModificar.Imagenes);
                    }

                    if (productoModificar.Categoria != null)
                    {
                        ddlCategoria.SelectedValue = productoModificar.Categoria.Id.ToString();
                    }
                }
            }
        }

        

        private void CargarCategorias()
        {
            try
            {
                CategoriaNegocio categoriaNegocio = new CategoriaNegocio();
                var categorias = categoriaNegocio.listar();

                ddlCategoria.DataSource = categorias;
                ddlCategoria.DataTextField = "Nombre";
                ddlCategoria.DataValueField = "Id";
                ddlCategoria.DataBind();

                ddlCategoria.Items.Insert(0, new ListItem("Seleccione una categoría", ""));
            }
            catch (Exception)
            {
                // En caso de error, mantener solo el item por defecto
            }
        }

        private void CreateImageTextBoxes()
        {
            phImagenes.Controls.Clear();
            var initial = ViewState["InitialImages"] as List<string>;
            var current = ViewState["CurrentImageValues"] as List<string>;

            for (int i = 0; i < ImagenCount; i++)
            {
                var tb = new TextBox
                {
                    ID = "img_" + i,
                    CssClass = "form-control img-input img-url mb-2"
                };
                tb.Attributes["placeholder"] = "URL de imagen " + (i + 1);

                // Priorizar valores actuales guardados, luego iniciales
                if (current != null && i < current.Count && !string.IsNullOrEmpty(current[i]))
                {
                    tb.Text = current[i];
                }
                else if (!IsPostBack && initial != null && i < initial.Count)
                {
                    tb.Text = initial[i];
                }

                phImagenes.Controls.Add(tb);
            }
        }

        protected void btnAgregarImagen_Click(object sender, EventArgs e)
        {
            // Guardar los valores actuales antes de incrementar el contador
            GuardarValoresImagenes();

            ImagenCount = ImagenCount + 1;
            CreateImageTextBoxes();
        }


        private void GuardarValoresImagenes()
        {
            List<string> valoresActuales = new List<string>();

            // Obtener los valores de los TextBox actuales
            for (int i = 0; i < ImagenCount; i++)
            {
                var tb = phImagenes.FindControl("img_" + i) as TextBox;
                if (tb != null)
                {
                    valoresActuales.Add(tb.Text ?? string.Empty);
                }
                else
                {
                    valoresActuales.Add(string.Empty);
                }
            }

            // Guardar en ViewState
            ViewState["CurrentImageValues"] = valoresActuales;
        }

        protected void btnAceptar_Click(object sender, EventArgs e)
        {
            try
            {
                Producto producto = new Producto();
                ProductoNegocio negocio = new ProductoNegocio();

                // Validaciones básicas
                if (string.IsNullOrEmpty(txtNombre.Text.Trim()))
                {
                    prodError = true;
                    lblError.Text = "El nombre del producto es requerido.";
                    return;
                }

                if (string.IsNullOrEmpty(ddlCategoria.SelectedValue))
                {
                    prodError = true;
                    lblError.Text = "Debe seleccionar una categoría.";
                    return;
                }

                if (string.IsNullOrEmpty(ddlTalle.SelectedValue))
                {
                    prodError = true;
                    lblError.Text = "Debe seleccionar un talle.";
                    return;
                }

                decimal precio;
                if (!decimal.TryParse(txtPrecio.Text, System.Globalization.NumberStyles.Any, System.Globalization.CultureInfo.InvariantCulture, out precio) || precio < 0)
                {
                    prodError = true;
                    lblError.Text = "El precio debe ser un número válido mayor o igual a 0.";
                    return;
                }

                int stock;
                if (!int.TryParse(txtStock.Text, out stock) || stock < 0)
                {
                    prodError = true;
                    lblError.Text = "El stock debe ser un número válido mayor o igual a 0.";
                    return;
                }

                int stockMinimo;
                if (!int.TryParse(txtStockMinimo.Text, out stockMinimo) || stockMinimo < 0)
                {
                    prodError = true;
                    lblError.Text = "El stock mínimo debe ser un número válido mayor o igual a 0.";
                    return;
                }

                if (stockMinimo > stock)
                {
                    prodError = true;
                    lblError.Text = "El stock mínimo no puede ser mayor que el stock actual.";
                    return;
                }

                // Cargar datos del producto
                producto.Nombre = txtNombre.Text.Trim();
                producto.Descripcion = txtDesc.Text.Trim();
                producto.Talle = ddlTalle.SelectedValue;
                producto.PrecioBase = precio;
                producto.StockActual = stock;
                producto.StockMinimo = stockMinimo;
                producto.Categoria = new Categoria { Id = int.Parse(ddlCategoria.SelectedValue) };

                // Cargar imágenes
                List<string> imagenes = new List<string>();
                for (int i = 0; i < ImagenCount; i++)
                {
                    var tb = phImagenes.FindControl("img_" + i) as TextBox;
                    if (tb != null && !string.IsNullOrEmpty(tb.Text.Trim()))
                    {
                        imagenes.Add(tb.Text.Trim());
                    }
                }
                producto.Imagenes = imagenes;

                if (Request.QueryString["Id"] != null)
                {
                    // Modificar producto existente
                    producto.Id = int.Parse(txtId.Text);
                    negocio.modificar(producto);
                    foreach (var imagen in producto.Imagenes)
                    {
                        negocio.agregarImagen(producto.Id, imagen);
                    }
                    prodSuccess = true;
                    lblSuccess.Text = "Producto modificado exitosamente.";
                }
                else
                {
                    // Agregar nuevo producto
                    int idNuevo = negocio.agregar(producto);
                    foreach (var imagen in producto.Imagenes)
                    {
                        negocio.agregarImagen(idNuevo, imagen);
                    }
                    prodSuccess = true;
                    lblSuccess.Text = "Producto agregado exitosamente.";
                    LimpiarFormulario();
                }
            }
            catch (Exception ex)
            {
                prodError = true;
                lblError.Text = "Error al procesar el producto: " + ex.Message;
            }
        }

        protected void btnEliminar_Click(object sender, EventArgs e)
        {
            try
            {
                if (Request.QueryString["Id"] != null)
                {
                    int id = int.Parse(Request.QueryString["Id"]);
                    ProductoNegocio negocio = new ProductoNegocio();
                    negocio.eliminar(id);

                    Response.Redirect("../PaginasPublic/Catalogo.aspx");
                }
            }
            catch (Exception ex)
            {
                prodError = true;
                lblError.Text = "Error al eliminar el producto: " + ex.Message;
            }
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            Response.Redirect("../PaginasPublic/Catalogo.aspx");
        }

        private void LimpiarFormulario()
        {
            txtNombre.Text = "";
            txtDesc.Text = "";
            txtPrecio.Text = "";
            txtStock.Text = "";
            txtStockMinimo.Text = "";
            ddlCategoria.SelectedIndex = 0;
            ddlTalle.SelectedIndex = 0;
            ImagenCount = 1;
            CreateImageTextBoxes();
        }


    }
}