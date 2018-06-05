using FluentAssertions;
using MeetingRoom.Common.Test.Base;
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

namespace MeetingRoom.Infra.Data.Test.Features.Employees
{
    [TestFixture]
    public class EmployeeRepositoryTest
    {
        private IEmployeeRepository _repository;

        [SetUp]
        public void Initialize()
        {
            Base.SeedDatabase();
            _repository = new EmployeeRepository();
        }

        [Test]
        public void EmployeeRepository_Save_ShouldBeOk()
        {
            Employee employee = ObjectMotherEmployee.GetEmployee();
            Employee result = _repository.Save(employee);
            result.Should().NotBeNull();
            result.Id.Should().BeGreaterThan(0);
        }

        [Test]
        public void EmployeeRepository_Save_InvalidNullOrEmptyName_ShouldBeFail()
        {
            Employee employee = ObjectMotherEmployee.GetEmployeeWithEmptyName();
            Action executeAction = () => _repository.Save(employee);
            executeAction.Should().Throw<EmployeeEmptyOrNullNameException>();
        }

        [Test]
        public void EmployeeRepository_Update_InvalidNullOrEmptyName_ShouldBeFail()
        {
            Employee employee = ObjectMotherEmployee.GetEmployeeWithEmptyName();
            employee.Id = 1;
            Action executeAction = () => _repository.Update(employee);
            executeAction.Should().Throw<EmployeeEmptyOrNullNameException>();
        }

        [Test]
        public void EmployeeRepository_Update_InvalidId_ShouldBeFail()
        {
            Employee employee = ObjectMotherEmployee.GetEmployee();
            Action executeAction = () => _repository.Update(employee);
            executeAction.Should().Throw<IdentifierUndefinedException>();
        }

        [Test]
        public void EmployeeRepository_Delete_ShouldBeOk()
        {
            Employee employee = _repository.Get(1);
            _repository.Delete(employee);
            Employee result = _repository.Get(1);
            result.Should().BeNull();
        }

        [Test]
        public void EmployeeRepository_Get_ShouldBeOk()
        {
            Employee result = _repository.Get(1);
            result.Should().NotBeNull();
            result.Id.Should().Be(1);
        }

        [Test]
        public void EmployeeRepository_Get_InvalidId_ShouldBeFail()
        {
            Action executeAction = () => _repository.Get(0);
            executeAction.Should().Throw<IdentifierUndefinedException>();
        }

        [Test]
        public void EmployeeRepository_GetAll_ShouldBeOk()
        {
            IEnumerable<Employee> employees = _repository.GetAll();
            employees.Count().Should().BeGreaterThan(0);
        }
    }
}
