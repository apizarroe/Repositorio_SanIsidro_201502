using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

using PryMuniIntegrado.DAL;
using PryMuniIntegrado.ET;

namespace PryMuniIntegrado.BL
{
    public class EvaluacionBL
    {
        #region Funciones Estaticas
        public static ObservableCollection<Evaluacion> ListarEvaluacionPorCodigo(string codigo)
        {
            return EvaluacionDAL.ListarEvaluacionPorCodigo(codigo);
        }
        public static ObservableCollection<Evaluacion> ListarEvaluacion()
        {
            return EvaluacionDAL.ListarEvaluacion();
        }
        public static Evaluacion ObtenerEvaluacion(string codigo)
        {
            return EvaluacionDAL.ObtenerEvaluacion(codigo);
        }
        #endregion
    }
}
