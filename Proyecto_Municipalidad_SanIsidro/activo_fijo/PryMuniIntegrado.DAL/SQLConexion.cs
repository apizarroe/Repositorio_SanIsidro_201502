using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace PryMuniIntegrado.DAL
{
    public class SQLConexion
    {
        public static SqlConnection Conectar()
        {
            return new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["Cnx"].ConnectionString);
        }
    }
}
