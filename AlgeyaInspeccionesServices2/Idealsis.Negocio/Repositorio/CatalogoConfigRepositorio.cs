
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
    public class CatalogoConfigRepositorio
    {
        SysCatalogosConfig Tabla;
        public CatalogoConfigRepositorio()
        {
            Tabla = new SysCatalogosConfig();
        }

        public CatalogoConfig Buscar(short Codigo)
        {
            if (Tabla.BuscarPorId(Codigo))
            {
                return new CatalogoConfig()
                {
                    Codigo = Tabla.Codigo,
                    Descripcion = Tabla.Descripcion,
                    Etiqueta = Tabla.Etiqueta,
                    ConCodigo = Tabla.ConCodigo,
                    ConCorta = Tabla.ConCorta,
                    ConEstatus = Tabla.ConEstatus,
                    CatHijo = Tabla.CatHijo,
                    CatPadre = Tabla.CatPadre,
                    Bandera = Tabla.Bandera
                };
            }
            else
            {
                return null;
            }
        }
        public void Guardar(CatalogoConfig entidad)
        {
            Tabla.Codigo = entidad.Codigo;
            Tabla.Descripcion = entidad.Descripcion;
            Tabla.Etiqueta = entidad.Etiqueta;
            Tabla.ConCodigo = entidad.ConCodigo;
            Tabla.ConCorta = entidad.ConCorta;
            Tabla.ConEstatus = entidad.ConEstatus;
            Tabla.CatPadre = entidad.CatPadre;
            Tabla.CatHijo = entidad.CatHijo;
            Tabla.Bandera = entidad.Bandera;
            Tabla.Guardar();
        }
        public bool Remove(short Codigo)
        {
            Tabla.BorrarPorId(Codigo,false);
            return true;
        }

        public List<CatalogoConfig> GetAll()
        {
            List<CatalogoConfig> Lista = new List<CatalogoConfig>();
            DataTable Datos = Tabla.Listar("");
            if (Datos != null)
            {
                foreach (DataRow row in Datos.Rows)
                {
                    Lista.Add(new CatalogoConfig()
                    {
                        Codigo = Convert.ToInt16(Convert.IsDBNull(row["Codigo"]) ? 0 : row["Codigo"]),
                        Descripcion = Convert.IsDBNull(row["Descripcion"]) ? "" : row["Descripcion"].ToString(),
                        Etiqueta = Convert.IsDBNull(row["Etiqueta"]) ? "" : row["Etiqueta"].ToString(),
                        ConCodigo = Convert.ToByte(Convert.IsDBNull(row["ConCodigo"]) ? 0 : row["ConCodigo"]),
                        ConCorta = Convert.ToByte(Convert.IsDBNull(row["ConCorta"]) ? 0 : row["ConCorta"]),
                        ConEstatus = Convert.ToByte(Convert.IsDBNull(row["ConEstatus"]) ? 0 : row["ConEstatus"]),
                        CatPadre = Convert.ToInt16(Convert.IsDBNull(row["CatPadre"]) ? 0 : row["CatPadre"]),
                        CatHijo = Convert.ToInt16(Convert.IsDBNull(row["CatHijo"]) ? 0 : row["CatHijo"]),
                        Bandera = Convert.ToByte(Convert.IsDBNull(row["Bandera"]) ? 0 : row["Bandera"])
                    });
                }
                Datos.Dispose();
            }
            return Lista;
        }
    }
}
