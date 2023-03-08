using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TripsLog_12_1.Models
{
    public class TripViewModel : DropDownViewModel
    {
        public Trip Trip { get; set;}

        public int PageNumber { get; set; }

        public string DestinationName { get; set; }

        public int[] SelectedActivities { get; set; }
    }
}
