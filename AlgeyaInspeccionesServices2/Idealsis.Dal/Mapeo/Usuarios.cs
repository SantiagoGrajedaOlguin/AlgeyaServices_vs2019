using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace Idealsis.Dal.Mapeo
{
    public class Usuarios
    {
        const string Tabla = "SegCatalogos";

        int    id;
        short  catalogo;
        string codigo;
        string descripcion;
        string password;
        int    idPadre;
        int    idDireccion;
        short  orden;
        byte   nivel;
        short  icono;
        int    numMensajes;
        byte   activo;

        public int Id { get { return id; } set { id = value; } }
        public short Catalogo { get { return catalogo; } set { catalogo = value; } }
        public string Codigo { get { return codigo; } set { codigo = value; } }
        public string Descripcion { get { return descripcion; } set { descripcion = value; } }
        public string Password { get { return password; } set { password = value; } }
        public int IdPadre { get { return idPadre; } set { idPadre = value; } }
        public int IdDireccion { get { return idDireccion; } set { idDireccion = value; } }
        public short Orden { get { return orden; } set { orden = value; } }
        public byte Nivel { get { return nivel; } set { nivel = value; } }
        public short Icono { get { return icono; } set { icono = value; } }
        public int NumMensajes { get { return numMensajes; } set { numMensajes = value; } }
        public byte Activo { get { return activo; } set { activo = value; } }

        public Usuarios()
        {
        }

        public Usuarios(short vCatalogo)
        {
            catalogo = vCatalogo;
        }

        public int Guardar()
        {
            string c;
            string v;
            var cnn = new Conexion();

            cnn.Conectar();
            c = "Id,0,9|Catalogo|Codigo,1|Descripcion,1|Password,1|IdPadre|IdDireccion|Orden|Nivel|Icono|NumMensajes|Activo";
            v = id + "|" + catalogo + "|" + codigo + "|" + descripcion + "|" + password + "|" + idPadre + "|" + idDireccion + "|" + orden + "|" + nivel + "|" + icono + "|" + numMensajes + "|" + activo;
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
            password = Convert.IsDBNull(Registro["Password"]) ? "" : Registro["Password"].ToString();
            idPadre = Convert.ToInt32(Convert.IsDBNull(Registro["IdPadre"]) ? 0 : Registro["IdPadre"]);
            idDireccion = Convert.ToInt32(Convert.IsDBNull(Registro["IdDireccion"]) ? 0 : Registro["IdDireccion"]);
            orden = Convert.ToInt16(Convert.IsDBNull(Registro["Orden"]) ? 0 : Registro["Orden"]);
            nivel = Convert.ToByte(Convert.IsDBNull(Registro["Nivel"]) ? 0 : Registro["Nivel"]);
            icono = Convert.ToInt16(Convert.IsDBNull(Registro["Icono"]) ? 0 : Registro["Icono"]);
            numMensajes = Convert.ToInt32(Convert.IsDBNull(Registro["NumMensajes"]) ? 0 : Registro["NumMensajes"]);
            activo = Convert.ToByte(Convert.IsDBNull(Registro["Activo"]) ? 0 : Registro["Activo"]);
        }
        public bool Desplegar(int Direccion, short vCatalogo, string vCodigo)
        {
            var cnn = new Conexion();
            bool Result = false;
            cnn.Conectar();
            IDataReader Registro = cnn.MoverCatalogo(Tabla, Direccion, vCatalogo, vCodigo);
            if (Registro.Read())
            {
                Result = true;
                AsignarValores(Registro);
            }
            Registro.Dispose();
            cnn.Dispose();
            return Result;
        }
        public int GetPerfil()
        {
            int Result = 0;
            SegCatalogosDetalle detalle = new SegCatalogosDetalle();
            Result = detalle.ObtenerIdPerfil(id);
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
            result = cnn.ExisteRegistro(Tabla, "Catalogo=" + vCatalogo + " and Codigo='" + vCodigo + "'" + (vDesc.Length > 0 ? " and Descripcion='" + vDesc + "'" : ""), "Id");
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
        public int GetId(short vCatalogo, string vCodigo)
        {
            var cnn = new Conexion();
            int result;
            cnn.Conectar();
            result = Convert.ToInt32(cnn.DameValor(Tabla, "Catalogo=" + vCatalogo + " and Codigo='" + vCodigo + "'", "Id"));
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

        public void BorrarPerfil(int vUsuario, int vPerfil)
        {
            var cnn = new Conexion();
            cnn.Conectar();
            cnn.Exec("DELETE FROM SegCatalogosDetalle WHERE Usuario=" + vUsuario + " and Perfil=" + vPerfil );
            cnn.Exec("DELETE FROM SegCatalogosOpciones WHERE Usuario=" + vUsuario + " and Perfil=" + vPerfil);
            cnn.Exec("DELETE FROM SegCatalogosRestricciones WHERE Usuario=" + vUsuario + " and Perfil=" + vPerfil);
            cnn.Dispose();
        }
        public bool BorrarPorId(int vId, bool Preguntar)
        {
            var cnn = new Conexion();
            bool Respuesta;
            cnn.Conectar();
            short vCat = Convert.ToInt16(cnn.DameValor(Tabla, "Id=" + vId, "Catalogo"));
            Respuesta = cnn.BorrarRegistro(Tabla, "Id=" + vId, Preguntar);
            if (vCat == 1)//Usuario
            {
                cnn.Exec("DELETE FROM SegCatalogosDetalle WHERE Usuario=" + vId);
                cnn.Exec("DELETE FROM SegCatalogosOpciones WHERE Usuario=" + vId);
                cnn.Exec("DELETE FROM SegCatalogosRestricciones WHERE Usuario=" + vId);
            }
            if (vCat == 2)//Perfil
            {
                cnn.Exec("DELETE FROM SegCatalogosOpciones WHERE Usuario=0 and Perfil=" + vId);
                cnn.Exec("DELETE FROM SegCatalogosRestricciones WHERE Usuario=0 and Perfil=" + vId);
            }
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
            strConsulta = "SELECT Descripcion, Codigo, Id, Password, Nivel, IdPadre, Activo FROM [" + Tabla + "]" + strFiltro + " ORDER BY Descripcion";

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
            strConsulta = "SELECT Descripcion, Codigo, Id, Password, Nivel, IdPadre, Activo FROM [" + Tabla + "]" + strFiltro + " ORDER BY Descripcion";

            //obtener registros
            cnn.Conectar();
            IDataReader Registro = cnn.AbreSQL(strConsulta);
            cnn.Dispose();
            return Registro;
        }


        public DataTable ListarTabla(int vCatalogo, int vIdPadre, string vDescripcion)
        {
            var cnn = new Conexion();
            string strConsulta;

            /*
            //generar consulta SQL
            strFiltro = " WHERE Catalogo=" + vCatalogo;
            if (vIdPadre > 0) strFiltro = strFiltro + " and IdPadre=" + vIdPadre;
            if (vDescripcion.Length > 0) strFiltro = strFiltro + " and Descripcion LIKE " + cnn.nSt(vDescripcion);
            strConsulta = "SELECT Descripcion, Codigo, Id, Password, Nivel, IdPadre, Activo FROM [" + Tabla + "]" + strFiltro + " ORDER BY Descripcion";
            */
            strConsulta = "Sp_SegCatalogosListar";

            //obtener registros
            cnn.Conectar();
            DataTable Registro = cnn.GetSpTabla(strConsulta, vCatalogo);
            cnn.Dispose();
            return Registro;
        }

        public DataSet ListarTodos()
        {
            var cnn = new Conexion();

            //obtener registros
            cnn.Conectar();
            DataSet Registro = cnn.GetSpDataSet("Sp_SegCatalogosListarTodos");
            cnn.Dispose();
            return Registro;
        }

        public int ConsultarOpciones(int vUsuario, int vPerfil, string vTipo, ref JObject objJSON)
        {
            //IDataReader Registro;
            int Count;
            var cnn = new Conexion();
            string strFiltro = "";
            string strJSON = "";
            string strConsulta;

            cnn.Conectar();
            //if (cnn.ExisteRegistro("SegCatalogosOpciones", "Usuario=" + vUsuario + " and Perfil=" + vPerfil + " and Tipo='" + vTipo,""))
            //{
            //generar consulta SQL
            strFiltro = strFiltro + " and d.Usuario=" + vUsuario;
            strFiltro = strFiltro + " and d.Perfil=" + vPerfil;
            if (vTipo.Length > 0) strFiltro = strFiltro + " and d.Tipo=" + Helper.strSql(vTipo);
            strConsulta = "SELECT d.Tipo,d.Codigo,o.Padre,o.Descripcion,o.Origen,o.Catalogo,o.Orden,d.SoloLectura FROM SegCatalogosOpciones as d INNER JOIN SegOpciones as o ON (d.Tipo=o.Tipo) and (d.Codigo=o.Codigo) WHERE d.Id>0" + strFiltro + " ORDER BY o.Orden, d.Tipo, d.Codigo";
            //}
            //else
            //{
            //    strConsulta = "SELECT Tipo,Codigo,Padre,Descripcion,Orden,0 as SoloLectura FROM SegOpciones ORDER BY Orden,Tipo,Codigo";
            //}

            //obtener registros
            Count = 0;
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
                vCodigo = 1; if (cnn.ExisteRegistro(Tabla, "SELECT Max(Codigo) as c FROM [" + Tabla + "] WHERE Catalogo=" + vCatalogo + (vCatPadre > 0 ? " and IdPadre=" + vIdPadre : ""), "c")) vCodigo = Convert.ToInt32(Helper.vg_DAT.Length > 0 ? Helper.vg_DAT : "0") + 1;
                cnn.Dispose();
            }
            return vCodigo;
        }

        

    }
}
