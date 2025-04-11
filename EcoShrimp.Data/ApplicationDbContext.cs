using EcoShrimp.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace EcoShrimp.Data
{
	public class ApplicationDbContext : DbContext
	{
		public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

		public DbSet<AppConnects> AppConnects { get; set; }
		public DbSet<AppDataSensor> AppDataSensor { get; set; }
		public DbSet<AppProInstances> AppProInstances { get; set; }
		public DbSet<AppFarms> AppFarms { get; set; }
		public DbSet<AppImges> AppImges { get; set; }
		public DbSet<AppPonds> AppPonds { get; set; }
		public DbSet<AppCategories> AppCategories { get; set; }
		public DbSet<AppProducts> AppProducts { get; set; }
		public DbSet<AppSeasons> AppSeasons { get; set; }
		public DbSet<AppRequests> AppRequests { get; set; }
		public DbSet<AppCateNews> AppCateNews { get; set; }
		public DbSet<AppNews> AppNews { get; set; }
		public DbSet<AppTimeIntervals> AppTimeIntervals { get; set; }
		public DbSet<AppUsers> AppUsers { get; set; }
		public DbSet<AppRoles> AppRoles { get; set; }
		public DbSet<AppRolePermission> AppRolePermission { get; set; }
		public DbSet<AppPermissions> AppPermissions { get; set; }
		public DbSet<AppShrimp> AppShrimps { get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder)  // Fluent API
		{
			base.OnModelCreating(modelBuilder);
			modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
		}
	}
}
