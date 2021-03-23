using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Idealsis.Dal.Mapeo
{
    public class SysCatalogosConfig
    {
        const string  Tabla = "SysCatalogosConfig";
        short  codigo;
        string descripcion;
        string etiqueta;
        byte   genero;
        byte   conCodigo;
        byte   conEstatus;
        byte   conCorta;
        byte   bandera;
        byte   detalle;
        byte   conPred;
        short  catSat;
        short  catPadre;
        short  catHijo;
        string menuHijo;

        public short Codigo { get { return codigo; } set { codigo = value; } }
        public string Descripcion { get { return descripcion; } set { descripcion = value; } }
        public string Etiqueta { get { return etiqueta; } set { etiqueta = value; } }
        public byte Genero { get { return genero; } set { genero = value; } }
        public byte ConCodigo { get { return conCodigo; } set { conCodigo = value; } }
        public byte ConEstatus { get { return conEstatus; } set { conEstatus = value; } }
        public byte ConCorta { get { return conCorta; } set { conCorta = value; } }
        public byte Bandera { get { return bandera; } set { bandera = value; } }
        public byte Detalle { get { return detalle; } set { detalle = value; } }
        public byte ConPred { get { return conPred; } set { conPred = value; } }
        public short CatSat { get { return catSat; } set { catSat = value; } }
        public short CatPadre { get { return catPadre; } set { catPadre = value; } }
        public short CatHijo { get { return catHijo; } set { catHijo = value; } }
        public string MenuHijo { get { return menuHijo; } set { menuHijo = value; } }

        public void Guardar()
        { 
            string c;
            string v;
            var cnn = new Conexion();

            cnn.Conectar();
            c = "Codigo,0,1|Descripcion,1|Etiqueta,1|Genero|ConCodigo|ConEstatus|ConCorta|Bandera|Detalle|ConPred|CatSat|CatPadre|CatHijo|MenuHijo,1";
            v = codigo + "|" + descripcion + "|" + etiqueta + "|" + genero + "|" + conCodigo + "|" + conEstatus + "|" + conCorta + "|" + bandera + "|" + detalle + "|" + conPred + "|" + catSat + "|" + catPadre + "|" + catHijo + "|" + menuHijo;
            cnn.GuardarRegistro( Tabla, c, v);
            cnn.Dispose();
            cnn = null;
        }

        private void AsignarValores(IDataReader Registro)
        {
            codigo = Convert.ToInt16(Registro["Codigo"].ToString());
            descripcion = Convert.IsDBNull(Registro["Descripcion"]) ? "" : Registro["Descripcion"].ToString(); 
            etiqueta = Convert.IsDBNull(Registro["Etiqueta"]) ? "" : Registro["Etiqueta"].ToString(); 
            genero = Convert.ToByte(Convert.IsDBNull(Registro["Genero"]) ? 0 : Registro["Genero"]); 
            conCodigo = Convert.ToByte(Convert.IsDBNull(Registro["ConCodigo"]) ? 0 : Registro["ConCodigo"]);
            conEstatus = Convert.ToByte(Convert.IsDBNull(Registro["ConEstatus"]) ? 0 : Registro["ConEstatus"]);
            conCorta = Convert.ToByte(Convert.IsDBNull(Registro["ConCorta"]) ? 0 : Registro["ConCorta"]); 
            bandera = Convert.ToByte(Convert.IsDBNull(Registro["Bandera"]) ? 0 : Registro["Bandera"]); 
            detalle = Convert.ToByte(Convert.IsDBNull(Registro["Detalle"]) ? 0 : Registro["Detalle"]); 
            conPred = Convert.ToByte(Convert.IsDBNull(Registro["ConPred"]) ? 0 : Registro["ConPred"]); 
            catSat = Convert.ToInt16(Convert.IsDBNull(Registro["CatSat"]) ? 0 : Registro["CatSat"]); 
            catPadre = Convert.ToInt16(Convert.IsDBNull(Registro["CatPadre"]) ? 0 : Registro["CatPadre"]); 
            catHijo = Convert.ToInt16(Convert.IsDBNull(Registro["CatHijo"]) ? 0 : Registro["CatHijo"]);
            menuHijo = Convert.IsDBNull(Registro["MenuHijo"]) ? "" : Registro["MenuHijo"].ToString();
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

        public string ObtenerValor(short vCodigo, string Campo)
        {
            var cnn = new Conexion();
            string result = "";
            cnn.Conectar();
            result = cnn.DameValor(Tabla, "Codigo=" + vCodigo, Campo);
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

        public bool BorrarPorId(short vCodigo, bool Preguntar)
        {
            var cnn = new Conexion();
            bool Respuesta;
            cnn.Conectar();
            Respuesta = cnn.BorrarRegistro(Tabla, "Codigo=" + vCodigo, Preguntar);
            cnn.Dispose();
            return Respuesta;
        }

        public DataTable Listar(string vDescripcion)
        {
            var cnn = new Conexion();
            string strFiltro;
            string strConsulta;

            //generar consulta SQL
            strFiltro = " WHERE Codigo>0";
            if (vDescripcion.Length > 0) strFiltro = strFiltro + " and Descripcion LIKE " + cnn.nSt(vDescripcion);
            strConsulta = "SELECT Codigo, Descripcion, Etiqueta, ConCodigo, ConCorta, CatPadre, CatHijo, ConEstatus, Bandera FROM [" + Tabla + "]" + strFiltro + " ORDER BY Codigo";

            //obtener registros
            cnn.Conectar();
            DataTable Registro = cnn.GetTabla(strConsulta);
            cnn.Dispose();
            return Registro;
        }

    }
}
