using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ObrasPublicas.Models.ExpedienteTecnicoOP
{
    public class DocumentoExpTecOPModel
    {
        [Required]
        public String NroDocumento { get; set; }
        [Required]
        public DateTime FechaEmision { get; set; }
        [Required]
        public String Descripcion { get; set; }
        [Required]
        public String TipoDocmento { get; set; }
    }
}