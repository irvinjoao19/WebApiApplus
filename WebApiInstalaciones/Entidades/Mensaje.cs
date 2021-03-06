﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    public class Mensaje
    {
        public int codigoBase { get; set; }
        public int codigoRetorno { get; set; }
        public string mensaje { get; set; }        
        public int codigoRetornoCable { get; set; }        
        public int codigoRetornoEquipo { get; set; }        
        public int codigoRetornoConductor { get; set; }
        public List<MensajeDetalle> detalle { get; set; }
    }
}
