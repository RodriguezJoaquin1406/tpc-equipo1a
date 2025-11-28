using Dominio;
using Negocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using Tp_Cuatrimestral_Equipo1A.Helpers;

namespace Tp_Cuatrimestral_Equipo1A.PaginasPublic
{
    public partial class FinalizarCompra : System.Web.UI.Page
    {
        private Usuario _usuarioLogueado;

        protected void Page_Load(object sender, EventArgs e)
        {
            _usuarioLogueado = (Usuario)Session["usuario"];

            if (!IsPostBack)
            {
                if (_usuarioLogueado == null)
                {
                    Response.Redirect("../Login.aspx", false);
                    return;
                }

                cargarDatosUsuario();
                cargarDirecciones();
                cargarMetodoPago();
                cargarResumen();
            }
        }
        private void cargarDatosUsuario()
        {
            lblNombre.Text = _usuarioLogueado.Nombre;
            lblNombreUsuario.Text = _usuarioLogueado.NombreUsuario;
            lblEmail.Text = _usuarioLogueado.Email;
            lblTelefono.Text = _usuarioLogueado.Telefono ?? "-";
        }

        private void cargarMetodoPago()
        {
            MetodoPagoNegocio negocio = new MetodoPagoNegocio();
            ddlMetodoPago.DataSource = negocio.Listar();
            ddlMetodoPago.DataTextField = "Nombre";
            ddlMetodoPago.DataValueField = "Id";
            ddlMetodoPago.DataBind();
        }

        private void cargarResumen()
        {
            ItemCarritoNegocio negocio = new ItemCarritoNegocio();
            var resumen = negocio.obtenerResumen(_usuarioLogueado.Id);

            rptResumen.DataSource = resumen;
            rptResumen.DataBind();

            decimal total = resumen.Sum(item => item.Subtotal);
            litTotal.Text = total.ToString("C2");
        }
        public Usuario UsuarioLogueado
        {
            get { return _usuarioLogueado; }
        }

        private void mostrarDatosUsuario()
        {
            lblNombre.Text = _usuarioLogueado.Nombre;
            lblNombreUsuario.Text = _usuarioLogueado.NombreUsuario;
            lblEmail.Text = _usuarioLogueado.Email;
            lblTelefono.Text = _usuarioLogueado.Telefono;
        }

        private void cargarDirecciones()
        {
            DireccionNegocio dirNegocio = new DireccionNegocio();
            var direcciones = dirNegocio.ListarPorUsuario(_usuarioLogueado.Id);

            if (direcciones == null || direcciones.Count == 0)
            {
                ddlDireccion.Visible = false;
                lblSinDireccion.Visible = true;
                btnAgregarDireccion.Visible = true;
            }
            else
            {
                ddlDireccion.Visible = true;
                lblSinDireccion.Visible = false;
                btnAgregarDireccion.Visible = false;

                // Mostrar Calle + Número + Ciudad
                ddlDireccion.DataSource = direcciones.Select(d => new
                {
                    Id = d.Id,
                    Texto = $"{d.Calle}, {d.Numero}, {d.Ciudad}, {d.Provincia}, CP {d.CodigoPostal}"

                }).ToList();

                ddlDireccion.DataTextField = "Texto";
                ddlDireccion.DataValueField = "Id";
                ddlDireccion.DataBind();
            }
        }

        private void cargarMetodosPago()
        {
            MetodoPagoNegocio mpNegocio = new MetodoPagoNegocio();
            ddlMetodoPago.DataSource = mpNegocio.Listar();
            ddlMetodoPago.DataTextField = "Nombre";
            ddlMetodoPago.DataValueField = "Id";
            ddlMetodoPago.DataBind();
        }

        private void cargarResumenCarrito()
        {
            ItemCarritoNegocio carritoNegocio = new ItemCarritoNegocio();
            List<ItemCarrito> carrito = carritoNegocio.listarCarrito(_usuarioLogueado.Id);

            rptResumen.DataSource = carrito;
            rptResumen.DataBind();

            litTotal.Text = carrito.Sum(x => x.Subtotal).ToString("C2");
        }

