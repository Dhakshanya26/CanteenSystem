using System.Collections.Generic;
using System.Threading.Tasks;
using CanteenSystem.Dto.Models;
using CanteenSystem.Web.Controllers;
using CanteenSystem.Web.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Xunit;

namespace CanteenSystem.Web.Test
{
    public class CartsControllerTests
    {
       
        [Fact]
        public async Task GetIndexView()
        {
            // Arrange
            var controller = new CartsController(new Dal.CanteenSystemDbContext());
            //Act
            var result = await controller.Index(3) as ViewResult;
            //Assert
            Assert.Equal("Index", result.ViewName); 
         

        }
        [Fact]
        public async Task VerifyGetIndexView()
        {
            // Arrange
            var controller = new CartsController(new Dal.CanteenSystemDbContext());
            //Act
            var result = await controller.Index(1) as ViewResult;
            //Assert
            var carts = (List<Cart>)result.ViewData.Model;
            Assert.True(carts.Count > 0); 
        }
        [Fact]
        public async Task GetConfirmOrderPayNowView()
        {
            // Arrange
            var controller = new CartsController(new Dal.CanteenSystemDbContext());
            //Act
            var result = await controller.ConfirmOrderPayNow(3) as ViewResult;
            //Assert
            Assert.Equal("ConfirmOrderPayNow", result.ViewName);


        }
        [Fact]
        public async Task VerifyGetConfirmOrderPayNowView()
        {
            // Arrange
            var controller = new CartsController(new Dal.CanteenSystemDbContext());
            //Act
            var result = await controller.ConfirmOrderPayNow(1) as ViewResult;
            //Assert
            var card = (CardModel)result.ViewData.Model;
            Assert.NotNull(card);
        }  
    }
}