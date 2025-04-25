using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RuppSystem.model
{
    public class Employee
    {
        public string EmployeeID { get; set; }
        public string FullName { get; set; }
        public string Gender { get; set; }
        public string ContactNumber { get; set; }
        public string Position { get; set; }
        public string ImagePath { get; set; }
        public decimal Salary { get; set; }
        public DateTime InsertDate { get; set; }
        public string Status { get; set; }
 
        public static Employee Create(string id, 
                                       string fullName, 
                                       string gender, string contact, string position, string image, decimal salary, string status)
        {
            return new Employee
            {
                EmployeeID = id,
                FullName = fullName,
                Gender = gender,
                ContactNumber = contact,
                Position = position,
                ImagePath = image,
                Salary = salary,
                InsertDate = DateTime.Today,
                Status = status
            };
        }
    }
}
