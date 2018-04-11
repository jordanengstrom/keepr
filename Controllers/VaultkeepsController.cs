using System;
using System.Collections.Generic;
using keepr.Models;
using keepr.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace keepr.Controllers
{
    [Route("api/[controller]")]
    public class VaultkeepsController : Controller
    {
        private readonly VaultkeepRepository _repo;
        public VaultkeepsController(VaultkeepRepository repo)
        {
            _repo = repo;
        }

        //Find one
        [HttpGet("{id}")]
        public Vaultkeep GetOneVaultkeep(int id)
        {
            return _repo.GetOneVaultkeep(id);
        }

        //Find keeps by vault
        [HttpGet("report/{vaultId}")]
        public IEnumerable<Vaultkeep> GetKeepsByVault(int vaultId)
        {
            return _repo.GetKeepsByVault(vaultId);
        }

        //Count how many times a keep has been kept
        [HttpGet("keepcount")]
        public int KeepCount(Keep keep){
            return _repo.KeepCount(keep.Id);
        }

        [HttpPost]
        public Vaultkeep AddVaultkeep([FromBody]Vaultkeep vaultkeep)
        {
            if (ModelState.IsValid)
            {
                return _repo.AddVaultkeep(vaultkeep);
            }
            return null;
        }

        [HttpPut("{id}")]
        public Vaultkeep UpdateVaultkeep([FromBody]Vaultkeep vaultkeepData)
        {
            Vaultkeep vaultkeep = _repo.GetOneVaultkeep(vaultkeepData.Id);
            return _repo.UpdateVaultkeep(vaultkeep, vaultkeepData);
        }

        [HttpDelete("{id}")]
        public Vaultkeep DeleteVaultkeep(Vaultkeep vaultkeep) {
          return _repo.DeleteVaultkeep(vaultkeep);
        }
    }
}