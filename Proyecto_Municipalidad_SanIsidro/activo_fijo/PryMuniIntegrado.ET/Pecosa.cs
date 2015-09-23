using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace PryMuniIntegrado.ET
{
    public class Pecosa
    {
        [Display(Name = "Cod.Pecosa")]
        public string Codigo { get; set; }
        [Display(Name = "Fecha de Registro")]
        public DateTime FechaRegistro { get; set; }
        [Display(Name = "Responsable")]
        public string Responsable { get; set; }
        [Display(Name = "Estado")]
        public EEstado Estado { get; set; }
        [Display(Name = "Resolución")]
        public string Resolucion { get; set; }
        [Display(Name = "Cod.Informe")]
        public string CodInforme { get; set; }
        [Display(Name = "Fecha de Registro de Informe")]
        public DateTime FechaRegistroInforme { get; set; }

    }
}
