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
    
    public partial class CT_PREDIO
    {
        public int int_IdPredio { get; set; }
        public Nullable<int> int_IdLote { get; set; }
        public string chr_CodigoPredio { get; set; }
        public string var_Nombre { get; set; }
        public string var_Ubicacion { get; set; }
        public Nullable<decimal> dcm_AreaConstruida { get; set; }
        public string var_Observaciones { get; set; }
        public Nullable<int> int_Altura { get; set; }
        public System.DateTime dtm_FechaCreacion { get; set; }
        public Nullable<System.DateTime> dtm_FechaActualizacion { get; set; }
        public System.DateTime dtm_FechaRegistro { get; set; }
    
        public virtual CT_LOTE CT_LOTE { get; set; }
    }
}