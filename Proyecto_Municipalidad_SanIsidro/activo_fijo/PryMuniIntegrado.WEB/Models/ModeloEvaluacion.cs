using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

using PryMuniIntegrado.BL;
using PryMuniIntegrado.ET;

namespace PryMuniIntegrado.WEB.Models
{
    public class ModeloEvaluacion
    {
        #region Propiedades
        [Required]
        [Display(Name = "Codigo de Evaluacion")]
        public string CodigoEvaluacion { get; set; }
        [Required]
        [Display(Name = "Codigo de Inventario")]
        public string CodigoInventario { get; set; }
        public ObservableCollection<Evaluacion> ListaEvaluacion { get; set; }
        public ObservableCollection<Evaluacion> ListaEvaluacionFiltrada { get; set; }
        #endregion

        public ModeloEvaluacion()
        {            
            this.ListaEvaluacion = EvaluacionBL.ListarEvaluacion();
            this.ListaEvaluacionFiltrada = new ObservableCollection<Evaluacion>();
            this.CodigoEvaluacion = string.Empty;
            this.CodigoInventario = string.Empty;
        }
    }
}