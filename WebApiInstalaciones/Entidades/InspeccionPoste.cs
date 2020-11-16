using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    public class InspeccionPoste
    {
        public int inspeccionCampoId { get; set; }
        public int inspeccionId { get; set; }
        public int itemGeneral { get; set; }
        public string codPoste { get; set; }
        public string distritoCod { get; set; }
        public string direccion { get; set; }
        public string nlote { get; set; }
        public string tipoCablePre { get; set; }
        public int cantCable { get; set; }
        public int estadoId { get; set; }
        public string nombreEstado { get; set; }
        public string latitud { get; set; }
        public string longitud { get; set; }


        // nuevo para los formularios
        public string utmX { get; set; }
        public string utmY { get; set; }
        public string nroPoste { get; set; }
        public int id_MaterialSoporte { get; set; }
        public int id_TipoPoste { get; set; }
        public int id_CargaTrabajo { get; set; }
        public int id_TipoArmado { get; set; }
        public decimal alturaPoste { get; set; }
        public decimal alturaLibrePoste { get; set; }
        public int id_estadoMensula { get; set; }
        public int id_NivelCriticidad { get; set; }
        public string comentariosGenerales { get; set; }
        public decimal vanoMT_AngIzq { get; set; }
        public decimal vanoMT_AngDer { get; set; }
        public decimal vanoMT_AngAde { get; set; }
        public decimal vanoMT_AngAtra { get; set; }
        public decimal vanoMT_VanIzq { get; set; }
        public decimal vanoMT_VanDer { get; set; }
        public decimal vanoMT_VanAde { get; set; }
        public decimal vanoMT_VanAtra { get; set; }
        public string comentariosVanoMt { get; set; }
        public decimal vanoBT_AngIzq { get; set; }
        public decimal vanoBT_AngDer { get; set; }
        public decimal vanoBT_AngAde { get; set; }
        public decimal vanoBT_AngAtra { get; set; }
        public decimal vanoBT_VanIzq { get; set; }
        public decimal vanoBT_VanDer { get; set; }
        public decimal vanoBT_VanAde { get; set; }
        public decimal vanoBT_VanAtra { get; set; }
        public decimal AngRetenidaBt { get; set; }
        public string comentariosVanoBt { get; set; }
        public int id_reteBT_tipoR1 { get; set; }
        public decimal reteBT_AlturaR1 { get; set; }
        public string reteBT_DirR1 { get; set; }
        public int id_reteBT_Estado1 { get; set; }
        public int id_reteBT_tipoR2 { get; set; }
        public decimal reteBT_AlturaR2 { get; set; }
        public string reteBT_DirR2 { get; set; }
        public int id_reteBT_Estado2 { get; set; }
        public string comentarios_ReteBT { get; set; }
        public string infoAdd_RolloReserva { get; set; }
        public string infoAdd_Fotos { get; set; }
        public int id_infoAdd_Consecion { get; set; }
        public string infoAdd_Niveltension { get; set; }
        public string infoCnew_CableNuevo { get; set; }
        public decimal infoCnew_vanoIzq { get; set; }
        public decimal infoCnew_vanoDer { get; set; }
        public decimal infoCnew_vanoAde { get; set; }
        public decimal infoCnew_vanoAtra { get; set; }
        public decimal infoCnew_alturaInstala { get; set; }
        public int obsC_Cumplimiento { get; set; }
        public int obsC_PosteInclinado { get; set; }
        public int obsC_PosteSubida { get; set; }
        public int obsC_PosteSaturado { get; set; }
        public string comentarios_obsC { get; set; }
        public int resFac_Factible { get; set; }
        public int id_resFac_ObsPrincipal { get; set; }
        public string Obs_generales { get; set; }
        public int usuarioId { get; set; }

        public InspeccionCable cable { get; set; }
        public InspeccionConductor conductor { get; set; }
        public InspeccionEquipo equipo { get; set; }
        public List<InspeccionPhoto> photos { get; set; }
    }
}
