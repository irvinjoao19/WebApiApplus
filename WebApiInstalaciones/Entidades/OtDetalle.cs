using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    public class OtDetalle
    {
        public int formatoDetalleId { get; set; }
        public int formatoId { get; set; }
        public string codigoSoporte { get; set; }
        public string alim { get; set; }
        public string armado { get; set; }
        public string tipoMaterialId { get; set; }
        public string nombreTipoMaterialId { get; set; }
        public string tamanio { get; set; }
        public string funcionId { get; set; }
        public string redSDS { get; set; }
        public string redAP { get; set; }
        public string redAmbas { get; set; }
        public int cNumeroId { get; set; }
        public string seccCod { get; set; }
        public string seccCap { get; set; }
        public string seccFus { get; set; }
        public string tipoConductorId { get; set; }
        public string lvano { get; set; }
        public string conduSecc { get; set; }
        public string conduFases { get; set; }

        public string tipoAisladorId { get; set; }
        public string aislaMaterial { get; set; }
        public string aislaCantidad { get; set; }
        public string vientoViolin { get; set; }
        public string vientoSimple { get; set; }
        public string vientoCantidad { get; set; }
        public string pastoral { get; set; }
        public string observaciones { get; set; }
        public string codigoVia { get; set; }
        public string llave { get; set; }
        public string sistemas { get; set; }
        public string cajaDeriva { get; set; }


        public string retenidaV { get; set; }
        public string retenidaS { get; set; }
        public string pastotalC { get; set; }
        public string pastotalGF { get; set; }
        public string equipoTipo { get; set; }
        public string equipoModelo { get; set; }
        public string lampara { get; set; }
        public string direccion { get; set; }

        public string fecha { get; set; }
    }
}