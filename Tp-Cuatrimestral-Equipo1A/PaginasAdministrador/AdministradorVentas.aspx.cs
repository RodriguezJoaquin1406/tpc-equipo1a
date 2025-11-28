using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Dominio;
using Negocio;
using Tp_Cuatrimestral_Equipo1A.Helpers;

namespace Tp_Cuatrimestral_Equipo1A.PaginasAdministrador
{
    public partial class AdministradorVentas : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // Validación Administrador
            if (!IsPostBack)
            {
                if (Session["usuario"] == null || !((Dominio.Usuario)Session["usuario"]).EsAdmin)
                {
                    Response.Redirect("../PaginasPublic/Inicio.aspx");
                }
                cargarTabla();
            }
        }

        private void cargarTabla(DateTime? fechaDesde = null, DateTime? fechaHasta = null)
        {
            try
            {
                VentaNegocio negocio = new VentaNegocio();
                List<Venta> listaVentas = negocio.Listar();

                // Aplicar filtros de fecha si se especifican
                if (fechaDesde.HasValue)
                {
                    listaVentas = listaVentas.Where(v => v.Fecha.Date >= fechaDesde.Value.Date).ToList();
                }

                if (fechaHasta.HasValue)
                {
                    listaVentas = listaVentas.Where(v => v.Fecha.Date <= fechaHasta.Value.Date).ToList();
                }

                // Ordenar por fecha descendente (más recientes primero)
                listaVentas = listaVentas.OrderByDescending(v => v.Fecha).ToList();

                dgvVentas.DataSource = listaVentas;
                dgvVentas.DataBind();

                // Mostrar resumen
                actualizarResumen(listaVentas);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void actualizarResumen(List<Venta> ventas)
        {
            if (ventas != null && ventas.Count > 0)
            {
                decimal totalVentas = ventas.Sum(v => v.Total);
                int cantidadVentas = ventas.Count;

                lblResumen.Text = $"Mostrando {cantidadVentas} venta(s) - Total: {totalVentas:C}";
            }
            else
            {
                lblResumen.Text = "No se encontraron ventas en el período seleccionado.";
            }
        }

        protected decimal GetTotal(object dataItem)
        {
            var venta = (Venta)dataItem;
            return venta.Total;
        }

        protected void btnFiltrar_Click(object sender, EventArgs e)
        {
            DateTime? fechaDesde = null;
            DateTime? fechaHasta = null;

            if (!string.IsNullOrEmpty(txtFechaDesde.Text))
            {
                fechaDesde = DateTime.Parse(txtFechaDesde.Text);
            }

            if (!string.IsNullOrEmpty(txtFechaHasta.Text))
            {
                fechaHasta = DateTime.Parse(txtFechaHasta.Text);
            }

            cargarTabla(fechaDesde, fechaHasta);
        }

        protected void btnLimpiarFiltro_Click(object sender, EventArgs e)
        {
            txtFechaDesde.Text = "";
            txtFechaHasta.Text = "";
            cargarTabla();
        }

        protected void btnVerDetalles_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            int idVenta = int.Parse(btn.CommandArgument);

            try
            {
                VentaNegocio negocio = new VentaNegocio();
                Venta ventaSeleccionada = negocio.buscarPorId(idVenta);

                if (ventaSeleccionada != null)
                {
                    // Cargar información básica de la venta
                    lblIdVentaDetalles.Text = ventaSeleccionada.Id.ToString();
                    lblNumeroFactura.Text = ventaSeleccionada.NumeroFactura;
                    lblFechaVenta.Text = ventaSeleccionada.Fecha.ToString("dd/MM/yyyy HH:mm");

                    // Información del cliente
                    if (ventaSeleccionada.Cliente != null)
                    {
                        lblNombreCliente.Text = ventaSeleccionada.Cliente.Nombre;
                        lblUsuarioCliente.Text = ventaSeleccionada.Cliente.NombreUsuario;
                        lblEmailCliente.Text = ventaSeleccionada.Cliente.Email;
                        lblTelefonoCliente.Text = ventaSeleccionada.Cliente.Telefono ?? "No especificado";
                    }

                    // Cargar detalles de la venta (productos)
                    dgvDetallesVenta.DataSource = ventaSeleccionada.Detalles;
                    dgvDetallesVenta.DataBind();

                    // Total
                    lblTotalVenta.Text = ventaSeleccionada.Total.ToString("C");

                    ModalHelper.MostrarModal(this, "modalDetalles");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void btnEliminar_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            int idVenta = int.Parse(btn.CommandArgument);

            try
            {
                VentaNegocio negocio = new VentaNegocio();
                negocio.eliminarVenta(idVenta);

                // Recargar tabla manteniendo filtros
                DateTime? fechaDesde = null;
                DateTime? fechaHasta = null;

                if (!string.IsNullOrEmpty(txtFechaDesde.Text))
                {
                    fechaDesde = DateTime.Parse(txtFechaDesde.Text);
                }

                if (!string.IsNullOrEmpty(txtFechaHasta.Text))
                {
                    fechaHasta = DateTime.Parse(txtFechaHasta.Text);
                }

                cargarTabla(fechaDesde, fechaHasta);
            }
            catch (Exception ex)
            {
                // Mostrar error
                throw ex;
            }
        }
    }
}