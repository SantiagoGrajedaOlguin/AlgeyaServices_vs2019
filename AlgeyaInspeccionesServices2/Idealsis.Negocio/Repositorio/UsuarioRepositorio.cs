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
    public class UsuarioRepositorio
    {
        Usuarios Catalogo;
        short TipoCatalogo = 1; //Perfil de usuarios
        public UsuarioRepositorio()
        {
            Catalogo = new Usuarios();
        }

        public Usuario Buscar(string NombreUsuario)
        {
            if (Catalogo.BuscarPorCodigo(TipoCatalogo, NombreUsuario))
            {
                return new Usuario()
                {
                    Id = Catalogo.Id,
                    Codigo = Catalogo.Codigo,
                    Descripcion = Catalogo.Descripcion,
                    Activo = Catalogo.Activo,
                    Password = Catalogo.Password,
                    IdGrupo = Catalogo.IdPadre,
                    IdPerfil = Catalogo.GetPerfil() 
                };
            }
            else
            {
                return null;
            }
        }
        public int Guardar(Usuario entidad)
        {
            SegCatalogosDetalle Detalle;

            int Id = entidad.Id;
            Catalogo.Id = Id;
            Catalogo.Catalogo = TipoCatalogo;
            Catalogo.Codigo = entidad.Codigo;
            Catalogo.Descripcion = entidad.Descripcion;
            Catalogo.Password = entidad.Password;
            Catalogo.IdPadre = entidad.IdGrupo;
            Catalogo.Nivel = (byte)entidad.Nivel;
            Catalogo.Activo = (byte)entidad.Activo;
            if (Id > 0)
            {
                int PerfilAnterior = Catalogo.GetPerfil();
                if (PerfilAnterior>0)
                {
                    if (PerfilAnterior!=entidad.IdPerfil)
                    {
                        Catalogo.BorrarPerfil(Id,PerfilAnterior);
                    }
                }
            }
            Id = Catalogo.Guardar();
            if (entidad.IdPerfil > 0)
            {
                Detalle = new SegCatalogosDetalle();
                Detalle.Usuario = Id;
                Detalle.Perfil = entidad.IdPerfil;
                Detalle.ConVigencia = 0;
                Detalle.InicioVigencia = "";
                Detalle.FinVigencia = "";
                Detalle.Guardar(true);
            }

            //guardar correo electronico
            /*
            SysConfigEmail Email = new SysConfigEmail();
            Email.Origen = 1;
            Email.IdOrigen = Id;
            Email.Email = entidad.Correo.Email;
            Email.Descripcion = entidad.Descripcion;
            Email.ServidorSmtp = entidad.Correo.ServidorSmtp;
            Email.Autentificacion = entidad.Correo.Autentificacion;
            Email.Usuario = entidad.Correo.Usuario;
            Email.Password = entidad.Correo.Password;
            Email.ConSsl = entidad.Correo.ConSsl;
            Email.Libreria = entidad.Correo.Libreria;
            Email.Guardar(true);
            */
            ConfigEmailRepositorio Email = new ConfigEmailRepositorio();
            entidad.Correo.Origen = 1;
            entidad.Correo.IdOrigen = Id;
            Email.Guardar(entidad.Correo,true);
            return Id;
        }
        public bool Remove(int Id)
        {
            return Catalogo.BorrarPorId(Id, false);
        }

        public List<Opcion> GetOpcionesHabilitadas(Usuario entidad, string Tipo)
        {
            JObject Datos = new JObject();
            List<Opcion> Lista = new List<Opcion>();
            int i;
            int CountFilas = Catalogo.ConsultarOpciones(entidad.Id,entidad.IdPerfil, Tipo, ref Datos);
            if (CountFilas > 0)
            {
                for (i = 0; i < CountFilas; i++)
                {
                    Lista.Add(new Opcion()
                    {
                        Tipo = Datos["Records"][i]["Tipo"].ToString(),
                        Codigo = Datos["Records"][i]["Codigo"].ToString(),
                        Descripcion = Datos["Records"][i]["Descripcion"].ToString(),
                        Padre = Datos["Records"][i]["Padre"].ToString(),
                        Origen = Convert.ToInt16(Convert.ToString(Datos["Records"][i]["Origen"]) == "" ? 0 : Datos["Records"][i]["Origen"]),
                        Catalogo = Convert.ToInt16(Convert.ToString(Datos["Records"][i]["Catalogo"]) == "" ? 0 : Datos["Records"][i]["Catalogo"]),
                        Orden = Convert.ToInt32(Convert.ToString(Datos["Records"][i]["Orden"]) == "" ? 0 : Datos["Records"][i]["Orden"])
                    });
                }
            }
            return Lista;
        }

        public List<Usuario> GetAll()
        {
            SegCatalogosDetalle Detalle = new SegCatalogosDetalle();
            ConfigEmailRepositorio Email = new ConfigEmailRepositorio();

            List<Usuario> Lista = new List<Usuario>();
            DataTable  Datos = Catalogo.ListarTabla(TipoCatalogo, 0, "");
            if (Datos != null)
            {
                foreach (DataRow row in Datos.Rows)
                {
                    Lista.Add(new Usuario()
                    {
                        Id = Convert.ToInt32(Convert.IsDBNull(row["Id"]) ? 0 : row["Id"]),
                        Codigo = Convert.IsDBNull(row["Codigo"]) ? "" : row["Codigo"].ToString(),
                        Descripcion = Convert.IsDBNull(row["Descripcion"]) ? "" : row["Descripcion"].ToString(),
                        Password = Convert.IsDBNull(row["Password"]) ? "" : row["Password"].ToString(),
                        IdGrupo = Convert.ToInt32(Convert.IsDBNull(row["IdPadre"]) ? 0 : row["IdPadre"]),
                        IdPerfil = Detalle.ObtenerIdPerfil(Convert.ToInt32(Convert.IsDBNull(row["Id"]) ? 0 : row["Id"])),
                        Nivel = Convert.ToInt32(Convert.IsDBNull(row["Nivel"]) ? 0 : row["Nivel"]),
                        Correo = Email.GetUno(1, Convert.ToInt32(Convert.IsDBNull(row["Id"]) ? 0 : row["Id"]))
                    });
                }
            }
            return Lista;
        }

        public void GetAllListas(ref List<Usuario> ListaUsuarios, ref List<UsuarioPerfil> ListaPerfiles, ref List<UsuarioGrupo> ListaGrupos)
        {
            SegCatalogosDetalle Detalle = new SegCatalogosDetalle();
            ConfigEmailRepositorio Email = new ConfigEmailRepositorio();
            DataSet Datos = Catalogo.ListarTodos();
            if (Datos != null)
            {
                foreach (DataRow row in Datos.Tables[0].Rows)
                {
                    ListaUsuarios.Add(new Usuario()
                    {
                        Id = Convert.ToInt32(Convert.IsDBNull(row["Id"]) ? 0 : row["Id"]),
                        Codigo = Convert.IsDBNull(row["Codigo"]) ? "" : row["Codigo"].ToString(),
                        Descripcion = Convert.IsDBNull(row["Descripcion"]) ? "" : row["Descripcion"].ToString(),
                        Password = Convert.IsDBNull(row["Password"]) ? "" : row["Password"].ToString(),
                        IdGrupo = Convert.ToInt32(Convert.IsDBNull(row["IdPadre"]) ? 0 : row["IdPadre"]),
                        IdPerfil = Detalle.ObtenerIdPerfil(Convert.ToInt32(Convert.IsDBNull(row["Id"]) ? 0 : row["Id"])),
                        Nivel = Convert.ToInt32(Convert.IsDBNull(row["Nivel"]) ? 0 : row["Nivel"]),
                        Correo = Email.GetUno(1, Convert.ToInt32(Convert.IsDBNull(row["Id"]) ? 0 : row["Id"]))
                    });
                }
                foreach (DataRow row in Datos.Tables[1].Rows)
                {
                    ListaPerfiles.Add(new UsuarioPerfil()
                    {
                        Id = Convert.ToInt32(Convert.IsDBNull(row["Id"]) ? 0 : row["Id"]),
                        Codigo = Convert.IsDBNull(row["Codigo"]) ? "" : row["Codigo"].ToString(),
                        Descripcion = Convert.IsDBNull(row["Descripcion"]) ? "" : row["Descripcion"].ToString(),
                        Nivel = Convert.ToInt32(Convert.IsDBNull(row["Nivel"]) ? 0 : row["Nivel"])
                    });
                }
                foreach (DataRow row in Datos.Tables[2].Rows)
                {
                    ListaGrupos.Add(new UsuarioGrupo()
                    {
                        Id = Convert.ToInt32(Convert.IsDBNull(row["Id"]) ? 0 : row["Id"]),
                        Codigo = Convert.IsDBNull(row["Codigo"]) ? "" : row["Codigo"].ToString(),
                        Descripcion = Convert.IsDBNull(row["Descripcion"]) ? "" : row["Descripcion"].ToString(),
                    });
                }
            }
        }

    }
}
