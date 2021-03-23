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
    public class DatoOpcionRepositorio
    {
        SysDatosOpciones Tabla;

        public DatoOpcionRepositorio()
        {
            Tabla = new SysDatosOpciones();
        }

        public void Guardar(DatoOpcion entidad)
        {
            Tabla.Dato = entidad.Dato;
            Tabla.Posicion = entidad.Posicion;
            Tabla.Descripcion = entidad.Descripcion;
            Tabla.Guardar();
        }

        public bool Remove(int Dato, short Posicion)
        {
            return Tabla.BorrarPorId(Dato, Posicion);
        }

        public List<DatoOpcion> GetAll(int Dato)
        {
            List<DatoOpcion> Lista = new List<DatoOpcion>();
            DataTable Datos = Tabla.Listar(Dato);
            if (Datos != null)
            {
                foreach (DataRow row in Datos.Rows)
                {
                    Lista.Add(new DatoOpcion()
                    {
                        Dato = Convert.ToInt32(Convert.IsDBNull(row["Dato"]) ? 0 : row["Dato"]),
                        Posicion = Convert.ToInt16(Convert.IsDBNull(row["Posicion"]) ? 0 : row["Posicion"]),
                        Descripcion = Convert.IsDBNull(row["Descripcion"]) ? "" : row["Descripcion"].ToString()
                    });
                }
                Datos.Dispose();
            }
            return Lista;
        }


    }
}
