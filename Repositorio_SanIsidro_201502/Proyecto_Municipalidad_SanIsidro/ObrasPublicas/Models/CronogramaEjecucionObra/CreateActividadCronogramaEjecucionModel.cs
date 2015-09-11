using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ObrasPublicas.Models.CronogramaEjecucionObra
{
    public class CreateActividadCronogramaEjecucionModel
    {
        public int IdCronograma { get; set; }
        public int IdExpediente { get; set; }
        public int IdProyecto { get; set; }
        public String NomProyecto { get; set; }
        public int PlazoEjecucion { get; set; }

        [Required]
        public String NomAct { get; set; }
        [Required]
        public String FechaIniProgAct { get; set; }
        [Required]
        public String FechaFinProgAct { get; set; }
        [Required]
        public String FechaIniEjecAct { get; set; }
        [Required]
        public String FechaFinEjecAct { get; set; }
        [Required]
        public decimal CostoAct { get; set; }
        [Required]
        public int CantidadRRHHAct { get; set; }
        [Required]
        public String ResponsableActTipo { get; set; }
        public String IdResponsablePersonaNatural { get; set; }
        public String IdResponsablePersonaJuridica { get; set; }

        public String IdAreaResponsable { get; set; }
        //[Required]
        //public String ResponsableActNom { get; set; }
        //[Required]
        //public String ResponsableActApePat { get; set; }
        //[Required]
        //public String ResponsableActRazonSocial { get; set; }
    }
}