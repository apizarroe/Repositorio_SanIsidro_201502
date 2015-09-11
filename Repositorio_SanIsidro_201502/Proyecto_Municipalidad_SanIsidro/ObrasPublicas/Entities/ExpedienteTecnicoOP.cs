using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObrasPublicas.Entities
{
    public class ExpedienteTecnicoOP
    {
        public const String STR_ID_TIPO_EJECUTOR_PERSONA = "P";
        public const String STR_ID_TIPO_EJECUTOR_EMPRESA = "E";

        public const String STR_ID_TIPO_DOC_ESPEC_TEC = "ET";
        public const String STR_ID_TIPO_DOC_ESTUDIOS_BAS = "EB";
        public const String STR_ID_TIPO_DOC_METRADOS = "MP";
        public const String STR_ID_TIPO_DOC_ANALISIS_COS = "AC";
        public const String STR_ID_TIPO_DOC_PRES_ANALIT = "PA";
        public const String STR_ID_TIPO_DOC_PROG_OBRA = "PO";
        public const String STR_ID_TIPO_DOC_LISTADO_INSUMOS = "LI";
        public const String STR_ID_TIPO_DOC_PLANOS_EJEC = "PE";
        public const String STR_ID_TIPO_DOC_EST_SUELO = "ES";

        public int IdExpediente { get; set; }
        public ProyectoInversion Proyecto { get; set; }
        public String Descripcion { get; set; }
        public String Especificaciones { get; set; }
        public Decimal ValorReferencial { get; set; }
        //public String PartidaPresupuestaria { get; set; }
        public String TipoEjecutor { get; set; }
        public int EjecutorId { get; set; }
        public String EjecutorNom { get; set; }
        public String EjecutorApe { get; set; }
        public String EjecutorRazonSocial { get; set; }
        public int ContactoId { get; set; }
        public String ContactoNom { get; set; }
        public String ContactoApe { get; set; }
        public String ContactoTelefono { get; set; }
        public String ContactoEmail { get; set; }
        public String ContactoDireccion { get; set; }
        public int SupervisorId { get; set; }
        public String SupervisorNom { get; set; }
        public String SupervisorApe { get; set; }
        public String SupervisorTelefono { get; set; }
        public String SupervisorEmail { get; set; }

        public List<DocumentoExpedienteTecnicoOP> Documentos { get; set; }
    }
}
