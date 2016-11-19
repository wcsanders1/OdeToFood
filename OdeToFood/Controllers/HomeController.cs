using OdeToFood.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
using System.Configuration;

namespace OdeToFood.Controllers
{
    public class HomeController : Controller
    {
        IOdeToFoodDb _db;

        public HomeController()
        {
            _db = new OdeToFoodDb();
        }

        public HomeController(IOdeToFoodDb db)
        {
            _db = db;
        }

        public ActionResult Autocomplete(string term)
        {
            var model =
                _db.Query<Restaurant>()
                    .Where(r => r.Name.StartsWith(term))
                    .Take(10)
                    .Select(r => new
                    {
                        label = r.Name
                    });

            return Json(model, JsonRequestBehavior.AllowGet);
        }

        [OutputCache(CacheProfile = "Long",
            VaryByHeader = "X-Requested-With;Accept-Language",
            Location = System.Web.UI.OutputCacheLocation.Server)]
        public ActionResult Index(string searchTerm = null, int page = 1)
        {
            //var model =
            //    from r in _db.Restaurants
            //    orderby r.Reviews.Average(review => review.Rating) descending
            //    select new RestaurantListViewModel
            //    {
            //        Id = r.Id,
            //        Name = r.Name,
            //        City = r.City,
            //        Country = r.Country,
            //        CountOfReviews = r.Reviews.Count()
            //    };

            //Above and below work equally well here except only in below can you use .Take(10)

            var model =
                _db.Query<Restaurant>()
                .OrderBy(r => r.Id)
                //.OrderByDescending(r => r.Reviews.Average(review => review.Rating))
                .Where(r => searchTerm == null || r.Name.StartsWith(searchTerm))            
                .Select(r => new RestaurantListViewModel
                        { 
                            Id = r.Id,
                            Name = r.Name,
                            City = r.City,
                            Country = r.Country,
                            CountOfReviews = r.Reviews.Count()
                        }).ToPagedList(page, 10);    //this specifies 10 restaurants per page

            ViewBag.MailServer = ConfigurationManager.AppSettings["Mailserver"];

            if (Request.IsAjaxRequest())
            {
                return PartialView("_Restaurants", model);
            }

            return View(model);
        }

        public ActionResult About()
        {
            var model = new AboutModel();
            model.Name = "Bill";
            model.Location = "Grand Rapids";

            return View(model);
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        protected override void Dispose(bool disposing)
        {
            if (_db != null)
            {
                _db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}