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
    
    public partial class SM_INSPECCION
    {
        public SM_INSPECCION()
        {
            this.SM_INFORME_INSPECCION = new HashSet<SM_INFORME_INSPECCION>();
        }
    
        public int CodigoInspeccion { get; set; }
        public Nullable<System.DateTime> FechaInspeccion { get; set; }
        public Nullable<int> coVia { get; set; }
        public string LugarInspeccion { get; set; }
        public Nullable<int> CodigoServicio { get; set; }
        public Nullable<int> coUsuario { get; set; }
        public Nullable<System.DateTime> FechaCreacion { get; set; }
        public Nullable<System.DateTime> FechaActualizacion { get; set; }
        public string ResponsableServicio { get; set; }
        public Nullable<int> CodigoPersonaEjecutor { get; set; }
        public Nullable<System.TimeSpan> HoraFin { get; set; }
        public Nullable<System.TimeSpan> HoraIni { get; set; }
        public Nullable<int> Estado { get; set; }
    
        public virtual MA_PERSONA MA_PERSONA { get; set; }
        public virtual ICollection<SM_INFORME_INSPECCION> SM_INFORME_INSPECCION { get; set; }
        public virtual SM_SERVICIO SM_SERVICIO { get; set; }
    }
}
