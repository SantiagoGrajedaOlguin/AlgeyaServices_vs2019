using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Idealsis.Negocio
{
    public class Expediente
    {
        int    id;
        string folio;
        string codigo;
        string fecha;
        string hora;
        int    cliente;
        int    tipoCliente;
        int    auditor;
        int    tipoAuditoria;
        string descripcion;
        string fechaAceptacion;
        string fechaInicio;
        
        //CAMPOS NUEVOS
        string fechaCierre;          // Fecha 
        string fechaDictamen;        // Fecha
        string folioDictamen;        // Captura directa alfanumerico
        int    numRecomendaciones;   // Numero de recomendaciones, Captura directa numero
        int    numHallazgos;         // Podria ser calculado, o dejarlo como capura directa numero
        int    estatusInforme;       // Estatus de informe, Combo, Catalogo de estatus 84
        string periodoAuditar;       // Cadena de texto libre
        int    conclusion;           // Conclusión del informe: Cumple, Mayormente Cumple, etc. Catalogo de cumplimiento: 93
        /******************/


        string vencimiento;
        int    estatus;
        string usuario;
        int    numEntregados;
        int    numPendientes;
        string usuarioMod;
        string fechaMod;
        byte   terminada;

        string clienteDesc;
        string tipoClienteDesc;
        string auditorDesc;
        string tipoAuditoriaDesc;
        string estatusDesc;
        string estatusInformeDesc;
        string conclusionDesc;

        public int Id { get => id; set => id = value; }
        public string Folio { get => folio; set => folio = value; }
        public string Codigo { get => codigo; set => codigo = value; }
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
        public string FolioDictamen { get => folioDictamen; set => folioDictamen = value; }
        public int NumRecomendaciones { get => numRecomendaciones; set => numRecomendaciones = value; }
        public int NumHallazgos { get => numHallazgos; set => numHallazgos = value; }
        public int EstatusInforme { get => estatusInforme; set => estatusInforme = value; }
        public int Conclusion { get => conclusion; set => conclusion = value; }
        public string PeriodoAuditar { get => periodoAuditar; set => periodoAuditar = value; }

        public string Vencimiento { get => vencimiento; set => vencimiento = value; }
        public int Estatus { get => estatus; set => estatus = value; }
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
    }
}
