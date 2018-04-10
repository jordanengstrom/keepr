using System;
using System.Collections.Generic;
using keepr.Models;
using keepr.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace keepr.Controllers
{
    [Route("api/[controller]")]
    public class KeepsController : Controller
    {
        private readonly KeepRepository _repo;
        public KeepsController(KeepRepository repo)
        {
            _repo = repo;
        }

        // Come back to this:
        // [HttpGet]
        // public IEnumerable<Vault> GetAllKeeps()
        // {
        //   return _repo.GetAllKeeps();
        // }

        //Find one
        [HttpGet("{id}")]
        public Keep GetKeepById(int id)
        {
            return _repo.GetKeepById(id);
        }

        [HttpPost]
        public Keep AddKeep([FromBody]Keep keep)
        {
            if (ModelState.IsValid)
            {
                return _repo.AddKeep(keep);
            }
            return null;
        }

        //Find many
        [HttpGet("report/{userId}")]
        public IEnumerable<Keep> GetUserKeeps(string userId)
        {
            return _repo.GetUserKeeps(userId);
        }

        // [HttpGet("report/{vaultId}")]
        // public IEnumerable<Keep> GetVaultKeeps(int vaultId)
        // {
        //     return _repo.GetVaultKeeps(vaultId);
        // }

        // Come back to this:   
        [HttpPut("{id}")]
        public Keep UpdateKeep([FromBody]Keep keepData)
        {
            Keep keep = _repo.GetKeepById(keepData.Id);
            return _repo.UpdateKeep(keep, keepData);
        }

        [HttpDelete("{id}")]
        public Keep DeleteKeep(Keep keep) {
          return _repo.DeleteKeep(keep);
        }
    }
}