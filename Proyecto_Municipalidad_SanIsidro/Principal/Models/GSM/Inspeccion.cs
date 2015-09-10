using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Models.GSM
{
    public class Inspeccion
    {
        public Servicio oServicio{get;set;}

        public int IdRegistro { get; set; } 

        public String fechaProgramada { get; set; }
        public String horaInicio{get;set;}
        public String horaFin{get;set;}
        public String direccion{get;set;}
        public String personaAsignada { get; set; }
        public int IdPersona { get; set; }

        public bool nuevo { get; set; }
    }
}