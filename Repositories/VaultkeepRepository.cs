using System;
using System.Data;
using keepr.Models;
using Dapper;
using System.Collections.Generic;

namespace keepr.Repositories
{
    public class VaultkeepRepository
    {
        private readonly IDbConnection _db;
        public VaultkeepRepository(IDbConnection db)
        {
            _db = db;
        }
        public Vaultkeep AddVaultkeep(Vaultkeep vaultkeepData)
        {
            //construct a vault
            Vaultkeep vaultkeep = new Vaultkeep()
            {
                VaultId = vaultkeepData.VaultId,
                KeepId = vaultkeepData.KeepId,
                UserId = vaultkeepData.UserId
            };

            // run a sql command
            var success = _db.Execute(@"
        INSERT INTO vaultkeeps(
          id,
          vaultId, 
          keepId,
          userId
        ) VALUES(
          @Id,
          @VaultId,
          @KeepId,
          @UserId
        )
      ", vaultkeep);
            if (success < 1)
            {
                throw new Exception("vaultkeep exception");
            }
            // return created vault
            return vaultkeep;
        }
        //Find one
        public Vaultkeep GetOneVaultkeep(int id)
        {
            Vaultkeep vaultkeep = _db.QueryFirstOrDefault<Vaultkeep>(@"
        SELECT * FROM vaults WHERE id = @Id
      ", new { Id = id });

            if (vaultkeep == null) { throw new Exception("There was an error getting the vaultkeep"); }

            return vaultkeep;
        }
        //Find many
        public IEnumerable<Vaultkeep> GetKeepsByVault(int vaultId)
        {
            return _db.Query<Vaultkeep>(@"
                SELECT * FROM vaultkeeps vk
                    INNER JOIN keeps k ON k.id = vk.keepId 
                WHERE (vaultId = @VaultId)
            ", new { VaultId = vaultId });
        }
        public Vaultkeep UpdateVaultkeep(Vaultkeep vaultkeep, Vaultkeep vaultkeepData)
        {
            var i = _db.Execute(@"
                UPDATE vaultkeeps SET
                    id = @Id,
                    vaultId = @VaultId,
                    keepId = @KeepId,
                    userId = @UserId
                WHERE id = @Id
            ", vaultkeepData);
            if (i > 0)
            {
                return vaultkeepData;
            }
            return null;
        }
        public Vaultkeep DeleteVaultkeep(Vaultkeep vaultkeep)
        {
            var i = _db.Execute(@"
                DELETE FROM vaultkeeps WHERE id = @Id;
            ", vaultkeep);
            if (i > 0)
            {
                return vaultkeep;
            }
            return null;
        }
    }
}