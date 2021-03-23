
//>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
//using ADOX;
using System.IO;
using System.Drawing;
using System.Globalization;

namespace Idealsis.Dal
{
    public class Conexion : IDisposable
    {
        const string cg_Msg_1 = "¿ Desea borrar el registro ?";

        public SqlConnection sqlConexion;
        public string CadenaDeConexion;
        public int    ProveedorDeDatos;
        public int    TiempoDeEspera;
        public int    Cursor;
        public string NombreDB;
        public string Sistema;

        public string VersionSistema;
        public string FechaSistema;
        public string UsuarioActivo;
        public string Pc;

        public bool   Conectada;
        public bool   EsAlta;
        public string SQL;

        public string RutaScriptSQL;
        public bool   ValidarTablas = false;
        public bool   ValidarVersion = false;
        public bool   ValidarDB = true;

        public Conexion()
        {
            Inicializar();
        }

        public Conexion(bool pValidarTablas, bool pValidarVersion, bool pValidarDB)
        {
            Inicializar();
            ValidarTablas = pValidarTablas;
            ValidarVersion = pValidarVersion;
            ValidarDB = pValidarDB;
        }

        private void Inicializar()
        {
            Sistema = "OBVIAM";
            CadenaDeConexion = System.Configuration.ConfigurationManager.AppSettings["CadenaDeConexion"].ToString();
            Conectada = false;
        }
        public bool Conectar()
        {
            SqlCommand vp_rs;
            string FechaLimite;
            try
            {

                //OneMoreTry:
                Conectada = false;

                //Screen.MousePointer = 11
                if (sqlConexion != null) Conectada = (sqlConexion.State == ConnectionState.Open);
                if (!Conectada)
                {
                    CadenaDeConexion = CadenaDeConexion.Replace("provider=SQLOLEDB;", "");
                    sqlConexion = new SqlConnection(CadenaDeConexion);
                    sqlConexion.Open();
                }
                if (ProveedorDeDatos == 1) //si es SQLServer
                {
                    vp_rs = new SqlCommand("SET DATEFIRST 1", (SqlConnection)sqlConexion);
                    vp_rs.ExecuteNonQuery();
                    vp_rs.Dispose();
                }
                Conectada = true;

                if (ValidarTablas)
                {
                    //verificar las tablas
                    ScriptSql.Leer(sqlConexion, RutaScriptSQL);
                }

                if (ValidarVersion)
                {
                    //validar versión del sistema
                    if (VersionSistema.Length > 0)
                    {
                        if (isDate(VersionSistema))
                        {
                            FechaLimite = "";
                            vp_rs = new SqlCommand("SELECT UltimaVersion FROM sysConfigEmpresa", (SqlConnection)sqlConexion);
                            SqlDataReader reader = vp_rs.ExecuteReader();
                            if (reader.Read()) FechaLimite = Helper.CNull(reader[0].ToString());
                            reader.Close();
                            vp_rs.Dispose();
                            if (isDate(FechaLimite))
                            {
                                if (Convert.ToDateTime(VersionSistema) < Convert.ToDateTime(FechaLimite))
                                {
                                    Conectada = false;
                                    CerrarConexion();
                                    //Screen.MousePointer = vbDefault
                                    Helper.showAviso("La versión de sistema que desea ejecutar no corresponde con la versión de la base de datos\n\nPara poder continuar debera actualizar el sistema a la versión mas reciente");
                                    return Conectada;
                                }
                            }
                        }
                    }

                }
                return Conectada;
            }
            catch (System.Data.Common.DbException ex)
            {

                if (ex.ErrorCode == 91 || ex.ErrorCode == -2147467259)
                {

                    //no se pudo encontrar el archivo
                    if (ValidarDB)
                    {
                        if (Helper.showPregunta(ex.Message + "\n\n¿Desea crear la base de datos?"))
                        {
                            //CrearBaseDeDatos(NombreDB); //crear la base de datos
                            //goto OneMoreTry;
                        }
                    }
                    else
                    {
                        Helper.showError(ex.Message);
                    }
                    return Conectada;
                }
                else if (ex.ErrorCode == 3044 || ex.ErrorCode == 3024)
                {

                    //no se pudo encontrar el path
                    Helper.showError(ex.Message);
                    return Conectada;
                }
                //else if (ex.ErrorCode == -2147217900)
                //    Resume Next

                Helper.showError(ex.Message);
                return Conectada;
            }

            //ShowErrorCnn

            //>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>
            //esta seccion se ejecuta solo si ocurre un error inesperado.
            //>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>
            /*

            Screen.MousePointer = vbDefault

            If Err.Number = 91 Or Err = -2147467259 Then
       
                'no se pudo encontrar el archivo
                If ValidarDB Then
                    If MsgBox(Err.Description + vbCrLf + vbCrLf + "¿Desea crear la base de datos?", 4 + 48) = vbYes Then
                        CrearBaseDeDatos NombreDB 'crear la base de datos
                        GoTo OneMoreTry
                    End If
                Else
                    MsgBox Err.Description + vbCrLf + vbCrLf, vbExclamation
                End If
                Resume Termina

            ElseIf Err = 3044 Or Err = 3024 Then
        
               'no se pudo encontrar el path
                MsgBox Err.Description + vbCrLf + vbCrLf, vbCritical
                Resume Termina

            ElseIf Err.Number = -2147217900 Then
                Resume Next
            End If
            ShowError


            Set Conexion = Nothing
            Conectada = False
            GoSub Termina
            */

        }
        public void CerrarConexion()
        {
            if (sqlConexion.State != ConnectionState.Closed)
            {
                sqlConexion.Close();
            }
            if (sqlConexion != null)
            {
                sqlConexion.Dispose();
                sqlConexion = null;
            }
        }

