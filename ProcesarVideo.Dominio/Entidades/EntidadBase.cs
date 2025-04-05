using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Videos.Dominio.Entidades
{
    public abstract class EntidadBase
    {
        [Key]
        [Column("id")]
        public Guid Id { get; set; }

        [Column("fechacreacion", TypeName = "timestamp(6)")]
        public DateTime FechaCreacion { get; set; }

        [Column("fechaactualizacion", TypeName = "timestamp(6)")]
        public DateTime? FechaActualizacion { get; set; }
    }
}
