using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EmployeeManagementSystem;
using RuppSystem.model;

namespace RuppSystem.repositary
{
    public class SearchByIdOrNameStrategy : IEmployeeSearchStrategy
    {
        public List<Employee> Search(List<Employee> employees, string keyword)
        {
            return employees
                .Where(e => e.EmployeeID.ToLower().Contains(keyword.ToLower()) ||
                            e.FullName.ToLower().Contains(keyword.ToLower()))
                .ToList();
        }
    }
}
