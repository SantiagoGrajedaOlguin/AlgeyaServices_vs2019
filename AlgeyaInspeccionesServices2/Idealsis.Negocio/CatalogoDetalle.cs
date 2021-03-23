using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Idealsis.Negocio
{
    public class CatalogoDetalle
    {
        int     id;
        int     idOrigen;
        short   tipo;
        short   posicion;
        int     idCatalogo;
        int     idDato;
        int     idPersona;
        int     idArticulo;
        string  descripcion;
        decimal cantidad;
        float   valor;
        string  texto;
        string  notas;
        byte    esRequerido;

        public int Id { get { return id; } set { id = value; } }
        public int IdOrigen { get { return idOrigen; } set { idOrigen = value; } }
        public short Tipo { get { return tipo; } set { tipo = value; } }
        public short Posicion { get { return posicion; } set { posicion = value; } }
        public int IdCatalogo { get { return idCatalogo; } set { idCatalogo = value; } }
        public int IdDato { get { return idDato; } set { idDato = value; } }
        public int IdPersona { get { return idPersona; } set { idPersona = value; } }
        public int IdArticulo { get { return idArticulo; } set { idArticulo = value; } }
        public string Descripcion { get { return descripcion; } set { descripcion = value; } }
        public decimal Cantidad { get { return cantidad; } set { cantidad = value; } }
        public float Valor { get { return valor; } set { valor = value; } }
        public string Texto { get { return texto; } set { texto = value; } }
        public string Notas { get { return notas; } set { notas = value; } }
        public byte EsRequerido { get { return esRequerido; } set { esRequerido = value; } }
    }
}
