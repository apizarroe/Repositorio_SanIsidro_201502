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
