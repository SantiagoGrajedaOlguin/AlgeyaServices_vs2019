
using System;
using System.Data;
using Newtonsoft.Json.Linq;

namespace Idealsis.Dal.Mapeo
{
    public class SegCatalogosOpciones
    {
        const string  Tabla = "SegCatalogosOpciones";
        int    id;
        int    usuario;
        int    perfil;
        string tipo;
        string codigo;
        byte   soloLectura;

        public int Id { get { return id; } set { id = value; } }
        public int Usuario { get { return usuario; } set { usuario = value; } }
        public int Perfil { get { return perfil; } set { perfil = value; } }
        public string Tipo { get { return tipo; } set { tipo = value; } }
        public string Codigo { get { return codigo; } set { codigo = value; } }
        public byte SoloLectura { get { return soloLectura; } set { soloLectura = value; } }

        public int Guardar(bool ObtenerId)
        {
            string c;
            string v;
            var cnn = new Conexion();

            cnn.Conectar();
            if (ObtenerId) id = (int)Helper.Val(cnn.DameValor(Tabla, "Usuario=" + Usuario + " and Perfil=" + Perfil + " and Tipo=" + Helper.strSql(tipo) + " and Codigo=" + Helper.strSql(codigo), "Id"));

            c = "Id,0,9|Usuario|Perfil|Tipo,1|Codigo,1|SoloLectura";
            v = id + "|" + usuario + "|" + perfil + "|" + tipo + "|" + codigo + "|" + soloLectura;
            id = cnn.GuardarRegistro(Tabla, c, v);
            cnn.Dispose();
            return id;
        }

        private void AsignarValores(IDataReader Registro)
        {
            id = Convert.ToInt32(Registro["Id"].ToString());
            usuario = Convert.ToInt32(Convert.IsDBNull(Registro["Usuario"]) ? 0 : Registro["Usuario"]);
            perfil = Convert.ToInt32(Convert.IsDBNull(Registro["Perfil"]) ? 0 : Registro["Perfil"]);
            tipo = Convert.IsDBNull(Registro["Tipo"]) ? "" : Registro["Tipo"].ToString();
            codigo = Convert.IsDBNull(Registro["Codigo"]) ? "" : Registro["Codigo"].ToString();
            soloLectura = Convert.ToByte(Convert.IsDBNull(Registro["SoloLectura"]) ? 0 : Registro["SoloLectura"]);
        }

        public bool ConsultarPorId(int vId)
        {
            var cnn = new Conexion();
            bool result = false;
            cnn.Conectar();
            IDataReader Registro = cnn.ObtenerRegistro(Tabla, "Id=" + vId);
            if (Registro.Read())
            {
                result = true;
                AsignarValores(Registro);
            }
            Registro.Dispose();
            cnn.Dispose();
            return result;
        }

        public void BorrarPorId(int vId, bool ObtenerId)
        {
            var cnn = new Conexion();
            cnn.Conectar();
            if (ObtenerId) vId = (int)Helper.Val(cnn.DameValor(Tabla, "Usuario=" + usuario + " and Perfil=" + perfil + " and Tipo=" + Helper.strSql(tipo) + " and Codigo=" + Helper.strSql(codigo), "Id"));
            cnn.Exec("DELETE FROM [" + Tabla + "] WHERE Id=" + vId.ToString());
            cnn.Dispose();
        }

        public int BorrarTodos(int vUsuario, int vPerfil)
        {
            var cnn = new Conexion();
            cnn.Conectar();
            int Count = cnn.Exec("DELETE FROM [" + Tabla + "] WHERE Usuario=" + vUsuario + " and Perfil=" + vPerfil);
            cnn.Dispose();
            return Count;
        }