        public int GuardarRegistro(string nombreTabla, string campos, string valores)
        {
            //CAMPO
            //1. Nombre

            //2. Tipo Dato

            //   0=Numerico
            //   1=Cadena
            //   2=Fecha
            //   3=Hora

            //3. Clase

            //   0=Campo Normal
            //   1=Indice de busqueda
            //   2=Inicializar si es nuevo registro
            //   3=Valor numerico acumulable

            SqlCommand vp_rs;
            int Result = 0;
            int count = 0;
            int i = 1;        // contador
            string cpo = "";     // nombre del campo
            string Nam = "";     // nombre del campo
            byte tpo = 0;      // tipo de campo
            byte idx = 0;      // es parte de indice
            string dat = "";     // dato a guardar en el campo

            string vp_busc = ""; //cadena para buscar datos
            string vp_guar = ""; //cadena para actualizar datos
            string vp_valu = ""; //
            bool IsIdentity = false;
            string cpoIdentity = "";

            byte isNew = 0;
            string sql = "";
            try
            {

                //obtener lista de campos usados para la busqueda
                do
                {
                    cpo = Helper.CutStr(campos, "|", i).Trim();
                    if (cpo.Length == 0) break;
                    idx = (byte)Helper.Val(Helper.CutStr(cpo, ",", 3, "0").Trim());
                    if (idx == 9)
                    {
                        IsIdentity = true;
                        cpoIdentity = Helper.CutStr(cpo, ",", 1).Trim();
                    }
                    if (idx == 1 || IsIdentity)
                    {
                        dat = Helper.CutStr(valores, "|", i); //obtener dato
                        if (idx == 1 || (idx==9 && Helper.Val(dat) > 0))
                        {
                            if (IsIdentity) Result = Helper.Val(dat);
                            Nam = "[" + Helper.CutStr(cpo, ",", 1).Trim().Replace("[", "").Replace("]", "") + "]";
                            tpo = (byte)Helper.Val(Helper.CutStr(cpo, ",", 2, "0").Trim());
                            if (tpo == 0) dat = Helper.ValNumSave(dat);
                            if (tpo == 1) dat = strSql(dat);
                            if (tpo == 2) 
                                dat = (isDate(dat) ? DateSQL(dat) : "NULL");
                            if (tpo == 3) dat = (isTime(dat) ? TimeSQL(dat) : "NULL");
                            if (tpo == 4) dat = (isDate(dat) ? DTSQL(dat) : "NULL");
                            vp_busc = vp_busc + (vp_busc.Length > 0 ? " and " : "") + Nam + "=" + dat;
                        }
                    }
                    i += 1;
                } while (cpo != "" && idx != 0);
                nombreTabla = "[" + nombreTabla + "]";
                isNew = 1;

                //establece si sera un registro nuevo
                if (vp_busc.Length > 0)
                {
                    vp_busc = " WHERE " + vp_busc;
                    sql = "SELECT Count(*) as num FROM " + nombreTabla + vp_busc;
                    vp_rs = new SqlCommand(sql, (SqlConnection)sqlConexion);
                    count = (int)vp_rs.ExecuteScalar();
                    isNew = (byte)(count > 0 ? 0 : 1);
                    vp_rs.Dispose();
                }

                //obtener lista de campos a actualizar
                i = 1;
                vp_guar = "";
                vp_valu = "";
                do
                {
                    cpo = Helper.CutStr(campos, "|", i).Trim();
                    if (cpo.Length == 0) break;
                    Nam = "[" + Helper.CutStr(cpo, ",", 1).Trim().Replace("[", "").Replace("]", "") + "]";
                    tpo = (byte)Helper.Val(Helper.CutStr(cpo, ",", 2, "0").Trim());
                    idx = (byte)Helper.Val(Helper.CutStr(cpo, ",", 3, "0").Trim());
                    dat = Helper.CutStr(valores, "|", i); //obtener dato
                    if (tpo == 0) dat = Helper.ValNumSave(dat);
                    if (tpo == 1) dat = strSql(dat);
                    if (tpo == 2) 
                        dat = (isDate(dat) ? DateSQL(dat) : "NULL");
                    if (tpo == 3) dat = (isTime(dat) ? TimeSQL(dat) : "NULL");
                    if (tpo == 4) dat = (isTime(dat) ? DTSQL(dat) : "NULL");

                    if (isNew == 1)
                    {
                        if (idx != 9) //si no es campo IDENTITY
                        {
                            vp_guar = vp_guar + (vp_guar.Length > 0 ? ", " : "") + Nam;
                            vp_valu = vp_valu + (vp_valu.Length > 0 ? ", " : "") + dat;
                        }
                    }
                    else
                    {
                        if (idx != 2 && idx != 1 && idx != 9) vp_guar = vp_guar + (vp_guar.Length > 0 ? ", " : "") + Nam + "=" + (idx == 3 ? Nam + "+" : "") + dat;
                    }
                    i += 1;
                } while (cpo != "");

                EsAlta = isNew == 1;

                //definir consulta a ejecutar
                sql = "";
                if (isNew == 1)
                    sql = "INSERT INTO " + nombreTabla + " (" + vp_guar + ")" + (IsIdentity ? " OUTPUT Inserted." + cpoIdentity : "") + " VALUES (" + vp_valu + ")";
                else
                {
                    if (vp_guar.Length > 0) sql = "UPDATE " + nombreTabla + " SET " + vp_guar + vp_busc;
                }
                if (sql.Length > 0)
                {
                    vp_rs = new SqlCommand(sql, (SqlConnection)sqlConexion);
                    if (isNew == 1 && IsIdentity)
                    {
                        Result = (int)vp_rs.ExecuteScalar();
                    }
                    else
                    {
                        vp_rs.ExecuteNonQuery();
                    }
                    vp_rs.Dispose();
                }
                sql = "";
                valores = "";
                campos = "";
                return Result;
            }
            catch (System.Data.Common.DbException ex)
            {
                Helper.showError(ex.Message);
                return Result;
            }
        }

