using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AppInspeccionServicios.model
{
    public class Bodega
    {
		public int Id { get; set; }
		public string Descripcion { get; set; }
		public string Direccion { get; set; }
		public string NoExterior { get; set; }
		public string NoInterior { get; set; }
		public string Colonia { get; set; }
		public string Municipio { get; set; }
		public string Poblacion { get; set; }
		public int Cliente { get; set; }
		public string ClienteDesc { get; set; }
		public int Tipo { get; set; }
		public string TipoDesc { get; set; }

	}
}
