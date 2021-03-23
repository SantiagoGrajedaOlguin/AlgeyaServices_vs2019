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
    public class DatoRepositorio
    {
        SysDatos Tabla;

        public DatoRepositorio()
        {
            Tabla = new SysDatos();
        }

        public Dato BuscarPorCodigo(short Catalogo, int Codigo)
        {
            if (Tabla.BuscarPorCodigo(Catalogo, Codigo))
            {
                return new Dato()
                {
                    Id = Tabla.Id,
                    Catalogo = Tabla.Catalogo,
                    Codigo = Tabla.Codigo,
                    Descripcion = Tabla.Descripcion,
                    EsEtiqueta = Tabla.EsEtiqueta,
                    Tipo = Tabla.Tipo,
                    Formato = Tabla.Formato,
                    FormatoCap = Tabla.FormatoCap,
                    FormatoDes = Tabla.FormatoDes
                };
            }
            else
            {
                return null;
            }
        }

        public int Guardar(Dato entidad)
        {

            Tabla.Id = entidad.Id;
            Tabla.Catalogo = entidad.Catalogo;
            Tabla.Codigo = entidad.Codigo;
            Tabla.Descripcion = entidad.Descripcion;
            Tabla.EsEtiqueta = entidad.EsEtiqueta;
            Tabla.Tipo = entidad.Tipo;
            Tabla.Formato = entidad.Formato;
            Tabla.FormatoCap = entidad.FormatoCap;
            Tabla.FormatoDes = entidad.FormatoDes;
            Tabla.Id = Tabla.Guardar();

            SysDatosOpciones DatoOpciones = new SysDatosOpciones();
            DatoOpciones.GuardarCadena(Tabla.Id, entidad.Opciones);
            return Tabla.Id;
        }

        public bool Remove(int Id)
        {
            return Tabla.BorrarPorId(Id);
        }

        public Dato GetUno(short Catalogo, int IdDato)
        {
            SysDatosOpciones DatoOpciones = new SysDatosOpciones();
            Dato Uno = new Dato();
            DataTable Datos = Tabla.ListarTabla(Catalogo, IdDato, "");
            if (Datos != null)
            {
                if (Datos.Rows.Count > 0)
                {
                    DataRow row = Datos.Rows[0];
                    Uno.Id = IdDato;
                    Uno.Catalogo = Convert.ToInt16(Convert.IsDBNull(row["Catalogo"]) ? 0 : row["Catalogo"]);
                    Uno.Codigo = Convert.ToInt32(Convert.IsDBNull(row["Codigo"]) ? 0 : row["Codigo"]);
                    Uno.Descripcion = Convert.IsDBNull(row["Descripcion"]) ? "" : row["Descripcion"].ToString();
                    Uno.EsEtiqueta = Convert.ToByte(Convert.IsDBNull(row["EsEtiqueta"]) ? 0 : row["EsEtiqueta"]);
                    Uno.Tipo = Convert.ToByte(Convert.IsDBNull(row["Tipo"]) ? 0 : row["Tipo"]);
                    Uno.Opciones = DatoOpciones.ObtenerCadena(IdDato);
                    Uno.Formato = Convert.ToInt16(Convert.IsDBNull(row["Formato"]) ? 0 : row["Formato"]);
                    Uno.FormatoCap = Convert.IsDBNull(row["FormatoCap"]) ? "" : row["FormatoCap"].ToString();
                    Uno.FormatoDes = Convert.IsDBNull(row["FormatoDes"]) ? "" : row["FormatoDes"].ToString();
                }
                Datos.Dispose();
            }
            return Uno;
        }

        public List<Dato> GetAll(short Catalogo)
        {
            SysDatosOpciones DatoOpciones = new SysDatosOpciones();
            int IdDato;
            List<Dato> Lista = new List<Dato>();
            DataTable Datos = Tabla.ListarTabla(Catalogo, 0,"");
            if (Datos != null)
            {
                foreach (DataRow row in Datos.Rows)
                {
                    IdDato = Convert.ToInt32(Convert.IsDBNull(row["Id"]) ? 0 : row["Id"]);
                    Lista.Add(new Dato()
                    {
                        Id = IdDato,
                        Catalogo = Convert.ToInt16(Convert.IsDBNull(row["Catalogo"]) ? 0 : row["Catalogo"]),
                        Codigo = Convert.ToInt32(Convert.IsDBNull(row["Codigo"]) ? 0 : row["Codigo"]),
                        Descripcion = Convert.IsDBNull(row["Descripcion"]) ? "" : row["Descripcion"].ToString(),
                        EsEtiqueta = Convert.ToByte(Convert.IsDBNull(row["EsEtiqueta"]) ? 0 : row["EsEtiqueta"]),
                        Tipo = Convert.ToByte(Convert.IsDBNull(row["Tipo"]) ? 0 : row["Tipo"]),
                        Opciones = DatoOpciones.ObtenerCadena(IdDato),
                        Formato = Convert.ToInt16(Convert.IsDBNull(row["Formato"]) ? 0 : row["Formato"]),
                        FormatoCap = Convert.IsDBNull(row["FormatoCap"]) ? "" : row["FormatoCap"].ToString(),
                        FormatoDes = Convert.IsDBNull(row["FormatoDes"]) ? "" : row["FormatoDes"].ToString()
                    });
                }
                Datos.Dispose();
            }
            return Lista;
        }
    }
}
