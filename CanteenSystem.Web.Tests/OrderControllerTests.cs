using System.Collections.Generic;
using System.Threading.Tasks;
using CanteenSystem.Dto.Models;
using CanteenSystem.Web.Controllers;
using CanteenSystem.Web.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Xunit;

namespace CanteenSystem.Web.Test
{
    public class OrderControllerTests
    {
       
        [Fact]
        public async Task GetOrderConfirmationView()
        {
            // Arrange
            var controller = new OrdersController(new Dal.CanteenSystemDbContext());
            //Act
            var result = await controller.OrderConfirmation("success") as ViewResult;
            //Assert
            Assert.Equal("OrderConfirmation", result.ViewName); 
         

        }
        [Fact]
        public async Task VerifyGetOrderConfirmationView()
        {
            // Arrange
            var controller = new OrdersController(new Dal.CanteenSystemDbContext());
            //Act
            var result = await controller.OrderConfirmation("success") as ViewResult;
            //Assert
            var orderConfirmationModel = (OrderConfirmationModel)result.ViewData.Model;
            Assert.NotNull(orderConfirmationModel.NotificationMessage); 
        }
        [Fact] 
        public async Task GetIndexView()
        {
            // Arrange
            var controller = new OrdersController(new Dal.CanteenSystemDbContext());
            //Act
            var result = await controller.Index() as ViewResult;
            //Assert
            Assert.Equal("Index", result.ViewName);


        }
        [Fact]
        public async Task VerifyGetIndexView()
        {
            // Arrange
            var controller = new OrdersController(new Dal.CanteenSystemDbContext());
            //Act
            var result = await controller.Index() as ViewResult;
            //Assert
            var orders = (List<Order>)result.ViewData.Model;
            Assert.True(orders.Count >0);
        }  
    }
}