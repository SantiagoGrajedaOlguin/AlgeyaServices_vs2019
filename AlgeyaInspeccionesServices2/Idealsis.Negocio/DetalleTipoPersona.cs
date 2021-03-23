using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Idealsis.Negocio
{
    public class DetalleTipoPersona
    {
        int    id;
        int    idOrigen;
        short  posicion;
        int    idDato;
        string descripcion;
        byte   esRequerido;
        Dato   dato;

        public int    Id { get { return id; } set { id = value; } }
        public int    IdOrigen { get { return idOrigen; } set { idOrigen = value; } }
        public short  Posicion { get { return posicion; } set { posicion = value; } }
        public int    IdDato { get { return idDato; } set { idDato = value; } }
        public string Descripcion { get { return descripcion; } set { descripcion = value; } }
        public byte   EsRequerido { get { return esRequerido; } set { esRequerido = value; } }
        public Dato   Dato { get { return dato; } set { dato = value; } }
    }
}
