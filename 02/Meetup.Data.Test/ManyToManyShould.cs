using System.Collections.Generic;
using System.Linq;
using Meetup.Domain;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace Meetup.Data.Test
{
    public class ManyToManyShould
    {
        private readonly MeetupContext _context;
        public ManyToManyShould()
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

        [Fact]
        public void Get_Users_With_Group()
        {
            var userWithGroups = _context.Users
                .Include(x => x.Groups)
                .ThenInclude(x => x.Group)
                .FirstOrDefault(x => x.Id == 1);
        }

        [Fact]
        public void Remove_User_Group_In_Memory()
        {
            using (var context = new MeetupContext())
            {
                var user = context.Users.First();
                var group = context.Groups.First();
                var userGroup = new UserGroup { Group = group, User = user };
                user.Groups.Add(userGroup);
                context.SaveChanges();
            }


            _context.Remove(new UserGroup { UserId = 1, GroupId = 1 });
            _context.SaveChanges();
        }

        [Fact]
        public void Remove_SubEntity_Detached()
        {
            User userWithGroups;
            using (var context = new MeetupContext())
            {
                var tmpUser  = _context.Users
                   .Include(x => x.Groups)
                   .ThenInclude(x => x.Group)
                   .First(x => x.Id == 1);

                tmpUser.Groups.Add(new UserGroup { UserId = tmpUser.Id,GroupId = 1 });
                context.SaveChanges();

                userWithGroups = _context.Users
                    .Include(x => x.Groups)
                    .ThenInclude(x => x.Group)
                    .First(x => x.Id == 1);
            }

            var group = userWithGroups.Groups.FirstOrDefault();
            userWithGroups.Groups.Remove(group);
            _context.Attach(userWithGroups);
            _context.SaveChanges();
        }
    }
}
