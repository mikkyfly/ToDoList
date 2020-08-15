using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace ToDo
{
	public partial class MainPage : ContentPage
	{
		List<User> Users;

		List<Task> Tasks;

		public MainPage()
		{
			InitializeComponent();
			Users = UserGenerator.GenerateUsers();
			Tasks = TaskGenerator.GenerateTasks(Users);

		}
	}
}
