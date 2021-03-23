using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Idealsis.Negocio
{
    public class Persona
    {
        int    id;
        byte   tipoDePersona;
        int    idPadre;
        int    codigo;
        string descripcion;
        string corta;
        byte   requiereCCC;
        byte   tipoAdmin;
        int    tipo;
        int    puesto;
        int    sistema;
        int    representante;
        int    oficial;
        string rfc;
        string objetoSocial;
        string fechaConstitucion;
        string folioEscritura;
        string registroCondusef;
        string direccion;
        string noExterior;
        string noInterior;
        string referencia;
        string colonia;
        string cp;
        string pais;
        string estado;
        string municipio;
        string poblacion;
        string email;
        string telefono;
        int    activo;
        string detalleJson;

        public int Id { get { return id; } set { id = value; } }
        public byte TipoDePersona { get { return tipoDePersona; } set { tipoDePersona = value; } }
        public int IdPadre { get { return idPadre; } set { idPadre = value; } }
        public int Codigo { get { return codigo; } set { codigo = value; } }
        public string Descripcion { get { return descripcion; } set { descripcion = value; } }
        public string Corta { get { return corta; } set { corta = value; } }
        public byte RequiereCCC { get { return requiereCCC; } set { requiereCCC = value; } }
        public byte TipoAdmin { get { return tipoAdmin; } set { tipoAdmin = value; } }
        public int Tipo { get { return tipo; } set { tipo = value; } }
        public int Puesto { get { return puesto; } set { puesto = value; } }
        public int Representante { get { return representante; } set { representante = value; } }
        public int Oficial { get { return oficial; } set { oficial = value; } }
        public int Sistema { get { return sistema; } set { sistema = value; } }
        public string Rfc { get { return rfc; } set { rfc = value; } }
        public string ObjetoSocial { get { return objetoSocial; } set { objetoSocial = value; } }
        public string FechaConstitucion { get { return fechaConstitucion; } set { fechaConstitucion = value; } }
        public string FolioEscritura { get { return folioEscritura; } set { folioEscritura = value; } }
        public string RegistroCondusef { get { return registroCondusef; } set { registroCondusef = value; } }
        public string Direccion { get { return direccion; } set { direccion = value; } }
        public string NoExterior { get { return noExterior; } set { noExterior = value; } }
        public string NoInterior { get { return noInterior; } set { noInterior = value; } }
        public string Referencia { get { return referencia; } set { referencia = value; } }
        public string Colonia { get { return colonia; } set { colonia = value; } }
        public string Cp { get { return cp; } set { cp = value; } }
        public string Pais { get { return pais; } set { pais = value; } }
        public string Estado { get { return estado; } set { estado = value; } }
        public string Municipio { get { return municipio; } set { municipio = value; } }
        public string Poblacion { get { return poblacion; } set { poblacion = value; } }
        public string Email { get { return email; } set { email = value; } }
        public string Telefono { get { return telefono; } set { telefono = value; } }
        public int Activo { get { return activo; } set { activo = value; } }
        public string DetalleJson { get { return detalleJson; } set { detalleJson = value; } }

    }
}
