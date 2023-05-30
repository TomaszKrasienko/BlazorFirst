using System;
using Microsoft.EntityFrameworkCore;

namespace BlazorWebFullstackCrud.Server.Data
{
	public class DataContext : DbContext
	{
        public DbSet<Comic> Comics { get; set; }
        public DbSet<SuperHero> SuperHeroes { get; set; }
        public DataContext()
        {

        }
        public DataContext(DbContextOptions<DataContext> options) : base(options)
		{
		}
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Comic>().HasData(
                new Comic() { Id = 1, Name = "Marvel" },
                new Comic() { Id = 2, Name = "DC" }
                );
            modelBuilder.Entity<SuperHero>().HasData(
                new SuperHero() { Id = 1, FirstName = "Peter", LastName = "Parker", HeroName = "Spiderman", ComicId = 1 },
                new SuperHero() { Id = 2, FirstName = "Bruce", LastName = "Wayne", HeroName = "Batman", ComicId = 2 }
                ); 
            base.OnModelCreating(modelBuilder);
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }
    }
}

