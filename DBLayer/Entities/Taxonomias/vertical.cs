using System.ComponentModel.DataAnnotations;

namespace ESOF.WebApp.DBLayer.Entities
{
    public class Vertical
    {
        public Vertical()
        {
            Roles_verticals = new List<Role_verticals>();
            VerticalsUsers = new List<verticalsUser>();

        }
        [Key]
        public Guid VerticalId { get; set; }

        [Required]
        public string VerticalName { get; set; }
        
        public ICollection<Role_verticals> Roles_verticals { get; set; }
        
        public ICollection<verticalsUser> VerticalsUsers { get; set; }
    }
}