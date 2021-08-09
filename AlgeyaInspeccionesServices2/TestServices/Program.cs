using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestServices
{
    class Program
    {
        static void Main(string[] args)
        {
            Sinc_Local.SincronizacionClient client = new Sinc_Local.SincronizacionClient();
            client.Sincronizar("{cuerpo}", "{detalle}", "{calidades}", "{observaciones}", "{detalleObservaciones}");
        }
    }
}
