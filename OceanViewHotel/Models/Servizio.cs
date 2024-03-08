using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OceanViewHotel.Models
{
    public class Servizio
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public DateTime DataServizio { get; set; } = DateTime.Today;
        [ForeignKey("Prenotazione")]
        public int IdPrenotazione { get; set; }
        [ForeignKey("ServPerPren")]
        public int IdServizio { get; set; }
        public virtual Prenotazione Prenotazione { get; set; }
        public virtual ServPerPren ServPerPren { get; set; }
    }
}
