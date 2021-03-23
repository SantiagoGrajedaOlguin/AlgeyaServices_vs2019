
using System;
using System.Data;

namespace Idealsis.Dal.Mapeo
{
    public class SegSesionesActivas
    {
        const string  Tabla = "SegSesionesActivas";
        int    usuario;
        string pc;
        string fecha;
        string hora;
        int    spid;

        public int Usuario { get { return usuario; } set { usuario = value; } }
        public string Pc { get { return pc; } set { pc = value; } }
        public string Fecha { get { return fecha; } set { fecha = value; } }
        public string Hora { get { return hora; } set { hora = value; } }
        public int Spid { get { return spid; } set { spid = value; } }

        public void Guardar()
        {
            string c;
            string v;
            var cnn = new Conexion();

            cnn.Conectar();
            c = "Usuario,0,1|Pc,1,1|Fecha,2|Hora,3|Spid";
            v = usuario + "|" + Helper.Mid(pc, 0, 50) + "|" + fecha + "|" + hora + "|" + spid;
            cnn.GuardarRegistro(Tabla, c, v);
            cnn.Dispose();
        }

        private void AsignarValores(IDataReader Registro)
        {
            usuario = Convert.ToInt32(Convert.IsDBNull(Registro["Usuario"]) ? 0 : Registro["Usuario"]);
            pc = Convert.IsDBNull(Registro["Pc"]) ? "" : Registro["Pc"].ToString();
            fecha = Convert.IsDBNull(Registro["Fecha"]) ? "" : Convert.ToDateTime(Registro["Fecha"]).ToString("dd/MM/yyyy hh:mm:ss tt");
            hora = Convert.IsDBNull(Registro["Hora"]) ? "" : Convert.ToDateTime(Registro["Hora"]).ToString("dd/MM/yyyy hh:mm:ss tt");
            spid = Convert.ToInt32(Convert.IsDBNull(Registro["Spid"]) ? 0 : Registro["Spid"]);
        }

        public bool ConsultarPorId(int vUsuario, string vPc)
        {
            var cnn = new Conexion();
            bool result = false;
            cnn.Conectar();
            IDataReader Registro = cnn.ObtenerRegistro(Tabla, "Usuario=" + vUsuario + " and Pc=" + Helper.strSql(vPc));
            if (Registro.Read())
            {
                result = true;
                AsignarValores(Registro);
            }
            Registro.Dispose();
            cnn.Dispose();
            return result;
        }

        public void BorrarPorId(int vUsuario, string vPc)
        {
            var cnn = new Conexion();
            cnn.Conectar();
            cnn.Exec("DELETE FROM [" + Tabla + "] WHERE Usuario=" + vUsuario + " and Pc=" + Helper.strSql(Helper.Mid(vPc,0,50)));
            cnn.Dispose();
        }

    }
}
