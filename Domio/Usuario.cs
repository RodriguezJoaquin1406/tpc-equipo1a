using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domio
{
    internal class Usuario
    {
        public int Id { get; set; }
        public string NombreUsuario { get; set; }
        public string Contraseña { get; set; }
        public string Rol { get; set; } // "Administrador" o "Vendedor alguno mas?? cliente?? si puede hacer compra online.....veremos "

    }
}
