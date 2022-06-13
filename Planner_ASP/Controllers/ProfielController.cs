﻿using Microsoft.AspNetCore.Mvc;
using BusnLogicBW;
using DalMSSQL;
using Planner_ASP.Models;
using InterfaceLib;

namespace Planner_ASP.Controllers
{
    public class ProfielController : Controller
    {
        private GebruikerContainer gc;
        private RankContainer rc;

        private readonly IConfiguration _configuration;

        public ProfielController(IConfiguration ic)
        {
            _configuration = ic;
            gc = new GebruikerContainer(new GebruikerMSSQLDAL(_configuration["ConnectionStrings:Connstring"]));
            rc = new RankContainer(new RankMSSQLDAL(_configuration["ConnectionStrings:Connstring"]));
        }

        [HttpGet]
        public IActionResult Index()
        {
            if(HttpContext.Session.GetString("Naam") != null)
            {
                Gebruiker g = gc.FindByID(HttpContext.Session.GetInt32("ID").Value);
                List<Rank> ranks = rc.GetRanks();
                ProfielViewModel vm = new(g, ranks);
                return View(vm);
            }
           return RedirectToAction("Index", "Login");
        }

        [HttpPost]
        public IActionResult Index(ProfielViewModel profilevm)
        {
            Gebruiker g = new(profilevm.Naam, profilevm.GameNaam, profilevm.PlannerNaam, profilevm.Email,
                profilevm.Rank1s, profilevm.Rank2s, profilevm.Rank3s, HttpContext.Session.GetInt32("ID").Value);
            gc.Update(g);
            return RedirectToAction("Index");
        }

    }
}
