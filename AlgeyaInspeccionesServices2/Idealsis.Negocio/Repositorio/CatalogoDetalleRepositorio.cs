
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
    public class CatalogoDetalleRepositorio
    {
        SysCatalogosDetalle Tabla;
        public CatalogoDetalleRepositorio()
        {
            Tabla = new SysCatalogosDetalle();
        }

        public int Guardar(CatalogoDetalle entidad)
        {
            Tabla.Id = entidad.Id;
            Tabla.IdOrigen = entidad.IdOrigen;
            Tabla.Tipo = entidad.Tipo;
            Tabla.Posicion = entidad.Posicion;
            Tabla.IdArticulo = entidad.IdArticulo;
            Tabla.IdPersona = entidad.IdPersona;
            Tabla.IdCatalogo = entidad.IdCatalogo;
            Tabla.IdDato = entidad.IdDato;
            Tabla.Cantidad = entidad.Cantidad;
            Tabla.Valor = entidad.Valor;
            Tabla.Descripcion = entidad.Descripcion;
            Tabla.Texto = entidad.Texto ;
            Tabla.Notas = entidad.Notas ;
            Tabla.EsRequerido = entidad.EsRequerido;
            Tabla.Id = Tabla.Guardar();
            return Tabla.Id;
        }
        public bool Remove(int Id)
        {
            Tabla.BorrarPorId(Id, false);
            return true;
        }

        public string GetDesc(int IdOrigen, short Tipo)
        {
            return Tabla.GetDesc(IdOrigen, Tipo);
        }

        public List<CatalogoDetalle> GetAll(int IdOrigen, short Tipo, int IdCatalogo, int IdDato)
        {
            List<CatalogoDetalle> Lista = new List<CatalogoDetalle>();
            DataTable Datos = Tabla.ListarTabla(IdOrigen, Tipo, IdCatalogo, IdDato, "");
            if (Datos != null)
            {
                foreach (DataRow row in Datos.Rows)
                {
                    Lista.Add(new CatalogoDetalle()
                    {
                        Id = Convert.ToInt32(Convert.IsDBNull(row["Id"]) ? 0 : row["Id"]),
                        IdOrigen = Convert.ToInt32(Convert.IsDBNull(row["IdOrigen"]) ? 0 : row["IdOrigen"]),
                        Tipo = Convert.ToInt16(Convert.IsDBNull(row["Tipo"]) ? 0 : row["Tipo"]),
                        Posicion = Convert.ToInt16(Convert.IsDBNull(row["Posicion"]) ? 0 : row["Posicion"]),
                        IdCatalogo = Convert.ToInt32(Convert.IsDBNull(row["IdCatalogo"]) ? 0 : row["IdCatalogo"]),
                        IdDato = Convert.ToInt32(Convert.IsDBNull(row["IdDato"]) ? 0 : row["IdDato"]),
                        IdPersona = Convert.ToInt32(Convert.IsDBNull(row["IdPersona"]) ? 0 : row["IdPersona"]),
                        IdArticulo = Convert.ToInt32(Convert.IsDBNull(row["IdArticulo"]) ? 0 : row["IdArticulo"]),
                        Cantidad = Convert.ToDecimal(Convert.IsDBNull(row["Cantidad"]) ? 0 : row["Cantidad"]),
                        Valor = Convert.ToSingle(Convert.IsDBNull(row["Valor"]) ? 0 : row["Valor"]),
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