        public IDataReader ObtenerRegistro(string Tabla, string strBuscar)
        {
            string SQL = "";
            //bool res = false;
            try
            {

                if (strBuscar.IndexOf("SELECT") >= 0)
                    SQL = ValSQL(strBuscar);
                else
                    SQL = "SELECT * FROM  [" + Tabla + "]" + (strBuscar.Length > 0 ? " WHERE " + strBuscar : "");

                var Registro = new SqlCommand(SQL, (SqlConnection)sqlConexion);
                return Registro.ExecuteReader();
                /*
                if (Reader.Read())
                {
                    res = true;
                    Fila = Reader;
                }
                else
                {
                    res = false;
                    Fila = null;
                }
                Registro.Dispose();
                return Reader;
                */
            }
            catch (System.Data.Common.DbException ex)
            {
                Helper.showError(ex.Message);
                return null;
            }
        }

        public IDataReader MoverCodigo(string Tabla, int Direccion, int vCodigo)
        {
            string Filtro = "";
            switch (Direccion)
            {
                case 0: Filtro = "WHERE Codigo=" + vCodigo + " ORDER BY Codigo"; break;
                case 1: Filtro = "ORDER BY Codigo ASC"; break;
                case 2: Filtro = "WHERE Codigo < " + vCodigo + " ORDER BY Codigo DESC"; break;
                case 3: Filtro = "WHERE Codigo > " + vCodigo + " ORDER BY Codigo ASC"; break;
                case 4: Filtro = "ORDER BY Codigo DESC"; break;
            }
            return ObtenerRegistro(Tabla, "SELECT TOP 1 * FROM [" + Tabla + "] " + Filtro);
        }

