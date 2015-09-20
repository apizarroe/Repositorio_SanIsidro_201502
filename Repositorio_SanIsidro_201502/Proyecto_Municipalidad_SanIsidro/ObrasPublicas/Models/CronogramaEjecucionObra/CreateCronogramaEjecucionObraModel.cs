using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ObrasPublicas.Models.CronogramaEjecucionObra
{
    public class CreateCronogramaEjecucionObraModel : IValidatableObject
    {
        public int IdProyecto { get; set; }
        public String NomProyecto { get; set; }
        public String UbicacionProyecto { get; set; }
        [DisplayFormat(DataFormatString = "{0:n0}")]
        public Decimal ValorRefProyecto { get; set; }
        [DisplayFormat(DataFormatString = "{0:n0}")]
        public Decimal ValorRefExpediente { get; set; }
        
        public int IdExpediente { get; set; }

        [Required(ErrorMessage = "El campo Plazo de Ejecución es obligatorio")]
        public int PlazoEjecucion { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext context)
        {
            List<ValidationResult> lstValidations = new List<ValidationResult>();
            if (this.PlazoEjecucion <= 0)
            {
                lstValidations.Add(new ValidationResult("El plazo de ejecución debe ser mayor a cero", new[] { "PlazoEjecucion" }));
            }
            return lstValidations;
        }
    }
}