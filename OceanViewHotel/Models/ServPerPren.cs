using System.ComponentModel.DataAnnotations;

namespace OceanViewHotel.Models
{
    public class ServPerPren
    {
        [Key]
        public int Id { get; set; }
        public string Descrizione { get; set; }
        public double Costo { get; set; }
        public virtual ICollection<Servizio> Servizi { get; set; }
    }
}
