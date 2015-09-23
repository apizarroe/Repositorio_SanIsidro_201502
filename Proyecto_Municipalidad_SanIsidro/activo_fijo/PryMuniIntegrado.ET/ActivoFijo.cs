using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace PryMuniIntegrado.ET
{
    public class ActivoFijo
    {
        #region Propiedades
        [Display(Name = "Código")]
        public string CodigoActivo { get; set; }
        [Display(Name = "Descripción")]
        public string Decripcion { get; set; }
        [Display(Name = "Marca")]
        public string Marca { get; set; }
        [Display(Name = "Modelo")]
        public string Modelo { get; set; }
        [Display(Name = "Tipo")]
        public string Tipo { get; set; }
        [Display(Name = "Sede")]
        public string Sede { get; set; }
        [Display(Name = "FechaAlta")]
        public DateTime FechaAlta { get; set; }
        [Display(Name = "Evaluar?")]
        public bool Evaluar { get; set; }
        [Display(Name="Causal de baja")]
        public string CausalBaja { get; set; }
        [Display(Name = "Valor de Libros")]
        public string ValorLibros { get; set; }
        [Display(Name = "Depreción Acumulada")]
        public string DeprecionAcumulada { get; set; }
        #endregion

        public ActivoFijo()
        {
            this.CodigoActivo = string.Empty;
            this.Decripcion = string.Empty;
            this.Marca = string.Empty;
            this.Modelo = string.Empty;
            this.Tipo = string.Empty;
            this.Sede = string.Empty;
            this.FechaAlta = new DateTime();
            this.Evaluar = false;
            this.CausalBaja = string.Empty;
            this.ValorLibros = string.Empty;
            this.DeprecionAcumulada = string.Empty;
        }

        #region Funciones Sobrecargadas
        public override string ToString()
        {
            return this.CodigoActivo;
        }
        #endregion
    }
}
