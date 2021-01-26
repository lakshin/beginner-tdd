using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using RoomBooker.Core.DataInterface;
using RoomBooker.Core.Domain;

namespace RoomBooker.Core.Processor
{
	[TestClass]
	public class RoomBookingRequestProcessorTests
	{
		private RoomBookingRequest _request;
		private List<Room> _availableRooms;
		private Mock<IRoomBookingRespository> _roomBookingRepositoryMock;
		private Mock<IRoomRepository> _roomRepositoryMock;
		private RoomBookingRequestProcessor _processor;

		[TestInitialize]
		public void Before_each()
		{
			_request = new RoomBookingRequest
			{
				Name = "Jhon Doe",
				Email = "jhon@gmail.com",
				Date = new DateTime(2021, 01, 26)
			};

			_availableRooms = new List<Room> { new Room() };

			_roomBookingRepositoryMock = new Mock<IRoomBookingRespository>();
			_roomRepositoryMock = new Mock<IRoomRepository>();

			_roomRepositoryMock.Setup(x => x.GetAvailableRooms(_request.Date)).Returns(_availableRooms);

			_processor = new RoomBookingRequestProcessor(_roomBookingRepositoryMock.Object, _roomRepositoryMock.Object);
		}

		[TestMethod]
		public void BookRoom_OnExecute_ShouldReturnRoomBookingResultWithRequestValues()
		{
			//Arrange
			var request = new RoomBookingRequest
			{
				Name = "Jhon Doe",
				Email = "jhon@gmail.com",
				Date = new DateTime(2021, 01, 26)
			};

			//Act
			RoomBookingResult result = _processor.BookRoom(request);

			//Assert
			Assert.IsNotNull(result);
			Assert.AreEqual(request.Name, result.Name);
			Assert.AreEqual(request.Email, result.Email);
			Assert.AreEqual(request.Date, result.Date);
		}

		[TestMethod]
		public void BookRoom_OnExecute_ShouldThrowExceptionWhenRequestIsNull()
		{
			//Arrange

			// Act & Assert
			var exception = Assert.ThrowsException<ArgumentNullException>(() => _processor.BookRoom(null));
			Assert.AreEqual("request", exception.ParamName);
		}

		[TestMethod]
		public void BookRoom_OnExecute_ShouldSaveRoomBooking()
		{
			//Arrange
			RoomBooking savedRoomBooking = null;
			_roomBookingRepositoryMock.Setup(x => x.Save(It.IsAny<RoomBooking>()))
				.Callback<RoomBooking>(roomBooking =>
				{
					savedRoomBooking = roomBooking;
				});

			//Act
			_processor.BookRoom(_request);

			//Assert
			_roomBookingRepositoryMock.Verify(x => x.Save(It.IsAny<RoomBooking>()), Times.Once);
			Assert.IsNotNull(savedRoomBooking);
			Assert.AreEqual(_request.Name, savedRoomBooking.Name);
			Assert.AreEqual(_request.Email, savedRoomBooking.Email);
			Assert.AreEqual(_request.Date, savedRoomBooking.Date);
		}

		[TestMethod]
		public void BookRoom_OnExecute_ShouldNotSaveRoomBookingIfNoRoomAvailable()
		{
			//Arrange
			//Ensure that no room available before calling
			_availableRooms.Clear();

			//Act
			_processor.BookRoom(_request);

			//Assert
			_roomBookingRepositoryMock.Verify(x => x.Save(It.IsAny<RoomBooking>()), Times.Never);
		}
	}
}
