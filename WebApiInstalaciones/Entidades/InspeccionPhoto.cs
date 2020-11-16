using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    public class InspeccionPhoto
    {
        public int inspeccionPhotoId { get; set; }
        public int inspeccionCampoId { get; set; }
        public string fotoUrl { get; set; }        
        public int estado { get; set; }
        public int usuarioId { get; set; }
        public string fecha { get; set; } 
    }
}
