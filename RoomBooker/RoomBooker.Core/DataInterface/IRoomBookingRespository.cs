using System;
using System.Collections.Generic;
using System.Text;
using RoomBooker.Core.Domain;

namespace RoomBooker.Core.DataInterface
{
	public interface IRoomBookingRespository
	{
		void Save(RoomBooking roomBooking);
	}
}
