using EmployeeDetails.Models;
using EmployeeDetails.Repository;
using Microsoft.EntityFrameworkCore;

namespace EmployeeDetails.Service
{
    public class SQLEmployeeService : IEmployeeService
    {
        private readonly AppdbContextRepository context;
        public SQLEmployeeService(AppdbContextRepository context)
        {
            this.context = context;
        }

        public IEnumerable<Employee> GetAllEmployee()
        {
            return context.Employees.Include<Employee>("Department");
        }
        public Employee? GetEmployee(int Id)
        {

            //context.Employee.Find(Id);
            Employee e = context.Employees.Include(e => e.Department).FirstOrDefault(m => m.Id == Id);

            return e;
        }

        public Employee Add(Employee employee)
        {
            context.Employees.Add(employee);
            context.SaveChanges();
            return employee;
        }

        public Employee Update(Employee employeeChanges)
        {

            context.Entry(employeeChanges).State = EntityState.Modified;
            context.Update(employeeChanges);
            context.SaveChanges();
            return employeeChanges;
        }

        public IEnumerable<Department> GetAllDepartment()
        {
            return context.Departments;
        }

        public Employee? Delete(int Id)
        {
            Employee? employee = context.Employees.Find(Id);
            if (employee != null)
            {
                context.Employees.Remove(employee);
                context.SaveChanges();
            }
            return employee;
        }



        public User? Login(string email, string password)
        {
            return context.Users.FirstOrDefault(u => u.Email == email && u.Password == password);
        }

        public User AddUser(User user)
        {
            context.Users.Add(user);
            context.SaveChanges();
            return user;
        }

        public IEnumerable<User> GetAllUser()
        {
            return context.Users;
        }
    }
}
