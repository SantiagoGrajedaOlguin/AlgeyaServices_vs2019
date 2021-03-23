using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace Idealsis.Dal.Mapeo
{
    public class SysCatalogosDetalle
    {
        const string Tabla = "SysCatalogosDetalle";

        int     id;
        int     idOrigen;
        short   tipo;
        short   posicion;
        int     idArticulo;
        int     idPersona;
        int     idCatalogo;
        int     idDato;
        decimal cantidad;
        float   valor;
        string  descripcion;
        string  texto;
        string  notas;
        byte    esRequerido;

        public int Id { get { return id; } set { id = value; } }
        public int IdOrigen { get { return idOrigen; } set { idOrigen = value; } }
        public short Tipo { get { return tipo; } set { tipo = value; } }
        public short Posicion { get { return posicion; } set { posicion = value; } }
        public int IdArticulo { get { return idArticulo; } set { idArticulo = value; } }
        public int IdPersona { get { return idPersona; } set { idPersona = value; } }
        public int IdCatalogo { get { return idCatalogo; } set { idCatalogo = value; } }
        public int IdDato { get { return idDato; } set { idDato = value; } }
        public decimal Cantidad { get { return cantidad; } set { cantidad = value; } }
        public float Valor { get { return valor; } set { valor = value; } }
        public string Descripcion { get { return descripcion; } set { descripcion = value; } }
        public string Texto { get { return texto; } set { texto = value; } }
        public string Notas { get { return notas; } set { notas = value; } }
        public byte EsRequerido { get { return esRequerido; } set { esRequerido = value; } }

        public int Guardar()
        {
            string c;
            string v;
            var cnn = new Conexion();

            cnn.Conectar();
            c = "Id,0,9|IdOrigen|Tipo|Posicion|IdArticulo|IdPersona|IdCatalogo|IdDato|Cantidad|Valor|Descripcion,1|Texto,1|Notas,1|EsRequerido";
            v = id + "|" + idOrigen + "|" + tipo + "|" + posicion + "|" + idArticulo + "|" + idPersona + "|" + idCatalogo + "|" +idDato + "|" + cantidad + "|" + valor + "|" + descripcion + "|" + texto + "|" + notas + "|" + esRequerido;
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
            idArticulo = Convert.ToInt32(Convert.IsDBNull(Registro["IdArticulo"]) ? 0 : Registro["IdArticulo"]);
            idPersona = Convert.ToInt32(Convert.IsDBNull(Registro["IdPersona"]) ? 0 : Registro["IdPersona"]);
            idCatalogo = Convert.ToInt32(Convert.IsDBNull(Registro["IdCatalogo"]) ? 0 : Registro["IdCatalogo"]);
            idDato = Convert.ToInt32(Convert.IsDBNull(Registro["IdDato"]) ? 0 : Registro["IdDato"]);
            cantidad = Convert.ToDecimal(Convert.IsDBNull(Registro["Cantidad"]) ? 0 : Registro["Cantidad"]);
            valor = Convert.ToSingle(Convert.IsDBNull(Registro["Valor"]) ? 0 : Registro["Valor"]);
            descripcion = Convert.IsDBNull(Registro["Descripcion"]) ? "" : Registro["Descripcion"].ToString();
            texto = Convert.IsDBNull(Registro["Texto"]) ? "" : Registro["Texto"].ToString();
            notas = Convert.IsDBNull(Registro["Notas"]) ? "" : Registro["Notas"].ToString();
            esRequerido = Convert.ToByte(Convert.IsDBNull(Registro["EsRequerido"]) ? 0 : Registro["EsRequerido"]);
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
        public bool BuscarPorCodigo(int vIdOrigen, short vPosicion)
        {
            var cnn = new Conexion();
            bool result = false;
            cnn.Conectar();
            IDataReader Registro = cnn.ObtenerRegistro(Tabla, "IdOrigen=" + vIdOrigen + " and Posicion=" + vPosicion);
            if (Registro.Read())
            {
                result = true;
                AsignarValores(Registro);
            }
            Registro.Dispose();
            cnn.Dispose();
            return result;
        }

        public string GetDesc(int vIdOrigen, short vTipo)
        {
            var cnn = new Conexion();
            cnn.Conectar();
            string result = cnn.DameValor(Tabla, "IdOrigen=" + vIdOrigen + " and Tipo=" + vTipo,"Descripcion");
            cnn.Dispose();
            return result;
        }

        public bool Existe(int vIdOrigen, short vPosicion)
        {
            var cnn = new Conexion();
            bool result = false;
            cnn.Conectar();
            result = cnn.ExisteRegistro(Tabla, "IdOrigen=" + vIdOrigen + " and Posicion=" + vPosicion, "Id");
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
        public bool BorrarPorCodigo(int vIdOrigen, short vPosicion, bool Preguntar)
        {
            var cnn = new Conexion();
            bool Respuesta;
            cnn.Conectar();
            Respuesta = cnn.BorrarRegistro(Tabla, "IdOrigen=" + vIdOrigen + (vPosicion>0? " and Posicion=" + vPosicion: ""), Preguntar);
            cnn.Dispose();
            return Respuesta;
        }

        public DataTable ListarTabla(int vIdOrigen, short vTipo, int vIdCatalogo, int vIdDato, string vDescripcion)
        {
            var cnn = new Conexion();
            string strConsulta;
            string strFiltro;
            
            //generar consulta SQL
            strFiltro = " WHERE IdOrigen=" + vIdOrigen;
            if (vTipo > 0) strFiltro = strFiltro + " and Tipo=" + vTipo;
            if (vIdCatalogo > 0) strFiltro = strFiltro + " and IdCatalogo=" + vIdCatalogo;
            if (vIdDato > 0) strFiltro = strFiltro + " and IdDato=" + vIdDato;
            if (vDescripcion.Length > 0) strFiltro = strFiltro + " and Descripcion LIKE " + cnn.nSt(vDescripcion);
            strConsulta = "SELECT Id,IdOrigen,Tipo,Posicion,IdArticulo,IdPersona,IdCatalogo,IdDato,Cantidad,Valor,Descripcion,Texto,Notas,EsRequerido FROM [" + Tabla + "]" + strFiltro + " ORDER BY Posicion,Id";

            //obtener registros
            cnn.Conectar();
            DataTable Registro = cnn.GetTabla(strConsulta);
            cnn.Dispose();
            return Registro;
        }

        public int SiguientePosicion(int vIdOrigen)
        {
            int vPosicion;
            var cnn = new Conexion();
            cnn.Conectar();
            vPosicion = 1; if (cnn.ExisteRegistro(Tabla, "SELECT Max(Posicion) as c FROM [" + Tabla + "] WHERE IdOrigen=" + vIdOrigen, "c")) vPosicion = Convert.ToInt32(Helper.vg_DAT.Length > 0 ? Helper.vg_DAT : "0") + 1;
            cnn.Dispose();
            return vPosicion;
        }

    }
}
