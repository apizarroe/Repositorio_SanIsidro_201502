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
    
    public partial class MA_AREA
    {
        public MA_AREA()
        {
            this.MA_EMPLEADO = new HashSet<MA_EMPLEADO>();
        }
    
        public int idArea { get; set; }
        public string descArea { get; set; }
        public Nullable<int> UsrRegistra { get; set; }
        public Nullable<System.DateTime> FecRegistra { get; set; }
        public Nullable<int> UsrModifica { get; set; }
        public Nullable<System.DateTime> FecModifica { get; set; }
    
        public virtual ICollection<MA_EMPLEADO> MA_EMPLEADO { get; set; }
    }
}
