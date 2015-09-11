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
    }
}