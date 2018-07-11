using System;
using System.Collections.Generic;

namespace Meetup.Domain
{
    public class MeetupEvent
    {
        public int Id { get; set; }
        public Group Group { get; set; }
        public DateTime From { get; set; }
        public DateTime To { get; set; }
        public string Location { get; set; }
        public List<UserMeetup> Assistants { get; set; }
    }
}
