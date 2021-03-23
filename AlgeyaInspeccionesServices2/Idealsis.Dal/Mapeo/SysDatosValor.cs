using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace Idealsis.Dal.Mapeo
{
    public class SysDatosValor
    {
        const string Tabla = "SysDatosValor";


        short origen;
        int idOrigen;
        int dato;
        decimal valorNumero;
        string valorCadena;
        string valorFecha;

        public short Origen { get { return origen; } set { origen = value; } }
        public int IdOrigen { get { return idOrigen; } set { idOrigen = value; } }
        public int Dato { get { return dato; } set { dato = value; } }
        public decimal  ValorNumero { get { return valorNumero; } set { valorNumero = value; } }
        public string   ValorCadena { get { return valorCadena; } set { valorCadena = value; } }
        public string   ValorFecha { get { return valorFecha; } set { valorFecha = value; } }

        public void Guardar()
        {
            string c;
            string v;
            var cnn = new Conexion();

            cnn.Conectar();
            c = "Origen,0,1|IdOrigen,0,1|Dato,0,1|ValorNumero|ValorCadena,1|ValorFecha,4";
            v = origen + "|" + idOrigen + "|" + dato + "|" + valorNumero + "|" + valorCadena + "|" + valorFecha;
            cnn.GuardarRegistro(Tabla, c, v);
            cnn.Dispose();
        }

        private void AsignarValores(IDataReader Registro)
        {
            origen = Convert.ToInt16(Convert.IsDBNull(Registro["Origen"]) ? 0 : Registro["Origen"]);
            idOrigen = Convert.ToInt32(Convert.IsDBNull(Registro["IdOrigen"]) ? 0 : Registro["IdOrigen"]);
            dato = Convert.ToInt16(Convert.IsDBNull(Registro["Dato"]) ? 0 : Registro["Dato"]);
            valorNumero = Convert.ToDecimal(Convert.IsDBNull(Registro["ValorNumero"]) ? 0 : Registro["ValorNumero"]);
            valorCadena = Convert.IsDBNull(Registro["ValorCadena"]) ? "" : Registro["ValorCadena"].ToString();
            valorFecha = Convert.IsDBNull(Registro["ValorFecha"]) ? "" : Convert.ToDateTime(Registro["ValorFecha"]).ToString("dd/MM/yyyy hh:mm:ss tt");
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

        public bool BorrarPorCodigo(short Origen, int IdOrigen, int Dato)
        {
            var cnn = new Conexion();
            bool Respuesta;
            cnn.Conectar();
            Respuesta = cnn.BorrarRegistro(Tabla, "Origen=" + Origen + " and IdOrigen =" + IdOrigen + " and Dato=" + Dato, false);
            cnn.Dispose();
            return Respuesta;
        }

        public DataTable Listar(short Origen, int IdOrigen, string vDescripcion)
        {
            var cnn = new Conexion();
            string strFiltro;
            string strConsulta;

            //generar consulta SQL
            strFiltro = " WHERE Origen=" + Origen + " and IdOrigen=" + IdOrigen;
            if (vDescripcion.Length > 0) strFiltro = strFiltro + " and Descripcion LIKE " + cnn.nSt(vDescripcion);
            strConsulta = "SELECT d.Dato, d.EsRequerido, d.Orden, SysDatos.Descripcion, SysDatos.EsEtiqueta, SysDatos.Tipo, sysDatos.Formato" +
                          " FROM  [" + Tabla + "] as d LEFT JOIN SysDatos ON d.Dato=SysDatos.Id" + strFiltro +
                          " ORDER BY d.Orden";

            //obtener registros
            cnn.Conectar();
            DataTable Registro = cnn.GetTabla(strConsulta);
            cnn.Dispose();
            return Registro;
        }

    }
}
