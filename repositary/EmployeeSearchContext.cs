using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EmployeeManagementSystem;
using RuppSystem.model;

namespace RuppSystem.repositary
{
    public class EmployeeSearchContext
    {
        private IEmployeeSearchStrategy _searchStrategy;

        public EmployeeSearchContext(IEmployeeSearchStrategy searchStrategy)
        {
            _searchStrategy = searchStrategy;
        }

        public void SetStrategy(IEmployeeSearchStrategy searchStrategy)
        {
            _searchStrategy = searchStrategy;
        }

        public List<Employee> Search(List<Employee> employees, string keyword)
        {
            return _searchStrategy.Search(employees, keyword);
        }
    }
}
