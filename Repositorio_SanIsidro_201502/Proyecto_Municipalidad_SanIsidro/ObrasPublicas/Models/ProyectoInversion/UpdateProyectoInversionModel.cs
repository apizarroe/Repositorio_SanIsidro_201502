using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ObrasPublicas.Models.ProyectoInversion
{
    public class UpdateProyectoInversionModel : IValidatableObject
    {
        [Required]
        public int IdProyecto { get; set; }
        
        public String CodSNIP { get; set; }

        [Required(ErrorMessage = "Debe ingresar el nombre")]
        public String Nombre { get; set; }
        [Required(ErrorMessage = "Debe ingresar la descripción")]
        public String Descripcion { get; set; }
        [Required(ErrorMessage = "Debe ingresar la ubicación")]
        public String Ubicacion { get; set; }
        [Required(ErrorMessage = "Debe seleccionar una via de la lista")]
        public String NomVia { get; set; }
        [Required(ErrorMessage = "Debe seleccionar una via de la lista")]
        public int IdVia { get; set; }
        [Required(ErrorMessage = "Debe seleccionar el tipo de via")]
        public String TipoVia { get; set; }
        [Required(ErrorMessage = "Debe ingresar el número de beneficiarios")]
        public int Beneficiarios { get; set; }
        [Required(ErrorMessage = "Debe ingresar el valor referencial")]
        public Decimal ValorReferencial { get; set; }
        [Required]
        public String IdEstado { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (this.IdEstado == ObrasPublicas.Entities.ProyectoInversion.STR_ID_ESTADO_VIABLE && String.IsNullOrWhiteSpace(this.CodSNIP)) {
                yield return new ValidationResult("El Código SNIP es obligatorio para cambiar de estado a Viable.");
            }
            else if (this.IdEstado != ObrasPublicas.Entities.ProyectoInversion.STR_ID_ESTADO_VIABLE && !String.IsNullOrWhiteSpace(this.CodSNIP))
            {
                yield return new ValidationResult("Si va a ingresar el código SNIP, debe seleccionar el estado Viable.");
            }
        }

        public String SearchCodSNIP { get; set; }
        public String SearchNombre { get; set; }
        public String SearchUbicacion { get; set; }
        public String SearchIdEstado { get; set; }
    }
}