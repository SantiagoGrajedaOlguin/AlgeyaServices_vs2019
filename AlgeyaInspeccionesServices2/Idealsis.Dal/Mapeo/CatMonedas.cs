
using System;
using System.Data;

namespace Idealsis.Dal.Mapeo
{
    public class CatMonedas
    {
        const string Tabla = "CatMonedas";

        short  codigo;
        string descripcion;
        string simbolo;
        string texto;
        byte   esPred;
        byte   esNacional;
        string claveSat;

        public short  Codigo { get { return codigo; } set { codigo = value; } }
        public string Descripcion { get { return descripcion; } set { descripcion = value; } }
        public string Simbolo { get { return simbolo; } set { simbolo = value; } }
        public string Texto { get { return texto; } set { texto = value; } }
        public byte   EsPred { get { return esPred; } set { esPred = value; } }
        public byte   EsNacional { get { return esNacional; } set { esNacional = value; } }
        public string ClaveSat { get { return claveSat; } set { claveSat = value; } }

        public void Guardar()
        {
            string c;
            string v;
            var cnn = new Conexion();

            cnn.Conectar();
            c = "Codigo,0,1|Descripcion,1|Simbolo,1|Texto,1|EsPred|EsNacional|ClaveSat,1";
            v = codigo + "|" + descripcion + "|" + simbolo + "|" + texto + "|" + esPred + "|" + esNacional + "|" + claveSat;

            if (esPred > 0) cnn.Exec("UPDATE [" + Tabla + "] SET EsPred=0 WHERE Codigo<>" + codigo);

            cnn.GuardarRegistro(Tabla, c, v);
            //cnn.RegistrarBitacora("Monedas", (cnn.EsAlta ? "Alta" : "Modificación") + ", Codigo: " + codigo + " " + descripcion);
            cnn.Dispose();
        }

        private void AsignarValores(IDataReader Registro)
        {
            codigo = Convert.ToInt16(Convert.IsDBNull(Registro["Codigo"]) ? 0 : Registro["Codigo"]);
            descripcion = Convert.IsDBNull(Registro["Descripcion"]) ? "" : Registro["Descripcion"].ToString();
            simbolo = Convert.IsDBNull(Registro["Simbolo"]) ? "" : Registro["Simbolo"].ToString();
            texto = Convert.IsDBNull(Registro["Texto"]) ? "" : Registro["Texto"].ToString();
            esNacional = Convert.ToByte(Convert.IsDBNull(Registro["EsNacional"]) ? 0 : Registro["EsNacional"]);
            esPred = Convert.ToByte(Convert.IsDBNull(Registro["EsPred"]) ? 0 : Registro["EsPred"]);
            claveSat = Convert.IsDBNull(Registro["ClaveSat"]) ? "" : Registro["ClaveSat"].ToString();
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
            if (Respuesta)
            {
                cnn.RegistrarBitacora("Monedas", "Borrar, Codigo: " + vCodigo);
            }
            cnn.Dispose();
            return Respuesta;
        }

        public DataTable Listar(string vDescripcion)
        {
            var cnn = new Conexion();
            string strFiltro;
            string strConsulta;

            //generar consulta SQL
            strFiltro = "";
            if (vDescripcion.Length > 0) strFiltro = strFiltro + " and Descripcion LIKE " + Helper.nSt(vDescripcion);
            strConsulta = "SELECT Codigo,Descripcion,Simbolo,Texto,EsPred,EsNacional,ClaveSat FROM [" + Tabla + "] WHERE Codigo>0" + strFiltro + " ORDER BY Descripcion";

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
