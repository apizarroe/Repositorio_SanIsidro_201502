using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObrasPublicas.Entities
{
    [Serializable] 
    public class DocumentoExpedienteTecnicoOP
    {
        public int IdDocumento { get; set; }
        public int IdExpediente { get; set; }
        public String NroDocumento { get; set; }
        public DateTime FechaEmision { get; set; }
        public String Descripcion { get; set; }
        public String TipoDocumento { get; set; }
        public String NomTipoDocumento { get; set; }
        public String RutaArchivo { get; set; }
        public String NomArchivo { get; set; }
        public byte[] Archivo { get; set; }

    }
}
