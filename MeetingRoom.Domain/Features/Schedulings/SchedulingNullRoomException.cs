using MeetingRoom.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeetingRoom.Domain.Features.Schedulings
{
    public class SchedulingNullRoomException : BusinessException
    {
        public SchedulingNullRoomException() : base("Sala não pode ser nula!")
        {
        }
    }
}
