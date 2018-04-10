using System;
using System.Data;
using keepr.Models;
using Dapper;
using System.Collections.Generic;

namespace keepr.Repositories
{
    public class KeepRepository
    {
        private readonly IDbConnection _db;
        public KeepRepository(IDbConnection db)
        {
            _db = db;
        }

        public Keep AddKeep(Keep keepData)
        {
            //construct a keep
            Keep keep = new Keep()
            {
                Id = keepData.Id,
                Img = keepData.Img,
                Link = keepData.Link,
                Description = keepData.Description,
                UserId = keepData.UserId
            };

            // run a sql command
            var success = _db.Execute(@"
        INSERT INTO keeps(
          id,
          img,
          link, 
          description,
          userId
        ) VALUES(
          @Id,
          @Img,
          @Link,
          @Description,
          @UserId
        )
      ", keep);
            if (success < 1)
            {
                throw new Exception("Unable to create keep");
            }
            // return created keep
            return keep;
        }

        //Find one
        public Keep GetKeepById(int id)
        {
            Keep keep = _db.QueryFirstOrDefault<Keep>(@"
        SELECT * FROM keeps WHERE id = @Id
      ", new { Id = id });

            if (keep == null) { throw new Exception("There was an error getting the keep"); }

            return keep;
        }

        //Find many
        public IEnumerable<Keep> GetUserKeeps(string userId)
        {
            return _db.Query<Keep>(@"
                SELECT * FROM keeps WHERE userId = @UserId
            ", new {UserId = userId});
        }

        public Keep UpdateKeep(Keep keep, Keep keepData)
        {
            var i = _db.Execute(@"
                UPDATE keeps SET
                    id = @Id,
                    img = @Img,
                    link = @Link,
                    description = @Description,
                    userId = @UserId
                WHERE id = @Id
            ", keepData);
            if (i > 0)
            {
                return keepData;
            }
            return null;
        }

        public Keep DeleteKeep(Keep keep)
        {
            var i = _db.Execute(@"
                DELETE FROM keeps WHERE id = @Id;
            ", keep);
            if (i > 0)
            {
                return keep;
            }
            return null;
        }
    }
}