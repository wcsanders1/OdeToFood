using Microsoft.VisualStudio.TestTools.UnitTesting;
using OdeToFood.Controllers;
using OdeToFood.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OdeToFood.Tests.Controllers
{
    [TestClass]
    public class RestaurantControllerTests
    {
        public void Create_Saves_Restaurant_When_Valid()
        {
            var db = new FakeOdeToFoodDb();
            var controller = new RestaurantsController(db);

            controller.Create(new Restaurant());

            Assert.AreEqual(1, db.Added.Count);
            Assert.AreEqual(true, db.Saved);
        }

        public void Create_Does_Not_Save_Restaurant_When_Invalid()
        {
            var db = new FakeOdeToFoodDb();
            var controller = new RestaurantsController(db);
            controller.ModelState.AddModelError("", "Invalid");

            controller.Create(new Restaurant());

            Assert.AreEqual(0, db.Added.Count);
        }
    }
}
