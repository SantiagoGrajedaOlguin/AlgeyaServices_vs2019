using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Idealsis.Negocio
{
    public class PersonaDetalle
    {
        int    id;
        int    idOrigen;
        short  tipo;
        short  posicion;
        string codigo;
        string descripcion;
        int    idPersona;
        int    idCatalogo;
        int    idDato;
        float  valor;
        string notas;
        byte   bandera;

        public int Id { get { return id; } set { id = value; } }
        public int IdOrigen { get { return idOrigen; } set { idOrigen = value; } }
        public short Tipo { get { return tipo; } set { tipo = value; } }
        public short Posicion { get { return posicion; } set { posicion = value; } }
        public string Codigo { get { return codigo; } set { codigo = value; } }
        public string Descripcion { get { return descripcion; } set { descripcion = value; } }
        public int IdPersona { get { return idPersona; } set { idPersona = value; } }
        public int IdCatalogo { get { return idCatalogo; } set { idCatalogo = value; } }
        public int IdDato { get { return idDato; } set { idDato = value; } }
        public float Valor { get { return valor; } set { valor = value; } }
        public string Notas { get { return notas; } set { notas = value; } }
        public byte Bandera { get { return bandera; } set { bandera = value; } }
    }
}
