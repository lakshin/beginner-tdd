using System;
using System.Collections.Generic;
using System.Text;
using RoomBooker.Core.Domain;

namespace RoomBooker.Core.DataInterface
{
	public interface IRoomRepository
	{
		IReadOnlyCollection<Room> GetAvailableRooms(DateTime date);
	}
}
