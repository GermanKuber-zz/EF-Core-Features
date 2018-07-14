using System;
using System.Linq;
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

            //TODO: 01 - Configuro propiedades del tipo shadow
            ConfigureShadowProperty(modelBuilder);

            CreateSeeds(modelBuilder);
        }

        private static void ConfigureShadowProperty(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<MeetupEvent>().Property<DateTime>("Created").IsRequired();
            modelBuilder.Entity<MeetupEvent>().Property<DateTime>("LastModified");




            //TODO: 04 - Se agrega la shadow property Created a todas las entidades
            //modelBuilder.Model
            //    .GetEntityTypes()
            //    .ToList()
            //    .ForEach(x => modelBuilder.Entity(x.Name).Property<DateTime>("Created").IsRequired());

        }

        private static void ConfigureEntities(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserGroup>()
                .HasKey(k => new { k.GroupId, k.UserId });

            modelBuilder.Entity<UserMeetup>()
                .HasKey(k => new { k.MeetupId, k.UserId });

            var navigation = modelBuilder.Entity<MeetupEvent>().Metadata.FindNavigation(nameof(MeetupEvent.Assistants));
            navigation.SetPropertyAccessMode(PropertyAccessMode.Field);

            modelBuilder.Entity<Group>()
                .HasOne(i => i.Founder)
                .WithOne()
                .HasForeignKey<User>(u => u.GroupFounder);

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

        //public override int SaveChanges()
        //{
        //    //TODO: 05 - Detecto las entidades que se creaton y verifico les agrego la hora
        //    ChangeTracker.DetectChanges();
        //    var timestamp = DateTime.Now;

        //    ChangeTracker.Entries()
        //        .Where(x => x.State == EntityState.Added)
        //        .ToList()
        //        ?.ForEach(entry => entry.Property("Created").CurrentValue = timestamp);

        //    return base.SaveChanges();
        //}
    }
}
