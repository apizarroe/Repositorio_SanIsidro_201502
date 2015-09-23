using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace PryMuniIntegrado.ET
{
    public enum EEstado : byte { PENDIENTE, CERRADO }

    public class Inventario
    {
        #region Propiedades
        [Display(Name = "Código")]
        public string Codigo { get; set; }
        [Display(Name = "Comité")]
        public string Comite { get; set; }
        [Display(Name = "Periodo")]
        public int Periodo { get; set; }
        [Display(Name = "Estado")]
        public EEstado Estado { get; set; }
        [DataType(DataType.Date)]        
        [Display(Name = "Inicio Programado")]
        public DateTime InicioProgramado { get; set; }
        [DataType(DataType.Date)]        
        [Display(Name = "Inicio Real")]
        public DateTime InicioReal { get; set; }
        [DataType(DataType.Date)]        
        [Display(Name = "Final Real")]
        public DateTime FinalReal { get; set; }
        public ObservableCollection<ActivoFijo> Activos { get; set; }
        #endregion

        public Inventario()
        {
            this.Codigo = string.Empty;
            this.Comite = string.Empty;
            this.Periodo = 0;
            this.Estado = EEstado.PENDIENTE;
            this.InicioProgramado = new DateTime();
            this.InicioReal = new DateTime();
            this.FinalReal = new DateTime();
            this.Activos = new ObservableCollection<ActivoFijo>();
        }

        public override string ToString()
        {
            return this.Codigo;
        }
    }
}
