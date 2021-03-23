using System;
using System.Data;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Idealsis.Dal.Mapeo
{
    public class CatMonedasTiposDeCambio
    {

        const string   Tabla = "CatMonedasTiposDeCambio";
        short   moneda;
        string  fecha;
        string  usuario;
        string  hora;
        float   tipoDeCambio;

        public short Moneda { get { return moneda; } set { moneda = value; } }
        public string Fecha { get { return fecha; } set { fecha = value; } }
        public string Usuario { get { return usuario; } set { usuario = value; } }
        public string Hora { get { return hora; } set { hora = value; } }
        public float TipoDeCambio { get { return tipoDeCambio; } set { tipoDeCambio = value; } }

        public void Guardar()
        {
            string c;
            string v;
            var cnn = new Conexion();

            cnn.Conectar();
            c = "Moneda,0,1|Fecha,2,1|Usuario,1|Hora,3|TipoDeCambio";
            v = moneda + "|" + fecha + "|" + usuario + "|" + hora + "|" + tipoDeCambio;
            cnn.GuardarRegistro(Tabla, c, v);
            cnn.RegistrarBitacora("Tipo de cambio", (cnn.EsAlta? "Nuevo": "Modificación") + ", Moneda: " + moneda + " TC:" + tipoDeCambio);
            cnn.Dispose();
            cnn = null;
        }

        private void AsignarValores(IDataReader Registro)
        {
            moneda = Convert.ToInt16(Convert.IsDBNull(Registro["Moneda"]) ? 0 : Registro["Moneda"]);
            fecha = Convert.IsDBNull(Registro["Fecha"]) ? "" : Registro["Fecha"].ToString();
            usuario = Convert.IsDBNull(Registro["Usuario"]) ? "" : Registro["Usuario"].ToString();
            hora = Convert.IsDBNull(Registro["Hora"]) ? "" : Registro["Hora"].ToString();
            tipoDeCambio = Convert.ToSingle(Convert.IsDBNull(Registro["TipoDeCambio"]) ? 0 : Registro["TipoDeCambio"]);
            Registro.Dispose();
        }

        public bool Desplegar(int Direccion, int vCodigo)
        {
            var cnn = new Conexion();
            bool Result = false;
            cnn.Conectar();
            IDataReader Registro = cnn.MoverCodigo(Tabla, Direccion, vCodigo);
            if (Registro.Read())
            {
                Result = true;
                AsignarValores(Registro);
            }
            Registro.Dispose();
            cnn.Dispose();
            return Result;
        }

        public float BuscarPorFecha(short vMoneda, string vFecha, string vUltimaFecha)
        {
            var cnn = new Conexion();
            float vTipoDeCambio = 1;

            cnn.Conectar();
            vUltimaFecha = "";
            if (cnn.ExisteRegistro(Tabla, "Moneda=" + vMoneda + " and Fecha=" + cnn.DateSQL(vFecha) + " and TipoDeCambio>0", "TipoDeCambio"))
            {
                vUltimaFecha = vFecha;
                vTipoDeCambio = Helper.Val(Helper.vg_DAT);
            }
            else if (cnn.ExisteRegistro(Tabla, "SELECT TOP 1 TipoDeCambio,Fecha FROM catMonedasTiposDeCambio WHERE Moneda=" + vMoneda + " and Fecha<=" + cnn.DateSQL(vFecha) + " and TipoDeCambio>0 ORDER BY Fecha DESC", "TipoDeCambio,Fecha"))
            {
                vTipoDeCambio = Helper.Val(Helper.CutStr(Helper.vg_DAT, "|", 1));
                vUltimaFecha = String.Format("{0:d/M/yyyy}", Helper.CutStr(Helper.vg_DAT, "|", 2));
            }
            cnn.Dispose();
            cnn = null;
            return vTipoDeCambio;
        }

        public bool BorrarPorId(short vMoneda, string vFecha, bool Preguntar)
        {
            var cnn = new Conexion();
            bool Respuesta;
            cnn.Conectar();
            Respuesta = cnn.BorrarRegistro(Tabla, "Moneda=" + vMoneda + " and Fecha=" + cnn.DateSQL(vFecha), Preguntar);
            cnn.Dispose();
            return Respuesta;
        }

        public int Listar(string vDescripcion, ref string strJSON)
        {
            int Count;
            var cnn = new Conexion();
            string strFiltro;
            string strConsulta;

            //generar consulta SQL
            strFiltro = "";
            strConsulta = "SELECT Moneda,Fecha,TipoDeCambio FROM [" + Tabla + "] WHERE Moneda>0" + strFiltro + " ORDER BY Moneda,Fecha";

            //obtener registros
            strJSON = "";
            Count = 0;
            cnn.Conectar();
            cnn.SQL = strConsulta;
            IDataReader Registro = cnn.AbreSQL();
            if (cnn.Hay(Registro))
            {
                strJSON = Json.ReaderToJson(Registro, ref Count);
                Registro.Dispose();
            }
            cnn.Dispose();
            return Count;
        }

    }
}
