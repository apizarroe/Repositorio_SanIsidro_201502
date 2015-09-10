
namespace Dominio.Core.Entities.GSM
{
    using System;
    using System.Collections.Generic;
    
    public   class MA_PERSONANATURAL
    {
        public int idPersonaNatural { get; set; }
        public   int  idPersona { get; set; }
        public string NroDocIdentidad { get; set; }
        public string Nombres { get; set; }
        public string ApellidoPaterno { get; set; }
        public string ApellidoMaterno { get; set; }
     
    }
}
