
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
    public class CatalogoRepositorio
    {
        SysCatalogos Tabla;

        public CatalogoRepositorio()
        {
            Tabla = new SysCatalogos();
        }

        public Catalogo BuscarPorId(int Id)
        {
            if (Tabla.BuscarPorId(Id))
            {
                return new Catalogo()
                {
                    Id = Tabla.Id,
                    Tipo = Tabla.Catalogo,
                    IdPadre = Tabla.IdPadre,
                    Codigo = Tabla.Codigo,
                    Descripcion = Tabla.Descripcion,
                    Corta = Tabla.Corta,
                    ValorStr = Tabla.ValorStr,
                    Activo = Tabla.Activo,
                    Bandera1 = Tabla.Bandera1,
                    Bandera2 = Tabla.Bandera2
                };
            }
            else
            {
                return null;
            }
        }

        public string GetDesc(int Id)
        {
            if (Tabla.BuscarPorId(Id))
            {
                return Tabla.Descripcion;
            }
            else
            {
                return "";
            }
        }

        public int SiguienteCodigo(short Tipo)
        {
            return Tabla.SiguienteCodigo(Tipo,0,0);
        }

        public Catalogo BuscarPorCodigo(short Tipo, int Codigo)
        {
            if (Tabla.BuscarPorCodigo(Tipo,Codigo))
            {
                return new Catalogo()
                {
                    Id = Tabla.Id,
                    Tipo = Tabla.Catalogo,
                    IdPadre = Tabla.IdPadre,
                    Codigo = Tabla.Codigo,
                    Descripcion = Tabla.Descripcion,
                    Corta = Tabla.Corta,
                    ValorStr = Tabla.ValorStr,
                    Activo = Tabla.Activo,
                    Bandera1 = Tabla.Bandera1,
                    Bandera2 = Tabla.Bandera2
                };
            }
            else
            {
                return null;
            }
        }

        public int Guardar(Catalogo entidad)
        {
            Tabla.Id = entidad.Id;
            Tabla.Catalogo = entidad.Tipo;
            Tabla.IdPadre = entidad.IdPadre;
            Tabla.Codigo = entidad.Codigo;
            Tabla.Descripcion = entidad.Descripcion;
            Tabla.Corta = entidad.Corta;
            Tabla.ValorStr = entidad.ValorStr;
            Tabla.Activo = entidad.Activo;
            Tabla.Bandera1 = entidad.Bandera1;
            Tabla.Bandera2 = entidad.Bandera2;
            return Tabla.Guardar();
        }

        public bool Remove(int Id)
        {
            return Tabla.BorrarPorId(Id, false);
        }

        public List<Catalogo> GetAll(short TipoCatalogo, int IdPadre)
        {
            List<Catalogo> Lista = new List<Catalogo>();
            DataTable Datos = Tabla.ListarTabla(TipoCatalogo, IdPadre, "");
            if (Datos != null)
            {
                foreach (DataRow row in Datos.Rows)
                {
                    Lista.Add(new Catalogo()
                    {
                        Id = Convert.ToInt32(Convert.IsDBNull(row["Id"]) ? 0 : row["Id"]),
                        Tipo = Convert.ToInt16(Convert.IsDBNull(row["Catalogo"]) ? 0 : row["Catalogo"]),
                        IdPadre = Convert.ToInt32(Convert.IsDBNull(row["IdPadre"]) ? 0 : row["IdPadre"]),
                        Codigo = Convert.ToInt32(Convert.IsDBNull(row["Codigo"]) ? 0 : row["Codigo"]),
                        Descripcion = Convert.IsDBNull(row["Descripcion"]) ? "" : row["Descripcion"].ToString(),
                        Corta = Convert.IsDBNull(row["Corta"]) ? "" : row["Corta"].ToString(),
                        ValorStr = Convert.IsDBNull(row["ValorStr"]) ? "" : row["ValorStr"].ToString(),
                        Activo = Convert.ToByte(Convert.IsDBNull(row["Activo"]) ? 0 : row["Activo"]),
                        Bandera1 = Convert.ToByte(Convert.IsDBNull(row["Bandera1"]) ? 0 : row["Bandera1"]),
                        Bandera2 = Convert.ToByte(Convert.IsDBNull(row["Bandera2"]) ? 0 : row["Bandera2"])
                    });
                }
                Datos.Dispose();
            }
            return Lista;
        }

    }
}
