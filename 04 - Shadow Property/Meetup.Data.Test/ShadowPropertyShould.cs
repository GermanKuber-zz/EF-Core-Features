using System;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using Meetup.Domain;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace Meetup.Data.Test
{
    public class ShadowPropertyShould
    {
        private readonly MeetupContext _context;
        public ShadowPropertyShould()
        {
            _context = new MeetupContext();
        }

        [Fact]
        public void Throw_Exception_About_Required_ShadowProperty()
        {
            //TODO: 02 - Inserto una entidad con shadow property requerida en null
            var eventMeetup = new MeetupEvent(DateTime.Now.AddDays(5), DateTime.Now.AddDays(5));
            _context.Meetups.Add(eventMeetup);

            var timesTamp = DateTime.Now;
            _context.Entry(eventMeetup).Property("Created").CurrentValue = null;
            _context.Entry(eventMeetup).Property("LastModified").CurrentValue = timesTamp;

            Action act = () => _context.SaveChanges();

            act.Should().Throw<InvalidOperationException>();
        }

        [Fact]
        public void Insert_Event_With_ShadowProperty()
        {
            //TODO: 03 - Inserto un evento con Shadow Property
            var eventMeetup = CreateData(DateTime.Now);

            eventMeetup.Id.Should().NotBe(0);
        }

        [Fact]
        public void Get_MeetupEvent_By_Created_Date()
        {
            CreateData(DateTime.Now);

            var events = _context.Meetups
                .Where(x => EF.Property<DateTime>(x, "Created") > DateTime.Now.AddDays(-1))
                .ToList();

            events.Should().NotBeNull();
        }

        [Fact]
        public void Get_ShadowProperty_In_Entity()
        {
            var timesTamp = DateTime.Now;
           var meetup = CreateData(timesTamp);

            var events = _context.Meetups
                .Where(x => x.Id == meetup.Id)
                .Select(s => new { s.Id, s.From, s.To, Created = EF.Property<DateTime>(s, "Created") })
                .First();

            events.Created.Should().Be(timesTamp);
        }

        private MeetupEvent CreateData(DateTime created)
        {
            var eventMeetup = new MeetupEvent(DateTime.Now.AddDays(5), DateTime.Now.AddDays(5));
            _context.Meetups.Add(eventMeetup);
            _context.Entry(eventMeetup).Property("Created").CurrentValue = created;
            _context.Entry(eventMeetup).Property("LastModified").CurrentValue = created;
            _context.SaveChanges();
            return eventMeetup;
        }
    }
}
