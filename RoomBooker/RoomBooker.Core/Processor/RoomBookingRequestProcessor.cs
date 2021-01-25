using System;
using RoomBooker.Core.Domain;

namespace RoomBooker.Core.Processor
{
	public class RoomBookingRequestProcessor
	{
		public RoomBookingRequestProcessor()
		{
		}

		public RoomBookingResult BookRoom(RoomBookingRequest request)
		{
			if (request == null) throw new ArgumentNullException(nameof(request));

			return new RoomBookingResult
			{
				Name = request.Name,
				Email = request.Email,
				Date = request.Date
			};
		}
	}
}