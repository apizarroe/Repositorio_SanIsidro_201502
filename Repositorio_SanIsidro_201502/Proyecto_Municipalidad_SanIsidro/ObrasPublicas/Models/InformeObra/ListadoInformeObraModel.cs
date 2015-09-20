using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ObrasPublicas.Models.InformeObra
{
    public class ListadoInformeObraModel
    {
        public int IdProyecto { get; set; }
        public int IdExpediente { get; set; }
        public String NomProyecto { get; set; }
        public String UbicacionProyecto { get; set; }
        public String ValorRefProyecto { get; set; }
        [DisplayFormat(DataFormatString = "{0:n0}")]
        public Decimal CostoProyecto { get; set; }
        public String NomEjecutor { get; set; }
        public String NomSupervisor { get; set; }
        [DisplayFormat(DataFormatString = "{0:n2}")]
        public Decimal ValorRefExpediente { get; set; }
    }
}