using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OceanViewHotel.Models
{
    public class Prenotazione
    {
        public int Id { get; set; }
        public DateOnly DataPrenotazione { get; set; } = DateOnly.FromDateTime(DateTime.Today);
        public DateOnly DataCheckIn { get; set; }
        public DateOnly DataCheckOut { get; set; }
        public double Caparra { get; set; }
        public double Tariffa { get; set; }
        [ForeignKey("Camera")]
        [Display(Name = "Camera")]
        public int IdCamera { get; set; }
        [ForeignKey("Cliente")]
        [Display(Name = "Cliente")]
        public int IdCliente { get; set; }
        [ForeignKey("Pensione")]
        [Display(Name = "Pensione")]
        public int IdPensione { get; set; }
        public virtual Cliente Cliente { get; set; }
        public virtual Pensione Pensione { get; set; }
        public virtual Camera Camera { get; set; }
        public virtual ICollection<Servizio> Servizi { get; set; }
    }
}
