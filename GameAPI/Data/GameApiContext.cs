using GameAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace GameAPI.Data
{
    public class GameApiContext(DbContextOptions<GameApiContext> options) : DbContext(options)
    {
        public DbSet<User> Users { get; set; }
        public DbSet<GameCompany> GameCompanies { get; set; }
        public DbSet<Publisher> Publishers { get; set; }
        public DbSet<Platform> Platforms { get; set; }
        public DbSet<Game> Games { get; set; }
        public DbSet<GameDetail> GameDetails { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // GameCompany → Games (1:N)
            modelBuilder.Entity<Game>()
                .HasOne(g => g.GameCompany)
                .WithMany(gc => gc.Games)
                .HasForeignKey(g => g.GameCompanyId)
                .OnDelete(DeleteBehavior.Restrict);

            // Publisher → Games (1:N)
            modelBuilder.Entity<Game>()
                .HasOne(g => g.Publisher)
                .WithMany(p => p.Games)
                .HasForeignKey(g => g.PublisherId)
                .OnDelete(DeleteBehavior.Restrict);

            // Game → GameDetail (1:1)
            modelBuilder.Entity<Game>()
                .HasOne(g => g.GameDetail)
                .WithOne(gd => gd.Game)
                .HasForeignKey<GameDetail>(gd => gd.GameId)
                .OnDelete(DeleteBehavior.Cascade);

            // Game ↔ Platform (Many-to-Many)
            modelBuilder.Entity<Game>()
                .HasMany(g => g.Platforms)
                .WithMany(p => p.Games)
                .UsingEntity(j => j.ToTable("GamePlatforms"));

            // User → everything (1:N)
            modelBuilder.Entity<User>()
                .HasMany(u => u.Games)
                .WithOne(g => g.User)
                .HasForeignKey(g => g.UserId);

            modelBuilder.Entity<User>()
                .HasMany(u => u.GameCompanies)
                .WithOne(gc => gc.User)
                .HasForeignKey(gc => gc.UserId);

            modelBuilder.Entity<User>()
                .HasMany(u => u.Publishers)
                .WithOne(p => p.User)
                .HasForeignKey(p => p.UserId);

            modelBuilder.Entity<User>()
                .HasMany(u => u.Platforms)
                .WithOne(p => p.User)
                .HasForeignKey(p => p.UserId);

            modelBuilder.Entity<User>()
                .HasMany(u => u.GameDetails)
                .WithOne(gd => gd.User)
                .HasForeignKey(gd => gd.UserId);


            // Users
            modelBuilder.Entity<User>().HasData(
                new User { UserId = 1, UserName = "admin", Email = "admin@gameapi.com", Password = "admin123", Role = "Admin" },
                new User { UserId = 2, UserName = "companyuser", Email = "company@gameapi.com", Password = "company123", Role = "Company" },
                new User { UserId = 3, UserName = "vieweruser", Email = "viewer@gameapi.com", Password = "viewer123", Role = "Viewer" }
            );

            // Game Companies
            modelBuilder.Entity<GameCompany>().HasData(
                new GameCompany { GameCompanyId = 1, Name = "Naughty Dog", FoundedYear = 1984, HeadQuarter = "California, USA", UserId = 2 },
                new GameCompany { GameCompanyId = 2, Name = "CD Projekt Red", FoundedYear = 1994, HeadQuarter = "Warsaw, Poland", UserId = 2 },
                new GameCompany { GameCompanyId = 3, Name = "Rockstar Games", FoundedYear = 1998, HeadQuarter = "New York, USA", UserId = 2 },
                new GameCompany { GameCompanyId = 4, Name = "Ubisoft", FoundedYear = 1986, HeadQuarter = "Montreuil, France", UserId = 2 },
                new GameCompany { GameCompanyId = 5, Name = "343 Industries", FoundedYear = 2007, HeadQuarter = "Washington, USA", UserId = 2 }
            );

            // Publishers
            modelBuilder.Entity<Publisher>().HasData(
                new Publisher { PublisherId = 1, Name = "Sony Interactive Entertainment", Country = "Japan", UserId = 2 },
                new Publisher { PublisherId = 2, Name = "Warner Bros. Games", Country = "USA", UserId = 2 },
                new Publisher { PublisherId = 3, Name = "Microsoft Studios", Country = "USA", UserId = 2 },
                new Publisher { PublisherId = 4, Name = "Ubisoft Entertainment", Country = "France", UserId = 2 }
            );

            // Platforms
            modelBuilder.Entity<Platform>().HasData(
                new Platform { PlatformId = 1, Name = "PlayStation 5", Type = "Console", UserId = 1 },
                new Platform { PlatformId = 2, Name = "Xbox Series X", Type = "Console", UserId = 1 },
                new Platform { PlatformId = 3, Name = "PC", Type = "Desktop", UserId = 1 },
                new Platform { PlatformId = 4, Name = "Nintendo Switch", Type = "Console", UserId = 1 }
            );

            // Games
            modelBuilder.Entity<Game>().HasData(
                new Game { GameId = 1, Title = "The Last of Us Part II", GameCompanyId = 1, PublisherId = 1, UserId = 2 },
                new Game { GameId = 2, Title = "Cyberpunk 2077", GameCompanyId = 2, PublisherId = 2, UserId = 2 },
                new Game { GameId = 3, Title = "GTA V", GameCompanyId = 3, PublisherId = 2, UserId = 2 },
                new Game { GameId = 4, Title = "Assassin’s Creed Valhalla", GameCompanyId = 4, PublisherId = 4, UserId = 2 },
                new Game { GameId = 5, Title = "Halo Infinite", GameCompanyId = 5, PublisherId = 3, UserId = 2 },
                new Game { GameId = 6, Title = "Uncharted 4", GameCompanyId = 1, PublisherId = 1, UserId = 2 },
                new Game { GameId = 7, Title = "The Witcher 3: Wild Hunt", GameCompanyId = 2, PublisherId = 2, UserId = 2 },
                new Game { GameId = 8, Title = "Far Cry 6", GameCompanyId = 4, PublisherId = 4, UserId = 2 }
            );

            // Game Details
            modelBuilder.Entity<GameDetail>().HasData(
                new GameDetail { GameDetailId = 1, GameId = 1, Genre = "Action-Adventure", ReleaseDate = new DateTime(2020, 6, 19), Description = "Survival story set in a post-apocalyptic world.", UserId = 2 },
                new GameDetail { GameDetailId = 2, GameId = 2, Genre = "RPG", ReleaseDate = new DateTime(2020, 12, 10), Description = "Futuristic open-world RPG set in Night City.", UserId = 2 },
                new GameDetail { GameDetailId = 3, GameId = 3, Genre = "Action-Adventure", ReleaseDate = new DateTime(2013, 9, 17), Description = "Open-world action crime game.", UserId = 2 },
                new GameDetail { GameDetailId = 4, GameId = 4, Genre = "Action-RPG", ReleaseDate = new DateTime(2020, 11, 10), Description = "Viking-era open-world RPG.", UserId = 2 },
                new GameDetail { GameDetailId = 5, GameId = 5, Genre = "FPS", ReleaseDate = new DateTime(2021, 12, 8), Description = "Master Chief returns to battle the Banished.", UserId = 2 },
                new GameDetail { GameDetailId = 6, GameId = 6, Genre = "Adventure", ReleaseDate = new DateTime(2016, 5, 10), Description = "Nathan Drake’s final treasure-hunting adventure.", UserId = 2 },
                new GameDetail { GameDetailId = 7, GameId = 7, Genre = "RPG", ReleaseDate = new DateTime(2015, 5, 19), Description = "Monster-hunting in a rich fantasy open world.", UserId = 2 },
                new GameDetail { GameDetailId = 8, GameId = 8, Genre = "Shooter", ReleaseDate = new DateTime(2021, 10, 7), Description = "Cuba-inspired open-world revolution story.", UserId = 2 }
            );

        }

    }
}
