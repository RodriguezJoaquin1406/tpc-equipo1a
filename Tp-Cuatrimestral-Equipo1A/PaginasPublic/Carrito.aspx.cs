using System;
using System.Collections.Generic;
using System.EnterpriseServices;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Dominio;
using Negocio;
using Tp_Cuatrimestral_Equipo1A.Helpers;

namespace Tp_Cuatrimestral_Equipo1A.PaginasPublic
{
    public partial class Carrito : System.Web.UI.Page
    {
        public Usuario UsuarioLogueado
        {
            get { return Session["usuario"] != null ? (Usuario)Session["usuario"] : null; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CargarCarrito();
            }
        }
        private void CargarCarrito()
        {
            try
            {
                ItemCarritoNegocio carritoNegocio = new ItemCarritoNegocio();
                List<ItemCarrito> listaProductos = new List<ItemCarrito>();

                if (UsuarioLogueado != null)
                {
                    // hay usuario logueado entonces cargamos su carrito desde la base de datos
                    listaProductos = carritoNegocio.listarCarrito(UsuarioLogueado.Id);

                }
                else
                {
                    // Manejar el caso cuando no hay usuario logueado
                    listaProductos = new List<ItemCarrito>();
                    // Hacer una carga de productos por session y mostrarlos
                    if (Session["carritoTemporal"] != null)
                    {
                        listaProductos = (List<ItemCarrito>)Session["carritoTemporal"];
                    }
                }

                if (listaProductos.Count > 0 && listaProductos != null)
                {
                    // Confirmado usuario tiene un carrito existente entonces cargamos repeater
                    rptCarrito.DataSource = listaProductos;
                    rptCarrito.DataBind();
                    lblMensajeCarrito.Visible = false;


                    litCantidad.Text = listaProductos.Count().ToString();
                    litTotal.Text = listaProductos.Sum(x => x.Subtotal).ToString("C2");
                }
                else
                {
                    lblMensajeCarrito.Text = "Tu carrito está vacío.";
                    lblMensajeCarrito.Visible = true;
                }

                upModal.Update();

            }
            catch (Exception)
            {

                throw;
            }

            
        }

        public void btn_EliminarClick(object sender, EventArgs e)
        {
            // Mandamos dos valores en el boton entonces hacemos un vector y lo spliteamos por ";"
            string[] valores = (((Button)sender).CommandArgument).Split(';');
            // Separamos los valores
            int idProducto = int.Parse(valores[0]);
            int cantidad = int.Parse(valores[1]);
            // Hidden Field para poder capturar valores
            hfIdProductoAEliminar.Value = idProducto.ToString();
            hfCantidadProducto.Value = cantidad.ToString();

            ddlCantidadEliminar.Items.Clear();
            ddlCantidadEliminar.Items.Add(new ListItem("Todo (" + cantidad + ")", cantidad.ToString()));

            for (int i = 1; i < cantidad; i++)
            {
                ddlCantidadEliminar.Items.Add(new ListItem(i.ToString(), i.ToString()));
            }

            ScriptManager.RegisterStartupScript(this, this.GetType(), "abrir", "$('#modalEliminar').modal('show');", true);
        }

        public void btn_ConfirmarEliminarClick(object sender, EventArgs e)
        {
            try
            {
                int idProducto = int.Parse(hfIdProductoAEliminar.Value);
                int cantidadAEliminar = int.Parse(ddlCantidadEliminar.SelectedValue);
                int cantTotalProducto = int.Parse(hfCantidadProducto.Value);

                ItemCarritoNegocio carritoNegocio = new ItemCarritoNegocio();

                if (cantidadAEliminar == cantTotalProducto) 
                {
                    carritoNegocio.eliminarDelCarrito(UsuarioLogueado.Id, idProducto);
                    CargarCarrito();
                }

                if (cantidadAEliminar != cantTotalProducto)
                {
                    int cantFinal = (cantTotalProducto > cantidadAEliminar) ? cantTotalProducto - cantidadAEliminar : 0;
                    if (cantFinal != 0) { carritoNegocio.actualizarCantidad(UsuarioLogueado.Id, idProducto, cantFinal ); }
                }

                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "cerrarModal", "$('#modalEliminar').modal('hide');", true);

                System.Text.StringBuilder scr = new System.Text.StringBuilder();

                scr.Append("var modal = bootstrap.Modal.getInstance(document.getElementById('modalEdicion'));");
                scr.Append("if (modal) { modal.hide(); }"); // Intenta cerrar suavemente

                // FUERZA BRUTA: Elimina el fondo oscuro si quedó pegado
                scr.Append("var backdrops = document.getElementsByClassName('modal-backdrop');");
                scr.Append("while(backdrops[0]) { backdrops[0].parentNode.removeChild(backdrops[0]); }");

                // Restaura el scroll de la página
                scr.Append("document.body.classList.remove('modal-open');");
                scr.Append("document.body.style = '';");

                ScriptManager.RegisterStartupScript(this, this.GetType(), "HideModalFix", scr.ToString(), true);



                CargarCarrito();
            }
            catch (Exception)
            {

                throw;
            }
        }

        protected void btnComprar_Click(object sender, EventArgs e)
        {
            Usuario usuario = Session["usuario"] as Usuario;

            if (Session["usuario"] == null)
            {
                Response.Redirect("~/PaginasPublic/Login.aspx?origen=carrito", false);
                return;
            }

            ItemCarritoNegocio carritoNegocio = new ItemCarritoNegocio();
            List<ItemCarrito> carrito = carritoNegocio.listarCarrito(usuario.Id);

            if (carrito == null || carrito.Count == 0)
            {
                lblMensajeCarrito.Text = "Tu carrito está vacío.";
                lblMensajeCarrito.CssClass = "text-danger fw-bold fs-5";
                return;
            }

            Response.Redirect("FinalizarCompra.aspx", false);
        }
    }
}