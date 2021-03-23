
using System;
using System.Data;
using System.Data.SqlTypes;
using System.Globalization;
using System.Windows.Forms;

namespace Idealsis.Dal.Mapeo
{
    public class ExpedientesCuerpo
    {
        const string Tabla = "ExpedientesCuerpo";

        int    id;
        string folio;
        string fecha;
        string hora;
        int    cliente;
        int    tipoCliente;
        int    auditor;
        int    tipoAuditoria;
        string descripcion;
        string fechaAceptacion;
        string fechaInicio;
        string fechaCierre;
        string fechaDictamen;
        string folioDictamen;
        int    numRecomendaciones;
        int    numHallazgos;
        int    estatusInforme;
        string periodoAuditar;
        string vencimiento;
        int    estatus;
        string usuario;
        int    numEntregados;
        int    numPendientes;
        string usuarioMod;
        string fechaMod;
        byte   terminada;
        int    conclusion;

        string clienteDesc;
        string tipoClienteDesc;
        string auditorDesc;
        string tipoAuditoriaDesc;
        string estatusDesc;
        string estatusInformeDesc;
        string conclusionDesc;


        public int Id { get => id; set => id = value; }
        public string Folio { get => folio; set => folio = value; }
        public string Fecha { get => fecha; set => fecha = value; }
        public string Hora { get => hora; set => hora = value; }
        public int Cliente { get => cliente; set => cliente = value; }
        public int TipoCliente { get => tipoCliente; set => tipoCliente = value; }
        public int Auditor { get => auditor; set => auditor = value; }
        public int TipoAuditoria { get => tipoAuditoria; set => tipoAuditoria = value; }
        public string Descripcion { get => descripcion; set => descripcion = value; }
        public string FechaAceptacion { get => fechaAceptacion; set => fechaAceptacion = value; }
        public string FechaInicio { get => fechaInicio; set => fechaInicio = value; }
        public string FechaCierre { get => fechaCierre; set => fechaCierre = value; }
        public string FechaDictamen { get => fechaDictamen; set => fechaDictamen = value; }
        public int NumRecomendaciones { get => numRecomendaciones; set => numRecomendaciones = value; }
        public int NumHallazgos { get => numHallazgos; set => numHallazgos = value; }
        public string Vencimiento { get => vencimiento; set => vencimiento = value; }
        public string FolioDictamen { get => folioDictamen; set => folioDictamen = value; }
        public string PeriodoAuditar { get => periodoAuditar; set => periodoAuditar = value; }
        public int Estatus { get => estatus; set => estatus = value; }
        public int EstatusInforme { get => estatusInforme; set => estatusInforme = value; }
        public int Conclusion { get => conclusion; set => conclusion = value; }
        public string Usuario { get => usuario; set => usuario = value; }
        public int NumEntregados { get => numEntregados; set => numEntregados = value; }
        public int NumPendientes { get => numPendientes; set => numPendientes = value; }
        public string UsuarioMod { get => usuarioMod; set => usuarioMod = value; }
        public string FechaMod { get => fechaMod; set => fechaMod = value; }
        public byte Terminada { get => terminada; set => terminada = value; }
        public string ClienteDesc { get => clienteDesc; set => clienteDesc = value; }
        public string TipoClienteDesc { get => tipoClienteDesc; set => tipoClienteDesc = value; }
        public string AuditorDesc { get => auditorDesc; set => auditorDesc = value; }
        public string TipoAuditoriaDesc { get => tipoAuditoriaDesc; set => tipoAuditoriaDesc = value; }
        public string EstatusDesc { get => estatusDesc; set => estatusDesc = value; }
        public string EstatusInformeDesc { get => estatusInformeDesc; set => estatusInformeDesc = value; }
        public string ConclusionDesc { get => conclusionDesc; set => conclusionDesc = value; }

