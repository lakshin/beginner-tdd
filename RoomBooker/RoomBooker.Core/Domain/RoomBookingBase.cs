using System;
using System.Collections.Generic;
using System.Text;

namespace RoomBooker.Core.Domain
{
	public class RoomBookingBase
	{
		public string Name { get; set; }
		public string Email { get; set; }
		public DateTime Date { get; set; }
	}
}
