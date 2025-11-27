using Dominio;
using Negocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Tp_Cuatrimestral_Equipo1A.PaginasPublic
{
    public partial class MisCompras : System.Web.UI.Page
    {
        protected Usuario _usuarioLogueado;

        protected void Page_Load(object sender, EventArgs e)
        {
            _usuarioLogueado = (Usuario)Session["usuario"];

            if (_usuarioLogueado == null)
            {
                Response.Redirect("../Login.aspx", false);
                return;
            }

            if (!IsPostBack)
            {
                cargarPedidos();
            }
        }

        private void cargarPedidos()
        {
            PedidoNegocio pedidoNegocio = new PedidoNegocio();
            List<Pedido> pedidos = pedidoNegocio.listarPorUsuario(_usuarioLogueado.Id);

            if (pedidos == null || pedidos.Count == 0)
            {
                lblSinCompras.Text = "No tenés compras registradas.";
                lblSinCompras.Visible = true;
                return;
            }

            // Cargar detalles para cada pedido
            DetallePedidoNegocio detalleNegocio = new DetallePedidoNegocio();
            foreach (var pedido in pedidos)
            {
                pedido.Detalles = detalleNegocio.ListarPorPedido(pedido.Id);
            }

            rptPedidos.DataSource = pedidos;
            rptPedidos.DataBind();
        }
    }
}