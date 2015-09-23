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
    public class ActivoFijoDAL
    {
        #region Funciones Estaticas        
        public static bool InsertarActivoAInventario(string codigoInventario, string codigoActivo)
        {
            bool registrado = false;

            try
            {
                using (var cnn = SQLConexion.Conectar())
                {
                    cnn.Open();
                    var query = new SqlCommand("usp_InsertarActivoAInventario", cnn);
                    query.CommandType = CommandType.StoredProcedure;
                    query.Parameters.Add(new SqlParameter("@CodigoInventario", codigoInventario));
                    query.Parameters.Add(new SqlParameter("@CodigoActivo", codigoActivo));
                    query.ExecuteNonQuery();
                    registrado = true;
                }
            }
            catch (ArgumentException e)
            {
                throw new Exception(e.Message);
            }

            return registrado;
        }
        public static bool ActualizarActivoDeInventario(string codigoActivo, string codigoInventario, int evaluar)
        {
            bool actualizado = false;

            try
            {
                using (var cnn = SQLConexion.Conectar())
                {
                    cnn.Open();
                    var query = new SqlCommand("usp_ActualizarActivoDeInventario", cnn);
                    query.CommandType = CommandType.StoredProcedure;
                    query.Parameters.Add(new SqlParameter("@CodigoActivo", codigoActivo));
                    query.Parameters.Add(new SqlParameter("@CodigoInventario", codigoInventario));
                    query.Parameters.Add(new SqlParameter("@Evaluar", evaluar));
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
        public static bool EliminarActivoInventario(string codigoActivo, string codigoInventario)
        {
            bool eliminado = false;

            try
            {
                using (var cnn = SQLConexion.Conectar())
                {
                    cnn.Open();
                    var query = new SqlCommand("usp_EliminarActivoDeInventario", cnn);
                    query.CommandType = CommandType.StoredProcedure;
                    query.Parameters.Add(new SqlParameter("@CodigoActivo", codigoActivo));
                    query.Parameters.Add(new SqlParameter("@CodigoInventario", codigoInventario));
                    query.ExecuteNonQuery();
                    eliminado = true;
                }
            }
            catch (ArgumentException e)
            {
                throw new Exception(e.Message);
            }

            return eliminado;
        }
        public static ObservableCollection<ActivoFijo> ListarActivosDeMaestro()
        {
            var lista = new ObservableCollection<ActivoFijo>();

            try
            {
                using (var cnn = SQLConexion.Conectar())
                {
                    cnn.Open();
                    var query = new SqlCommand("usp_ListarActivosDeMaestro", cnn);
                    query.CommandType = CommandType.StoredProcedure;

                    using (var dr = query.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            var activo = new ActivoFijo()
                            {
                                CodigoActivo = dr["CodigoActivo"].ToString()
                            };
                            lista.Add(activo);
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
        public static ObservableCollection<ActivoFijo> ListarActivosDeInventario(string codigo)
        {
            var lista = new ObservableCollection<ActivoFijo>();

            try
            {
                using (var cnn = SQLConexion.Conectar())
                {
                    cnn.Open();
                    var query = new SqlCommand("usp_ListarActivosDeInventario", cnn);
                    query.CommandType = CommandType.StoredProcedure;
                    query.Parameters.Add(new SqlParameter("@Codigo", codigo));

                    using (var dr = query.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            var activo = new ActivoFijo
                            {
                                CodigoActivo = dr["CodigoActivo"].ToString(),
                                Decripcion = dr["Descripcion"].ToString(),
                                Marca = dr["Marca"].ToString(),
                                Modelo = dr["Modelo"].ToString(),
                                Tipo = dr["Tipo"].ToString(),
                                Sede = dr["Sede"].ToString(),
                                FechaAlta = Convert.ToDateTime(dr["FechaAlta"]),
                                Evaluar = Convert.ToInt32(dr["Evaluar?"]) == 1
                            };
                            lista.Add(activo);
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
        public static ObservableCollection<ActivoFijo> ListarActivosDeEvaluacion(string codigo)
        {
            var lista = new ObservableCollection<ActivoFijo>();

            try
            {
                using (var cnn = SQLConexion.Conectar())
                {
                    cnn.Open();
                    var query = new SqlCommand("usp_ListarActivosDeEvaluacion", cnn);
                    query.CommandType = CommandType.StoredProcedure;
                    query.Parameters.Add(new SqlParameter("@CODIGO", codigo));

                    using (var dr = query.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            var activo = new ActivoFijo
                            {
                                CodigoActivo = dr["CodigoActivo"].ToString(),
                                Decripcion = dr["Descripcion"].ToString(),
                                Marca = dr["Marca"].ToString(),
                                Modelo = dr["Modelo"].ToString(),
                                Tipo = dr["Tipo"].ToString(),
                                Sede = dr["Sede"].ToString(),
                                FechaAlta = Convert.ToDateTime(dr["FechaAlta"]),
                                CausalBaja = dr["CausalBaja"].ToString()
                            };
                            lista.Add(activo);
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
        public static ObservableCollection<ActivoFijo> ListarActivosDeInforme(string codigo)
        {
            var lista = new ObservableCollection<ActivoFijo>();

            try
            {
                using (var cnn = SQLConexion.Conectar())
                {
                    cnn.Open();
                    var query = new SqlCommand("usp_ListarActivosDeInforme", cnn);
                    query.CommandType = CommandType.StoredProcedure;
                    query.Parameters.Add(new SqlParameter("@CODIGO", codigo));

                    using (var dr = query.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            var activo = new ActivoFijo
                            {
                                CodigoActivo = dr["CodigoActivo"].ToString(),
                                Decripcion = dr["Descripcion"].ToString(),
                                Marca = dr["Marca"].ToString(),
                                Modelo = dr["Modelo"].ToString(),
                                Tipo = dr["Tipo"].ToString(),
                                Sede = dr["Sede"].ToString(),
                                FechaAlta = Convert.ToDateTime(dr["FechaAlta"]),
                                CausalBaja = dr["CausalBaja"].ToString(),
                                ValorLibros = dr["ValorLibros"].ToString(),
                                DeprecionAcumulada = dr["DeprecionAcumulada"].ToString()
                            };
                            lista.Add(activo);
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
        public static ObservableCollection<ActivoFijo> ListarActivosDePecosa(string codigo)
        {
            var lista = new ObservableCollection<ActivoFijo>();

            try
            {
                using (var cnn = SQLConexion.Conectar())
                {
                    cnn.Open();
                    var query = new SqlCommand("usp_ListarActivosDePecosa", cnn);
                    query.CommandType = CommandType.StoredProcedure;
                    query.Parameters.Add(new SqlParameter("@CODIGO", codigo));

                    using (var dr = query.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            var activo = new ActivoFijo
                            {
                                CodigoActivo = dr["CodigoActivo"].ToString(),
                                Decripcion = dr["Descripcion"].ToString(),
                                Marca = dr["Marca"].ToString(),
                                Modelo = dr["Modelo"].ToString(),
                                Tipo = dr["Tipo"].ToString(),
                                Sede = dr["Sede"].ToString(),
                                FechaAlta = Convert.ToDateTime(dr["FechaAlta"]),
                            };
                            lista.Add(activo);
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