

namespace Dominio.Core.Entities.GSM
{
    using System;
    using System.Collections.Generic;
    
    public   class SM_CATEGORIA_SERVICIO
    {
        public SM_CATEGORIA_SERVICIO()
        { 
        }
    
        public int CodigoCategoriaServicio { get; set; }
        public string nombreCategoria { get; set; }
        public string descripcionCateogira { get; set; }
     
    }
}
