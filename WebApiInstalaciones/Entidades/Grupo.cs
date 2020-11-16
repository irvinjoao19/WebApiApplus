using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    public class Grupo
    {
        public int grupoId { get; set; }
        public string nombre { get; set; }
        public int detalleId { get; set; }
        public string codigo { get; set; }
        public string descripcion { get; set; }
        public string vencimiento { get; set; }
        public string valor { get; set; }
        public int estado { get; set; } 
    }
}
