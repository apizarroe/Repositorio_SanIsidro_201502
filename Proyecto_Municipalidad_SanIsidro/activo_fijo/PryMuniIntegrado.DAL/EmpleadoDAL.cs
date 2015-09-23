using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

using PryMuniIntegrado.ET;

namespace PryMuniIntegrado.DAL
{
    public class EmpleadoDAL
    {
        public static ObservableCollection<Empleado> ListarEmpleadoDeComite(string codigo)
        {
            var lista = new ObservableCollection<Empleado>();

            try
            {
                using (var cnn = SQLConexion.Conectar())
                {
                    cnn.Open();
                    var query = new SqlCommand("usp_ListarEmpleadoPorComite", cnn);
                    query.CommandType = CommandType.StoredProcedure;
                    query.Parameters.Add(new SqlParameter("@Codigo", codigo));

                    using (var dr = query.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            var empleado = new Empleado
                            {
                                Codigo = dr["Codigo"].ToString(),
                            };
                            lista.Add(empleado);
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
    }
}
