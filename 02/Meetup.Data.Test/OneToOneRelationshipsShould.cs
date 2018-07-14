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

        private Group GetGroup()
        {
            var netBaires = _context.Groups
                .Include(x => x.Founder)
                .First(x => x.Name == "Net-Baires");
            return netBaires;
        }
    }
}
