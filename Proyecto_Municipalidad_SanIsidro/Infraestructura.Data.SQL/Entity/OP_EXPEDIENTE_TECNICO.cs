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
    
    public partial class OP_EXPEDIENTE_TECNICO
    {
        public OP_EXPEDIENTE_TECNICO()
        {
            this.OP_CRONOGRAMA_EJECUCION = new HashSet<OP_CRONOGRAMA_EJECUCION>();
            this.OP_DOCUMENTO_EXPEDIENTE_TECNICO = new HashSet<OP_DOCUMENTO_EXPEDIENTE_TECNICO>();
        }
    
        public int coExpediente { get; set; }
        public Nullable<int> coProyecto { get; set; }
        public string txDescripcion { get; set; }
        public string txEspecificacionesTecnicas { get; set; }
        public Nullable<System.DateTime> feElaboracionPresupuesto { get; set; }
        public Nullable<decimal> nuValorReferencial { get; set; }
        public string nuPartidaPresupuestaria { get; set; }
        public Nullable<int> coEjecutor { get; set; }
        public Nullable<int> coSupervisor { get; set; }
        public Nullable<int> coContacto { get; set; }
    
        public virtual MA_PERSONA MA_PERSONA { get; set; }
        public virtual MA_PERSONA MA_PERSONA1 { get; set; }
        public virtual MA_PERSONA MA_PERSONA2 { get; set; }
        public virtual ICollection<OP_CRONOGRAMA_EJECUCION> OP_CRONOGRAMA_EJECUCION { get; set; }
        public virtual ICollection<OP_DOCUMENTO_EXPEDIENTE_TECNICO> OP_DOCUMENTO_EXPEDIENTE_TECNICO { get; set; }
        public virtual OP_PROYECTO_INVERSION_PUBLICA OP_PROYECTO_INVERSION_PUBLICA { get; set; }
    }
}
