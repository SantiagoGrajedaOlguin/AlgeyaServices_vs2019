
using System;
using System.Data;

namespace Idealsis.Dal.Mapeo
{
    public class CatDirecciones
    {
        const string Tabla = "CatDirecciones";

        int id;
        byte origen;
        int idOrigen;
        short posicion;
        string rfc;
        string curp;
        string descripcion;
        string consignatario;
        int tipoIde;
        string numeroIde;
        int idParent;
        byte esPrincipal;
        byte paraEnvio;
        byte paraFactura;
        byte esReferencia;
        byte esAval;
        string idFiscalExt;
        int idPaisExt;
        string direccion;
        string noExterior;
        string noInterior;
        string referencia;
        int idPais;
        string pais;
        int idEstado;
        string estado;
        int idMunicipio;
        string municipio;
        int idPoblacion;
        string poblacion;
        int idColonia;
        string colonia;
        string cp;
        string telefono;
        string telefono2;
        string fax;
        string email;
        string emailPagos;
        string whatsApp;
        int viaDeEnvio;
        byte tipoDePersona;
        int idEmail;
        string regimenFiscal;


        public int Id { get => id; set => id = value; }
        public byte Origen { get => origen; set => origen = value; }
        public int IdOrigen { get => idOrigen; set => idOrigen = value; }
        public short Posicion { get => posicion; set => posicion = value; }
        public string Rfc { get => rfc; set => rfc = value; }
        public string Curp { get => curp; set => curp = value; }
        public string Descripcion { get => descripcion; set => descripcion = value; }
        public string Consignatario { get => consignatario; set => consignatario = value; }
        public int TipoIde { get => tipoIde; set => tipoIde = value; }
        public string NumeroIde { get => numeroIde; set => numeroIde = value; }
        public int IdParent { get => idParent; set => idParent = value; }
        public byte EsPrincipal { get => esPrincipal; set => esPrincipal = value; }
        public byte ParaEnvio { get => paraEnvio; set => paraEnvio = value; }
        public byte ParaFactura { get => paraFactura; set => paraFactura = value; }
        public byte EsReferencia { get => esReferencia; set => esReferencia = value; }
        public byte EsAval { get => esAval; set => esAval = value; }
        public string IdFiscalExt { get => idFiscalExt; set => idFiscalExt = value; }
        public int IdPaisExt { get => idPaisExt; set => idPaisExt = value; }
        public string Direccion { get => direccion; set => direccion = value; }
        public string NoExterior { get => noExterior; set => noExterior = value; }
        public string NoInterior { get => noInterior; set => noInterior = value; }
        public string Referencia { get => referencia; set => referencia = value; }
        public int IdPais { get => idPais; set => idPais = value; }
        public string Pais { get => pais; set => pais = value; }
        public int IdEstado { get => idEstado; set => idEstado = value; }
        public string Estado { get => estado; set => estado = value; }
        public int IdMunicipio { get => idMunicipio; set => idMunicipio = value; }
        public string Municipio { get => municipio; set => municipio = value; }
        public int IdPoblacion { get => idPoblacion; set => idPoblacion = value; }
        public string Poblacion { get => poblacion; set => poblacion = value; }
        public int IdColonia { get => idColonia; set => idColonia = value; }
        public string Colonia { get => colonia; set => colonia = value; }
        public string Cp { get => cp; set => cp = value; }
        public string Telefono { get => telefono; set => telefono = value; }
        public string Telefono2 { get => telefono2; set => telefono2 = value; }
        public string Fax { get => fax; set => fax = value; }
        public string Email { get => email; set => email = value; }
        public string EmailPagos { get => emailPagos; set => emailPagos = value; }
        public string WhatsApp { get => whatsApp; set => whatsApp = value; }
        public int ViaDeEnvio { get => viaDeEnvio; set => viaDeEnvio = value; }
        public byte TipoDePersona { get => tipoDePersona; set => tipoDePersona = value; }
        public int IdEmail { get => idEmail; set => idEmail = value; }
        public string RegimenFiscal { get => regimenFiscal; set => regimenFiscal = value; }

