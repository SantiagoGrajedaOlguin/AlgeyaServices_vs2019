
using System;
using System.Data;

namespace Idealsis.Dal.Mapeo
{
    public class CatDocumentosTokens
    {
        const string Tabla = "CatDocumentosTokens";

        string codigo;
        string descripcion;
        int    tipoDato;
        string formato;

        public string Codigo { get { return codigo; } set { codigo = value; } }
        public string Descripcion { get { return descripcion; } set { descripcion = value; } }
        public int TipoDato { get { return tipoDato; } set { tipoDato = value; } }
        public string Formato { get { return formato; } set { formato = value; } }

        public void Guardar()
        {
            string c;
            string v;
            var cnn = new Conexion();

            cnn.Conectar();
            c = "Codigo,0,1|Descripcion,1|TipoDato|Formato,1";
            v = codigo + "|" + descripcion + "|" + tipoDato + "|" + formato;

            cnn.GuardarRegistro(Tabla, c, v);
            cnn.Dispose();
        }

        private void AsignarValores(IDataReader Registro)
        {
            codigo = Convert.IsDBNull(Registro["Codigo"]) ? "" : Registro["Codigo"].ToString();
            descripcion = Convert.IsDBNull(Registro["Descripcion"]) ? "" : Registro["Descripcion"].ToString();
            tipoDato = Convert.ToByte(Convert.IsDBNull(Registro["TipoDato"]) ? 0 : Registro["TipoDato"]);
            formato = Convert.IsDBNull(Registro["Formato"]) ? "" : Registro["Formato"].ToString();
            Registro.Dispose();
        }

        public bool BorrarPorId(string vCodigo)
        {
            var cnn = new Conexion();
            bool Respuesta;
            cnn.Conectar();
            Respuesta = cnn.BorrarRegistro(Tabla, "Codigo=" + cnn.strSql(vCodigo), false);
            /*
            if (Respuesta)
            {
                cnn.RegistrarBitacora("Documento token", "Borrar, Codigo: " + vCodigo);
            }
            */
            cnn.Dispose();
            return Respuesta;
        }

        public DataTable Listar(string vDescripcion)
        {
            var cnn = new Conexion();
            string strFiltro;
            string strConsulta;

            //generar consulta SQL
            strFiltro = "";
            if (vDescripcion.Length > 0) strFiltro = strFiltro + " and Descripcion LIKE " + Helper.nSt(vDescripcion);
            strConsulta = "SELECT Codigo,Descripcion,TipoDato,Formato FROM [" + Tabla + "] WHERE Len(Codigo)>0" + strFiltro + " ORDER BY Descripcion";

            //obtener registros
            cnn.Conectar();
            DataTable Registro = cnn.GetTabla(strConsulta);
            cnn.Dispose();
            return Registro;
        }

    }
}
