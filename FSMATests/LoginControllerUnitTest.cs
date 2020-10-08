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
            LoginController controller = new LoginController();
            IActionResult result = controller.Index() as IActionResult;
            Assert.IsNotNull(result);
        }
    }
}