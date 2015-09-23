using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

using PryMuniIntegrado.BL;
using PryMuniIntegrado.ET;
using System.Collections;

namespace PryMuniIntegrado.WEB.Models
{
    public class ModeloInventario
    {
        #region Propiedades
        [Display(Name = "Mostrar")]
        public string Cantidad { get; set; }
        [Display(Name = "Periodo (Año)")]
        public string Periodo { get; set; }
        [Display(Name = "Estado")]
        public string Estado { get; set; }
        [Display(Name="Codigo de Activo")]
        public string CodigoActivo { get; set; }
        public string CodigoInventario { get; set; }
        public Inventario Inventario { get; set; }
        public ObservableCollection<ActivoFijo> ListaActivo { get; set; }
        public ObservableCollection<Inventario> ListaInventario { get; set; }
        public ObservableCollection<Inventario> ListaInventarioFiltrada { get; set; }
        #endregion

        public ModeloInventario()
        {            
            this.Cantidad = string.Empty;
            this.Periodo = string.Empty;
            this.Estado = string.Empty;
            this.CodigoActivo = string.Empty;
            this.CodigoInventario = string.Empty;
            this.Inventario = new Inventario();
            this.ListaActivo = ActivoFijoBL.ListarActivosDeMaestro();
            this.ListaInventario = InventarioBL.ListarInventario();
            this.ListaInventarioFiltrada = ListaInventario;
        }

        #region Funciones
        public void ObtenerInventario(string codigo)
        {
            if (Inventario == null)
                Inventario = new Inventario();

            this.Inventario = InventarioBL.ObtenerInventario(codigo);
            this.Inventario.Activos = ActivoFijoBL.ListarActivosDeInventario(codigo);
        }
        public void ObtenerInventario(string codigoInventario, string codigoActivo)
        {
            if(!string.IsNullOrEmpty(codigoActivo))
            {
                ActivoFijoBL.InsertarActivoAInventario(codigoInventario, codigoActivo);
            }
            ObtenerInventario(codigoInventario);
        }
        public void GrabaInventario(string codigoInventario, EEstado estado, DateTime fechaInicio)
        {
            if (!string.IsNullOrEmpty(codigoInventario))
            {
                InventarioBL.GrabarInventario(codigoInventario, estado, fechaInicio);
            }
            ObtenerInventario(codigoInventario);
        }
        public void AgregarActivoAInventario(ActivoFijo activo)
        {
            if(activo != null)
            {
                this.Inventario.Activos.Add(activo);
            }
        }
        public void ActualizarActivoInventario(string codigoActivo, string codigoInventario, bool evalua)
        {
            if(!string.IsNullOrEmpty(codigoActivo) && !string.IsNullOrEmpty(codigoInventario))
            {
                ActivoFijoBL.ActualizarActivoDeInventario(codigoActivo, codigoInventario, evalua ? 1 : 0);
            }
            ObtenerInventario(codigoInventario);
        }
        public void EliminaActivoInventario(string codigoActivo, string codigoInventario)
        {
            if (!string.IsNullOrEmpty(codigoActivo) && !string.IsNullOrEmpty(codigoInventario))
            {
                ActivoFijoBL.EliminarActivoInventario(codigoActivo, codigoInventario);
            }
            ObtenerInventario(codigoInventario);
        }
        #endregion
    }
}