using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace PryMuniIntegrado.ET
{
    public class Empleado
    {
        [Display(Name = "Codigo Empleado")]
        public string Codigo { get; set; }

        public Empleado()
        {
            this.Codigo = string.Empty;
        }
    }
}
