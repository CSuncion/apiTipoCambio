using Microsoft.EntityFrameworkCore;

namespace ApiTipoCambio.Model
{
    public class TipoCambioDbContext : DbContext
    {
        public TipoCambioDbContext(DbContextOptions options) : base(options)
        {

        }
        public DbSet<TipoCambio> TipoCambio { get; set; }
        public DbSet<UserDto> User { get; set; }
    }
}
