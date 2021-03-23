
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
    public class DetalleSistemaRepositorio
    {
        CatSistemasDetalle Tabla;
        public DetalleSistemaRepositorio()
        {
            Tabla = new CatSistemasDetalle();
        }

        public int Guardar(DetalleSistema entidad)
        {
            Tabla.Id = entidad.Id;
            Tabla.IdOrigen = entidad.IdOrigen;
            Tabla.Posicion = entidad.Posicion;
            Tabla.Descripcion = entidad.Descripcion;
            Tabla.Estatus = entidad.Estatus;
            return Tabla.Guardar();
        }
        public bool Remove(int Id)
        {
            Tabla.BorrarPorId(Id, false);
            return true;
        }

        public List<DetalleSistema> GetAll(int IdOrigen)
        {
            List<DetalleSistema> Lista = new List<DetalleSistema>();
            DataTable Datos = Tabla.ListarTabla(IdOrigen, "");
            if (Datos != null)
            {
                foreach (DataRow row in Datos.Rows)
                {
                    Lista.Add(new DetalleSistema()
                    {
                        Id = Convert.ToInt32(Convert.IsDBNull(row["Id"]) ? 0 : row["Id"]),
                        IdOrigen = Convert.ToInt32(Convert.IsDBNull(row["IdOrigen"]) ? 0 : row["IdOrigen"]),
                        Posicion = Convert.ToInt16(Convert.IsDBNull(row["Posicion"]) ? 0 : row["Posicion"]),
                        Descripcion = Convert.IsDBNull(row["Descripcion"]) ? "" : row["Descripcion"].ToString(),
                        Estatus = Convert.IsDBNull(row["Estatus"]) ? "" : row["Estatus"].ToString()
                    });
                }
                Datos.Dispose();
            }
            return Lista;
        }

    }
}