        public IDataReader MoverCatalogo(string Tabla, int Direccion, short vCatalogo, string vCodigo)
        {
            string Filtro = "";
            switch (Direccion)
            {
                case 0: Filtro = " and Codigo='" + vCodigo + "' ORDER BY Codigo"; break;
                case 1: Filtro = " ORDER BY Codigo ASC"; break;
                case 2: Filtro = " and Codigo < '" + vCodigo + "' ORDER BY Codigo DESC"; break;
                case 3: Filtro = " and Codigo > '" + vCodigo + "' ORDER BY Codigo ASC"; break;
                case 4: Filtro = " ORDER BY Codigo DESC"; break;
            }
            return ObtenerRegistro(Tabla, "SELECT TOP 1 * FROM [" + Tabla + "] WHERE Catalogo=" + vCatalogo + Filtro);
        }

        public IDataReader MoverClase(string Tabla, int Direccion, byte vClase, int vCodigo)
        {
            string Filtro = "";
            switch (Direccion)
            {
                case 0: Filtro = " and Codigo=" + vCodigo + " ORDER BY Codigo"; break;
                case 1: Filtro = " ORDER BY Codigo ASC"; break;
                case 2: Filtro = " and Codigo < " + vCodigo + " ORDER BY Codigo DESC"; break;
                case 3: Filtro = " and Codigo > " + vCodigo + " ORDER BY Codigo ASC"; break;
                case 4: Filtro = " ORDER BY Codigo DESC"; break;
            }
            return ObtenerRegistro(Tabla, "SELECT TOP 1 * FROM [" + Tabla + "] WHERE Clase=" + vClase + Filtro);
        }

        public IDataReader MoverPadre(string Tabla, int Direccion, short vCatalogo, short vCatPadre, int vIdPadre, int vCodigo, int vId)
        {
            string Filtro = "";
            switch (Direccion)
            {
                case 0: Filtro = " WHERE Id=" + vId; break;
                case 1: Filtro = " ORDER BY Codigo ASC"; break;
                case 2: Filtro = " and Codigo < " + vCodigo + " ORDER BY Codigo DESC"; break;
                case 3: Filtro = " and Codigo > " + vCodigo + " ORDER BY Codigo ASC"; break;
                case 4: Filtro = " ORDER BY Codigo DESC"; break;
            }
            //return ObtenerRegistro(Tabla, "SELECT TOP 1 * FROM [" + Tabla + "] WHERE Clase=" + vClase + Filtro);
            return ObtenerRegistro(Tabla, "SELECT TOP 1 * FROM [" + Tabla + "] " + (Direccion > 0 ? "WHERE Catalogo=" + vCatalogo + (vCatPadre > 0 ? " and IdPadre=" + vIdPadre : "") : "") + Filtro);
        }

        public bool Hay(IDataReader Records)
        {
            bool Result = false;
            if (Records != null)
            {
                if (!Records.IsClosed)
                {
                    Result = true;
                    //If Records.EOF And Records.BOF Then Hay = False
                }
            }
            return Result;
        }

        public bool BorrarRegistro(string Tabla, string strBuscar)
        {
            if (!Helper.showPregunta(cg_Msg_1)) return false;
            Exec("DELETE FROM [" + Tabla + "]" + (strBuscar.Length > 0 ? " WHERE " + strBuscar : ""));
            return true;
        }

        public bool BorrarRegistro(string Tabla, string strBuscar, bool Preguntar)
        {
            if (Preguntar) if (!Helper.showPregunta(cg_Msg_1)) return false;
            Exec("DELETE FROM [" + Tabla + "]" + (strBuscar.Length > 0 ? " WHERE " + strBuscar : ""));
            return true;
        }

