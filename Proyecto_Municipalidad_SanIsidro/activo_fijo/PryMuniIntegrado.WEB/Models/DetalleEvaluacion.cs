using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Collections.ObjectModel;

using PryMuniIntegrado.BL;
using PryMuniIntegrado.ET;

namespace PryMuniIntegrado.WEB.Models
{
    public class DetalleEvaluacion
    {
        #region Propiedades
        public Evaluacion Evaluacion { get; set; }
        public ObservableCollection<ActivoFijo> Activos { get; set; }
        #endregion

        public DetalleEvaluacion(string codigo)
        {
            this.Evaluacion = EvaluacionBL.ObtenerEvaluacion(codigo);
            this.Activos = ActivoFijoBL.ListarActivosDeEvaluacion(codigo);
        }
    }
}