        public int Guardar()
        {
            string c;
            string v;
            var cnn = new Conexion();

            cnn.Conectar();

            c = "Id,0,9|Folio,1|Fecha,2|Hora,3|Cliente|TipoCliente|Auditor|TipoAuditoria|Descripcion,1|FechaAceptacion,2|FechaInicio,2|FechaCierre,2|Vencimiento,2|";
            v = id + "|" + folio + "|" + fecha + "|" + hora + "|" + cliente + "|" + tipoCliente + "|" + auditor + "|" + tipoAuditoria + "|" + Helper.Mid(descripcion,0,200) + "|" + fechaAceptacion + "|" + fechaInicio + "|" + fechaCierre + "|" + vencimiento + "|";
            
            c = c + "Estatus|Conclusion|Usuario,1|NumEntregados,0,2|NumPendientes,0,2|UsuarioMod,1|FechaMod,2|Terminada,0,2|";
            v = v + estatus + "|" + conclusion + "|" + usuario + "|" + numEntregados + "|" + numPendientes + "|" + usuarioMod + "|" + fechaMod + "|" + terminada + "|";

            c = c + "FechaDictamen,2|FolioDictamen,1|NumRecomendaciones|NumHallazgos|EstatusInforme|PeriodoAuditar,1";
            v = v + fechaDictamen + "|" + folioDictamen + "|" + numRecomendaciones + "|" + numHallazgos + "|" + estatusInforme + "|" + periodoAuditar;

            id = cnn.GuardarRegistro(Tabla, c, v);

            cnn.Dispose();
            return Id;
        }

