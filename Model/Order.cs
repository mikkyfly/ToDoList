using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Model
{
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
	/// <summary>
	/// задание
	/// </summary>
	public class Order
	{
		[Key]
		public int Id { get; set; }

		public PublicUser Customer { get; set; }

		//public ICollection<PublicUser> Executors { get; set; }
		public List<PublicUser> Executors { get; set; }

		//public DateTime StartDate { get; set; }

		//public DateTime ExporationData { get; set; }

		//public DateTime EndDate { get; set; }

		//public Status Status { get; set; }

		//public Urgency Urgency { get; set; }

		//public string TaskText { get; set; }
	}
}
