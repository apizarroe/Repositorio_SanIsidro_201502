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
        public String UbicacionProyecto { get; set; }
        public String ValorRefProyecto { get; set; }
        [DisplayFormat(DataFormatString = "{0:n2}")]
        public Decimal ValorRefExpediente { get; set; }

        [Required(ErrorMessage = "El campo Fecha de Entrega Programada es obligatorio")]
        public String FecEntregaProg { get; set; }
        [Required(ErrorMessage = "El campo Fecha de Entrega Efectiva es obligatorio")]
        public String FecEntregaEfec { get; set; }
        [Required(ErrorMessage = "El campo Observaciones es obligatorio")]
        public String Observaciones { get; set; }
        [Required(ErrorMessage = "El campo Tipo de Entrega es obligatorio")]
        public String TipoEntrega { get; set; }
        [Required(ErrorMessage = "El campo Proveedor es obligatorio")]
        public int IdProveedor { get; set; }
        [Required(ErrorMessage = "El campo Material es obligatorio")]
        public int IdMaterial { get; set; }
        [Required(ErrorMessage = "El campo Cantidad es obligatorio")]
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

            if (DateTime.TryParse(FecEntregaProg, out datFecTmp) && DateTime.TryParse(FecEntregaEfec, out datFecTmp))
            {
                if (Convert.ToDateTime(FecEntregaEfec) < Convert.ToDateTime(FecEntregaProg))
                {
                    lstValidations.Add(new ValidationResult("La fecha de entrega efectiva debe ser mayor a la fecha programada", new[] { "FecEntregaEfec" }));
                }
            }

            if (this.Cantidad <= 0)
            {
                lstValidations.Add(new ValidationResult("La cantidad debe ser mayor a 0", new[] { "Cantidad" }));
            }

            return lstValidations;
        }
    }
}