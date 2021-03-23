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
    public class UsuarioGrupoRepositorio
    {
        Usuarios Catalogo;
        short TipoCatalogo = 3;//Grupo de usuarios
        public UsuarioGrupoRepositorio()
        {
            Catalogo = new Usuarios();
        }

        public int Guardar(UsuarioGrupo entidad)
        {
            Catalogo.Id = entidad.Id;
            Catalogo.Catalogo = TipoCatalogo;
            Catalogo.Codigo = entidad.Codigo;
            Catalogo.Descripcion = entidad.Descripcion;
            Catalogo.Nivel = 1;
            Catalogo.Activo = 1;
            return Catalogo.Guardar();
        }

        public bool Remove(int Id)
        {
            return Catalogo.BorrarPorId(Id, false);
        }

        public List<UsuarioGrupo> GetAll()
        {
            List<UsuarioGrupo> Lista = new List<UsuarioGrupo>();
            DataTable Datos = Catalogo.ListarTabla(TipoCatalogo, 0, "");
            if (Datos != null)
            {
                foreach (DataRow row in Datos.Rows)
                {
                    Lista.Add(new UsuarioGrupo()
                    {
                        Id = Convert.ToInt32(Convert.IsDBNull(row["Id"]) ? 0 : row["Id"]),
                        Codigo = Convert.IsDBNull(row["Codigo"]) ? "" : row["Codigo"].ToString(),
                        Descripcion = Convert.IsDBNull(row["Descripcion"]) ? "" : row["Descripcion"].ToString(),
                    });
                }
                Datos.Dispose();
            }
            return Lista;
        }

    }
}
