
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace Idealsis.Dal.Mapeo
{
    public class SysDatos
    {
        const string Tabla = "SysDatos";

        int    id;
        short  catalogo;
        int    codigo;
        string descripcion;
        byte   esEtiqueta;
        byte   tipo;
        short  formato;
        string formatoDes;
        string formatoCap;

        public int Id { get { return id; } set { id = value; } }
        public short Catalogo { get { return catalogo; } set { catalogo = value; } }
        public int Codigo { get { return codigo; } set { codigo = value; } }
        public string Descripcion { get { return descripcion; } set { descripcion = value; } }
        public byte EsEtiqueta { get { return esEtiqueta; } set { esEtiqueta = value; } }
        public byte Tipo { get { return tipo; } set { tipo = value; } }
        public short Formato { get { return formato; } set { formato = value; } }
        public string FormatoDes { get { return formatoDes; } set { formatoDes = value; } }
        public string FormatoCap { get { return formatoCap; } set { formatoCap = value; } }

        public int Guardar()
        {
            string c;
            string v;
            var cnn = new Conexion();

            cnn.Conectar();
            c = "Id,0,9|Catalogo|Codigo|Descripcion,1|EsEtiqueta|Tipo|Formato|FormatoDes,1|FormatoCap,1";
            v = id + "|"  + catalogo + "|" + codigo + "|" + descripcion + "|" + esEtiqueta + "|" + tipo + "|" + formato + "|" + formatoDes + "|" + formatoCap;
            id = cnn.GuardarRegistro(Tabla, c, v);
            cnn.Dispose();
            return id;
        }

        private void AsignarValores(IDataReader Registro)
        {
            id = Convert.ToInt32(Convert.IsDBNull(Registro["Id"]) ? 0 : Registro["Id"]);
            catalogo = Convert.ToInt16(Convert.IsDBNull(Registro["Catalogo"]) ? 0 : Registro["Catalogo"]);
            codigo = Convert.ToInt32(Convert.IsDBNull(Registro["Codigo"]) ? 0 : Registro["Codigo"]);
            descripcion = Convert.IsDBNull(Registro["Descripcion"]) ? "" : Registro["Descripcion"].ToString();
            esEtiqueta = Convert.ToByte(Convert.IsDBNull(Registro["EsEtiqueta"]) ? 0 : Registro["EsEtiqueta"]);
            tipo = Convert.ToByte(Convert.IsDBNull(Registro["Tipo"]) ? 0 : Registro["Tipo"]);
            formato = Convert.ToInt16(Convert.IsDBNull(Registro["Formato"]) ? 0 : Registro["Formato"]);
            formatoDes = Convert.IsDBNull(Registro["FormatoDes"]) ? "" : Registro["FormatoDes"].ToString();
            formatoCap = Convert.IsDBNull(Registro["FormatoCap"]) ? "" : Registro["FormatoCap"].ToString();
        }

        public bool BuscarPorCodigo(short vCatalogo, int vCodigo)
        {
            var cnn = new Conexion();
            bool result = false;
            cnn.Conectar();
            IDataReader Registro = cnn.ObtenerRegistro(Tabla, "Catalogo=" + vCatalogo + " and Codigo=" + vCodigo);
            if (Registro.Read())
            {
                result = true;
                AsignarValores(Registro);
            }
            Registro.Dispose();
            cnn.Dispose();
            return result;
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

        public bool Existe(short vCatalogo, short vCodigo)
        {
            var cnn = new Conexion();
            bool result = false;
            cnn.Conectar();
            result = cnn.ExisteRegistro(Tabla, "Catalogo=" + vCatalogo + " and Codigo=" + vCodigo, "Codigo");
            cnn.Dispose();
            return result;
        }

        public string ObtenerValor(short vCatalogo, short vCodigo, string Campo)
        {
            var cnn = new Conexion();
            string result = "";
            cnn.Conectar();
            result = cnn.DameValor(Tabla, "Catalogo=" + vCatalogo + " and Codigo=" + vCodigo, Campo);
            cnn.Dispose();
            return result;
        }

        public bool BorrarPorId(int vId)
        {
            var cnn = new Conexion();
            bool Respuesta;
            bool EstaRelacionado = false;
            cnn.Conectar();
           
            EstaRelacionado = cnn.ExisteRegistro("SysCatalogosDetalle", "Tipo=1 and IdDato=" + vId,"");
            if (EstaRelacionado)
            {
                cnn.Dispose();
                throw new Exception("Elimación no permitida\n El dato ya se encuentra relacionado a otra tabla");
            }
            else
            {
                Respuesta = cnn.BorrarRegistro(Tabla, "Id=" + vId, false);
                cnn.Dispose();
            }
            return Respuesta;
        }

        public DataTable ListarTabla(short vCatalogo, int vDato, string vDescripcion)
        {
            var cnn = new Conexion();
            string strFiltro;
            string strConsulta;

            //generar consulta SQL
            strFiltro = " WHERE Catalogo=" + vCatalogo;
            if (vDato > 0) strFiltro = strFiltro + " and Id=" + vDato;
            if (vDescripcion.Length > 0) strFiltro = strFiltro + " and Descripcion LIKE " + cnn.nSt(vDescripcion);
            strConsulta = "SELECT Id,Catalogo,Codigo,Descripcion,EsEtiqueta,Tipo,Formato,FormatoDes,FormatoCap FROM [" + Tabla + "]" + strFiltro + " ORDER BY Id";

            //obtener registros
            cnn.Conectar();
            DataTable Registro = cnn.GetTabla(strConsulta);
            cnn.Dispose();
            return Registro;
        }

        public int SiguienteCodigo(short vCatalogo)
        {
            int vCodigo;
            var cnn = new Conexion();
            cnn.Conectar();
            vCodigo = 1; if (cnn.ExisteRegistro(Tabla, "SELECT Max(Codigo) as c FROM [" + Tabla + "] WHERE Catalogo=" + vCatalogo, "c")) vCodigo = Convert.ToInt32(Helper.vg_DAT.Length > 0 ? Helper.vg_DAT : "0") + 1;
            cnn.Dispose();
            return vCodigo;
        }

    }
}
