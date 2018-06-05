using MeetingRoom.Domain.Features.Schedulings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeetingRoom.Common.Test.Features.Schedulings
{
    public static partial class ObjectMotherScheduling
    {
        public static Scheduling GetScheduling()
        {
            return new Scheduling()
            {
                StartTime = new DateTime(2018, 6, 10, 7, 0, 0),
                EndTime = new DateTime(2018, 6, 10, 10, 0, 0)
            };
        }

        public static Scheduling GetSchedulingInvalidStartTime()
        {
            return new Scheduling()
            {
                StartTime = new DateTime(2018, 6, 2, 7, 0, 0),
                EndTime = new DateTime(2018, 6, 10, 10, 0, 0)
            };
        }

        public static Scheduling GetSchedulingInvalidEndTime()
        {
            return new Scheduling()
            {
                StartTime = new DateTime(2018, 6, 10, 7, 0, 0),
                EndTime = new DateTime(2018, 6, 9, 10, 0, 0)
            };
        }
    }
}
