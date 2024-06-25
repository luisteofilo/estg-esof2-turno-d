using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ESOF.WebApp.DBLayer.Entities
{
    public class Role_verticals
    {
        
        public Role_verticals()
        {
            Skills_verticals = new List<skil_veticals>();

        }
        [Key]
        public Guid Role_verticalsId { get; set; }

        [Required]
        public string Role_verticalsName { get; set; }
        
        public Guid VerticalId { get; set; }
        
        public Vertical Vertical { get; set; }
        public ICollection<skil_veticals> Skills_verticals { get; set; }
    }
}