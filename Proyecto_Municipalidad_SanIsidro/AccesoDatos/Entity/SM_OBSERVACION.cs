//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace AccesoDatos.Entity
{
    using System;
    using System.Collections.Generic;
    
    public partial class SM_OBSERVACION
    {
        public int CodigoObservacion { get; set; }
        public Nullable<System.DateTime> FechaIncidencia { get; set; }
        public string DescripcionIncidencia { get; set; }
        public Nullable<System.DateTime> FechaCreacion { get; set; }
        public Nullable<System.DateTime> FechaActualizacion { get; set; }
        public int CodigoClausulaContrato { get; set; }
        public int CodigoInformeInspeccion { get; set; }
    
        public virtual SM_CLAUSULA_CONTRATO SM_CLAUSULA_CONTRATO { get; set; }
        public virtual SM_INFORME_INSPECCION SM_INFORME_INSPECCION { get; set; }
    }
}
