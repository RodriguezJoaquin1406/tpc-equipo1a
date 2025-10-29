using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Dominio;
using Negocio;

namespace Tp_Cuatrimestral_Equipo1A.PaginasPublic
{
    public partial class Catalogo : System.Web.UI.Page
    {
        public List<Producto> listaProductos { get; set; }
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                ProductoNegocio negocio = new ProductoNegocio();
                try
                {
                    listaProductos = negocio.listar();
                    Session.Add("listaProductos", listaProductos);
                    RepeaterProductos.DataSource = listaProductos;
                    RepeaterProductos.DataBind();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                }
            }
        }
    }
}