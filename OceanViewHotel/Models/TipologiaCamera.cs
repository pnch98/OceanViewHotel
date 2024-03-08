using System.ComponentModel.DataAnnotations.Schema;

namespace OceanViewHotel.Models
{
    [Table("TipologieCamera")]
    public class TipologiaCamera
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double Costo { get; set; }
        public virtual ICollection<Camera> Camere { get; set; }
    }
}
