using MeetingRoom.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeetingRoom.Domain.Features.Employees
{
    public class EmployeeEmptyOrNullTelephoneExtensionException : BusinessException
    {
        public EmployeeEmptyOrNullTelephoneExtensionException() : base("Ramal do funcionário está vazio!")
        {

        }
    }
}
