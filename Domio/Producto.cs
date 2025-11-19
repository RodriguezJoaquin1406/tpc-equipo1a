using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{
    public class Producto
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public Categoria Categoria { get; set; }
        public string Talle { get; set; } 
        public decimal PrecioBase { get; set; }
        public int StockActual { get; set; }
        public int StockMinimo { get; set; }
        public List<string> Imagenes { get; set; } = new List<string>();

        //public override string ToString()
        //{
        //    return this.Nombre;
        //}
    }
}
