using Meetup.Domain;
using Microsoft.EntityFrameworkCore;
using System;

namespace Meetup.Data
{
    public class MeetupContext : DbContext
    {
        public DbSet<Group> Groups { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<MeetupEvent> Meetups { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=(LocalDb)\\MSSQLLocalDB;Initial Catalog=MeetupDb;Integrated Security=SSPI;");
            base.OnConfiguring(optionsBuilder);
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserGroup>()
                        .HasKey(k => new { k.GroupId, k.UserId });

            modelBuilder.Entity<UserMeetup>()
                   .HasKey(k => new { k.MeetupId, k.UserId });

            base.OnModelCreating(modelBuilder);
        }
    }
}
