using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AppInspeccionServicios.model
{
    public class Articulo
    {
        public int Id { get; set; }
		public string Descripcion { get; set; }
		public string Corta { get; set; }
		public int Grupo { get; set; }
		public string GrupoDesc { get; set; }
		public int UMedida { get; set; }
		public string UMedidaDesc { get; set; }
		public decimal Costo { get; set; }
		public decimal Existencia { get; set; }
	}
}
