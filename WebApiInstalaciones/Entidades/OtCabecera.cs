using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    public class OtCabecera
    {
        public int formatoId { get; set; }
        public int tipoFormatoId { get; set; }
        public string nombreTipoFormato { get; set; }
        public int seccionId { get; set; }
        public string seccion { get; set; }
        public int otId { get; set; }
        public string nroOt { get; set; }
        public string nombrecliente { get; set; }
        public string alimentador { get; set; }
        public string modulo { get; set; }
        public string sed { get; set; }
        public string ubicacion { get; set; }
        public string distrito { get; set; }
        public string ubicacionSed { get; set; }
        public string coordenadaX { get; set; }
        public string coordenadaY { get; set; }
        public string fechaRegistro { get; set; }
        public int estadoId { get; set; }
        public string nombreEstado { get; set; }
		
		public string convencional { get; set; }
		public string compacta { get; set; }
		public string aerea { get; set; }
		public string pmi { get; set; }
		public string aNivel { get; set; }
		public string pedestal { get; set; }
		public string monoposte { get; set; }
		public string reCloser { get; set; }
		public string subTerranea { get; set; }
		public string boveda { get; set; }
		public string biposte { get; set; }
		public string sbc { get; set; }
		public string soporte { get; set; }
		public string setProtocolo  { get; set; }
		public string cuadrilla  { get; set; }
		public string lamina  { get; set; }
		public string letra  { get; set; }		  
    
		public int active { get; set; }
        public int identity { get; set; }
        public int usuario { get; set; }         
        public int cadistaId { get; set; }                 
        public string dibujar { get; set; }         
        public int supervisorId { get; set; }         
        public List<OtDetalle> details { get; set; }
        public List<OtEquipo> equipos { get; set; }
        public List<OtHoja123> hojas123 { get; set; }
        public List<OtHoja4> hojas4 { get; set; }
        public List<OtHoja56> hojas567 { get; set; }
        public List<OtProtocolo> protocolos { get; set; }
        public List<OtPhoto> photos { get; set; }
    }
}
