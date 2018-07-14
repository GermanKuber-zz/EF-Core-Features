using Meetup.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Meetup.Data
{
    public class MeetupContext : DbContext
    {
        public DbSet<Group> Groups { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<MeetupEvent> Meetups { get; set; }

        public static readonly LoggerFactory MyConsoleLoggerFactory
               = new LoggerFactory(new[] {
                          new Microsoft.Extensions.Logging.Debug.DebugLoggerProvider()});


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder
                   .UseLoggerFactory(MyConsoleLoggerFactory)
                   .UseSqlServer("Data Source=(LocalDb)\\MSSQLLocalDB;Initial Catalog=MeetupDb;Integrated Security=SSPI;")
                   .EnableSensitiveDataLogging();
            base.OnConfiguring(optionsBuilder);
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            ConfigureEntities(modelBuilder);

            CreateSeeds(modelBuilder);

            base.OnModelCreating(modelBuilder);
        }

        private static void ConfigureEntities(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserGroup>()
                .HasKey(k => new { k.GroupId, k.UserId });

            modelBuilder.Entity<UserMeetup>()
                .HasKey(k => new { k.MeetupId, k.UserId });

            var navigation = modelBuilder.Entity<MeetupEvent>().Metadata.FindNavigation(nameof(MeetupEvent.Assistants));
            navigation.SetPropertyAccessMode(PropertyAccessMode.Field);

            //TODO: 03 - Configuro la FK
            modelBuilder.Entity<Group>()
                .HasOne(i => i.Founder)
                .WithOne()
                .HasForeignKey<User>(u => u.GroupFounder);

             //TODO: 07 - Configuro la relación del perfil
            modelBuilder.Entity<User>()
                .HasOne(s => s.Profile)
                .WithOne(u => u.User)
                .IsRequired();

        }

        private void CreateSeeds(ModelBuilder modelBuilder)
        {
            var founder = new User { Id = 1, Email = "german.kuber@outlook.com" };

            modelBuilder.Entity<User>().HasData(founder);

            modelBuilder.Entity<User>().HasData(new User { Id = 2, Email = "Juan.Roso@hotmail.com" });

            modelBuilder.Entity<Group>().HasData(new Group { Id = 1, Name = "Net-Baires", Description = "Comunidad de .Net" });
        }
    }
}
