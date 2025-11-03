using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{
    public class Pedido
    {
        public int Id { get; set; }
        public int IdUsuario { get; set; }
        public DateTime Fecha { get; set; }
        public string Estado { get; set; } // Pendiente, Pagado, Enviado, Cancelado
        public int IdDireccion { get; set; }
        public int IdMetodoPago { get; set; }
        public decimal Total { get; set; }

        // composiciones nuevas (para ecomecerce)
        public Usuario Usuario { get; set; }
        public Direccion Direccion { get; set; }
        public MetodoPago MetodoPago { get; set; }
    }
}
