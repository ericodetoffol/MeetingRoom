﻿using MeetingRoom.Domain.Exceptions;
using MeetingRoom.Domain.Features.Rooms;
using MeetingRoom.Infrastructure;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeetingRoom.Infra.Data.Features.Rooms
{
    public class RoomRepository : IRoomRepository
    {
        public Room Save(Room room)
        {
            room.Validate();
            string sql = "INSERT INTO TBRoom (Name,NumberOfSeats,Disponibility) VALUES (@Name, @NumberOfSeats, @Disponibility)";
            room.Id = Db.Insert(sql, Take(room, false));
            return room;
        }

        public Room Update(Room room)
        {
            if (room.Id < 1)
                throw new IdentifierUndefinedException();
            room.Validate();
            string sql = "UPDATE TBRoom SET Name = @Name, NumberOfSeats = @NumberOfSeats, Disponibility = @Disponibility WHERE Id = @Id";
            Db.Update(sql, Take(room));
            return room;
        }

        public Room Get(long id)
        {
            if (id < 1)
                throw new IdentifierUndefinedException();
            string sql = "SELECT * FROM TBRoom WHERE Id = @Id";
            return Db.Get(sql, Make, new object[] { "Id", id });
        }

        public IEnumerable<Room> GetAll()
        {
            string sql = "SELECT * FROM TBRoom";
            return Db.GetAll(sql, Make);
        }

        public bool Delete(Room room)
        {
            if (room.Id < 1)
                throw new IdentifierUndefinedException();
            string sql = "DELETE FROM TBRoom WHERE Id = @Id";
            Db.Delete(sql, new object[] { "Id", room.Id });
            return true;
        }

        private static Func<IDataReader, Room> Make = reader =>
           new Room
           {
               Id = Convert.ToInt32(reader["Id"]),
               Name = reader["Name"].ToString(),
               NumberOfSeats = Convert.ToInt32(reader["NumberOfSeats"]),
               Disponibility = Convert.ToBoolean(reader["Disponibility"])
           };

        private object[] Take(Room room, bool hasId = true)
        {
            object[] parametros = null;

            if (hasId)
            {
                parametros = new object[]
                    {
                "@Name", room.Name,
                "@NumberOfSeats", room.NumberOfSeats,
                "@Disponibility", room.Disponibility,
                "@Id", room.Id,
                    };
            }
            else
            {
                parametros = new object[]
              {
                "@Name", room.Name,
                "@NumberOfSeats", room.NumberOfSeats,
                "@Disponibility", room.Disponibility
              };
            }
            return parametros;
        }
    }
}
