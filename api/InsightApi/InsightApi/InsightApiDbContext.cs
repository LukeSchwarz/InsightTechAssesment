using InsightApi.Models;
using Microsoft.EntityFrameworkCore;

namespace InsightApi
{
	public class InsightApiDbContext : DbContext
	{
		public InsightApiDbContext(DbContextOptions<InsightApiDbContext> options) : base(options)
		{
		}

		public virtual DbSet<Account> Account { get; set; }

		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			base.OnConfiguring(optionsBuilder);
		}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<Account>()
				.Property(a => a.OpenStatus)
				.HasConversion<string>();

			modelBuilder.Entity<Account>()
				.Property(a => a.AccountOwnership)
				.HasConversion<string>();
		}
	}
}
