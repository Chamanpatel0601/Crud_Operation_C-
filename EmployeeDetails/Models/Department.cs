using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using EmployeeDetails.Models;

namespace EmployeeDetails.Models
{
    public class Department
    {
        [Display(Name = "DeptId")]
        public int Id { get; set; }

        [Display(Name = "DeptName")]
        public string? Name { get; set; }

        public ICollection<Employee> Employees { get; set; }
    }
}
