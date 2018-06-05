using MeetingRoom.Domain.Features.Employees;
using MeetingRoom.Domain.Features.Rooms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeetingRoom.Domain.Features.Schedulings
{
    public class Scheduling
    {
        public long Id { get; set; }
        public Employee Employee { get; set; }
        public Room Room { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        

        public void Validate()
        {
            if (CompareStartTimeSmallerCurrent(StartTime))
                throw new SchedulingStartTimeOverFlowException();
            if (CompareEndTimeBiggerStartTime())
                throw new SchedulingEndTimeLessThanStartTimeException();
            if (Employee == null)
                throw new SchedulingNullEmployeeException();
            if (Room == null)
                throw new SchedulingNullRoomException();
            if (Room.Disponibility == false)
                throw new SchedulingUnavailableRoomException();
        }

        public bool CheckBusyTimeStartTime(List<Scheduling> schedulings)
        {
            foreach (Scheduling s in schedulings)
            {
                if (s.StartTime.Day == StartTime.Day)
                {
                    if (s.StartTime.Hour == StartTime.Hour)
                    {
                        return true;
                    }
                    if (CompareSmallerEndTime(s.StartTime))
                    { 
                        return true;
                    }
                }
            }
            return false;
        }

        public bool CompareEndTimeBiggerStartTime()
        {
            int result = DateTime.Compare(EndTime, StartTime);
            if (result <= 0)
            {
                return true;
            }
            return false;
        }

        public bool CompareStartTimeSmallerCurrent(DateTime dt)
        {
            int result = DateTime.Compare(dt, DateTime.Now);
            if (result <= 0)
            {
                return true;
            }
            return false;
        }

        public bool CompareSmallerEndTime(DateTime dt)
        {
            if (dt.Hour < EndTime.Hour)
            {
                return true;
            }
            return false;
        }

        public bool CheckBusyTimeEndTime(List<Scheduling> schedulings)
        {
            foreach (Scheduling s in schedulings)
            {
                if (s.EndTime.Day == EndTime.Day)
                {
                    if (EndTime.Hour > s.StartTime.Hour)
                    {
                        return true;
                    }
                }
            }
            return false;
        }
    }
}
