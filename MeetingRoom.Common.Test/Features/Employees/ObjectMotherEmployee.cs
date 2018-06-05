using MeetingRoom.Domain.Features.Employees;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeetingRoom.Common.Test.Features.Employees
{
    public static partial class ObjectMotherEmployee
    {
        public static Employee GetEmployee()
        {
            return new Employee()
            {
                Name = "Test",
                EmployeePosition = "Test",
                TelephoneExtension = "Test"
            };
        }

        public static Employee GetEmployeeWithEmptyName()
        {
            return new Employee()
            {
                Name = "",
                EmployeePosition = "Test",
                TelephoneExtension = "Test"
            };
        }

        public static Employee GetEmployeeWithEmptyEmployeePosition()
        {
            return new Employee()
            {
                Name = "Test",
                EmployeePosition = "",
                TelephoneExtension = "Test"
            };
        }

        public static Employee GetEmployeeWithEmptyTelephoneExtension()
        {
            return new Employee()
            {
                Name = "Test",
                EmployeePosition = "Test",
                TelephoneExtension = ""
            };
        }
    }
}
