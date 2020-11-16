using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    public class Estado
    {
        public int estadoId { get; set; }
        public string abreviatura { get; set; }
        public string descripcion { get; set; }
        public string tipoProceso { get; set; }
        public string descripcionTP { get; set; }
        public int moduloId { get; set; }
        public int orden { get; set; }
        public string backColor { get; set; }
        public string foreColor { get; set; }
        public int activo { get; set; }
    }
}
