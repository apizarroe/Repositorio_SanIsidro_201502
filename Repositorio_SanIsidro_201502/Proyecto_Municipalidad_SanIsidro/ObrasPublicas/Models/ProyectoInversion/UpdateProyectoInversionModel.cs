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
        [Required]
        public String Nombre { get; set; }
        [Required]
        public String Descripcion { get; set; }
        [Required]
        public String Ubicacion { get; set; }
        [Required]
        public int IdVia { get; set; }
        [Required]
        public String TipoVia { get; set; }
        [Required]
        public int Beneficiarios { get; set; }
        [Required]
        public Decimal ValorReferencial { get; set; }
        [Required]
        public String IdEstado { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (this.IdEstado==ObrasPublicas.Entities.ProyectoInversion.STR_ID_ESTADO_VIABLE && String.IsNullOrWhiteSpace(this.CodSNIP))
                yield return new ValidationResult("El campo CodSNIP es obligatorio.");
        }

        public String SearchCodSNIP { get; set; }
        public String SearchNombre { get; set; }
        public String SearchUbicacion { get; set; }
        public String SearchIdEstado { get; set; }
    }
}