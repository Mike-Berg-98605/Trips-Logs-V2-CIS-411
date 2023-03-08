using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using TripsLog_12_1.Models;

namespace TripsLog_12_1.Controllers
{
    public class HomeController : Controller
    {
        private Repository<Trip> data { get; set; }

        public HomeController(TripLogContext ctx) => data = new Repository<Trip>(ctx);

        public ViewResult Index()
        {
            var options = new QueryOptions<Trip>
            {
                Includes = "Destination, Accomodation, TripActivities.Activity",
                OrderBy = t => t.StartDate
            };

            var trips = data.List(options);
            return View(trips);
        }
    }
}
