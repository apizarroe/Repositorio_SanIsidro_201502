using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ObrasPublicas.Models.EntregaMaterialOP
{
    public class UpdateEntregaMaterialOP : IValidatableObject
    {
        public int IdProyecto { get; set; }
        public String NomProyecto { get; set; }
        public int IdEntrega { get; set; }
        [Required]
        public String FecEntregaProg { get; set; }
        [Required]
        public String FecEntregaEfec { get; set; }
        [Required]
        public String Observaciones { get; set; }
        [Required]
        public String TipoEntrega { get; set; }
        [Required]
        public int IdProveedor { get; set; }
        [Required]
        public int IdMaterial { get; set; }
        [Required]
        public int Cantidad { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext context)
        {
            List<ValidationResult> lstValidations = new List<ValidationResult>();

            DateTime datFecTmp;
            if (!DateTime.TryParse(FecEntregaProg, out datFecTmp))
            {
                lstValidations.Add(new ValidationResult("La fecha de entrega programada es incorrecta", new[] { "FecEntregaProg" }));
            }
            else if (Convert.ToDateTime(FecEntregaProg) < DateTime.Now)
            {
                lstValidations.Add(new ValidationResult("La fecha de entrega programada debe ser mayor a la fecha actual", new[] { "FecEntregaProg" }));
            }
            if (!DateTime.TryParse(FecEntregaEfec, out datFecTmp))
            {
                lstValidations.Add(new ValidationResult("La fecha de entrega efectiva es incorrecta", new[] { "FecEntregaEfec" }));
            }
            else if (Convert.ToDateTime(FecEntregaEfec) < DateTime.Now)
            {
                lstValidations.Add(new ValidationResult("La fecha de entrega efectiva debe ser mayor a la fecha actual", new[] { "FecEntregaEfec" }));
            }
            return lstValidations;
        }
    }
}