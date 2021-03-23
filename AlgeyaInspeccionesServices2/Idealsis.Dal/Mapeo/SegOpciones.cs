
using System;
using System.Data;
using System.Data.SqlClient;
using Newtonsoft.Json.Linq;

namespace Idealsis.Dal.Mapeo
{
    public class SegOpciones
    {

        const string  Tabla = "SegOpciones";
        string tipo;
        string codigo;
        string padre;
        string descripcion;
        short origen;
        short catalogo;
        byte esPermiso;
        int orden;

        public string Tipo { get { return tipo; } set { tipo = value; } }
        public string Codigo { get { return codigo; } set { codigo = value; } }
        public string Padre { get { return padre; } set { padre = value; } }
        public string Descripcion { get { return descripcion; } set { descripcion = value; } }
        public short  Origen { get { return origen; } set { origen = value; } }
        public short Catalogo { get { return catalogo; } set { catalogo = value; } }
        public byte EsPermiso { get { return esPermiso; } set { esPermiso = value; } }
        public int    Orden { get { return orden; } set { orden = value; } }

        public void Guardar()
        {
            string c;
            string v;
            var cnn = new Conexion();

            cnn.Conectar();
            c = "Tipo,1,1|Codigo,1,1|Padre,1|Descripcion,1|Origen|Catalogo|EsPermiso|Orden";
            v = tipo + "|" + codigo + "|" + padre + "|" + descripcion + "|" + origen + "|" + catalogo + "|" + esPermiso + "|" + orden;
            cnn.GuardarRegistro(Tabla, c, v);
            cnn.Dispose();
        }

        private void AsignarValores(IDataReader Registro)
        {
            tipo = Convert.IsDBNull(Registro["Tipo"]) ? "" : Registro["Tipo"].ToString();
            codigo = Convert.IsDBNull(Registro["Codigo"]) ? "" : Registro["Codigo"].ToString();
            padre = Convert.IsDBNull(Registro["Padre"]) ? "" : Registro["Padre"].ToString();
            descripcion = Convert.IsDBNull(Registro["Descripcion"]) ? "" : Registro["Descripcion"].ToString();
            origen = Convert.ToInt16(Convert.IsDBNull(Registro["Origen"]) ? 0 : Registro["Origen"]);
            catalogo = Convert.ToInt16(Convert.IsDBNull(Registro["Catalogo"]) ? 0 : Registro["Catalogo"]);
            esPermiso = Convert.ToByte(Convert.IsDBNull(Registro["EsPermiso"]) ? 0 : Registro["EsPermiso"]);
            orden = Convert.ToInt32(Convert.IsDBNull(Registro["Orden"]) ? 0 : Registro["Orden"]);

        }

        public bool BuscarPorId(string vTipo, string vCodigo)
        {
            var cnn = new Conexion();
            bool result = false;
            cnn.Conectar();
            IDataReader Registro = cnn.ObtenerRegistro(Tabla, "Tipo=" + Helper.strSql(vTipo) + " and Codigo=" + Helper.strSql(vCodigo));
            if (Registro.Read())
            {
                result = true;
                AsignarValores(Registro);
            }
            Registro.Dispose();
            cnn.Dispose();
            return result;
        }

        public void BorrarPorId(string vTipo, string vCodigo)
        {
            var cnn = new Conexion();
            cnn.Conectar();
            cnn.Exec("DELETE FROM [" + Tabla + "] WHERE Tipo=" + Helper.strSql(vTipo) + " and Codigo=" + Helper.strSql(vCodigo));
            cnn.Dispose();
        }

        public void BorrarPorTipo(string vTipo)
        {
            var cnn = new Conexion();
            cnn.Conectar();
            cnn.Exec("DELETE FROM [" + Tabla + "] WHERE Tipo=" + Helper.strSql(vTipo));
            cnn.Dispose();
        }

        public int Listar(byte OmitirAdmin, string vTipo, ref string strJSON)
        {
            int Count;
            var cnn = new Conexion();
            string strFiltro;
            string strConSql;

            //generar consulta SQL
            strFiltro = "";
            if (OmitirAdmin > 0) strFiltro = strFiltro + " and Tipo<>'Admin'";
            if (vTipo.Length > 0) strFiltro = strFiltro + " and Tipo=" + Helper.strSql(vTipo);
            strConSql = "SELECT Tipo,Codigo,Padre,Descripcion,Origen,Catalogo,EsPermiso,Orden FROM [" + Tabla + "] WHERE len(Codigo)>0" + strFiltro + " ORDER BY " + (vTipo.Length > 0? "Orden" : "Tipo,Codigo");

            //obtener registros
            strJSON = "";
            Count = 0;
            cnn.Conectar();
            cnn.SQL = strConSql;
            IDataReader Registro = cnn.AbreSQL();
            if (cnn.Hay(Registro))
            {
                strJSON = Json.ReaderToJson(Registro, ref Count);
                Registro.Dispose();
            }
            cnn.Dispose();
            return Count;
        }

        public DataTable ListarTabla(byte OmitirAdmin, string vTipo)
        {
            var cnn = new Conexion();
            string strFiltro;
            string strConSql;

            //generar consulta SQL
            strFiltro = "";
            if (OmitirAdmin > 0) strFiltro = strFiltro + " and Tipo<>'Admin'";
            if (vTipo.Length > 0) strFiltro = strFiltro + " and Tipo=" + Helper.strSql(vTipo);
            strConSql = "SELECT Tipo,Codigo,Padre,Descripcion,Origen,Catalogo,EsPermiso,Orden FROM [" + Tabla + "] WHERE len(Codigo)>0" + strFiltro + " ORDER BY " + (vTipo.Length > 0 ? "Orden" : "Tipo,Codigo");

            //obtener registros
            cnn.Conectar();
            DataTable Registro = cnn.GetTabla(strConSql);
            cnn.Dispose();
            return Registro;
        }

    }
}
