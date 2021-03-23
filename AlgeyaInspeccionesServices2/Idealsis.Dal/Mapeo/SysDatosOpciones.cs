using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace Idealsis.Dal.Mapeo
{
    public class SysDatosOpciones
    {
        const string Tabla = "SysDatosOpciones";

        int    dato;
        short  posicion;
        string descripcion;


        public int Dato { get { return dato; } set { dato = value; } }
        public short Posicion { get { return posicion; } set { posicion = value; } }
        public string Descripcion { get { return descripcion; } set { descripcion = value; } }

        public void Guardar()
        {
            string c;
            string v;
            var cnn = new Conexion();

            cnn.Conectar();
            c = "Dato,0,1|Posicion,0,1|Descripcion,1";
            v = dato + "|" + posicion + "|" + descripcion;
            cnn.GuardarRegistro(Tabla, c, v);
            cnn.Dispose();
            cnn = null;
        }

        public void GuardarCadena(int IdDato, string Opciones)
        {
            string c;
            string v;
            string Desc="";
            int p = 1;
            var cnn = new Conexion();

            cnn.Conectar();
            //limpiar opciones
            cnn.Exec("DELETE FROM [" + Tabla + "] WHERE Dato=" + IdDato);
            do
            {
                Desc = Helper.CutStr(Opciones, "|", p).Trim();
                if (Desc.Length == 0) break;
                c = "Dato,0,1|Posicion,0,1|Descripcion,1";
                v = IdDato + "|" + p + "|" + Desc;
                cnn.GuardarRegistro(Tabla, c, v);
                p += 1;
            } while (Desc != "");
            cnn.Dispose();
            cnn = null;
        }

        private void AsignarValores(IDataReader Registro)
        {
            dato = Convert.ToInt32(Convert.IsDBNull(Registro["Dato"]) ? 0 : Registro["Dato"]);
            posicion = Convert.ToInt16(Convert.IsDBNull(Registro["Posicion"]) ? 0 : Registro["Posicion"]);
            descripcion = Convert.IsDBNull(Registro["Descripcion"]) ? "" : Registro["Descripcion"].ToString();
        }

        public bool BuscarPorId(int vDato, short vPosicion)
        {
            var cnn = new Conexion();
            bool result = false;
            cnn.Conectar();
            IDataReader Registro = cnn.ObtenerRegistro(Tabla, "Dato=" + vDato + " and Posicion=" + vPosicion);
            if (Registro.Read())
            {
                result = true;
                AsignarValores(Registro);
            }
            Registro.Dispose();
            cnn.Dispose();
            return result;
        }

        public bool BorrarPorId(int vDato, short vPosicion)
        {
            var cnn = new Conexion();
            bool Respuesta;
            cnn.Conectar();
            Respuesta = cnn.BorrarRegistro(Tabla, "Dato=" + vDato + " and Posicion=" + vPosicion, false);
            cnn.Dispose();
            return Respuesta;
        }

        public DataTable Listar(int Dato)
        {
            var cnn = new Conexion();
            string strFiltro;
            string strConsulta;

            //generar consulta SQL
            strFiltro = " WHERE Dato=" + Dato;
            strConsulta = "SELECT Dato,Posicion,Descripcion FROM [" + Tabla + "]" + strFiltro + " ORDER BY Posicion";

            //obtener registros
            cnn.Conectar();
            DataTable Registro = cnn.GetTabla(strConsulta);
            cnn.Dispose();
            return Registro;
        }


        public string ObtenerCadena(int Dato)
        {
            var cnn = new Conexion();
            string strFiltro;
            string strConsulta;
            string Result="";
            string Desc;

            //generar consulta SQL
            strFiltro = " WHERE Dato=" + Dato;
            strConsulta = "SELECT Dato,Posicion,Descripcion FROM [" + Tabla + "]" + strFiltro + " ORDER BY Posicion";

            //obtener registros
            cnn.Conectar();
            DataTable Registro = cnn.GetTabla(strConsulta);
            foreach (DataRow row in Registro.Rows)
            {
                if (Result.Length>0)  Result = Result + "|";
                Desc = Convert.IsDBNull(row["Descripcion"]) ? "" : row["Descripcion"].ToString();
                Result = Result + Desc;
            }
            cnn.Dispose();
            Registro.Dispose();
            return Result;
        }


    }
}
