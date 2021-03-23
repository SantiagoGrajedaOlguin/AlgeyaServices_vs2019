
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
    public class DetalleEntidadRepositorio
    {
        SysCatalogosDetalle Tabla;
        public DetalleEntidadRepositorio()
        {
            Tabla = new SysCatalogosDetalle();
        }

        public int Guardar(DetalleEntidad entidad)
        {
            Tabla.Id = entidad.Id;
            Tabla.IdOrigen = entidad.IdOrigen;
            Tabla.IdCatalogo = 0;
            Tabla.Tipo = entidad.Tipo;
            Tabla.Posicion = entidad.Posicion;
            Tabla.IdArticulo = 0;
            Tabla.IdPersona = 0;
            Tabla.IdDato = 0;
            Tabla.Cantidad = 0;
            Tabla.Valor = 0;
            Tabla.Descripcion = entidad.Descripcion;
            Tabla.Texto = entidad.Texto;
            Tabla.Notas = entidad.Notas;
            Tabla.EsRequerido = entidad.EsRequerido;
            Tabla.Id = Tabla.Guardar();
            return Tabla.Id;
        }
        public bool Remove(int Id)
        {
            Tabla.BorrarPorId(Id,false);
            return true;
        }

        public List<DetalleEntidad> GetAll(int IdOrigen, short Tipo)
        {
            List<DetalleEntidad> Lista = new List<DetalleEntidad>();
            DataTable Datos = Tabla.ListarTabla(IdOrigen, Tipo, 0, 0, "");
            if (Datos != null)
            {
                foreach (DataRow row in Datos.Rows)
                {
                    Lista.Add(new DetalleEntidad()
                    {
                        Id = Convert.ToInt32(Convert.IsDBNull(row["Id"]) ? 0 : row["Id"]),
                        IdOrigen = Convert.ToInt32(Convert.IsDBNull(row["IdOrigen"]) ? 0 : row["IdOrigen"]),
                        Tipo = Convert.ToInt16(Convert.IsDBNull(row["Tipo"]) ? 0 : row["Tipo"]),
                        Posicion = Convert.ToInt16(Convert.IsDBNull(row["Posicion"]) ? 0 : row["Posicion"]),
                        Descripcion = Convert.IsDBNull(row["Descripcion"]) ? "" : row["Descripcion"].ToString(),
                        Texto = Convert.IsDBNull(row["Texto"]) ? "" : row["Texto"].ToString(),
                        Notas = Convert.IsDBNull(row["Notas"]) ? "" : row["Notas"].ToString(),
                        EsRequerido = Convert.ToByte(Convert.IsDBNull(row["EsRequerido"]) ? 0 : row["EsRequerido"])
                    });
                }
                Datos.Dispose();
            }
            return Lista;
        }

    }
}