        public bool ExisteRegistro(string Tabla, string strBuscar, string Campos)
        {
            string SQL = "";
            bool Result = false;
            int i = 0;
            try
            {

                if (strBuscar.IndexOf("SELECT") >= 0)
                    SQL = ValSQL(strBuscar);
                else
                    SQL = "SELECT " + (Campos.Length > 0 ? Campos : " * ") + " FROM  [" + Tabla + "]" + (strBuscar.Length > 0 ? " WHERE " + strBuscar : "");

                var Registro = new SqlCommand(SQL, (SqlConnection)sqlConexion);
                SqlDataReader Reader = Registro.ExecuteReader();
                Helper.vg_DAT = "";
                if (Reader.Read())
                {
                    Result = true;
                    if (Campos.Length > 0)
                    {
                        for (i = 0; i < Reader.FieldCount; i++)
                        {
                            Helper.vg_DAT = Helper.vg_DAT + (i > 0 ? "|" : "") + Reader[i].ToString();
                        }
                    }
                }
                Reader.Close();
                Registro.Dispose();
                return Result;
            }
            catch (System.Data.Common.DbException ex)
            {
                Helper.showError(ex.Message);
                return Result;
            }
        }

        public string DameValor(string Tabla, string strBuscar, string Campos)
        {
            string SQL = "";
            string Result = "";
            //int    i = 0;
            try
            {

                if (Campos.Length == 0) Campos = "Descripcion";
                if (strBuscar.IndexOf("SELECT") >= 0)
                    SQL = ValSQL(strBuscar);
                else
                    SQL = "SELECT " + (Campos.Length > 0 ? Campos : " * ") + " FROM  [" + Tabla + "]" + (strBuscar.Length > 0 ? " WHERE " + strBuscar : "");

                var Registro = new SqlCommand(SQL, (SqlConnection)sqlConexion);
                var Valor = Registro.ExecuteScalar();
                if (Valor != null)
                {
                    Result = Valor.ToString();
                }
                return Result;
                //return Convert.ToString(Result);
                
            }
            catch (System.Data.Common.DbException ex)
            {
                Helper.showError(ex.Message);
                return Result;
            }
        }

        public string DameValor(string Tabla, string strBuscar)
        {
            string SQL = "";
            string Result = "";
            string Campos = "Descripcion";
            try
            {

                if (strBuscar.IndexOf("SELECT") >= 0)
                    SQL = ValSQL(strBuscar);
                else
                    SQL = "SELECT " + (Campos.Length > 0 ? Campos : " * ") + " FROM  [" + Tabla + "]" + (strBuscar.Length > 0 ? " WHERE " + strBuscar : "");

                var Registro = new SqlCommand(SQL, (SqlConnection)sqlConexion);
                return (string)Registro.ExecuteScalar();
            }
            catch (System.Data.Common.DbException ex)
            {
                Helper.showError(ex.Message);
                return Result;
            }
        }

        public IDataReader AbreSQL()
        {
            try
            {
                var cmd = new SqlCommand(SQL, (SqlConnection)sqlConexion);
                return cmd.ExecuteReader();
            }
            catch (System.Data.Common.DbException ex)
            {
                Helper.showError(ex.Message);
                return null;
            }
        }

        public IDataReader AbreSQL(string cadSQL)
        {
            try
            {
                var cmd = new SqlCommand(cadSQL, (SqlConnection)sqlConexion);
                return cmd.ExecuteReader();
            }
            catch (System.Data.Common.DbException ex)
            {
                Helper.showError(ex.Message);
                return null;
            }
        }

        public DataTable GetTabla(string cadSQL)
        {
            DataTable Tabla = new DataTable();
            try
            {
                var cmd = new SqlCommand(cadSQL, (SqlConnection)sqlConexion);

                using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                {
                    da.Fill(Tabla);
                }
                return Tabla;
            }
            catch (System.Data.Common.DbException ex)
            {
                Helper.showError(ex.Message);
                return null;
            }
        }

        public DataTable GetSpTabla(string NombreProcedimiento, int Catalogo)
        {
            DataTable Tabla = new DataTable();
            try
            {
                SqlCommand cmd = new SqlCommand(NombreProcedimiento, (SqlConnection)sqlConexion);
                cmd.Parameters.AddWithValue("@Catalogo", Catalogo);
                cmd.CommandType = CommandType.StoredProcedure;
                //SqlDataAdapter da = new SqlDataAdapter();
                //da.SelectCommand = cmd;
                //da.Fill(Tabla);
                using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                {
                    da.Fill(Tabla);
                }
                
                return Tabla;
            }
            catch (System.Data.Common.DbException ex)
            {
                Helper.showError(ex.Message);
                return null;
            }
        }

