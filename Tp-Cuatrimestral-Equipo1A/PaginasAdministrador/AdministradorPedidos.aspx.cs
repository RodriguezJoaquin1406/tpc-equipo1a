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
    public partial class AdministradorPedidos : System.Web.UI.Page
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

        private void cargarTabla(string filtroEstado = "")
        {
            try
            {
                PedidoNegocio negocio = new PedidoNegocio();
                List<Pedido> listaPedidos = negocio.Listar();

                // Aplicar filtro de estado si se especifica
                if (!string.IsNullOrEmpty(filtroEstado))
                {
                    listaPedidos = listaPedidos.Where(p => p.Estado == filtroEstado).ToList();
                }

                // Ordenar por fecha descendente (más recientes primero)
                listaPedidos = listaPedidos.OrderByDescending(p => p.Fecha).ToList();

                dgvPedidos.DataSource = listaPedidos;
                dgvPedidos.DataBind();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected string GetEstadoBadgeClass(string estado)
        {
            if (string.IsNullOrEmpty(estado)) return "bg-secondary";

            switch (estado.ToLower())
            {
                case "pendiente":
                    return "bg-warning text-dark";
                case "pagado":
                    return "bg-success";
                case "enviado":
                    return "bg-primary";
                case "cancelado":
                    return "bg-danger";
                default:
                    return "bg-secondary";
            }
        }

        protected void ddlFiltroEstado_SelectedIndexChanged(object sender, EventArgs e)
        {
            cargarTabla(ddlFiltroEstado.SelectedValue);
        }

        protected void btnVerDetalles_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            int idPedido = int.Parse(btn.CommandArgument);

            try
            {
                PedidoNegocio negocio = new PedidoNegocio();

                // Buscar el pedido en la lista actual para obtener los datos básicos
                List<Pedido> lista = negocio.Listar();
                Pedido pedidoSeleccionado = lista.Find(x => x.Id == idPedido);

                if (pedidoSeleccionado != null)
                {
                    // Cargar información básica
                    lblIdPedidoDetalles.Text = pedidoSeleccionado.Id.ToString();
                    lblUsuario.Text = pedidoSeleccionado.Usuario != null ? pedidoSeleccionado.Usuario.NombreUsuario : "N/A";
                    lblFechaPedido.Text = pedidoSeleccionado.Fecha.ToString("dd/MM/yyyy HH:mm");
                    lblEstadoPedido.Text = pedidoSeleccionado.Estado;
                    lblTotalPedido.Text = pedidoSeleccionado.Total.ToString("C");

                    // Información de dirección y método de pago
                    if (pedidoSeleccionado.Direccion != null)
                    {
                        lblDireccion.Text = $"{pedidoSeleccionado.Direccion.Calle}, {pedidoSeleccionado.Direccion.Ciudad}, {pedidoSeleccionado.Direccion.Provincia}";
                    }
                    else
                    {
                        lblDireccion.Text = "No especificada";
                    }

                    if (pedidoSeleccionado.MetodoPago != null)
                    {
                        lblMetodoPago.Text = pedidoSeleccionado.MetodoPago.Nombre;
                    }
                    else
                    {
                        lblMetodoPago.Text = "No especificado";
                    }

                    // Cargar detalles del pedido (productos)
                    List<DetallePedido> detalles = negocio.listarDetalles(idPedido);
                    dgvDetallesPedido.DataSource = detalles;
                    dgvDetallesPedido.DataBind();

                    ModalHelper.MostrarModal(this, "modalDetalles");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void btnCambiarEstado_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            int idPedido = int.Parse(btn.CommandArgument);

            try
            {
                PedidoNegocio negocio = new PedidoNegocio();
                List<Pedido> lista = negocio.Listar();
                Pedido pedidoSeleccionado = lista.Find(x => x.Id == idPedido);

                if (pedidoSeleccionado != null)
                {
                    hfIdPedidoEstado.Value = idPedido.ToString();
                    ddlNuevoEstado.SelectedValue = pedidoSeleccionado.Estado;
                    lblErrorEstado.Text = "";

                    ModalHelper.MostrarModal(this, "modalCambiarEstado");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void btnGuardarEstado_Click(object sender, EventArgs e)
        {
            try
            {
                int idPedido = int.Parse(hfIdPedidoEstado.Value);
                string nuevoEstado = ddlNuevoEstado.SelectedValue;

                PedidoNegocio negocio = new PedidoNegocio();
                negocio.actualizarEstado(idPedido, nuevoEstado);

                cargarTabla(ddlFiltroEstado.SelectedValue);
                ModalHelper.CerrarModal(this, "modalCambiarEstado");
            }
            catch (Exception ex)
            {
                lblErrorEstado.Text = "Error al cambiar el estado: " + ex.Message;
                ModalHelper.LimpiarBackdropYMostrarModal(this, "modalCambiarEstado");
            }
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            int idPedido = int.Parse(btn.CommandArgument);

            hfIdPedidoCancelar.Value = idPedido.ToString();

            try
            {
                PedidoNegocio negocio = new PedidoNegocio();
                List<Pedido> lista = negocio.Listar();
                Pedido pedidoSeleccionado = lista.Find(x => x.Id == idPedido);

                if (pedidoSeleccionado != null && pedidoSeleccionado.Usuario != null)
                {
                    lblMensajeCancelar.Text = $"¿Estás seguro que deseas cancelar el pedido #{pedidoSeleccionado.Id} del usuario <b>{pedidoSeleccionado.Usuario.NombreUsuario}</b>?";
                }
                else
                {
                    lblMensajeCancelar.Text = $"¿Estás seguro que deseas cancelar el pedido #{idPedido}?";
                }

                ModalHelper.MostrarModal(this, "modalConfirmaCancelar");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void btnConfirmaCancelar_Click(object sender, EventArgs e)
        {
            try
            {
                int idPedido = int.Parse(hfIdPedidoCancelar.Value);

                PedidoNegocio negocio = new PedidoNegocio();
                negocio.actualizarEstado(idPedido, "Cancelado");

                cargarTabla(ddlFiltroEstado.SelectedValue);
                ModalHelper.CerrarModal(this, "modalConfirmaCancelar");
            }
            catch (Exception ex)
            {
                lblErrorCancelar.Text = "Error al cancelar el pedido: " + ex.Message;
                ModalHelper.LimpiarBackdropYMostrarModal(this, "modalConfirmaCancelar");
            }
        }
    }
}