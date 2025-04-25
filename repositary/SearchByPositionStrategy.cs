using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EmployeeManagementSystem;
using RuppSystem.model;

namespace RuppSystem.repositary
{
    public class SearchByPositionStrategy : IEmployeeSearchStrategy
    {
        public List<Employee> Search(List<Employee> employees, string keyword)
        {
            return employees
                .Where(e => e.Position.ToLower().Contains(keyword.ToLower()))
                .ToList();
        }
    }
}
