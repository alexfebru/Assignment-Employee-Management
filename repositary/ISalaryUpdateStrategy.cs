using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RuppSystem.repositary
{
    public interface ISalaryUpdateStrategy
    {
        bool UpdateSalary(string employeeID, int salary);
    }
}
