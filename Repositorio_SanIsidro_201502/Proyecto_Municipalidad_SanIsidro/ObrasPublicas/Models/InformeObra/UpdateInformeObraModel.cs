using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ObrasPublicas.Models.InformeObra
{
    public class UpdateInformeObraModel
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

        public int IdInforme { get; set; }
        public int IdEstado { get; set; }
        public String NomEstado { get; set; }
        public String Titulo { get; set; }
        [Required(ErrorMessage = "El campo Conclusión es obligatorio")]
        public String Conclusion { get; set; }
        [Required(ErrorMessage = "El campo Recomendación es obligatorio")]
        public String Recomendacion { get; set; }
        public String TipoInforme { get; set; }
    }
}