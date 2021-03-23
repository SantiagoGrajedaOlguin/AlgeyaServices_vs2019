
using System;
using System.Data;

namespace Idealsis.Dal.Mapeo
{
    class SysUltimoRegistro
    {
        const string Tabla = "sysUltimoRegistro";
        public string Usuario;
        public int    Id;
        public string Hora;
        public string Pc;

        public void Guardar()
        {
            string c;
            string v;
            var cnn = new Conexion();

            cnn.Conectar();
            c = "Usuario,1,1|Id,0,1|Hora,4|Pc,1";
            v = Usuario + "|" + Id + "|" + Hora + "|" + Pc;
            cnn.GuardarRegistro(Tabla, c, v);
            cnn.Dispose();
            cnn = null;

        }
        private void AsignarValores(IDataReader Registro)
        {
            Usuario = Registro["Usuario"].ToString();
            Id = Convert.ToInt32(Registro["Id"]);
            Hora = Convert.IsDBNull(Registro["Hora"]) ? "" : Convert.ToDateTime(Registro["Hora"]).ToString("dd/MM/yyyy hh:mm:ss tt");
            Pc = Registro["Pc"].ToString();
            Registro.Dispose();
        }

        public bool BuscarPorId(string vUsuario, int vId)
        {
            IDataReader Registro;
            var cnn = new Conexion();
            bool Result = false;
            cnn.Conectar();
            Registro = cnn.ObtenerRegistro(Tabla, "Usuario=" + Helper.strSql(vUsuario) + " and Id=" + vId);
            if (Registro.Read())
            {
                Result = true;
                AsignarValores(Registro);
            }
            Registro.Dispose();
            cnn.Dispose();
            cnn = null;
            return Result;
        }

        public void BorrarPorId(string vUsuario, int vId)
        {
            var cnn = new Conexion();
            cnn.Conectar();
            cnn.Exec("DELETE FROM [" + Tabla + "] WHERE Usuario=" + Helper.strSql(vUsuario) + " and Id=" + vId);
            cnn.Dispose();
            cnn = null;
        }
    }
}
