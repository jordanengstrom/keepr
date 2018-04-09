using System;
using System.Data;
using keepr.Models;
using Dapper;
using System.Collections.Generic;

namespace keepr.Repositories
{
    public class VaultRepository
    {
        private readonly IDbConnection _db;
        public VaultRepository(IDbConnection db)
        {
            _db = db;
        }

        public Vault AddVault(Vault vaultData)
        {
            //construct a vault
            Vault vault = new Vault()
            {
                Name = vaultData.Name,
                Description = vaultData.Description,
                UserId = vaultData.UserId
            };

            // run a sql command
            var success = _db.Execute(@"
        INSERT INTO vaults(
          id,
          name, 
          description,
          userId
        ) VALUES(
          @Id,
          @Name,
          @Description,
          @UserId
        )
      ", vault);
            if (success < 1)
            {
                throw new Exception("vault exception");
            }
            // return created vault
            return vault;
        }

        public IEnumerable<Vault> GetUserVaults(string userId)
        {
            // IEnumerable<Vault> Vaults = _db.QueryMultiple
            return _db.QueryFirstOrDefault(@"
                SELECT * FROM vaults WHERE userId = @UserId
            ", new {UserId = userId});
        }

        public Vault GetVaultById(int id)
        {
            Vault vault = _db.QueryFirstOrDefault<Vault>(@"
        SELECT * FROM vaults WHERE id = @Id
      ", new { Id = id });

            if (vault == null) { throw new Exception("There was an error getting the vault"); }

            return vault;
        }

        public Vault UpdateVault(Vault vault, Vault vaultData)
        {
            var i = _db.Execute(@"
                UPDATE vaults SET
                    name = @Name,
                    description = @Description
                WHERE id = @Id
            ", vaultData);
            if (i > 0)
            {
                return vaultData;
            }
            return null;
        }

        public Vault DeleteVault(Vault vault)
        {
            var i = _db.Execute(@"
                DELETE FROM vaults WHERE id = @Id;
            ", vault);
            if (i > 0)
            {
                return vault;
            }
            return null;
        }
    }
}