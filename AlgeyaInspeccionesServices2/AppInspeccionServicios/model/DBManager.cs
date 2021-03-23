
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.Data.SqlClient;
using System.Configuration;
using System.Data;

namespace AppInspeccionServicios.model
{
    public class BDmanager
    {
        private String conSQL;
        public BDmanager()
        {
            conSQL = ConfigurationManager.ConnectionStrings["connSQL"].ToString();
            //conSQL = System.Configuration.ConfigurationManager.AppSettings["CadenaDeConexion"].ToString();
        }
        public SqlDataReader getReader(string sp, Dictionary<string, object> parametros)
        {
            SqlConnection conn = new SqlConnection(conSQL);
            conn.Open();
            SqlCommand comm = conn.CreateCommand();
            comm.CommandType = CommandType.StoredProcedure;
            comm.CommandText = sp;
            if (parametros != null)
            {
                foreach (KeyValuePair<string, object> kvp in parametros)
                    comm.Parameters.Add(new SqlParameter(kvp.Key, kvp.Value));
            }
            return comm.ExecuteReader(System.Data.CommandBehavior.CloseConnection);
        }

        public int getEscalar(string sp, Dictionary<string, object> parametros)
        {
            int result = 0;
            using (SqlConnection con = new SqlConnection(conSQL))
            {
                using (SqlCommand comm = new SqlCommand(sp, con))
                {
                    comm.CommandType = CommandType.StoredProcedure;
                    if (parametros != null)
                    {
                        foreach (KeyValuePair<string, object> kvp in parametros)
                            comm.Parameters.Add(new SqlParameter(kvp.Key, kvp.Value));
                    }
                    con.Open();
                    result = (int)comm.ExecuteScalar();
                    con.Close();
                }
            }
            return result;
        }

        public void getEscalarVoid(string sp, Dictionary<string, object> parametros)
        {
            using (SqlConnection con = new SqlConnection(conSQL))
            {
                using (SqlCommand comm = new SqlCommand(sp, con))
                {
                    comm.CommandType = CommandType.StoredProcedure;
                    if (parametros != null)
                    {
                        foreach (KeyValuePair<string, object> kvp in parametros)
                            comm.Parameters.Add(new SqlParameter(kvp.Key, kvp.Value));
                    }
                    con.Open();
                    comm.ExecuteScalar();
                    con.Close();
                }
            }
        }

        public string getEscalarString(string sp, Dictionary<string, object> parametros)
        {
            string result = "";
            using (SqlConnection con = new SqlConnection(conSQL))
            {
                using (SqlCommand comm = new SqlCommand(sp, con))
                {
                    comm.CommandType = CommandType.StoredProcedure;
                    if (parametros != null)
                    {
                        foreach (KeyValuePair<string, object> kvp in parametros)
                            comm.Parameters.Add(new SqlParameter(kvp.Key, kvp.Value));
                    }
                    con.Open();
                    result = (string)comm.ExecuteScalar();
                    con.Close();
                }
            }
            return result;
        }
    }
}