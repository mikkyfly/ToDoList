using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Model;
using Server.Model;

namespace Server.Controllers
{
    public class HomeController : Controller
    {
        readonly ApplicationContext _ctx;
        
        public HomeController(ApplicationContext ctx)
        {
            _ctx = ctx;
        }

        public IActionResult Index()
        {
            return Content("Hello");
        }
    }
}