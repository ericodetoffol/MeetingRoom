using MeetingRoom.Domain.Exceptions;
using MeetingRoom.Domain.Features.Schedulings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeetingRoom.Application.Features.Schedulings
{
    public class SchedulingService : ISchedulingService
    {
        private ISchedulingRepository _repository;

        public SchedulingService(ISchedulingRepository repository)
        {
            _repository = repository;
        }

        public Scheduling Add(Scheduling scheduling)
        {
            scheduling.Validate();
            return _repository.Save(scheduling);
        }

        public Scheduling Update(Scheduling scheduling)
        {
            if (scheduling.Id < 1)
                throw new IdentifierUndefinedException();
            scheduling.Validate();
            return _repository.Update(scheduling);
        }

        public void Delete(Scheduling scheduling)
        {
            if (scheduling.Id < 1)
                throw new IdentifierUndefinedException();
            _repository.Delete(scheduling);
        }

        public Scheduling Get(long id)
        {
            if (id < 1)
                throw new IdentifierUndefinedException();
            return _repository.Get(id);
        }

        public IEnumerable<Scheduling> GetAll()
        {
            return _repository.GetAll();
        }
    }
}
