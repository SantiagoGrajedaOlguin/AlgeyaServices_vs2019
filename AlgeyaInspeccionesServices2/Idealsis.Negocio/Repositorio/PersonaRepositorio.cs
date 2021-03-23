using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Idealsis.Dal;
using Idealsis.Dal.Mapeo;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Idealsis.Negocio.Repositorio
{
    public class PersonaRepositorio
    {
        CatPersonas Catalogo;
        CatDirecciones CatalogoDir;

        public PersonaRepositorio()
        {
            Catalogo = new CatPersonas();
            CatalogoDir = new CatDirecciones();
        }

        public string GetDes(int Id)
        {
            return Catalogo.GetDes(Id);
        }

        public Persona BuscarPorId(int Id)
        {
            if (Catalogo.BuscarPorId(Id))
            {
                int IdDir = Catalogo.IdDireccion;
                Persona Result = new Persona
                {
                    Id = Catalogo.Id,
                    TipoDePersona = Catalogo.TipoDePersona,
                    IdPadre = Catalogo.IdPadre,
                    Codigo = Catalogo.Codigo,
                    Descripcion = Catalogo.Descripcion,
                    Corta = Catalogo.Corta,
                    Tipo = Catalogo.Tipo,
                    Puesto = Catalogo.Puesto,
                    Sistema = Catalogo.Sistema,
                    Representante = Catalogo.Representante ,
                    Oficial = Catalogo.Oficial,
                    ObjetoSocial = Catalogo.ObjetoSocial,
                    FechaConstitucion = Catalogo.FechaConstitucion,
                    FolioEscritura = Catalogo.FolioEscritura,
                    RegistroCondusef = Catalogo.RegistroCondusef,
                    Activo = Catalogo.Activo
                };
                if (CatalogoDir.BuscarPorId(IdDir))
                {
                    Result.Rfc = CatalogoDir.Rfc;
                    Result.Direccion = CatalogoDir.Direccion;
                    Result.Direccion  = CatalogoDir.Direccion;
                    Result.NoExterior  = CatalogoDir.NoExterior;
                    Result.NoInterior = CatalogoDir.NoExterior;
                    Result.Referencia = CatalogoDir.Referencia;
                    Result.Colonia = CatalogoDir.Colonia;
                    Result.Cp = CatalogoDir.Cp;
                    Result.Pais = CatalogoDir.Pais;
                    Result.Estado = CatalogoDir.Estado;
                    Result.Municipio = CatalogoDir.Municipio;
                    Result.Poblacion = CatalogoDir.Poblacion;
                    Result.Email = CatalogoDir.Email;
                    Result.Telefono = CatalogoDir.Telefono;
                }
                return Result;
            }
            else
            {
                return null;
            }
        }
        public int Guardar(Persona entidad)
        {
            int Id = entidad.Id;
            Catalogo.Id = Id;
            Catalogo.TipoDePersona  = entidad.TipoDePersona;
            Catalogo.IdPadre = entidad.IdPadre;
            Catalogo.Codigo = entidad.Codigo;
            Catalogo.Descripcion = entidad.Descripcion;
            Catalogo.Corta = entidad.Corta;
            Catalogo.TipoAdmin  = entidad.TipoAdmin;
            Catalogo.RequiereCCC = entidad.RequiereCCC;
            Catalogo.Tipo = entidad.Tipo;
            Catalogo.Puesto = entidad.Puesto;
            Catalogo.Sistema = entidad.Sistema;
            Catalogo.Representante  = entidad.Representante;
            Catalogo.Oficial  = entidad.Oficial;
            Catalogo.ObjetoSocial = entidad.ObjetoSocial;
            Catalogo.FechaConstitucion = entidad.FechaConstitucion;
            Catalogo.FolioEscritura = entidad.FolioEscritura;
            Catalogo.RegistroCondusef = entidad.RegistroCondusef;
            Catalogo.Activo = (byte)entidad.Activo;
            Catalogo.FechaDeAlta = DateTime.Now.ToShortDateString();
            Catalogo.DetalleJson = entidad.DetalleJson;
            Id = Catalogo.Guardar();

            if (entidad.DetalleJson != null)
            {
                if (entidad.DetalleJson.Length > 0)
                {
                    //---Guardar detalle en JSON---//
                    PersonaDetalleRepositorio CatalogoDet = new PersonaDetalleRepositorio();
                    var ListaDetalle = JsonConvert.DeserializeObject<List<PersonaDetalle>>(entidad.DetalleJson);
                    foreach (PersonaDetalle Detalle in ListaDetalle)
                    {
                        Detalle.IdOrigen = Id;
                        CatalogoDet.Guardar(Detalle);
                    }
                    //-----------------------------//
                }
            }
            CatalogoDir.Origen = 1;
            CatalogoDir.Rfc = entidad.Rfc;
            CatalogoDir.IdOrigen = Id;
            CatalogoDir.Direccion = entidad.Direccion;
            CatalogoDir.NoExterior = entidad.NoExterior;
            CatalogoDir.NoInterior = entidad.NoInterior;
            CatalogoDir.Referencia = entidad.Referencia;
            CatalogoDir.Colonia = entidad.Colonia;
            CatalogoDir.Cp = entidad.Cp;
            CatalogoDir.Pais = entidad.Pais;
            CatalogoDir.Estado = entidad.Estado;
            CatalogoDir.Municipio = entidad.Municipio;
            CatalogoDir.Poblacion = entidad.Poblacion;
            CatalogoDir.Email = entidad.Email;
            CatalogoDir.Telefono = entidad.Telefono;
            CatalogoDir.Guardar(true);
            return Id;
        }
        public bool Remove(int Id)
        {
            return Catalogo.BorrarPorId(Id, false);
        }
        public List<Persona> GetAll(byte tipoDePersona, int idPadre)
        {
            List<Persona> Lista = new List<Persona>();
            DataTable Datos = Catalogo.Listar(tipoDePersona, idPadre, "");
            if (Datos != null)
            {
                foreach (DataRow row in Datos.Rows)
                {
                    Lista.Add(new Persona()
                    {
                        Id = Convert.ToInt32(Convert.IsDBNull(row["Id"]) ? 0 : row["Id"]),
                        TipoDePersona = Convert.ToByte(Convert.IsDBNull(row["TipoDePersona"]) ? 0 : row["TipoDePersona"]),
                        IdPadre = Convert.ToInt16(Convert.IsDBNull(row["IdPadre"]) ? 0 : row["IdPadre"]),
                        Codigo = Convert.ToInt16(Convert.IsDBNull(row["Codigo"]) ? 0 : row["Codigo"]),
                        Descripcion = Convert.IsDBNull(row["Descripcion"]) ? "" : row["Descripcion"].ToString(),
                        Corta = Convert.IsDBNull(row["Corta"]) ? "" : row["Corta"].ToString(),
                        RequiereCCC = Convert.ToByte(Convert.IsDBNull(row["RequiereCCC"]) ? 0 : row["RequiereCCC"]),
                        TipoAdmin = Convert.ToByte(Convert.IsDBNull(row["TipoAdmin"]) ? 0 : row["TipoAdmin"]),
                        Tipo = Convert.ToInt32(Convert.IsDBNull(row["Tipo"]) ? 0 : row["Tipo"]),
                        Puesto = Convert.ToInt32(Convert.IsDBNull(row["Puesto"]) ? 0 : row["Puesto"]),
                        Sistema = Convert.ToInt32(Convert.IsDBNull(row["Sistema"]) ? 0 : row["Sistema"]),
                        Representante = Convert.ToInt32(Convert.IsDBNull(row["Representante"]) ? 0 : row["Representante"]),
                        Oficial = Convert.ToInt32(Convert.IsDBNull(row["Oficial"]) ? 0 : row["Oficial"]),
                        ObjetoSocial = Convert.IsDBNull(row["ObjetoSocial"]) ? "" : row["ObjetoSocial"].ToString(),
                        FechaConstitucion = Convert.IsDBNull(row["FechaConstitucion"]) ? "" : Convert.ToDateTime(row["FechaConstitucion"]).ToString("dd/MM/yyyy hh:mm:ss tt"),
                        FolioEscritura = Convert.IsDBNull(row["FolioEscritura"]) ? "" : row["FolioEscritura"].ToString(),
                        Rfc = Convert.IsDBNull(row["Rfc"]) ? "" : row["Rfc"].ToString(),
                        RegistroCondusef = Convert.IsDBNull(row["RegistroCondusef"]) ? "" : row["RegistroCondusef"].ToString(),
                        Activo = Convert.ToByte(Convert.IsDBNull(row["Activo"]) ? 0 : row["Activo"]),
                        Direccion = Convert.IsDBNull(row["Direccion"]) ? "" : row["Direccion"].ToString(),
                        NoExterior = Convert.IsDBNull(row["NoExterior"]) ? "" : row["NoExterior"].ToString(),
                        NoInterior = Convert.IsDBNull(row["NoInterior"]) ? "" : row["NoInterior"].ToString(),
                        Referencia = Convert.IsDBNull(row["Referencia"]) ? "" : row["Referencia"].ToString(),
                        Colonia = Convert.IsDBNull(row["Colonia"]) ? "" : row["Colonia"].ToString(),
                        Cp = Convert.IsDBNull(row["Cp"]) ? "" : row["Cp"].ToString(),
                        Pais = Convert.IsDBNull(row["Pais"]) ? "" : row["Pais"].ToString(),
                        Estado = Convert.IsDBNull(row["Estado"]) ? "" : row["Estado"].ToString(),
                        Municipio = Convert.IsDBNull(row["Municipio"]) ? "" : row["Municipio"].ToString(),
                        Poblacion = Convert.IsDBNull(row["Poblacion"]) ? "" : row["Poblacion"].ToString(),
                        Email = Convert.IsDBNull(row["Email"]) ? "" : row["Email"].ToString(),
                        Telefono = Convert.IsDBNull(row["Telefono"]) ? "" : row["Telefono"].ToString()
                    });
                }
                Datos.Dispose();
            }
            return Lista;
        }
    }
}
