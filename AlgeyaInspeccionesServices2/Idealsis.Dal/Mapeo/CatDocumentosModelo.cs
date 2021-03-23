
using System;
using System.Data;

namespace Idealsis.Dal.Mapeo
{
    public class CatDocumentosModelo
    {
        const string Tabla = "CatDocumentosModelo";

        int    codigo;
        string descripcion;
        int    tipoAuditoria;
        int    tipoEntidad;
        byte   origenDatos;
        byte   activo;

        public int Codigo { get { return codigo; } set { codigo = value; } }
        public string Descripcion { get { return descripcion; } set { descripcion = value; } }
        public int TipoAuditoria { get { return tipoAuditoria; } set { tipoAuditoria = value; } }
        public int TipoEntidad { get { return tipoEntidad; } set { tipoEntidad = value; } }
        public byte OrigenDatos { get { return origenDatos; } set { origenDatos = value; } }
        public byte Activo { get { return activo; } set { activo = value; } }

        public void Guardar()
        {
            string c;
            string v;
            var cnn = new Conexion();

            cnn.Conectar();
            c = "Codigo,0,1|Descripcion,1|TipoAuditoria|TipoEntidad|OrigenDatos|Activo";
            v = codigo + "|" + descripcion + "|" + tipoAuditoria + "|" + tipoEntidad + "|" + origenDatos + "|" + activo;

            cnn.GuardarRegistro(Tabla, c, v);
            cnn.Dispose();
        }

        private void AsignarValores(IDataReader Registro)
        {
            codigo = Convert.ToInt32(Convert.IsDBNull(Registro["Codigo"]) ? 0 : Registro["Codigo"]);
            descripcion = Convert.IsDBNull(Registro["Descripcion"]) ? "" : Registro["Descripcion"].ToString();
            tipoAuditoria = Convert.ToByte(Convert.IsDBNull(Registro["TipoAuditoria"]) ? 0 : Registro["TipoAuditoria"]);
            tipoEntidad = Convert.ToByte(Convert.IsDBNull(Registro["TipoEntidad"]) ? 0 : Registro["TipoEntidad"]);
            origenDatos = Convert.ToByte(Convert.IsDBNull(Registro["OrigenDatos"]) ? 0 : Registro["OrigenDatos"]);
            activo = Convert.ToByte(Convert.IsDBNull(Registro["Activo"]) ? 0 : Registro["Activo"]);
            Registro.Dispose();
        }

        public bool Desplegar(int Direccion, short vCodigo)
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

        public bool BuscarPorId(short vCodigo)
        {
            var cnn = new Conexion();
            bool result = false;
            cnn.Conectar();
            IDataReader Registro = cnn.ObtenerRegistro(Tabla, "Codigo=" + vCodigo);
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

        public bool BorrarPorId(short vCodigo)
        {
            var cnn = new Conexion();
            bool Respuesta;
            cnn.Conectar();
            Respuesta = cnn.BorrarRegistro(Tabla, "Codigo=" + vCodigo, false);
            /*
            if (Respuesta)
            {
                cnn.RegistrarBitacora("Documentos modelo", "Borrar, Codigo: " + vCodigo);
            }
            */
            cnn.Dispose();
            return Respuesta;
        }

        public DataTable Listar(string vDescripcion, int TipoAuditoria, int TipoEntidad)
        {
            var cnn = new Conexion();
            string strFiltro;
            string strConsulta;

            //generar consulta SQL
            strFiltro = "";
            if (vDescripcion.Length > 0) strFiltro = strFiltro + " and Descripcion LIKE " + Helper.nSt(vDescripcion);
            if (TipoAuditoria > 0) strFiltro = strFiltro + " and TipoAuditoria=" + TipoAuditoria;
            if (TipoEntidad > 0) strFiltro = strFiltro + " and TipoEntidad=" + TipoEntidad;
            strConsulta = "SELECT Codigo,Descripcion,TipoAuditoria,TipoEntidad,OrigenDatos,Activo FROM [" + Tabla + "] WHERE Codigo>0" + strFiltro + " ORDER BY Descripcion";

            //obtener registros
            cnn.Conectar();
            DataTable Registro = cnn.GetTabla(strConsulta);
            cnn.Dispose();
            return Registro;
        }

        public int SiguienteCodigo()
        {
            int vCodigo;
            var cnn = new Conexion();
            cnn.Conectar();
            vCodigo = 1; if (cnn.ExisteRegistro(Tabla, "SELECT Max(Codigo) as c FROM [" + Tabla + "]", "c")) vCodigo = Convert.ToInt32(Helper.vg_DAT) + 1;
            cnn.Dispose();
            return vCodigo;
        }

    }
}
