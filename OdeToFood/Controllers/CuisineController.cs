using OdeToFood.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OdeToFood.Controllers
{
    public class CuisineController : Controller
    {
        //[Authorize]
        [Log]
        public ActionResult Search(string name)
        {
            throw new Exception("Something terrible has happened");

            var message = Server.HtmlEncode(name);

            return Content(message);
        }

    }
}