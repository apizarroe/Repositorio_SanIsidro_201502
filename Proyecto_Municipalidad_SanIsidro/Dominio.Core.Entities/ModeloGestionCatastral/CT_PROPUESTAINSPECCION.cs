//------------------------------------------------------------------------------
// <auto-generated>
//    Este código se generó a partir de una plantilla.
//
//    Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//    Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Dominio.Core.Entities.ModeloGestionCatastral
{
    using System;
    using System.Collections.Generic;
    
    public partial class CT_PROPUESTAINSPECCION
    {
        public CT_PROPUESTAINSPECCION()
        {
            this.CT_PROPUESTAINSPECCION_EMPLEADO = new HashSet<CT_PROPUESTAINSPECCION_EMPLEADO>();
        }
    
        public int int_IdPropuestaInspeccion { get; set; }
        public Nullable<int> IdEmpleado { get; set; }
        public Nullable<int> int_IdSolicitud { get; set; }
        public string var_NroPropuesta { get; set; }
        public string var_Descripcion { get; set; }
        public Nullable<int> int_CantResponsable { get; set; }
        public System.DateTime dtm_FechaDocumento { get; set; }
        public System.DateTime dtm_FechaRegistro { get; set; }
        public string var_Observacion { get; set; }
        public Nullable<int> int_Estado { get; set; }
    
        public virtual ICollection<CT_PROPUESTAINSPECCION_EMPLEADO> CT_PROPUESTAINSPECCION_EMPLEADO { get; set; }
        public virtual MA_EMPLEADO MA_EMPLEADO { get; set; }
        public virtual CT_SOLICITUD CT_SOLICITUD { get; set; }
    }
}
