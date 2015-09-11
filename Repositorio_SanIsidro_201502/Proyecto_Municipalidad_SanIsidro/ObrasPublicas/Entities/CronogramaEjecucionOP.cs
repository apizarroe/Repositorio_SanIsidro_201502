using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObrasPublicas.Entities
{
    public class CronogramaEjecucionOP
    {
        public ProyectoInversion ProyectoInversion { get; set; }
        public ExpedienteTecnicoOP ExpedienteTecnico { get; set; }
        public int IdCronograma { get; set; }
        public DateTime FechaEmision { get; set; }
        public int PlazoEjecucion { get; set; }
        public List<ActividadCronogramaOP> ListaActividades { get; set; }
    }
}
