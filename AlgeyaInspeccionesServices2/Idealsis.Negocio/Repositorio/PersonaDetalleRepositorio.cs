
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
    public class PersonaDetalleRepositorio
    {
        CatPersonasDetalle Tabla;
        public PersonaDetalleRepositorio()
        {
            Tabla = new CatPersonasDetalle();
        }

        public void Guardar(PersonaDetalle entidad)
        {
            Tabla.Id = entidad.Id;
            Tabla.IdOrigen = entidad.IdOrigen;
            Tabla.Tipo = entidad.Tipo;
            Tabla.Posicion = entidad.Posicion;
            Tabla.Codigo = entidad.Codigo;
            Tabla.Descripcion = entidad.Descripcion;
            Tabla.IdPersona = entidad.IdPersona;
            Tabla.IdCatalogo = entidad.IdCatalogo;
            Tabla.IdDato = entidad.IdDato;
            Tabla.Valor = entidad.Valor;
            Tabla.Notas = entidad.Notas;
            Tabla.Bandera = entidad.Bandera;
            Tabla.Guardar();
        }
        public bool Remove(int Id)
        {
            Tabla.BorrarPorId(Id, false);
            return true;
        }

        public List<PersonaDetalle> GetAll(int IdOrigen, short Tipo)
        {
            List<PersonaDetalle> Lista = new List<PersonaDetalle>();
            DataTable Datos = Tabla.ListarTabla(IdOrigen, Tipo, "");
            if (Datos != null)
            {
                foreach (DataRow row in Datos.Rows)
                {
                    Lista.Add(new PersonaDetalle()
                    {
                        Id = Convert.ToInt32(Convert.IsDBNull(row["Id"]) ? 0 : row["Id"]),
                        IdOrigen = Convert.ToInt32(Convert.IsDBNull(row["IdOrigen"]) ? 0 : row["IdOrigen"]),
                        Tipo = Convert.ToInt16(Convert.IsDBNull(row["Tipo"]) ? 0 : row["Tipo"]),
                        Posicion = Convert.ToInt16(Convert.IsDBNull(row["Posicion"]) ? 0 : row["Posicion"]),
                        Codigo = Convert.IsDBNull(row["Codigo"]) ? "" : row["Codigo"].ToString(),
                        Descripcion = Convert.IsDBNull(row["Descripcion"]) ? "" : row["Descripcion"].ToString(),
                        IdPersona = Convert.ToInt32(Convert.IsDBNull(row["IdPersona"]) ? 0 : row["IdPersona"]),
                        IdCatalogo = Convert.ToInt32(Convert.IsDBNull(row["IdCatalogo"]) ? 0 : row["IdCatalogo"]),
                        IdDato = Convert.ToInt32(Convert.IsDBNull(row["IdDato"]) ? 0 : row["IdDato"]),
                        Valor = Convert.ToSingle(Convert.IsDBNull(row["Valor"]) ? 0 : row["Valor"]),
                        Notas = Convert.IsDBNull(row["Notas"]) ? "" : row["Notas"].ToString(),
                        Bandera = Convert.ToByte(Convert.IsDBNull(row["Bandera"]) ? 0 : row["Bandera"])
                    });
                }
                Datos.Dispose();
            }
            return Lista;
        }

    }
}
