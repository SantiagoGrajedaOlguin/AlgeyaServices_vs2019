
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
    public class ExpedienteDetalleRepositorio
    {
        ExpedientesDetalle Tabla;
        public ExpedienteDetalleRepositorio()
        {
            Tabla = new ExpedientesDetalle();
        }

        public int Guardar(ExpedienteDetalle entidad)
        {
            Tabla.Id = entidad.Id;
            Tabla.IdOrigen = entidad.IdOrigen;
            Tabla.Tipo = entidad.Tipo;
            Tabla.Posicion = entidad.Posicion;
            Tabla.Codigo = entidad.Codigo;
            Tabla.EsRequerido = entidad.EsRequerido;
            Tabla.Dato = entidad.Dato;
            Tabla.Persona = entidad.Persona;
            Tabla.Plantilla = entidad.Plantilla;
            Tabla.Valor = entidad.Valor;
            Tabla.Comentarios = entidad.Comentarios;
            Tabla.Notas = entidad.Notas;
            Tabla.Estatus = entidad.Estatus;
            Tabla.FechaEstatus = entidad.FechaEstatus;
            Tabla.IdArchivo = entidad.IdArchivo;
            Tabla.Fecha = entidad.Fecha;
            Tabla.Usuario = entidad.Usuario;
            Tabla.UsuarioMod = entidad.UsuarioMod;
            Tabla.FechaMod = entidad.FechaMod;
            Tabla.NumReq = entidad.NumReq;
            Tabla.NumAdd = entidad.NumAdd;
            return Tabla.Guardar();
        }
        public bool Remove(int Id)
        {
            Tabla.BorrarPorId(Id, false);
            return true;
        }

        public string DameMes(short Mes)
        {
            switch(Mes)
            {
                case 1: return "Enero";
                case 2: return "Febrero";
                case 3: return "Marzo";
                case 4: return "Abril";
                case 5: return "Mayo";
                case 6: return "Junio";
                case 7: return "Julio";
                case 8: return "Agosto";
                case 9: return "Septiembre";
                case 10: return "Octubre";
                case 11: return "Noviembre";
                default: return "Diciembre";
            }
        }

        public bool RemovePadre(int IdPadre, bool BorrarPadre)
        {
            Tabla.BorrarPorPadre(IdPadre, BorrarPadre);
            return true;
        }

        public List<ExpedienteDetalle> GetAll(int IdOrigen, byte Tipo)
        {
            List<ExpedienteDetalle> Lista = new List<ExpedienteDetalle>();
            DataTable Datos = Tabla.ListarTabla(IdOrigen, Tipo, 0, "");
            if (Datos != null)
            {
                foreach (DataRow row in Datos.Rows)
                {
                    Lista.Add(new ExpedienteDetalle()
                    {
                        Id = Convert.ToInt32(Convert.IsDBNull(row["Id"]) ? 0 : row["Id"]),
                        IdOrigen = Convert.ToInt32(Convert.IsDBNull(row["IdOrigen"]) ? 0 : row["IdOrigen"]),
                        Tipo = Convert.ToByte(Convert.IsDBNull(row["Tipo"]) ? 0 : row["Tipo"]),
                        Posicion = Convert.ToInt32(Convert.IsDBNull(row["Posicion"]) ? 0 : row["Posicion"]),
                        Codigo = Convert.ToInt32(Convert.IsDBNull(row["Codigo"]) ? 0 : row["Codigo"]),
                        EsRequerido = Convert.ToByte(Convert.IsDBNull(row["EsRequerido"]) ? 0 : row["EsRequerido"]),
                        Dato = Convert.ToInt32(Convert.IsDBNull(row["Dato"]) ? 0 : row["Dato"]),
                        Persona = Convert.ToInt32(Convert.IsDBNull(row["Persona"]) ? 0 : row["Persona"]),
                        Plantilla = Convert.ToInt32(Convert.IsDBNull(row["Plantilla"]) ? 0 : row["Plantilla"]),
                        Valor = Convert.ToDecimal(Convert.IsDBNull(row["Valor"]) ? 0 : row["Valor"]),
                        Comentarios = Convert.IsDBNull(row["Comentarios"]) ? "" : row["Comentarios"].ToString(),
                        Notas = Convert.IsDBNull(row["Notas"]) ? "" : row["Notas"].ToString(),
                        Estatus = Convert.ToInt32(Convert.IsDBNull(row["Estatus"]) ? 0 : row["Estatus"]),
                        FechaEstatus = Convert.IsDBNull(row["FechaEstatus"]) ? "" : Convert.ToDateTime(row["FechaEstatus"]).ToString("dd/MM/yyyy hh:mm:ss tt"),
                        IdArchivo = Convert.ToInt32(Convert.IsDBNull(row["IdArchivo"]) ? 0 : row["IdArchivo"]),
                        Fecha = Convert.IsDBNull(row["Fecha"]) ? "" : Convert.ToDateTime(row["Fecha"]).ToString("dd/MM/yyyy hh:mm:ss tt"),
                        Usuario = Convert.IsDBNull(row["Usuario"]) ? "" : row["Usuario"].ToString(),
                        FechaMod = Convert.IsDBNull(row["FechaMod"]) ? "" : Convert.ToDateTime(row["FechaMod"]).ToString("dd/MM/yyyy hh:mm:ss tt"),
                        UsuarioMod = Convert.IsDBNull(row["UsuarioMod"]) ? "" : row["UsuarioMod"].ToString(),
                        NumReq = Convert.ToInt16(Convert.IsDBNull(row["NumReq"]) ? 0 : row["NumReq"]),
                        NumAdd = Convert.ToInt16(Convert.IsDBNull(row["NumAdd"]) ? 0 : row["NumAdd"])
                    });
                }
                Datos.Dispose();
            }
            return Lista;
        }

        public List<ExpedienteDetalle> GetAll(int IdOrigen, byte Tipo, int Posicion)
        {
            List<ExpedienteDetalle> Lista = new List<ExpedienteDetalle>();
            DataTable Datos = Tabla.ListarTabla(IdOrigen, Tipo, Posicion, "");
            if (Datos != null)
            {
                foreach (DataRow row in Datos.Rows)
                {
                    Lista.Add(new ExpedienteDetalle()
                    {
                        Id = Convert.ToInt32(Convert.IsDBNull(row["Id"]) ? 0 : row["Id"]),
                        IdOrigen = Convert.ToInt32(Convert.IsDBNull(row["IdOrigen"]) ? 0 : row["IdOrigen"]),
                        Tipo = Convert.ToByte(Convert.IsDBNull(row["Tipo"]) ? 0 : row["Tipo"]),
                        Posicion = Convert.ToInt32(Convert.IsDBNull(row["Posicion"]) ? 0 : row["Posicion"]),
                        Codigo = Convert.ToInt32(Convert.IsDBNull(row["Codigo"]) ? 0 : row["Codigo"]),
                        EsRequerido = Convert.ToByte(Convert.IsDBNull(row["EsRequerido"]) ? 0 : row["EsRequerido"]),
                        Dato = Convert.ToInt32(Convert.IsDBNull(row["Dato"]) ? 0 : row["Dato"]),
                        Persona = Convert.ToInt32(Convert.IsDBNull(row["Persona"]) ? 0 : row["Persona"]),
                        Plantilla = Convert.ToInt32(Convert.IsDBNull(row["Plantilla"]) ? 0 : row["Plantilla"]),
                        Valor = Convert.ToDecimal(Convert.IsDBNull(row["Valor"]) ? 0 : row["Valor"]),
                        Comentarios = Convert.IsDBNull(row["Comentarios"]) ? "" : row["Comentarios"].ToString(),
                        Notas = Convert.IsDBNull(row["Notas"]) ? "" : row["Notas"].ToString(),
                        Estatus = Convert.ToInt32(Convert.IsDBNull(row["Estatus"]) ? 0 : row["Estatus"]),
                        FechaEstatus = Convert.IsDBNull(row["FechaEstatus"]) ? "" : Convert.ToDateTime(row["FechaEstatus"]).ToString("dd/MM/yyyy hh:mm:ss tt"),
                        IdArchivo = Convert.ToInt32(Convert.IsDBNull(row["IdArchivo"]) ? 0 : row["IdArchivo"]),
                        Fecha = Convert.IsDBNull(row["Fecha"]) ? "" : Convert.ToDateTime(row["Fecha"]).ToString("dd/MM/yyyy hh:mm:ss tt"),
                        Usuario = Convert.IsDBNull(row["Usuario"]) ? "" : row["Usuario"].ToString(),
                        FechaMod = Convert.IsDBNull(row["FechaMod"]) ? "" : Convert.ToDateTime(row["FechaMod"]).ToString("dd/MM/yyyy hh:mm:ss tt"),
                        UsuarioMod = Convert.IsDBNull(row["UsuarioMod"]) ? "" : row["UsuarioMod"].ToString(),
                        NumReq = Convert.ToInt16(Convert.IsDBNull(row["NumReq"]) ? 0 : row["NumReq"]),
                        NumAdd = Convert.ToInt16(Convert.IsDBNull(row["NumAdd"]) ? 0 : row["NumAdd"])
                    });
                }
                Datos.Dispose();
            }
            return Lista;
        }

        public List<ExpedienteDetalle> GetAllDato(int IdOrigen, byte Tipo, int Dato)
        {
            List<ExpedienteDetalle> Lista = new List<ExpedienteDetalle>();
            DataTable Datos = Tabla.ListarTablaDato(IdOrigen, Tipo, Dato, "");
            if (Datos != null)
            {
                foreach (DataRow row in Datos.Rows)
                {
                    Lista.Add(new ExpedienteDetalle()
                    {
                        Id = Convert.ToInt32(Convert.IsDBNull(row["Id"]) ? 0 : row["Id"]),
                        IdOrigen = Convert.ToInt32(Convert.IsDBNull(row["IdOrigen"]) ? 0 : row["IdOrigen"]),
                        Tipo = Convert.ToByte(Convert.IsDBNull(row["Tipo"]) ? 0 : row["Tipo"]),
                        Posicion = Convert.ToInt32(Convert.IsDBNull(row["Posicion"]) ? 0 : row["Posicion"]),
                        Codigo = Convert.ToInt32(Convert.IsDBNull(row["Codigo"]) ? 0 : row["Codigo"]),
                        EsRequerido = Convert.ToByte(Convert.IsDBNull(row["EsRequerido"]) ? 0 : row["EsRequerido"]),
                        Dato = Convert.ToInt32(Convert.IsDBNull(row["Dato"]) ? 0 : row["Dato"]),
                        Persona = Convert.ToInt32(Convert.IsDBNull(row["Persona"]) ? 0 : row["Persona"]),
                        Plantilla = Convert.ToInt32(Convert.IsDBNull(row["Plantilla"]) ? 0 : row["Plantilla"]),
                        Valor = Convert.ToDecimal(Convert.IsDBNull(row["Valor"]) ? 0 : row["Valor"]),
                        Comentarios = Convert.IsDBNull(row["Comentarios"]) ? "" : row["Comentarios"].ToString(),
                        Notas = Convert.IsDBNull(row["Notas"]) ? "" : row["Notas"].ToString(),
                        Estatus = Convert.ToInt32(Convert.IsDBNull(row["Estatus"]) ? 0 : row["Estatus"]),
                        FechaEstatus = Convert.IsDBNull(row["FechaEstatus"]) ? "" : Convert.ToDateTime(row["FechaEstatus"]).ToString("dd/MM/yyyy hh:mm:ss tt"),
                        IdArchivo = Convert.ToInt32(Convert.IsDBNull(row["IdArchivo"]) ? 0 : row["IdArchivo"]),
                        Fecha = Convert.IsDBNull(row["Fecha"]) ? "" : Convert.ToDateTime(row["Fecha"]).ToString("dd/MM/yyyy hh:mm:ss tt"),
                        Usuario = Convert.IsDBNull(row["Usuario"]) ? "" : row["Usuario"].ToString(),
                        FechaMod = Convert.IsDBNull(row["FechaMod"]) ? "" : Convert.ToDateTime(row["FechaMod"]).ToString("dd/MM/yyyy hh:mm:ss tt"),
                        UsuarioMod = Convert.IsDBNull(row["UsuarioMod"]) ? "" : row["UsuarioMod"].ToString(),
                        NumReq = Convert.ToInt16(Convert.IsDBNull(row["NumReq"]) ? 0 : row["NumReq"]),
                        NumAdd = Convert.ToInt16(Convert.IsDBNull(row["NumAdd"]) ? 0 : row["NumAdd"])
                    });
                }
                Datos.Dispose();
            }
            return Lista;
        }

        //IdAuditoria, IdTipoPersona

        public List<ExpedienteDetalle> GetDetallePersona(int IdAuditoria, int IdTipoPersona)
        {
            List<ExpedienteDetalle> Lista = new List<ExpedienteDetalle>();
            DataTable Datos = Tabla.ListarDetallePersona(IdAuditoria, IdTipoPersona);
            if (Datos != null)
            {
                foreach (DataRow row in Datos.Rows)
                {
                    Lista.Add(new ExpedienteDetalle()
                    {
                        Id = Convert.ToInt32(Convert.IsDBNull(row["Id"]) ? 0 : row["Id"]),
                        IdOrigen = Convert.ToInt32(Convert.IsDBNull(row["IdOrigen"]) ? 0 : row["IdOrigen"]),
                        Tipo = Convert.ToByte(Convert.IsDBNull(row["Tipo"]) ? 0 : row["Tipo"]),
                        Posicion = Convert.ToInt32(Convert.IsDBNull(row["Posicion"]) ? 0 : row["Posicion"]),
                        Codigo = Convert.ToInt32(Convert.IsDBNull(row["Codigo"]) ? 0 : row["Codigo"]),
                        EsRequerido = Convert.ToByte(Convert.IsDBNull(row["EsRequerido"]) ? 0 : row["EsRequerido"]),
                        Dato = Convert.ToInt32(Convert.IsDBNull(row["Dato"]) ? 0 : row["Dato"]),
                        Persona = Convert.ToInt32(Convert.IsDBNull(row["Persona"]) ? 0 : row["Persona"]),
                        Plantilla = Convert.ToInt32(Convert.IsDBNull(row["Plantilla"]) ? 0 : row["Plantilla"]),
                        Valor = Convert.ToDecimal(Convert.IsDBNull(row["Valor"]) ? 0 : row["Valor"]),
                        Comentarios = Convert.IsDBNull(row["Comentarios"]) ? "" : row["Comentarios"].ToString(),
                        Notas = Convert.IsDBNull(row["Notas"]) ? "" : row["Notas"].ToString(),
                        Estatus = Convert.ToInt32(Convert.IsDBNull(row["Estatus"]) ? 0 : row["Estatus"]),
                        FechaEstatus = Convert.IsDBNull(row["FechaEstatus"]) ? "" : Convert.ToDateTime(row["FechaEstatus"]).ToString("dd/MM/yyyy hh:mm:ss tt"),
                        IdArchivo = Convert.ToInt32(Convert.IsDBNull(row["IdArchivo"]) ? 0 : row["IdArchivo"]),
                        Fecha = Convert.IsDBNull(row["Fecha"]) ? "" : Convert.ToDateTime(row["Fecha"]).ToString("dd/MM/yyyy hh:mm:ss tt"),
                        Usuario = Convert.IsDBNull(row["Usuario"]) ? "" : row["Usuario"].ToString(),
                        FechaMod = Convert.IsDBNull(row["FechaMod"]) ? "" : Convert.ToDateTime(row["FechaMod"]).ToString("dd/MM/yyyy hh:mm:ss tt"),
                        UsuarioMod = Convert.IsDBNull(row["UsuarioMod"]) ? "" : row["UsuarioMod"].ToString(),
                        NumReq = Convert.ToInt16(Convert.IsDBNull(row["NumReq"]) ? 0 : row["NumReq"]),
                        NumAdd = Convert.ToInt16(Convert.IsDBNull(row["NumAdd"]) ? 0 : row["NumAdd"])
                    });
                }
                Datos.Dispose();
            }
            return Lista;
        }
    }
}
