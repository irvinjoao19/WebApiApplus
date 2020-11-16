using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    public class OtPhoto
    {
        public int formatoFotoId { get; set; }
        public int formatoId { get; set; }
        public string fotoUrl { get; set; }
        public string observacion { get; set; }
        public int estado { get; set; }
        public int usuarioId { get; set; }        
        public string fecha { get; set; }
    }
}
