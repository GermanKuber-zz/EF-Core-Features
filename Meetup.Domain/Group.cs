using System;
using System.Collections.Generic;

namespace Meetup.Domain
{
    public class Group
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public List<UserGroup> Users { get; set; }
        public List<MeetupEvent> Meetups { get; set; }


    }
}
