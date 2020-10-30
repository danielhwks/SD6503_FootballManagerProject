using NUnit.Framework;
using FootballStatisticsManagementApp.Controllers;
using FootballStatisticsManagementApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;

namespace Tests
{
    public class LeagueControllerUnitTest
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
            LeaguesController controller = new LeaguesController(databaseContext);
            var resultTask = controller.Index();
            resultTask.Wait();
            IActionResult result = resultTask.Result as IActionResult;
            Assert.IsNotNull(result);
        }

        [Test]
        public void ValidResponseDetails()
        {
            LeaguesController controller = new LeaguesController(databaseContext);
            var resultTask = controller.Details(1);
            resultTask.Wait();
            IActionResult result = resultTask.Result as IActionResult;
            Assert.IsNotNull(result);
        }

        [Test]
        public void ValidResponseCreate()
        {
            LeaguesController controller = new LeaguesController(databaseContext);
            IActionResult result = controller.Create() as IActionResult;
            Assert.IsNotNull(result);
        }

        [Test]
        public void ValidResponseEdit()
        {
            LeaguesController controller = new LeaguesController(databaseContext);
            var resultTask = controller.Edit(1);
            resultTask.Wait();
            IActionResult result = resultTask.Result as IActionResult;
            Assert.IsNotNull(result);
        }

        [Test]
        public void ValidResponseDelete()
        {
            LeaguesController controller = new LeaguesController(databaseContext);
            var resultTask = controller.Delete(1);
            resultTask.Wait();
            IActionResult result = resultTask.Result as IActionResult;
            Assert.IsNotNull(result);
            //var countTask = databaseContext.League.CountAsync();
            //countTask.Wait();
            //int count = countTask.Result;
            //Assert.AreEqual(1, count);

            //databaseContext.League.Add(new League()

            //{
            //    LeagueId = 1,
            //    Year = "2007"
            //});
            //databaseContext.SaveChanges();
        }

        [Test]
        public void CheckFunctionCreate()
        {
            var preCountTask = databaseContext.League.CountAsync();
            preCountTask.Wait();
            int preCount = preCountTask.Result;
            LeaguesController controller = new LeaguesController(databaseContext);
            League l = new League();
            l.Year = "2007";
            var resultTask = controller.Create(l);
            resultTask.Wait();
            IActionResult result = resultTask.Result as IActionResult;
            var postCountTask = databaseContext.League.CountAsync();
            postCountTask.Wait();
            int postCount = postCountTask.Result;
            Assert.AreEqual(preCount+1, postCount);
        }
    }
}