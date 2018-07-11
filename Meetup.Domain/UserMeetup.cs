namespace Meetup.Domain
{
    public class UserMeetup
    {
        public int UserId { get; set; }
        public User User { get; set; }
        public int MeetupId { get; set; }
        public MeetupEvent Meetup { get; set; }
    }
}