        public DataTable GetSpTabla(string NombreProcedimiento, SqlParameter Parametros)
        {
            DataTable Tabla = new DataTable();
            try
            {
                SqlCommand cmd = new SqlCommand(NombreProcedimiento, (SqlConnection)sqlConexion);
                cmd.Parameters.AddWithValue("@Catalogo", 1);
                cmd.CommandType = CommandType.StoredProcedure;
                //SqlDataAdapter da = new SqlDataAdapter();
                //da.SelectCommand = cmd;
                //da.Fill(Tabla);
                using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                {
                    da.Fill(Tabla);
                }

                return Tabla;
            }
            catch (System.Data.Common.DbException ex)
            {
                Helper.showError(ex.Message);
                return null;
            }
        }

        public int EjecutaSp(string NombreProcedimiento, int Id, string Json)
        {
            try
            {
                SqlCommand cmd = new SqlCommand(NombreProcedimiento, (SqlConnection)sqlConexion);
                cmd.Parameters.AddWithValue("@IdOrigen", Id);
                cmd.Parameters.AddWithValue("@DetalleJson ", Json);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.ExecuteNonQuery();
                return 1;
            }
            catch (System.Data.Common.DbException ex)
            {
                Helper.showError(ex.Message);
                return -1;
            }
        }

        public DataSet GetSpDataSet(string NombreProcedimiento)
        {
            DataSet Tablas = new DataSet();
            try
            {
                var cmd = new SqlCommand(NombreProcedimiento, (SqlConnection)sqlConexion);
                cmd.CommandType = CommandType.StoredProcedure;
                using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                {
                    da.Fill(Tablas);
                }
                return Tablas;
            }
            catch (System.Data.Common.DbException ex)
            {
                Helper.showError(ex.Message);
                return null;
            }
        }

        public void RegistrarBitacora(string Opcion, string Accion)
        {
            string c;
            string v;
            try
            {
                c = "Id,0,9|Fecha,2|Hora,3|FechaSistema,2|Usuario,1|Opcion,1|Accion,1|Pc,1|IdEmpleado|Version,1|IdDocumento|Tipo";
                v = "0|" + DateTime.Now.ToString("yyyy/MM/dd") + "|" + DateTime.Now.ToString("hh:mm:ss") + "|" + FechaSistema + "|" + UsuarioActivo + "|" + Helper.Mid(Opcion, 0, 60) + "|" + Helper.Mid(Accion, 0, 160) + "|" + Helper.Mid(Pc, 0, 50) + "|0|" + Helper.Mid(VersionSistema, 0, 20) + "|0|0";
                GuardarRegistro("segBitacora", c, v);
            }
            catch (System.Data.Common.DbException ex)
            {
                Helper.showError(ex.Message);
            }
        }
        public void RegistrarBitacora(string Opcion, string Accion, int IdEmpleado, int IdOrigen, int Tipo)
        {
            string c;
            string v;
            try
            {
                c = "Id,0,9|Fecha,2|Hora,3|FechaSistema,2|Usuario,1|Opcion,1|Accion,1|Pc,1|IdEmpleado|Version,1|IdDocumento|Tipo";
                v = "0|" + DateTime.Now.ToString("dd/MM/yyyy") + "|" + DateTime.Now.ToString("hh:mm:ss") + "|" + FechaSistema + "|" + UsuarioActivo + "|" + Helper.Mid(Opcion, 0, 60) + "|" + Helper.Mid(Accion, 0, 150) + "|" + Helper.Mid(Pc, 0, 50) + "|" + IdEmpleado.ToString() + "|" + VersionSistema.Substring(0, 20) + "|" + IdOrigen.ToString() + "|" + Tipo.ToString();
                GuardarRegistro("segBitacora", c, v);
            }
            catch (System.Data.Common.DbException ex)
            {
                Helper.showError(ex.Message);
            }
        }

