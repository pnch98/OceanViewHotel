using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OceanViewHotel.Models
{
    public class Camera
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [ForeignKey("TipologiaCamera")]
        public int IdTipologiaCamera { get; set; }
        [Required]
        public int Numero { get; set; }
        public virtual TipologiaCamera TipologiaCamera { get; set; }
        public virtual ICollection<Prenotazione> Prenotazioni { get; set; }
    }
}

