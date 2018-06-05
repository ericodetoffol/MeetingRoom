using MeetingRoom.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeetingRoom.Domain.Features.Employees
{
    public class EmployeeEmptyOrNullNameException : BusinessException
    {
        public EmployeeEmptyOrNullNameException() : base("Nome do Funcionario está vazio!")
        {
        }
    }
}