        private void AsignarValores(IDataReader Registro)
        {
            var cnn = new Conexion();
            //CultureInfo culture = new CultureInfo("es-MX");

            cnn.Conectar();
            id = Convert.ToInt32(Convert.IsDBNull(Registro["Id"]) ? 0 : Registro["Id"]);
            folio = Convert.IsDBNull(Registro["Folio"]) ? "" : Registro["Folio"].ToString();
            fecha = Convert.IsDBNull(Registro["Fecha"]) ? "" : Convert.ToDateTime(Registro["Fecha"]).ToString("dd/MM/yyyy hh:mm:ss tt");
            hora = Convert.IsDBNull(Registro["Hora"]) ? "" : Registro["Hora"].ToString();
            cliente = Convert.ToInt32(Convert.IsDBNull(Registro["Cliente"]) ? 0 : Registro["Cliente"]);
            tipoCliente = Convert.ToInt32(Convert.IsDBNull(Registro["TipoCliente"]) ? 0 : Registro["TipoCliente"]);
            auditor = Convert.ToInt32(Convert.IsDBNull(Registro["Auditor"]) ? 0 : Registro["Auditor"]);
            tipoAuditoria = Convert.ToInt32(Convert.IsDBNull(Registro["TipoAuditoria"]) ? 0 : Registro["TipoAuditoria"]);
            descripcion = Convert.IsDBNull(Registro["Descripcion"]) ? "" : Registro["Descripcion"].ToString();
            fechaAceptacion = Convert.IsDBNull(Registro["FechaAceptacion"]) ? "" : Convert.ToDateTime(Registro["FechaAceptacion"]).ToString("dd/MM/yyyy hh:mm:ss tt");
            fechaInicio = Convert.IsDBNull(Registro["FechaInicio"]) ? "" : Convert.ToDateTime(Registro["FechaInicio"]).ToString("dd/MM/yyyy hh:mm:ss tt");
            fechaCierre = Convert.IsDBNull(Registro["FechaCierre"]) ? "" : Convert.ToDateTime(Registro["FechaCierre"]).ToString("dd/MM/yyyy hh:mm:ss tt");
            vencimiento = Convert.IsDBNull(Registro["Vencimiento"]) ? "" : Convert.ToDateTime(Registro["Vencimiento"]).ToString("dd/MM/yyyy hh:mm:ss tt");
            estatus = Convert.ToInt32(Convert.IsDBNull(Registro["Estatus"]) ? 0 : Registro["Estatus"]);
            conclusion = Convert.ToInt32(Convert.IsDBNull(Registro["Conclusion"]) ? 0 : Registro["Conclusion"]);
            usuario = Convert.IsDBNull(Registro["Usuario"]) ? "" : Registro["Usuario"].ToString();

            fechaDictamen = Convert.IsDBNull(Registro["FechaDictamen"]) ? "" : Convert.ToDateTime(Registro["FechaDictamen"]).ToString("dd/MM/yyyy hh:mm:ss tt");
            folioDictamen = Convert.IsDBNull(Registro["FolioDictamen"]) ? "" : Registro["FolioDictamen"].ToString();
            numRecomendaciones = Convert.ToInt32(Convert.IsDBNull(Registro["NumRecomendaciones"]) ? 0 : Registro["NumRecomendaciones"]);
            numHallazgos = Convert.ToInt32(Convert.IsDBNull(Registro["NumHallazgos"]) ? 0 : Registro["NumHallazgos"]);
            estatusInforme = Convert.ToInt32(Convert.IsDBNull(Registro["EstatusInforme"]) ? 0 : Registro["EstatusInforme"]);
            periodoAuditar = Convert.IsDBNull(Registro["PeriodoAuditar"]) ? "" : Registro["PeriodoAuditar"].ToString();

            numEntregados = Convert.ToInt32(Convert.IsDBNull(Registro["NumEntregados"]) ? 0 : Registro["NumEntregados"]);
            numPendientes = Convert.ToInt32(Convert.IsDBNull(Registro["NumPendientes"]) ? 0 : Registro["NumPendientes"]);
            usuarioMod = Convert.IsDBNull(Registro["UsuarioMod"]) ? "" : Registro["UsuarioMod"].ToString();
            fechaMod = Convert.IsDBNull(Registro["FechaMod"]) ? "" : Registro["FechaMod"].ToString();
            terminada = Convert.ToByte(Convert.IsDBNull(Registro["Terminada"]) ? 0 : Registro["Terminada"]);
            clienteDesc = cnn.DameValor("CatPersonas", "Id=" + cliente, "Descripcion");
            tipoClienteDesc = cnn.DameValor("SysCatalogos", "Id=" + tipoCliente, "Descripcion");
            auditorDesc = cnn.DameValor("CatPersonas", "Id=" + auditor, "Descripcion");
            tipoAuditoriaDesc = cnn.DameValor("SysCatalogos", "Id=" + tipoAuditoria, "Descripcion");
            estatusDesc = cnn.DameValor("SysCatalogos", "Id=" + estatus, "Descripcion");
            estatusInformeDesc = cnn.DameValor("SysCatalogos", "Id=" + estatusInforme, "Descripcion");
            conclusionDesc = cnn.DameValor("SysCatalogos", "Id=" + conclusion, "Descripcion");

            Registro.Dispose();
            cnn.Dispose();
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

        public DataTable ListarTabla(int IdUsuario)
        {
            var cnn = new Conexion();
            string strFiltro="";
            string strConsulta;

            cnn.Conectar();

            //generar consulta SQL
            if (IdUsuario>0)
            {
                if (cnn.ExisteRegistro("SegCatalogosRestricciones","Usuario=" + IdUsuario + " and Tipo=1","Id"))
                {
                    strFiltro = strFiltro + " and Exists(SELECT Id FROM SegCatalogosRestricciones WHERE Usuario=" + IdUsuario + " and Tipo=1 and Codigo=d.Cliente)";
                }
                if (cnn.ExisteRegistro("SegCatalogosRestricciones", "Usuario=" + IdUsuario + " and Tipo=2", "Id"))
                {
                    strFiltro = strFiltro + " and Exists(SELECT Id FROM SegCatalogosRestricciones WHERE Usuario=" + IdUsuario + " and Tipo=2 and Codigo=d.Id)";
                }
            }
            strConsulta = "SELECT d.*,CatPersonas.Descripcion as ClienteDesc,TiposCliente.Descripcion as TipoClienteDesc, Auditores.Descripcion as AuditorDesc, TiposAuditoria.Descripcion as TipoAuditoriaDesc, CatEstatus.Descripcion as EstatusDesc, CatConclusiones.Descripcion as ConclusionDesc, EstInforme.Descripcion as EstatusInformeDesc" + 
                          " FROM (((((([" + Tabla + "] as d LEFT JOIN CatPersonas ON d.Cliente=CatPersonas.Id) LEFT JOIN SysCatalogos as TiposCliente ON d.TipoCliente=TiposCliente.Id) LEFT JOIN CatPersonas as Auditores ON d.Auditor=Auditores.Id) LEFT JOIN SysCatalogos as TiposAuditoria ON d.TipoAuditoria=TiposAuditoria.Id) LEFT JOIN SysCatalogos as CatEstatus ON d.Estatus=CatEstatus.Id) LEFT JOIN SysCatalogos as CatConclusiones ON d.Conclusion=CatConclusiones.Id) LEFT JOIN SysCatalogos as EstInforme ON d.EstatusInforme=EstInforme.Id" + 
                          " WHERE d.Id>0" + strFiltro + 
                          " ORDER BY d.Id";

            //obtener registros
            DataTable Registro = cnn.GetTabla(strConsulta);
            cnn.Dispose();
            return Registro;
        }
    }
}
