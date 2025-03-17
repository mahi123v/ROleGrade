using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace RolesGrade.Models
{
    public class Salary
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public decimal Amount { get; set; }

        // Foreign Key for Employee
        public int EmployeeId { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        // Navigation Property for the relationship
        public Employee Employee { get; set; } // One salary record for each employee

       
    }
}
