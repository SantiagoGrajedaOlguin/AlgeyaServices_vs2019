
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
    public class ExpedienteHallazgoRepositorio
    {
        ExpedientesHallazgo Tabla;
        public ExpedienteHallazgoRepositorio()
        {
            Tabla = new ExpedientesHallazgo();
        }

        public int Guardar(ExpedienteHallazgo entidad)
        {
            Tabla.Id = entidad.Id;
            Tabla.IdOrigen = entidad.IdOrigen;
            Tabla.Folio = entidad.Folio;
            Tabla.Fecha = entidad.Fecha;
            Tabla.Hora = entidad.Hora;
            Tabla.Tipo = entidad.Tipo;
            Tabla.Nivel = entidad.Nivel;
            Tabla.Persona = entidad.Persona;
            Tabla.Descripcion = entidad.Descripcion;
            Tabla.Area = entidad.Area;
            Tabla.FechaCompromiso = entidad.FechaCompromiso;
            Tabla.Estatus = entidad.Estatus;
            Tabla.Usuario = entidad.Usuario;
            Tabla.UsuarioMod = entidad.UsuarioMod;
            Tabla.FechaMod = entidad.FechaMod;
            return Tabla.Guardar();
        }
        public ExpedienteHallazgo BuscarPorId(int Id)
        {
            if (Tabla.BuscarPorId(Id))
            {
                return new ExpedienteHallazgo()
                {
                    Id = Tabla.Id,
                    IdOrigen = Tabla.IdOrigen,
                    Folio = Tabla.Folio,
                    Fecha = Tabla.Fecha,
                    Hora = Tabla.Hora,
                    Tipo = Tabla.Tipo,
                    Persona = Tabla.Persona,
                    Nivel = Tabla.Nivel,
                    Descripcion = Tabla.Descripcion,
                    Area = Tabla.Area,
                    FechaCompromiso = Tabla.FechaCompromiso,
                    Estatus = Tabla.Estatus,
                    Usuario = Tabla.Usuario,
                    UsuarioMod = Tabla.UsuarioMod,
                    FechaMod = Tabla.FechaMod,
                    TipoDesc = Tabla.TipoDesc,
                    NivelDesc = Tabla.NivelDesc,
                    AreaDesc = Tabla.AreaDesc,
                    EstatusDesc = Tabla.EstatusDesc
                };
            }
            else
            {
                return null;
            }
        }

        public bool Remove(int Id)
        {
            Tabla.BorrarPorId(Id, false);
            return true;
        }

        public List<ExpedienteHallazgo> GetAll(int IdOrigen)
        {
            List<ExpedienteHallazgo> Lista = new List<ExpedienteHallazgo>();
            DataTable Datos = Tabla.ListarTabla(IdOrigen);
            if (Datos != null)
            {
                foreach (DataRow row in Datos.Rows)
                {
                    Lista.Add(new ExpedienteHallazgo()
                    {
                        Id = Convert.ToInt32(Convert.IsDBNull(row["Id"]) ? 0 : row["Id"]),
                        IdOrigen = Convert.ToInt32(Convert.IsDBNull(row["IdOrigen"]) ? 0 : row["IdOrigen"]),
                        Folio = Convert.ToInt32(Convert.IsDBNull(row["Folio"]) ? 0 : row["Folio"]),
                        Fecha = Convert.IsDBNull(row["Fecha"]) ? "" : Convert.ToDateTime(row["Fecha"]).ToString("dd/MM/yyyy hh:mm:ss tt"),
                        Hora = Convert.IsDBNull(row["Hora"]) ? "" : Convert.ToDateTime(row["Hora"]).ToString("dd/MM/yyyy hh:mm:ss tt"),
                        Tipo = Convert.ToInt32(Convert.IsDBNull(row["Tipo"]) ? 0 : row["Tipo"]),
                        Nivel = Convert.ToInt32(Convert.IsDBNull(row["Nivel"]) ? 0 : row["Nivel"]),
                        Persona = Convert.ToInt32(Convert.IsDBNull(row["Persona"]) ? 0 : row["Persona"]),
                        Descripcion = Convert.IsDBNull(row["Descripcion"]) ? "" : row["Descripcion"].ToString(),
                        Area = Convert.ToInt32(Convert.IsDBNull(row["Area"]) ? 0 : row["Area"]),
                        FechaCompromiso = Convert.IsDBNull(row["FechaCompromiso"]) ? "" : Convert.ToDateTime(row["FechaCompromiso"]).ToString("dd/MM/yyyy hh:mm:ss tt"),
                        Estatus = Convert.ToInt32(Convert.IsDBNull(row["Estatus"]) ? 0 : row["Estatus"]),
                        Usuario = Convert.IsDBNull(row["Usuario"]) ? "" : row["Usuario"].ToString(),
                        FechaMod = Convert.IsDBNull(row["FechaMod"]) ? "" : Convert.ToDateTime(row["FechaMod"]).ToString("dd/MM/yyyy hh:mm:ss tt"),
                        UsuarioMod = Convert.IsDBNull(row["UsuarioMod"]) ? "" : row["UsuarioMod"].ToString(),
                        TipoDesc = Convert.IsDBNull(row["TipoDesc"]) ? "" : row["TipoDesc"].ToString(),
                        NivelDesc = Convert.IsDBNull(row["NivelDesc"]) ? "" : row["NivelDesc"].ToString(),
                        AreaDesc = Convert.IsDBNull(row["AreaDesc"]) ? "" : row["AreaDesc"].ToString(),
                        EstatusDesc = Convert.IsDBNull(row["EstatusDesc"]) ? "" : row["EstatusDesc"].ToString()
                    });
                }
                Datos.Dispose();
            }
            return Lista;
        }
        public List<ExpedienteHallazgo> GetAll(int IdOrigen, int IdHallazgo)
        {
            List<ExpedienteHallazgo> Lista = new List<ExpedienteHallazgo>();
            DataTable Datos;
            if (IdHallazgo>0)
                Datos = Tabla.ListarTablaPorId(IdHallazgo);
            else
                Datos = Tabla.ListarTabla(IdOrigen);

            if (Datos != null)
            {
                foreach (DataRow row in Datos.Rows)
                {
                    Lista.Add(new ExpedienteHallazgo()
                    {
                        Id = Convert.ToInt32(Convert.IsDBNull(row["Id"]) ? 0 : row["Id"]),
                        IdOrigen = Convert.ToInt32(Convert.IsDBNull(row["IdOrigen"]) ? 0 : row["IdOrigen"]),
                        Folio = Convert.ToInt32(Convert.IsDBNull(row["Folio"]) ? 0 : row["Folio"]),
                        Fecha = Convert.IsDBNull(row["Fecha"]) ? "" : Convert.ToDateTime(row["Fecha"]).ToString("dd/MM/yyyy hh:mm:ss tt"),
                        Hora = Convert.IsDBNull(row["Hora"]) ? "" : Convert.ToDateTime(row["Hora"]).ToString("dd/MM/yyyy hh:mm:ss tt"),
                        Tipo = Convert.ToInt32(Convert.IsDBNull(row["Tipo"]) ? 0 : row["Tipo"]),
                        Nivel = Convert.ToInt32(Convert.IsDBNull(row["Nivel"]) ? 0 : row["Nivel"]),
                        Persona = Convert.ToInt32(Convert.IsDBNull(row["Persona"]) ? 0 : row["Persona"]),
                        Descripcion = Convert.IsDBNull(row["Descripcion"]) ? "" : row["Descripcion"].ToString(),
                        Area = Convert.ToInt32(Convert.IsDBNull(row["Area"]) ? 0 : row["Area"]),
                        FechaCompromiso = Convert.IsDBNull(row["FechaCompromiso"]) ? "" : Convert.ToDateTime(row["FechaCompromiso"]).ToString("dd/MM/yyyy hh:mm:ss tt"),
                        Estatus = Convert.ToInt32(Convert.IsDBNull(row["Estatus"]) ? 0 : row["Estatus"]),
                        Usuario = Convert.IsDBNull(row["Usuario"]) ? "" : row["Usuario"].ToString(),
                        FechaMod = Convert.IsDBNull(row["FechaMod"]) ? "" : Convert.ToDateTime(row["FechaMod"]).ToString("dd/MM/yyyy hh:mm:ss tt"),
                        UsuarioMod = Convert.IsDBNull(row["UsuarioMod"]) ? "" : row["UsuarioMod"].ToString(),
                        TipoDesc = Convert.IsDBNull(row["TipoDesc"]) ? "" : row["TipoDesc"].ToString(),
                        NivelDesc = Convert.IsDBNull(row["NivelDesc"]) ? "" : row["NivelDesc"].ToString(),
                        AreaDesc = Convert.IsDBNull(row["AreaDesc"]) ? "" : row["AreaDesc"].ToString(),
                        EstatusDesc = Convert.IsDBNull(row["EstatusDesc"]) ? "" : row["EstatusDesc"].ToString()
                    });
                }
                Datos.Dispose();
            }
            return Lista;
        }
    }
}
