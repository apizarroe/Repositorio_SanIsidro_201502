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
    
    public partial class MA_EMPLEADO
    {
        public MA_EMPLEADO()
        {
            this.CT_PROPUESTAINSPECCION = new HashSet<CT_PROPUESTAINSPECCION>();
            this.CT_PROPUESTAINSPECCION_EMPLEADO = new HashSet<CT_PROPUESTAINSPECCION_EMPLEADO>();
        }
    
        public int idEmpleado { get; set; }
        public Nullable<int> idPersona { get; set; }
        public Nullable<int> idCargo { get; set; }
        public Nullable<int> idArea { get; set; }
        public string CodigoEmpleado { get; set; }
        public Nullable<int> UsrRegistra { get; set; }
        public Nullable<System.DateTime> FecRegistra { get; set; }
        public Nullable<int> UsrModifica { get; set; }
        public Nullable<System.DateTime> FecModifica { get; set; }
    
        public virtual ICollection<CT_PROPUESTAINSPECCION> CT_PROPUESTAINSPECCION { get; set; }
        public virtual ICollection<CT_PROPUESTAINSPECCION_EMPLEADO> CT_PROPUESTAINSPECCION_EMPLEADO { get; set; }
    }
}
