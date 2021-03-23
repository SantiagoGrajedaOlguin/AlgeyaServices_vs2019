using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace Idealsis.Dal.Mapeo
{
    public class SysDatosOrden
    {
        const string Tabla = "SysDatosOrden";

        short origen;
        int   idOrigen;
        int   dato;
        int   orden;
        byte  esRequerido;

        public short Origen { get { return origen; } set { origen = value; } }
        public int IdOrigen { get { return idOrigen; } set { idOrigen = value; } }
        public int Dato { get { return dato; } set { dato = value; } }
        public int Orden { get { return orden; } set { orden = value; } }
        public byte EsRequerido { get { return esRequerido; } set { esRequerido = value; } }

        public void Guardar()
        {
            string c;
            string v;
            var cnn = new Conexion();

            cnn.Conectar();
            c = "Origen,0,1|IdOrigen,0,1|Dato,0,1|Orden|EsRequerido";
            v = origen + "|" + idOrigen + "|" + dato + "|" + orden + "|" + esRequerido;
            cnn.GuardarRegistro(Tabla, c, v);
            cnn.Dispose();
            cnn = null;
        }

        private void AsignarValores(IDataReader Registro)
        {
            origen = Convert.ToInt16(Convert.IsDBNull(Registro["Origen"]) ? 0 : Registro["Origen"]);
            idOrigen = Convert.ToInt16(Convert.IsDBNull(Registro["IdOrigen"]) ? 0 : Registro["IdOrigen"]);
            dato = Convert.ToInt16(Convert.IsDBNull(Registro["Dato"]) ? 0 : Registro["Dato"]);
            orden = Convert.ToInt16(Convert.IsDBNull(Registro["Orden"]) ? 0 : Registro["Orden"]);
            esRequerido = Convert.ToByte(Convert.IsDBNull(Registro["EsRequerido"]) ? 0 : Registro["EsRequerido"]);
        }

        public bool BuscarPorCodigo(short Origen, int IdOrigen, int Dato)
        {
            var cnn = new Conexion();
            bool result = false;
            cnn.Conectar();
            IDataReader Registro = cnn.ObtenerRegistro(Tabla, "Origen = " + Origen + " and IdOrigen =" + IdOrigen + " and Dato=" + Dato);
            if (Registro.Read())
            {
                result = true;
                AsignarValores(Registro);
            }
            Registro.Dispose();
            cnn.Dispose();
            return result;
        }

        public bool BorrarPorCodigo(short Origen, int IdOrigen, int Dato, bool Preguntar)
        {
            var cnn = new Conexion();
            bool Respuesta;
            cnn.Conectar();
            Respuesta = cnn.BorrarRegistro(Tabla, "Origen = " + Origen + " and IdOrigen =" + IdOrigen + " and Dato=" + Dato, Preguntar);
            cnn.Dispose();
            return Respuesta;
        }

        public DataTable Listar(short Origen, int IdOrigen)
        {
            //IDataReader Registro;
            var cnn = new Conexion();
            string strFiltro;
            string strConsulta;

            //generar consulta SQL
            strFiltro = " WHERE d.Origen=" + Origen;
            if (IdOrigen > 0) strFiltro = strFiltro + " and d.IdOrigen=" + IdOrigen;
            strConsulta = "SELECT d.Dato, d.EsRequerido, d.Orden, SysDatos.Descripcion, SysDatos.EsEtiqueta, SysDatos.Tipo, SysDatos.Formato, SysDatos.FormatoDes, SysDatos.FormatoCap" +
                          " FROM  [" + Tabla + "] as d LEFT JOIN sysDatos ON d.Dato=SysDatos.Id" + strFiltro +
                          " ORDER BY d.Orden";


            //obtener registros
            cnn.Conectar();
            DataTable Registro = cnn.GetTabla(strConsulta);
            cnn.Dispose();
            return Registro;
        }
    }
}
