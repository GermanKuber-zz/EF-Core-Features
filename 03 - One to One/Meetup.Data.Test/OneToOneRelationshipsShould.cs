using System;
using System.Data.SqlClient;
using System.Linq;
using FluentAssertions;
using Meetup.Domain;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace Meetup.Data.Test
{
    public class OneToOneRelationshipsShould
    {
        private readonly MeetupContext _context;
        public OneToOneRelationshipsShould()
        {
            _context = new MeetupContext();
        }

        [Fact]
        public void Add_Founder_To_Group_Through_User_Fk()
        {
            //TODO: 04 - Agrego un fundador al grupo 
            var netBaires = GetGroup();

            var german = _context.Users.First(x => x.Email == "german.kuber@outlook.com");

            netBaires.AddFounder(german);

            _context.SaveChanges();

            netBaires = GetGroup();

            netBaires.Founder.Should().Be(german);

            netBaires.SetFreeGroup(german);
            _context.SaveChanges();
        }

        [Fact]
        public void Add_Profile_To_User()
        {

            var german = _context.Users.Include(x => x.Profile).First(x => x.Email == "german.kuber@outlook.com");

            german.Profile = new Profile { Twitter = "Twiiter Test" };
            _context.SaveChanges();

            german = _context.Users.Include(x => x.Profile).First(x => x.Email == "german.kuber@outlook.com");

            german.Profile.Should().NotBeNull();

            german.Profile = null;
            _context.SaveChanges();
        }

        [Fact]
        public void Change_Object_One_To_Another()
        {
            //TODO: 09 - La entidad Profile se elimina  al cambiarla
            var newTwitter = "New Twitter";
            var german = _context.Users.Include(x => x.Profile).First(x => x.Email == "german.kuber@outlook.com");
            german.Profile = new Profile { Twitter = "Twiiter Test" };
            _context.SaveChanges();

            german.Profile = new Profile { Twitter = newTwitter };

            _context.SaveChanges();
            german.Profile.Id.Should().NotBe(0);
            german.Profile.Twitter.Should().Be(newTwitter);

            german.Profile = null;
            _context.SaveChanges();

        }

        [Fact]
        public void Change_Object_One_To_Another_Without_Tracking()
        {
            //TODO: 10 - Cambio el profile, y se agregan dos profile

            using (var context = new MeetupContext())
            {
                var german = context.Users.Include(x => x.Profile).First(x => x.Email == "german.kuber@outlook.com");
                german.Profile = new Profile { Twitter = "Twiiter Test 1" };
                context.SaveChanges();
            }


            User userWithoutTracking;
            using (var context = new MeetupContext())
            {
                userWithoutTracking = context.Users.Include(x => x.Profile).First(x => x.Email == "german.kuber@outlook.com");
            }
            userWithoutTracking.Profile = new Profile { Twitter = "New Twitter", User = userWithoutTracking };
            _context.Users.Attach(userWithoutTracking);

            Action act = () => _context.SaveChanges();

            act.Should().Throw<DbUpdateException>();

        }
       

        private Group GetGroup()
        {
            var netBaires = _context.Groups
                .Include(x => x.Founder)
                .First(x => x.Name == "Net-Baires");
            return netBaires;
        }
    }
}
