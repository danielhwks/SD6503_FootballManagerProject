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
            // Creates a new database context in memory for each test, this ensures that nothing is kept
            //  between tests that could interfere with the testing or give incorrent results.
            var options = new DbContextOptionsBuilder<HSD6503_ProjectSD6503_Project_DBDatabaseFSMDBmdfContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            databaseContext = new HSD6503_ProjectSD6503_Project_DBDatabaseFSMDBmdfContext(options);
            databaseContext.Database.EnsureCreated();
        }

        [Test]
        public void ValidResponseIndex()
        {
            // Create the League controller
            LeaguesController controller = new LeaguesController(databaseContext);
            
            // Call the Index page and wait for a response
            var resultTask = controller.Index();
            resultTask.Wait();
            IActionResult result = resultTask.Result as IActionResult;

            // Check that the controller returned a valid response
            Assert.IsNotNull(result);
        }

        [Test]
        public void ValidResponseDetails()
        {
            // Create the League controller
            LeaguesController controller = new LeaguesController(databaseContext);

            // Call the Details page and wait for a response
            var resultTask = controller.Details(1);
            resultTask.Wait();
            IActionResult result = resultTask.Result as IActionResult;

            // Check that the controller returned a valid response
            Assert.IsNotNull(result);
        }

        [Test]
        public void ValidResponseCreate()
        {
            // Create the League controller
            LeaguesController controller = new LeaguesController(databaseContext);

            // Call the Create page
            IActionResult result = controller.Create() as IActionResult;

            // Check that the controller returned a valid response
            Assert.IsNotNull(result);
        }

        [Test]
        public void ValidResponseEdit()
        {
            // Create the League controller
            LeaguesController controller = new LeaguesController(databaseContext);

            // Call the Edit page and wait for a response
            var resultTask = controller.Edit(1);
            resultTask.Wait();
            IActionResult result = resultTask.Result as IActionResult;

            // Check that the controller returned a valid response
            Assert.IsNotNull(result);
        }

        [Test]
        public void ValidResponseDelete()
        {
            // Create the League controller
            LeaguesController controller = new LeaguesController(databaseContext);

            // Call the Delete page and wait for a response
            var resultTask = controller.Delete(1);
            resultTask.Wait();
            IActionResult result = resultTask.Result as IActionResult;

            // Check that the controller returned a valid response
            Assert.IsNotNull(result);
        }

        [Test]
        public void CheckFunctionCreate()
        {
            // Count the number of leagues in the database
            var preCountTask = databaseContext.League.CountAsync();
            preCountTask.Wait();
            int preCount = preCountTask.Result;

            // Create the controller and call the Create method
            LeaguesController controller = new LeaguesController(databaseContext);
            League league = new League();
            league.Year = "2007";
            var resultTask = controller.Create(league);
            resultTask.Wait();
            IActionResult result = resultTask.Result as IActionResult;

            // Count the number of leagues in the database after the change
            var postCountTask = databaseContext.League.CountAsync();
            postCountTask.Wait();
            int postCount = postCountTask.Result;

            // Check that there is one more league in the database than what was
            //  in it previous to the Create method call
            Assert.AreEqual(preCount+1, postCount);
        }

        [Test]
        public void CheckFunctionEdit()
        {
            // Add an initial League object to the database prior to editing
            League league = new League();
            league.LeagueId = 1;
            league.Year = "2007";
            databaseContext.League.Add(league);
            databaseContext.SaveChanges();

            // Create the controller and call the Edit method, passing the new League object
            LeaguesController controller = new LeaguesController(databaseContext);
            league.Year = "2019";
            var resultTask = controller.Edit(1, league);
            resultTask.Wait();
            IActionResult result = resultTask.Result as IActionResult;

            // Fetch the league object from the database and check if it's value has been updated.
            var postLeagueTask = databaseContext.League.FirstOrDefaultAsync(l => l.LeagueId == 1);
            postLeagueTask.Wait();
            var postLeague = postLeagueTask.Result;
            Assert.AreEqual(postLeague.Year, "2019");
        }

        [Test]
        public void CheckFunctionDelete()
        {
            // Add an initial League object to the database prior to deleting
            League league = new League();
            league.LeagueId = 1;
            league.Year = "2007";
            databaseContext.League.Add(league);
            databaseContext.SaveChanges();

            // Count the number of leagues in the database
            var preCountTask = databaseContext.League.CountAsync();
            preCountTask.Wait();
            int preCount = preCountTask.Result;

            // Create the controller and call the Create method
            LeaguesController controller = new LeaguesController(databaseContext);
            var resultTask = controller.DeleteConfirmed(1);
            resultTask.Wait();
            IActionResult result = resultTask.Result as IActionResult;

            // Count the number of leagues in the database after the change
            var postCountTask = databaseContext.League.CountAsync();
            postCountTask.Wait();
            int postCount = postCountTask.Result;

            // Check that there is one more league in the database than what was
            //  in it previous to the Create method call
            Assert.AreEqual(preCount - 1, postCount);
        }
    }
}