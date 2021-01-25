using System;
using RoomBooker.Core.DataInterface;
using RoomBooker.Core.Domain;

namespace RoomBooker.Core.Processor
{
	public class RoomBookingRequestProcessor
	{
		private IRoomBookingRespository _roomBookingRepository;

		public RoomBookingRequestProcessor(IRoomBookingRespository roomBookingRepository)
		{
			this._roomBookingRepository = roomBookingRepository;
		}

		public RoomBookingResult BookRoom(RoomBookingRequest request)
		{
			if (request == null) throw new ArgumentNullException(nameof(request));

			this._roomBookingRepository.Save(this.CreateRoomBooking<RoomBooking>(request));

			return CreateRoomBooking<RoomBookingResult>(request);
		}

		private T CreateRoomBooking<T>(RoomBookingRequest request) where T : RoomBookingBase, new()
		{
			return new T
			{
				Name = request.Name,
				Email = request.Email,
				Date = request.Date
			};
		}
	}
}