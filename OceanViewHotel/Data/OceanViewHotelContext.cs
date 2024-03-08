using Microsoft.EntityFrameworkCore;

namespace OceanViewHotel.Data
{
    public class OceanViewHotelContext : DbContext
    {
        public OceanViewHotelContext(DbContextOptions<OceanViewHotelContext> options)
            : base(options)
        {
        }

        public DbSet<OceanViewHotel.Models.Camera> Camere { get; set; } = default!;
        public DbSet<OceanViewHotel.Models.TipologiaCamera> TipologieCamera { get; set; } = default!;
        public DbSet<OceanViewHotel.Models.Cliente> Clienti { get; set; } = default!;
        public DbSet<OceanViewHotel.Models.Prenotazione> Prenotazioni { get; set; } = default!;
        public DbSet<OceanViewHotel.Models.Servizio> Servizi { get; set; } = default!;
        public DbSet<OceanViewHotel.Models.Dipendente> Dipendenti { get; set; } = default!;
        public DbSet<OceanViewHotel.Models.ServPerPren> ServPerPrenList { get; set; } = default!;
        public DbSet<OceanViewHotel.Models.Pensione> Pensioni { get; set; } = default!;

    }
}
