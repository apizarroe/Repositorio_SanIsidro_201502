using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ObrasPublicas.Models.ProyectoInversion
{
    public class CreateProyectoInversionModel
    {
        [Required(ErrorMessage = "El campo Nombre es obligatorio")]
        public String Nombre { get; set; }
        [Required(ErrorMessage = "El campo Descripción es obligatorio")]
        public String Descripcion { get; set; }
        [Required(ErrorMessage = "El campo Ubicación es obligatorio")]
        public String Ubicacion { get; set; }
        [Required(ErrorMessage = "Debe seleccionar una via de la lista")]
        public String NomVia { get; set; }
        [Required(ErrorMessage = "Debe seleccionar una via de la lista")]
        public int IdVia { get; set; }
        [Required(ErrorMessage = "Debe seleccionar el tipo de via")]
        public String TipoVia { get; set; }
        [Required(ErrorMessage = "El campo Número de Beneficiarios es obligatorio")]
        public int Beneficiarios { get; set; }
        [Required(ErrorMessage = "El campo Valor Referencial es obligatorio")]
        public Decimal ValorReferencial { get; set; }

        public String NomViaReal { get; set; }
    }
}