using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{
    public class VentaDetalle
    {
        public int Id { get; set; }
        public int IdVenta { get; set; }
        public int IdProducto { get; set; }
        public int Cantidad { get; set; }
        public decimal PrecioUnitario { get; set; }

        // Composiciones
        public Producto Producto { get; set; }

        public decimal Subtotal
        {
            get { return PrecioUnitario * Cantidad; }
        }
    }
}