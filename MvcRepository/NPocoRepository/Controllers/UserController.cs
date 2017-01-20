using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NPocoRepository.Repositories;

namespace NPocoRepository.Controllers
{
    public class UserController : Controller
    {
        private UnitRepository unitRepository = new UnitRepository();
        // GET: User
        public ActionResult Index()
        {
            var user = unitRepository.UserRepository.GetHa();
            unitRepository.Dispose();
            return View(user);
        }

        public ActionResult Eddit()
        {
            var user = unitRepository.UserRepository.Get(filter: u => (new int[] { 1, 2, 3 }).Contains(u.ID), orderBy: u => u.OrderByDescending(q => q.ID)).ToList();
            unitRepository.Dispose();
            return View(user);

        }
    }
}