 

namespace Entidades.GSM
{
    using System;
    using System.Collections.Generic;
    
    public  class SM_SERVICIO
    {
        public SM_SERVICIO()
        { 
        }
    
        public int CodigoServicio { get; set; }
        public string NombreServicio { get; set; }
        public string DescripcionServicio { get; set; }
        public System.DateTime FechaInicio { get; set; }
        public System.DateTime FechaFin { get; set; }
        public int coVia { get; set; }
        public string LugarServicio { get; set; }
        public int CodigoTipoServicio { get; set; }
        public System.DateTime FechaCreacion { get; set; }
        public System.DateTime FechaActualizacion { get; set; }
        public int coUsuario { get; set; }
        public int CodigoCategoriaServicio { get; set; }
        public int CodigoExpedienteServicio { get; set; }
        public int CodigoPersonaResponsable { get; set; }
     
    }
}
