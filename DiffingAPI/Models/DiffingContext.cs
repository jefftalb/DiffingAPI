using Microsoft.EntityFrameworkCore;

namespace DiffingAPI.Models {
  public class DiffingContext : DbContext {
    public DiffingContext(DbContextOptions<DiffingContext> options) : base(options) { }
    public DbSet<DataPair> DataPairs { get; set; } = null!;
  }
}
