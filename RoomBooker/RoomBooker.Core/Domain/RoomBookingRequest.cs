using System;

namespace RoomBooker.Core.Domain
{
	public class RoomBookingRequest
	{
		public string Name { get; set; }
		public string Email { get; set; }
		public DateTime Date { get; set; }
	}
}