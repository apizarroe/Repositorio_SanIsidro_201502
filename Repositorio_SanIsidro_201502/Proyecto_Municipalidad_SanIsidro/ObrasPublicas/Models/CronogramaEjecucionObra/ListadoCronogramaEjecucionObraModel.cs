using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ObrasPublicas.Models.CronogramaEjecucionObra
{
    public class ListadoCronogramaEjecucionObraModel
    {
        public int IdCronograma { get; set; }
        public int IdExpediente { get; set; }
        public int IdProyecto { get; set; }
        public String NomProyecto { get; set; }
        public String UbicacionProyecto { get; set; }
        public String ValorRefProyecto { get; set; }
        [DisplayFormat(DataFormatString = "{0:n2}")]
        public Decimal ValorRefExpediente { get; set; }
        [DisplayFormat(DataFormatString = "{0:n2}")]
        public Decimal CostoProyecto { get; set; }
        public int PlazoEjecucion { get; set; }
    }
}