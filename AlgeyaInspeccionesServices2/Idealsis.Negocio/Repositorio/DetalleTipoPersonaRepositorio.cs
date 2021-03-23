
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
    public class DetalleTipoPersonaRepositorio
    {
        SysCatalogosDetalle Tabla;
        public DetalleTipoPersonaRepositorio()
        {
            Tabla = new SysCatalogosDetalle();
        }

        public int Guardar(DetalleTipoPersona entidad)
        {
            Tabla.Id = entidad.Id;
            Tabla.IdOrigen = entidad.IdOrigen;
            Tabla.Tipo = 1;
            Tabla.Posicion = entidad.Posicion;
            Tabla.IdArticulo = 0;
            Tabla.IdPersona = 0;
            Tabla.IdCatalogo = 0;
            Tabla.IdDato = entidad.IdDato;
            Tabla.Cantidad = 0;
            Tabla.Valor = 0;
            Tabla.Descripcion = entidad.Descripcion;
            Tabla.Texto = "";
            Tabla.Notas = "";
            Tabla.EsRequerido = entidad.EsRequerido;
            Tabla.Id = Tabla.Guardar();
            return Tabla.Id;
        }
        public bool Remove(int Id)
        {
            Tabla.BorrarPorId(Id, false);
            return true;
        }

        public List<DetalleTipoPersona> GetAll(int IdOrigen)
        {
            List<DetalleTipoPersona> Lista = new List<DetalleTipoPersona>();
            DatoRepositorio DatoRep = new DatoRepositorio();
            DataTable Datos = Tabla.ListarTabla(IdOrigen, 1, 0, 0,"");
            if (Datos != null)
            {
                foreach (DataRow row in Datos.Rows)
                {
                    Lista.Add(new DetalleTipoPersona()
                    {
                        Id = Convert.ToInt32(Convert.IsDBNull(row["Id"]) ? 0 : row["Id"]),
                        IdOrigen = Convert.ToInt32(Convert.IsDBNull(row["IdOrigen"]) ? 0 : row["IdOrigen"]),
                        Posicion = Convert.ToInt16(Convert.IsDBNull(row["Posicion"]) ? 0 : row["Posicion"]),
                        IdDato = Convert.ToInt32(Convert.IsDBNull(row["IdDato"]) ? 0 : row["IdDato"]),
                        Descripcion = Convert.IsDBNull(row["Descripcion"]) ? "" : row["Descripcion"].ToString(),
                        EsRequerido = Convert.ToByte(Convert.IsDBNull(row["EsRequerido"]) ? 0 : row["EsRequerido"]),
                        Dato = DatoRep.GetUno(1,Convert.ToInt32(Convert.IsDBNull(row["IdDato"]) ? 0 : row["IdDato"]))
                    });
                }
                Datos.Dispose();
            }
            return Lista;
        }
    }
}
