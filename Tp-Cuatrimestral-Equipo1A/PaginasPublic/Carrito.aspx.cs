using System;
using System.Collections.Generic;
using System.EnterpriseServices;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Dominio;
using Domio;
using Negocio;

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

            }
            catch (Exception)
            {

                throw;
            }

            
        }
    }
}