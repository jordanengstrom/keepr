using System;
using System.Data;
using keepr.Models;
using Dapper;

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
            //Generate an ID
            Guid g;
            // Create and display the value  GUIDs.
            g = Guid.NewGuid();
            string id = g.ToString();
            //   string pass = BCrypt.Net.BCrypt.HashPassword(vaultData.Password);

            //construct a keep
            Keep keep = new Keep()
            {
                Id = id,
                Link = keepData.Link,
                Description = keepData.Description,
                UserId = keepData.UserId
            };

            // run a sql command
            var success = _db.Execute(@"
        INSERT INTO keeps(
          keepId,
          link, 
          description,
          userId
        ) VALUES(
          @Id,
          @Link,
          @Description,
          @UserId
        )
      ", keep);
            if (success < 1)
            {
                throw new Exception("EMAIL IN USE");
            }
            // return created vault
            return keep;
        }

        public Keep GetKeepById(string id)
        {
            Keep keep = _db.QueryFirstOrDefault<Keep>(@"
        SELECT * FROM keeps WHERE keepId = @Id
      ", new { Id = id });

            if (keep == null) { throw new Exception("There was an error getting the keep"); }

            return keep;
        }

        public Keep UpdateKeep(Keep keep, Keep keepData)
        {
            var i = _db.Execute(@"
                UPDATE keeps SET
                    link = @Link,
                    description = @Description
                WHERE keepId = @Id
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
                DELETE FROM keeps WHERE keepId = @Id;
            ", keep);
            if (i > 0)
            {
                return keep;
            }
            return null;
        }
    }
}