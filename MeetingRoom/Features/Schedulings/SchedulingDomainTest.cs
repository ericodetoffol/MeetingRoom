using FluentAssertions;
using MeetingRoom.Common.Test.Features.Schedulings;
using MeetingRoom.Domain.Features.Employees;
using MeetingRoom.Domain.Features.Rooms;
using MeetingRoom.Domain.Features.Schedulings;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeetingRoom.Features.Schedulings
{
    [TestFixture]
    public class SchedulingDomainTest
    {
        private Mock<Employee> _mockEmployee;
        private Mock<Room> _mockRoom;

        [SetUp]
        public void Initialize()
        {
            _mockEmployee = new Mock<Employee>();
            _mockRoom = new Mock<Room>();
        }

        [Test]
        public void Scheduling_Valid_ShouldBeOk()
        {
            Scheduling scheduling = ObjectMotherScheduling.GetScheduling();
            scheduling.Id = 0;
            scheduling.Employee = _mockEmployee.Object;
            scheduling.Room = _mockRoom.Object;
            _mockRoom.Object.Disponibility = true;
            Action comparison = scheduling.Validate;
            comparison.Should().NotThrow<Exception>();
        }

        [Test]
        public void Scheduling_InvalidStartTimeOverFlow_ShouldBeFail()
        {
            Scheduling scheduling = ObjectMotherScheduling.GetSchedulingInvalidStartTime();
            scheduling.Id = 0;
            scheduling.Employee = _mockEmployee.Object;
            scheduling.Room = _mockRoom.Object;
            _mockRoom.Object.Disponibility = true;
            Action comparison = scheduling.Validate;
            comparison.Should().Throw<SchedulingStartTimeOverFlowException>();
        }


        [Test]
        public void Scheduling_InvalidNullRoom_ShouldBeFail()
        {
            Scheduling scheduling = ObjectMotherScheduling.GetScheduling();
            scheduling.Id = 0;
            scheduling.Employee = _mockEmployee.Object;
            _mockRoom.Object.Disponibility = true;
            Action comparison = scheduling.Validate;
            comparison.Should().Throw<SchedulingNullRoomException>();
        }

        [Test]
        public void Scheduling_UnavailableRoom_ShouldBeFail()
        {
            Scheduling scheduling = ObjectMotherScheduling.GetScheduling();
            scheduling.Id = 0;
            scheduling.Employee = _mockEmployee.Object;
            scheduling.Room = _mockRoom.Object;
            _mockRoom.Object.Disponibility = false;
            Action comparison = scheduling.Validate;
            comparison.Should().Throw<SchedulingUnavailableRoomException>();
        }

        [Test]
        public void Scheduling_InvalidEndTimeLessThanStartTime_ShouldBeFail()
        {
            Scheduling scheduling = ObjectMotherScheduling.GetSchedulingInvalidEndTime();
            scheduling.Id = 0;
            scheduling.Employee = _mockEmployee.Object;
            scheduling.Room = _mockRoom.Object;
            _mockRoom.Object.Disponibility = true;
            Action comparison = scheduling.Validate;
            comparison.Should().Throw<SchedulingEndTimeLessThanStartTimeException>();
        }

        [Test]
        public void Scheduling_InvalidNullEmployee_ShouldBeFail()
        {
            Scheduling scheduling = ObjectMotherScheduling.GetScheduling();
            scheduling.Id = 0;
            scheduling.Room = _mockRoom.Object;
            _mockRoom.Object.Disponibility = true;
            Action comparison = scheduling.Validate;
            comparison.Should().Throw<SchedulingNullEmployeeException>();
        }
    }
}
