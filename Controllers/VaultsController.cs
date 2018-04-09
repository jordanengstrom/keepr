using System;
using System.Collections.Generic;
using keepr.Models;
using keepr.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace keepr.Controllers
{
    [Route("api/[controller]")]
    public class VaultsController : Controller
    {
        private readonly VaultRepository _repo;
        public VaultsController(VaultRepository repo)
        {
            _repo = repo;
        }

        // [HttpGet] changed to get all by user
        // public IEnumerable<Vault> GetAllVaults()
        // {
        //   return _repo.GetAllVaults();
        // }

        [HttpGet("{id}")]
        public Vault GetVaultById(int id)
        {
            return _repo.GetVaultById(id);
        }

        [HttpPost]
        public Vault AddVault([FromBody]Vault vault)
        {
            if (ModelState.IsValid)
            {
                return _repo.AddVault(vault);
                // why not return the whole data set in order to setVaults?
            }
            return null;
        }

        [HttpGet("report/{userId}")]
        public IEnumerable<Vault> GetUserVaults(string userId)
        {
            return _repo.GetUserVaults(userId);
        }

        [HttpPut("{id}")]
        public Vault UpdateVault([FromBody]Vault vaultData)
        {
            Vault vault = _repo.GetVaultById(vaultData.Id);
            return _repo.UpdateVault(vault, vaultData);
        }

        [HttpDelete("{id}")]
        public Vault DeleteVault(Vault vault) {
          return _repo.DeleteVault(vault);
        }
    }
}