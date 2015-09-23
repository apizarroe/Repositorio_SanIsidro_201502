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
    public class InventarioDAL
    {
        #region Funciones Estaticas
        public static ObservableCollection<Inventario> ListarInventario()
        {
            var lista = new ObservableCollection<Inventario>();

            try
            {
                using (var cnn = SQLConexion.Conectar())
                {
                    cnn.Open();
                    var query = new SqlCommand("usp_ListarInventario", cnn);
                    query.CommandType = CommandType.StoredProcedure;
                    
                    using (var dr = query.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            var date = new DateTime();
                            var inventario = new Inventario();
                            inventario.Codigo = dr["Codigo"].ToString();
                            inventario.Comite = dr["Comite"].ToString();
                            inventario.Periodo = Convert.ToInt32(dr["Periodo"]);
                            inventario.Estado = (EEstado)Enum.Parse(typeof(EEstado), dr["Estado"].ToString());
                            DateTime.TryParse(dr["InicioProgramado"].ToString(), out date);
                            inventario.InicioProgramado = date;
                            DateTime.TryParse(dr["InicioReal"].ToString(), out date);
                            inventario.InicioReal = date;
                            DateTime.TryParse(dr["FinalReal"].ToString(), out date);
                            inventario.FinalReal = date;

                            lista.Add(inventario);
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
        public static ObservableCollection<Inventario> ListarInventarioPorEstadoAño(int cantidad, int periodo, string estado)
        {
            var lista = new ObservableCollection<Inventario>();

            try
            {
                using (var cnn = SQLConexion.Conectar())
                {
                    cnn.Open();
                    var query = new SqlCommand("usp_ListarInventarioPorEstadoAño", cnn);
                    query.CommandType = CommandType.StoredProcedure;
                    query.Parameters.Add(new SqlParameter("@Cant", cantidad));
                    query.Parameters.Add(new SqlParameter("@Periodo", periodo));
                    query.Parameters.Add(new SqlParameter("@Estado", estado));

                    using (var dr = query.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            var date = new DateTime();
                            var inventario = new Inventario();
                            inventario.Codigo = dr["Codigo"].ToString();
                            inventario.Comite = dr["Comite"].ToString();
                            inventario.Periodo = Convert.ToInt32(dr["Periodo"]);
                            inventario.Estado = (EEstado)Enum.Parse(typeof(EEstado), dr["Estado"].ToString());                            
                            DateTime.TryParse(dr["InicioProgramado"].ToString(), out date);
                            inventario.InicioProgramado = date;
                            DateTime.TryParse(dr["InicioReal"].ToString(), out date);
                            inventario.InicioReal = date;
                            DateTime.TryParse(dr["FinalReal"].ToString(), out date);
                            inventario.FinalReal = date;                            
                            lista.Add(inventario);
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
        public static bool GrabarInventario(string codigo, EEstado estado, DateTime fechaInicioReal)
        {
            bool actualizado = false;

            try
            {
                using (var cnn = SQLConexion.Conectar())
                {
                    cnn.Open();
                    var query = new SqlCommand("usp_GrabarInventario", cnn);
                    query.CommandType = CommandType.StoredProcedure;
                    query.Parameters.Add(new SqlParameter("@Codigo", codigo));
                    query.Parameters.Add(new SqlParameter("@Estado", string.Format("{0}" ,estado)));
                    query.Parameters.Add(new SqlParameter("@FechaInicio", fechaInicioReal));
                    query.ExecuteNonQuery();
                    actualizado = true;
                }
            }
            catch (ArgumentException e)
            {
                throw new Exception(e.Message);
            }

            return actualizado;
        }
        public static Inventario ObtenerInventario(string codigo)
        {
            Inventario inventario = null;
            try
            {
                using (var cnn = SQLConexion.Conectar())
                {
                    var query = new SqlCommand("usp_ObtenerInventario", cnn);
                    query.CommandType = CommandType.StoredProcedure;
                    query.Connection.Open();

                    query.Parameters.Add(new SqlParameter("@Codigo", codigo));
                    using (var dr = query.ExecuteReader())
                    {
                        dr.Read();
                        if (dr.HasRows)
                        {
                            var date = new DateTime();
                            inventario = new Inventario();
                            inventario.Codigo = dr["Codigo"].ToString();
                            inventario.Comite = dr["Comite"].ToString();
                            inventario.Periodo = Convert.ToInt32(dr["Periodo"]);
                            inventario.Estado = (EEstado)Enum.Parse(typeof(EEstado), dr["Estado"].ToString());
                            DateTime.TryParse(dr["InicioProgramado"].ToString(), out date);
                            inventario.InicioProgramado = date;
                            DateTime.TryParse(dr["InicioReal"].ToString(), out date);
                            inventario.InicioReal = date;
                            DateTime.TryParse(dr["FinalReal"].ToString(), out date);
                            inventario.FinalReal = date;
                        }
                    }
                }
            }
            catch (SqlException e)
            {
                throw new Exception(e.Message);
            }

            return inventario;
        }
        #endregion
    }
}
