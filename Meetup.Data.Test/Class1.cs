using System.Linq;
using Meetup.Domain;
using Xunit;

namespace Meetup.Data.Test
{
    public class UserGroupsTest
    {
        private readonly MeetupContext _context;
        public UserGroupsTest()
        {
            _context = new MeetupContext();
        }

        [Fact]
        public void Create_New_UserGroup()
        {
            var userGroup = new UserGroup { UserId = 1, GroupId = 1 };
            _context.Add(userGroup);
            _context.SaveChanges();

            _context.Remove(userGroup);
            _context.SaveChanges();
        }

        [Fact]
        public void Add_New_User_Group()
        {
            var user = _context.Users.First();
            var userGroup = new UserGroup { GroupId = 1 };
            user.Groups.Add(userGroup);
            _context.SaveChanges();

            _context.Remove(userGroup);
            _context.SaveChanges();
        }

        [Fact]
        public void Add_New_User_Group_Detached_Entity()
        {
            User user;

            using (var context = new MeetupContext())
            {
                user = context.Users.First();
            }
            var userGroup = new UserGroup { GroupId = 1 };
            user.Groups.Add(userGroup);
            _context.Users.Attach(user);
            _context.ChangeTracker.DetectChanges();
            _context.SaveChanges();

            _context.Remove(userGroup);
            _context.SaveChanges();
        }

        [Fact]
        public void Add_New_UserGroup_With_New_Group()
        {
            User user;

            using (var context = new MeetupContext())
            {
                user = context.Users.First();
            }

            var newGroup = new Group { Name = "MeetupJs", Description = "Meetup sobre JS" };
            var userGroup = new UserGroup { Group = newGroup };
            user.Groups.Add(userGroup);
            _context.Users.Attach(user);
            _context.ChangeTracker.DetectChanges();
            _context.SaveChanges();

            _context.Remove(userGroup);
            _context.Remove(newGroup);
            _context.SaveChanges();
        }
    }
}
