using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Model;
using Server.Model;

namespace Server.Controllers
{
	[ApiController]
	[Route("[controller]")]
	public class UserController : Controller
	{
		ApplicationContext _ctx;

		public UserController(ApplicationContext context)
		{
			_ctx = context;
		}

		[HttpGet("get-list")]
		public IEnumerable<PublicUser> Get(int skip = 0, int take = 10)
		{
			return _ctx.PublicUsers.Skip(skip).Take(take);
		}

		[HttpGet]
		public PublicUser Get(int id)
		{
			if (id == 0)
				return null;

			var user = _ctx.PublicUsers.FirstOrDefault(z => z.Id == id);

			return user;
		}

		[HttpPost]
		public void Post(string name, string login)
		{
			name = Request?.Form["name"];
			login = Request.Form["login"];

			if (_ctx.PublicUsers.Any(z => z.Login == login))
				return;

			var res = _ctx.PublicUsers.Add(new PublicUser()
			{
				Name = name,
				Login = login
			});

			_ctx.SaveChanges();
		}
	}
}
