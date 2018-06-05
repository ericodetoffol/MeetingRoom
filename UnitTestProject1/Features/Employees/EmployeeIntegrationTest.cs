using FluentAssertions;
using MeetingRoom.Application.Features.Employees;
using MeetingRoom.Common.Test.Features.Employees;
using MeetingRoom.Domain.Exceptions;
using MeetingRoom.Domain.Features.Employees;
using MeetingRoom.Infra.Data.Features.Employees;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTestProject1.Features.Employees
{
    [TestFixture]
    public class EmployeeIntegrationTest
    {
        private IEmployeeService _service;
        private IEmployeeRepository _repository;

        [SetUp]
        public void Initialize()
        {
            _repository = new EmployeeRepository();
            _service = new EmployeeService(_repository);
        }

        [Order(1)]
        [Test]
        public void EmployeeIntegration_Add_ShouldBeOk()
        {
            Employee employee = ObjectMotherEmployee.GetEmployee();
            Employee result = _service.Add(employee);
            result.Should().NotBeNull();
            result.Id.Should().BeGreaterThan(0);
        }

        [Order(2)]
        [Test]
        public void EmployeeIntegration_Add_InvalidEmptyOrNullName_ShouldBeFail()
        {
            Employee employee = ObjectMotherEmployee.GetEmployeeWithEmptyName();
            Action executeAction = () => _service.Add(employee);
            executeAction.Should().Throw<EmployeeEmptyOrNullNameException>();
        }

        [Order(3)]
        [Test]
        public void EmployeeIntegration_Update_ShouldBeOk()
        {
            Employee employee = _service.Get(1);
            string oldName = employee.Name;
            employee.Name = "Test";
            Employee result = _service.Update(employee);
            result.Should().NotBeNull();
            result.Id.Should().BeGreaterThan(0);
            result.Name.Should().NotBe(oldName);
        }

        [Order(4)]
        [Test]
        public void EmployeeIntegration_Update_InvalidId_ShouldBeFail()
        {
            Employee employee = ObjectMotherEmployee.GetEmployee();
            Action executeAction = () => _service.Update(employee);
            executeAction.Should().Throw<IdentifierUndefinedException>();
        }

        [Order(5)]
        [Test]
        public void EmployeeIntegration_Get_ShouldBeOk()
        {
            int id = 1;
            Employee employee = _service.Get(id);
            employee.Should().NotBeNull();
        }

        [Order(6)]
        [Test]
        public void EmployeeIntegration_Get_InvalidId_ShouldBeOk()
        {
            Action executeAction = () => _service.Get(0);
            executeAction.Should().Throw<IdentifierUndefinedException>();
        }

        [Order(7)]
        [Test]
        public void EmployeeIntegration_GetAll_ShouldBeOk()
        {
            IEnumerable<Employee> employees = _service.GetAll();
            employees.Count().Should().Equals(1);
        }

        [Order(8)]
        [Test]
        public void EmployeeIntegration_Delete_ShouldBeOk()
        {
            Employee employee = _service.Get(1);
            _service.Delete(employee);
            Employee result = _service.Get(1);
            result.Should().BeNull();
        }

        [Order(9)]
        [Test]
        public void EmployeeIntegration_Delete_InvalidId_ShouldBeOk()
        {
            Employee employee = ObjectMotherEmployee.GetEmployee();
            Action executeAction = () => _service.Delete(employee);
            executeAction.Should().Throw<IdentifierUndefinedException>();
        }
    }
}
