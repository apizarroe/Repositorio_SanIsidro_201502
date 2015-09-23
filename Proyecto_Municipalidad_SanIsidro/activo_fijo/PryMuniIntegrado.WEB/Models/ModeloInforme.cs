using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

using PryMuniIntegrado.BL;
using PryMuniIntegrado.ET;

namespace PryMuniIntegrado.WEB.Models
{
    public class ModeloInforme
    {
        #region Propiedades
        [Required]
        [Display(Name = "Codigo de Informe")]
        public string CodigoInforme { get; set; }
        [Required]
        [Display(Name = "Codigo de Evaluacion")]
        public string CodigoEvaluacion { get; set; }
        public ObservableCollection<Informe> ListaInforme { get; set; }
        public ObservableCollection<Informe> ListaInformeFiltrada { get; set; }
        #endregion

        public ModeloInforme()
        {            
            this.ListaInforme = InformeBL.ListarInforme();
            this.ListaInformeFiltrada = new ObservableCollection<Informe>();
            this.CodigoInforme = string.Empty;
            this.CodigoEvaluacion = string.Empty;            
        }
    }
}