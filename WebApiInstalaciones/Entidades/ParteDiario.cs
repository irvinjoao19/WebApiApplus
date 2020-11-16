using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    public class ParteDiario
    {

        public int parteDiarioId { get; set; }
        public string fechaSalidaProgramada { get; set; }
        public string horaInicio { get; set; }
        public string horaFin { get; set; }
        public string sector { get; set; }
        public int proyectistaId { get; set; }
        public int trabajoProgramadoId { get; set; }
        public int supervisorId { get; set; }
        public int distritoId { get; set; }
        public int otId { get; set; }
        public string nroSed { get; set; }
        public string fechaSalida { get; set; }
        public int cuadrillaId { get; set; }
        public int placaId { get; set; }
        public string observaciones { get; set; }
        public int estado { get; set; }
        public int usuarioId { get; set; }
        public int identity { get; set; }
    }
}
