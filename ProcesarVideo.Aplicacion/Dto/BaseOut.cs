
using System.Net;
using Videos.Aplicacion.Enum;

namespace Videos.Aplicacion.Dto
{
    public class BaseOut
    {
        public Resultado Resultado { get; set; }
        public string Mensaje { get; set; }
        public HttpStatusCode Status { get; set; }
    }
}
