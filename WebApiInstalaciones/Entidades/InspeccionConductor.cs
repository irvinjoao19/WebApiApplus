using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    public class InspeccionConductor
    {
        public int inspeccionConductorId { get; set; }
        public int inspeccionCampoId { get; set; }
        public int id_ternaMT { get; set; }
        public int id_nCond { get; set; }
        public int id_TipoArmado { get; set; }
        public decimal seccionIzq { get; set; }
        public decimal seccionDer { get; set; }
        public decimal seccionAdel { get; set; }
        public decimal seccionAtras { get; set; }
        public decimal vanoIzq { get; set; }
        public decimal vanoDer { get; set; }
        public decimal vanoAdel { get; set; }
        public decimal vanoAtras { get; set; }
        public decimal alturaIzq { get; set; }
        public decimal alturaDer { get; set; }
        public decimal alturaAdel { get; set; }
        public decimal alturaAtras { get; set; }
        public decimal distanciaIzq { get; set; }
        public decimal distanciaDer { get; set; }
        public decimal distanciaAdel { get; set; }
        public decimal distanciaAtras { get; set; }
        public decimal retIzq_1 { get; set; }
        public decimal retIzq_2 { get; set; }
        public decimal retIzq_3 { get; set; }
        public int retIzq_Estado { get; set; }
        public decimal retDer_1 { get; set; }
        public decimal retDer_2 { get; set; }
        public decimal retDer_3 { get; set; }
        public int retDer_Estado { get; set; }
        public decimal retAtras_1 { get; set; }
        public decimal retAtras_2 { get; set; }
        public decimal retAtras_3 { get; set; }
        public int retAtras_Estado { get; set; }
        public decimal retAde_1 { get; set; }
        public decimal retAde_2 { get; set; }
        public decimal retAde_3 { get; set; }
        public int retAde_Estado { get; set; }
        public string comentario { get; set; }
        public int identity { get; set; }
    }
}