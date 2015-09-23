using PryMuniIntegrado.ET;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace PryMuniIntegrado.DAL
{
    public class UsuarioDAL
    {
        public static bool ValidarUsuario(string usuario, string contraseña)
        {
            bool autenticado = false;

            try
            {                
                using (var cnn = SQLConexion.Conectar())
                {
                    var query = new SqlCommand("usp_ValidarUsuario", cnn);
                    query.CommandType = CommandType.StoredProcedure;
                    query.Connection.Open();

                    query.Parameters.Add(new SqlParameter("@Usuario", usuario));
                    query.Parameters.Add(new SqlParameter("@Contraseña", contraseña));

                    using (var dr = query.ExecuteReader())
                    {
                        dr.Read();
                        if (dr.HasRows)
                        {
                            autenticado = (Convert.ToInt32(dr["Usuario"]) == 1);
                        }
                    }
                }
            }
            catch(ArgumentException e)
            {
                throw new Exception(e.Message);
            }

            return autenticado;
        }
        public static Usuario ObtenerUsuario(string noUsuario)
        {
            Usuario usuario = null;
            try
            {
                using (var cnn = SQLConexion.Conectar())
                {
                    var query = new SqlCommand("usp_ObtenerUsuario", cnn);
                    query.CommandType = CommandType.StoredProcedure;
                    query.Connection.Open();

                    query.Parameters.Add(new SqlParameter("@noUsuario", noUsuario));
                    using (var dr = query.ExecuteReader())
                    {
                        dr.Read();
                        if (dr.HasRows)
                        {
                            usuario = new Usuario
                            {
                                NombreUsuario = dr["Usuario"].ToString(),
                                Contraseña = dr["Contraseña"].ToString(),
                                Roles = new String[] { dr["Rol"].ToString() }
                            };
                        }
                    }
                }
            }
            catch (SqlException e)
            {
                throw new Exception(e.Message);
            }

            return usuario;
        }
    }
}
