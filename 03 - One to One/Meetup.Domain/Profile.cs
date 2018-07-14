using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Meetup.Domain
{
    public class Profile
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Twitter { get; set; }


        public User User { get; set; } //TODO: 08 - Agrego propiedad de navegación
        public int UserId { get; set; } //TODO: 06 - Agrego FK
    }
}