        public int Consultar(int vUsuario, int vPerfil, string vTipo, ref JObject objJSON)
        {
            //IDataReader Registro;
            int Count;
            var cnn = new Conexion();
            string strFiltro;
            string strJSON = "";
            string strConsulta;

            //generar consulta SQL
            strFiltro = "";
            strFiltro = strFiltro + " and Usuario=" + vUsuario;
            strFiltro = strFiltro + " and Perfil=" + vPerfil;
            if (vTipo.Length > 0) strFiltro = strFiltro + " and Tipo=" + Helper.strSql(vTipo);
            strConsulta = "SELECT Id,Usuario,Perfil,Tipo,Codigo,SoloLectura FROM [" + Tabla + "] WHERE Id>0" + strFiltro + " ORDER BY Tipo,Codigo";

            //obtener registros
            Count = 0;
            cnn.Conectar();
            cnn.SQL = strConsulta;
            IDataReader Registro = cnn.AbreSQL();
            if (cnn.Hay(Registro))
            {
                strJSON = Json.RStoJSON(Registro, ref Count);
                Registro.Dispose();
            }
            cnn.Dispose();
            Helper.validarJSON(strJSON, ref objJSON);
            return Count;
        }

        public IDataReader Listar(int vUsuario, int vPerfil, string vTipo)
        {
            var cnn = new Conexion();
            string strFiltro;
            string strConsulta;

            //generar consulta SQL
            strFiltro = "";
            strFiltro = strFiltro + " and Usuario=" + vUsuario;
            strFiltro = strFiltro + " and Perfil=" + vPerfil;
            if (vTipo.Length > 0) strFiltro = strFiltro + " and Tipo=" + Helper.strSql(vTipo);
            strConsulta = "SELECT Id,Usuario,Perfil,Tipo,Codigo,SoloLectura FROM [" + Tabla + "] WHERE Id>0" + strFiltro + " ORDER BY Tipo,Codigo";

            //obtener registros
            cnn.Conectar();
            cnn.SQL = strConsulta;
            IDataReader Registro = cnn.AbreSQL();
            cnn.Dispose();
            return Registro;
        }

        public DataTable ListarTabla(int vUsuario, int vPerfil, string vTipo)
        {
            var cnn = new Conexion();
            string strFiltro;
            string strConsulta;

            //generar consulta SQL
            strFiltro = "";
            strFiltro = strFiltro + " and Usuario=" + vUsuario;
            strFiltro = strFiltro + " and Perfil=" + vPerfil;
            if (vTipo.Length > 0) strFiltro = strFiltro + " and Tipo=" + Helper.strSql(vTipo);
            strConsulta = "SELECT Id,Usuario,Perfil,Tipo,Codigo,SoloLectura FROM [" + Tabla + "] WHERE Id>0" + strFiltro + " ORDER BY Tipo,Codigo";

            //obtener registros
            cnn.Conectar();
            cnn.SQL = strConsulta;
            DataTable Registro = cnn.GetTabla(strConsulta);
            cnn.Dispose();
            return Registro;
        }

        public int GetId(int vUsuario, int vPerfil, string vTipo, string vCodigo)
        {
            var cnn = new Conexion();
            int Result;
            cnn.Conectar();
            Result = (int)Helper.Val(cnn.DameValor(Tabla, "Usuario=" + vUsuario + " and Perfil=" + vPerfil + " and Tipo=" + Helper.strSql(vTipo) + " and Codigo=" + Helper.strSql(vCodigo), "Id"));
            cnn.Dispose();
            return Result;
        }

        //esta la opción activa para el usuario?
        public bool EstaHabilitada(int vUsuario, string vTipo, string vCodigo, string vPerfil)
        {
            var cnn = new Conexion();
            bool Result;

            cnn.Conectar();
            Result = cnn.ExisteRegistro(Tabla, "Usuario=" + vUsuario + " and Tipo=" + Helper.strSql(vTipo) + " and Codigo=" + Helper.strSql(vCodigo) + vPerfil.ToString(),"");
            cnn.Dispose();
            return Result;
        }

        public bool EsEditable(int vUsuario, string vTipo, string vCodigo, string vPerfil)
        {
            var cnn = new Conexion();
            bool Result;
            cnn.Conectar();
            if (cnn.ExisteRegistro(Tabla, "SELECT TOP 1 SoloLectura FROM SegCatalogosOpciones WHERE Usuario=" + vUsuario + " and Tipo=" + Helper.strSql(vTipo) + " and Codigo=" + Helper.strSql(vCodigo) + vPerfil + " ORDER BY SoloLectura ASC", "SoloLectura"))
                Result = Helper.Val(Helper.vg_DAT) == 0;
            else
                Result = true;
            cnn.Dispose();
            return Result;
        }

    }
}
