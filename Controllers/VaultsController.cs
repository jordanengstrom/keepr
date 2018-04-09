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

    [HttpGet]
    public IEnumerable<Vault> Get()
    {
      return _repo.GetVaults();
    }

    [HttpGet("{id}")]
    public Vault Get(int id)
    {
      return _repo.GetVaultById(id);
    }

    [HttpPost]
    public Vault AddVault([FromBody]Vault vault)
    {
      if (ModelState.IsValid)
      {
        return _repo.AddVault(vault);
      }
      return null;
    }

    // [HttpGet("report/{userId}")]
    // public IEnumerable<UserBurgerOrderReport> GetReport(string userId){
    //   return _repo.GetUserBurgerReport(userId);
    // }

  }
}