using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ESOF.WebApp.DBLayer.Entities
{
    public class Role_verticals
    {
        [Key]
        public Guid Role_verticalsId { get; set; }

        [Required]
        public string Role_verticalsName { get; set; }

        // Foreign key
        public Guid VerticalId { get; set; }

        // Navigation properties
        public Vertical Vertical { get; set; }
        public ICollection<skil_veticals> Skills_verticals { get; set; }
    }
}