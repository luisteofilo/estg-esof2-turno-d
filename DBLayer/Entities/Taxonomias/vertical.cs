using System.ComponentModel.DataAnnotations;

namespace ESOF.WebApp.DBLayer.Entities
{
    public class Vertical
    {
        [Key]
        public Guid VerticalId { get; set; }

        [Required]
        public string VerticalName { get; set; }

        // Navigation property
        public ICollection<Role_verticals> Roles_verticals { get; set; }
        
        public ICollection<verticalsUser> VerticalsUsers { get; set; }
    }
}