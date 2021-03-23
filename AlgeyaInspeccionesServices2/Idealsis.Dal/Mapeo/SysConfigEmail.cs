using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Idealsis.Dal.Mapeo
{
    public class SysConfigEmail
    {
        const string Tabla = "SysConfigEmail";

        private int    id;
        private byte   origen;
        private int    idOrigen;
        private string descripcion;
        private string email;
        private string servidorSmtp;
        private string puerto;
        private byte   autentificacion;
        private string usuario;
        private string password;
        private byte   conSsl;
        private byte   libreria;

        public int Id { get => id; set => id = value; }
        public byte Origen { get => origen; set => origen = value; }
        public int IdOrigen { get => idOrigen; set => idOrigen = value; }
        public string Descripcion { get => descripcion; set => descripcion = value; }
        public string Email { get => email; set => email = value; }
        public string ServidorSmtp { get => servidorSmtp; set => servidorSmtp = value; }
        public string Puerto { get => puerto; set => puerto = value; }
        public byte Autentificacion { get => autentificacion; set => autentificacion = value; }
        public string Usuario { get => usuario; set => usuario = value; }
        public string Password { get => password; set => password = value; }
        public byte ConSsl { get => conSsl; set => conSsl = value; }
        public byte Libreria { get => libreria; set => libreria = value; }

        public int Guardar(bool ObtenerId)
        {
            string c;
            string v;
            var cnn = new Conexion();

            cnn.Conectar();

            if (ObtenerId) id = (int)Helper.Val(cnn.DameValor(Tabla, "Origen=" + origen + " and IdOrigen=" + idOrigen, "Id"));

            c = "Id,0,9|Origen|IdOrigen|Descripcion,1|Email,1|ServidorSmtp,1|Puerto,1|Autentificacion|Usuario,1|Password,1|ConSsl|Libreria";
            v = id + "|" + origen + "|" + idOrigen + "|" + descripcion + "|" + email + "|" + servidorSmtp + "|" + puerto + "|" + autentificacion + "|" + usuario + "|" + password + "|" + conSsl + "|" + libreria;
            id = cnn.GuardarRegistro(Tabla, c, v);
            cnn.Dispose();
            cnn = null;
            return id;
        }

        private void AsignarValores(IDataReader Registro)
        {
            id = Convert.ToInt32(Convert.IsDBNull(Registro["Id"]) ? 0 : Registro["Id"]);
            origen = Convert.ToByte(Convert.IsDBNull(Registro["Origen"]) ? 0 : Registro["Origen"]);
            idOrigen = Convert.ToInt32(Convert.IsDBNull(Registro["IdOrigen"]) ? 0 : Registro["IdOrigen"]);
            descripcion = Convert.IsDBNull(Registro["Descripcion"]) ? "" : Registro["Descripcion"].ToString();
            email = Convert.IsDBNull(Registro["Email"]) ? "" : Registro["Email"].ToString();
            servidorSmtp = Convert.IsDBNull(Registro["ServidorSmtp"]) ? "" : Registro["ServidorSmtp"].ToString();
            puerto = Convert.IsDBNull(Registro["Puerto"]) ? "" : Registro["Puerto"].ToString();
            autentificacion = Convert.ToByte(Convert.IsDBNull(Registro["autentificacion"]) ? 0 : Registro["autentificacion"]);
            usuario = Convert.IsDBNull(Registro["Usuario"]) ? "" : Registro["Usuario"].ToString();
            password = Convert.IsDBNull(Registro["Password"]) ? "" : Registro["Password"].ToString();
            libreria = Convert.ToByte(Convert.IsDBNull(Registro["Libreria"]) ? 0 : Registro["Libreria"]);
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
        public bool BorrarPorId(int vId)
        {
            var cnn = new Conexion();
            bool Respuesta;
            cnn.Conectar();
            Respuesta = cnn.BorrarRegistro(Tabla, "Id=" + vId, false);
            cnn.Dispose();
            return Respuesta;
        }

        public bool BorrarPorOrigen(byte Origen, int IdOrigen)
        {
            var cnn = new Conexion();
            bool Respuesta;
            cnn.Conectar();
            Respuesta = cnn.BorrarRegistro(Tabla, "Origen=" + Origen + " and IdOrigen=" + IdOrigen, false);
            cnn.Dispose();
            return Respuesta;
        }

        public DataTable ListarTabla(byte Origen, int IdOrigen, string vDescripcion)
        {
            var cnn = new Conexion();
            string strFiltro;
            string strConsulta;

            //generar consulta SQL
            strFiltro = " WHERE Origen=" + Origen + " and IdOrigen=" + IdOrigen;
            if (vDescripcion.Length > 0) strFiltro = strFiltro + " and Descripcion LIKE " + cnn.nSt(vDescripcion);
            strConsulta = "SELECT * FROM [" + Tabla + "]" + strFiltro + " ORDER BY Descripcion";

            //obtener registros
            cnn.Conectar();
            DataTable Registro = cnn.GetTabla(strConsulta);
            cnn.Dispose();
            return Registro;
        }

    }
}
