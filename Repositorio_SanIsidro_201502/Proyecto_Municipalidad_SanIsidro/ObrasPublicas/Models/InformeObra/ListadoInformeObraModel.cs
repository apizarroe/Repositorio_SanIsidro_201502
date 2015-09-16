using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ObrasPublicas.Models.InformeObra
{
    public class ListadoInformeObraModel
    {
        public int IdProyecto { get; set; }
        public int IdExpediente { get; set; }
        public String NomProyecto { get; set; }
        public String UbicacionProyecto { get; set; }
        public String ValorRefProyecto { get; set; }
        public String NomEjecutor { get; set; }
        public String NomSupervisor { get; set; }
    }
}