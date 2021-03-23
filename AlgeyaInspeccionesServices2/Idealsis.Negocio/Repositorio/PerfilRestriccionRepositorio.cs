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
    public class PerfilRestriccionRepositorio
    {
        SegCatalogosRestricciones Catalogo;
        public PerfilRestriccionRepositorio()
        {
            Catalogo = new SegCatalogosRestricciones();
        }

        public int Guardar(PerfilRestriccion entidad)
        {
            Catalogo.Id = entidad.Id;
            Catalogo.Usuario = entidad.Usuario;         // 0 = es solo perfil o id de usuario
            Catalogo.Perfil = entidad.Perfil;           // id del perfil
            Catalogo.Tipo = entidad.Tipo;               // 1 = cliente
            Catalogo.Codigo = entidad.Codigo;           // Id del cliente
            Catalogo.Cuenta = entidad.Cuenta;           // vacio
            Catalogo.Descripcion = entidad.Descripcion; // Nombre del cliente
            Catalogo.Padre = entidad.Padre;             // 0
            Catalogo.Valor = entidad.Valor;             // 0
            Catalogo.EsPred = entidad.EsPred;           // 0
            Catalogo.Posicion = entidad.Posicion;       // 0
            return Catalogo.Guardar(true);
        }
        public bool Remove(int Id)
        {
            Catalogo.BorrarPorId(Id);
            return true;
        }


        public bool RemoveAll(int Usuario, int Perfil)
        {
            Catalogo.BorrarTodos(Usuario, Perfil);
            return true;
        }

        public List<PerfilRestriccion> GetAll(int Usuario, int Perfil, short Tipo)
        {
            List<PerfilRestriccion> Lista = new List<PerfilRestriccion>();
            DataTable Datos = Catalogo.ListarTabla(Usuario, Perfil, Tipo);
            if (Datos != null)
            {
                foreach (DataRow row in Datos.Rows)
                {
                    Lista.Add(new PerfilRestriccion()
                    {
                        Id = Convert.ToInt32(Convert.IsDBNull(row["Id"]) ? 0 : row["Id"]),
                        Usuario = Convert.ToInt32(Convert.IsDBNull(row["Usuario"]) ? 0 : row["Usuario"]),
                        Perfil = Convert.ToInt32(Convert.IsDBNull(row["Perfil"]) ? 0 : row["Perfil"]),
                        Tipo = Convert.ToInt16(Convert.IsDBNull(row["Tipo"]) ? 0 : row["Tipo"]),
                        Codigo = Convert.ToInt32(Convert.IsDBNull(row["Codigo"]) ? 0 : row["Codigo"]),
                        Cuenta = Convert.IsDBNull(row["Cuenta"]) ? "" : row["Cuenta"].ToString(),
                        Descripcion = Convert.IsDBNull(row["Descripcion"]) ? "" : row["Descripcion"].ToString(),
                        Padre = Convert.ToInt32(Convert.IsDBNull(row["Padre"]) ? 0 : row["Padre"]),
                        Valor = Convert.ToInt16(Convert.IsDBNull(row["Valor"]) ? 0 : row["Valor"]),
                        EsPred = Convert.ToByte(Convert.IsDBNull(row["EsPred"]) ? 0 : row["EsPred"]),
                        Posicion = Convert.ToInt16(Convert.IsDBNull(row["Posicion"]) ? 0 : row["Posicion"])
                    });
                }
                Datos.Dispose();
            }
            return Lista;
        }
    }
}
