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
    
    public partial class MA_PERFIL
    {
        public MA_PERFIL()
        {
            this.MA_USUARIO = new HashSet<MA_USUARIO>();
        }
    
        public int coPerfil { get; set; }
        public string noDesPerfil { get; set; }
        public Nullable<short> fiPerfil { get; set; }
        public Nullable<System.DateTime> feCreacion { get; set; }
    
        public virtual ICollection<MA_USUARIO> MA_USUARIO { get; set; }
    }
}
