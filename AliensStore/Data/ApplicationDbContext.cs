using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using AliensStore.Data.Entity;

namespace AliensStore.Data
{
	public class ApplicationDbContext : IdentityDbContext
	{
		public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
			: base(options)
		{
		}
	    public DbSet<AliensStore.Data.Entity.Alien> Alien { get; set; } = default!;
	    public DbSet<AliensStore.Data.Entity.Planet> Planet { get; set; } = default!;
	    public DbSet<AliensStore.Data.Entity.Galaxy> Galaxy { get; set; } = default!;
	}
}
