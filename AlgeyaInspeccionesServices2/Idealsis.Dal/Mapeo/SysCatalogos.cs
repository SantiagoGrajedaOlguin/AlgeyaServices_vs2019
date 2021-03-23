using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace Idealsis.Dal.Mapeo
{
    public class SysCatalogos
    {
        const string Tabla = "SysCatalogos";

        int    id;
        short  catalogo;
        int    codigo;
        string descripcion;
        string corta;
        string claveSat;
        int    valorNum;
        float  valorDec;
        string valorStr;
        int    idPadre;
        int    idHijo;
        byte   bandera1;
        byte   bandera2;
        byte   esPred;
        byte   activo;

        public int Id { get { return id; } set { id = value; } }
        public short Catalogo { get { return catalogo; } set { catalogo = value; } }
        public int Codigo { get { return codigo; } set { codigo = value; } }
        public string Descripcion { get { return descripcion; } set { descripcion = value; } }
        public string Corta { get { return corta; } set { corta = value; } }
        public string ClaveSat { get { return claveSat; } set { claveSat = value; } }
        public int ValorNum { get { return valorNum; } set { valorNum = value; } }
        public float ValorDec { get { return valorDec; } set { valorDec = value; } }
        public string ValorStr { get { return valorStr; } set { valorStr = value; } }
        public int IdPadre { get { return idPadre; } set { idPadre = value; } }
        public int IdHijo { get { return idHijo; } set { idHijo = value; } }
        public byte Bandera1 { get { return bandera1; } set { bandera1 = value; } }
        public byte Bandera2 { get { return bandera2; } set { bandera2 = value; } }
        public byte EsPred { get { return esPred; } set { esPred = value; } }
        public byte Activo { get { return activo; } set { activo = value; } }

        public SysCatalogos()
        {
        }
        public SysCatalogos(short vCatalogo)
        {
            catalogo = vCatalogo;
        }

        public int Guardar()
        {
            string c;
            string v;
            var cnn = new Conexion();

            cnn.Conectar();
            c = "Id,0,9|Catalogo|Codigo|Descripcion,1|Corta,1|ClaveSat,1|ValorNum|ValorDec|ValorStr,1|IdPadre|IdHijo|Bandera1|Bandera2|EsPred|Activo";
            v = id + "|" + catalogo + "|" + codigo + "|" + descripcion + "|" + corta + "|" + claveSat + "|" + valorNum + "|" + valorDec + "|" + valorStr + "|" +  idPadre + "|" + idHijo + "|" + bandera1 + "|" + bandera2 + "|" + esPred + "|" + activo;
            id = cnn.GuardarRegistro(Tabla, c, v);
            cnn.Dispose();
            return id;
        }

        private void AsignarValores(IDataReader Registro)
        {
            id = Convert.ToInt32(Registro["Id"].ToString());
            catalogo = Convert.ToInt16(Convert.IsDBNull(Registro["Catalogo"]) ? 0 : Registro["Catalogo"]);
            codigo = Convert.ToInt32(Convert.IsDBNull(Registro["Codigo"]) ? 0: Registro["Codigo"]);
            descripcion = Convert.IsDBNull(Registro["Descripcion"])?"":Registro["Descripcion"].ToString();
            corta = Convert.IsDBNull(Registro["Corta"]) ? "" : Registro["Corta"].ToString();
            claveSat = Convert.IsDBNull(Registro["ClaveSat"]) ? "" : Registro["ClaveSat"].ToString();
            valorNum = Convert.ToInt32(Convert.IsDBNull(Registro["ValorNum"]) ? 0 : Registro["ValorNum"]);
            valorDec = Convert.ToSingle(Convert.IsDBNull(Registro["ValorDec"]) ? 0 : Registro["ValorDec"]);
            valorStr = Convert.IsDBNull(Registro["ValorStr"]) ? "" : Registro["ValorStr"].ToString();
            idPadre = Convert.ToInt32(Convert.IsDBNull(Registro["IdPadre"]) ? 0 : Registro["IdPadre"]); 
            idHijo = Convert.ToInt32(Convert.IsDBNull(Registro["IdHijo"]) ? 0 : Registro["IdHijo"]);
            bandera1 = Convert.ToByte(Convert.IsDBNull(Registro["Bandera1"]) ? 0 : Registro["Bandera1"]);
            bandera2 = Convert.ToByte(Convert.IsDBNull(Registro["Bandera2"]) ? 0 : Registro["Bandera2"]);
            esPred = Convert.ToByte(Convert.IsDBNull(Registro["EsPred"]) ? 0 : Registro["EsPred"]); 
            activo = Convert.ToByte(Convert.IsDBNull(Registro["Activo"]) ? 0 : Registro["Activo"]); 
        }
        public bool Desplegar(int Direccion, short vCatalogo, short vCatPadre, int vIdPadre, int vCodigo, int vId)
        {
            var cnn = new Conexion();
            bool Result = false;
            cnn.Conectar();
            IDataReader Registro = cnn.MoverPadre(Tabla, Direccion, vCatalogo, vCatPadre, vIdPadre, vCodigo, vId);
            if (Registro.Read())
            {
                Result = true;
                AsignarValores(Registro);
            }
            Registro.Dispose();
            cnn.Dispose();
            return Result;
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

        public bool BuscarPorCodigo(short vCatalogo, int vIdPadre, int vCodigo)
        {
            var cnn = new Conexion();
            bool result = false;
            cnn.Conectar();
            IDataReader Registro = cnn.ObtenerRegistro(Tabla, "Catalogo=" + vCatalogo + (vIdPadre>0?" and IdPadre=" + vIdPadre:"") + " and Codigo=" + vCodigo);
            if (Registro.Read())
            {
                result = true;
                AsignarValores(Registro);
            }
            Registro.Dispose();
            cnn.Dispose();
            return result;
        }
        public bool Existe(short vCatalogo, int vIdPadre, int vCodigo, string vDesc)
        {
            var cnn = new Conexion();
            bool result;
            cnn.Conectar();
            result = cnn.ExisteRegistro(Tabla, "Catalogo=" + vCatalogo + (vIdPadre>0?" and IdPadre=" + vIdPadre:"") + (vCodigo>0?" and Codigo=" + vCodigo:"") + (vDesc.Length  > 0 ? " and Descripcion='" + vDesc + "'" : ""),"Id");
            cnn.Dispose();
            return result;
        }

        public bool BuscarPorDesc(int vCatalogo, string vDescripcion)
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

        public int GetCod(int vId)
        {
            var cnn = new Conexion();
            int result;
            cnn.Conectar();
            result = Convert.ToInt32(cnn.DameValor(Tabla, "Id=" + vId, "Codigo"));
            cnn.Dispose();
            return result;
        }
        public int GetId(short vCatalogo, int vCodigo)
        {
            var cnn = new Conexion();
            int result;
            cnn.Conectar();
            result = Convert.ToInt32(cnn.DameValor(Tabla, "Catalogo=" + vCatalogo + " and Codigo=" + vCodigo, "Id"));
            cnn.Dispose();
            return result;
        }
        public string GetDes(int vId)
        {
            var cnn = new Conexion();
            string result;
            cnn.Conectar();
            result = cnn.DameValor(Tabla, "Id=" + vId, "Descripcion");
            cnn.Dispose();
            return result;
        }

        public string GetDesCat(short vCatalogo)
        {
            SysCatalogosConfig Catalogo = new SysCatalogosConfig();
            string result = Catalogo.ObtenerValor(vCatalogo, "Descripcion");
            return result;
        }

        public string ObtenerValor(int vId, string Campo)
        {

            var cnn = new Conexion();
            cnn.Conectar();
            string result = cnn.DameValor(Tabla, "Id=" + vId, Campo);
            cnn.Dispose();
            return result;
        }

        public string ObtenerValorConfig(short vCatalogo, string Campo)
        {
            SysCatalogosConfig Catalogo = new SysCatalogosConfig();
            string result = Catalogo.ObtenerValor(vCatalogo, Campo);
            return result;
        }

        public bool BorrarPorId(int vId, bool Preguntar)
        {
            var cnn = new Conexion();
            bool Respuesta;
            cnn.Conectar();
            Respuesta = cnn.BorrarRegistro(Tabla, "Id=" + vId, Preguntar);
            cnn.Exec("DELETE FROM sysCatalogosDetalle WHERE IdOrigen=" + vId);
            cnn.Exec("DELETE FROM sysDatosValor WHERE Origen=2 and IdOrigen=" + vId);
            cnn.Dispose();
            return Respuesta;
        }
        public int Consultar(int vCatalogo, int vIdPadre, string vDescripcion, ref JObject objJSON)
        {
            //IDataReader Registro;
            int Count;
            var cnn = new Conexion();
            string strFiltro;
            string strJSON = "";
            string strConsulta;

            //generar consulta SQL
            strFiltro = " WHERE Catalogo=" + vCatalogo;
            if (vIdPadre > 0) strFiltro = strFiltro + " and IdPadre=" + vIdPadre;
            if (vDescripcion.Length > 0) strFiltro = strFiltro + " and Descripcion LIKE " + cnn.nSt(vDescripcion);
            strConsulta = "SELECT Id,Catalogo,Codigo,Descripcion,Corta,Activo FROM [" + Tabla + "]" + strFiltro + " ORDER BY Id";

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

        public IDataReader Listar(int vCatalogo, int vIdPadre, string vDescripcion)
        {
            var cnn = new Conexion();
            string strFiltro;
            string strConsulta;

            //generar consulta SQL
            strFiltro = " WHERE Catalogo=" + vCatalogo;
            if (vIdPadre > 0) strFiltro = strFiltro + " and IdPadre=" + vIdPadre;
            if (vDescripcion.Length > 0) strFiltro = strFiltro + " and Descripcion LIKE " + cnn.nSt(vDescripcion);
            strConsulta = "SELECT Id,Catalogo,Codigo,Descripcion,Corta,Activo FROM [" + Tabla + "]" + strFiltro + " ORDER BY Id";

            //obtener registros
            cnn.Conectar();
            cnn.SQL = strConsulta;
            IDataReader Registro = cnn.AbreSQL();
            cnn.Dispose();
            return Registro;
        }

        public DataTable ListarTabla(int vCatalogo, int vIdPadre, string vDescripcion)
        {
            var cnn = new Conexion();
            string strConsulta;
            string strFiltro;
            
            //generar consulta SQL
            strFiltro = " WHERE Catalogo=" + vCatalogo;
            if (vIdPadre > 0) strFiltro = strFiltro + " and IdPadre=" + vIdPadre;
            if (vDescripcion.Length > 0) strFiltro = strFiltro + " and Descripcion LIKE " + cnn.nSt(vDescripcion);
            strConsulta = "SELECT Id,Catalogo,Codigo,Descripcion,Corta,IdPadre,IdHijo,EsPred,ValorStr,ClaveSat,Bandera1,Bandera2,Activo FROM [" + Tabla + "]" + strFiltro + " ORDER BY Codigo";
            
            //obtener registros
            cnn.Conectar();
            DataTable Registro = cnn.GetTabla(strConsulta);
            //DataTable Registro = cnn.GetSpTabla("Sp_SysCatalogosListar", vCatalogo);
            cnn.Dispose();
            return Registro;
        }


        public int SiguienteCodigo(short vCatalogo, short vCatPadre, int vIdPadre)
        {
            int vCodigo;
            if (vCatPadre > 0 && vIdPadre == 0)
            {
                vCodigo = 0;
            }
            else
            {
                var cnn = new Conexion();
                cnn.Conectar();
                vCodigo = 1; if (cnn.ExisteRegistro(Tabla, "SELECT Max(Codigo) as c FROM [" + Tabla + "] WHERE Catalogo=" + vCatalogo + (vCatPadre > 0 ? " and IdPadre=" + vIdPadre : ""), "c")) vCodigo = Convert.ToInt32(Helper.vg_DAT.Length>0? Helper.vg_DAT:"0") + 1;
                cnn.Dispose();
            }
            return vCodigo;
        }

    }
}
