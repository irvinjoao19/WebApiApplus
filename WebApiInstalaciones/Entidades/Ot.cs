using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    public class Ot
    {
        public int otId { get; set; }
        public string numeroOt { get; set; }
        public string nroOt { get; set; }
        public string descripcion { get; set; }
        public string nombre { get; set; }
        public string direccion { get; set; }
        public string distrito { get; set; }
        public string fechaLegal { get; set; }
        public string fechaAsignacion { get; set; }
        public int proyectistaId { get; set; }
        public string fechaRecepcion { get; set; }
        public string estado { get; set; }
        public int diasVencimiento { get; set; }
        public int estadoId { get; set; }

        public int active { get; set; }
        public string comentario { get; set; }
        public int motivoId { get; set; }
        public int coordinadorId { get; set; }
        public string nombreCoordinador { get; set; }

        public int supervisorId { get; set; }
        public string nombreSupervisor { get; set; }

        public List<OtCabecera> otCabecera { get; set; }
    }
}
