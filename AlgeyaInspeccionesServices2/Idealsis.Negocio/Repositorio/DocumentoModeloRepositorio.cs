using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Idealsis.Dal;
using Idealsis.Dal.Mapeo;
using Newtonsoft.Json.Linq;

namespace Idealsis.Negocio.Repositorio
{
    public class DocumentoModeloRepositorio
    {
        CatDocumentosModelo Tabla;

        /// <summary>
        /// Constructor
        /// </summary>
        public DocumentoModeloRepositorio()
        {
            Tabla = new CatDocumentosModelo();
        }

        public int Guardar(DocumentoModelo entidad)
        {
            Tabla.Codigo = entidad.Codigo;
            Tabla.Descripcion = entidad.Descripcion;
            Tabla.TipoAuditoria = entidad.TipoAuditoria;
            Tabla.TipoEntidad = entidad.TipoEntidad;
            Tabla.OrigenDatos = entidad.OrigenDatos;
            Tabla.Activo = entidad.Activo;
            Tabla.Guardar();
            return Tabla.Codigo;
        }

        public bool Remove(short Codigo)
        {
            return Tabla.BorrarPorId(Codigo);
        }

        public List<DocumentoModelo> GetAll(int nTipoAuditoria, int nTipoEntidad)
        {
            List<DocumentoModelo> Lista = new List<DocumentoModelo>();
            DataTable Datos = Tabla.Listar("", nTipoAuditoria,nTipoEntidad);
            if (Datos != null)
            {
                foreach (DataRow row in Datos.Rows)
                {
                    Lista.Add(new DocumentoModelo()
                    {
                        Codigo = Convert.ToInt32(Convert.IsDBNull(row["Codigo"]) ? 0 : row["Codigo"]),
                        Descripcion = Convert.IsDBNull(row["Descripcion"]) ? "" : row["Descripcion"].ToString(),
                        TipoAuditoria = Convert.ToInt32(Convert.IsDBNull(row["TipoAuditoria"]) ? 0 : row["TipoAuditoria"]),
                        TipoEntidad = Convert.ToInt32(Convert.IsDBNull(row["TipoEntidad"]) ? 0 : row["TipoEntidad"]),
                        OrigenDatos = Convert.ToByte(Convert.IsDBNull(row["OrigenDatos"]) ? 0 : row["OrigenDatos"]),
                        Activo = Convert.ToByte(Convert.IsDBNull(row["Activo"]) ? 0 : row["Activo"])
                    });
                }
                Datos.Dispose();
            }
            return Lista;
        }

    }
}
