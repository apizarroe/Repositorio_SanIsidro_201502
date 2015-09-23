using System.Data.SqlClient;
using System.Data;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

using PryMuniIntegrado.ET;
using System;

namespace PryMuniIntegrado.DAL
{
    public class PecosaDAL
    {
        #region Funciones Estaticas
        public static ObservableCollection<Pecosa> ListarPecosa()
        {
            var lista = new ObservableCollection<Pecosa>();

            try
            {
                using (var cnn = SQLConexion.Conectar())
                {
                    cnn.Open();
                    var query = new SqlCommand("usp_ListarPecosa", cnn);
                    query.CommandType = CommandType.StoredProcedure;

                    using (var dr = query.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            var date = new DateTime();
                            var pecosa = new Pecosa();
                            pecosa.Codigo = dr["CodigoPecosa"].ToString();
                            DateTime.TryParse(dr["FechaRegistro"].ToString(), out date);
                            pecosa.FechaRegistro = date;
                            pecosa.Responsable = dr["Responsable"].ToString();
                            pecosa.Estado = (EEstado)Enum.Parse(typeof(EEstado), 
                                string.IsNullOrEmpty(dr["Estado"].ToString()) ? "TODOS" : dr["Estado"].ToString());
                            pecosa.CodInforme = dr["CodInforme"].ToString();
                            DateTime.TryParse(dr["FechaRegistroInforme"].ToString(), out date);
                            pecosa.FechaRegistroInforme = date;
                            pecosa.Resolucion = dr["Resolucion"].ToString();
                            lista.Add(pecosa);
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
        public static ObservableCollection<Pecosa> ListarPecosaPorCodigo(string codigo)
        {
            var lista = new ObservableCollection<Pecosa>();

            try
            {
                using (var cnn = SQLConexion.Conectar())
                {
                    cnn.Open();
                    var query = new SqlCommand("usp_ListarPecosaPorCodigo", cnn);
                    query.CommandType = CommandType.StoredProcedure;
                    query.Parameters.Add(new SqlParameter("@CODIGO", codigo));
                    using (var dr = query.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            var date = new DateTime();
                            var pecosa = new Pecosa();
                            pecosa.Codigo = dr["CodigoPecosa"].ToString();
                            DateTime.TryParse(dr["FechaRegistro"].ToString(), out date);
                            pecosa.FechaRegistro = date;
                            pecosa.Responsable = dr["Responsable"].ToString();
                            pecosa.Estado = (EEstado)Enum.Parse(typeof(EEstado),
                                string.IsNullOrEmpty(dr["Estado"].ToString()) ? "TODOS" : dr["Estado"].ToString());
                            pecosa.CodInforme = dr["CodInforme"].ToString();
                            DateTime.TryParse(dr["FechaRegistroInforme"].ToString(), out date);
                            pecosa.FechaRegistroInforme = date;
                            pecosa.Resolucion = dr["Resolucion"].ToString();
                            lista.Add(pecosa);
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

        public static bool GrabarPecosa(Pecosa pecosa)
        {
            bool grabado = false;

            try
            {
                using (var cnn = SQLConexion.Conectar())
                {
                    cnn.Open();
                    var query = new SqlCommand("usp_GrabarPecosa", cnn);
                    query.CommandType = CommandType.StoredProcedure;
                    query.Parameters.Add(new SqlParameter("@CodigoPecosa", pecosa.Codigo));
                    query.Parameters.Add(new SqlParameter("@CodigoInfTec", pecosa.CodInforme));
                    query.Parameters.Add(new SqlParameter("@CodigoResBaja", pecosa.Resolucion));
                    query.Parameters.Add(new SqlParameter("@EstadoPecosa", pecosa.Estado));
                    
                    grabado = (query.ExecuteNonQuery() == 1);
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

            return grabado;
        }
    }
}
