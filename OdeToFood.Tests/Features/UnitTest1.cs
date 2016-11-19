using System;
using System.Text;
using System.Linq;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OdeToFood.Models;

namespace OdeToFood.Tests.Features
{
    /// <summary>
    /// Summary description for UnitTest1
    /// </summary>
    [TestClass]
    public class UnitTest1
    {
        public UnitTest1()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        private TestContext testContextInstance;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        #region Additional test attributes
        //
        // You can use the following additional attributes as you write your tests:
        //
        // Use ClassInitialize to run code before running the first test in the class
        // [ClassInitialize()]
        // public static void MyClassInitialize(TestContext testContext) { }
        //
        // Use ClassCleanup to run code after all tests in a class have run
        // [ClassCleanup()]
        // public static void MyClassCleanup() { }
        //
        // Use TestInitialize to run code before running each test 
        // [TestInitialize()]
        // public void MyTestInitialize() { }
        //
        // Use TestCleanup to run code after each test has run
        // [TestCleanup()]
        // public void MyTestCleanup() { }
        //
        #endregion

        [TestMethod]
        public void Computes_Result_For_One_Review()
        {
            var data = BuildRestaurantAndReviews(ratings: 4);

            var rater = new RestaurantRater(data);
            var result = rater.ComputeResult(new SimpleRatingAlgorithm(), 10);

            Assert.AreEqual(4, result.Rating);
        }

        [TestMethod]
        public void Computes_Results_For_Two_Reviews()
        {
            var data = BuildRestaurantAndReviews(ratings: new[] { 4, 8 });

            var rater = new RestaurantRater(data);
            var result = rater.ComputeResult(new SimpleRatingAlgorithm(), 10);

            Assert.AreEqual(6, result.Rating);
        }

        [TestMethod]
        public void Rating_Includes_Only_First_N_Reviews()
        {
            var data = BuildRestaurantAndReviews(1, 1, 1, 10, 10, 10);

            var rater = new RestaurantRater(data);
            var result = rater.ComputeResult(new SimpleRatingAlgorithm(), 3);

            Assert.AreEqual(1, result.Rating);
        }

        [TestMethod]
        public void Weighted_Average_For_Two_Reviews()
        {
            var data = BuildRestaurantAndReviews(3, 9);
            var rater = new RestaurantRater(data);
            var result = rater.ComputeResult(new WeightedRatingAlgorithm(), 10);

            Assert.AreEqual(5, result.Rating);
        }

        private Restaurant BuildRestaurantAndReviews(params int[] ratings)
        {
            var restaurant = new Restaurant();

            restaurant.Reviews =
                ratings.Select(r => new RestaurantReview { Rating = r })
                .ToList();

            return restaurant;
        }
    }
}
