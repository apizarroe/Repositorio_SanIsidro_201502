using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

using PryMuniIntegrado.DAL;
using PryMuniIntegrado.ET;

namespace PryMuniIntegrado.BL
{
    public class PecosaBL
    {
        #region Funcions Estaticas
        public static ObservableCollection<Pecosa> ListarPecosa()
        {
            return PecosaDAL.ListarPecosa();
        }
        public static ObservableCollection<Pecosa> ListarPecosaPorCodigo(string codigo)
        {
            return PecosaDAL.ListarPecosaPorCodigo(codigo);
        }
        public static bool GrabarPecosa(Pecosa pecosa)
        {
            return PecosaDAL.GrabarPecosa(pecosa);
        }
        #endregion

        
    }
}
