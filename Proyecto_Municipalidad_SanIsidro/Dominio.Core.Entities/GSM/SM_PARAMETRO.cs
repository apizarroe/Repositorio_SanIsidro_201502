using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Core.Entities.GSM
{
    public class SM_PARAMETRO
    {
        public String name { get; set; }
        public String value { get; set; }
        public String value2 { get; set; }

        public int Pagina { get; set; }
        public int Paginacion { get; set; }
        public int Tipo { get; set; }
        public int Id { get; set; }
        public DateTime fecha1 { get; set; }
        public DateTime fecha2 { get; set; }
    }
}
