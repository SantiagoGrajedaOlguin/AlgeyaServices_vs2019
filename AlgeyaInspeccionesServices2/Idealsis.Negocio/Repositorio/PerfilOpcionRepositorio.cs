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
    public class PerfilOpcionRepositorio
    {
        SegCatalogosOpciones Catalogo;
        public PerfilOpcionRepositorio()
        {
            Catalogo = new SegCatalogosOpciones();
        }

        public int Guardar(PerfilOpcion entidad)
        {
            Catalogo.Id = entidad.Id;
            Catalogo.Usuario = entidad.Usuario;
            Catalogo.Perfil = entidad.Perfil;
            Catalogo.Tipo = entidad.Tipo;
            Catalogo.Codigo = entidad.Codigo;
            Catalogo.SoloLectura = entidad.SoloLectura;
            return Catalogo.Guardar(true);
        }
        public bool Remove(int Id)
        {
            Catalogo.BorrarPorId(Id,false);
            return true;
        }

        public bool RemoveAll(int Usuario, int Perfil)
        {
            Catalogo.BorrarTodos(Usuario, Perfil);
            return true;
        }

        public int GetId(int Usuario, int Perfil, string Tipo, string Codigo)
        {
            return Catalogo.GetId(Usuario, Perfil, Tipo, Codigo);
        }

        public bool Existe(int Usuario, int Perfil, string Tipo, string Codigo)
        {
            return Catalogo.GetId(Usuario, Perfil, Tipo, Codigo)>0;
        }

        public List<PerfilOpcion> GetAll(int Usuario, int Perfil, string Tipo)
        {
            List<PerfilOpcion> Lista = new List<PerfilOpcion>();
            DataTable Datos = Catalogo.ListarTabla(Usuario, Perfil, Tipo);
            if (Datos != null)
            {
                foreach (DataRow row in Datos.Rows)
                {
                    Lista.Add(new PerfilOpcion()
                    {
                        Id = Convert.ToInt32(Convert.IsDBNull(row["Id"]) ? 0 : row["Id"]),
                        Usuario = Convert.ToInt32(Convert.IsDBNull(row["Usuario"]) ? 0 : row["Usuario"]),
                        Perfil = Convert.ToInt32(Convert.IsDBNull(row["Perfil"]) ? 0 : row["Perfil"]),
                        Tipo = Convert.IsDBNull(row["Tipo"]) ? "" : row["Tipo"].ToString(),
                        Codigo = Convert.IsDBNull(row["Codigo"]) ? "" : row["Codigo"].ToString(),
                        SoloLectura = Convert.ToByte(Convert.IsDBNull(row["SoloLectura"]) ? 0 : row["SoloLectura"])
                    });
                }
                Datos.Dispose();
            }
            return Lista;
        }

    }
}
