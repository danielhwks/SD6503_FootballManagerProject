using NUnit.Framework;
using FootballStatisticsManagementApp.Controllers;
using FootballStatisticsManagementApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;

namespace Tests
{
    public class PlayerControllerUnitTest
    {
        HSD6503_ProjectSD6503_Project_DBDatabaseFSMDBmdfContext databaseContext;

        [SetUp]
        public void Setup()
        {
            var options = new DbContextOptionsBuilder<HSD6503_ProjectSD6503_Project_DBDatabaseFSMDBmdfContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            databaseContext = new HSD6503_ProjectSD6503_Project_DBDatabaseFSMDBmdfContext(options);
            databaseContext.Database.EnsureCreated();
        }

        [Test]
        public void ValidResponseIndex()
        {
            PlayersController controller = new PlayersController(databaseContext);
            var resultTask = controller.Index();
            resultTask.Wait();
            IActionResult result = resultTask.Result as IActionResult;
            Assert.IsNotNull(result);
        }

        [Test]
        public void ValidResponseDetails()
        {
            PlayersController controller = new PlayersController(databaseContext);
            var resultTask = controller.Details(1);
            resultTask.Wait();
            IActionResult result = resultTask.Result as IActionResult;
            Assert.IsNotNull(result);
        }

        [Test]
        public void ValidResponseCreate()
        {
            PlayersController controller = new PlayersController(databaseContext);
            IActionResult result = controller.Create() as IActionResult;
            Assert.IsNotNull(result);
        }

        [Test]
        public void ValidResponseEdit()
        {
            PlayersController controller = new PlayersController(databaseContext);
            var resultTask = controller.Edit(1);
            resultTask.Wait();
            IActionResult result = resultTask.Result as IActionResult;
            Assert.IsNotNull(result);
        }

        [Test]
        public void ValidResponseDelete()
        {
            PlayersController controller = new PlayersController(databaseContext);
            var resultTask = controller.Delete(1);
            resultTask.Wait();
            IActionResult result = resultTask.Result as IActionResult;
            Assert.IsNotNull(result);
        }
    }
}