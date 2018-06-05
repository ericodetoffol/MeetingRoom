using FluentAssertions;
using MeetingRoom.Common.Test.Features.Rooms;
using MeetingRoom.Domain.Features.Rooms;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeetingRoom.Features.Rooms
{
    [TestFixture]
    public class RoomDomainTest
    {
        [Test]
        public void Test_Room_Valid_ShouldBeOk()
        {
            Room room = ObjectMotherRoom.GetRoom();
            room.Id = 0;
            Action comparison = room.Validate;
            comparison.Should().NotThrow<Exception>();
        }

        [Test]
        public void Test_Room_InvalidNumberOfSeats_ShouldBeFail()
        {
            Room room = ObjectMotherRoom.GetRoomWithInvalidNumberOfSeats();
            room.Id = 0;
            Action comparison = room.Validate;
            comparison.Should().Throw<RoomInvalidNumberOfSeatsException>();
        }

        [Test]
        public void Test_Room_InvalidEmptyOrNullName_ShouldBeFail()
        {
            Room room = ObjectMotherRoom.GetRoomWithEmptyName();
            room.Id = 0;
            Action comparison = room.Validate;
            comparison.Should().Throw<RoomEmptyOrNullNameException>();
        }
    }
}
