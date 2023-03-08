using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TripsLog_12_1.Models
{
    public class ManageViewModel : DropDownViewModel
    {
        public Destination Destination { get; set; }

        public Accomodation Accomodation { get; set; }

        public Activity Activity { get; set; }
    }
}
