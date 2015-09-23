using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace PryMuniIntegrado.ET
{
    public class Usuario
    {        
        [Required]
        [Display(Name = "Usuario")]
        public string NombreUsuario { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Contraseña")]
        public string Contraseña { get; set; }
        public string[] Roles
        {
            get;
            set;
        }
    }
}
