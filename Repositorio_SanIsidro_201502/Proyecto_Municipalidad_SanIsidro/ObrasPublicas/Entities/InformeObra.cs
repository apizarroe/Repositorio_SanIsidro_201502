using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ObrasPublicas.Entities
{
    public class InformeObra
    {
        public const int INT_ID_ESTADO_GENERADO = 1;
        public const int INT_ID_ESTADO_ANULADO = 2;
        public ProyectoInversion ProyectoInversion { get; set; }
        public DateTime FechaEmision { get; set; }
        public int IdEstado { get; set; }
        public String NomEstado { get; set; }
        public int IdInforme { get; set; }
        public String Titulo { get; set; }
        public String Conclusion { get; set; }
        public String Recomendacion { get; set; }
        public String TipoInforme { get; set; }
    }
}