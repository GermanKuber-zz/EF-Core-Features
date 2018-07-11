using System;
using System.Collections.Generic;

namespace Meetup.Domain
{
    public class UserMeetup
    {
        public Guid UserId { get; set; }
        public User User { get; set; }
        public Guid MeetupId { get; set; }
        public MeetupEvent Meetup { get; set; }
    }
}
