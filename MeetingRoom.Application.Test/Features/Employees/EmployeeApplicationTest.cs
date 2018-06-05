using FluentAssertions;
using MeetingRoom.Application.Features.Employees;
using MeetingRoom.Common.Test.Features.Employees;
using MeetingRoom.Domain.Exceptions;
using MeetingRoom.Domain.Features.Employees;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeetingRoom.Application.Test.Features.Employees
{
    [TestFixture]
    public class EmployeeApplicationTest
    {
        private IEmployeeService _service;
        private Mock<IEmployeeRepository> _mockRepository;

        [SetUp]
        public void Initialize()
        {
            _mockRepository = new Mock<IEmployeeRepository>();
            _service = new EmployeeService(_mockRepository.Object);
        }

        [Test]
        public void EmployeeService_Save_ShouldBeOk()
        {
            Employee employee = ObjectMotherEmployee.GetEmployee();
            _mockRepository
                .Setup(m => m.Save(employee))
                .Returns(new Employee { Id = 1 });
            Employee result = _service.Add(employee);
            result.Should().NotBeNull();
            result.Id.Should().BeGreaterThan(0);
            _mockRepository.Verify(m => m.Save(employee));
        }

        [Test]
        public void EmployeeService_Save_InvalidEmptyOrNullName_ShouldBeFail()
        {
            Employee employee = ObjectMotherEmployee.GetEmployeeWithEmptyName();
            Action executeAction = () => _service.Add(employee);
            executeAction.Should().Throw<EmployeeEmptyOrNullNameException>();
            _mockRepository.VerifyNoOtherCalls();
        }

        [Test]
        public void EmployeeService_Delete_ShouldBeOk()
        {
            Employee employee = ObjectMotherEmployee.GetEmployee();
            employee.Id = 1;
            _mockRepository
                .Setup(m => m.Delete(employee));
            _service.Delete(employee);
            _mockRepository.Verify(m => m.Delete(employee));
        }

        [Test]
        public void EmployeeService_Delete_InvalidId_ShouldBeOk()
        {
            Employee employee = ObjectMotherEmployee.GetEmployee();
            _mockRepository
                .Setup(m => m.Delete(employee));
            Action executeAction = () => _service.Delete(employee);
            executeAction.Should().Throw<IdentifierUndefinedException>();
            _mockRepository.VerifyNoOtherCalls();
        }

        [Test]
        public void EmployeeService_Update_ShouldBeOk()
        {
            Employee employee = ObjectMotherEmployee.GetEmployee();
            employee.Id = 1;
            _mockRepository
                .Setup(m => m.Update(employee))
                .Returns(new Employee { Id = 1, Name = "Test" });
            Employee result = _service.Update(employee);
            result.Should().NotBeNull();
            result.Id.Should().BeGreaterThan(0);
            _mockRepository.Verify(m => m.Update(employee));
        }

        [Test]
        public void EmployeeService_Update_InvalidId_ShouldBeFail()
        {
            Employee employee = ObjectMotherEmployee.GetEmployee();
            Action executeAction = () => _service.Update(employee);
            executeAction.Should().Throw<IdentifierUndefinedException>();
            _mockRepository.VerifyNoOtherCalls();
        }

        [Test]
        public void EmployeeService_Update_InvalidEmptyOrNullBranch_ShouldBeFail()
        {
            Employee employee = ObjectMotherEmployee.GetEmployeeWithEmptyTelephoneExtension();
            employee.Id = 1;
            Action executeAction = () => _service.Update(employee);
            executeAction.Should().Throw<EmployeeEmptyOrNullTelephoneExtensionException>();
            _mockRepository.VerifyNoOtherCalls();
        }

        [Test]
        public void EmployeeService_Get_ShouldBeOk()
        {
            int id = 1;
            _mockRepository
                .Setup(m => m.Get(id))
                .Returns(ObjectMotherEmployee.GetEmployee());
            Employee employee = _service.Get(id);
            employee.Should().NotBeNull();
            _mockRepository.Verify(m => m.Get(id));
        }

        [Test]
        public void EmployeeService_Get_InvalidId_ShouldBeOk()
        {
            Action executeAction = () => _service.Get(0);
            executeAction.Should().Throw<IdentifierUndefinedException>();
            _mockRepository.VerifyNoOtherCalls();
        }

        [Test]
        public void EmployeeService_GetAll_ShouldBeOk()
        {
            _mockRepository
                .Setup(m => m.GetAll())
                .Returns(new List<Employee>()
                        {
                            new Employee { Id = 1 },
                            new Employee { Id = 2 },
                            new Employee { Id = 3 }
                        });
            IEnumerable<Employee> employees = _service.GetAll();
            employees.Count().Should().Equals(3);
            _mockRepository.Verify(m => m.GetAll());
        }
    }
}