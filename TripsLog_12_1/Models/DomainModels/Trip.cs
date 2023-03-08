using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace TripsLog_12_1.Models
{
    public class Trip
    {
        public int TripId { get; set; }

        [Range(1, 999999, ErrorMessage = "Please select a Destination")]
        public int DestinationId { get; set; }

        public Destination Destination { get; set; }
        [DataType(DataType.Date)]
        [Required(ErrorMessage = "Please enter the date your trip starts.")]
        public DateTime? StartDate { get; set; }
        [DataType(DataType.Date)]
        [Required(ErrorMessage = "Please enter the date your trip ends.")]
        public DateTime? EndDate { get; set; }

        public int? AccomodationId { get; set; }

        public Accomodation Accomodation { get; set; }

        public ICollection<TripActivity> TripActivities { get; set; }
    }
}
