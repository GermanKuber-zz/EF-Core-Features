using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Meetup.Domain
{
    public class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Email { get; set; }
        public List<UserGroup> Groups { get; set; } = new List<UserGroup>();

        //TODO: 02 - Agrego una FK al grupo fundado, esta FK es nulleable
        public int? GroupFounder { get; set; } 
        //TODO: 05 - Agrego un perfile al usuario One to One
        public Profile Profile { get; set; }
    }
}
