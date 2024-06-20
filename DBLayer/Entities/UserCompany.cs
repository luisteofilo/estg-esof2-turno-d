using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ESOF.WebApp.DBLayer.Entities
{
    public class UserCompany
    {
        [Key]
        public Guid UserCompanyId { get; set; }
        
        [Required]
        public Guid UserId { get; set; }
        
        [Required]
        public Guid CompanyId { get; set; }
        
        [Required]
        public int YearsWorked { get; set; }
        
        [Required]
        public DateTime StartDate { get; set; }
        
        public DateTime? EndDate { get; set; } // Data de término opcional
        
        [Required]
        public bool IsCurrentlyEmployed { get; set; } // Indica se ainda está empregado

        [ForeignKey(nameof(UserId))]
        public User User { get; set; }
        
        [ForeignKey(nameof(CompanyId))]
        public Companies Company { get; set; }
    }
}