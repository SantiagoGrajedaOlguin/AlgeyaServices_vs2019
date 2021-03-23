
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace Idealsis.Dal.Mapeo
{
    public class SysCatalogosSat
    {
        const string Tabla = "sysCatalogosSat";

        int    id;
        short  catalogo;
        string codigo;
        string descripcion;
        string valor1;
        string valor2;
        byte   porDefecto;

        public int Id { get { return id; } set { id = value; } }
        public short Catalogo { get { return catalogo; } set { catalogo = value; } }
        public string Codigo { get { return codigo; } set { codigo = value; } }
        public string Descripcion { get { return descripcion; } set { descripcion = value; } }
        public string Valor1 { get { return valor1; } set { valor1 = value; } }
        public string Valor2 { get { return valor2; } set { valor2 = value; } }
        public byte PorDefecto { get { return porDefecto; } set { porDefecto = value; } }


        public int Guardar()
        {
            string c;
            string v;
            var cnn = new Conexion();

            cnn.Conectar();
            c = "Id,0,9|Catalogo|Codigo,1|Descripcion,1|Valor1,1|Valor2,1|PorDefecto";
            v = id + "|" + catalogo + "|" + codigo + "|" + descripcion + "|" + valor1 + "|" + valor2 + "|" + porDefecto;
            id = cnn.GuardarRegistro(Tabla, c, v);
            cnn.Dispose();
            return id;
        }

        private void AsignarValores(IDataReader Registro)
        {
            id = Convert.ToInt32(Registro["Id"].ToString());
            catalogo = Convert.ToInt16(Convert.IsDBNull(Registro["Catalogo"]) ? 0 : Registro["Catalogo"]);
            codigo = Convert.IsDBNull(Registro["Codigo"]) ? "" : Registro["Codigo"].ToString();
            descripcion = Convert.IsDBNull(Registro["Descripcion"]) ? "" : Registro["Descripcion"].ToString();
            valor1 = Convert.IsDBNull(Registro["Valor1"]) ? "" : Registro["Valor1"].ToString();
            valor2 = Convert.IsDBNull(Registro["Valor2"]) ? "" : Registro["Valor2"].ToString();
            porDefecto = Convert.ToByte(Convert.IsDBNull(Registro["PorDefecto"]) ? 0 : Registro["PorDefecto"]);
        }

        public bool BuscarPorId(int vId)
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

        public bool BuscarPorCodigo(short vCatalogo, string vCodigo)
        {
            var cnn = new Conexion();
            bool result = false;
            cnn.Conectar();
            IDataReader Registro = cnn.ObtenerRegistro(Tabla, "Catalogo=" + vCatalogo + " and Codigo='" + vCodigo + "'");
            if (Registro.Read())
            {
                result = true;
                AsignarValores(Registro);
            }
            Registro.Dispose();
            cnn.Dispose();
            return result;
        }

        public bool Existe(short vCatalogo, string vCodigo, string vDesc)
        {
            var cnn = new Conexion();
            bool result;
            cnn.Conectar();
            result = cnn.ExisteRegistro(Tabla, "Catalogo=" + vCatalogo + (vCodigo.Length > 0 ? " and Codigo='" + vCodigo + "'" : "") + (vDesc.Length > 0 ? " and Descripcion='" + vDesc + "'" : ""), "Id");
            cnn.Dispose();
            return result;
        }

        public bool BuscarPorDesc(short vCatalogo, string vDescripcion)
        {
            var cnn = new Conexion();
            bool result = false;
            cnn.Conectar();
            IDataReader Registro = cnn.ObtenerRegistro(Tabla, "Catalogo = " + vCatalogo + " and Descripcion = " + cnn.strSql(vDescripcion));
            if (Registro.Read())
            {
                result = true;
                AsignarValores(Registro);
            }
            Registro.Dispose();
            cnn.Dispose();
            return result;
        }

        public string GetCod(int vId)
        {
            var cnn = new Conexion();
            string result;
            cnn.Conectar();
            result = cnn.DameValor(Tabla, "Id=" + vId, "Codigo");
            cnn.Dispose();
            return result;
        }
        public string GetDes(short vCatalogo, string vCodigo)
        {
            var cnn = new Conexion();
            string result;
            cnn.Conectar();
            result = cnn.DameValor(Tabla, "Catalogo=" + vCatalogo + " and Codigo='" + vCodigo + "'", "Descripcion");
            cnn.Dispose();
            return result;
        }
        public string GetDesCat(short vCatalogo)
        {
            SysCatalogosSatConfig Catalogo = new SysCatalogosSatConfig();
            string result;
            result = Catalogo.ObtenerValor(vCatalogo, "Descripcion");
            return result;
        }

        public string ObtenerValor(int vId, string Campo)
        {
            var cnn = new Conexion();
            string result;
            cnn.Conectar();
            result = cnn.DameValor(Tabla, "Id=" + vId, Campo);
            cnn.Dispose();
            return result;
        }

        public bool BorrarPorId(int vId, bool Preguntar)
        {
            var cnn = new Conexion();
            bool Respuesta;
            cnn.Conectar();
            Respuesta = cnn.BorrarRegistro(Tabla, "Id=" + vId, Preguntar);
            cnn.Dispose();
            return Respuesta;
        }
        public int Consultar(int vCatalogo, string vDescripcion, ref JObject objJSON)
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

    }
}
