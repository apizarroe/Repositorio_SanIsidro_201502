 
 
namespace Entidades.GSM
{
    using System;
    using System.Collections.Generic;

    public   class MA_PERSONA
    {
        public MA_PERSONA()
        {
        }

        public int idPersona { get; set; }
        public int idDistrito { get; set; }
        public int idTipoDocIdentidad { get; set; }
        public string Direccion { get; set; }
        public string Telefono { get; set; }
        public string Correo { get; set; }
        public int idRepresentanteLegal { get; set; }
        public string RUC { get; set; }


    }
}
