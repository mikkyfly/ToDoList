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
            Test2();
            return Content("Hello");
        }

        private void Test2()
        {
            var u = new User
            {
                Login = "Kir"
            };
            
            var u2 = new User
            {
                Login = "Misha"
            };
            var u3 = new User
            {
                Login = "Rust"
            };
            
            _ctx.Users.AddRange(u, u2, u3);
            _ctx.SaveChanges();
            
            var exList = new List<User>();
            exList.Add(u2);
            exList.Add(u3);

            var order = new Order
            {
                Customer = u,
                ExecutorIds = exList.Select(z => z.Id).ToArray()
            };
            _ctx.Orders.Add(order);
            _ctx.SaveChanges();

        }
    }
}