        protected void btnAgregarDireccion_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "abrirModal", "$('#modalDireccion').modal('show');", true);
        }

        protected void btnConfirmarDireccion_Click(object sender, EventArgs e)
        {
            if (_usuarioLogueado == null)
            {
                Response.Redirect("../Login.aspx", false);
                return;
            }

            try
            {
                Direccion nueva = new Direccion
                {
                    IdUsuario = _usuarioLogueado.Id,
                    Calle = txtCalle.Text.Trim(),
                    Numero = txtNumero.Text.Trim(),
                    Ciudad = txtCiudad.Text.Trim(),
                    CodigoPostal = txtCodigoPostal.Text.Trim(),
                    Provincia = txtProvincia.Text.Trim()
                };

                DireccionNegocio dirNegocio = new DireccionNegocio();
                dirNegocio.agregar(nueva);

                // Guardo un flag
                Session["direccionAgregada"] = true;

                //  mismo endpoint, pero ahora en GET, sino cuando apreto f5 me vuelve a cargar la dirección otra vez
                Response.Redirect(Request.RawUrl, false);

            }
            catch (Exception ex)
            {
                // si da error
                lblSinDireccion.Text = "Error al guardar la dirección: " + ex.Message;
                lblSinDireccion.CssClass = "text-danger fw-bold";
                lblSinDireccion.Visible = true;
            }
        }

        protected void btnConfirmarCompra_Click(object sender, EventArgs e)
        {
            try
            {
                // Validar sesión
                if (_usuarioLogueado == null)
                {
                    Response.Redirect("../Login.aspx", false);
                    return;
                }

                // Validar selección de dirección y método de pago
                if (ddlDireccion.SelectedItem == null || ddlMetodoPago.SelectedItem == null)
                {
                    lblErrorCompra.Text = "Debés seleccionar una dirección y un método de pago.";
                    lblErrorCompra.Visible = true;
                    return;
                }

                int idDireccion = int.Parse(ddlDireccion.SelectedValue);
                int idMetodoPago = int.Parse(ddlMetodoPago.SelectedValue);

                // Obtener ítems del carrito
                ItemCarritoNegocio carritoNegocio = new ItemCarritoNegocio();
                List<ItemCarrito> carrito = carritoNegocio.listarCarrito(_usuarioLogueado.Id);

                if (carrito == null || carrito.Count == 0)
                {
                    lblErrorCompra.Text = "Tu carrito está vacío. No se puede confirmar la compra.";
                    lblErrorCompra.Visible = true;
                    return;
                }

                // Calcular total
                decimal total = carrito.Sum(item => item.Subtotal);

                // Crear pedido
                Pedido pedido = new Pedido
                {
                    IdUsuario = _usuarioLogueado.Id,
                    Fecha = DateTime.Now,
                    IdDireccion = idDireccion,
                    IdMetodoPago = idMetodoPago,
                    Total = total,
                    Estado = "Pendiente",

                };

                PedidoNegocio pedidoNegocio = new PedidoNegocio();
                int idPedido = pedidoNegocio.crearPedido(pedido); // devuelve el ID generado

                // Crear detalle de pedido
                DetallePedidoNegocio detalleNegocio = new DetallePedidoNegocio();
                foreach (var item in carrito)
                {
                    DetallePedido detalle = new DetallePedido
                    {
                        IdPedido = idPedido,
                        IdProducto = item.IdProducto,
                        Talle = item.talle,
                        Cantidad = item.cantidad,
                        PrecioUnitario = item.precio
                    };

                    detalleNegocio.agregar(detalle);
                }

                // Descontar Stock
                foreach (var item in carrito)
                {
                    ProductoNegocio productoNegocio = new ProductoNegocio();
                    productoNegocio.descontarStock(item.IdProducto, item.cantidad);
                }

                // Vaciar carrito
                carritoNegocio.vaciarCarrito(_usuarioLogueado.Id);

                // Redirigir
                Response.Redirect("MisCompras.aspx", false);
            }
            catch (Exception ex)
            {
                lblErrorCompra.Text = "Error al confirmar la compra: " + ex.Message;
                lblErrorCompra.Visible = true;
            }
        }

    }
}