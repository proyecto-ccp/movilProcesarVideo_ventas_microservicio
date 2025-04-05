using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Videos.Aplicacion.Dto
{
    public class VideoDto
    {
        public Guid Id { get; set; }
        public Guid IdCliente { get; set; }
        public int IdProducto { get; set; }
        public string Nombre { get; set; }
        public string UrlVideo { get; set; }
        public string UrlImagen { get; set; }
        public string EstadoCarga { get; set; }
    }

    public class VideoOut : BaseOut
    {
        public VideoDto Video { get; set; }
    }

    public class VideoListOut : BaseOut
    {
        public List<VideoDto> Videos { get; set; }
    }
}
