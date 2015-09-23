using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Web;

using PryMuniIntegrado.BL;
using PryMuniIntegrado.ET;
using System.ComponentModel.DataAnnotations;

namespace PryMuniIntegrado.WEB.Models
{
    public class DetalleInventario
    {
        #region Propiedades
        public String CodigoActivo { get; set; }
        public ActivoFijo Activo { get; set; }
        public Inventario Inventario { get; set; }
        public ObservableCollection<ActivoFijo> ActivosMaestro { get; set; }
        public ObservableCollection<ActivoFijo> ActivosInventario { get; set; }
        #endregion

        public DetalleInventario() 
        {
            this.CodigoActivo = string.Empty;
            this.Activo = new ActivoFijo();
            this.Inventario = new Inventario();
            this.ActivosMaestro = ActivoFijoBL.ListarActivosDeMaestro();
            this.ActivosInventario = new ObservableCollection<ActivoFijo>();
        }
        
        public void Inicializa(string codigo)
        {
            this.Inventario = InventarioBL.ObtenerInventario(codigo);
            this.ActivosInventario = ActivoFijoBL.ListarActivosDeInventario(codigo);
        }

        internal void Procesa(string codActivo)
        {
            this.Activo = (from a in ActivosMaestro.Where(a => a.CodigoActivo == codActivo) select a).ToList()[0];
            this.ActivosInventario.Add(Activo);
            this.ActivosMaestro.Remove(Activo);
        }
    }
}