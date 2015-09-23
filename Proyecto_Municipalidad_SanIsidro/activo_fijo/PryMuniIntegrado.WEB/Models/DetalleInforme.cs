using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Web;

using PryMuniIntegrado.BL;
using PryMuniIntegrado.ET;

namespace PryMuniIntegrado.WEB.Models
{
    public class DetalleInforme
    {
        #region Propiedades
        public Informe Informe { get; set; }
        public ObservableCollection<ActivoFijo> Activos { get; set; }
        #endregion

        public DetalleInforme(string codigo)
        {
            this.Informe = InformeBL.ListarInformePorCodigo(codigo)[0];
            this.Activos = ActivoFijoBL.ListarActivosDeInforme(codigo);
        }
    }
}