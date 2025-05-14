using EmployeeDetails.Models;


namespace EmployeeDetails.Service
{
         public interface IEmployeeService
         {
            Employee? GetEmployee(int Id);
            IEnumerable<Employee> GetAllEmployee();
            Employee Add(Employee employee);
            Employee Update(Employee employeeChanges);
            Employee? Delete(int Id);
            IEnumerable<Department> GetAllDepartment();

            User? Login(string email, string password);
            User AddUser(User user);

        IEnumerable<User> GetAllUser();

    }

}
