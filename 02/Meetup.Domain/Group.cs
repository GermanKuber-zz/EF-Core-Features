using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Meetup.Domain
{
    public class Group
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public List<UserGroup> Users { get; set; }
        public List<MeetupEvent> Meetups { get; set; }
        //TODO: 01 - Agrego un usuario como fundador
        public User Founder { get; set; }

    }
}
