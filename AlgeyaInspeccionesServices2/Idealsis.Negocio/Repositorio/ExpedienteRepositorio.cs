using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Idealsis.Dal;
using Idealsis.Dal.Mapeo;
using Newtonsoft.Json.Linq;

namespace Idealsis.Negocio.Repositorio
{
    public class ExpedienteRepositorio
    {
        ExpedientesCuerpo Catalogo;

        public ExpedienteRepositorio()
        {
            Catalogo = new ExpedientesCuerpo();
        }

        public Expediente BuscarPorId(int Id)
        {

            if (Catalogo.BuscarPorId(Id))
            {
                Expediente Result = new Expediente
                {
                    Id = Catalogo.Id,
                    Folio = Catalogo.Folio,
                    Fecha = Catalogo.Fecha,
                    Hora = Catalogo.Hora,
                    Cliente = Catalogo.Cliente,
                    TipoCliente = Catalogo.TipoCliente,
                    Auditor = Catalogo.Auditor,
                    TipoAuditoria = Catalogo.TipoAuditoria,
                    Descripcion = Catalogo.Descripcion,
                    FechaAceptacion = Catalogo.FechaAceptacion,
                    FechaInicio = Catalogo.FechaInicio,
                    FechaCierre = Catalogo.FechaCierre,
                    FechaDictamen = Catalogo.FechaDictamen,
                    FolioDictamen = Catalogo.FolioDictamen,
                    NumRecomendaciones = Catalogo.NumRecomendaciones,
                    NumHallazgos = Catalogo.NumHallazgos,
                    EstatusInforme = Catalogo.EstatusInforme,
                    PeriodoAuditar = Catalogo.PeriodoAuditar,
                    Conclusion = Catalogo.Conclusion,
                    Vencimiento = Catalogo.Vencimiento,
                    Estatus = Catalogo.Estatus,
                    Usuario = Catalogo.Usuario,
                    NumEntregados = Catalogo.NumEntregados,
                    NumPendientes = Catalogo.NumPendientes,
                    UsuarioMod = Catalogo.UsuarioMod,
                    FechaMod = Catalogo.FechaMod,
                    Terminada = Catalogo.Terminada,
                    ClienteDesc = Catalogo.ClienteDesc,
                    TipoClienteDesc = Catalogo.TipoClienteDesc,
                    AuditorDesc = Catalogo.AuditorDesc,
                    EstatusDesc = Catalogo.EstatusDesc,
                    EstatusInformeDesc = Catalogo.EstatusInformeDesc,
                    ConclusionDesc = Catalogo.ConclusionDesc,
                    TipoAuditoriaDesc = Catalogo.TipoAuditoriaDesc
                };
                return Result;
            }
            else
            {
                return null;
            }
        }
        public int Guardar(Expediente entidad)
        {
            int Id = entidad.Id;
            Catalogo.Id = Id;
            Catalogo.Folio  = entidad.Folio;
            Catalogo.Fecha = entidad.Fecha;
            Catalogo.Hora = entidad.Hora;
            Catalogo.Cliente = entidad.Cliente;
            Catalogo.TipoCliente = entidad.TipoCliente;
            Catalogo.Auditor = entidad.Auditor;
            Catalogo.TipoAuditoria = entidad.TipoAuditoria;
            Catalogo.Descripcion = entidad.Descripcion;
            Catalogo.FechaAceptacion = entidad.FechaAceptacion;
            Catalogo.FechaInicio = entidad.FechaInicio;
            Catalogo.FechaCierre = entidad.FechaCierre;
            Catalogo.FechaDictamen = entidad.FechaDictamen;
            Catalogo.FolioDictamen = entidad.FolioDictamen;
            Catalogo.NumRecomendaciones = entidad.NumRecomendaciones;
            Catalogo.NumHallazgos = entidad.NumHallazgos;
            Catalogo.EstatusInforme = entidad.EstatusInforme;
            Catalogo.PeriodoAuditar = entidad.PeriodoAuditar;
            Catalogo.Conclusion = entidad.Conclusion;
            Catalogo.Vencimiento = entidad.Vencimiento;
            Catalogo.Estatus = entidad.Estatus;
            Catalogo.Usuario = entidad.Usuario;
            Catalogo.NumEntregados = entidad.NumEntregados;
            Catalogo.NumPendientes = entidad.NumPendientes;
            Catalogo.UsuarioMod = entidad.UsuarioMod;
            Catalogo.FechaMod = entidad.FechaMod;
            Id = Catalogo.Guardar();
            return Id;
        }
        public bool Remove(int Id)
        {
            return Catalogo.BorrarPorId(Id);
        }
        public List<Expediente> GetAll(int IdUsuario)
        {
            List<Expediente> Lista = new List<Expediente>();
            DataTable Datos = Catalogo.ListarTabla(IdUsuario);
            if (Datos != null)
            {
                foreach (DataRow row in Datos.Rows)
                {
                    Lista.Add(new Expediente()
                    {
                        Id = Convert.ToInt32(Convert.IsDBNull(row["Id"]) ? 0 : row["Id"]),
                        Folio = Convert.IsDBNull(row["Folio"]) ? "" : row["Folio"].ToString(),
                        Codigo = Convert.IsDBNull(row["Folio"]) ? "" : row["Folio"].ToString(),
                        Fecha = Convert.IsDBNull(row["Fecha"]) ? "" : Convert.ToDateTime(row["Fecha"]).ToString("dd/MM/yyyy hh:mm:ss tt"),
                        Hora = Convert.IsDBNull(row["Hora"]) ? "" : row["Hora"].ToString(),
                        Cliente = Convert.ToInt32(Convert.IsDBNull(row["Cliente"]) ? 0 : row["Cliente"]),
                        TipoCliente = Convert.ToInt32(Convert.IsDBNull(row["TipoCliente"]) ? 0 : row["TipoCliente"]),
                        Auditor = Convert.ToInt32(Convert.IsDBNull(row["Auditor"]) ? 0 : row["Auditor"]),
                        TipoAuditoria = Convert.ToInt32(Convert.IsDBNull(row["TipoAuditoria"]) ? 0 : row["TipoAuditoria"]),
                        Descripcion = Convert.IsDBNull(row["Descripcion"]) ? "" : row["Descripcion"].ToString(),
                        FechaAceptacion = Convert.IsDBNull(row["FechaAceptacion"]) ? "" : Convert.ToDateTime(row["FechaAceptacion"]).ToString("dd/MM/yyyy hh:mm:ss tt"),
                        FechaInicio = Convert.IsDBNull(row["FechaInicio"]) ? "" : Convert.ToDateTime(row["FechaInicio"]).ToString("dd/MM/yyyy hh:mm:ss tt"),
                        FechaCierre = Convert.IsDBNull(row["FechaCierre"]) ? "" : Convert.ToDateTime(row["FechaCierre"]).ToString("dd/MM/yyyy hh:mm:ss tt"),
                        FechaDictamen = Convert.IsDBNull(row["FechaDictamen"]) ? "" : Convert.ToDateTime(row["FechaDictamen"]).ToString("dd/MM/yyyy hh:mm:ss tt"),
                        FolioDictamen = Convert.IsDBNull(row["FolioDictamen"]) ? "" : row["FolioDictamen"].ToString(),
                        NumRecomendaciones = Convert.ToInt32(Convert.IsDBNull(row["NumRecomendaciones"]) ? 0 : row["NumRecomendaciones"]),
                        NumHallazgos = Convert.ToInt32(Convert.IsDBNull(row["NumHallazgos"]) ? 0 : row["NumHallazgos"]),
                        EstatusInforme = Convert.ToInt32(Convert.IsDBNull(row["EstatusInforme"]) ? 0 : row["EstatusInforme"]),
                        PeriodoAuditar = Convert.IsDBNull(row["PeriodoAuditar"]) ? "" : row["PeriodoAuditar"].ToString(),
                        Conclusion = Convert.ToInt32(Convert.IsDBNull(row["Conclusion"]) ? 0 : row["Conclusion"]),
                        Vencimiento = Convert.IsDBNull(row["Vencimiento"]) ? "" : Convert.ToDateTime(row["Vencimiento"]).ToString("dd/MM/yyyy hh:mm:ss tt"),
                        Estatus = Convert.ToInt32(Convert.IsDBNull(row["Estatus"]) ? 0 : row["Estatus"]),
                        Usuario = Convert.IsDBNull(row["Usuario"]) ? "" : row["Usuario"].ToString(),
                        NumEntregados = Convert.ToInt32(Convert.IsDBNull(row["NumEntregados"]) ? 0 : row["NumEntregados"]),
                        NumPendientes = Convert.ToInt32(Convert.IsDBNull(row["NumPendientes"]) ? 0 : row["NumPendientes"]),
                        UsuarioMod = Convert.IsDBNull(row["UsuarioMod"]) ? "" : row["UsuarioMod"].ToString(),
                        FechaMod = Convert.IsDBNull(row["FechaMod"]) ? "" : Convert.ToDateTime(row["FechaMod"]).ToString("dd/MM/yyyy hh:mm:ss tt"),
                        Terminada = Convert.ToByte(Convert.IsDBNull(row["Terminada"]) ? 0 : row["Terminada"]),
                        ClienteDesc = Convert.IsDBNull(row["ClienteDesc"]) ? "" : row["ClienteDesc"].ToString(),
                        TipoClienteDesc = Convert.IsDBNull(row["TipoClienteDesc"]) ? "" : row["TipoClienteDesc"].ToString(),
                        AuditorDesc = Convert.IsDBNull(row["AuditorDesc"]) ? "" : row["AuditorDesc"].ToString(),
                        TipoAuditoriaDesc = Convert.IsDBNull(row["TipoAuditoriaDesc"]) ? "" : row["TipoAuditoriaDesc"].ToString(),
                        EstatusDesc = Convert.IsDBNull(row["EstatusDesc"]) ? "" : row["EstatusDesc"].ToString(),
                        EstatusInformeDesc = Convert.IsDBNull(row["EstatusInformeDesc"]) ? "" : row["EstatusInformeDesc"].ToString(),
                        ConclusionDesc = Convert.IsDBNull(row["ConclusionDesc"]) ? "" : row["ConclusionDesc"].ToString()
                    });
                }
                Datos.Dispose();
            }
            return Lista;
        }
    }
}
