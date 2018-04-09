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

    // [HttpGet]
    // public IEnumerable<Vault> GetAllVaults()
    // {
    //   return _repo.GetAllVaults();
    // }

    [HttpGet("{id}")]
    public Vault Get(int id)
    {
      return _repo.GetVaultById(id);
    }

    [HttpGet("{userId}")]
    public IEnumerable<Vault> GetUserVaults(string userId)
    {
      return _repo.GetUserVaults(userId);
    }

    [HttpPost]
    public Vault AddVault([FromBody]Vault vault)
    {
      if (ModelState.IsValid)
      {
        // System.Console.WriteLine("ARE WE IN HERE?");
        return _repo.AddVault(vault);
        // why not return the whole data set in order to setVaults?
      }
      return null;
    }

    // [HttpGet("report/{id}")]
    // public IEnumerable<UserBurgerOrderReport> GetReport(string id){
    //   return _repo.GetUserBurgerReport(id);
    // }

  }
}