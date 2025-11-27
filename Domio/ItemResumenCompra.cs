using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio{
    public class ItemResumenCompra
    {
        public string Nombre { get; set; }
        public string Talle { get; set; }
        public int Cantidad { get; set; }
        public decimal Subtotal { get; set; }
    }
}
