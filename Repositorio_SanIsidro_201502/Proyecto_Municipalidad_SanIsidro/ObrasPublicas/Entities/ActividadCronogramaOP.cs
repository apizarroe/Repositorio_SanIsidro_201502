using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObrasPublicas.Entities
{
    public class ActividadCronogramaOP
    {
        public int IdActividad { get; set; }
        public String Nombre { get; set; }
        public DateTime FechaIniProg { get; set; }
        public DateTime FechaFinProg { get; set; }
        public DateTime FechaIniEjec { get; set; }
        public DateTime FechaFinEjec { get; set; }
        public decimal Costo { get; set; }
        public int CantidadRRHH { get; set; }
        public String IdTipoResponsable { get; set; }
        public String ResponsableNom { get; set; }
        public String ResponsableApe { get; set; }
        public String ResponsableRazSoc { get; set; }
        public int? IdArea { get; set; }
        public int IdEmpleado { get; set; }
    }
}
