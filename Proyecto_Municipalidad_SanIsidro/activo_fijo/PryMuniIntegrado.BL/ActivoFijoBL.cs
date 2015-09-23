using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

using PryMuniIntegrado.DAL;
using PryMuniIntegrado.ET;

namespace PryMuniIntegrado.BL
{
    public class ActivoFijoBL
    {
        #region Funciones Estaticas
        public static ObservableCollection<ActivoFijo> ListarActivosDeMaestro()
        {
            return ActivoFijoDAL.ListarActivosDeMaestro();
        }
        public static ObservableCollection<ActivoFijo> ListarActivosDeInventario(string codigo)
        {
            return ActivoFijoDAL.ListarActivosDeInventario(codigo);
        }
        public static bool InsertarActivoAInventario(string codigoInventario, string codigoActivo)
        {
            return ActivoFijoDAL.InsertarActivoAInventario(codigoInventario, codigoActivo);
        }
        public static bool ActualizarActivoDeInventario(string codigoActivo, string codigoInventario, int evaluar)
        {
            return ActivoFijoDAL.ActualizarActivoDeInventario(codigoActivo, codigoInventario, evaluar);
        }
        public static bool EliminarActivoInventario(string codigoActivo, string codigoInventario)
        {
            return ActivoFijoDAL.EliminarActivoInventario(codigoActivo, codigoInventario);
        }
        public static ObservableCollection<ActivoFijo> ListarActivosDeEvaluacion(string codigo)
        {
            return ActivoFijoDAL.ListarActivosDeEvaluacion(codigo);
        }
        public static ObservableCollection<ActivoFijo> ListarActivosDeInforme(string codigo)
        {
            return ActivoFijoDAL.ListarActivosDeInforme(codigo);
        }
        public static ObservableCollection<ActivoFijo> ListarActivosDePecosa(string codigo)
        {
            return ActivoFijoDAL.ListarActivosDePecosa(codigo);
        }
        #endregion
    }
}
