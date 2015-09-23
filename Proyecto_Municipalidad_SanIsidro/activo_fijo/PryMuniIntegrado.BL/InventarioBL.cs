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
    public class InventarioBL
    {
        #region Propiedades
        [Required]
        [Display(Name = "Mostrar")]
        public int Cantidad { get; set; }
        [Required]
        [Display(Name = "Estado")]
        public EEstado Estado { get; set; }
        #endregion

        #region Funciones Estaticas
        public static ObservableCollection<Inventario> ListarInventario()
        {
            return InventarioDAL.ListarInventario();
        }
        public static ObservableCollection<Inventario> ListarInventarioPorEstadoAño(int cantidad, int periodo, string estado)
        {
            return InventarioDAL.ListarInventarioPorEstadoAño(cantidad, periodo, estado);
        }
        public static bool GrabarInventario(string codigo, EEstado estado, DateTime fechaInicio)
        {
            return InventarioDAL.GrabarInventario(codigo, estado, fechaInicio);
        }
        public static Inventario ObtenerInventario(string codigo)
        {
            return InventarioDAL.ObtenerInventario(codigo);
        }
        #endregion
    }
}
