//------------------------------------------------------------------------------
// <auto-generated>
//    Este código se generó a partir de una plantilla.
//
//    Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//    Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Infraestructura.Data.SQL
{
    using System;
    using System.Collections.Generic;
    
    public partial class INFORMETERCERO
    {
        public string idintormeTercero { get; set; }
        public Nullable<int> idSolicitudTercero { get; set; }
        public Nullable<System.DateTime> Fecha { get; set; }
        public string Hora { get; set; }
        public string Respuesta { get; set; }
        public Nullable<int> UsrRegistra { get; set; }
        public Nullable<System.DateTime> FecRegistra { get; set; }
        public Nullable<int> UsrModifica { get; set; }
        public Nullable<System.DateTime> FecModifica { get; set; }
    
        public virtual SOLICITUDTERCERO SOLICITUDTERCERO { get; set; }
    }
}
