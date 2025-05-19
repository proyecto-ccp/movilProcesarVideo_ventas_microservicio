using System.Diagnostics.CodeAnalysis;

namespace Videos.Aplicacion.Dto
{
    [ExcludeFromCodeCoverage]
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

    [ExcludeFromCodeCoverage]
    public class VideoOut : BaseOut
    {
        public VideoDto Video { get; set; }
    }

    [ExcludeFromCodeCoverage]
    public class VideoListOut : BaseOut
    {
        public List<VideoDto> Videos { get; set; }
    }
}
