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

        public Keep AddKeep(Keep keep)
        {
            int id = _db.ExecuteScalar<int>(@"
            INSERT INTO keeps (
                img,
                link,
                description,
                userId,
                views,
                keeps,
                public
            ) VALUES (
                @Img,
                @Link,
                @Description,
                @UserId,
                @Views,
                @Keeps,
                @Public
            ); SELECT LAST_INSERT_ID()", keep);
            keep.Id = id;
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
        // public IEnumerable<Keep> GetVaultKeeps(int vaultId)
        // {
        //     return _db.Query<Keep>(@"
        //         SELECT * FROM keeps WHERE vaultId = @VaultId
        //     ", new {VaultId = vaultId});
        // }

        //Find all
        public IEnumerable<Keep> GetAllKeeps()
        {
            return _db.Query<Keep>(@"
                SELECT * FROM keeps
            ");
        }
        public Keep UpdateViews(Keep keep, Keep keepData)
        {
            var i = _db.Execute(@"
                UPDATE keeps SET
                    id = @Id,
                    img = @Img,
                    link = @Link,
                    description = @Description,
                    userId = @UserId,
                    views = @Views + 1,
                    keeps = @Keeps,
                    public = @Public
                 WHERE id = @Id
            ", keepData);
            if (i > 0)
            {
                return keepData;
            }
            return null;
        }

        // public Keep UpdateViews(Keep keep, Keep keepData)
        // {
        //     var i = _db.Execute(@"
        //         UPDATE keeps SET
        //             id = @Id,
        //             img = @Img,
        //             link = @Link,
        //             description = @Description,
        //             userId = @UserId,
        //             views = @Views + 1,
        //             keeps = @Keeps,
        //             public = @Public
        //          WHERE id = @Id
        //     ", keepData);
        //     if (i > 0)
        //     {
        //         return keepData;
        //     }
        //     return null;
        // }
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