        //public void CrearBaseDeDatos(string NombreDB)
        //{
        //    if (ProveedorDeDatos == 1)
        //    {
        //        using (SqlConnection cnn = new SqlConnection(CadenaDeConexion.Replace("Catalog=" + NombreDB, "Catalog=master")))
        //        {
        //            cnn.Open();
        //            var cmd = new SqlCommand("CREATE DATABASE [" + NombreDB + "];", (SqlConnection)cnn);
        //            cmd.ExecuteNonQuery();
        //            cmd.Dispose();
        //        }
        //    }
        //    else
        //    {
        //        var vp_cat = new ADOX.Catalog();
        //        //crear la base de datos de access
        //        vp_cat.Create(CadenaDeConexion);
        //        vp_cat = null;
        //    }
        //}

        public void RespaldarDB(string RutaArchivo)
        {
            try
            {
                using (SqlConnection cnn = new SqlConnection(CadenaDeConexion.Replace("Catalog=" + NombreDB, "Catalog=master")))
                {
                    cnn.Open();
                    var cmd = new SqlCommand("BACKUP DATABASE [" + NombreDB + "] TO DISK = '" + RutaArchivo + "' WITH INIT", (SqlConnection)cnn);
                    cmd.ExecuteNonQuery();
                    cmd.Dispose();
                    Helper.showAviso("El proceso finalizó con éxito");
                }
            }
            catch (System.Data.Common.DbException ex)
            {
                Helper.showError(ex.Message);
            }
        }

        public void RecuperarDB(string RutaArchivo, bool Reemplazar)
        {
            string strConSql;
            string ArchivoDbf = "";
            string ArchivoLog = "";
            string NombreDbf = "";
            string NombreLog = "";
            //IDataReader rs;
            try
            {
                Exec("USE [" + NombreDB + "]");
                //SQL = "RESTORE FILELISTONLY FROM DISK = '" + RutaArchivo + "'";
                //AbreSQL(rs);

                using (SqlConnection cnn = new SqlConnection(CadenaDeConexion.Replace("Catalog=" + NombreDB, "Catalog=master")))
                {
                    cnn.Open();
                    if (ArchivoDbf.Length > 0)
                        strConSql = "RESTORE DATABASE [" + NombreDB + "]" +
                                        " FROM DISK = '" + RutaArchivo + "'" +
                                        " WITH MOVE '" + NombreDbf + "' TO '" + ArchivoDbf + "'" +
                                        (ArchivoLog.Length > 0 ? " , MOVE '" + NombreLog + "' TO '" + ArchivoLog + "'" : "") + (Reemplazar ? ", REPLACE" : "");
                    else
                        strConSql = "RESTORE DATABASE [" + NombreDB + "] FROM DISK = '" + RutaArchivo + "'" + (Reemplazar ? " WITH REPLACE" : "");

                    var cmd = new SqlCommand(strConSql, (SqlConnection)cnn);
                    cmd.ExecuteNonQuery();
                    cmd.Dispose();
                    Helper.showAviso("El proceso finalizó con éxito");
                }
            }
            catch (System.Data.Common.DbException ex)
            {
                Helper.showError(ex.Message);
            }
        }

        //public void GuardarVarBin(string sqlStrCmd, string FileName)
        //{
        //    ADODB.Stream stm = new ADODB.Stream();
        //    stm.Type = ADODB.StreamTypeEnum.adTypeBinary;
        //    stm.Open();
        //    stm.LoadFromFile(FileName);
        //    var Comand = new SqlCommand(sqlStrCmd, (SqlConnection)sqlConexion);
        //    Comand.Parameters[0].Value = stm.Read();
        //    Comand.ExecuteNonQuery();
        //    stm.Close();
        //    stm = null;
        //    Comand.Dispose();
        //}
        ////public void LeerImagenBinaria(ADOField As ADODB.Field, unPicture As IPictureDisp, Optional vp_rut As String = "pictemp");
        //public Image LeerImagenBinaria(object imgArray , string vp_rut)
        //{
        //    ADODB.Stream stm = new ADODB.Stream();
        //    byte vp_del = 0;

        //    if (vp_rut == "pictemp") vp_del = 1;
        //    stm.Type = ADODB.StreamTypeEnum.adTypeBinary;
        //    stm.Open();
        //    stm.Write(imgArray);
        //    stm.SaveToFile( vp_rut, ADODB.SaveOptionsEnum.adSaveCreateOverWrite);
        //    stm.Close();
        //    stm = null;
        //    var img = Image.FromFile(vp_rut);
        //    if (vp_del > 0)
        //    {
        //        File.Delete(vp_rut);
        //    }
        //    return img;
        //}

