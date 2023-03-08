using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TripsLog_12_1.Models
{
    public class DropDownViewModel
    {
        public IEnumerable<Destination> Destinations { get; set; }

        public IEnumerable<Accomodation> Accomodations { get; set; }

        public IEnumerable<Activity> Activities { get; set; }

    }
}
