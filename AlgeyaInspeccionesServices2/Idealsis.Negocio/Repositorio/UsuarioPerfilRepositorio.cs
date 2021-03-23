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
    public class UsuarioPerfilRepositorio
    {
        Usuarios Catalogo;
        short TipoCatalogo = 2; //Perfil de usuarios
        public UsuarioPerfilRepositorio()
        {
            Catalogo = new Usuarios();
        }

        public int Guardar(UsuarioPerfil entidad)
        {
            Catalogo.Id = entidad.Id;
            Catalogo.Catalogo = TipoCatalogo;
            Catalogo.Codigo = entidad.Codigo;
            Catalogo.Descripcion = entidad.Descripcion;
            Catalogo.Nivel = (byte)entidad.Nivel;
            Catalogo.Activo = 1;
            return Catalogo.Guardar();
        }
        public bool Remove(int Id)
        {
            return Catalogo.BorrarPorId(Id, false);
        }

        public List<UsuarioPerfil> GetAll()
        {
            List<UsuarioPerfil> Lista = new List<UsuarioPerfil>();
            DataTable Datos = Catalogo.ListarTabla(TipoCatalogo, 0, "");
            if (Datos != null)
            {
                foreach (DataRow row in Datos.Rows)
                {
                    Lista.Add(new UsuarioPerfil()
                    {
                        Id = Convert.ToInt32(Convert.IsDBNull(row["Id"]) ? 0 : row["Id"]),
                        Codigo = Convert.IsDBNull(row["Codigo"]) ? "" : row["Codigo"].ToString(),
                        Descripcion = Convert.IsDBNull(row["Descripcion"]) ? "" : row["Descripcion"].ToString(),
                        Nivel = Convert.ToInt32(Convert.IsDBNull(row["Nivel"]) ? 0 : row["Nivel"])
                    });
                }
                Datos.Dispose();
            }
            return Lista;
        }
    }
}
