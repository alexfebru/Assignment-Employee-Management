using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EmployeeManagementSystem;
using RuppSystem.model;

namespace RuppSystem.repositary
{
    public interface IEmployeeSearchStrategy
    {
        List<Employee> Search(List<Employee> employees, string keyword);
    }
}
