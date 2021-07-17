using ApiDisney.Models;
using Microsoft.EntityFrameworkCore;

namespace ApiDisney.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
        :base(options)
        {
            
        }

        public DbSet<Character> Characters{ get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<Movie> Movies { get; set; }
        public DbSet<CharacterMovie> CharacterMovies{ get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Character
            modelBuilder.Entity<Character>().HasKey(x => x.Id_Character);

            //Genre
            modelBuilder.Entity<Genre>().HasKey(x => x.Id_Genre);

            //Movie
            modelBuilder.Entity<Movie>().HasKey(x => x.Id_Movie);
            modelBuilder.Entity<Movie>()
                .HasOne(x => x.Genre)
                .WithMany(x => x.Movies)
                .HasForeignKey(x=>x.Id_Genre);
            //modelBuilder.Entity<Movie>().Navigation(x => x.CharacterMovies).IsRequired(false);
            //modelBuilder.Entity<Movie>().Navigation(x => x.Genre).IsRequired(false);
            //modelBuilder.Entity<Movie>().Property(x => x.Rating).IsRequired(false);

            //CharacterMovie
            modelBuilder.Entity<CharacterMovie>().HasKey(cm => new {cm.Id_Movie, cm.Id_Character});
            modelBuilder.Entity<CharacterMovie>()
                .HasOne(c => c.Character)
                .WithMany(x => x.CharacterMovies)
                .HasForeignKey(x => x.Id_Character);
            modelBuilder.Entity<CharacterMovie>()
                .HasOne(c => c.Movie)
                .WithMany(x => x.CharacterMovies)
                .HasForeignKey(x => x.Id_Movie);


            base.OnModelCreating(modelBuilder);
        }
    }
}