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
    public class DocumentoTokenRepositorio
    {
        CatDocumentosTokens Tabla;

        /// <summary>
        /// Constructor
        /// </summary>
        public DocumentoTokenRepositorio()
        {
            Tabla = new CatDocumentosTokens();
        }

        public string Guardar(DocumentoToken entidad)
        {
            Tabla.Codigo = entidad.Codigo;
            Tabla.Descripcion = entidad.Descripcion;
            Tabla.TipoDato = entidad.TipoDato;
            Tabla.Formato = entidad.Formato;
            Tabla.Guardar();
            return Tabla.Codigo;
        }

        public bool Remove(string Codigo)
        {
            return Tabla.BorrarPorId(Codigo);
        }

        public List<DocumentoToken> GetAll()
        {
            List<DocumentoToken> Lista = new List<DocumentoToken>();
            DataTable Datos = Tabla.Listar("");
            if (Datos != null)
            {
                foreach (DataRow row in Datos.Rows)
                {
                    Lista.Add(new DocumentoToken()
                    {
                        Codigo = Convert.IsDBNull(row["Codigo"]) ? "" : row["Codigo"].ToString(),
                        Descripcion = Convert.IsDBNull(row["Descripcion"]) ? "" : row["Descripcion"].ToString(),
                        TipoDato = Convert.ToInt32(Convert.IsDBNull(row["TipoDato"]) ? 0 : row["TipoDato"]),
                        Formato = Convert.IsDBNull(row["Formato"]) ? "" : row["Formato"].ToString()
                    });
                }
                Datos.Dispose();
            }
            return Lista;
        }

    }
}
