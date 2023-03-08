using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TripsLog_12_1.Models
{
    public class Destination
    {
        public int DestinationId { get; set; }

        public string Name { get; set; }

        public ICollection<Trip> Trips { get; set; }
    }
}
