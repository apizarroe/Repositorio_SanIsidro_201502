using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ObrasPublicas.Models.CronogramaEjecucionObra
{
    public class UpdateCronogramaEjecucionObraModel
    {
        public int IdCronograma { get; set; }
        public int IdExpediente { get; set; }
        public int IdProyecto { get; set; }
        public String NomProyecto { get; set; }
        [Required]
        public int PlazoEjecucion { get; set; }
    }
}