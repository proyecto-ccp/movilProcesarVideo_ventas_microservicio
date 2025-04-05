
using System.ComponentModel.DataAnnotations.Schema;

namespace Videos.Dominio.Entidades
{
    [Table("tbl_video")]
    public class Video : EntidadBase
    {
        [Column("idcliente")]
        public Guid IdCliente { get; set; }

        [Column("idproducto")]
        public int IdProducto { get; set; }

        [Column("nombre")]
        public string Nombre { get; set; }

        //[NotMapped]
        //public string Ruta { get; set; }

        [NotMapped]
        public string Archivo { get; set; }

        [Column("urlvideo")]
        public string UrlVideo { get; set; }

        [Column("urlimagen")]
        public string UrlImagen { get; set; }

        [Column("estadocarga")]
        public string EstadoCarga { get; set; }
    }
}
