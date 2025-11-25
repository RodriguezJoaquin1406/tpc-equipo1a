using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio     
{
    public class Usuario
    {
        public int Id { get; set; }
        public string NombreUsuario { get; set; }
        public string Contrasena { get; set; }
        public string Rol { get; set; }

        // Datos personales-ya no existe mas cliente
        public string Nombre { get; set; }
        public string Email { get; set; }
        public string Telefono { get; set; }

        public bool EsAdmin
        {
            get
            {
                return Rol != null && Rol.ToLower() == "administrador";
            }
        }
    }
}
