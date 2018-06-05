using MeetingRoom.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeetingRoom.Domain.Features.Rooms
{
    public class RoomInvalidNumberOfSeatsException : BusinessException
    {
        public RoomInvalidNumberOfSeatsException() : base("Número de lugares deve ser maior de 1.")
        {

        }
    }
}
