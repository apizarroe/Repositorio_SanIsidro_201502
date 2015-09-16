using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dominio.Core.Entities;

namespace ObrasPublicas.Entities
{
    public class EntregaMaterialOP
    {
        public ProyectoInversion Proyecto { get; set; }
        public int IdEntrega { get; set; }
        public DateTime FecEntregaProg { get; set; }
        public DateTime FecEntregaEfec { get; set; }
        public String Observaciones { get; set; }
        public String TipoEntrega { get; set; }
        public Proveedor Proveedor { get; set; }
        public MaterialOP Material { get; set; }
        public int Cantidad { get; set; }

        public const String STR_ID_TIPO_CONFORME = "C";
        public const String STR_ID_TIPO_FUERA_DE_FECHA = "F";
    }
}
