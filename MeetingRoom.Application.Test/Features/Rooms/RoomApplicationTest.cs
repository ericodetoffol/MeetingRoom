using FluentAssertions;
using MeetingRoom.Application.Features.Rooms;
using MeetingRoom.Common.Test.Features.Rooms;
using MeetingRoom.Domain.Exceptions;
using MeetingRoom.Domain.Features.Rooms;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeetingRoom.Application.Test.Features.Rooms
{
    [TestFixture]
    public class RoomApplicationTest
    {
        private IRoomService _service;
        private Mock<IRoomRepository> _mockRepository;

        [SetUp]
        public void Initialize()
        {
            _mockRepository = new Mock<IRoomRepository>();
            _service = new RoomService(_mockRepository.Object);
        }

        [Test]
        public void RoomService_Save_ShouldBeOk()
        {
            Room room = ObjectMotherRoom.GetRoom();
            _mockRepository
                .Setup(m => m.Save(room))
                .Returns(new Room { Id = 1 });
            Room result = _service.Add(room);
            result.Should().NotBeNull();
            result.Id.Should().BeGreaterThan(0);
            _mockRepository.Verify(m => m.Save(room));
        }

        [Test]
        public void RoomService_Save_InvalidEmptyOrNullName_ShouldBeFail()
        {
            Room room = ObjectMotherRoom.GetRoomWithEmptyName();
            Action executeAction = () => _service.Add(room);
            executeAction.Should().Throw<RoomEmptyOrNullNameException>();
            _mockRepository.VerifyNoOtherCalls();
        }

        [Test]
        public void RoomService_Delete_ShouldBeOk()
        {
            Room room = ObjectMotherRoom.GetRoom();
            room.Id = 1;
            _mockRepository
                .Setup(m => m.Delete(room));
            _service.Delete(room);
            _mockRepository.Verify(m => m.Delete(room));
        }

        [Test]
        public void RoomService_Delete_InvalidId_ShouldBeFail()
        {
            Room room = ObjectMotherRoom.GetRoom();
            _mockRepository
                .Setup(m => m.Delete(room));
            Action executeAction = () => _service.Delete(room);
            executeAction.Should().Throw<IdentifierUndefinedException>();
            _mockRepository.VerifyNoOtherCalls();
        }

        [Test]
        public void RoomService_Update_ShouldBeOk()
        {
            Room room = ObjectMotherRoom.GetRoom();
            room.Id = 1;
            _mockRepository
                .Setup(m => m.Update(room))
                .Returns(new Room { Id = 1, Name = "Treinamento" });
            Room result = _service.Update(room);
            result.Should().NotBeNull();
            result.Id.Should().BeGreaterThan(0);
            _mockRepository.Verify(m => m.Update(room));
        }

        [Test]
        public void RoomService_Update_InvalidNumberOfSeats_ShouldBeFail()
        {
            Room room = ObjectMotherRoom.GetRoomWithInvalidNumberOfSeats();
            room.Id = 1;
            Action executeAction = () => _service.Update(room);
            executeAction.Should().Throw<RoomInvalidNumberOfSeatsException>();
            _mockRepository.VerifyNoOtherCalls();
        }

        [Test]
        public void RoomService_Update_InvalidId_ShouldBeFail()
        {
            Room room = ObjectMotherRoom.GetRoom();
            Action executeAction = () => _service.Update(room);
            executeAction.Should().Throw<IdentifierUndefinedException>();
            _mockRepository.VerifyNoOtherCalls();
        }

        [Test]
        public void RoomService_Get_ShouldBeOk()
        {
            int id = 1;
            _mockRepository
                .Setup(m => m.Get(id))
                .Returns(ObjectMotherRoom.GetRoom());
            Room result = _service.Get(id);
            result.Should().NotBeNull();
            _mockRepository.Verify(m => m.Get(id));
        }

        [Test]
        public void RoomService_Get_InvalidId_ShouldBeFail()
        {
            Action executeAction = () => _service.Get(0);
            executeAction.Should().Throw<IdentifierUndefinedException>();
            _mockRepository.VerifyNoOtherCalls();
        }

        [Test]
        public void RoomService_GetAll_ShouldBeOk()
        {
            _mockRepository
                .Setup(m => m.GetAll())
                .Returns(new List<Room>()
                        {
                            new Room { Id = 1 },
                            new Room { Id = 2 },
                            new Room { Id = 3 }
                        });
            IEnumerable<Room> rooms = _service.GetAll();
            rooms.Count().Should().Equals(3);
            _mockRepository.Verify(m => m.GetAll());
        }
    }
}
