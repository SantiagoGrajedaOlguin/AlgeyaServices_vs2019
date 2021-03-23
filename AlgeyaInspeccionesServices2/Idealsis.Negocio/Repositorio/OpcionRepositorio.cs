
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
    public class OpcionRepositorio
    {
        SegOpciones Tabla;
        public OpcionRepositorio()
        {
            Tabla = new SegOpciones();
        }

        public void Guardar(Opcion entidad)
        {
            Tabla.Tipo = entidad.Tipo;
            Tabla.Codigo = entidad.Codigo;
            Tabla.Descripcion = entidad.Descripcion;
            Tabla.Padre = entidad.Padre;
            Tabla.Origen = entidad.Origen;
            Tabla.Catalogo = entidad.Catalogo;
            Tabla.EsPermiso = entidad.EsPermiso;
            Tabla.Orden = entidad.Orden;
            Tabla.Guardar();
        }
        public bool Remove(string Tipo, string Codigo)
        {
            Tabla.BorrarPorId(Tipo, Codigo);
            return true;
        }

        public List<Opcion> GetAll(string Tipo)
        {
            List<Opcion> Lista = new List<Opcion>();
            DataTable Datos = Tabla.ListarTabla(0, Tipo);
            if (Datos != null)
            {
                foreach (DataRow row in Datos.Rows)
                {
                    Lista.Add(new Opcion()
                    {
                        Tipo = Convert.IsDBNull(row["Tipo"]) ? "" : row["Tipo"].ToString(),
                        Codigo = Convert.IsDBNull(row["Codigo"]) ? "" : row["Codigo"].ToString(),
                        Padre = Convert.IsDBNull(row["Padre"]) ? "" : row["Padre"].ToString(),
                        Descripcion = Convert.IsDBNull(row["Descripcion"]) ? "" : row["Descripcion"].ToString(),
                        Origen = Convert.ToInt16(Convert.IsDBNull(row["Origen"]) ? 0 : row["Origen"]),
                        Catalogo = Convert.ToInt16(Convert.IsDBNull(row["Catalogo"]) ? 0 : row["Catalogo"]),
                        EsPermiso = Convert.ToByte(Convert.IsDBNull(row["EsPermiso"]) ? 0 : row["EsPermiso"]),
                        Orden = Convert.ToInt32(Convert.IsDBNull(row["Orden"]) ? 0 : row["Orden"])
                    });
                }
                Datos.Dispose();
            }
            return Lista;
        }

    }
}
