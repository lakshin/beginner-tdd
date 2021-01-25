using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RoomBooker.Core.Domain;

namespace RoomBooker.Core.Processor
{
	[TestClass]
	public class RoomBookingRequestProcessorTests
	{
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

			var processor = new RoomBookingRequestProcessor();

			//Act
			RoomBookingResult result = processor.BookRoom(request);

			//Assert
			Assert.IsNotNull(result);
			Assert.AreEqual(request.Name, result.Name);
			Assert.AreEqual(request.Email, result.Email);
			Assert.AreEqual(request.Date, result.Date);
		}
	}
}
