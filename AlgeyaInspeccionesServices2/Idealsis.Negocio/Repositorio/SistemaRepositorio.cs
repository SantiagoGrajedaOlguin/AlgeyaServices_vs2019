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
    public class SistemaRepositorio
    {
        CatSistemas Tabla;

        public SistemaRepositorio()
        {
            Tabla = new CatSistemas();
        }

        public Sistema BuscarPorId(int Id)
        {
            if (Tabla.BuscarPorId(Id))
            {
                return new Sistema()
                {
                    Id = Tabla.Id,
                    Codigo = Tabla.Codigo,
                    Descripcion = Tabla.Descripcion,
                    Desarrollador = Tabla.Desarrollador,
                    Lenguaje = Tabla.Lenguaje,
                    Plataforma = Tabla.Plataforma ,
                    EnRed = Tabla.EnRed,
                    EnSucursales = Tabla.EnSucursales
                };
            }
            else
            {
                return null;
            }
        }
        public int Guardar(Sistema entidad)
        {
            Tabla.Id = entidad.Id;
            Tabla.Codigo = entidad.Codigo;
            Tabla.Descripcion = entidad.Descripcion;
            Tabla.Desarrollador = entidad.Desarrollador;
            Tabla.Lenguaje = entidad.Lenguaje;
            Tabla.Plataforma = entidad.Plataforma;
            Tabla.EnRed = entidad.EnRed;
            Tabla.EnSucursales = entidad.EnSucursales;
            return Tabla.Guardar();
        }

        public bool Remove(int Id)
        {
            return Tabla.BorrarPorId(Id);
        }

        public List<Sistema> GetAll()
        {
            List<Sistema> Lista = new List<Sistema>();
            DataTable Datos = Tabla.ListarTabla("");
            if (Datos != null)
            {
                foreach (DataRow row in Datos.Rows)
                {
                    Lista.Add(new Sistema()
                    {
                        Id = Convert.ToInt32(Convert.IsDBNull(row["Id"]) ? 0 : row["Id"]),
                        Codigo = Convert.ToInt32(Convert.IsDBNull(row["Codigo"]) ? 0 : row["Codigo"]),
                        Descripcion = Convert.IsDBNull(row["Descripcion"]) ? "" : row["Descripcion"].ToString(),
                        Desarrollador = Convert.IsDBNull(row["Desarrollador"]) ? "" : row["Desarrollador"].ToString(),
                        Lenguaje = Convert.IsDBNull(row["Lenguaje"]) ? "" : row["Lenguaje"].ToString(),
                        Plataforma = Convert.IsDBNull(row["Plataforma"]) ? "" : row["Plataforma"].ToString(),
                        EnRed = Convert.ToByte(Convert.IsDBNull(row["EnRed"]) ? 0 : row["EnRed"]),
                        EnSucursales = Convert.ToByte(Convert.IsDBNull(row["EnSucursales"]) ? 0 : row["EnSucursales"])
                    });
                }
                Datos.Dispose();
            }
            return Lista;
        }

    }
}
