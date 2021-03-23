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
    public class MonedaRepositorio
    {
        CatMonedas Tabla;

        /// <summary>
        /// Constructor
        /// </summary>
        public MonedaRepositorio()
        {
            Tabla = new CatMonedas();
        }

        public int Guardar(Moneda entidad)
        {
            Tabla.Codigo = entidad.Codigo;
            Tabla.Descripcion = entidad.Descripcion;
            Tabla.Simbolo = entidad.Simbolo;
            Tabla.Texto = entidad.Texto;
            Tabla.EsNacional = entidad.EsNacional;
            Tabla.EsPred = entidad.EsPred;
            Tabla.ClaveSat = entidad.ClaveSat;
            Tabla.Guardar();
            return Tabla.Codigo;
        }

        public bool Remove(short Codigo)
        {
            return Tabla.BorrarPorId(Codigo);
        }

        public List<Moneda> GetAll()
        {
            List<Moneda> Lista = new List<Moneda>();
            DataTable Datos = Tabla.Listar("");
            if (Datos != null)
            {
                foreach (DataRow row in Datos.Rows)
                {
                    Lista.Add(new Moneda()
                    {
                        Codigo = Convert.ToInt16(Convert.IsDBNull(row["Codigo"]) ? 0 : row["Codigo"]),
                        Descripcion = Convert.IsDBNull(row["Descripcion"]) ? "" : row["Descripcion"].ToString(),
                        Simbolo = Convert.IsDBNull(row["Simbolo"]) ? "" : row["Simbolo"].ToString(),
                        Texto = Convert.IsDBNull(row["Texto"]) ? "" : row["Texto"].ToString(),
                        EsPred = Convert.ToByte(Convert.IsDBNull(row["EsPred"]) ? 0 : row["EsPred"]),
                        EsNacional = Convert.ToByte(Convert.IsDBNull(row["EsNacional"]) ? 0 : row["EsNacional"]),
                        ClaveSat = Convert.IsDBNull(row["ClaveSat"]) ? "" : row["ClaveSat"].ToString()
                    });
                }
                Datos.Dispose();
            }
            return Lista;
        }

    }
}
