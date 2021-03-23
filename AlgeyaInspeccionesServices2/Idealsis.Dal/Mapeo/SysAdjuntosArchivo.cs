using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Idealsis.Dal.Mapeo
{
    class SysAdjuntosArchivo
    {
        const string Tabla = "sysAdjuntosArchivo";
        public int   Id;
        public byte  Bandera;
        public Image Archivo;


        public void guardarArchivo(ref int vId, string Ruta)
        {
            string CommandText;
            var cnn = new Conexion();

            cnn.Conectar();
            if (File.Exists(Ruta))
            {
                Bandera = 1;
                vId = cnn.GuardarRegistro(Tabla, "Id,0,9|Bandera", vId + "|1");
                CommandText = "UPDATE [" + Tabla + "] SET Archivo=? WHERE Id=" + vId;
                cnn.GuardarVarBin(CommandText, Ruta);
            }
            else
            {
                cnn.Exec("DELETE FROM sysAdjuntosArchivo WHERE Id=" + vId);
            }
            Id = vId;
            cnn.Dispose();
        }
        public void guardarPicture(ref int vId, Image unPicture)// As IPictureDisp)
        {
            string Ruta="";
            string CommandText;
            var cnn = new Conexion();

            cnn.Conectar();
            Ruta = Path.GetTempFileName();
            unPicture.Save(Ruta);
            if (File.Exists(Ruta))
            {
                Bandera = 1;
                vId = cnn.GuardarRegistro(Tabla, "Id,0,9|Bandera", vId + "|1");
                CommandText = "UPDATE sysAdjuntosArchivo SET Archivo=? WHERE Id=" + Id;
                cnn.GuardarVarBin(CommandText, Ruta);
                File.Delete(Ruta);
            }
            else
            {
                cnn.Exec("DELETE FROM sysAdjuntosArchivo WHERE Id=" + vId);
            }
            Id = vId;
            unPicture.Dispose();
            cnn.Dispose();
        }

        public Image LeerImagen(int IdImagen) //As IPictureDisp
        { 
            IDataReader Registro;
            var cnn = new Conexion();

            cnn.Conectar();
            Archivo.Dispose();
            Registro = cnn.ObtenerRegistro("sysAdjuntosArchivo", "Id=" + IdImagen);
            if (Registro.Read())
            {
                if (!Convert.IsDBNull(Registro["Archivo"]))
                {
                    Archivo = cnn.LeerImagenBinaria(Registro["Archivo"], "");
                }
                Registro.Dispose();
            }
            cnn.Dispose();
            return Archivo;
        }

    }
}