        public int Exec(string comandoSql)
        {
            var com = new SqlCommand(ValSQL(comandoSql), (SqlConnection)sqlConexion);
            return com.ExecuteNonQuery();
        }

        public string strSql(string cad)
        {
            cad = cad.Replace("'", "''");
            return "'" + cad + "'";
        }

        public string nSt(string cad)
        {
            cad = cad.Replace("'", "''");
            return "'%" + cad + "%'";
        }

        public bool isDate(object obj)
        {
            try
            {
                CultureInfo culture = new CultureInfo("es-MX");
                DateTime x = Convert.ToDateTime(obj, culture);
                return true;
            }
            catch (Exception ex) {  }
            return false;
        }
        public bool isTime(object obj)
        {
            try
            {
                DateTime x = Convert.ToDateTime(obj);
                return true;
            }
            catch (Exception ex) {  }
            return false;
        }

        public string DateSQL(string vFecha)
        {
            string result = "null";
            //CultureInfo culture = new CultureInfo("en-US");
            CultureInfo culture = new CultureInfo("es-MX");
            //es - ES

            if (isDate(vFecha))
            {
                if (ProveedorDeDatos == 1)
                {
                    //if (CultureInfo.CurrentCulture.Name == "en-US")
                    //{
                    //    result = "CONVERT(DATETIME, '" + String.Format("{0:yyyy-MM-dd}", Convert.ToDateTime(vFecha, culture)) + " 00:00:00', 102)";
                    //}
                    //else
                    //{
                        result = "CONVERT(DATETIME, '" + String.Format("{0:yyyy-MM-dd}", Convert.ToDateTime(vFecha, culture)) + " 00:00:00', 102)";
                    //}
                    //result = "CONVERT(DATETIME, '" + vFecha.ToString("dd-MM-yyyy") + " 00:00:00', 102)";
                }
                else
                {
                    result = "#" + String.Format("{0:yyyy-MM-dd}", vFecha) + "#";
                }
            }
            return result;
        }

        public string DTSQL(string vFecha)
        {
            string result = "null";
            string vFec;
            string vHor;
            if (isDate(vFecha))
            {
                if (ProveedorDeDatos == 1)
                {
                    //vFecha = String.Format("{0:yyyy/dd/MM HH:mm:ss}", vFecha);
                    //vFecha = DateTime.ParseExact(vFecha, "dd/MM/yyyyT HH:mm:ss", CultureInfo.InvariantCulture).ToString("yyyy/dd/MM");
                    vFec = Helper.CutStr(vFecha, " ", 1);
                    vHor = Helper.CutStr(vFecha, " ", 2);
                    result = "CONVERT(DATETIME, '" + String.Format("{0:yyyy/dd/MM}", vFec) + " " + String.Format("{0:HH:mm:ss}", vHor) + "', 102)";
                }
                else
                {
                    result = "#" + String.Format("{0:yyyy/dd/MM}", vFecha) + " " + String.Format("{0:HH:mm:ss}", vFecha) + "#";
                }
            }
            return result;
        }

        /// <summary>
        /// Convierte la hora en formato SQL valido segun el proveedor de datos
        /// </summary>
        /// <param name="vHora"> Hora a convertir.</param>
        public string TimeSQL(string vHora)
        {
            string result = "null";
            string Fecha;
            string Hora;

            if (isDate(vHora))
            {
                if (ProveedorDeDatos == 1)
                {
                    Fecha = Helper.CutStr(vHora, " ", 1);
                    Hora = Helper.CutStr(vHora, " ", 2);
                    //result = "CONVERT(DATETIME, '" + String.Format("{0:yyyy/dd/MM}", Fecha) + " " + String.Format("{0:HH:mm:ss}", Hora) + "', 102)";
                    result = "CONVERT(DATETIME, '1899-12-30 " + String.Format("{0:HH:mm:ss}", Hora) + "', 102)";
                }
                else
                {
                    result = "#" + String.Format("{0:hh:mm:ss tt}", vHora) + "#";
                }
            }
            return result;
        }

        private string ValSQL(string SQL)
        {
            var Validar = new TransactSql();
            return Validar.Validar(SQL, ProveedorDeDatos);
        }

        public void Dispose()
        {
            CerrarConexion();
            GC.SuppressFinalize(this);
        }

    }
}

