using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TripsLog_12_1.Models;
using Microsoft.EntityFrameworkCore;

namespace TripsLog_12_1.Controllers
{
    public class ManageController : Controller
    {
       private UnitOfWork data { get; set; }

        public ManageController(TripLogContext ctx) => data = new UnitOfWork(ctx);

        [HttpGet]
        public ViewResult Index()
        {
            var vm = new ManageViewModel();
            LoadDropDownData(vm);
            return View(vm);
        }

        [HttpPost]
        public RedirectToActionResult Add(ManageViewModel vm)
        {
            bool needsSave = false;
            string notifyMsg = "";

            if (!string.IsNullOrEmpty(vm.Destination.Name))
            {
                data.Destinations.Insert(vm.Destination);
                notifyMsg = $"{notifyMsg} {vm.Destination.Name}, ";
                needsSave = true;
            }
            if (!string.IsNullOrEmpty(vm.Accomodation.Name))
            {
                data.Accomodations.Insert(vm.Accomodation);
                notifyMsg = $"{notifyMsg} {vm.Accomodation.Name}, ";
                needsSave = true;
            }
            if (!string.IsNullOrEmpty(vm.Activity.Name))
            {
                data.Activities.Insert(vm.Activity);
                notifyMsg = $"{notifyMsg} {vm.Activity.Name}, ";
                needsSave = true;
            }
            if (needsSave)
            { 
                data.Save();
                TempData["message"] = notifyMsg + " added";
            }

            return RedirectToAction("Confirm");
        }

        [HttpPost]
        public IActionResult Delete(ManageViewModel vm)
        {
            bool needsSave = false;
            string notifyMsg = "";

            if (vm.Destination.DestinationId > 0)
            {
                vm.Destination = data.Destinations.Get(vm.Destination.DestinationId);
                data.Destinations.Delete(vm.Destination);
                notifyMsg = $"{notifyMsg} {vm.Destination.Name}, ";
                needsSave = true;
            }
            if (vm.Accomodation.AccomodationId > 0)
            {
                vm.Accomodation = data.Accomodations.Get(vm.Accomodation.AccomodationId);
                data.Accomodations.Delete(vm.Accomodation);
                notifyMsg = $"{notifyMsg} {vm.Accomodation.Name}, ";
                needsSave = true;
            }
            if (vm.Activity.ActivityId > 0)
            {
                vm.Activity = data.Activities.Get(vm.Activity.ActivityId);
                data.Activities.Delete(vm.Activity);
                notifyMsg = $"{notifyMsg} {vm.Activity.Name}, ";
                needsSave = true;
            }
            if (needsSave)
            {
                try
                {
                    data.Save();
                    TempData["message"] = notifyMsg + " deleted";
                }
                catch
                {
                    TempData["message"] = $"Unable to delete {vm.Destination.Name} because it's associated with a trip.";
                    LoadDropDownData(vm);
                    return View("Index", vm);
                }
            }
            return RedirectToAction("Confirm");
        }
        public ViewResult Confirm() => View();

        private void LoadDropDownData(ManageViewModel vm)
        {
            vm.Destinations = data.Destinations.List(new QueryOptions<Destination> {
                OrderBy = d => d.Name
            });
            vm.Accomodations = data.Accomodations.List(new QueryOptions<Accomodation> {
                OrderBy = d => d.Name
            });
            vm.Activities = data.Activities.List(new QueryOptions<Activity> {
                OrderBy = d => d.Name
            });
        }
    }
}
