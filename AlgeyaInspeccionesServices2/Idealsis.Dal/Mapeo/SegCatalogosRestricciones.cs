
using Newtonsoft.Json.Linq;
using System;
using System.Data;

namespace Idealsis.Dal.Mapeo
{
    public class SegCatalogosRestricciones
    {
        const string Tabla = "SegCatalogosRestricciones";
        int    id;
        int    usuario;
        int    perfil;
        short  tipo;
        short  catalogo;
        int    codigo;
        string cuenta;
        string descripcion;
        int    padre;
        short  valor;
        byte   esPred;
        short  posicion;

        public int Id { get { return id; } set { id = value; } }
        public int Usuario { get { return usuario; } set { usuario = value; } }
        public int Perfil { get { return perfil; } set { perfil = value; } }
        public short Tipo { get { return tipo; } set { tipo = value; } }
        public short Catalogo { get { return catalogo; } set { catalogo = value; } }
        public int Codigo { get { return codigo; } set { codigo = value; } }
        public string Cuenta { get { return cuenta; } set { cuenta = value; } }
        public string Descripcion { get { return descripcion; } set { descripcion = value; } }
        public int Padre { get { return padre; } set { padre = value; } }
        public short Valor { get { return valor; } set { valor = value; } }
        public byte EsPred { get { return esPred; } set { esPred = value; } }
        public short Posicion { get { return posicion; } set { posicion = value; } }

        public int Guardar(bool ObtenerId)
        {
            string c;
            string v;
            var cnn = new Conexion();

            cnn.Conectar();
            if (ObtenerId) id = (int)Helper.Val(cnn.DameValor(Tabla, "Usuario=" + usuario + " and Perfil=" + perfil + " and Tipo=" + tipo + " and Codigo=" + Codigo, "Id"));

            c = "Id,0,9|Usuario|Perfil|Tipo|Catalogo|Codigo|Cuenta,1|Descripcion,1|Padre|Valor|EsPred|Posicion";
            v = id + "|" + usuario + "|" + perfil + "|" + tipo + "|" + catalogo + "|" + codigo + "|" + cuenta + "|" + descripcion + "|" + padre + "|" + valor + "|" + esPred + "|" + posicion;
            id = cnn.GuardarRegistro(Tabla, c, v);
            cnn.Dispose();
            return id;
        }

        private void AsignarValores(IDataReader Registro)
        {
            id = Convert.ToInt32(Registro["Id"].ToString());
            usuario = Convert.ToInt32(Convert.IsDBNull(Registro["Usuario"]) ? 0 : Registro["Usuario"]);
            perfil = Convert.ToInt32(Convert.IsDBNull(Registro["Perfil"]) ? 0 : Registro["Perfil"]);
            tipo = Convert.ToInt16(Convert.IsDBNull(Registro["Tipo"]) ? 0 : Registro["Tipo"]);
            catalogo = Convert.ToInt16(Convert.IsDBNull(Registro["Catalogo"]) ? 0 : Registro["Catalogo"]);
            codigo = Convert.ToInt32(Convert.IsDBNull(Registro["Codigo"]) ? 0 : Registro["Codigo"]);
            cuenta = Convert.IsDBNull(Registro["Cuenta"]) ? "" : Registro["Cuenta"].ToString();
            descripcion = Convert.IsDBNull(Registro["Descripcion"]) ? "" : Registro["Descripcion"].ToString();
            padre = Convert.ToInt32(Convert.IsDBNull(Registro["Padre"]) ? 0 : Registro["Padre"]);
            valor = Convert.ToInt16(Convert.IsDBNull(Registro["Valor"]) ? 0 : Registro["Valor"]);
            esPred = Convert.ToByte(Convert.IsDBNull(Registro["EsPred"]) ? 0 : Registro["EsPred"]);
            posicion = Convert.ToInt16(Convert.IsDBNull(Registro["Posicion"]) ? 0 : Registro["Posicion"]);
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
        public bool ConsultarPorUsuario(int vUsuario, int vPerfil, int vTipo, int vCatalogo, int vCodigo)
        {
            var cnn = new Conexion();
            bool result = false;
            cnn.Conectar();
            IDataReader Registro = cnn.ObtenerRegistro(Tabla, "Usuario=" + vUsuario + " and Perfil=" + vPerfil + " and Tipo=" + vTipo + " and Catalogo=" + vCatalogo + " and Codigo=" + vCodigo);
            if (Registro.Read())
            {
                result = true;
                AsignarValores(Registro);
            }
            Registro.Dispose();
            cnn.Dispose();
            return result;
        }

        public void BorrarPorId(int vId)
        {
            var cnn = new Conexion();
            cnn.Conectar();
            cnn.Exec("DELETE FROM [" + Tabla + "] WHERE Id=" + vId);
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

        public int Consultar(int vUsuario, int vPerfil, short vTipo, ref JObject objJSON)
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
            if (vTipo>0) strFiltro = strFiltro + " and Tipo=" + vTipo;
            strConsulta = "SELECT Id,Usuario,Perfil,Tipo,Catalogo,Posicion,Codigo,Descripcion,Valor,EsPred,Padre,Cuenta FROM [" + Tabla + "] WHERE Id>0" + strFiltro + " ORDER BY Tipo,Posicion";

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

        public IDataReader Listar(int vUsuario, int vPerfil, short vTipo)
        {
            var cnn = new Conexion();
            string strFiltro;
            string strConsulta;

            //generar consulta SQL
            strFiltro = "";
            strFiltro = strFiltro + " and Usuario=" + vUsuario;
            strFiltro = strFiltro + " and Perfil=" + vPerfil;
            if (vTipo > 0) strFiltro = strFiltro + " and Tipo=" + vTipo;
            strConsulta = "SELECT Id,Usuario,Perfil,Tipo,Catalogo,Posicion,Codigo,Descripcion,Valor,EsPred,Padre,Cuenta FROM [" + Tabla + "] WHERE Id>0" + strFiltro + " ORDER BY Tipo,Posicion";

            //obtener registros
            cnn.Conectar();
            cnn.SQL = strConsulta;
            IDataReader Registro = cnn.AbreSQL();
            cnn.Dispose();
            return Registro;
        }

        public DataTable ListarTabla(int vUsuario, int vPerfil, short vTipo)
        {
            var cnn = new Conexion();
            string strFiltro;
            string strConsulta;

            //generar consulta SQL
            strFiltro = "";
            strFiltro = strFiltro + " and Usuario=" + vUsuario;
            strFiltro = strFiltro + " and Perfil=" + vPerfil;
            if (vTipo > 0) strFiltro = strFiltro + " and Tipo=" + vTipo;
            strConsulta = "SELECT Id,Usuario,Perfil,Tipo,Catalogo,Posicion,Codigo,Descripcion,Valor,EsPred,Padre,Cuenta FROM [" + Tabla + "] WHERE Id>0" + strFiltro + " ORDER BY Tipo,Posicion";

            //obtener registros
            cnn.Conectar();
            DataTable Registro = cnn.GetTabla(strConsulta);
            cnn.Dispose();
            return Registro;
        }
    }
}
