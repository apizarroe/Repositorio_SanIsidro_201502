using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Web;

using PryMuniIntegrado.BL;
using PryMuniIntegrado.ET;

namespace PryMuniIntegrado.WEB.Models
{
    public class DetallePecosa
    {
        #region Propiedades
        public Pecosa Pecosa { get; set; }
        public ObservableCollection<ActivoFijo> Activos { get; set; }
        #endregion
        public DetallePecosa() { }
        public DetallePecosa(string codigo)
        {
            this.Pecosa = PecosaBL.ListarPecosaPorCodigo(codigo)[0];
            this.Activos = ActivoFijoBL.ListarActivosDePecosa(codigo);
        }
    }
}