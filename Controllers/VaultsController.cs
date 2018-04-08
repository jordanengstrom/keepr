using System;
using System.Collections.Generic;
using keepr.Models;
using keepr.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace keepr.Controllers
{
  [Route("api/[controller]")]
  public class BurgersController : Controller
  {
    private readonly VaultRepository _repo;
    public BurgersController(VaultRepository repo)
    {
      _repo = repo;
    }

    [HttpGet]
    public IEnumerable<Burger> Get()
    {
      return _repo.GetBurgers();
    }

    [HttpGet("{id}")]
    public Burger Get(int id)
    {
      return _repo.GetById(id);
    }

    [HttpPost]
    public Burger AddBurger([FromBody]Burger burger)
    {
      if (ModelState.IsValid)
      {
        return _repo.Add(burger);
      }
      return null;
    }

    [HttpGet("report/{userId}")]
    public IEnumerable<UserBurgerOrderReport> GetReport(string userId){
      return _repo.GetUserBurgerReport(userId);
    }

  }
}