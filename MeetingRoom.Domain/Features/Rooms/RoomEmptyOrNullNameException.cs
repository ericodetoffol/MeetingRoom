using MeetingRoom.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeetingRoom.Domain.Features.Rooms
{
    public class RoomEmptyOrNullNameException : BusinessException
    {
        public RoomEmptyOrNullNameException() : base("Nome da sala não pode ser vazio!")
        {
        }
    }
}
