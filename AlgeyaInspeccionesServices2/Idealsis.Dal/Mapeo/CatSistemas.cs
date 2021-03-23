
using System;
using System.Data;

namespace Idealsis.Dal.Mapeo
{
    public class CatSistemas
    {
        const string Tabla = "CatSistemas";

        int    id;
        int    codigo;
        string descripcion;
        string desarrollador;
        string lenguaje;
        string plataforma;
        byte   enRed;
        byte   enSucursales;

        public int    Id { get { return id; } set { id = value; } }
        public int    Codigo { get { return codigo; } set { codigo = value; } }
        public string Descripcion { get { return descripcion; } set { descripcion = value; } }
        public string Desarrollador { get { return desarrollador; } set { desarrollador = value; } }
        public string Lenguaje { get { return lenguaje; } set { lenguaje = value; } }
        public string Plataforma { get { return plataforma; } set { plataforma = value; } }
        public byte   EnRed { get { return enRed; } set { enRed = value; } }
        public byte   EnSucursales { get { return enSucursales; } set { enSucursales = value; } }

        public int Guardar()
        {
            string c;
            string v;
            var cnn = new Conexion();

            cnn.Conectar();
            c = "Id,0,9|Codigo|Descripcion,1|Desarrollador,1|Lenguaje,1|Plataforma,1|EnRed|EnSucursales";
            v = id + "|" + codigo + "|" + descripcion + "|" + desarrollador + "|" + lenguaje + "|" + plataforma + "|" + enRed + "|" + enSucursales;
            id = cnn.GuardarRegistro(Tabla, c, v);
            cnn.Dispose();
            return id;
        }

        private void AsignarValores(IDataReader Registro)
        {
            id = Convert.ToInt32(Convert.IsDBNull(Registro["Id"]) ? 0 : Registro["Id"]);
            codigo = Convert.ToInt32(Convert.IsDBNull(Registro["Codigo"]) ? 0 : Registro["Codigo"]);
            descripcion = Convert.IsDBNull(Registro["Descripcion"]) ? "" : Registro["Descripcion"].ToString();
            desarrollador = Convert.IsDBNull(Registro["Desarrollador"]) ? "" : Registro["Desarrollador"].ToString();
            lenguaje = Convert.IsDBNull(Registro["Lenguaje"]) ? "" : Registro["Lenguaje"].ToString();
            plataforma = Convert.IsDBNull(Registro["Plataforma"]) ? "" : Registro["Plataforma"].ToString();
            enRed = Convert.ToByte(Convert.IsDBNull(Registro["EnRed"]) ? 0 : Registro["EnRed"]);
            enSucursales = Convert.ToByte(Convert.IsDBNull(Registro["EnSucursales"]) ? 0 : Registro["EnSucursales"]);
            Registro.Dispose();
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

        public bool BuscarPorDesc(string vDescripcion)
        {
            var cnn = new Conexion();
            bool result = false;
            cnn.Conectar();
            IDataReader Registro = cnn.ObtenerRegistro(Tabla, "Descripcion=" + cnn.strSql(vDescripcion));
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
            if (Respuesta)
            {
                cnn.RegistrarBitacora("Sistemas", "Borrar, Id: " + vId);
            }
            cnn.Dispose();
            return Respuesta;
        }

        public DataTable ListarTabla(string vDescripcion)
        {
            var cnn = new Conexion();
            string strFiltro;
            string strConsulta;

            //generar consulta SQL
            strFiltro = " WHERE Id>0";
            if (vDescripcion.Length > 0) strFiltro = strFiltro + " and Descripcion LIKE " + cnn.nSt(vDescripcion);
            strConsulta = "SELECT Id,Codigo,Descripcion,Desarrollador,Lenguaje,Plataforma,EnRed,EnSucursales FROM [" + Tabla + "]" + strFiltro + " ORDER BY Id";

            //obtener registros
            cnn.Conectar();
            DataTable Registro = cnn.GetTabla(strConsulta);
            cnn.Dispose();
            return Registro;
        }

    }
}
