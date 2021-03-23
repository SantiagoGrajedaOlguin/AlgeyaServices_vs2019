using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace Idealsis.Dal.Mapeo
{
    public class ExpedientesHallazgo
    {
        const string Tabla = "ExpedientesHallazgo";

        int    id;
        int    idOrigen;
        int    folio;
        string fecha;
        string hora;
        int    tipo;
        int    nivel;
        int    persona;
        string descripcion;
        int    area;
        string fechaCompromiso;
        int    estatus;
        string usuario;
        string fechaMod;
        string usuarioMod;
        string tipoDesc;
        string nivelDesc;
        string areaDesc;
        string estatusDesc;


        public int Id { get => id; set => id = value; }
        public int IdOrigen { get => idOrigen; set => idOrigen = value; }
        public int Folio { get => folio; set => folio = value; }
        public string Fecha { get => fecha; set => fecha = value; }
        public string Hora { get => hora; set => hora = value; }
        public int Tipo { get => tipo; set => tipo = value; }
        public int Nivel { get => nivel; set => nivel = value; }
        public int Persona { get => persona; set => persona = value; }
        public string Descripcion { get => descripcion; set => descripcion = value; }
        public int Area { get => area; set => area = value; }
        public string FechaCompromiso { get => fechaCompromiso; set => fechaCompromiso = value; }
        public int Estatus { get => estatus; set => estatus = value; }
        public string Usuario { get => usuario; set => usuario = value; }
        public string FechaMod { get => fechaMod; set => fechaMod = value; }
        public string UsuarioMod { get => usuarioMod; set => usuarioMod = value; }
        public string TipoDesc { get => tipoDesc; }
        public string NivelDesc { get => nivelDesc; }
        public string AreaDesc { get => areaDesc; }
        public string EstatusDesc { get => estatusDesc; }

        public int Guardar()
        {
            string c;
            string v;
            var cnn = new Conexion();

            cnn.Conectar();
            c = "Id,0,9|IdOrigen|Folio|Fecha,2|Hora,3,2|Tipo|Nivel|Persona|Descripcion,1|";
            v = id + "|" + idOrigen + "|" + folio + "|" + fecha + "|" + hora + "|" + tipo + "|" + nivel + "|" + persona + "|" + descripcion + "|";
            c = c + "Area|FechaCompromiso,2|Estatus|Usuario,1,2|UsuarioMod,1|FechaMod,2";
            v = v + area + "|" + fechaCompromiso + "|" + estatus + "|" + usuario + "|" + usuarioMod + "|" + fechaMod;
            id = cnn.GuardarRegistro(Tabla, c, v);
            cnn.Dispose();
            cnn = null;
            return id;
        }

        private void AsignarValores(IDataReader Registro)
        {
            var cnn = new Conexion();
            cnn.Conectar();

            id = Convert.ToInt32(Registro["Id"].ToString());
            idOrigen = Convert.ToInt32(Convert.IsDBNull(Registro["IdOrigen"]) ? 0 : Registro["IdOrigen"]);
            folio = Convert.ToInt32(Convert.IsDBNull(Registro["Folio"]) ? 0 : Registro["Folio"]);
            fecha = Convert.IsDBNull(Registro["Fecha"]) ? "" : Registro["Fecha"].ToString();
            hora = Convert.IsDBNull(Registro["Hora"]) ? "" : Registro["Hora"].ToString();
            tipo = Convert.ToInt32(Convert.IsDBNull(Registro["Tipo"]) ? 0 : Registro["Tipo"]);
            nivel = Convert.ToInt32(Convert.IsDBNull(Registro["Nivel"]) ? 0 : Registro["Nivel"]);
            persona = Convert.ToInt32(Convert.IsDBNull(Registro["Persona"]) ? 0 : Registro["Persona"]);
            descripcion = Convert.IsDBNull(Registro["Descripcion"]) ? "" : Registro["Descripcion"].ToString();
            area = Convert.ToInt32(Convert.IsDBNull(Registro["Area"]) ? 0 : Registro["Area"]);
            fechaCompromiso = Convert.IsDBNull(Registro["FechaCompromiso"]) ? "" : Registro["FechaCompromiso"].ToString();
            estatus = Convert.ToInt32(Convert.IsDBNull(Registro["Estatus"]) ? 0 : Registro["Estatus"]);
            usuario = Convert.IsDBNull(Registro["Usuario"]) ? "" : Registro["Usuario"].ToString();
            fechaMod = Convert.IsDBNull(Registro["FechaMod"]) ? "" : Registro["FechaMod"].ToString();
            usuarioMod = Convert.IsDBNull(Registro["UsuarioMod"]) ? "" : Registro["UsuarioMod"].ToString();
            tipoDesc = cnn.DameValor("SysCatalogos", "Id=" + tipo, "Descripcion");
            switch (nivel)
            {
                case 1: nivelDesc = "Bajo"; break;
                case 2: nivelDesc = "Medio"; break;
                case 3: nivelDesc = "Alto"; break;
                default: nivelDesc = "Sin nivel"; break;
            }
            areaDesc = cnn.DameValor("SysCatalogos", "Id=" + area, "Descripcion");
            estatusDesc = cnn.DameValor("SysCatalogos", "Id=" + estatus, "Descripcion");
            Registro.Dispose();
            cnn.Dispose();
        }

        public bool BuscarPorId(int vId)
        {
            var cnn = new Conexion();
            bool result = false;
            cnn.Conectar();
            IDataReader Registro = cnn.ObtenerRegistro(Tabla, "Id=" + vId);
            if (Registro.Read())
            {
                result = true;
                AsignarValores(Registro);
            }
            Registro.Dispose();
            cnn.Dispose();
            return result;
        }

        public bool Existe(int vIdOrigen, int vFolio)
        {
            var cnn = new Conexion();
            bool result = false;
            cnn.Conectar();
            result = cnn.ExisteRegistro(Tabla, "IdOrigen=" + vIdOrigen + " and Folio=" + vFolio, "Id");
            cnn.Dispose();
            return result;
        }

        public string ObtenerValor(int vId, string Campo)
        {
            var cnn = new Conexion();
            string result = "";
            cnn.Conectar();
            result = cnn.DameValor(Tabla, "Id=" + vId, Campo);
            cnn.Dispose();
            return result;
        }

        public bool BorrarPorId(int vId, bool Preguntar)
        {
            var cnn = new Conexion();
            bool Respuesta;
            cnn.Conectar();
            Respuesta = cnn.BorrarRegistro(Tabla, "Id=" + vId, Preguntar);
            cnn.Dispose();
            return Respuesta;
        }

        public DataTable ListarTabla(int vIdOrigen)
        {
            var cnn = new Conexion();
            string strConsulta;
            string strFiltro;

            //generar consulta SQL
            strFiltro = " WHERE d.IdOrigen=" + vIdOrigen;

            strConsulta = "SELECT d.Id, d.IdOrigen, d.Folio, d.Fecha, d.Hora, d.Tipo, Tipos.Descripcion as TipoDesc, d.Nivel, NivelDesc = CASE d.Nivel WHEN 1 THEN 'Bajo' WHEN 2 THEN 'Medio' ELSE 'Alto' END, d.Persona, d.Descripcion," +
                          "d.Area, Areas.Descripcion as AreaDesc, d.FechaCompromiso, d.Estatus, Estados.Descripcion as EstatusDesc, d.Usuario, d.UsuarioMod, d.FechaMod" +
                          " FROM ([" + Tabla + "] as d LEFT JOIN SysCatalogos as Tipos ON d.Tipo=Tipos.Id" +
                          " LEFT JOIN SysCatalogos as Areas ON d.Area=Areas.Id) LEFT JOIN SysCatalogos as Estados ON d.Estatus=Estados.Id" +
                          strFiltro + " ORDER BY d.Fecha,d.Folio";

            //obtener registros
            cnn.Conectar();
            DataTable Registro = cnn.GetTabla(strConsulta);
            cnn.Dispose();
            return Registro;
        }

        public DataTable ListarTablaPorId(int vId)
        {
            var cnn = new Conexion();
            string strConsulta;
            string strFiltro;

            //generar consulta SQL
            strFiltro = " WHERE d.Id=" + vId;

            strConsulta = "SELECT d.Id, d.IdOrigen, d.Folio, d.Fecha, d.Hora, d.Tipo, Tipos.Descripcion as TipoDesc, d.Nivel, NivelDesc = CASE d.Nivel WHEN 1 THEN 'Bajo' WHEN 2 THEN 'Medio' ELSE 'Alto' END, d.Persona, d.Descripcion," +
                          "d.Area, Areas.Descripcion as AreaDesc, d.FechaCompromiso, d.Estatus, Estados.Descripcion as EstatusDesc, d.Usuario, d.UsuarioMod, d.FechaMod" +
                          " FROM ([" + Tabla + "] as d LEFT JOIN SysCatalogos as Tipos ON d.Tipo=Tipos.Id" +
                          " LEFT JOIN SysCatalogos as Areas ON d.Area=Areas.Id) LEFT JOIN SysCatalogos as Estados ON d.Estatus=Estados.Id" +
                          strFiltro + " ORDER BY d.Fecha,d.Folio";

            //obtener registros
            cnn.Conectar();
            DataTable Registro = cnn.GetTabla(strConsulta);
            cnn.Dispose();
            return Registro;
        }

    }
}
