
using System.ComponentModel.DataAnnotations.Schema;

namespace Videos.Dominio.Entidades
{
    [Table("tbl_video")]
    public class Video : EntidadBase
    {
        [NotMapped]
        public string Archivo { get; set; }

        [Column("nombre")]
        public string Nombre { get; set; }

        [Column("urlimagen")]
        public string UrlImagen { get; set; }

        [Column("estadocarga")]
        public string EstadoCarga { get; set; }
    }
}
