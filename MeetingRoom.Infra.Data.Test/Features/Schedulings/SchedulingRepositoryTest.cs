using FluentAssertions;
using MeetingRoom.Common.Test.Base;
using MeetingRoom.Common.Test.Features.Schedulings;
using MeetingRoom.Domain.Exceptions;
using MeetingRoom.Domain.Features.Employees;
using MeetingRoom.Domain.Features.Rooms;
using MeetingRoom.Domain.Features.Schedulings;
using MeetingRoom.Infra.Data.Features.Schedulings;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeetingRoom.Infra.Data.Test.Features.Schedulings
{
    [TestFixture]
    public class SchedulingRepositoryTest
    {
        private ISchedulingRepository _repository;
        private Mock<Employee> _mockEmployee;
        private Mock<Room> _mockRoom;

        [SetUp]
        public void Initialize()
        {
            Base.SeedDatabase();
            _repository = new SchedulingRepository();
            _mockEmployee = new Mock<Employee>();
            _mockRoom = new Mock<Room>();
        }

        [Test]
        public void SchedulingRepository_Save_ShouldBeOk()
        {
            Scheduling scheduling = ObjectMotherScheduling.GetScheduling();
            scheduling.Employee = _mockEmployee.Object;
            _mockEmployee.Object.Id = 1;
            scheduling.Room = _mockRoom.Object;
            _mockRoom.Object.Id = 1;
            _mockRoom.Object.Disponibility = true;
            Scheduling result = _repository.Save(scheduling);
            result.Should().NotBeNull();
            result.Id.Should().BeGreaterThan(0);
        }

        [Test]
        public void SchedulingRepository_Save_InvalidNullRoom_ShouldBeFail()
        {
            Scheduling scheduling = ObjectMotherScheduling.GetScheduling();
            scheduling.Employee = _mockEmployee.Object;
            _mockEmployee.Object.Id = 1;
            Action comparison = () => _repository.Save(scheduling);
            comparison.Should().Throw<SchedulingNullRoomException>();
        }

        [Test]
        public void SchedulingRepository_Update_ShouldBeOk()
        {
            Scheduling scheduling = _repository.Get(1);
            DateTime oldStartTime = scheduling.StartTime;
            scheduling.StartTime = new DateTime(2018, 6, 10, 7, 0, 0);
            scheduling.Room.Disponibility = true;
            Scheduling result = _repository.Update(scheduling);
            result.Should().NotBeNull();
            result.Id.Should().BeGreaterThan(0);
            result.StartTime.Should().NotBe(oldStartTime);
        }

        [Test]
        public void SchedulingRepository_Update_InvalidId_ShouldBeFail()
        {
            Scheduling scheduling = ObjectMotherScheduling.GetScheduling();
            Action comparison = () => _repository.Update(scheduling);
            comparison.Should().Throw<IdentifierUndefinedException>();
        }

        [Test]
        public void SchedulingRepository_Get_ShouldBeOk()
        {
            Scheduling result = _repository.Get(1);
            result.Should().NotBeNull();
        }

        [Test]
        public void SchedulingRepository_Get_InvalidId_ShouldBeFail()
        {
            Action comparison = () => _repository.Get(0);
            comparison.Should().Throw<IdentifierUndefinedException>();
        }

        [Test]
        public void SchedulingRepository_GetAll_ShouldBeOk()
        {
            var result = _repository.GetAll();
            result.Should().NotBeNull();
        }

        [Test]
        public void SchedulingRepository_Delete_ShouldBeOk()
        {
            Scheduling scheduling = _repository.Get(1);
            _repository.Delete(scheduling);
            Scheduling result = _repository.Get(1);
            result.Should().BeNull();
        }

        [Test]
        public void SchedulingRepository_Delete_InvalidId_ShouldBeFail()
        {
            Scheduling scheduling = ObjectMotherScheduling.GetScheduling();
            Action comparison = () => _repository.Delete(scheduling);
            comparison.Should().Throw<IdentifierUndefinedException>();
        }
    }
}
