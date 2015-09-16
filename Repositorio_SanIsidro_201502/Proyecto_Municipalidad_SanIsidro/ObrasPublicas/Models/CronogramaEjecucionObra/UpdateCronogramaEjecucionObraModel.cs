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
        public int IdProyecto { get; set; }
        public String NomProyecto { get; set; }
        public String UbicacionProyecto { get; set; }
        public String ValorRefProyecto { get; set; }

        public int IdCronograma { get; set; }
        public int IdExpediente { get; set; }

        [Required(ErrorMessage = "El campo Plazo de Ejecución es obligatorio")]
        public int PlazoEjecucion { get; set; }
    }
}