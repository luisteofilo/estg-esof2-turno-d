using System;
using System.ComponentModel.DataAnnotations;

namespace ESOF.WebApp.DBLayer.Entities
{
    public class skil_veticals
    {
        [Key]
        public Guid skil_veticalsId { get; set; }

        [Required]
        public string skil_veticalsName { get; set; }

        public int skil_veticalsExperiencia { get; set; }

        // Foreign key
        public Guid Role_verticalsId { get; set; }
        
        public Role_verticals RoleVerticals { get; set; }
       
    }
}