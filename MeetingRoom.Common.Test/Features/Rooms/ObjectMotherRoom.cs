using MeetingRoom.Domain.Features.Rooms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeetingRoom.Common.Test.Features.Rooms
{
    public static partial class ObjectMotherRoom
    {
        public static Room GetRoom()
        {
            return new Room()
            {
                Name = "Test",
                NumberOfSeats = 30
            };
        }

        public static Room GetRoomWithEmptyName()
        {
            return new Room()
            {
                Name = "",
                NumberOfSeats = 30
            };
        }

        public static Room GetRoomWithInvalidNumberOfSeats()
        {
            return new Room()
            {
                Name = "Test",
                NumberOfSeats = 0
            };
        }
    }
}
