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

        public int? GroupFounder { get; set; }  //TODO: 02 - Agrego una FK al grupo fundado
    }
}
