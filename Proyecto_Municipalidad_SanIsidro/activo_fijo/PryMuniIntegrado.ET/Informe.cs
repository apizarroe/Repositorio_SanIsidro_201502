using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace PryMuniIntegrado.ET
{
    public class Informe
    {
        [Display(Name="Código")]
        public string Codigo { get; set; }
        [Display(Name = "Estado")]
        public EEstado Estado { get; set; }
        [Display(Name = "Responsable")]
        public string Responsable { get; set; }
        [Display(Name = "Fecha de Registro")]
        public DateTime FechaRegistro { get; set; }
        [Display(Name = "Cód. Evaluacion")]
        public string CodEvaluacion { get; set; }
    }
}
