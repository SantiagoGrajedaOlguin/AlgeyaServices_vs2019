using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Idealsis.Negocio
{
    public class DetalleEntidad
    {
        int    id;
        int    idOrigen;
        short  tipo;
        short  posicion;
        string descripcion;
        string texto;
        string notas;
        byte   esRequerido;

        public int Id { get { return id; } set { id = value; } }
        public int IdOrigen { get { return idOrigen; } set { idOrigen = value; } }
        public short Tipo { get { return tipo; } set { tipo = value; } }
        public short Posicion { get { return posicion; } set { posicion = value; } }
        public string Descripcion { get { return descripcion; } set { descripcion = value; } }
        public string Texto { get { return texto; } set { texto = value; } }
        public string Notas { get { return notas; } set { notas = value; } }
        public byte EsRequerido { get { return esRequerido; } set { esRequerido = value; } }
    }
}
