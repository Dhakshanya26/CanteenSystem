using System.Collections.Generic;
using System.Threading.Tasks;
using CanteenSystem.Dto.Models;
using CanteenSystem.Web.Controllers;
using CanteenSystem.Web.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Xunit;

namespace CanteenSystem.Web.Test
{
    public class CardsControllerTests
    {
       
        [Fact]
        public async Task GetStudentView()
        {
            // Arrange
            var controller = new CardsController(new Dal.CanteenSystemDbContext());
            //Act
            var result = await controller.Student(3) as ViewResult;
            //Assert
            Assert.Equal("Student", result.ViewName); 
         

        }
        [Fact]
        public async Task VerifyGetStudentView()
        {
            // Arrange
            var controller = new CardsController(new Dal.CanteenSystemDbContext());
            //Act
            var result = await controller.Student(1,"test") as ViewResult;
            //Assert
            var cards = (List<Card>)result.ViewData.Model;
            Assert.True(cards.Count > 0); 
        }
        [Fact]
        public async Task GetParentView()
        {
            // Arrange
            var controller = new CardsController(new Dal.CanteenSystemDbContext());
            //Act
            var result = await controller.Parent(3) as ViewResult;
            //Assert
            Assert.Equal("Parent", result.ViewName);


        }
        [Fact]
        public async Task VerifyGetParentView()
        {
            // Arrange
            var controller = new CardsController(new Dal.CanteenSystemDbContext());
            //Act
            var result = await controller.Parent(1, "test") as ViewResult;
            //Assert
            var cards = (List<Card>)result.ViewData.Model;
            Assert.True(cards.Count > 0);
        }
        [Fact]
        public async Task GetAddStudentFundView()
        {
            // Arrange
            var controller = new CardsController(new Dal.CanteenSystemDbContext());
            //Act
            var result = await controller.AddStudentFund(3,7) as ViewResult;
            //Assert
            Assert.Equal("AddStudentFund", result.ViewName);


        }
        [Fact]
        public async Task VerifyGetAddStudentFundView()
        {
            // Arrange
            var controller = new CardsController(new Dal.CanteenSystemDbContext());
            //Act
            var result = await controller.AddStudentFund(1,6) as ViewResult;
            //Assert
            var cardModel = (CardModel)result.ViewData.Model;
            Assert.NotNull(cardModel);
        }
    }
}