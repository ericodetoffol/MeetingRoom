using MeetingRoom.Domain.Exceptions;
using MeetingRoom.Domain.Features.Employees;
using MeetingRoom.Infrastructure;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeetingRoom.Infra.Data.Features.Employees
{
    public class EmployeeRepository : IEmployeeRepository
    {
        public Employee Save(Employee employee)
        {
            employee.Validate();
            string sql = "INSERT INTO TBEmployee(Name,EmployeePosition,TelephoneExtension) " +
            "VALUES (@Name, @EmployeePosition, @TelephoneExtension)";
            employee.Id = Db.Insert(sql, Take(employee, false));
            return employee;
        }

        public Employee Update(Employee employee)
        {
            if (employee.Id < 1)
                throw new IdentifierUndefinedException();
            employee.Validate();
            string sql = "UPDATE TBEmployee SET Name = @Name, EmployeePosition = @EmployeePosition, TelephoneExtension = @TelephoneExtension WHERE Id = @Id";
            Db.Update(sql, Take(employee));
            return employee;
        }

        public Employee Get(long id)
        {
            if (id < 1)
                throw new IdentifierUndefinedException();
            string sql = "SELECT * FROM TBEmployee WHERE Id = @Id";
            return Db.Get(sql, Make, new object[] { "Id", id });
        }

        public IEnumerable<Employee> GetAll()
        {
            string sql = "SELECT * FROM TBEmployee";
            return Db.GetAll(sql, Make);
        }

        public bool Delete(Employee employee)
        {
            if (employee.Id < 1)
                throw new IdentifierUndefinedException();
            string sql = "DELETE FROM TBEmployee WHERE Id = @Id";
            Db.Delete(sql, new object[] { "Id", employee.Id });
            return true;
        }

        private static Func<IDataReader, Employee> Make = reader =>
           new Employee
           {
               Id = Convert.ToInt32(reader["Id"]),
               Name = reader["Name"].ToString(),
               EmployeePosition = reader["EmployeePosition"].ToString(),
               TelephoneExtension = reader["TelephoneExtension"].ToString()
           };

        private object[] Take(Employee employee, bool hasId = true)
        {
            object[] parametros = null;

            if (hasId)
            {
                parametros = new object[]
                    {
                "@Name", employee.Name,
                "@EmployeePosition", employee.EmployeePosition,
                "@TelephoneExtension", employee.TelephoneExtension,
                "@Id", employee.Id,
                    };
            }
            else
            {
                parametros = new object[]
              {
                "@Name", employee.Name,
                "@EmployeePosition", employee.EmployeePosition,
                "@TelephoneExtension", employee.TelephoneExtension 
              };
            }
            return parametros;
        }
    }
}
