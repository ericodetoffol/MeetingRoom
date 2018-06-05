using MeetingRoom.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeetingRoom.Domain.Features.Schedulings
{
    public class SchedulingStartTimeOverFlowException : BusinessException
    {
        public SchedulingStartTimeOverFlowException() : base("Data inicial é maior que a atual!")
        {
        }
    }
}
