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
    
    public partial class SM_INFORME_TECNICO
    {
        public SM_INFORME_TECNICO()
        {
            this.SM_RECURSOS = new HashSet<SM_RECURSOS>();
            this.SM_ANEXO = new HashSet<SM_ANEXO>();
        }
    
        public int CodigoInformeTecnico { get; set; }
        public Nullable<System.DateTime> FechaCreacion { get; set; }
        public Nullable<System.DateTime> FechaActualizacion { get; set; }
        public Nullable<System.DateTime> FechaRecepcion { get; set; }
        public string DescripcionInformeTecnico { get; set; }
        public string Requisito { get; set; }
        public string Objetivo { get; set; }
        public string Justificacion { get; set; }
        public Nullable<decimal> PresupuestoTentativo { get; set; }
        public int CodigoExpedienteServicio { get; set; }
        public int CodigoPersonaElaborador { get; set; }
        public Nullable<System.DateTime> FechaAprobacion { get; set; }
        public Nullable<int> CodigoEmpleadoAprobador { get; set; }
    
        public virtual MA_EMPLEADO MA_EMPLEADO { get; set; }
        public virtual MA_PERSONA MA_PERSONA { get; set; }
        public virtual SM_EXPEDIENTE_SERVICIO SM_EXPEDIENTE_SERVICIO { get; set; }
        public virtual ICollection<SM_RECURSOS> SM_RECURSOS { get; set; }
        public virtual ICollection<SM_ANEXO> SM_ANEXO { get; set; }
    }
}
