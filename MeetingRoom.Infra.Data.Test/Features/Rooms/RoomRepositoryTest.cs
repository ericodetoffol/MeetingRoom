using FluentAssertions;
using MeetingRoom.Common.Test.Base;
using MeetingRoom.Common.Test.Features.Rooms;
using MeetingRoom.Domain.Exceptions;
using MeetingRoom.Domain.Features.Rooms;
using MeetingRoom.Infra.Data.Features.Rooms;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeetingRoom.Infra.Data.Test.Features.Rooms
{
    [TestFixture]
    public class RoomRepositoryTest
    {
        private IRoomRepository _repository;

        [SetUp]
        public void Initialize()
        {
            Base.SeedDatabase();
            _repository = new RoomRepository();
        }

        [Test]
        public void RoomRepository_Save_ShouldBeOk()
        {
            Room room = ObjectMotherRoom.GetRoom();
            Room result = _repository.Save(room);
            result.Should().NotBeNull();
            result.Id.Should().BeGreaterThan(0);
        }

        [Test]
        public void RoomRepository_Save_InvalidNullOrEmptyName_ShouldBeFail()
        {
            Room room = ObjectMotherRoom.GetRoomWithEmptyName();
            Action executeAction = () => _repository.Save(room);
            executeAction.Should().Throw<RoomEmptyOrNullNameException>();
        }

        [Test]
        public void RoomRepository_Update_ShouldBeOk()
        {
            Room room = _repository.Get(1);
            string oldName = room.Name;
            room.Name = "Test";
            Room result = _repository.Update(room);
            result.Should().NotBeNull();
            result.Id.Should().BeGreaterThan(0);
            result.Name.Should().NotBe(oldName);
        }

        [Test]
        public void RoomRepository_Update_InvalidNumberOfSeats_ShouldBeFail()
        {
            Room room = ObjectMotherRoom.GetRoomWithInvalidNumberOfSeats();
            room.Id = 1;
            Action executeAction = () => _repository.Update(room);
            executeAction.Should().Throw<RoomInvalidNumberOfSeatsException>();
        }

        [Test]
        public void RoomRepository_Update_InvalidId_ShouldBeFail()
        {
            Room room = ObjectMotherRoom.GetRoom();
            Action executeAction = () => _repository.Update(room);
            executeAction.Should().Throw<IdentifierUndefinedException>();
        }

        [Test]
        public void RoomRepository_Delete_ShouldBeOk()
        {
            Room room = _repository.Get(1);
            _repository.Delete(room);
            Room result = _repository.Get(1);
            result.Should().BeNull();
        }

        [Test]
        public void RoomRepository_Delete_InvalidId_ShouldBeOk()
        {
            Room room = ObjectMotherRoom.GetRoom();
            Action executeAction = () => _repository.Delete(room);
            executeAction.Should().Throw<IdentifierUndefinedException>();
        }

        [Test]
        public void RoomRepository_Get_ShouldBeOk()
        {
            Room result = _repository.Get(1);
            result.Should().NotBeNull();
            result.Id.Should().Be(1);
        }

        [Test]
        public void RoomRepository_Get_InvalidId_ShouldBeFail()
        {
            Action executeAction = () => _repository.Get(0);
            executeAction.Should().Throw<IdentifierUndefinedException>();
        }

        [Test]
        public void RoomRepository_GetAll_ShouldBeOk()
        {
            IEnumerable<Room> rooms = _repository.GetAll();
            rooms.Count().Should().BeGreaterThan(0);
        }
    }
}
