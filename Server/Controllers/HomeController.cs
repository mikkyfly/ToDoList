using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Model;
using Server.Model;

namespace Server.Controllers
{
	public class HomeController : Controller
	{
		ApplicationContext _ctx;


		public HomeController(ApplicationContext ctx)
		{
			_ctx = ctx;
		}

		public string Index()
		{
			_ctx.PublicUsers.Add(new PublicUser
			{
				Login = "asdf"
			});
			_ctx.PublicUsers.Add(new PublicUser
			{
				Login = "zxcv"
			});

			_ctx.SaveChanges();

			var u1 = _ctx.PublicUsers.Find(1);
			//.FirstOrDefault(z => z.Id == 1);

			var u2 = _ctx.PublicUsers.Find(2);
				//.FirstOrDefault(z => z.Id == 2);

			_ctx.Orders.Add(new Order
			{
				Customer = u1,
				Executors = new List<PublicUser>() { u2 }
			});

			_ctx.SaveChanges();

			Console.WriteLine("1");


			var k = _ctx.Orders.First(z => z.Customer.Id == 1);

			var i = _ctx.Orders.First(z => z.Executors.Any(z => z.Id == 2));

			return "Hello";
		}
	}
}
