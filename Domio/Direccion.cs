using Dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{
    public class Direccion
    {
        public int Id { get; set; }
        public int IdUsuario { get; set; }
        public string Calle { get; set; }
        public string Ciudad { get; set; }
        public string CodigoPostal { get; set; }
        public string Provincia { get; set; }

    }

}
