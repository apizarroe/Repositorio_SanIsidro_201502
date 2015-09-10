using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Models.GSM
{
    public class Servicio
    {
        public int IdServicio { get; set; }
        public String Nombre { get; set; }
        public int IdTipoServicio { get; set; }
        public String TipoServicio { get; set; }

        public int Filas { get; set; }
    }
}