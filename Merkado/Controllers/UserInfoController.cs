﻿using Merkado.DAL;
using Merkado.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Merkado.Controllers
{
    public class UserInfoController : Controller
    {
        private readonly MerkadoDbContext _db;
        public UserInfoController(MerkadoDbContext db)
        {
            _db = db;
        }

        public IActionResult Index(string user)
        {
            if(user != null)
            {
                var userInfo = _db.Users
                              .Include(p => p.UserProducts)
                              .Include(o => o.Opinions)
                              .Where(i => i.Id == user)
                              .FirstOrDefault();

                var opinions = userInfo.Opinions;

                if (opinions != null && opinions.Count > 0)
                {
                    foreach (Opinion opinion in opinions)
                    {
                        opinion.ReviewerName = _db.Users.Where(rev => rev.Id == opinion.ReviewerId).Select(n => n.FirstName).FirstOrDefault();
                    }

                    userInfo.Opinions = opinions;
                }


                return View(userInfo);
            }
            else
            {
                return View("ErrorPage");
            }
            
        }
    }
}