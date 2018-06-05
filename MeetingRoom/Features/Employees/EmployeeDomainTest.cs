using FluentAssertions;
using MeetingRoom.Common.Test.Features.Employees;
using MeetingRoom.Domain.Features.Employees;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeetingRoom.Features.Employees
{
    [TestFixture]
    public class EmployeeDomainTest
    {
        [Test]
        public void Test_Employee_Valid_ShouldBeOk()
        {
            Employee employee = ObjectMotherEmployee.GetEmployee();
            employee.Id = 0;
            Action comparison = employee.Validate;
            comparison.Should().NotThrow<Exception>();
        }

        [Test]
        public void Test_Employee_InvalidEmptyOrNullTelephoneExtension_ShouldBeFail()
        {
            Employee employee = ObjectMotherEmployee.GetEmployeeWithEmptyTelephoneExtension();
            employee.Id = 0;
            Action comparison = employee.Validate;
            comparison.Should().Throw<EmployeeEmptyOrNullTelephoneExtensionException>();
        }

        [Test]
        public void Test_Employee_InvalidEmptyOrNullName_ShouldBeFail()
        {
            Employee employee = ObjectMotherEmployee.GetEmployeeWithEmptyName();
            employee.Id = 0;
            Action comparison = employee.Validate;
            comparison.Should().Throw<EmployeeEmptyOrNullNameException>();
        }

        [Test]
        public void Test_Employee_InvalidEmptyOrNullEmployeePosition_ShouldBeFail()
        {
            Employee employee = ObjectMotherEmployee.GetEmployeeWithEmptyEmployeePosition();
            employee.Id = 0;
            Action comparison = employee.Validate;
            comparison.Should().Throw<EmployeeEmptyOrNullEmployeePositionException>();
        }
    }
}
