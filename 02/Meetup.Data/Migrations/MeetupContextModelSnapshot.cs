﻿// <auto-generated />
using System;
using Meetup.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Meetup.Data.Migrations
{
    [DbContext(typeof(MeetupContext))]
    partial class MeetupContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.1-rtm-30846")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Meetup.Domain.Group", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Description");

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.ToTable("Groups");

                    b.HasData(
                        new { Id = 1, Description = "Comunidad de .Net", Name = "Net-Baires" }
                    );
                });

            modelBuilder.Entity("Meetup.Domain.MeetupEvent", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("Created");

                    b.Property<DateTime>("From");

                    b.Property<int?>("GroupId");

                    b.Property<DateTime>("LastModified");

                    b.Property<string>("Location");

                    b.Property<DateTime>("To");

                    b.HasKey("Id");

                    b.HasIndex("GroupId");

                    b.ToTable("Meetups");
                });

            modelBuilder.Entity("Meetup.Domain.Profile", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Twitter");

                    b.Property<int>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("UserId")
                        .IsUnique();

                    b.ToTable("Profile");
                });

            modelBuilder.Entity("Meetup.Domain.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Email");

                    b.Property<int?>("GroupFounder");

                    b.HasKey("Id");

                    b.HasIndex("GroupFounder")
                        .IsUnique()
                        .HasFilter("[GroupFounder] IS NOT NULL");

                    b.ToTable("Users");

                    b.HasData(
                        new { Id = 1, Email = "german.kuber@outlook.com" },
                        new { Id = 2, Email = "Juan.Roso@hotmail.com" }
                    );
                });

            modelBuilder.Entity("Meetup.Domain.UserGroup", b =>
                {
                    b.Property<int>("GroupId");

                    b.Property<int>("UserId");

                    b.HasKey("GroupId", "UserId");

                    b.HasIndex("UserId");

                    b.ToTable("UserGroup");
                });

            modelBuilder.Entity("Meetup.Domain.UserMeetup", b =>
                {
                    b.Property<int>("MeetupId");

                    b.Property<int>("UserId");

                    b.HasKey("MeetupId", "UserId");

                    b.HasIndex("UserId");

                    b.ToTable("UserMeetup");
                });

            modelBuilder.Entity("Meetup.Domain.MeetupEvent", b =>
                {
                    b.HasOne("Meetup.Domain.Group", "Group")
                        .WithMany("Meetups")
                        .HasForeignKey("GroupId");
                });

            modelBuilder.Entity("Meetup.Domain.Profile", b =>
                {
                    b.HasOne("Meetup.Domain.User", "User")
                        .WithOne("Profile")
                        .HasForeignKey("Meetup.Domain.Profile", "UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Meetup.Domain.User", b =>
                {
                    b.HasOne("Meetup.Domain.Group")
                        .WithOne("Founder")
                        .HasForeignKey("Meetup.Domain.User", "GroupFounder");
                });

            modelBuilder.Entity("Meetup.Domain.UserGroup", b =>
                {
                    b.HasOne("Meetup.Domain.Group", "Group")
                        .WithMany("Users")
                        .HasForeignKey("GroupId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Meetup.Domain.User", "User")
                        .WithMany("Groups")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Meetup.Domain.UserMeetup", b =>
                {
                    b.HasOne("Meetup.Domain.MeetupEvent", "Meetup")
                        .WithMany("Assistants")
                        .HasForeignKey("MeetupId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Meetup.Domain.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
