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
    public class ModeloPecosa
    {
        #region Propiedades
        [Required]
        [Display(Name = "Codigo de Pecosa")]
        public string CodigoPecosa { get; set; }
        [Required]
        [Display(Name = "Codigo de Informe")]
        public string CodigoInforme { get; set; }
        [Required]
        [Display(Name = "Mostrar")]
        public int Cantidad { get; set; }
        public ObservableCollection<Pecosa> ListaPecosa { get; set; }
        public ObservableCollection<Pecosa> ListaPecosaFiltrada { get; set; }
        #endregion

        public ModeloPecosa()
        {
            this.ListaPecosa = PecosaBL.ListarPecosa();
            this.ListaPecosaFiltrada = ListaPecosa;
            this.CodigoPecosa = string.Empty;            
            this.CodigoInforme = string.Empty;
            this.Cantidad = 0;
        }
    }
}