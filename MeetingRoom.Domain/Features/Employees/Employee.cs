using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeetingRoom.Domain.Features.Employees
{
    public class Employee
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string EmployeePosition { get; set; } 
        public string TelephoneExtension { get; set; } 

        public void Validate()
        {
            if (string.IsNullOrEmpty(Name))
                throw new EmployeeEmptyOrNullNameException();
            if (string.IsNullOrEmpty(EmployeePosition))
                throw new EmployeeEmptyOrNullEmployeePositionException();
            if (string.IsNullOrEmpty(TelephoneExtension))
                throw new EmployeeEmptyOrNullTelephoneExtensionException();
        }
    }
}
