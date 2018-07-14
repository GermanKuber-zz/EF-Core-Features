using System.Linq;
using FluentAssertions;
using Meetup.Domain;
using Xunit;

namespace Meetup.Data.Test
{
    public class EncapsulateCollectionsShould
    {
        private readonly MeetupContext _context;
        public EncapsulateCollectionsShould()
        {
            _context = new MeetupContext();
        }

        [Fact]
        public void Crete_New_MeetupEvent_Add_Assistants()
        {
            var meetupEvent = new MeetupEvent();
            _context.Add(meetupEvent);
            _context.SaveChanges();

            meetupEvent.AddAssistant(_context.Users.First());
            _context.SaveChanges();

            meetupEvent.Assistants.Count().Should().Be(1);

            _context.Remove(meetupEvent);
            _context.SaveChanges();

        }
    }
}
