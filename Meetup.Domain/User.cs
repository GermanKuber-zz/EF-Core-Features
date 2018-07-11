using System;
using System.Collections.Generic;

namespace Meetup.Domain
{
    public class User
    {
        public Guid Id { get; set; }
        public string Email { get; set; }
        public List<UserGroup> Groups { get; set; }

    }
}
