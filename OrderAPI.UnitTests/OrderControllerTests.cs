using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using OrderAPI.Controllers;
using OrderAPI.Models;
using OrderAPI.Services;
using System.Collections.Generic;

namespace OrderAPI.UnitTests
{
    public class OrderControllerTests
    {
        private readonly OrdersController _controller;
        private readonly Mock<IOrderService> _mockOrderService;

        public OrderControllerTests()
        {
            _mockOrderService = new Mock<IOrderService>();
            _controller = new OrdersController(_mockOrderService.Object);
        }

        [Test]
        public void GetOrders_ReturnsOkResult_WithListOfOrders()
        {
            // Arrange
            var mockOrders = new List<Order>
        {
            new Order { Id = 1, Item = "Item1", Quantity = 1 },
            new Order { Id = 2, Item = "Item2", Quantity = 2 }
        };
            _mockOrderService.Setup(service => service.GetAllOrders()).Returns(mockOrders);

            // Act
            var result = _controller.GetOrder();

            // Assert
            var okResult = result.Result as OkObjectResult;
            Assert.IsNotNull(okResult);
            var returnOrders = okResult.Value as List<Order>;
            Assert.AreEqual(2, returnOrders.Count);
        }

        [Test]
        public void GetOrderById_ReturnsOkResult_WithOrder()
        {
            // Arrange
            var orderId = 1;
            var mockOrder = new Order { Id = orderId, Item = "Item1", Quantity = 1 };
            _mockOrderService.Setup(service => service.GetOrderByID(orderId)).Returns(mockOrder);

            // Act
            var result = _controller.GetOrderByID(orderId);

            // Assert
            var okResult = result.Result as OkObjectResult;
            Assert.IsNotNull(okResult);
            var returnOrder = okResult.Value as Order;
            Assert.AreEqual(orderId, returnOrder.Id);
        }

        [Test]
        public void GetOrderById_ReturnsNotFoundResult_WhenOrderNotFound()
        {
            // Arrange
            var orderId = 1;
            _mockOrderService.Setup(service => service.GetOrderByID(orderId)).Returns((Order)null);

            // Act
            var result = _controller.GetOrderByID(orderId);

            // Assert
            Assert.IsInstanceOf<NotFoundResult>(result.Result);
        }

        [Test]
        public void CreateOrder_ReturnsCreatedAtActionResult_WithCreatedOrder()
        {
            // Arrange
            var mockOrder = new Order { Id = 1, Item = "NewItem", Quantity = 1 };
            _mockOrderService.Setup(service => service.CreateOrder(It.IsAny<Order>())).Returns(mockOrder);

            // Act
            var result = _controller.CreateOrder(mockOrder);

            // Assert
            var createdAtActionResult = result.Result as CreatedAtActionResult;
            Assert.IsNotNull(createdAtActionResult);
            var returnOrder = createdAtActionResult.Value as Order;
            Assert.AreEqual(1, returnOrder.Id);
        }

        [Test]
        public void UpdateOrder_ReturnsNoContentResult()
        {
            // Arrange
            var orderId = 1;
            var mockOrder = new Order { Id = orderId, Item = "UpdatedItem", Quantity = 2 };
            _mockOrderService.Setup(service => service.GetOrderByID(orderId)).Returns(mockOrder);

            // Act
            var result = _controller.UpdateOrder(orderId, mockOrder);

            // Assert
            Assert.IsInstanceOf<NoContentResult>(result);
        }

        [Test]
        public void UpdateOrder_ReturnsBadRequestResult_WhenIdsDoNotMatch()
        {
            // Arrange
            var orderId = 1;
            var mockOrder = new Order { Id = 2, Item = "UpdatedItem", Quantity = 2 };

            // Act
            var result = _controller.UpdateOrder(orderId, mockOrder);

            // Assert
            Assert.IsInstanceOf<BadRequestResult>(result);
        }

        [Test]
        public void UpdateOrder_ReturnsNotFoundResult_WhenOrderNotFound()
        {
            // Arrange
            var orderId = 1;
            var mockOrder = new Order { Id = orderId, Item = "UpdatedItem", Quantity = 2 };
            _mockOrderService.Setup(service => service.GetOrderByID(orderId)).Returns((Order)null);

            // Act
            var result = _controller.UpdateOrder(orderId, mockOrder);

            // Assert
            Assert.IsInstanceOf<NotFoundResult>(result);
        }

        [Test]
        public void DeleteOrder_ReturnsNoContentResult()
        {
            // Arrange
            var orderId = 1;
            var mockOrder = new Order { Id = orderId, Item = "Item1", Quantity = 1 };
            _mockOrderService.Setup(service => service.GetOrderByID(orderId)).Returns(mockOrder);

            // Act
            var result = _controller.DeleteOrder(orderId);

            // Assert
            Assert.IsInstanceOf<NoContentResult>(result);
        }

        [Test]
        public void DeleteOrder_ReturnsNotFoundResult_WhenOrderNotFound()
        {
            // Arrange
            var orderId = 1;
            _mockOrderService.Setup(service => service.GetOrderByID(orderId)).Returns((Order)null);

            // Act
            var result = _controller.DeleteOrder(orderId);

            // Assert
            Assert.IsInstanceOf<NotFoundResult>(result);
        }
    }
}