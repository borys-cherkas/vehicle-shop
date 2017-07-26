using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace UnitTests.Controllers.Home
{
    public class HomeController_Error_Test
    {

        [Fact]
        public void Error_ViewData_Message()
        {
            // Arrange
            //HomeController controller = new HomeController();

            //// Act
            //ViewResult result = controller.Index() as ViewResult;

            //// Assert
            //Assert.Equal("Hello world!", result?.ViewData["Message"]);
        }

        [Fact]
        public void Error_ViewResult_NotNull()
        {
            // Arrange
            //HomeController controller = new HomeController();
            //// Act
            //ViewResult result = controller.Index() as ViewResult;
            //// Assert
            //Assert.NotNull(result);
        }

        [Fact]
        public void Error_ViewName_EqualError()
        {
            // Arrange
            //HomeController controller = new HomeController();
            //// Act
            //ViewResult result = controller.Index() as ViewResult;
            //// Assert
            //Assert.Equal("Index", result?.ViewName);
        }
    }
}
