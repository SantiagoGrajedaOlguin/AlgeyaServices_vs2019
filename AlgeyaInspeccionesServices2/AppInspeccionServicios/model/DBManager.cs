
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.Data.SqlClient;
using System.Configuration;
using System.Data;
using System.Web.Script.Serialization;

namespace AppInspeccionServicios.model
{
    public class BDmanager
    {
        private String cadenaDeConexion;
        public BDmanager()
        {
            cadenaDeConexion = ConfigurationManager.ConnectionStrings["cadenaDeConexionSql"].ToString();

        }
        public String getJsonList(string nombreProcedimientoAlmacenado, Dictionary<string, object> parametros)
        {
            String result = "";
            using (SqlConnection conexion = new SqlConnection(cadenaDeConexion))
            {
                using (SqlCommand comando = new SqlCommand(nombreProcedimientoAlmacenado, conexion))
                {
                    comando.CommandType = CommandType.StoredProcedure;
                    if (parametros != null)
                    {
                        foreach (KeyValuePair<string, object> parametro in parametros)
                            comando.Parameters.Add(new SqlParameter(parametro.Key, parametro.Value));
                    }
                    conexion.Open();
                    SqlDataReader dataReaderResult = comando.ExecuteReader(CommandBehavior.CloseConnection);
                    result = new JavaScriptSerializer().Serialize(HelperJson.Serialize(dataReaderResult));
                    dataReaderResult.Close();
                    conexion.Close();
                }
            }
            return result;
        }

        public String[] getMultipleJsonList(string nombreProcedimientoAlmacenado, Dictionary<string, object> parametros)
        {
            String[] result = { "", "", "", "", "","","","","","" };
            using (SqlConnection conexion = new SqlConnection(cadenaDeConexion))
            {
                using (SqlCommand comando = new SqlCommand(nombreProcedimientoAlmacenado, conexion))
                {
                    comando.CommandType = CommandType.StoredProcedure;
                    if (parametros != null)
                    {
                        foreach (KeyValuePair<string, object> parametro in parametros)
                            comando.Parameters.Add(new SqlParameter(parametro.Key, parametro.Value));
                    }
                    conexion.Open();

                    //Cuerpo
                    SqlDataReader dataReaderResult = comando.ExecuteReader(CommandBehavior.CloseConnection);
                    result[0] = new JavaScriptSerializer().Serialize(HelperJson.Serialize(dataReaderResult));
                    //Detalle
                    dataReaderResult.NextResult();
                    result[1] = new JavaScriptSerializer().Serialize(HelperJson.Serialize(dataReaderResult));
                    //Bodegas
                    dataReaderResult.NextResult();
                    result[2] = new JavaScriptSerializer().Serialize(HelperJson.Serialize(dataReaderResult));
                    //Bodeguero
                    dataReaderResult.NextResult();
                    result[3] = new JavaScriptSerializer().Serialize(HelperJson.Serialize(dataReaderResult));
                    //Bodegas Internas
                    dataReaderResult.NextResult();
                    result[4] = new JavaScriptSerializer().Serialize(HelperJson.Serialize(dataReaderResult));

                    //Calidades
                    //Articulos
                    //Observaciones
                    //Observaciones detalle
                    //Resultados

                    dataReaderResult.Close();
                    conexion.Close();
                }
            }
            return result;
        }

        public SqlDataReader getReader(string sp, Dictionary<string, object> parametros)
        {
            SqlConnection conn = new SqlConnection(cadenaDeConexion);
            conn.Open();
            SqlCommand comm = conn.CreateCommand();
            comm.CommandType = CommandType.StoredProcedure;
            comm.CommandText = sp;
            if (parametros != null)
            {
                foreach (KeyValuePair<string, object> kvp in parametros)
                    comm.Parameters.Add(new SqlParameter(kvp.Key, kvp.Value));
            }
            return comm.ExecuteReader(CommandBehavior.CloseConnection);
        }

        public int getEscalar(string sp, Dictionary<string, object> parametros)
        {
            int result = 0;
            using (SqlConnection con = new SqlConnection(cadenaDeConexion))
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
            using (SqlConnection con = new SqlConnection(cadenaDeConexion))
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
            using (SqlConnection con = new SqlConnection(cadenaDeConexion))
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