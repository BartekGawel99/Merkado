﻿using Merkado.DAL;
using Merkado.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Linq;

namespace Merkado.Controllers
{
    
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly MerkadoDbContext _db;

        public HomeController(ILogger<HomeController> logger, MerkadoDbContext db)
        {
            _logger = logger;
            _db = db;
        }
        [AllowAnonymous]
        public IActionResult Index()
        {
            var productList = _db.Products
                                .Include(c => c.Category)
                                .Include(i => i.Images)
                                .Include(p => p.Providers)
                                .ToList();
            var categoryListy = _db.Products
                                .Include(c => c.Category)
                                .ToList();

            return View(productList);
        }
        
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public IActionResult AddProduct()
        {
            var list = new List<string>();
            foreach(var category in _db.Categories)
            {
                list.Add(category.Name.ToString());
            }
            ViewBag.list = list;
            return View();
        }

    }
}