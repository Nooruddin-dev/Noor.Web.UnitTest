
using Microsoft.AspNetCore.Mvc;
using Noor.Web.Api.Test.Controllers;
using Noor.Web.Api.Test.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Noor.XUnit.TestCases
{
    public class TestTodoController
    {
        // Simple test case by creating controller object
        [Fact]
        public async Task GetAllAsync_ShouldReturn200Status()
        {
            ///Arrange
            var toDoController = new TodoController();

            ///Act
            var result = (OkObjectResult)await toDoController.GetAllAsync();
            var usersList = (List<Users>)result.Value;

            ///Assert
            Assert.Equal("Noor", usersList?.FirstOrDefault()?.Name);
        }
    }
}
