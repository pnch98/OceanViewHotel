using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OceanViewHotel.Models
{
    [Table("Pensioni")]
    public class Pensione
    {
        [Key]
        public int Id { get; set; }
        public string Descrizione { get; set; }
        public double Costo { get; set; }
        public virtual ICollection<Prenotazione> Prenotazioni { get; set; }
    }
}
