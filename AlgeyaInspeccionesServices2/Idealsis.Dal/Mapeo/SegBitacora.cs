
using System;
using System.Data;

namespace Idealsis.Dal.Mapeo
{
    public class SegBitacora
    {
        const string   Tabla = "SegBitacora";
        int     id;
        string  fecha;
        string  hora;
        string  fechaSistema;
        string  usuario;
        string  opcion;
        string  accion;
        string  pc;
        string  version;
        short   sucursal;
        int     origen;
        int     idOrigen;
        int     idPersona;
        string  Filtros;

        public int Id { get { return id; } set { id = value; } }
        public string Fecha { get { return fecha; } set { fecha = value; } }
        public string Hora { get { return hora; } set { hora = value; } }
        public string FechaSistema { get { return fechaSistema; } set { fechaSistema = value; } }
        public string Usuario { get { return usuario; } set { usuario = value; } }
        public string Opcion { get { return opcion; } set { opcion = value; } }
        public string Accion { get { return accion; } set { accion = value; } }
        public string Pc { get { return pc; } set { pc = value; } }
        public string Version { get { return version; } set { version = value; } }
        public short Sucursal { get { return sucursal; } set { sucursal = value; } }
        public int Origen { get { return origen; } set { origen = value; } }
        public int IdOrigen { get { return idOrigen; } set { idOrigen = value; } }
        public int IdPersona { get { return idPersona; } set { idPersona = value; } }

        public void Guardar()
        {
            string c;
            string v;
            var cnn = new Conexion();

            cnn.Conectar();

            c = "Id,0,9|Fecha,2|Hora,3|FechaSistema,2|Usuario,1|Opcion,1|Accion,1|Pc,1|Version,1|Sucursal|Origen|IdOrige|IdPersona";
            v = id + "|" + fecha + "|" + hora + "|" + fechaSistema + "|" + usuario + "|" + Helper.Mid(opcion, 0, 60) + "|" + Helper.Mid(accion, 0, 150) + "|" + Helper.Mid(pc, 0, 50) + "|" + Helper.Mid(version, 0, 20) + "|" + origen + "|" + idOrigen + "|" + idPersona;
            id = cnn.GuardarRegistro(Tabla, c, v);
            cnn.Dispose();
        }
        private void AsignarValores(IDataReader Registro)
        {
            fecha = Convert.IsDBNull(Registro["Fecha"]) ? "" : Registro["Fecha"].ToString();
            hora = Convert.IsDBNull(Registro["Hora"]) ? "" : Registro["Hora"].ToString();
            fechaSistema = Convert.IsDBNull(Registro["FechaSistema"]) ? "" : Registro["FechaSistema"].ToString();
            usuario = Convert.IsDBNull(Registro["Usuario"]) ? "" : Registro["Usuario"].ToString();
            opcion = Convert.IsDBNull(Registro["Opcion"]) ? "" : Registro["Opcion"].ToString();
            accion = Convert.IsDBNull(Registro["Accion"]) ? "" : Registro["Accion"].ToString();
            pc = Convert.IsDBNull(Registro["Pc"]) ? "" : Registro["Pc"].ToString();
            version = Convert.IsDBNull(Registro["Version"]) ? "" : Registro["Version"].ToString();
            sucursal = Convert.ToInt16(Convert.IsDBNull(Registro["Sucursal"]) ? 0 : Registro["Sucursal"]);
            origen = Convert.ToInt32(Convert.IsDBNull(Registro["Origen"]) ? 0 : Registro["Origen"]);
            idOrigen = Convert.ToInt32(Convert.IsDBNull(Registro["IdOrigen"]) ? 0 : Registro["IdOrigen"]);
            idPersona = Convert.ToInt32(Convert.IsDBNull(Registro["IdPersona"]) ? 0 : Registro["IdPersona"]);
        }
        public bool ConsultarPorId(int vId)
        {
            var cnn = new Conexion();
            bool result = false;
            cnn.Conectar();
            IDataReader Registro = cnn.ObtenerRegistro(Tabla, "Id=" + vId.ToString());
            if (Registro.Read())
            {
                result = true;
                AsignarValores(Registro);
            }
            Registro.Dispose();
            cnn.Dispose();
            return result;
        }

        public void GenerarFiltros(string FechaIni, string FechaFin, string Usuario, int Operador1, string Opcion, string Opcion2, int Operador2, string Accion, string Accion2, string Pc)
        {
            Filtros = " WHERE Fecha between " + Helper.DateSQL(FechaIni) + " AND " + Helper.DateSQL(FechaFin);
            if (Usuario.Length > 0) Filtros = Filtros + " AND Usuario=" + Helper.strSql(Usuario);
            if (Opcion.Length > 0)
            {
                if (Operador1 > 0 && Opcion2.Length > 0)
                {
                    if (Operador1 == 1) Filtros = Filtros + " and (Opcion like " + Helper.nSt(Opcion) + " or Opcion like " + Helper.nSt(Opcion2) + ")";
                    if (Operador1 == 2) Filtros = Filtros + " and (Opcion like " + Helper.nSt(Opcion) + " and Opcion like " + Helper.nSt(Opcion2) + ")";
                }
                else
                {
                    Filtros = Filtros + " and Opcion like " + Helper.nSt(Opcion);
                }
            }
            if (Accion.Length > 0)
            {
                if (Operador2 > 0 && Accion2.Length > 0)
                {
                    if (Operador2 == 1) Filtros = Filtros + " and (Accion like " + Helper.nSt(Accion) + " or Accion like " + Helper.nSt(Accion2) + ")";
                    if (Operador2 == 2) Filtros = Filtros + " and (Accion like " + Helper.nSt(Accion) + " and Accion like " + Helper.nSt(Accion2) + ")";
                }
                else
                {
                    Filtros = Filtros + " and Accion like " + Helper.nSt(Accion);
                }
            }
            if (Pc.Length > 0) Filtros = Filtros + " AND Pc=" + Helper.strSql(Pc);
        }

        public int ConsultarPorFiltros(ref string strJSON)
        {
            int Count;
            var cnn = new Conexion();

            //obtener registros
            strJSON = "";
            Count = 0;
            cnn.Conectar();
            cnn.SQL = "SELECT Fecha,Hora,FechaSistema,Usuario,Opcion,Accion,Pc,Version,Sucursal FROM [" + Tabla + "] " + Filtros + " ORDER BY Fecha,Hora,Usuario";
            IDataReader Registro = cnn.AbreSQL();
            if (cnn.Hay(Registro))
            {
                strJSON = Json.ReaderToJson(Registro, ref Count);
                Registro.Dispose();
            }
            cnn.Dispose();
            return Count;
        }

        public void BorrarPorFiltros()
        {
            var cnn = new Conexion();
            cnn.Conectar();
            cnn.Exec("DELETE FROM [" + Tabla  + "] " + Filtros);
            cnn.Dispose();
        }

        public void BorrarPorId(int vpId)
        {
            var cnn = new Conexion();
            cnn.Conectar();
            cnn.Exec( "DELETE FROM [" + Tabla + "] WHERE Id=" + vpId.ToString());
            cnn.Dispose();
        }
    }
}