        public int Guardar(bool Buscar)
        {
            string c;
            string v;
            var cnn = new Conexion();

            cnn.Conectar();
            if (Buscar) id = Helper.Val(cnn.DameValor(Tabla, "Origen=" + origen + " and IdOrigen=" + idOrigen, "Id"));

            c = "Id,0,9|Origen|IdOrigen|Posicion|Rfc,1|Curp,1|Descripcion,1|Consignatario,1|TipoIde|NumeroIde,1|IdParent|EsPrincipal|ParaEnvio|ParaFactura|EsReferencia|EsAval|";
            c = c + "IdFiscalExt,1|IdPaisExt|Direccion,1|NoExterior,1|NoInterior,1|Referencia,1|IdPais|Pais,1|IdEstado|Estado,1|IdMunicipio|Municipio,1|IdPoblacion|Poblacion,1|IdColonia|Colonia,1|";
            c = c + "Cp,1|Telefono,1|Telefono2,1|Fax,1|Email,1|EmailPagos,1|WhatsApp,1|ViaDeEnvio|TipoDePersona|IdEmail|RegimenFiscal,1";

            v = id + "|" + origen + "|" + idOrigen + "|" + posicion + "|" + rfc + "|" + curp + "|" + descripcion + "|" + consignatario + "|" + tipoIde + "|" + numeroIde + "|" + idParent + "|" + esPrincipal + "|" + paraEnvio + "|" + paraFactura + "|" + esReferencia + "|" + esAval + "|";
            v = v + idFiscalExt + "|" + idPaisExt + "|" + direccion + "|" + noExterior + "|" + noInterior + "|" + referencia + "|" + idPais + "|" + pais + "|" + idEstado + "|" + estado + "|" + idMunicipio + "|" + municipio + "|" + idPoblacion + "|" + poblacion + "|" + idColonia + "|" + colonia + "|";
            v = v + cp + "|" + telefono + "|" + telefono2 + "|" + fax + "|" + email + "|" + emailPagos + "|" + whatsApp + "|" + viaDeEnvio + "|" + tipoDePersona + "|" + idEmail + "|" + regimenFiscal;

            id = cnn.GuardarRegistro(Tabla, c, v);

            //si es direccion de persona
            if (Origen==1) cnn.Exec("UPDATE CatPersonas SET IdDireccion=" + id + " WHERE Id=" + IdOrigen);
            cnn.Dispose();
            return id;
        }

