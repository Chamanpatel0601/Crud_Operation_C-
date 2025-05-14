using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace EmployeeDetails.Models
{
    public class Employee
    {
        public int Id { get; set; }

        [Required]
        [DisplayName("Employee Name")]
        public string? Name { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        public string? Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string? Password { get; set; } 

        public int DepartmentId { get; set; }
        public Department? Department { get; set; }
    }
}
