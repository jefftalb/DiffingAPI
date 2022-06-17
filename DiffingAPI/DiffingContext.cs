using Microsoft.EntityFrameworkCore;

namespace DiffingAPI {
  public class DiffingContext : DbContext {
    public DiffingContext(DbContextOptions<DiffingContext> options) : base(options) { }
    public DbSet<Diff> Diffs { get; set; } = null!;
  }
}
