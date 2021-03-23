using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace Idealsis.Dal.Mapeo
{
    public class CatSistemasDetalle
    {
        const string Tabla = "CatSistemasDetalle";

        int    id;
        int    idOrigen;
        short  posicion;
        string descripcion;
        string estatus;

        public int Id { get { return id; } set { id = value; } }
        public int IdOrigen { get { return idOrigen; } set { idOrigen = value; } }
        public short Posicion { get { return posicion; } set { posicion = value; } }
        public string Descripcion { get { return descripcion; } set { descripcion = value; } }
        public string Estatus { get { return estatus; } set { estatus = value; } }

        public int Guardar()
        {
            string c;
            string v;
            var cnn = new Conexion();

            cnn.Conectar();
            c = "Id,0,9|IdOrigen|Posicion|Descripcion,1|Estatus,1";
            v = id + "|" + idOrigen  + "|" + posicion + "|" + descripcion + "|" + estatus;
            id = cnn.GuardarRegistro(Tabla, c, v);
            cnn.Dispose();
            cnn = null;
            return id;
        }

        private void AsignarValores(IDataReader Registro)
        {
            id = Convert.ToInt32(Registro["Id"].ToString());
            idOrigen = Convert.ToInt32(Convert.IsDBNull(Registro["IdOrigen"]) ? 0 : Registro["IdOrigen"]);
            posicion = Convert.ToInt16(Convert.IsDBNull(Registro["Posicion"]) ? 0 : Registro["Posicion"]);
            descripcion = Convert.IsDBNull(Registro["Descripcion"]) ? "" : Registro["Descripcion"].ToString();
            estatus = Convert.IsDBNull(Registro["Estatus"]) ? "" : Registro["Estatus"].ToString();
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
            Respuesta = cnn.BorrarRegistro(Tabla, "IdOrigen=" + vIdOrigen + (vPosicion > 0 ? " and Posicion=" + vPosicion : ""), Preguntar);
            cnn.Dispose();
            return Respuesta;
        }

        public DataTable ListarTabla(int vIdOrigen, string vDescripcion)
        {
            var cnn = new Conexion();
            string strConsulta;
            string strFiltro;

            //generar consulta SQL
            strFiltro = " WHERE IdOrigen=" + vIdOrigen;
            if (vDescripcion.Length > 0) strFiltro = strFiltro + " and Descripcion LIKE " + cnn.nSt(vDescripcion);
            strConsulta = "SELECT Id,IdOrigen,Posicion,Descripcion,Estatus FROM [" + Tabla + "]" + strFiltro + " ORDER BY Posicion,Id";

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
