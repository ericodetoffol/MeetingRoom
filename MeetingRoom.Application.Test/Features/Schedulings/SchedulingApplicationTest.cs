using FluentAssertions;
using MeetingRoom.Application.Features.Schedulings;
using MeetingRoom.Common.Test.Features.Schedulings;
using MeetingRoom.Domain.Exceptions;
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

namespace MeetingRoom.Application.Test.Features.Schedulings
{
    [TestFixture]
    public class SchedulingApplicationTest
    {
        private ISchedulingService _service;
        private Mock<ISchedulingRepository> _mockRepository;
        private Mock<Employee> _mockEmployee;
        private Mock<Room> _mockRoom;

        [SetUp]
        public void Initialize()
        {
            _mockRepository = new Mock<ISchedulingRepository>();
            _service = new SchedulingService(_mockRepository.Object);
            _mockEmployee = new Mock<Employee>();
            _mockRoom = new Mock<Room>();
        }

        [Test]
        public void SchedulingService_Add_ShouldBeOk()
        {
            Scheduling scheduling = ObjectMotherScheduling.GetScheduling();
            scheduling.Employee = _mockEmployee.Object;
            scheduling.Room = _mockRoom.Object;
            _mockRoom.Object.Disponibility = true;
            _mockRepository
                .Setup(m => m.Save(scheduling))
                .Returns(new Scheduling { Id = 1 });
            Scheduling result = _service.Add(scheduling);
            result.Should().NotBeNull();
            result.Id.Should().BeGreaterThan(0);
            _mockRepository.Verify(m => m.Save(scheduling));
        }

        [Test]
        public void SchedulingService_Save_InvalidEmptyOrNullName_ShouldBeFail()
        {
            Scheduling scheduling = ObjectMotherScheduling.GetSchedulingInvalidStartTime();
            scheduling.Employee = _mockEmployee.Object;
            scheduling.Room = _mockRoom.Object;
            Action executeAction = () => _service.Add(scheduling);
            executeAction.Should().Throw<SchedulingStartTimeOverFlowException>();
            _mockRepository.VerifyNoOtherCalls();
        }

        [Test]
        public void SchedulingService_Update_ShouldBeOk()
        {
            Scheduling scheduling = ObjectMotherScheduling.GetScheduling();
            scheduling.Id = 1;
            scheduling.Employee = _mockEmployee.Object;
            scheduling.Room = _mockRoom.Object;
            _mockRoom.Object.Disponibility = true;
            _mockRepository
                .Setup(m => m.Update(scheduling))
                .Returns(new Scheduling { Id = 1 });
            Scheduling result = _service.Update(scheduling);
            result.Should().NotBeNull();
            result.Id.Should().BeGreaterThan(0);
            _mockRepository.Verify(m => m.Update(scheduling));
        }

        [Test]
        public void SchedulingService_Update_InvalidEndTimeLessThanStartTime_ShouldBeFail()
        {
            Scheduling scheduling = ObjectMotherScheduling.GetSchedulingInvalidEndTime();
            scheduling.Id = 1;
            scheduling.Employee = _mockEmployee.Object;
            scheduling.Room = _mockRoom.Object;
            Action executeAction = () => _service.Update(scheduling);
            executeAction.Should().Throw<SchedulingEndTimeLessThanStartTimeException>();
            _mockRepository.VerifyNoOtherCalls();
        }

        [Test]
        public void SchedulingService_Update_InvalidId_ShouldBeFail()
        {
            Scheduling scheduling = ObjectMotherScheduling.GetScheduling();
            scheduling.Employee = _mockEmployee.Object;
            scheduling.Room = _mockRoom.Object;
            Action executeAction = () => _service.Update(scheduling);
            executeAction.Should().Throw<IdentifierUndefinedException>();
            _mockRepository.VerifyNoOtherCalls();
        }

        [Test]
        public void SchedulingService_Delete_ShouldBeOk()
        {
            Scheduling scheduling = ObjectMotherScheduling.GetScheduling();
            scheduling.Id = 1;
            _mockRepository
                .Setup(m => m.Delete(scheduling));
            _service.Delete(scheduling);
            _mockRepository.Verify(m => m.Delete(scheduling));
        }

        [Test]
        public void SchedulingService_Delete_InvalidId_ShouldBeFail()
        {
            Scheduling scheduling = ObjectMotherScheduling.GetScheduling();
            _mockRepository
                .Setup(m => m.Delete(scheduling));
            Action executeAction = () => _service.Delete(scheduling);
            executeAction.Should().Throw<IdentifierUndefinedException>();
            _mockRepository.VerifyNoOtherCalls();
        }

        [Test]
        public void SchedulingService_Get_ShouldBeOk()
        {
            int id = 1;
            _mockRepository
                .Setup(m => m.Get(id))
                .Returns(ObjectMotherScheduling.GetScheduling());
            Scheduling result = _service.Get(id);
            result.Should().NotBeNull();
            _mockRepository.Verify(m => m.Get(id));
        }

        [Test]
        public void SchedulingService_Get_InvalidId_ShouldBeFail()
        {
            Action executeAction = () => _service.Get(0);
            executeAction.Should().Throw<IdentifierUndefinedException>();
            _mockRepository.VerifyNoOtherCalls();
        }

        [Test]
        public void SchedulingService_GetAll_ShouldBeOk()
        {
            _mockRepository
                .Setup(m => m.GetAll())
                .Returns(new List<Scheduling>()
                        {
                            new Scheduling { Id = 1 },
                            new Scheduling { Id = 2 },
                            new Scheduling { Id = 3 }
                        });
            IEnumerable<Scheduling> schedulings = _service.GetAll();
            schedulings.Count().Should().Equals(3);
            _mockRepository.Verify(m => m.GetAll());
        }
    }
}
