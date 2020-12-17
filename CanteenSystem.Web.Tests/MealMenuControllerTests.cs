using System.Collections.Generic;
using System.Threading.Tasks;
using CanteenSystem.Dto.Models;
using CanteenSystem.Web.Controllers;
using Microsoft.AspNetCore.Mvc;
using Xunit;

namespace CanteenSystem.Web.Test
{
    public class MealMenuControllerTests
    {
       
        [Fact]
        public async Task GetIndexView()
        {
            // Arrange
            var controller = new MealMenusController(new Dal.CanteenSystemDbContext());
            //Act
            var result = await controller.Index() as ViewResult;
            //Assert
            Assert.Equal("Index", result.ViewName);  

        }

        [Fact]
        public async Task VerifyGetStudentView()
        {
            // Arrange
            var controller = new MealMenusController(new Dal.CanteenSystemDbContext());
            //Act
            var result = await controller.Index() as ViewResult;
            //Assert 
            var meals = (List<MealMenu>)result.ViewData.Model;
            Assert.True(meals.Count >0);
        } 

        [Fact]
        public async Task GetDetailsView()
        {
            // Arrange
            var controller = new MealMenusController(new Dal.CanteenSystemDbContext());
            //Act
            var result = await controller.Details(1) as ViewResult;
            //Assert
            Assert.Equal("Details", result.ViewName); 
        }
        [Fact]
        public async Task VerifyGetDetailsView()
        {
            // Arrange
            var controller = new MealMenusController(new Dal.CanteenSystemDbContext());
            //Act
            var result = await controller.Details(1) as ViewResult;
            //Assert 
            var meal = (MealMenu)result.ViewData.Model;
            Assert.NotNull(meal);
        }
    }
}