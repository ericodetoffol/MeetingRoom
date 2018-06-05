using MeetingRoom.Domain.Features.Schedulings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeetingRoom.Application.Features.Schedulings
{
    public interface ISchedulingService
    {
        Scheduling Add(Scheduling scheduling);
        Scheduling Update(Scheduling scheduling);
        Scheduling Get(long id);
        IEnumerable<Scheduling> GetAll();
        void Delete(Scheduling scheduling);
    }
}
