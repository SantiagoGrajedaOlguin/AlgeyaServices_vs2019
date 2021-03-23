using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Idealsis.Dal.Mapeo
{
    public class SysCatalogosSatConfig
    {
        const string Tabla = "sysCatalogosSatConfig";
        short  codigo;
        string clave;
        string descripcion;
        string etiqueta1;
        string etiqueta2;
        string comentarios;


        public short Codigo { get { return codigo; } set { codigo = value; } }
        public string Clave { get { return clave; } set { clave = value; } }
        public string Descripcion { get { return descripcion; } set { descripcion = value; } }
        public string Etiqueta1 { get { return etiqueta1; } set { etiqueta1 = value; } }
        public string Etiqueta2 { get { return etiqueta2; } set { etiqueta2 = value; } }
        public string Comentarios { get { return comentarios; } set { comentarios = value; } }

        public void Guardar()
        {
            string c;
            string v;
            Conexion cnn = new Conexion();

            cnn.Conectar();
            c = "Codigo,0,1|Clave,1|Descripcion,1|Etiqueta1,1|Etiqueta2,1|Comentarios,1";
            v = codigo + "|" + clave  + "|" + descripcion + "|" + etiqueta1 + "|" + etiqueta2 + "|" + comentarios;
            cnn.GuardarRegistro(Tabla, c, v);
            cnn.Dispose();
        }

        private void AsignarValores(IDataReader Registro)
        {
            codigo = Convert.ToInt16(Registro["Codigo"].ToString());
            clave = Convert.IsDBNull(Registro["Clave"]) ? "" : Registro["Clave"].ToString();
            descripcion = Convert.IsDBNull(Registro["Descripcion"]) ? "" : Registro["Descripcion"].ToString();
            etiqueta1 = Convert.IsDBNull(Registro["Etiqueta1"]) ? "" : Registro["Etiqueta1"].ToString();
            etiqueta2 = Convert.IsDBNull(Registro["Etiqueta2"]) ? "" : Registro["Etiqueta2"].ToString();
            comentarios = Convert.IsDBNull(Registro["Comentarios"]) ? "" : Registro["Comentarios"].ToString();
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

        public bool BuscarPorCodigo(short vCodigo)
        {
            var cnn = new Conexion();
            bool result = false;
            cnn.Conectar();
            IDataReader Registro = cnn.ObtenerRegistro(Tabla, "Codigo=" + vCodigo);
            if (Registro.Read())
            {
                result = true;
                AsignarValores(Registro);
            }
            Registro.Dispose();
            cnn.Dispose();
            return result;
        }

        public bool BuscarPorClave(string vClave)
        {
            var cnn = new Conexion();
            bool result = false;
            cnn.Conectar();
            IDataReader Registro = cnn.ObtenerRegistro(Tabla, "Clave='" + vClave + "'");
            if (Registro.Read())
            {
                result = true;
                AsignarValores(Registro);
            }
            Registro.Dispose();
            cnn.Dispose();
            return result;
        }

        public string ObtenerValor(short vCodigo, string Campo)
        {
            var cnn = new Conexion();
            string result = "";
            cnn.Conectar();
            result = cnn.DameValor(Tabla, "Codigo=" + vCodigo, Campo);
            cnn.Dispose();
            return result;
        }

        public bool BuscarPorDesc(string vDescripcion)
        {
            var cnn = new Conexion();
            bool result = false;
            cnn.Conectar();
            IDataReader Registro = cnn.ObtenerRegistro(Tabla, "Descripcion=" + cnn.strSql(vDescripcion));
            if (Registro.Read())
            {
                result = true;
                AsignarValores(Registro);
            }
            Registro.Dispose();
            cnn.Dispose();
            return result;
        }

        public bool Borrar(short vCodigo, bool Preguntar)
        {
            var cnn = new Conexion();
            bool Respuesta;
            cnn.Conectar();
            Respuesta = cnn.BorrarRegistro(Tabla, "Codigo=" + vCodigo, Preguntar);
            cnn.Dispose();
            return Respuesta;
        }
        public int Consultar(string vDescripcion, ref JObject objJSON)
        {
            //IDataReader Registro;
            int Count;
            var cnn = new Conexion();
            string strFiltro;
            string strJSON = "";
            string strConsulta;

            //generar consulta SQL
            strFiltro = " WHERE Codigo>0";
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

        public int Listar(string vDescripcion, ref string strJSON)
        {
            int Count;
            var cnn = new Conexion();
            string strFiltro;
            string strConsulta;

            //generar consulta SQL
            strFiltro = "";
            if (vDescripcion.Length > 0) strFiltro = strFiltro + " and Descripcion LIKE " + Helper.nSt(vDescripcion);
            strConsulta = "SELECT Descripcion,Codigo FROM [" + Tabla + "] WHERE Codigo>0" + strFiltro + " ORDER BY Descripcion";

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
        public int SiguienteCodigo()
        {
            int vCodigo;
            var cnn = new Conexion();
            cnn.Conectar();
            vCodigo = 1; if (cnn.ExisteRegistro(Tabla, "SELECT Max(Codigo) as c FROM [" + Tabla + "]", "c")) vCodigo = Convert.ToInt32(Helper.vg_DAT) + 1;
            cnn.Dispose();
            return vCodigo;
        }

    }
}
