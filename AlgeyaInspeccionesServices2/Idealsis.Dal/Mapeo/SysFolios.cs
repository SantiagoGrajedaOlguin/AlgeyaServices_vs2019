using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Idealsis.Dal.Mapeo
{
    class SysFolios
    {
        const string Tabla = "sysFolios";
        public int    Id;
        public byte   Sucursal;
        public string Descripcion;
        public string Serie;
        public int    Folio;
        public byte   Estatus;
        public int    Formato;
        public int    IdSello;
        public string Leyenda;
        public void Guardar()
        {
            string c;
            string v;
            var cnn = new Conexion();

            cnn.Conectar();
            c = "Id,0,1|Sucursal,0,1|Descripcion,1|Serie,1|Folio|Estatus|Formato|IdSello|Leyenda,1";
            v = Id + "|" + Sucursal + "|" + Descripcion + "|" + Serie + "|" + Folio + "|" + Estatus + "|" + Formato + "|" + IdSello + "|" + Leyenda;
            cnn.GuardarRegistro(Tabla, c, v);
            cnn.Dispose();
            cnn = null;

        }
        private void AsignarValores(IDataReader Registro)
        {
            Id = Convert.ToInt32(Registro["Id"].ToString());
            Sucursal = Convert.ToByte(Registro["Sucursal"].ToString());
            Descripcion = Registro["Descripcion"].ToString();
            Serie = Registro["Serie"].ToString();
            Folio = Convert.ToInt32(Registro["Folio"].ToString());
            Estatus = Convert.ToByte(Registro["Estatus"].ToString());
            Formato = Convert.ToInt32(Registro["Formato"].ToString());
            IdSello = Convert.ToInt32(Registro["IdSello"].ToString());
            Leyenda = Registro["Leyenda"].ToString();
            Registro.Dispose();
        }

        public bool BuscarPorId(int vId, byte vSucursal)
        {
            IDataReader Registro;
            var cnn = new Conexion();
            bool Result = false;
            cnn.Conectar();
            Registro = cnn.ObtenerRegistro(Tabla, "Id=" + vId + " and Sucursal=" + vSucursal);
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
        public void BorrarPorId(int vId, byte vSucursal)
        {
            var cnn = new Conexion();
            cnn.Conectar();
            cnn.Exec("DELETE FROM [" + Tabla + "] WHERE Id=" + vId + " and Sucursal=" + vSucursal);
            cnn.Dispose();
            cnn = null;
        }
        public int AtraparFolio(byte vSucursal, int vId)
        {
            IDataReader vp_rs;
            var cnn = new Conexion();
            int Result=1;
            cnn.Conectar();
            vp_rs = cnn.ObtenerRegistro(Tabla, "Id=" + vId + " and Sucursal=" + vSucursal);
            if (vp_rs.Read())
            {
                Result = Convert.ToInt32(vp_rs["Folio"]);
                cnn.Exec("UPDATE [" + Tabla + "] SET Estatus=1, Folio=Folio+1 WHERE Id=" + vId + " and Sucursal=" + vSucursal);
            }
            else
            {
                Id = vId;
                Sucursal = vSucursal;
                Descripcion = "Expedientes";
                Folio = 1;
                Estatus = 1;
                Guardar();
            }
            cnn.Dispose();
            cnn = null;
            return Result;
        }

        public void LiberarFolio(byte vSucursal, int vId)
        {
            var cnn = new Conexion();
            cnn.Conectar();
            cnn.Exec("UPDATE sysFolios SET Estatus=0 WHERE Id=" + vId + " and Sucursal=" + vSucursal);
            cnn.Dispose();
            cnn = null;
        }

        public int DameFolio(byte vSucursal, int vId)
        {
            var cnn = new Conexion();
            int Result = 1;
            cnn.Conectar();
            if (cnn.ExisteRegistro(Tabla, "Id=" + vId + " and Sucursal=" + vSucursal, "Folio"))
            {
                Result = Helper.Val(Helper.vg_DAT);
            }
            else
            {
                cnn.GuardarRegistro(Tabla, "Id,0,1|Sucursal,0,1|Descripcion,1,2|Folio|Estatus", vId + "|" + vSucursal + "| |1|0");
            }
            cnn.Dispose();
            cnn = null;
            return Result;
        }

        public string DameSerie(byte vSucursal, int vId)
        {
            var cnn = new Conexion();
            cnn.Conectar();
            string Result = cnn.DameValor(Tabla, "Id=" + vId + " and Sucursal=" + vSucursal, "Serie");
            cnn.Dispose();
            cnn = null;
            return Result;
        }
    }
}
