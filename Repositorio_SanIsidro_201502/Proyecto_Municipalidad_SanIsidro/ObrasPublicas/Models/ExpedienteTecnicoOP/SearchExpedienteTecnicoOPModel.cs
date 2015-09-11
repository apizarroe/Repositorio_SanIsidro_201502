using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ObrasPublicas.Models.ExpedienteTecnicoOP
{
    public class SearchExpedienteTecnicoOPModel
    {
        public String SearchCodSNIP { get; set; }
        public String SearchNombre { get; set; }
        public String SearchUbicacion { get; set; }
        public String SearchIdEstado { get; set; }
    }
}