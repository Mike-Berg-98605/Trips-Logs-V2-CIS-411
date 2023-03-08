using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TripsLog_12_1.Models;

namespace TripsLog_12_1.Controllers
{
    public class TripController : Controller
    {
        private UnitOfWork data { get; set; }

        public TripController(TripLogContext ctx) => data = new UnitOfWork(ctx);

        public RedirectToActionResult Index() => RedirectToAction("Index", "Home");

        [HttpGet]
        public ViewResult Add(string id = "")
        {
            var vm = new TripViewModel();

            if (id.ToLower() == "page2")
            {
                vm.PageNumber = 2;

                int destId = (int)TempData.Peek(nameof(Trip.DestinationId));
                vm.DestinationName = data.Destinations.Get(destId).Name;

                vm.Activities = data.Activities.List(new QueryOptions<Activity> {
                    OrderBy = a => a.Name
                });

                return View("Add2", vm);
            }
            else
            {
                vm.PageNumber = 1;

                vm.Destinations = data.Destinations.List(new QueryOptions<Destination> {
                    OrderBy = d => d.Name
                });

                vm.Accomodations = data.Accomodations.List(new QueryOptions<Accomodation>
                {
                    OrderBy = a => a.Name
                });

                return View("Add1", vm);
            }
        }
        [HttpPost]
        public IActionResult Add(TripViewModel vm)
        {
            if (vm.PageNumber == 1)
            {
                if (ModelState.IsValid)
                {
                    TempData[nameof(Trip.DestinationId)] = vm.Trip.DestinationId;
                    TempData[nameof(Trip.StartDate)] = vm.Trip.StartDate;
                    TempData[nameof(Trip.EndDate)] = vm.Trip.EndDate;

                    if (vm.Trip.AccomodationId > 0)
                        TempData[nameof(Trip.AccomodationId)] = vm.Trip.AccomodationId;

                    return RedirectToAction("Add", new { id = "Page2" });
                }
                else
                {
                    vm.Destinations = data.Destinations.List(new QueryOptions<Destination>
                    {
                        OrderBy = d => d.Name
                    });
                    vm.Accomodations = data.Accomodations.List(new QueryOptions<Accomodation>
                    {
                        OrderBy = a => a.Name
                    });

                    return View("Add1", vm);
                }
            }
            else if (vm.PageNumber == 2)
            {
                vm.Trip = new Trip
                {
                    DestinationId = (int)TempData[nameof(Trip.DestinationId)],
                    StartDate = (DateTime)TempData[nameof(Trip.StartDate)],
                    EndDate = (DateTime)TempData[nameof(Trip.EndDate)]
                };
                if (TempData.Keys.Contains(nameof(Trip.AccomodationId)))
                    vm.Trip.AccomodationId = (int)TempData[nameof(Trip.AccomodationId)];

                foreach (int activityId in vm.SelectedActivities)
                {
                    if (vm.Trip.TripActivities == null) vm.Trip.TripActivities = new List<TripActivity>();
                    vm.Trip.TripActivities.Add(new TripActivity { ActivityId = activityId });
                }
                data.Trips.Insert(vm.Trip);
                data.Save();

                var dest = data.Destinations.Get(vm.Trip.DestinationId);
                TempData["message"] = $"Trip to {dest.Name} added.";

                return RedirectToAction("Index", "Home");
            }
            else
            {
                return View("Add2", vm);
            }
        }
    }
}
