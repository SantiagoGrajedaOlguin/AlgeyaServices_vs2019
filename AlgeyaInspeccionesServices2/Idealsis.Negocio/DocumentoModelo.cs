using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Idealsis.Negocio
{
    public class ImgPreviewConfigDTO
    {
        public string type { set; get; }
        public string caption { set; get; }
        public string cardUrl { set; get; }
        public string downloadUrl { set; get; }
        public string zoomData { set; get; }
        public long size { set; get; }
        public int key { set; get; }
    }

    public class DocumentoModelo
    {
        int    codigo;
        string descripcion;
        int    tipoAuditoria;
        int    tipoEntidad;
        byte   origenDatos; // ComboBox; 0=Auditoria, 1=Hallazgos
        byte   activo;

        public int Codigo { get { return codigo; } set { codigo = value; } }
        public string Descripcion { get { return descripcion; } set { descripcion = value; } }
        public int TipoAuditoria { get { return tipoAuditoria; } set { tipoAuditoria = value; } }
        public int TipoEntidad { get { return tipoEntidad; } set { tipoEntidad = value; } }
        public byte OrigenDatos { get { return origenDatos; } set { origenDatos = value; } }
        public byte Activo { get { return activo; } set { activo = value; } }

        public ImgPreviewConfigDTO filePreview { set; get; }
    }
}
