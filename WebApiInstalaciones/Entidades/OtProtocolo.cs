using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    public class OtProtocolo
    {
        public int protocoloId { get; set; }
        public int formatoId { get; set; }
        public int tipoProtocoloId { get; set; }
        public string tipoTerreno { get; set; }
        public string estadoTerreno { get; set; }
        public string resistenciaSeca { get; set; }
        public string resistenciaHumeda { get; set; }
        public string fechaSeca { get; set; }
        public string aSeca1 { get; set; }
        public string aSeca2 { get; set; }
        public string aSeca3 { get; set; }
        public string fechaHumeda { get; set; }
        public string aHumeda1 { get; set; }
        public string aHumeda2 { get; set; }
        public string aHumeda3 { get; set; }


        public string rBt1 { get; set; }
        public string pBt1 { get; set; }
        public string rBt2 { get; set; }
        public string pBt2 { get; set; }
        public string rBt3 { get; set; }
        public string pBt3 { get; set; }
        public string rBt4 { get; set; }
        public string pBt4 { get; set; }
        public string rBtE { get; set; }
        public string pBtE { get; set; }
        public string rMt1 { get; set; }
        public string pMt1 { get; set; }
        public string rMt2 { get; set; }
        public string pMt2 { get; set; }
        public string rMt3 { get; set; }
        public string pMt3 { get; set; }
        public string rMt4 { get; set; }
        public string pMt4 { get; set; }
        public string rMtE { get; set; }
        public string pMtE { get; set; }


        public string observaciones { get; set; }
        public string proyecto { get; set; }
        public string tipoSistema { get; set; }
        public string grupoResis { get; set; }
        public string nivelResis { get; set; }
        public string btNpozos { get; set; }
        public string btPozoTratado { get; set; }
        public string btNdosis { get; set; }
        public string btCompuesto { get; set; }
        public string mtNpozos { get; set; }
        public string mtPozoTratado { get; set; }
        public string mtNdosis { get; set; }
        public string mtCompuesto { get; set; }
        public string fecha { get; set; }
    }
}
