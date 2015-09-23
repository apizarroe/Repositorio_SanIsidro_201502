using System;
using System.Data.SqlClient;
using System.Data;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

using PryMuniIntegrado.ET;

namespace PryMuniIntegrado.DAL
{
    public class InformeDAL
    {
        #region Funciones Estaticas
        public static ObservableCollection<Informe> ListarInforme()
        {
            var lista = new ObservableCollection<Informe>();

            try
            {
                using (var cnn = SQLConexion.Conectar())
                {
                    cnn.Open();
                    var query = new SqlCommand("usp_ListarInforme", cnn);
                    query.CommandType = CommandType.StoredProcedure;

                    using (var dr = query.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            var date = new DateTime();
                            var informe = new Informe();
                            informe.Codigo = dr["Codigo"].ToString();
                            informe.Estado = (EEstado)Enum.Parse(typeof(EEstado), dr["Estado"].ToString());
                            informe.Responsable = dr["Responsable"].ToString();
                            DateTime.TryParse(dr["FechaRegistro"].ToString(), out date);
                            informe.FechaRegistro = date;
                            informe.CodEvaluacion = dr["CodEvaluacion"].ToString();

                            lista.Add(informe);
                        }
                    }
                }
            }
            catch (ArgumentException e)
            {
                throw new Exception(e.Message);
            }

            return lista;
        }
        public static ObservableCollection<Informe> ListarInformePorCodigo(string codigo)
        {
            var lista = new ObservableCollection<Informe>();

            try
            {
                using (var cnn = SQLConexion.Conectar())
                {
                    cnn.Open();
                    var query = new SqlCommand("usp_ListarInformePorCodigo", cnn);
                    query.CommandType = CommandType.StoredProcedure;
                    query.Parameters.Add(new SqlParameter("@CODIGO", codigo));
                    using (var dr = query.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            var date = new DateTime();
                            var informe = new Informe();
                            informe.Codigo = dr["Codigo"].ToString();
                            informe.Estado = (EEstado)Enum.Parse(typeof(EEstado), dr["Estado"].ToString());
                            informe.Responsable = dr["Responsable"].ToString();
                            DateTime.TryParse(dr["FechaRegistro"].ToString(), out date);
                            informe.FechaRegistro = date;
                            informe.CodEvaluacion = dr["CodEvaluacion"].ToString();

                            lista.Add(informe);
                        }
                    }
                }
            }
            catch (ArgumentException e)
            {
                throw new Exception(e.Message);
            }

            return lista;
        }
        #endregion
    }
}
