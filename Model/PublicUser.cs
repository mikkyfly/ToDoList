using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Model
{
	public class PublicUser
	{
		[Key]
		public int Id { get; set; }

		public string Login { get; set; }

		public string Name { get; set; }

		public string Surname { get; set; }
	}
}