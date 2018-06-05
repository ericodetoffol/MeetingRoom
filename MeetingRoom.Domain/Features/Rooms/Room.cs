using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeetingRoom.Domain.Features.Rooms
{
    public class Room
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public int NumberOfSeats { get; set; }
        public bool Disponibility { get; set; }

        public void Validate()
        {
            if (string.IsNullOrEmpty(Name))
                throw new RoomEmptyOrNullNameException();
            if (NumberOfSeats < 1)
                throw new RoomInvalidNumberOfSeatsException();
        }
    }
}
