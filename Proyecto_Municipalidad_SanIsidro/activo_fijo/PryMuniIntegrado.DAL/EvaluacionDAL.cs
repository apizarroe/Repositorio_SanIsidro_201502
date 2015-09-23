using PryMuniIntegrado.ET;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace PryMuniIntegrado.DAL
{
    public class EvaluacionDAL
    {
        public static ObservableCollection<Evaluacion> ListarEvaluacion()
        {
            var lista = new ObservableCollection<Evaluacion>();

            try
            {
                using (var cnn = SQLConexion.Conectar())
                {
                    cnn.Open();
                    var query = new SqlCommand("usp_ListarEvaluacion", cnn);
                    query.CommandType = CommandType.StoredProcedure;
                    
                    using (var dr = query.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            var date = new DateTime();
                            var evaluacion = new Evaluacion();
                            evaluacion.Codigo = dr["Codigo"].ToString();
                            evaluacion.Estado = (EEstado)Enum.Parse(typeof(EEstado), dr["Estado"].ToString());
                            evaluacion.Responsable = dr["Responsable"].ToString();
                            DateTime.TryParse(dr["FechaRegistro"].ToString(), out date);
                            evaluacion.FechaRegistro = date;
                            evaluacion.CodInventario = dr["CodInventario"].ToString();

                            lista.Add(evaluacion);
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
        public static ObservableCollection<Evaluacion> ListarEvaluacionPorCodigo(string codigo)
        {
            var lista = new ObservableCollection<Evaluacion>();

            try
            {
                using (var cnn = SQLConexion.Conectar())
                {
                    cnn.Open();
                    var query = new SqlCommand("usp_ListarEvaluacionPorCodigo", cnn);
                    query.CommandType = CommandType.StoredProcedure;
                    query.Parameters.Add(new SqlParameter("@CODIGO", codigo));
                    using (var dr = query.ExecuteReader())
                    {   
                        while (dr.Read())
                        {
                            var date = new DateTime();
                            var evaluacion = new Evaluacion();
                            evaluacion.Codigo = dr["Codigo"].ToString();
                            evaluacion.Estado = (EEstado)Enum.Parse(typeof(EEstado), dr["Estado"].ToString());
                            evaluacion.Responsable = dr["Responsable"].ToString();
                            DateTime.TryParse(dr["FechaRegistro"].ToString(), out date);
                            evaluacion.FechaRegistro = date;
                            evaluacion.CodInventario = dr["CodInventario"].ToString();

                            lista.Add(evaluacion);
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
        public static Evaluacion ObtenerEvaluacion(string codigo)
        {
            Evaluacion evaluacion = null;
            try
            {
                using (var cnn = SQLConexion.Conectar())
                {
                    var query = new SqlCommand("usp_ObtenerEvaluacion", cnn);
                    query.CommandType = CommandType.StoredProcedure;
                    query.Connection.Open();

                    query.Parameters.Add(new SqlParameter("@CODIGO", codigo));
                    using (var dr = query.ExecuteReader())
                    {
                        dr.Read();
                        if (dr.HasRows)
                        {
                            var date = new DateTime();
                            evaluacion = new Evaluacion();
                            evaluacion.Codigo = dr["Codigo"].ToString();
                            evaluacion.Estado = (EEstado)Enum.Parse(typeof(EEstado), dr["Estado"].ToString());
                            evaluacion.Responsable = dr["Responsable"].ToString();
                            DateTime.TryParse(dr["FechaRegistro"].ToString(), out date);
                            evaluacion.FechaRegistro = date;
                            evaluacion.CodInventario = dr["CodInventario"].ToString();
                        }
                    }
                }
            }
            catch (SqlException e)
            {
                throw new Exception(e.Message);
            }

            return evaluacion;
        }
    }
}
