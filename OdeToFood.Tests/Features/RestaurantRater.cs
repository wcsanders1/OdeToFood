using System;
using OdeToFood.Models;
using System.Linq;

namespace OdeToFood.Tests.Features
{
    internal class RestaurantRater
    {
        private Restaurant _restautrant;

        public RestaurantRater(Restaurant restaurant)
        {
            _restautrant = restaurant;
        }

        public RatingResult ComputeResult(IRatingAlgorithm algorithm,
                                            int numberOfReviewsToUse)
        {
            var filteredReviews = _restautrant.Reviews.Take(numberOfReviewsToUse);

            return algorithm.Compute(filteredReviews.ToList());
        }
    }
}