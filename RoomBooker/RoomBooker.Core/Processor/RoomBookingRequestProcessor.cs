using System;
using RoomBooker.Core.DataInterface;
using RoomBooker.Core.Domain;

namespace RoomBooker.Core.Processor
{
	public class RoomBookingRequestProcessor
	{
		private IRoomBookingRespository _roomBookingRepository;
		private IRoomRepository _roomRepository;

		public RoomBookingRequestProcessor(IRoomBookingRespository roomBookingRepository, IRoomRepository roomRepository)
		{
			this._roomBookingRepository = roomBookingRepository;
			this._roomRepository = roomRepository;
		}

		public RoomBookingResult BookRoom(RoomBookingRequest request)
		{
			if (request == null) throw new ArgumentNullException(nameof(request));

			var availableRooms = _roomRepository.GetAvailableRooms(request.Date);

			if (availableRooms.Count > 0)
			{
				this._roomBookingRepository.Save(this.CreateRoomBooking<RoomBooking>(request));
			}

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