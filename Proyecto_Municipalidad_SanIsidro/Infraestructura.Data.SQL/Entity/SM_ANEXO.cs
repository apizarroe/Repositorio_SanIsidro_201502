//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Infraestructura.Data.SQL.Entity
{
    using System;
    using System.Collections.Generic;
    
    public partial class SM_ANEXO
    {
        public SM_ANEXO()
        {
            this.SM_INFORME_TECNICO = new HashSet<SM_INFORME_TECNICO>();
        }
    
        public int CodigoAnexo { get; set; }
        public string NombreDocumento { get; set; }
        public string Descripcion { get; set; }
        public string Contenido { get; set; }
        public int CodigoInformeTecnico { get; set; }
    
        public virtual ICollection<SM_INFORME_TECNICO> SM_INFORME_TECNICO { get; set; }
    }
}
