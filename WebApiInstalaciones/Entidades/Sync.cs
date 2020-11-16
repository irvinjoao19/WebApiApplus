using System.Collections.Generic;

namespace Entidades
{
    public class Sync
    {
        public List<Formato> formatos { get; set; }
        public List<Grupo> grupos { get; set; }
        public List<Ot> ots { get; set; }
        public List<Sed> seds { get; set; }
        public List<Estado> estadoTrabajo { get; set; }
        public List<Estado> estadoPoste { get; set; }
        public List<Cadista> cadistas { get; set; }
        public List<PuestoTierra> puestos { get; set; }
        public List<Supervisor> supervisores { get; set; }
        public List<InspeccionPoste> otsPostes { get; set; }
    }
}
