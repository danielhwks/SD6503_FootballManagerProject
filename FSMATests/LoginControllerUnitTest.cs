using NUnit.Framework;
using FootballStatisticsManagementApp.Controllers;
using FootballStatisticsManagementApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;

namespace Tests
{
    public class LoginControllerUnitTest
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void ValidResponseIndex()
        {
            // Create the Player controller
            LoginController controller = new LoginController();

            // Call the Index page
            IActionResult result = controller.Index() as IActionResult;

            // Check that the controller returned a valid response
            Assert.IsNotNull(result);
        }
    }
}