using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ObrasPublicas.Models.CronogramaEjecucionObra
{
    public class ListadoCronogramaEjecucionObraModel
    {
        public int IdCronograma { get; set; }
        public int IdExpediente { get; set; }
        public int IdProyecto { get; set; }
        public String NomProyecto { get; set; }
    }
}