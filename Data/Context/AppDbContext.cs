using BudgetApp.Data.Components;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BudgetApp.Data.Context;

public class AppDbContext : IdentityDbContext<User, IdentityRole<Guid>, Guid>
{
	public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

	public DbSet<Category> Categories { get; set; }
	public DbSet<Transaction> Transactions { get; set; }
	public DbSet<Details> Details { get; set; }

	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		base.OnModelCreating(modelBuilder);

		// Relationships
		modelBuilder.Entity<Transaction>()
			.HasOne(t => t.User)
			.WithMany(u => u.Transactions)
			.HasForeignKey(t => t.UserId);

		modelBuilder.Entity<Transaction>()
			.HasOne(t => t.Category)
			.WithMany(c => c.Transactions)
			.HasForeignKey(t => t.CategoryId);

		modelBuilder.Entity<Details>()
			.HasOne(d => d.Transaction)
			.WithMany(t => t.Details)
			.HasForeignKey(d => d.TransactionId);
	}
}
