using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{
    public class Venta
    {
        public int Id { get; set; }
        public int IdCliente { get; set; }
        public DateTime Fecha { get; set; }
        public string NumeroFactura { get; set; }

        // Composiciones
        public Usuario Cliente { get; set; }
        public List<VentaDetalle> Detalles { get; set; }

        public decimal Total
        {
            get
            {
                return Detalles?.Sum(d => d.Subtotal) ?? 0;
            }
        }
    }
}