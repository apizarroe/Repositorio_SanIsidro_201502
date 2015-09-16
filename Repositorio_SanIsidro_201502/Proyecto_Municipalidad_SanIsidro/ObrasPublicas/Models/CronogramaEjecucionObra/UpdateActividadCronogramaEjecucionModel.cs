using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ObrasPublicas.Models.CronogramaEjecucionObra
{
    public class UpdateActividadCronogramaEjecucionModel : IValidatableObject
    {
        public int IdProyecto { get; set; }
        public String NomProyecto { get; set; }
        public int IdExpediente { get; set; }
        public int IdCronograma { get; set; }
        public int PlazoEjecucion { get; set; }
        public String UbicacionProyecto { get; set; }
        public String ValorRefProyecto { get; set; }

        public int IdActividad { get; set; }

        [Required(ErrorMessage = "El campo Nombre es obligatorio")]
        public String NomAct { get; set; }

        [Required(ErrorMessage = "El campo Fecha de Inicio Programada es obligatorio")]
        public String FechaIniProgAct { get; set; }

        [Required(ErrorMessage = "El campo Fecha Fin Programada es obligatorio")]
        public String FechaFinProgAct { get; set; }

        [Required(ErrorMessage = "El campo Fecha de Inicio de Ejecución es obligatorio")]
        public String FechaIniEjecAct { get; set; }

        [Required(ErrorMessage = "El campo Fecha Fin de Ejecución es obligatorio")]
        public String FechaFinEjecAct { get; set; }

        [Required(ErrorMessage = "El campo Costo es obligatorio")]
        public decimal CostoAct { get; set; }

        [Required(ErrorMessage = "El campo Cantidad es obligatorio")]
        public int CantidadRRHHAct { get; set; }

        [Required(ErrorMessage = "El campo Responsable es obligatorio")]
        public String ResponsableActTipo { get; set; }

        public String IdResponsablePersonaNatural { get; set; }
        public String IdResponsablePersonaJuridica { get; set; }

        public String IdAreaResponsable { get; set; }
        //[Required]
        //public String ResponsableActNom { get; set; }
        //[Required]
        //public String ResponsableActApePat { get; set; }
        //[Required]
        //public String ResponsableActRazonSocial { get; set; }
        
        public IEnumerable<ValidationResult> Validate(ValidationContext context)
        {
            List<ValidationResult> lstValidations = new List<ValidationResult>();

            DateTime datFecTmp;

            if (!DateTime.TryParse(this.FechaIniProgAct, out datFecTmp))
            {
                lstValidations.Add(new ValidationResult("El campo Fecha de inicio programada es incorrecta", new[] { "FechaIniProgAct" }));
            }
            else if (Convert.ToDateTime(this.FechaIniProgAct) < DateTime.Now)
            {
                lstValidations.Add(new ValidationResult("El campo Fecha de inicio programada debe ser mayor o igual a la fecha actual", new[] { "FechaIniProgAct" }));
            }

            if (!DateTime.TryParse(this.FechaFinProgAct, out datFecTmp))
            {
                lstValidations.Add(new ValidationResult("El campo Fecha fin programada es incorrecta", new[] { "FechaFinProgAct" }));
            }
            else if (Convert.ToDateTime(this.FechaFinProgAct) < DateTime.Now)
            {
                lstValidations.Add(new ValidationResult("El campo Fecha fin programada debe ser mayor o igual a la fecha actual", new[] { "FechaFinProgAct" }));
            }

            if (DateTime.TryParse(this.FechaIniProgAct, out datFecTmp) && DateTime.TryParse(this.FechaFinProgAct, out datFecTmp))
            {
                if (Convert.ToDateTime(this.FechaIniProgAct) > Convert.ToDateTime(this.FechaFinProgAct))
                {
                    lstValidations.Add(new ValidationResult("La fecha fin programada debe ser mayor a la fecha de inicio", new[] { "FechaFinProgAct" }));
                }
            }

            if (!DateTime.TryParse(this.FechaIniEjecAct, out datFecTmp))
            {
                lstValidations.Add(new ValidationResult("El campo Fecha de inicio de ejecución es incorrecta", new[] { "FechaIniEjecAct" }));
            }
            else if (Convert.ToDateTime(this.FechaIniEjecAct) < DateTime.Now)
            {
                lstValidations.Add(new ValidationResult("El campo Fecha de inicio de ejecución debe ser mayor o igual a la fecha actual", new[] { "FechaIniEjecAct" }));
            }

            if (!DateTime.TryParse(this.FechaFinEjecAct, out datFecTmp))
            {
                lstValidations.Add(new ValidationResult("El campo Fecha fin de ejecución es incorrecta", new[] { "FechaFinEjecAct" }));
            }
            else if (Convert.ToDateTime(this.FechaFinEjecAct) < DateTime.Now)
            {
                lstValidations.Add(new ValidationResult("El campo Fecha fin de ejecución debe ser mayor o igual a la fecha actual", new[] { "FechaFinEjecAct" }));
            }

            if (DateTime.TryParse(this.FechaIniEjecAct, out datFecTmp) && DateTime.TryParse(this.FechaFinEjecAct, out datFecTmp))
            {
                if (Convert.ToDateTime(this.FechaIniEjecAct) > Convert.ToDateTime(this.FechaFinEjecAct))
                {
                    lstValidations.Add(new ValidationResult("La fecha fin de ejecución debe ser mayor a la fecha de inicio", new[] { "FechaFinEjecAct" }));
                }
            }

            if (this.CostoAct <= 0) {
                lstValidations.Add(new ValidationResult("El costo debe ser mayor a 0", new[] { "CostoAct" }));
            }
            if (this.CantidadRRHHAct <= 0)
            {
                lstValidations.Add(new ValidationResult("La cantidad de recursos debe ser mayor a 0", new[] { "CantidadRRHHAct" }));
            }

            if (this.ResponsableActTipo == "P")
            {
                if (String.IsNullOrWhiteSpace(this.IdResponsablePersonaNatural))
                {
                    lstValidations.Add(new ValidationResult("Debe seleccionar un responsable", new[] { "IdResponsablePersonaNatural" }));
                }
            }
            else
            {
                if (String.IsNullOrWhiteSpace(this.IdResponsablePersonaJuridica))
                {
                    lstValidations.Add(new ValidationResult("Debe seleccionar un responsable", new[] { "IdResponsablePersonaJuridica" }));
                }
            }

            return lstValidations;
        }
    }
}