using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Idealsis.Dal.Mapeo
{
    class SysAdjuntosRelacion
    {
        const string  Tabla = "sysAdjuntosRelacion";
        public byte   Catalogo;
        public int    IdRelacion;
        public int    Posicion;
        public int    Tipo;
        public string FechaAlta;
        public string UsuarioAlta;
        public string FechaMod;
        public string UsuarioMod;
        public string Nombre;
        public string Extension;
        public string Ruta;
        public int    IdArchivo;

        public void Guardar()
        {
            string c;
            string v;
            var cnn = new Conexion();

            cnn.Conectar();
            c = "Catalogo,0,1|IdRelacion,0,1|Posicion,0,1|Tipo|FechaAlta,2,2|UsuarioAlta,1,2|FechaMod,2|UsuarioMod,1|Nombre,1|Extension,1|Ruta,1|IdArchivo";
            v = Catalogo + "|" + IdRelacion + "|" + Posicion + "|" + Tipo + "|" + FechaAlta + "|" + Helper.Mid(UsuarioAlta, 0, 25) + "|" + FechaMod + "|" + Helper.Mid(UsuarioMod, 0, 25) + "|" + Helper.Mid(Nombre, 0, 50) + "|" + Helper.Mid(Extension, 0, 10) + "|" + Ruta + "|" + IdArchivo;
            cnn.GuardarRegistro(Tabla, c, v);
            cnn.Dispose();
            cnn = null;
        }

        private void AsignarValores(IDataReader Registro)
        {
            Catalogo = Convert.ToByte(Registro["Catalogo"].ToString());
            IdRelacion = Convert.ToInt32(Registro["IdRelacion"].ToString());
            Posicion = Convert.ToInt32(Registro["Posicion"].ToString());
            Tipo = Convert.ToInt32(Registro["Tipo"].ToString());
            FechaAlta = Registro["FechaAlta"].ToString();
            UsuarioAlta = Registro["UsuarioAlta"].ToString();
            FechaMod = Registro["FechaMod"].ToString();
            UsuarioMod = Registro["UsuarioMod"].ToString();
            Nombre = Registro["Nombre"].ToString();
            Extension = Registro["Extension"].ToString();
            Ruta = Registro["Ruta"].ToString();
            IdArchivo = Convert.ToInt32(Registro["IdArchivo"].ToString());
        }

        public bool ConsultarPorId(byte vCatalogo, int vIdRelacion, int vPosicion)
        {
            var cnn = new Conexion();
            bool result = false;
            cnn.Conectar();
            IDataReader Registro = cnn.ObtenerRegistro(Tabla, "Catalogo=" + vCatalogo + " and IdRelacion=" + vIdRelacion + " and Posicion=" + vPosicion);
            if (Registro.Read())
            {
                result = true;
                AsignarValores(Registro);
            }
            Registro.Dispose();
            cnn.Dispose();
            return result;
        }

        public void guardarImagenCatalogo(byte vCatalogo, int vId, int vPosicion, Image unPicture)
        {
            int IdArchivo;
            SysAdjuntosArchivo Archivo = new SysAdjuntosArchivo();
            var cnn = new Conexion();

            cnn.Conectar();
            IdArchivo = (int)Helper.Val(cnn.DameValor("sysAdjuntosRelacion", "Catalogo=" + vCatalogo + " and IdRelacion=" + vId + " and Posicion=" + vPosicion, "IdArchivo"));
            if (unPicture != null)
            {
                Archivo.guardarPicture(ref IdArchivo, unPicture);
                cnn.Exec("UPDATE sysAdjuntosRelacion SET IdArchivo=" + IdArchivo + " WHERE Catalogo=" + vCatalogo + " and IdRelacion=" + vId + " and Posicion=" + vPosicion);
            }
            else
            {
                cnn.Exec("DELETE FROM sysAdjuntosRelacion WHERE Catalogo=" + vCatalogo + " and IdRelacion=" + vId + " and Posicion=" + vPosicion);
                if (IdArchivo > 0) cnn.Exec("DELETE FROM sysAdjuntosArchivo WHERE Id=" + IdArchivo);
            }
            Archivo = null;
            cnn.Dispose();
            cnn = null;
        }

        public bool BorrarPorId(byte vCatalogo, int vIdRelacion, int vPosicion)
        {
            var cnn = new Conexion();
            bool Respuesta;
            cnn.Conectar();
            Respuesta = cnn.BorrarRegistro(Tabla, "Catalogo=" + vCatalogo + " and IdRelacion=" + vIdRelacion + " and Posicion=" + vPosicion, false);
            cnn.Dispose();
            cnn = null;
            return Respuesta;
        }

        public int ObtenerIdArchivo(byte vCatalogo, int vIdRelacion, int vPosicion)
        {
            var cnn = new Conexion();
            cnn.Conectar();
            int IdArchivo = Helper.Val(cnn.DameValor(Tabla, "Catalogo=" + vCatalogo + " and IdRelacion=" + vIdRelacion + " and Posicion=" + vPosicion, "IdArchivo"));
            cnn.Dispose();
            cnn = null;
            return IdArchivo;
        }

        public void guardarImagen(byte vCatalogo, int vId, int vPosicion, Image vPicture)
        {
            int IdArchivo;
            string c;
            string v;
            var cnn = new Conexion();

            cnn.Conectar();
            if (vPicture != null)
            {
                c = "Catalogo,0,1|IdRelacion,0,1|Posicion,0,1|Tipo|Ruta,1|IdArchivo,0,2";
                v = vCatalogo + "|" + vId + "|" + vPosicion + "|0||0";
                cnn.GuardarRegistro(Tabla, c, v);
                guardarImagenCatalogo(vCatalogo, vId, vPosicion, vPicture);
            }
            else
            {
                IdArchivo = Helper.Val(cnn.DameValor(Tabla, "Catalogo=" + vCatalogo + " and IdRelacion=" + vId + " and Posicion=" + vPosicion, "IdArchivo"));
                cnn.Exec( "DELETE FROM sysAdjuntosRelacion WHERE Catalogo=" + vCatalogo + " and IdRelacion=" + vId + " and Posicion=" + vPosicion);
                if (IdArchivo > 0) cnn.Exec( "DELETE FROM sysAdjuntosArchivo WHERE Id=" + IdArchivo);
            }
            cnn.Dispose();
            cnn = null;
        }

        public int Consultar(byte vCatalogo, int vIdRelacion, int vPosicion, string strJSON)
        {
            var cnn = new Conexion();
            int Count;
            string strFiltro;
            string strConSql;

            //generar consulta SQL
            strFiltro = " WHERE d.Catalogo=" + vCatalogo;
            if (vIdRelacion > 0) strFiltro = strFiltro + " and d.IdRelacion=" + vIdRelacion;
            if (vPosicion > 0) strFiltro = strFiltro + " and d.Posicion=" + vPosicion;

            strConSql = "SELECT d.Posicion, d.Tipo, catRequisitos.Descripcion, d.FechaAlta, d.Nombre, d.Extension, d.FechaMod, d.UsuarioMod" +
                        " FROM  sysAdjuntosRelacion as d LEFT JOIN catRequisitos ON d.Tipo=catRequisitos.Codigo" +
                        strFiltro +
                        " ORDER BY d.Posicion";

            //obtener registros
            strJSON = "";
            Count = 0;
            cnn.Conectar();
            cnn.SQL = strConSql;
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
