using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    public class InspeccionEquipo
    {

        public int inspeccionEquipoId { get; set; }
        public int inspeccionCampoId { get; set; }
        public string electrico1 { get; set; }
        public decimal cantidad1 { get; set; }
        public string telecomunica1 { get; set; }
        public decimal telecantidad1 { get; set; }
        public string electrico2 { get; set; }
        public decimal cantidad2 { get; set; }
        public string telecomunica2 { get; set; }
        public decimal telecantidad2 { get; set; }
        public string electrico3 { get; set; }
        public decimal cantidad3 { get; set; }
        public string telecomunica3 { get; set; }
        public decimal telecantidad3 { get; set; }
        public string electrico4 { get; set; }
        public decimal cantidad4 { get; set; }
        public string telecomunica4 { get; set; }
        public decimal telecantidad4 { get; set; }
        public string comentario { get; set; }
        public int identity { get; set; }
    }
}
