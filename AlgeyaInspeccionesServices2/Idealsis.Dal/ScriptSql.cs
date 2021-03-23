using System;
using System.Text;
using System.Data.SqlClient;
using System.IO;

namespace Idealsis.Dal
{
    public class ScriptSql
    {

        private static string ObtenerTabla(string Texto)
        {
            string Result = "";
            Texto = Texto.Trim();
            if (Helper.Mid(Texto,0, 2) == "/*")
            {

                if (Helper.Mid(Texto,Texto.Length - 2) == "*/")
                {
                    Result = Helper.Mid(Texto,2, Texto.Length - 4);
                }
            }
            return Result;
        }

        public static bool Leer(SqlConnection cnn, string strFilePath)
        {
            SqlCommand Comand;
            string     NomTabla;
            string     incmd;
            string     batch="";
            bool       Agregar;
            string     cadSql;
            try
            {
                using (StreamReader Archivo = new StreamReader(@strFilePath, Encoding.UTF8))
                {
                    NomTabla = "";
                    Agregar = false;
                    while ((incmd = Archivo.ReadLine()) != null)
                    {
                        if (Helper.Mid(incmd,0, 2) == "/*")
                        {
                            Agregar = false;
                            NomTabla = ObtenerTabla(incmd).Trim();
                            if (NomTabla.Length > 0)
                            {
                                cadSql = "SELECT Count(*) as num FROM sysobjects WHERE name='" + NomTabla + "' and xtype='U'";
                                Comand = new SqlCommand(cadSql, (SqlConnection)cnn);
                                int count = (int)Comand.ExecuteScalar();
                                Comand.Dispose();
                                Agregar = count <= 0;
                            }
                        }

                        if (Agregar)
                        {

                            if (Helper.Mid(incmd,0, 2).ToUpper() != "GO")
                            {
                                if (Helper.Mid(incmd,0, 2) != "/*") batch = batch + " " + incmd;
                            }
                            else
                            {
                                if (batch != "")
                                {

                                    Comand = new SqlCommand(batch, (SqlConnection)cnn);
                                    Comand.ExecuteNonQuery();
                                    Comand.Dispose();
                                    batch = "";
                                }
                            }
                        }

                    }
                    Archivo.Close();
                    Archivo.Dispose();
                    return true;
                }
            }
            catch (Exception e)
            {
                //Helper.showError(e.Message + "\r\r" + fun.ParseMySql(Conexion.Proveedor, batch));
                Helper.showError(e.Message + "\r\r" + batch);
                //Console.WriteLine(e.Message);
                return false;
            }
        }

    }//clase
}//namespace
