using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{
    public class ItemCarrito
    {
        public int IdProducto { get; set; }
        public string nombre { get; set; }  
        public decimal precio { get; set; }
        public string imagen { get; set; }
        public int cantidad { get; set; }
        public decimal Subtotal
        {
            get { return precio * cantidad; }
        }
    }
}
