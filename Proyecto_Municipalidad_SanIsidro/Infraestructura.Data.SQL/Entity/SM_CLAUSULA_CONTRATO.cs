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
    
    public partial class SM_CLAUSULA_CONTRATO
    {
        public SM_CLAUSULA_CONTRATO()
        {
            this.SM_OBSERVACION = new HashSet<SM_OBSERVACION>();
        }
    
        public int CodigoClausulaContrato { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public Nullable<System.DateTime> FechaCreacion { get; set; }
        public Nullable<System.DateTime> FechaActualizacion { get; set; }
        public int CodigoContratoServicio { get; set; }
    
        public virtual SM_CONTRATO_SERVICIO SM_CONTRATO_SERVICIO { get; set; }
        public virtual ICollection<SM_OBSERVACION> SM_OBSERVACION { get; set; }
    }
}
