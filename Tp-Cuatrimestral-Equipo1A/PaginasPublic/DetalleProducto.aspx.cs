using Dominio;
using Microsoft.Ajax.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
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
                    int id;

                    // Si no pudo convertir a entero (id nulo, o texto), redirige.
                    if (!int.TryParse(Request.QueryString["Id"], out id))
                    {
                        Response.Redirect("Catalogo.aspx");
                        return;
                    }

                    // Nose si se usa el id 0, pero dejo comentado el chequeo por las dudas.
                    //if (id == 0)
                    //{
                    //    Response.Redirect("Productos.aspx");
                    //    return;
                    //}

                    // Validar existencia del producto

                    Negocio.ProductoNegocio productoNegocio = new Negocio.ProductoNegocio();

                    Producto producto = productoNegocio.buscarPorId(id);

                    if (producto != null)
                    {
                        // Mostrar detalles del producto
                    }
                    else
                    {
                        Response.Redirect("Catalogo.aspx");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error: " + ex.ToString());
                    Response.Redirect("Catalogo.aspx");
                }
            }
        }
    }
}