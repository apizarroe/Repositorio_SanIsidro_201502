using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObrasPublicas.Entities
{
    public class ProyectoInversion
    {
        public const String STR_ID_ESTADO_EN_CONSULTA = "E";
        public const String STR_ID_ESTADO_VIABLE = "V";
        public const String STR_ID_ESTADO_INVIABLE = "I";
        public const String STR_ID_ESTADO_ADJUDICADO = "A";
        public const String STR_ID_ESTADO_CERRADO = "C";

        public int IdProyecto { get; set; }
        public String CodSNIP { get; set; }
        public String Nombre { get; set; }
        public String Ubicacion { get; set; }
        public String Descripcion { get; set; }
        public int IdVia { get; set; }
        public int Beneficiarios { get; set; }
        public Decimal ValorReferencial { get; set; }
        public String TipoVia { get; set; }
        public String NomVia { get; set; }
        public String IdEstado { get; set; }
        public String NomEstado { get; set; }
        public Decimal CostoProyecto { get; set; }

        public int IdExpediente { get; set; }
        public int IdCronograma { get; set; }
        public DateTime FechaEmisionCrono { get; set; }
        public int PlazoEjecucionCrono { get; set; }
    }
}
