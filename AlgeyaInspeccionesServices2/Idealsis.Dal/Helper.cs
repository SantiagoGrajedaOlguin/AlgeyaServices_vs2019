using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Drawing;
using System.Drawing.Imaging;
using System.Windows.Forms;
using Microsoft.Win32;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Idealsis.Dal
{
    public class Helper
    {

        public static string vg_DAT = "";
        public static string vg_REG = "";
        public static string vg_MOD = "";

        public static string ValidarAcentos(string Cadena)
        {
            Encoding iso = Encoding.GetEncoding("ISO-8859-1");
            Encoding utf8 = Encoding.UTF8;
            byte[] utfBytes = utf8.GetBytes(Cadena);
            byte[] isoBytes = Encoding.Convert(utf8, iso, utfBytes);
            return iso.GetString(isoBytes);
        }
        public static string ValidarJValor(string Cadena)
        {
            if (Cadena.IndexOf('"') >= 0)
            {
                Cadena = Cadena.Replace("\"", "\\\"");
            }
            return Cadena;
        }


        public static bool existeArchivo(string vp_fil)
        {
            try
            {

                return File.Exists(vp_fil);
            }

            catch (Exception ex) { Console.WriteLine(ex.Message); }
            return false;

        }

        public static bool ExisteDir(string vp_dir)
        {
            try
            {

                return Directory.Exists(vp_dir);
            }

            catch (Exception ex) { Console.WriteLine(ex.Message); }
            return false;
        }

        public static string ReplaceVb6(string Cadena, string oldValue, string newValue, int startIndex, int count)
        {
            var sCadena = new System.Text.StringBuilder(Cadena);
            sCadena.Replace(oldValue, newValue, startIndex, count);
            return sCadena.ToString();
        }


        /// <summary>
        /// Funcion para determinar si el valor indicado es numerico o no.
        /// </summary>
        /// <param name="obj"> Valor que se desea validar.</param>
        public static bool isNumeric(object obj)
        {
            try
            {
                double x = Convert.ToDouble(obj);
                return true;
            }
            catch (Exception ex) { Console.WriteLine(ex.Message); }
            return false;
        }


        /// <summary>
        /// Funcion para determinar si el valor indicado es fecha o no.
        /// </summary>
        /// <param name="obj"> Valor que se desea validar.</param>
        public static bool isDate(object obj)
        {
            try
            {
                DateTime x = Convert.ToDateTime(obj);
                return true;
            }
            catch (Exception ex) { Console.WriteLine(ex.Message); }
            return false;
        }
        public static bool isTime(object obj)
        {
            try
            {
                DateTime x = Convert.ToDateTime(obj);
                return true;
            }
            catch (Exception ex) { Console.WriteLine(ex.Message); }
            return false;
        }

        /// <summary>
        /// Funcion para determinar si el valor BOOL es NULO y devolver un dato valido.
        /// </summary>
        /// <param name="Valor"> Valor que se desea validar.</param>
        public static bool BNull(Object Valor)
        {
            if (Convert.IsDBNull(Valor))
                return false;
            else
                return (bool)Valor;
        }

        /// <summary>
        /// Funcion para determinar si el valor NUMERICO es NULO y devolver un dato valido.
        /// </summary>
        /// <param name="Valor"> Valor que se desea validar.</param>
        public static Object VNull(Object Valor)
        {
            if (Convert.IsDBNull(Valor))
                return 0;
            else
                return Valor;
        }

        /// <summary>
        /// Funcion para determinar si el valor STRING es NULO y devolver un dato valido.
        /// </summary>
        /// <param name="Valor"> Valor que se desea validar.</param>
        public static string CNull(Object Valor)
        {
            if (Convert.IsDBNull(Valor))
                return "";
            else
                return (string)Valor;

        }
        public static float TextWidth(string text, Font f)
        {
            // define context used for determining glyph metrics.        
            Bitmap bitmap = new Bitmap(1, 1);
            Graphics grfx = Graphics.FromImage(bitmap);

            // determine width         
            SizeF bounds = grfx.MeasureString(text, f);
            return bounds.Width;
        }

        public static string ParseMySql(int proveedor, string comandoSql)
        {
            string cad = comandoSql;
            string nam = "";
            string par = "";
            int pos = 0;
            int ini = 0;
            int fin = 0;
            if (proveedor == 1)//convertir a MySql
            {

                cad = cad.Replace("len(", "LENGTH(");
                cad = cad.Replace("LEN(", "LENGTH(");
                cad = cad.Replace("[DBO].", "");
                cad = cad.Replace("[dbo].", "");
                cad = cad.Replace("image", "mediumblob");
                cad = cad.Replace("IMAGE", "mediumblob");
                cad = cad.Replace("smallmoney", "DECIMAL(9,4)");
                cad = cad.Replace("SMALLMONEY", "DECIMAL(9,4)");
                cad = cad.Replace("money", "DECIMAL(19,4)");
                cad = cad.Replace("MONEY", "DECIMAL(19,4)");

                //cambiar sintaxis de primary key
                ini = cad.IndexOf("CONSTRAINT");
                if (ini >= 0)
                {
                    fin = cad.IndexOf("PRIMARY KEY");
                    if (fin >= 0)
                    {
                        par = cad.Substring(ini, fin - ini);
                        cad = cad.Replace(par, " ");
                    }

                }
                cad = cad.Replace("CLUSTERED ", "");

                //validar indice
                pos = cad.IndexOf("CREATE TABLE");
                if (pos >= 0)
                {
                    cad = cad.Replace("  ", " ");
                    ini = cad.IndexOf('[', pos + 12);
                    if (ini > 0)
                    {
                        fin = cad.IndexOf(']', ini);
                        if (fin > ini)
                        {
                            nam = cad.Substring(ini + 1, fin - (ini + 1));
                        }
                    }

                    cad = cad.Replace("CREATE TABLE ", "CREATE TABLE IF NOT EXISTS ");
                    cad = cad.Replace("COLLATE SQL_Latin1_General_CP1_CI_AS ", "");
                    cad = cad.Replace("COLLATE SQL_LATIN1_GENERAL_CP1_CI_AS ", "");
                    cad = cad.Replace(") NULL", ") DEFAULT NULL");
                    cad = cad + " ENGINE=InnoDB DEFAULT CHARSET=latin1;";

                }
                //mediumblob
                cad = cad.Replace('[', '`');
                cad = cad.Replace(']', '`');

            }
            //Clipboard.Clear();
            //Clipboard.SetText(cad);

            return cad;

        }

        #region Funciones para aceptar tipos de caracter en textbox

        public static bool LetrasYCaracteresParaNombreYApeido(string str)
        {
            System.Text.RegularExpressions.Regex reg = new System.Text.RegularExpressions.Regex("[^a-zA-Z,ñ,Ñ, ]");
            return reg.IsMatch(str);
        }
        public static bool ValidacionCalleNumeroColonia(string str)
        {
            System.Text.RegularExpressions.Regex reg = new System.Text.RegularExpressions.Regex("[^a-zA-Z,0-9,.,ñ,Ñ,:,,,-,/, ]");
            return reg.IsMatch(str);
        }
        public static bool SoloLetras(string str)
        {
            System.Text.RegularExpressions.Regex reg = new System.Text.RegularExpressions.Regex("[^a-zA-ZñÑáéíóúÁÉÍÓÚ,] * $");
            return reg.IsMatch(str);
        }
        public static bool SoloNumeros(string str)
        {
            System.Text.RegularExpressions.Regex reg = new System.Text.RegularExpressions.Regex("[^0-9]");
            return reg.IsMatch(str);
        }
        public static bool SoloNumerosyElPunto(string str)
        {
            System.Text.RegularExpressions.Regex reg = new System.Text.RegularExpressions.Regex("[^0-9,.]");
            return reg.IsMatch(str);
        }
        public static bool ContieneSoloLetrasyNumeros(string palabra)
        {
            System.Text.RegularExpressions.Regex reg = new System.Text.RegularExpressions.Regex("[^a-zA-ZñÑ0-9]");
            return reg.IsMatch(palabra);

        }
        #endregion

        /// <summary>
        /// Funcion que arroja la seccion de una cadena dividida por comas
        /// </summary>
        /// <param name="cad"> Cadena de parametros.</param>
        /// <param name="delim"> Caracter de separacion.</param>
        /// <param name="nsec"> Numero de seccion o parte a cortar.</param>
        public static string CutStr(string cad, string delim, int nsec)
        {
            int ini = 0;     // Posicion inicial de la seccion
            int fin = -1;    // Posicion final de la seccion
            int par = 0;     // Numero de parte
            string res = "";

            if (cad != null)
            {
                if (cad.Length > 0 && delim.Length > 0 && nsec > 0)
                {
                    do
                    {
                        ini = fin + 1;
                        fin = cad.IndexOf(delim, ini);
                        par = par + 1;
                        if (fin < 0)
                        {
                            if (par < nsec)
                            {
                                ini = 0;
                                fin = 0;
                                par = nsec;
                            }
                            else
                            {
                                fin = cad.Length;
                            }
                        }
                    }
                    while (par < nsec);
                    if (delim.Length > 1 && nsec > 1 && fin > ini) ini = ini + delim.Length - 1;
                    if (fin > ini) res = cad.Substring(ini, fin - ini);
                }
            }
            return res;
        }

        public static string CutStr(string cad, string delim, int nsec, string Default)
        {
            int ini = 0;     // Posicion inicial de la seccion
            int fin = -1;    // Posicion final de la seccion
            int par = 0;     // Numero de parte
            string res = "";

            if (cad.Length > 0 && delim.Length > 0 && nsec > 0)
            {
                do
                {
                    ini = fin + 1;
                    fin = cad.IndexOf(delim, ini);
                    par = par + 1;
                    if (fin < 0)
                    {
                        if (par < nsec)
                        {
                            ini = 0;
                            fin = 0;
                            par = nsec;
                        }
                        else
                        {
                            fin = cad.Length;
                        }
                    }
                }
                while (par < nsec);
                if (delim.Length > 1 && nsec > 1 && fin > ini) ini = ini + delim.Length - 1;
                if (fin > ini) res = cad.Substring(ini, fin - ini);
            }
            if (res == "") res = Default;
            return res;
        }

        /// <summary>
        /// Funcion que arroja el numero de secciones de una cadena dividida por un caracter especial
        /// </summary>
        /// <param name="cad"> Cadena de parametros.</param>
        /// <param name="delim"> Caracter de separacion.</param>
        public static int numStr(string cad, string delim)
        {
            int ini = 0;  // posicion inicial de la seccion
            int fin = 0;  // posicion final de la seccion
            int num = 0;  // numero de seccion

            if (cad.Length > 0 && delim.Length > 0)
            {
                do
                {
                    ini = fin + 1;
                    fin = cad.IndexOf(delim, ini);
                    num += 1;
                }
                while (ini < fin);
            }
            return num;
        }


        /// <summary>
        /// Convierte la fecha en formato SQL valido segun el proveedor de datos
        /// </summary>
        /// <param name="vFecha"> Fecha a convertir.</param>
        public static string DateSQL(string vFecha)
        {
            string result = "null";
            int provDat = 1;

            if (isDate(vFecha))
            {
                if (provDat == 1)
                    result = "CONVERT(DATETIME, '" + String.Format("{0:yyyy/MM/dd}", Convert.ToDateTime(vFecha)) + " 00:00:00', 102)";
                else
                    result = "#" + String.Format("{0:yyyy/MM/dd}", vFecha) + "#";
            }
            return result;
        }

        public static string DTSQL(string vFecha)
        {
            string result = "null";
            int provDat = 1;

            if (isDate(vFecha))
            {
                if (provDat == 1)
                    result = "CONVERT(DATETIME, '" + String.Format("{0:yyyy/MM/dd}", vFecha) + " " + String.Format("{0:HH:mm:ss}", vFecha) + "', 102)";
                else
                    result = "#" + String.Format("{0:yyyy/MM/dd}", vFecha) + " " + String.Format("{0:HH:mm:ss}", vFecha) + "#";
            }
            return result;
        }

        /// <summary>
        /// Convierte la hora en formato SQL valido segun el proveedor de datos
        /// </summary>
        /// <param name="vHora"> Hora a convertir.</param>
        public static string TimeSQL(string vHora)
        {
            string result = "null";
            int provDat = 1;
            if (isDate(vHora))
            {
                if (provDat == 1)
                    result = "CONVERT(DATETIME, '1899-12-30 " + String.Format("{0:HH:mm:ss}", vHora) + "', 102)";
                else
                    result = "#" + String.Format("{0:hh:mm:ss tt}", vHora) + "#";
            }
            return result;
        }
        public static string GenerarKey(string RazonSocial)
        {
            int i;
            int ncar;  //numero total de caracteres
            float ntot = 0;   //suma total de caracteres
            int nesp = 0;  //numero de espacios
            int nvoc = 0;  //numero de vocales
            int ncon = 0;  //numero de consonantes
            int nPos = 0;
            int ccon = 0;
            string vp_cve = "";
            //int vp_nPos = 0;
            int n;


            RazonSocial = RazonSocial.ToUpper();
            ncar = RazonSocial.Length;
            for (i = 0; i < ncar; i++)
            {
                ntot = ntot + Helper.Val(String.Format("{0:000}", (Convert.ToInt32(RazonSocial.Substring(i, 1)) * i)).Substring(0, 3));
                if (Mid(RazonSocial, i, 1) == " ")
                {
                    nesp = nesp + 1;
                    nPos = nPos + i;
                }
                else if (EsVocal(Mid(RazonSocial, i, 1)))
                {
                    nvoc = nvoc + 1;
                }
                else
                {
                    ncon = ncon + 1;
                }
            }
            vp_cve = String.Format("{0:00}", ncon).Substring(0, 2) + String.Format("{0:00}", nvoc).Substring(0, 2) + String.Format("{0:00}", nesp).Substring(0, 2) + String.Format("{0:00}", ncar).Substring(0, 2) + String.Format("{0:00000}", ntot).Substring(0, 5) + String.Format("{0:00}", nPos).Substring(0, 2);

            if (vg_MOD.Length > ncar)
                n = vg_MOD.Length;
            else
                n = ncar;
            ncon = vg_MOD.Length;
            for (i=0;i<ncon; i++)
            {
                if (ccon > vp_cve.Length) break;
                nPos = Val(Mid(vp_cve, i, 1)) + Val(Mid(vg_MOD, i, 1));
                //if (nPos > 9) vp_nPos = 9;
                vp_cve = (i > 1 ? Mid(vp_cve, 1, i - 1) : "") + nPos + Mid(vp_cve, i + 1, n - i);
            }
            return vp_cve;

        }

        private static bool EsVocal(string car)
        {
            bool Result = false;
            car = car.ToUpper();
            if (car == "A" || car == "E" || car == "I" || car == "O" || car == "U" || car == "Á" || car == "É" || car == "Í" || car == "Ó" || car == "Ú") Result = true;
            return Result;
        }

        /// <summary>
        /// Convierte la cadena en formato SQL valido segun el proveedor de datos
        /// </summary>
        /// <param name="cad"> Cadena a convertir.</param>
        public static string strSql(string cad)
        {
            cad = cad.Replace("'", "''");
            return "'" + cad + "'";
        }

        /// <summary>
        /// Convierte la cadena en formato SQL valido segun el proveedor de datos
        /// </summary>
        /// <param name="cad"> Cadena a convertir.</param>
        public static string nSt(string cad)
        {
            cad = cad.Replace("'", "''");
            return "'%" + cad + "%'";
        }

        /// <summary>
        /// Valida la cadena para permitir solo numeros
        /// </summary>
        /// <param name="vp_txt"> Cadena a validar.</param>
        /// <param name="vp_car"> Caracter capturado.</param>
        /// <param name="vp_ent"> Numero de enteros permitidos.</param>
        /// <param name="vp_dec"> Numero de decimales permitidas.</param>
        /// <param name="vp_spos"> Posición actual del cursor.</param>
        /// <param name="vp_slen"> Numero de caracteres seleccionados.</param>

        public static Image ByteArrayToImage(byte[] byteArrayIn)
        {
            MemoryStream ms = new MemoryStream(byteArrayIn);
            return Image.FromStream(ms);
        }

        public static byte[] ImageToByteArray(Image imageIn)
        {
            MemoryStream ms = new MemoryStream();
            imageIn.Save(ms, ImageFormat.Jpeg);
            return ms.ToArray();
        }

        public static void showError(string msg)
        {
            //MessageBox.Show(msg, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            Console.WriteLine(msg);
        }

        /// <summary>
        /// Muestra mensaje con una pregunta y devuleve la respuesta
        /// </summary>
        /// <param name="msg"> Mensaje que se desea desplegar.</param>
        public static bool showPregunta(string msg)
        {
            DialogResult res;
            res = MessageBox.Show(msg, Application.ProductName, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            return res == DialogResult.Yes;
        }

        /// <summary>
        /// Muestra mensaje de aviso
        /// </summary>
        /// <param name="msg"> Mensaje que se desea desplegar.</param>
        public static void showAviso(string msg)
        {
            //MessageBox.Show(msg, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        public static string GetSetting(string appName, string section, string key, string sDefault)
        {
            // Los datos de VB se guardan en:
            // HKEY_CURRENT_USER\Software\VB and VBA Program Settings
            RegistryKey rk = Registry.CurrentUser.OpenSubKey(@"Software\VB and VBA Program Settings\" +
                                                              appName + "\\" + section);
            string s = sDefault;
            if (rk != null)
                s = (string)rk.GetValue(key);
            //
            return s;
        }
        public static string GetSetting(string appName, string section, string key)
        {
            return GetSetting(appName, section, key, "");
        }
        public static void SaveSetting(string appName, string section, string key, string setting)
        {
            // Los datos de VB se guardan en:
            // HKEY_CURRENT_USER\Software\VB and VBA Program Settings
            RegistryKey rk = Registry.CurrentUser.CreateSubKey(@"Software\VB and VBA Program Settings\" +
                                                                appName + "\\" + section);
            rk.SetValue(key, setting);
        }
        public static int Val(string Valor)
        {
            if (Valor == "")
                return 0;
            else if (!isNumeric(Valor))
                return 0;
            else if (Valor.IndexOf(".")>=0)
                return Convert.ToInt32(CutStr(Valor,".",1));
            else
            {
                return Convert.ToInt32(Valor);
            }
        }
        public static string ValNumSave(string Valor)
        {
            if (Valor == "")
                return "0";
            else if (!isNumeric(Valor))
                return "0";
            else
                return Valor;
        }
        public static void validarJSON(string strJSON, ref JObject objJSON)
        {
            if (strJSON.Length > 0)
            {
                //validar resultado
                objJSON = (JObject)JsonConvert.DeserializeObject(strJSON);
                
                //objJSON = Json.parse(strJSON);
                if (objJSON != null)
                {
                    //if (Json.GetParserErrors() != "")
                    //{
                    //    objJSON = null;
                    //}
                }
            }
        }
        public static string Mid(string Cadena,int Inicio, int Largo)
        {
            int    Max = Cadena.Length;
            string Result = "";
            if (Max > Inicio)
            {
                if (Inicio + Largo > Max)
                {
                    Largo = Max - Inicio;
                }
                Result = Cadena.Substring(Inicio, Largo);
            }
            return Result;

        }
        public static string Mid(string Cadena, int Inicio)
        {
            int Max = Cadena.Length;
            int Largo = Max;
            string Result = "";

            if (Max > Inicio)
            {
                if (Inicio + Largo > Max)
                {
                    Largo = Max - Inicio;
                }
                Result = Cadena.Substring(Inicio, Largo);
            }
            return Result;

        }

    }
}
