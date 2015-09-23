using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

using PryMuniIntegrado.DAL;
using PryMuniIntegrado.ET;

namespace PryMuniIntegrado.BL
{
    public class InformeBL
    {
        #region Funciones Estaticas
        public static ObservableCollection<Informe> ListarInforme()
        {
            return InformeDAL.ListarInforme();
        }
        #endregion

        public static ObservableCollection<Informe> ListarInformePorCodigo(string codigo)
        {
            return InformeDAL.ListarInformePorCodigo(codigo);
        }
    }
}
