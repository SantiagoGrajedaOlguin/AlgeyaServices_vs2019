using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace Idealsis.Dal.Mapeo
{
    public class CatPersonas
    {
        const string Tabla = "CatPersonas";

        int    id;
        byte   tipoDePersona;
        int    idPadre;
        int    codigo;
        string descripcion;
        string apellidoPaterno;
        string apellidoMaterno;
        string nombres;
        string corta;
        string objetoSocial;
        string fechaConstitucion;
        string folioEscritura;
        string registroCondusef;
        int    agrupador;
        byte   sinAgrupador;
        byte   esAgrupador;
        byte   esTransportista;
        byte   esDistribuidor;
        byte   okObsequios;
        byte   requiereCCC;
        byte   tipoAdmin;
        int tipo;
        int sistema;
        int representante;
        int oficial;
        int zona;
        int ruta;
        int orden;
        byte reqOrden;
        byte reqRecepcion;
        byte reqIDeposito;
        int idQuienFactura;
        byte aplicaRedondeo;
        string descripcionFac;
        string esMoral;
        int idDireccion;
        string contactoVta;
        string contactoCob;
        string revisionPagos;
        short situacion;
        short accionLimiteExedido;
        short accionSaldoVencido;
        short moneda;
        int politica;
        int condicion;
        int cobrador;
        int vendedor;
        int diaCobranza;
        int diaRevision;
        short plazo;
        decimal limite;
        decimal limiteDisponible;
        byte creditoBloqueado;
        decimal saldo;
        decimal saldoVencido;
        byte preciosEnUsd;
        byte cobrarImpuestos;
        byte retenerImpuestos;
        short impuestoFijo;
        short retencionFija;
        string fechaDeAlta;
        byte activo;
        byte envioAutDeEmail;
        string usoDeCfdi;
        int adendaTipo;
        string adendaSucursal;
        string adendaProveedor;
        short complementoTipo;
        int idLogo;
        string numLicencia;
        string venLicencia;
        int tarifa;
        int numInsidencias;
        byte conBeneficiario;
        string serie;
        int folio;
        int puesto;
        byte esTercero;
        byte estaAPrueba;
        string inicioPrueba;
        short diasPrueba;
        byte sinComision;
        string fechaSinCom;
        string serieFactura;
        int comision;
        string password;
        byte verEnVentas;
        byte verEnCompras;
        byte reqAutorizacion;
        int codigoScd;
        byte esPred;
        string detalleJson;

        public int Id { get { return id; } set { id = value; } }
        public byte TipoDePersona { get { return tipoDePersona; } set { tipoDePersona = value; } }
        public int IdPadre { get { return idPadre; } set { idPadre = value; } }
        public int Codigo { get { return codigo; } set { codigo = value; } }
        public string Descripcion { get { return descripcion; } set { descripcion = value; } }
        public string Corta { get { return corta; } set { corta = value; } }
        //public string ApellidoPaterno { get => apellidoPaterno; set => apellidoPaterno = value; }
        //public string ApellidoMaterno { get => apellidoMaterno; set => apellidoMaterno = value; }
        //public string Nombres { get => nombres; set => nombres = value; }
        public string ObjetoSocial { get { return objetoSocial; } set { objetoSocial = value; } }
        public string FechaConstitucion { get { return fechaConstitucion; } set { fechaConstitucion = value; } }
        public string FolioEscritura { get { return folioEscritura; } set { folioEscritura = value; } }
        public string RegistroCondusef { get { return registroCondusef; } set { registroCondusef = value; } }

        //public int Agrupador { get => agrupador; set => agrupador = value; }
        //public byte SinAgrupador { get => sinAgrupador; set => sinAgrupador = value; }
        //public byte EsAgrupador { get => esAgrupador; set => esAgrupador = value; }
        //public byte EsTransportista { get => esTransportista; set => esTransportista = value; }
        //public byte EsDistribuidor { get => esDistribuidor; set => esDistribuidor = value; }
        //public byte OkObsequios { get => okObsequios; set => okObsequios = value; }

        public byte RequiereCCC { get => requiereCCC; set => requiereCCC  = value; }
        public byte TipoAdmin { get => tipoAdmin; set => tipoAdmin = value; }
        public int Tipo { get { return tipo; } set { tipo = value; } }
        public int Sistema { get { return sistema; } set { sistema = value; } }
        public int Representante { get { return representante; } set { representante = value; } }
        public int Oficial { get { return oficial; } set { oficial = value; } }

        //public int Zona { get => zona; set => zona = value; }
        //public int Ruta { get => ruta; set => ruta = value; }
        //public int Orden { get => orden; set => orden = value; }
        //public byte ReqOrden { get => reqOrden; set => reqOrden = value; }
        //public byte ReqRecepcion { get => reqRecepcion; set => reqRecepcion = value; }
        //public byte ReqIDeposito { get => reqIDeposito; set => reqIDeposito = value; }
        //public int IdQuienFactura { get => idQuienFactura; set => idQuienFactura = value; }
        //public byte AplicaRedondeo { get => aplicaRedondeo; set => aplicaRedondeo = value; }
        //public string DescripcionFac { get => descripcionFac; set => descripcionFac = value; }
        //public string EsMoral { get => esMoral; set => esMoral = value; }
        public int IdDireccion { get => idDireccion; set => idDireccion = value; }
        //public string ContactoVta { get => contactoVta; set => contactoVta = value; }
        //public string ContactoCob { get => contactoCob; set => contactoCob = value; }
        //public string RevisionPagos { get => revisionPagos; set => revisionPagos = value; }
        //public short Situacion { get => situacion; set => situacion = value; }
        //public short AccionLimiteExedido { get => accionLimiteExedido; set => accionLimiteExedido = value; }
        //public short AccionSaldoVencido { get => accionSaldoVencido; set => accionSaldoVencido = value; }
        //public short Moneda { get => moneda; set => moneda = value; }
        //public int Politica { get => politica; set => politica = value; }
        //public int Condicion { get => condicion; set => condicion = value; }
        //public int Cobrador { get => cobrador; set => cobrador = value; }
        //public int Vendedor { get => vendedor; set => vendedor = value; }
        //public int DiaCobranza { get => diaCobranza; set => diaCobranza = value; }
        //public int DiaRevision { get => diaRevision; set => diaRevision = value; }
        //public short Plazo { get => plazo; set => plazo = value; }
        //public decimal Limite { get => limite; set => limite = value; }
        //public decimal LimiteDisponible { get => limiteDisponible; set => limiteDisponible = value; }
        //public byte CreditoBloqueado { get => creditoBloqueado; set => creditoBloqueado = value; }
        //public decimal Saldo { get => saldo; set => saldo = value; }
        //public decimal SaldoVencido { get => saldoVencido; set => saldoVencido = value; }
        //public byte PreciosEnUsd { get => preciosEnUsd; set => preciosEnUsd = value; }
        //public byte CobrarImpuestos { get => cobrarImpuestos; set => cobrarImpuestos = value; }
        //public byte RetenerImpuestos { get => retenerImpuestos; set => retenerImpuestos = value; }
        //public short ImpuestoFijo { get => impuestoFijo; set => impuestoFijo = value; }
        //public short RetencionFija { get => retencionFija; set => retencionFija = value; }
        public string FechaDeAlta { get { return fechaDeAlta; } set { fechaDeAlta = value; } }
        public byte Activo { get { return activo; } set { activo = value; } }
        //public byte EnvioAutDeEmail { get => envioAutDeEmail; set => envioAutDeEmail = value; }
        //public string UsoDeCfdi { get => usoDeCfdi; set => usoDeCfdi = value; }
        //public int AdendaTipo { get => adendaTipo; set => adendaTipo = value; }
        //public string AdendaSucursal { get => adendaSucursal; set => adendaSucursal = value; }
        //public string AdendaProveedor { get => adendaProveedor; set => adendaProveedor = value; }
        //public short ComplementoTipo { get => complementoTipo; set => complementoTipo = value; }
        //public int IdLogo { get => idLogo; set => idLogo = value; }
        //public string NumLicencia { get => numLicencia; set => numLicencia = value; }
        //public string VenLicencia { get => venLicencia; set => venLicencia = value; }
        //public int Tarifa { get => tarifa; set => tarifa = value; }
        //public int NumInsidencias { get => numInsidencias; set => numInsidencias = value; }
        //public byte ConBeneficiario { get => conBeneficiario; set => conBeneficiario = value; }
        //public string Serie { get => serie; set => serie = value; }
        //public int Folio { get => folio; set => folio = value; }
        public int Puesto { get => puesto; set => puesto = value; }
        //public byte EsTercero { get => esTercero; set => esTercero = value; }
        //public byte EstaAPrueba { get => estaAPrueba; set => estaAPrueba = value; }
        //public string InicioPrueba { get => inicioPrueba; set => inicioPrueba = value; }
        //public short DiasPrueba { get => diasPrueba; set => diasPrueba = value; }
        //public byte SinComision { get => sinComision; set => sinComision = value; }
        //public string FechaSinCom { get => fechaSinCom; set => fechaSinCom = value; }
        //public string SerieFactura { get => serieFactura; set => serieFactura = value; }
        //public int Comision { get => comision; set => comision = value; }
        //public string Password { get => password; set => password = value; }
        //public byte VerEnVentas { get => verEnVentas; set => verEnVentas = value; }
        //public byte VerEnCompras { get => verEnCompras; set => verEnCompras = value; }
        //public byte ReqAutorizacion { get => reqAutorizacion; set => reqAutorizacion = value; }
        //public int CodigoScd { get => codigoScd; set => codigoScd = value; }
        //public byte EsPred { get => esPred; set => esPred = value; }
        public string DetalleJson { get => detalleJson; set => detalleJson = value; }

        public CatPersonas()
        {
        }
        public CatPersonas(byte vTipoDePersona)
        {
            tipoDePersona = vTipoDePersona;
        }

        public int Guardar()
        {
            string c;
            string v;
            var cnn = new Conexion();

            cnn.Conectar();
            c = "Id,0,9|TipoDePersona|IdPadre|Codigo|Descripcion,1|Corta,1|ApellidoPaterno,1|ApellidoMaterno,1|Nombres,1|ObjetoSocial,1|FechaConstitucion,2|FolioEscritura,1|RegistroCondusef,1|Agrupador|SinAgrupador|EsAgrupador|EsTransportista|EsDistribuidor|OkObsequios|RequiereCCC|TipoAdmin|";
            c = c + "Tipo|Sistema|Zona|Ruta|orden|ReqOrden|ReqRecepcion|ReqIDeposito|IdQuienFactura|AplicaRedondeo|DescripcionFac,1|EsMoral,1|IdDireccion|ContactoVta,1|ContactoCob,1|RevisionPagos,1|";
            c = c + "Situacion|AccionLimiteExedido|AccionSaldoVencido|Moneda|Politica|Condicion|Cobrador|Vendedor|DiaCobranza|DiaRevision|Plazo|Limite|LimiteDisponible|CreditoBloqueado|Saldo|SaldoVencido|PreciosEnUsd|CobrarImpuestos|RetenerImpuestos|ImpuestoFijo|RetencionFija|";
            c = c + "FechaDeAlta,2,2|Activo|EnvioAutDeEmail|UsoDeCfdi,1|AdendaTipo|AdendaSucursal,1|AdendaProveedor,1|ComplementoTipo|IdLogo|NumLicencia,1|VenLicencia,2|Tarifa|NumInsidencias,0,2|ConBeneficiario|Serie,1|Folio|Puesto|Representante|Oficial|";
            c = c + "EsTercero|EstaAPrueba|InicioPrueba,2|DiasPrueba|SinComision|FechaSinCom,2|SerieFactura,1|Comision|Password,1|VerEnVentas|VerEnCompras|ReqAutorizacion|CodigoScd|EsPred";

            v = id + "|" + tipoDePersona + "|" + idPadre + "|" + codigo + "|" + descripcion + "|" + corta + "|" + apellidoPaterno + "|" + apellidoMaterno + "|" + nombres + "|" + objetoSocial+ "|" + fechaConstitucion + "|" + folioEscritura + "|" + registroCondusef + "|" + agrupador + "|" + sinAgrupador + "|" + esAgrupador + "|" + esTransportista + "|" + esDistribuidor + "|" + okObsequios + "|" + requiereCCC + "|" + tipoAdmin + "|";
            v = v + tipo + "|" + sistema + "|" + zona + "|" + ruta + "|" + orden + "|" + reqOrden + "|" + reqRecepcion + "|" + reqIDeposito + "|" + idQuienFactura + "|" + aplicaRedondeo + "|" + descripcionFac + "|" + esMoral + "|" + idDireccion + "|" + contactoVta + "|" + contactoCob + "|" + revisionPagos + "|";
            v = v + situacion + "|" + accionLimiteExedido + "|" + accionSaldoVencido + "|" + moneda + "|" + politica + "|" + condicion + "|" + cobrador + "|" + vendedor + "|" + diaCobranza + "|" + diaRevision + "|" + plazo + "|" + limite + "|" + limiteDisponible + "|" + creditoBloqueado + "|" + saldo + "|" + saldoVencido + "|" + preciosEnUsd + "|" + cobrarImpuestos + "|" + retenerImpuestos + "|" + impuestoFijo + "|" + retencionFija + "|";
            v = v + fechaDeAlta + "|" + activo + "|" + envioAutDeEmail + "|" + usoDeCfdi + "|" + adendaTipo + "|" + adendaSucursal + "|" + adendaProveedor + "|" + complementoTipo + "|" + idLogo + "|" + numLicencia + "|" + venLicencia + "|" + tarifa + "|" + numInsidencias + "|" + conBeneficiario + "|" + serie + "|" + folio + "|" + puesto + "|" + representante + "|" + oficial + "|";
            v = v + esTercero + "|" + estaAPrueba + "|" + inicioPrueba + "|" + diasPrueba + "|" + sinComision + "|" + fechaSinCom + "|" + serieFactura + "|" + comision + "|" + password + "|" + verEnVentas + "|" + verEnCompras + "|" + reqAutorizacion + "|" + codigoScd + "|" + esPred;

            id = cnn.GuardarRegistro(Tabla, c, v);

            cnn.Exec("DELETE FROM CatPersonasDetalle WHERE IdOrigen=" + id);
            /*
            if (detalleJson!=null)
            { 
                if (detalleJson.Length>0)
                {
                    //guardar detalle
                    cnn.EjecutaSp("GuardarPersonasDetalle", id, detalleJson);
                }
            }
            */
            cnn.Dispose();
            return id;
        }

        private void AsignarValores(IDataReader Registro)
        {
            id = Convert.ToInt32(Registro["Id"].ToString());
            tipoDePersona = Convert.ToByte(Convert.IsDBNull(Registro["TipoDePersona"]) ? 0 : Registro["TipoDePersona"]);
            idPadre = Convert.ToInt32(Convert.IsDBNull(Registro["IdPadre"]) ? 0 : Registro["IdPadre"]);
            codigo = Convert.ToInt32(Convert.IsDBNull(Registro["Codigo"]) ? 0 : Registro["Codigo"]);
            descripcion = Convert.IsDBNull(Registro["Descripcion"]) ? "" : Registro["Descripcion"].ToString();
            corta = Convert.IsDBNull(Registro["Corta"]) ? "" : Registro["Corta"].ToString();
            apellidoPaterno = Convert.IsDBNull(Registro["ApellidoPaterno"]) ? "" : Registro["ApellidoPaterno"].ToString();
            apellidoMaterno = Convert.IsDBNull(Registro["ApellidoMaterno"]) ? "" : Registro["ApellidoMaterno"].ToString();
            nombres = Convert.IsDBNull(Registro["Nombres"]) ? "" : Registro["Nombres"].ToString();
            objetoSocial = Convert.IsDBNull(Registro["ObjetoSocial"]) ? "" : Registro["ObjetoSocial"].ToString();
            fechaConstitucion = Convert.IsDBNull(Registro["FechaConstitucion"]) ? "" : Registro["FechaConstitucion"].ToString();
            folioEscritura = Convert.IsDBNull(Registro["FolioEscritura"]) ? "" : Registro["FolioEscritura"].ToString();
            registroCondusef = Convert.IsDBNull(Registro["RegistroCondusef"]) ? "" : Registro["RegistroCondusef"].ToString();
            requiereCCC = Convert.ToByte(Convert.IsDBNull(Registro["RequiereCCC"]) ? 0 : Registro["RequiereCCC"]);
            tipoAdmin = Convert.ToByte(Convert.IsDBNull(Registro["TipoAdmin"]) ? 0 : Registro["TipoAdmin"]);
            tipo = Convert.ToInt32(Convert.IsDBNull(Registro["Tipo"]) ? 0 : Registro["Tipo"]);
            puesto = Convert.ToInt32(Convert.IsDBNull(Registro["Puesto"]) ? 0 : Registro["Puesto"]);
            sistema = Convert.ToInt32(Convert.IsDBNull(Registro["Sistema"]) ? 0 : Registro["Sistema"]);
            representante = Convert.ToInt32(Convert.IsDBNull(Registro["Representante"]) ? 0 : Registro["Representante"]);
            oficial = Convert.ToInt32(Convert.IsDBNull(Registro["Oficial"]) ? 0 : Registro["Oficial"]);
            idDireccion = Convert.ToInt32(Convert.IsDBNull(Registro["IdDireccion"]) ? 0 : Registro["IdDireccion"]);
            activo = Convert.ToByte(Convert.IsDBNull(Registro["Activo"]) ? 0 : Registro["Activo"]);
        }
        public bool Desplegar(int Direccion, short vTipoDePersona, int vCodigo, int vId)
        {
            var cnn = new Conexion();
            bool Result = false;
            cnn.Conectar();
            IDataReader Registro = cnn.MoverPadre(Tabla, Direccion, vTipoDePersona, 0, 0, vCodigo, vId);
            if (Registro.Read())
            {
                Result = true;
                AsignarValores(Registro);
            }
            Registro.Dispose();
            cnn.Dispose();
            return Result;
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
        public bool BuscarPorCodigo(short vTipoDePersona, int vCodigo)
        {
            var cnn = new Conexion();
            bool result = false;
            cnn.Conectar();
            IDataReader Registro = cnn.ObtenerRegistro(Tabla, "TipoDePersona=" + vTipoDePersona + " and Codigo=" + vCodigo);
            if (Registro.Read())
            {
                result = true;
                AsignarValores(Registro);
            }
            Registro.Dispose();
            cnn.Dispose();
            return result;
        }

        public bool BuscarPorCodigo(short vTipoDePersona, int vIdPadre, int vCodigo)
        {
            var cnn = new Conexion();
            bool result = false;
            cnn.Conectar();
            IDataReader Registro = cnn.ObtenerRegistro(Tabla, "TipoDePersona=" + vTipoDePersona + (vIdPadre > 0 ? " and IdPadre=" + vIdPadre : "") + " and Codigo=" + vCodigo);
            if (Registro.Read())
            {
                result = true;
                AsignarValores(Registro);
            }
            Registro.Dispose();
            cnn.Dispose();
            return result;
        }
        public bool Existe(short vTipoDePersona, int vCodigo, string vDesc)
        {
            var cnn = new Conexion();
            bool result;
            cnn.Conectar();
            result = cnn.ExisteRegistro(Tabla, "TipoDePersona=" + vTipoDePersona + (vCodigo > 0 ? " and Codigo=" + vCodigo : "") + (vDesc.Length > 0 ? " and Descripcion='" + vDesc + "'" : ""), "Id");
            cnn.Dispose();
            return result;
        }

        public bool BuscarPorDesc(int vTipoDePersona, string vDescripcion)
        {
            var cnn = new Conexion();
            bool result = false;
            cnn.Conectar();
            IDataReader Registro = cnn.ObtenerRegistro(Tabla, "TipoDePersona = " + vTipoDePersona + " and Descripcion = " + cnn.strSql(vDescripcion));
            if (Registro.Read())
            {
                result = true;
                AsignarValores(Registro);
            }
            Registro.Dispose();
            cnn.Dispose();
            return result;
        }

        public int GetCod(int vId)
        {
            var cnn = new Conexion();
            int result;
            cnn.Conectar();
            result = Convert.ToInt32(cnn.DameValor(Tabla, "Id=" + vId, "Codigo"));
            cnn.Dispose();
            return result;
        }
        public int GetId(short vTipoDePersona, int vCodigo)
        {
            var cnn = new Conexion();
            int result;
            cnn.Conectar();
            result = Convert.ToInt32(cnn.DameValor(Tabla, "TipoDePersona=" + vTipoDePersona + " and Codigo=" + vCodigo, "Id"));
            cnn.Dispose();
            return result;
        }
        public string GetDes(int vId)
        {
            var cnn = new Conexion();
            string result;
            cnn.Conectar();
            result = cnn.DameValor(Tabla, "Id=" + vId, "Descripcion");
            cnn.Dispose();
            return result;
        }

        public string ObtenerValor(int vId, string Campo)
        {

            var cnn = new Conexion();
            cnn.Conectar();
            string result = cnn.DameValor(Tabla, "Id=" + vId, Campo);
            cnn.Dispose();
            return result;
        }

        public bool BorrarPorId(int vId, bool Preguntar)
        {
            var cnn = new Conexion();
            bool Respuesta;
            cnn.Conectar();
            Respuesta = cnn.BorrarRegistro(Tabla, "Id=" + vId, Preguntar);
            if (vId>0) cnn.Exec("DELETE FROM CatPersonas WHERE IdPadre=" + vId);
            cnn.Dispose();
            return Respuesta;
        }

        public DataTable Listar(byte vTipoDePersona, int vIdPadre, string vDescripcion)
        {
            var cnn = new Conexion();
            string strFiltro;
            string strConsulta;

            //generar consulta SQL
            strFiltro = " WHERE c.TipoDePersona=" + vTipoDePersona;
            if (vIdPadre>0) strFiltro = strFiltro  + " and c.IdPadre=" + vIdPadre;
            if (vDescripcion.Length > 0) strFiltro = strFiltro + " and c.Descripcion LIKE " + Helper.nSt(vDescripcion);
            strConsulta = "SELECT c.*,d.Rfc,d.Direccion,d.NoExterior,d.NoInterior,d.Referencia,d.Colonia,d.Cp,d.Pais,d.Estado,d.Municipio,d.Poblacion,d.Email,d.Telefono FROM CatPersonas as c LEFT JOIN CatDirecciones as d ON c.IdDireccion=d.Id " + strFiltro + " ORDER BY c.Descripcion";

            //obtener registros
            cnn.Conectar();
            DataTable Registro = cnn.GetTabla(strConsulta);
            cnn.Dispose();
            return Registro;
        }

        public int SiguienteCodigo(short vTipoDePersona)
        {
            int vCodigo;
            var cnn = new Conexion();
            cnn.Conectar();
            vCodigo = 1; if (cnn.ExisteRegistro(Tabla, "SELECT Max(Codigo) as c FROM [" + Tabla + "] WHERE TipoDePersona=" + vTipoDePersona, "c")) vCodigo = Convert.ToInt32(Helper.vg_DAT.Length > 0 ? Helper.vg_DAT : "0") + 1;
            cnn.Dispose();
            return vCodigo;
        }
    }
}
