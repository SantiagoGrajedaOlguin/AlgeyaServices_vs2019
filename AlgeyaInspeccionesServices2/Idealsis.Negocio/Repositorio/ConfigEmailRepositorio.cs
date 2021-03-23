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
    public class ConfigEmailRepositorio
    {
        SysConfigEmail Tabla;

        public ConfigEmailRepositorio()
        {
            Tabla = new SysConfigEmail();
        }

        public ConfigEmail BuscarPorId(int Id)
        {
            if (Tabla.BuscarPorId(Id))
            {
                return new ConfigEmail()
                {
                    Id = Tabla.Id,
                    Origen = Tabla.Origen,
                    IdOrigen = Tabla.IdOrigen,
                    Descripcion = Tabla.Descripcion,
                    Email = Tabla.Email ,
                    ServidorSmtp = Tabla.ServidorSmtp ,
                    Puerto = Tabla.Puerto ,
                    Autentificacion = Tabla.Autentificacion ,
                    Usuario = Tabla.Usuario ,
                    Password = Tabla.Password,
                    ConSsl = Tabla.ConSsl,
                    Libreria = Tabla.Libreria
                };

            }
            else
            {
                return null;
            }
        }

        public int Guardar(ConfigEmail entidad, bool BuscarId)
        {

            Tabla.Id = entidad.Id;
            Tabla.Origen = entidad.Origen;
            Tabla.IdOrigen = entidad.IdOrigen ;
            Tabla.Descripcion = String.IsNullOrEmpty(entidad.Descripcion)? "":entidad.Descripcion;
            Tabla.Email = String.IsNullOrEmpty(entidad.Email) ? "" : entidad.Email;
            Tabla.ServidorSmtp = String.IsNullOrEmpty(entidad.ServidorSmtp) ? "" : entidad.ServidorSmtp;
            Tabla.Puerto = String.IsNullOrEmpty(entidad.Puerto) ? "" : entidad.Puerto;
            Tabla.Autentificacion = entidad.Autentificacion;
            Tabla.Usuario = String.IsNullOrEmpty(entidad.Usuario) ? "" : entidad.Usuario;
            Tabla.Password = String.IsNullOrEmpty(entidad.Password) ? "" : entidad.Password;
            Tabla.ConSsl = entidad.ConSsl;
            Tabla.Libreria = entidad.Libreria;
            Tabla.Id = Tabla.Guardar(BuscarId);
            return Tabla.Id;
        }

        public bool Remove(int Id)
        {
            return Tabla.BorrarPorId(Id);
        }

        public ConfigEmail GetUno(byte Origen, int IdOrigen)
        {
            ConfigEmail Uno = new ConfigEmail();
            DataTable Datos = Tabla.ListarTabla(Origen, IdOrigen, "");
            if (Datos != null)
            {
                if (Datos.Rows.Count > 0)
                {
                    DataRow row = Datos.Rows[0];
                    Uno.Id = Convert.ToInt32(Convert.IsDBNull(row["Id"]) ? 0 : row["Id"]);
                    Uno.Origen = Convert.ToByte(Convert.IsDBNull(row["Origen"]) ? 0 : row["Origen"]);
                    Uno.IdOrigen = Convert.ToInt32(Convert.IsDBNull(row["IdOrigen"]) ? 0 : row["IdOrigen"]);
                    Uno.Descripcion = Convert.IsDBNull(row["Descripcion"]) ? "" : row["Descripcion"].ToString();
                    Uno.Email = Convert.IsDBNull(row["Email"]) ? "" : row["Email"].ToString();
                    Uno.ServidorSmtp = Convert.IsDBNull(row["ServidorSmtp"]) ? "" : row["ServidorSmtp"].ToString();
                    Uno.Puerto = Convert.IsDBNull(row["Puerto"]) ? "" : row["Puerto"].ToString();
                    Uno.Autentificacion = Convert.ToByte(Convert.IsDBNull(row["Autentificacion"]) ? 0 : row["Autentificacion"]);
                    Uno.Usuario = Convert.IsDBNull(row["Usuario"]) ? "" : row["Usuario"].ToString();
                    Uno.Password = Convert.IsDBNull(row["Password"]) ? "" : row["Password"].ToString();
                    Uno.ConSsl = Convert.ToByte(Convert.IsDBNull(row["ConSsl"]) ? 0 : row["ConSsl"]);
                    Uno.Libreria = Convert.ToByte(Convert.IsDBNull(row["Libreria"]) ? 0 : row["Libreria"]);
                }
                Datos.Dispose();
            }
            return Uno;
        }

        public List<ConfigEmail> GetAll(byte Origen, int IdOrigen)
        {
            List<ConfigEmail> Lista = new List<ConfigEmail>();
            DataTable Datos = Tabla.ListarTabla(Origen,IdOrigen,"");
            if (Datos != null)
            {
                foreach (DataRow row in Datos.Rows)
                {
                    Lista.Add(new ConfigEmail()
                    {
                        Id = Convert.ToInt32(Convert.IsDBNull(row["Id"]) ? 0 : row["Id"]),
                        Origen = Convert.ToByte(Convert.IsDBNull(row["Origen"]) ? 0 : row["Origen"]),
                        IdOrigen = Convert.ToInt32(Convert.IsDBNull(row["IdOrigen"]) ? 0 : row["IdOrigen"]),
                        Descripcion = Convert.IsDBNull(row["Descripcion"]) ? "" : row["Descripcion"].ToString(),
                        Email = Convert.IsDBNull(row["Email"]) ? "" : row["Email"].ToString(),
                        ServidorSmtp = Convert.IsDBNull(row["ServidorSmtp"]) ? "" : row["ServidorSmtp"].ToString(),
                        Puerto = Convert.IsDBNull(row["Puerto"]) ? "" : row["Puerto"].ToString(),
                        Autentificacion = Convert.ToByte(Convert.IsDBNull(row["Autentificacion"]) ? 0 : row["Autentificacion"]),
                        Usuario = Convert.IsDBNull(row["Usuario"]) ? "" : row["Usuario"].ToString(),
                        Password = Convert.IsDBNull(row["Password"]) ? "" : row["Password"].ToString(),
                        ConSsl = Convert.ToByte(Convert.IsDBNull(row["ConSsl"]) ? 0 : row["ConSsl"]),
                        Libreria = Convert.ToByte(Convert.IsDBNull(row["Libreria"]) ? 0 : row["Libreria"])
                    });
                }
                Datos.Dispose();
            }
            return Lista;
        }
    }
}
