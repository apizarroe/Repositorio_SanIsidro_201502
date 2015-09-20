using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ObrasPublicas.Models.InformeObra
{
    public class CreateInformeObraModel
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

        [Required(ErrorMessage = "El campo Titulo es obligatorio")]
        public String Titulo { get; set; }
        [Required(ErrorMessage = "El campo Conclusión es obligatorio")]
        public String Conclusion { get; set; }
        [Required(ErrorMessage = "El campo Recomendación es obligatorio")]
        public String Recomendacion { get; set; }
        [Required(ErrorMessage = "El campo Tipo de Informe es obligatorio")]
        public String TipoInforme { get; set; }
    }
}