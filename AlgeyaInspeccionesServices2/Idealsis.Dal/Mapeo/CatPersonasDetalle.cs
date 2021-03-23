using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace Idealsis.Dal.Mapeo
{
    public class CatPersonasDetalle
    {
        const string Tabla = "CatPersonasDetalle";

        int    id;
        int    idOrigen;
        short  tipo;
        short  posicion;
        string codigo;
        string descripcion;
        int    idPersona;
        int    idCatalogo;
        int    idDato;
        float  valor;
        string notas;
        byte   bandera;

        public int Id { get { return id; } set { id = value; } }
        public int IdOrigen { get { return idOrigen; } set { idOrigen = value; } }
        public short Tipo { get { return tipo; } set { tipo = value; } }
        public short Posicion { get { return posicion; } set { posicion = value; } }
        public string Codigo { get { return codigo; } set { codigo = value; } }
        public string Descripcion { get { return descripcion; } set { descripcion = value; } }
        public int IdPersona { get { return idPersona; } set { idPersona = value; } }
        public int IdCatalogo { get { return idCatalogo; } set { idCatalogo = value; } }
        public int IdDato { get { return idDato; } set { idDato = value; } }
        public float Valor { get { return valor; } set { valor = value; } }
        public string Notas { get { return notas; } set { notas = value; } }
        public byte Bandera { get { return bandera; } set { bandera = value; } }

        public int Guardar()
        {
            string c;
            string v;
            var cnn = new Conexion();

            cnn.Conectar();
            c = "Id,0,9|IdOrigen|Tipo|Posicion|Codigo,1|Descripcion,1|IdPersona|IdCatalogo|IdDato|Valor|Notas,1|Bandera";
            v = id + "|" + idOrigen + "|" + tipo + "|" + posicion + "|" + codigo + "|" + descripcion + "|" + idPersona + "|" + idCatalogo + "|" + idDato + "|" + valor + "|" + notas + "|" + bandera;
            id = cnn.GuardarRegistro(Tabla, c, v);
            cnn.Dispose();
            cnn = null;
            return id;
        }

        private void AsignarValores(IDataReader Registro)
        {
            id = Convert.ToInt32(Registro["Id"].ToString());
            idOrigen = Convert.ToInt32(Convert.IsDBNull(Registro["IdOrigen"]) ? 0 : Registro["IdOrigen"]);
            tipo = Convert.ToInt16(Convert.IsDBNull(Registro["Tipo"]) ? 0 : Registro["Tipo"]);
            posicion = Convert.ToInt16(Convert.IsDBNull(Registro["Posicion"]) ? 0 : Registro["Posicion"]);
            codigo = Convert.IsDBNull(Registro["Codigo"]) ? "" : Registro["Codigo"].ToString();
            descripcion = Convert.IsDBNull(Registro["Descripcion"]) ? "" : Registro["Descripcion"].ToString();
            idPersona = Convert.ToInt32(Convert.IsDBNull(Registro["IdPersona"]) ? 0 : Registro["IdPersona"]);
            idCatalogo = Convert.ToInt32(Convert.IsDBNull(Registro["IdCatalogo"]) ? 0 : Registro["IdCatalogo"]);
            idDato = Convert.ToInt32(Convert.IsDBNull(Registro["IdDato"]) ? 0 : Registro["IdDato"]);
            valor = Convert.ToSingle(Convert.IsDBNull(Registro["Valor"]) ? 0 : Registro["Valor"]);
            notas = Convert.IsDBNull(Registro["Notas"]) ? "" : Registro["Notas"].ToString();
            bandera = Convert.ToByte(Convert.IsDBNull(Registro["Bandera"]) ? 0 : Registro["Bandera"]);
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
        public bool BuscarPorCodigo(int vIdOrigen, short vTipo, short vPosicion)
        {
            var cnn = new Conexion();
            bool result = false;
            cnn.Conectar();
            IDataReader Registro = cnn.ObtenerRegistro(Tabla, "IdOrigen=" + vIdOrigen + " and Tipo=" + vTipo + " and Posicion=" + vPosicion);
            if (Registro.Read())
            {
                result = true;
                AsignarValores(Registro);
            }
            Registro.Dispose();
            cnn.Dispose();
            return result;
        }

        public bool Existe(int vIdOrigen, short vTipo, short vPosicion)
        {
            var cnn = new Conexion();
            bool result = false;
            cnn.Conectar();
            result = cnn.ExisteRegistro(Tabla, "IdOrigen=" + vIdOrigen + " and Tipo=" + vTipo + " and Posicion=" + vPosicion, "Id");
            cnn.Dispose();
            return result;
        }

        public string ObtenerValor(int vId, string Campo)
        {
            var cnn = new Conexion();
            string result = "";
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

        public DataTable ListarTabla(int vIdOrigen, short vTipo, string vDescripcion)
        {
            var cnn = new Conexion();
            string strConsulta;
            string strFiltro;

            //generar consulta SQL
            strFiltro = " WHERE IdOrigen=" + vIdOrigen;
            if (vTipo > 0) strFiltro = strFiltro + " and Tipo=" + vTipo;
            if (vDescripcion.Length > 0) strFiltro = strFiltro + " and Descripcion LIKE " + cnn.nSt(vDescripcion);
            strConsulta = "SELECT Id,IdOrigen,Tipo,Posicion,Codigo,Descripcion,IdPersona,IdCatalogo,IdDato,Valor,Notas,Bandera FROM [" + Tabla + "]" + strFiltro + " ORDER BY Posicion,Id";

            //obtener registros
            cnn.Conectar();
            DataTable Registro = cnn.GetTabla(strConsulta);
            cnn.Dispose();
            return Registro;
        }

        public int SiguientePosicion(int vIdOrigen, short vTipo)
        {
            int vPosicion;
            var cnn = new Conexion();
            cnn.Conectar();
            vPosicion = 1; if (cnn.ExisteRegistro(Tabla, "SELECT Max(Posicion) as c FROM [" + Tabla + "] WHERE IdOrigen=" + vIdOrigen + " and Tipo=" + vTipo, "c")) vPosicion = Convert.ToInt32(Helper.vg_DAT.Length > 0 ? Helper.vg_DAT : "0") + 1;
            cnn.Dispose();
            return vPosicion;
        }

    }
}