        private void AsignarValores(IDataReader Registro)
        {
            id = Convert.ToInt32(Convert.IsDBNull(Registro["Id"]) ? 0 : Registro["Id"]);
            origen = Convert.ToByte(Convert.IsDBNull(Registro["Origen"]) ? 0 : Registro["Origen"]);
            idOrigen = Convert.ToInt32(Convert.IsDBNull(Registro["IdOrigen"]) ? 0 : Registro["IdOrigen"]);
            posicion = Convert.ToInt16(Convert.IsDBNull(Registro["Posicion"]) ? 0 : Registro["Posicion"]);
            rfc = Convert.IsDBNull(Registro["Rfc"]) ? "" : Registro["Rfc"].ToString();
            curp = Convert.IsDBNull(Registro["Curp"]) ? "" : Registro["Curp"].ToString();
            descripcion = Convert.IsDBNull(Registro["Descripcion"]) ? "" : Registro["Descripcion"].ToString();
            consignatario = Convert.IsDBNull(Registro["Consignatario"]) ? "" : Registro["Consignatario"].ToString();
            tipoIde = Convert.ToInt32(Convert.IsDBNull(Registro["TipoIde"]) ? 0 : Registro["TipoIde"]);
            numeroIde = Convert.IsDBNull(Registro["NumeroIde"]) ? "" : Registro["NumeroIde"].ToString();
            idParent = Convert.ToInt32(Convert.IsDBNull(Registro["IdParent"]) ? 0 : Registro["IdParent"]);
            esPrincipal = Convert.ToByte(Convert.IsDBNull(Registro["EsPrincipal"]) ? 0 : Registro["EsPrincipal"]);
            paraEnvio = Convert.ToByte(Convert.IsDBNull(Registro["ParaEnvio"]) ? 0 : Registro["ParaEnvio"]);
            paraFactura = Convert.ToByte(Convert.IsDBNull(Registro["ParaFactura"]) ? 0 : Registro["ParaFactura"]);
            esReferencia = Convert.ToByte(Convert.IsDBNull(Registro["EsReferencia"]) ? 0 : Registro["EsReferencia"]);
            esAval = Convert.ToByte(Convert.IsDBNull(Registro["EsAval"]) ? 0 : Registro["EsAval"]);
            idFiscalExt = Convert.IsDBNull(Registro["IdFiscalExt"]) ? "" : Registro["IdFiscalExt"].ToString();
            idPaisExt = Convert.ToInt32(Convert.IsDBNull(Registro["IdPaisExt"]) ? 0 : Registro["IdPaisExt"]);
            direccion = Convert.IsDBNull(Registro["Direccion"]) ? "" : Registro["Direccion"].ToString();
            noExterior = Convert.IsDBNull(Registro["NoExterior"]) ? "" : Registro["NoExterior"].ToString();
            noInterior = Convert.IsDBNull(Registro["NoInterior"]) ? "" : Registro["NoInterior"].ToString();
            referencia = Convert.IsDBNull(Registro["Referencia"]) ? "" : Registro["Referencia"].ToString();
            idPais = Convert.ToInt32(Convert.IsDBNull(Registro["IdPais"]) ? 0 : Registro["IdPais"]);
            Pais = Convert.IsDBNull(Registro["Pais"]) ? "" : Registro["Pais"].ToString();
            idEstado = Convert.ToInt32(Convert.IsDBNull(Registro["IdEstado"]) ? 0 : Registro["IdEstado"]);
            estado = Convert.IsDBNull(Registro["Estado"]) ? "" : Registro["Estado"].ToString();
            idMunicipio = Convert.ToInt32(Convert.IsDBNull(Registro["IdMunicipio"]) ? 0 : Registro["IdMunicipio"]);
            municipio = Convert.IsDBNull(Registro["Municipio"]) ? "" : Registro["Municipio"].ToString();
            idPoblacion = Convert.ToInt32(Convert.IsDBNull(Registro["IdPoblacion"]) ? 0 : Registro["IdPoblacion"]);
            poblacion = Convert.IsDBNull(Registro["Poblacion"]) ? "" : Registro["Poblacion"].ToString();
            idColonia = Convert.ToInt32(Convert.IsDBNull(Registro["IdColonia"]) ? 0 : Registro["IdColonia"]);
            colonia = Convert.IsDBNull(Registro["Colonia"]) ? "" : Registro["Colonia"].ToString();
            cp = Convert.IsDBNull(Registro["Cp"]) ? "" : Registro["Cp"].ToString();
            telefono = Convert.IsDBNull(Registro["Telefono"]) ? "" : Registro["Telefono"].ToString();
            telefono2 = Convert.IsDBNull(Registro["Telefono2"]) ? "" : Registro["Telefono2"].ToString();
            fax = Convert.IsDBNull(Registro["Fax"]) ? "" : Registro["Fax"].ToString();
            email = Convert.IsDBNull(Registro["Email"]) ? "" : Registro["Email"].ToString();
            emailPagos = Convert.IsDBNull(Registro["EmailPagos"]) ? "" : Registro["EmailPagos"].ToString();
            whatsApp = Convert.IsDBNull(Registro["WhatsApp"]) ? "" : Registro["WhatsApp"].ToString();
            viaDeEnvio = Convert.ToInt32(Convert.IsDBNull(Registro["ViaDeEnvio"]) ? 0 : Registro["ViaDeEnvio"]);
            TipoDePersona = Convert.ToByte(Convert.IsDBNull(Registro["TipoDePersona"]) ? 0 : Registro["TipoDePersona"]);
            IdEmail = Convert.ToInt32(Convert.IsDBNull(Registro["IdEmail"]) ? 0 : Registro["IdEmail"]);
            regimenFiscal = Convert.IsDBNull(Registro["RegimenFiscal"]) ? "" : Registro["RegimenFiscal"].ToString();
            Registro.Dispose();
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

        public bool BuscarPorOrigen(byte Origen, int IdOrigen)
        {
            var cnn = new Conexion();
            bool result = false;
            cnn.Conectar();
            IDataReader Registro = cnn.ObtenerRegistro(Tabla, "Origen=" + Origen + " and IdOrigen=" + IdOrigen);
            if (Registro.Read())
            {
                result = true;
                AsignarValores(Registro);
            }
            Registro.Dispose();
            cnn.Dispose();
            return result;
        }

        public bool BorrarPorId(int vId)
        {
            var cnn = new Conexion();
            bool Respuesta;
            cnn.Conectar();
            Respuesta = cnn.BorrarRegistro(Tabla, "Id=" + vId, false);
            cnn.Dispose();
            return Respuesta;
        }

        public DataTable ListarTabla(byte Origen, int IdOrigen)
        {
            var cnn = new Conexion();
            string strFiltro;
            string strConsulta;

            //generar consulta SQL
            strFiltro = " WHERE Origen=" + Origen + " and IdOrigen=" + IdOrigen;
            strConsulta = "SELECT * FROM [" + Tabla + "]" + strFiltro + " ORDER BY Posicion";

            //obtener registros
            cnn.Conectar();
            DataTable Registro = cnn.GetTabla(strConsulta);
            cnn.Dispose();
            return Registro;
        }

    }
}
