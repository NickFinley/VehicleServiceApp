using Microsoft.AspNetCore.Mvc;
using VehicleServiceApp.Controllers;
using VehicleServiceApp.Models;

namespace VehicleControllerTest
{
    public class VehicleControllerTest
    {
        private readonly VehiclesController _controller;
        private readonly IVehicleService _service;

        public VehicleControllerTest()
        {
            _service = new VehicleServiceDummy();
            _controller = new VehiclesController(_service);
        }

        [Fact]
        public void GetVehicles_WhenCalled_ReturnsOkResult()
        {
            QueryParameters parameters = new();
            // Act
            var okResult = _controller.GetVehicles(parameters);

            // Assert
            Assert.IsType<OkObjectResult>(okResult as OkObjectResult);
        }

        [Fact]
        public void GetVehicles_WhenCalled_ReturnsAllItems()
        {
            QueryParameters parameters = new();
            // Act
            var okResult = _controller.GetVehicles(parameters) as OkObjectResult;

            // Assert
            var items = Assert.IsType<List<Vehicle>>(okResult?.Value);
            Assert.Equal(3, items.Count);
        }

        [Fact]
        public void GetVehicle_UnknownIdPassed_ReturnsNotFoundResult()
        {
            // Act
            var notFoundResult = _controller.GetVehicle(0);

            // Assert
            Assert.IsType<NotFoundResult>(notFoundResult);
        }

        [Fact]
        public void GetVehicle_ExistingIdPassed_ReturnsOkResult()
        {
            // Arrange
            int testId = 1;

            // Act
            var okResult = _controller.GetVehicle(testId);

            // Assert
            Assert.IsType<OkObjectResult>(okResult as OkObjectResult);
        }

        [Fact]
        public void GetVehicle_ExistingIdPassed_ReturnsRightItem()
        {
            // Arrange
            int testId = 1;

            // Act
            var okResult = _controller.GetVehicle(testId) as OkObjectResult;

            // Assert
            Assert.IsType<Vehicle>(okResult.Value);
            Assert.Equal(testId, (okResult.Value as Vehicle).Id);
        }

        [Fact]
        public void PostVehicle_InvalidObjectPassed_ReturnsBadRequest()
        {
            // Arrange
            var makeMissingItem = new Vehicle()
            {
                Id = 4,
                Model = "Silverado"
            };
            _controller.ModelState.AddModelError("Make", "Required");

            // Act
            var badResponse = _controller.PostVehicle(makeMissingItem);

            // Assert
            Assert.IsType<BadRequestObjectResult>(badResponse);
        }

        [Fact]
        public void PostVehicle_ValidObjectPassed_ReturnsCreatedResponse()
        {
            // Arrange
            Vehicle testItem = new Vehicle()
            {
                Make = "Toyota",
                Model = "4Runner",
                Year = 2008
            };

            // Act
            var createdResponse = _controller.PostVehicle(testItem);

            // Assert
            Assert.IsType<CreatedAtActionResult>(createdResponse);
        }

        [Fact]
        public void PostVehicle_ValidObjectPassed_ReturnedResponseHasCreatedItem()
        {
            // Arrange
            var testItem = new Vehicle()
            {
                Id = 5,
                Make = "Toyota",
                Model = "Highlander",
                Year = 2009
            };

            // Act
            var createdResponse = _controller.PostVehicle(testItem) as CreatedAtActionResult;
            var item = createdResponse.Value as Vehicle;

            // Assert
            Assert.IsType<Vehicle>(item);
            Assert.True(item.Id == 5);
        }

        [Fact]
        public void DeleteVehicle_NotExistingIdPassed_ReturnsNotFoundResponse()
        {
            // Arrange
            var testId = -1;

            // Act
            var badResponse = _controller.DeleteVehicle(testId);

            // Assert
            Assert.IsType<NotFoundResult>(badResponse);
        }

        [Fact]
        public void DeleteVehicle_ExistingIdPassed_ReturnsOkResult()
        {
            // Arrange
            var testId = 1;

            // Act
            var okResult = _controller.GetVehicle(testId) as OkObjectResult;

            // Assert
            Assert.IsType<OkObjectResult>(okResult);
        }

        [Fact]
        public void DeleteVehicle_ExistingIdPassed_RemovesOneItem()
        {
            // Arrange
            var testId = 1;

            // Act
            var okResponse = _controller.DeleteVehicle(testId);

            // Assert
            Assert.Equal(2, _service.GetVehicles(null).Count);
        }
    }
}
