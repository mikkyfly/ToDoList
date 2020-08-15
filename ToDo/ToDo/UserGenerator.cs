using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ToDo
{
	public class User
	{
		public long Id { get; set; }

		public string Name { get; set; }

		public string Password { get; set; }
	}

	static class StringGenerator
	{
		static Random _rand = new Random();

		static string _alphabet = "абвгдеёжзийклмнопрстуфхцчшщъыьэюя";

		public static string GenerateString(int maxLen)
		{
			var len = _rand.Next(4, maxLen);

			var res = string.Empty;

			for (int i = 0; i < len; i++)
				res += _alphabet[_rand.Next(0, _alphabet.Length - 1)];

			return res;
		}
	}

	public static class UserGenerator
	{
		public static int Count = 10;

		public static List<User> GenerateUsers()
		{
			var users = new List<User>();

			for (int i = 1; i <= Count; i++)
				users.Add(new User()
				{
					Name = StringGenerator.GenerateString(8),
					Id = i
				});

			return users;
		}
	}
	public enum Status
	{
		Appointed,
		InWork,
		RequiresVerification,
		Ready,
		ReOpened,
		Frozen,
	}

	public enum Urgency
	{
		Low,
		Middle,
		High
	}

	public class Task
	{
		public long Id { get; set; }

		public DateTime StartDate { get; set; }

		public DateTime EndDate { get; set; }

		public Status Status { get; set; }

		public Urgency Urgency { get; set; }

		public string TaskText { get; set; }

		public User Executor { get; set; }

		public User Customer { get; set; }
	}
	public static class TaskGenerator
	{
		static int modifier = 2;

		static Random rand = new Random();


		static User GetOtherUser(User curUser, List<User> allUsers)
		{
			allUsers = allUsers.Where(z => z != curUser).ToList();

			return allUsers[rand.Next(0, allUsers.Count * 3) % allUsers.Count];
		}

		static Urgency GetRandUrgency()
		{
			var values = Enum.GetValues(typeof(Urgency));

			return (Urgency)values.GetValue(rand.Next(0, values.Length));
		}

		public static List<Task> GenerateTasks(List<User> users)
		{
			var tasks = new List<Task>();

			int id = 1;

			for (int i = 0; i < users.Count; i++)
				for (int j = 0; j < modifier; j++)
				{
					var otherUser = GetOtherUser(users[i], users);

					var date = DateTime.Now;
					var days = rand.Next(3, 15);

					date = date.AddDays(-days);

					tasks.Add(new Task()
					{
						Id = id++,
						Customer = otherUser,
						Executor = users[i],
						StartDate = date,
						Urgency = GetRandUrgency(),
						Status = Status.Appointed,
						TaskText = StringGenerator.GenerateString(500)
					});;
				}

			return tasks;
		}
	}
}
