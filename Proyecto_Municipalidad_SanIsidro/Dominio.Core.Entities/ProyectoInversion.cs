using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Core.Entities
{
    public class ProyectoInversion
    {
        public const String STR_ID_ESTADO_EN_CONSULTA = "E";
        public const String STR_ID_ESTADO_VIABLE = "V";
        public const String STR_ID_ESTADO_INVIABLE = "I";

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
    }
}
