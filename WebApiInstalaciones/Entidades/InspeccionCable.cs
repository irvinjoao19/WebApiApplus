using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    public class InspeccionCable
    {
        public int inspeccionCableId { get; set; }
        public int inspeccionCampoId { get; set; }
        public decimal circuitoBT { get; set; }
        public int condIzqTipo1 { get; set; }
        public decimal condIzqAltura1_BT { get; set; }
        public int condIzqTipo2 { get; set; }
        public decimal condIzqAltura2_BT { get; set; }
        public decimal cableBT { get; set; }
        public int condDerTipo1 { get; set; }
        public decimal condDerAltura1_BT { get; set; }
        public int condDerTipo2 { get; set; }
        public decimal condDerAltura2_BT { get; set; }
        public decimal longbrazo { get; set; }
        public int condAdeTipo1 { get; set; }
        public decimal condAdeAltura1_BT { get; set; }
        public int condAdeTipo2 { get; set; }
        public decimal condAdeAltura2_BT { get; set; }
        public int condAtrasTipo1 { get; set; }
        public decimal condAtrasAltura1_BT { get; set; }
        public int condAtrasTipo2 { get; set; }
        public decimal condAtrasAltura2_BT { get; set; }
        public string comentarioCableBT { get; set; }
        public string tipoCable1 { get; set; }
        public decimal condIzqCant1 { get; set; }
        public decimal condIzqAltura1_Te { get; set; }
        public decimal condIzqCant2 { get; set; }
        public decimal condIzqAltura2_Te { get; set; }
        public string tipoCable2 { get; set; }
        public decimal condDerCant1 { get; set; }
        public decimal condDerAltura1_Te { get; set; }
        public decimal condDerCant2 { get; set; }
        public decimal condDerAltura2_Te { get; set; }
        public string cableAdss { get; set; }
        public decimal condAdeCant1 { get; set; }
        public decimal condAdeAltura1_Te { get; set; }
        public decimal condAdeCant2 { get; set; }
        public decimal condAdeAltura2_Te { get; set; }
        public string cableCoaxial { get; set; }
        public decimal condAtrasCant1 { get; set; }
        public decimal condAtrasAltura1_Te { get; set; }
        public decimal condAtrasCant2 { get; set; }
        public decimal condAtrasAltura2_Te { get; set; }
        public string otrosCables { get; set; }
        public decimal longCant1 { get; set; }
        public decimal longAltura1_Te { get; set; }
        public decimal longCant2 { get; set; }
        public decimal longAltura2_Te { get; set; }
        public string comentarioTele { get; set; }
        public int identity { get; set; }
    }
}