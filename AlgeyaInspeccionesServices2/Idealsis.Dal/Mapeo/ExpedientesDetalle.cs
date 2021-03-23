using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace Idealsis.Dal.Mapeo
{
    public class ExpedientesDetalle
    {
        const string Tabla = "ExpedientesDetalle";

        int     id;
        int     idOrigen;
        byte    tipo;
        int     posicion;
        int     codigo;
        byte    esRequerido;
        int     dato;
        int     persona;
        int     plantilla;
        decimal valor;
        string  comentarios;
        string  notas;
        int     estatus;
        string  fechaEstatus;
        int     idArchivo;
        string  fecha;
        string  usuario;
        string  fechaMod;
        string  usuarioMod;
        int     numReq;
        int     numAdd;

        public int Id { get => id; set => id = value; }
        public int IdOrigen { get => idOrigen; set => idOrigen = value; }
        public byte Tipo { get => tipo; set => tipo = value; }
        public int Posicion { get => posicion; set => posicion = value; }
        public int Codigo { get => codigo; set => codigo = value; }
        public byte EsRequerido { get => esRequerido; set => esRequerido = value; }
        public int Dato { get => dato; set => dato = value; }
        public int Persona { get => persona; set => persona = value; }
        public int Plantilla { get => plantilla; set => plantilla = value; }
        public decimal Valor { get => valor; set => valor = value; }
        public string Comentarios { get => comentarios; set => comentarios = value; }
        public string Notas { get => notas; set => notas = value; }
        public int Estatus { get => estatus; set => estatus = value; }
        public string FechaEstatus { get => fechaEstatus; set => fechaEstatus = value; }
        public int IdArchivo { get => idArchivo; set => idArchivo = value; }
        public string Fecha { get => fecha; set => fecha = value; }
        public string Usuario { get => usuario; set => usuario = value; }
        public string FechaMod { get => fechaMod; set => fechaMod = value; }
        public string UsuarioMod { get => usuarioMod; set => usuarioMod = value; }
        public int NumReq { get => numReq; set => numReq = value; }
        public int NumAdd { get => numAdd; set => numAdd = value; }

        public int Guardar()
        {
            string c;
            string v;
            var cnn = new Conexion();

            cnn.Conectar();
            c = "Id,0,9|IdOrigen|Tipo|Posicion|Codigo|EsRequerido|Dato|Persona|Plantilla|Valor|";
            v = id + "|" + idOrigen + "|" + tipo + "|" + posicion + "|" + codigo + "|" + esRequerido + "|" + dato + "|" + persona + "|" + plantilla + "|" + valor + "|";
            c = c + "Comentarios,1|Notas,1|Estatus|FechaEstatus,2|IdArchivo|Fecha,2,2|FechaMod,2|Usuario,1,2|UsuarioMod,1|NumReq,0,2|NumAdd,0,2";
            v = v + comentarios + "|" + notas + "|" + estatus + "|" + fechaEstatus + "|" + idArchivo + "|" + fecha + "|" + fechaMod + "|" + usuario + "|" + usuarioMod + "|" + numReq + "|" + numAdd;
            id = cnn.GuardarRegistro(Tabla, c, v);
            cnn.Dispose();
            cnn = null;
            return Id;
        }

        private void AsignarValores(IDataReader Registro)
        {
            id = Convert.ToInt32(Registro["Id"].ToString());
            idOrigen = Convert.ToInt32(Convert.IsDBNull(Registro["IdOrigen"]) ? 0 : Registro["IdOrigen"]);
            tipo = Convert.ToByte(Convert.IsDBNull(Registro["Tipo"]) ? 0 : Registro["Tipo"]);
            posicion = Convert.ToInt32(Convert.IsDBNull(Registro["Posicion"]) ? 0 : Registro["Posicion"]);
            codigo = Convert.ToInt32(Convert.IsDBNull(Registro["Codigo"]) ? 0 : Registro["Codigo"]);
            esRequerido = Convert.ToByte(Convert.IsDBNull(Registro["EsRequerido"]) ? 0 : Registro["EsRequerido"]);
            dato = Convert.ToInt32(Convert.IsDBNull(Registro["Dato"]) ? 0 : Registro["Dato"]);
            persona = Convert.ToInt32(Convert.IsDBNull(Registro["Persona"]) ? 0 : Registro["Persona"]);
            plantilla = Convert.ToInt32(Convert.IsDBNull(Registro["Plantilla"]) ? 0 : Registro["Plantilla"]);
            valor = Convert.ToDecimal(Convert.IsDBNull(Registro["Valor"]) ? 0 : Registro["Valor"]);
            notas = Convert.IsDBNull(Registro["Notas"]) ? "" : Registro["Notas"].ToString();
            estatus = Convert.ToInt32(Convert.IsDBNull(Registro["Estatus"]) ? 0 : Registro["Estatus"]);
            fechaEstatus = Convert.IsDBNull(Registro["FechaEstatus"]) ? "" : Convert.ToDateTime(Registro["FechaEstatus"]).ToString("dd/MM/yyyy hh:mm:ss tt");
            idArchivo = Convert.ToInt32(Convert.IsDBNull(Registro["IdArchivo"]) ? 0 : Registro["IdArchivo"]);
            fecha = Convert.IsDBNull(Registro["Fecha"]) ? "" : Convert.ToDateTime(Registro["Fecha"]).ToString("dd/MM/yyyy hh:mm:ss tt");
            usuario = Convert.IsDBNull(Registro["Usuario"]) ? "" : Registro["Usuario"].ToString();
            fechaMod = Convert.IsDBNull(Registro["FechaMod"]) ? "" : Convert.ToDateTime(Registro["FechaMod"]).ToString("dd/MM/yyyy hh:mm:ss tt");
            usuarioMod = Convert.IsDBNull(Registro["UsuarioMod"]) ? "" : Registro["UsuarioMod"].ToString();
            numReq = Convert.ToInt32(Convert.IsDBNull(Registro["NumReq"]) ? 0 : Registro["NumReq"]);
            numAdd = Convert.ToInt32(Convert.IsDBNull(Registro["NumAdd"]) ? 0 : Registro["NumAdd"]);
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

        public bool Existe(int vIdOrigen, byte vTipo, short vPosicion)
        {
            var cnn = new Conexion();
            bool result = false;
            cnn.Conectar();
            result = cnn.ExisteRegistro(Tabla, "IdOrigen=" + vIdOrigen + " and Tipo=" + vTipo + " and Posicion=" + vPosicion, "Id");
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

        public bool BorrarPorPadre(int vId, bool BorrarPadre)
        {
            var cnn = new Conexion();
            bool Respuesta=true;
            cnn.Conectar();
            if (BorrarPadre) cnn.BorrarRegistro(Tabla, "Id=" + vId, false);
            cnn.Exec("DELETE FROM " + Tabla + " WHERE Tipo=14 and IdOrigen=" + vId);
            cnn.Dispose();
            return Respuesta;
        }

        public DataTable ListarTabla(int vIdOrigen, byte vTipo, int vPosicion, string vDescripcion)
        {
            var cnn = new Conexion();
            string strConsulta;
            string strFiltro;

            //generar consulta SQL
            strFiltro = " WHERE IdOrigen=" + vIdOrigen;
            if (vTipo > 0) strFiltro = strFiltro + " and Tipo=" + vTipo;
            if (vPosicion > 0) strFiltro = strFiltro + " and Posicion=" + vPosicion;
            if (vDescripcion.Length > 0) strFiltro = strFiltro + " and Descripcion LIKE " + cnn.nSt(vDescripcion);
            strConsulta = "SELECT * FROM [" + Tabla + "]" + strFiltro + " ORDER BY Posicion,Id";

            //obtener registros
            cnn.Conectar();
            DataTable Registro = cnn.GetTabla(strConsulta);
            cnn.Dispose();
            return Registro;
        }

        public DataTable ListarTablaDato(int vIdOrigen, byte vTipo, int vDato, string vDescripcion)
        {
            var cnn = new Conexion();
            string strConsulta;
            string strFiltro;

            //generar consulta SQL
            strFiltro = " WHERE IdOrigen=" + vIdOrigen;
            if (vTipo > 0) strFiltro = strFiltro + " and Tipo=" + vTipo;
            if (vDato > 0) strFiltro = strFiltro + " and Dato=" + vDato;
            if (vDescripcion.Length > 0) strFiltro = strFiltro + " and Descripcion LIKE " + cnn.nSt(vDescripcion);
            strConsulta = "SELECT * FROM [" + Tabla + "]" + strFiltro + " ORDER BY Posicion,Id";

            //obtener registros
            cnn.Conectar();
            DataTable Registro = cnn.GetTabla(strConsulta);
            cnn.Dispose();
            return Registro;
        }

        public DataTable ListarDetallePersona(int IdAuditoria, int TipoPersona)
        {
            var cnn = new Conexion();
            string strConsulta;
            string strFiltro;

            //generar consulta SQL
            strFiltro = " WHERE c.IdOrigen=" + IdAuditoria + " and c.Tipo=13 and c.Posicion=" + TipoPersona;
            strConsulta = "SELECT d.* FROM ExpedientesDetalle as c INNER JOIN ExpedientesDetalle as d ON (c.Id=d.IdOrigen) and (d.Tipo=14)" + strFiltro + " ORDER BY d.Posicion,d.Id";

            //obtener registros
            cnn.Conectar();
            DataTable Registro = cnn.GetTabla(strConsulta);
            cnn.Dispose();
            return Registro;
        }
    }
}
