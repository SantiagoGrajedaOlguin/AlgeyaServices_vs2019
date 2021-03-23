using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Idealsis.Dal.Mapeo
{
    public class SysConfigValor
    {
        const string Tabla = "SysConfigValor";
        short  catalogo;
        string codigo;
        string descripcion;
        string modulo;
        string grupo;
        int    orden;
        string valor;

        public short Catalogo { get { return catalogo; } set { catalogo = value; } }
        public string Codigo { get { return codigo; } set { codigo = value; } }
        public string Descripcion { get { return descripcion; } set { descripcion = value; } }
        public string Modulo { get { return modulo; } set { modulo = value; } }
        public string Grupo { get { return grupo; } set { grupo = value; } }
        public int Orden { get { return orden; } set { orden = value; } }
        public string Valor { get { return valor; } set { valor = value; } }

        public void Guardar()
        {
            string c;
            string v;
            var cnn = new Conexion();

            cnn.Conectar();
            c = "Catalogo,0,1|Codigo,1,1|Descripcion,1|Modulo,1|Grupo,1|Orden|Valor,1";
            v = catalogo + "|" + codigo + "|" + descripcion + "|" + modulo + "|" + grupo + "|" + orden + "|" + valor;
            cnn.GuardarRegistro(Tabla, c, v);
            cnn.Dispose();
            cnn = null;
        }

        private void AsignarValores(IDataReader Registro)
        {
            catalogo = Convert.ToInt16(Convert.IsDBNull(Registro["Catalogo"]) ? 0 : Registro["Catalogo"]);
            codigo = Convert.IsDBNull(Registro["Codigo"]) ? "" : Registro["Codigo"].ToString();
            descripcion = Convert.IsDBNull(Registro["Descripcion"]) ? "" : Registro["Descripcion"].ToString();
            modulo = Convert.IsDBNull(Registro["Modulo"]) ? "" : Registro["Modulo"].ToString();
            grupo = Convert.IsDBNull(Registro["Grupo"]) ? "" : Registro["Grupo"].ToString();
            orden = Convert.ToInt16(Convert.IsDBNull(Registro["Orden"]) ? 0 : Registro["Orden"]);
            valor = Convert.IsDBNull(Registro["Valor"]) ? "" : Registro["Valor"].ToString();
        }

        public bool Desplegar(int Direccion, short vCodigo)
        {
            var cnn = new Conexion();
            bool Result = false;
            cnn.Conectar();
            IDataReader Registro = cnn.MoverCodigo(Tabla, Direccion, vCodigo);
            if (Registro.Read())
            {
                Result = true;
                AsignarValores(Registro);
            }
            Registro.Dispose();
            cnn.Dispose();
            return Result;
        }

        public bool BuscarPorId(short vCatalogo, string vCodigo)
        {
            var cnn = new Conexion();
            bool result = false;
            cnn.Conectar();
            IDataReader Registro = cnn.ObtenerRegistro(Tabla, "Catalogo=" + vCatalogo + " and Codigo=" + Helper.strSql(vCodigo));
            if (Registro.Read())
            {
                result = true;
                AsignarValores(Registro);
            }
            Registro.Dispose();
            cnn.Dispose();
            return result;
        }
        public string ObtenerValor(short vCatalogo, string vCodigo, string Campo)
        {
            var cnn = new Conexion();
            string result = "";
            cnn.Conectar();
            result = cnn.DameValor(Tabla, "Catalogo=" + vCatalogo + " and Codigo=" + cnn.strSql(vCodigo), Campo);
            cnn.Dispose();
            return result;
        }

        public bool BuscarPorDesc(short vCatalogo, string vDescripcion)
        {
            var cnn = new Conexion();
            bool result = false;
            cnn.Conectar();
            IDataReader Registro = cnn.ObtenerRegistro(Tabla, "Catalogo=" + vCatalogo + " and Descripcion=" + cnn.strSql(vDescripcion));
            if (Registro.Read())
            {
                result = true;
                AsignarValores(Registro);
            }
            Registro.Dispose();
            cnn.Dispose();
            return result;
        }

        public bool BorrarPorId(short vCatalogo, string vCodigo, bool Preguntar)
        {
            var cnn = new Conexion();
            bool Respuesta;
            cnn.Conectar();
            Respuesta = cnn.BorrarRegistro(Tabla, "Catalogo=" + vCatalogo + " and Codigo=" + cnn.strSql(vCodigo), Preguntar);
            cnn.Dispose();
            return Respuesta;
        }
        public int Consultar(short vCatalogo, string vDescripcion, ref JObject objJSON)
        {
            //IDataReader Registro;
            int Count;
            var cnn = new Conexion();
            string strFiltro;
            string strJSON = "";
            string strConsulta;

            //generar consulta SQL
            strFiltro = " WHERE Catalogo=" + vCatalogo;
            if (vDescripcion.Length > 0) strFiltro = strFiltro + " and Descripcion LIKE " + cnn.nSt(vDescripcion);
            strConsulta = "SELECT Descripcion, Codigo FROM [" + Tabla + "]" + strFiltro + " ORDER BY Descripcion";

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

        public int Listar(short vCatalogo, string vDescripcion, ref string strJSON)
        {
            int Count;
            var cnn = new Conexion();
            string strFiltro;
            string strConsulta;

            //generar consulta SQL
            strFiltro = "";
            if (vDescripcion.Length > 0) strFiltro = strFiltro + " and Descripcion LIKE " + Helper.nSt(vDescripcion);
            strConsulta = "SELECT Descripcion,Codigo FROM [" + Tabla + "] WHERE Catalogo=" + vCatalogo + strFiltro + " ORDER BY Descripcion";

            //obtener registros
            strJSON = "";
            Count = 0;
            cnn.Conectar();
            cnn.SQL = strConsulta;
            IDataReader Registro = cnn.AbreSQL();
            if (cnn.Hay(Registro))
            {
                strJSON = Json.ReaderToJson(Registro, ref Count);
                Registro.Dispose();
            }
            cnn.Dispose();
            return Count;
        }
    }
}
