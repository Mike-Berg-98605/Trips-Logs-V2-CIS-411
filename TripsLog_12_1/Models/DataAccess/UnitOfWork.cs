using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace TripsLog_12_1.Models
{
    public class UnitOfWork : IUnitOfWork
    {
        private TripLogContext context { get; set; }

        public UnitOfWork(TripLogContext ctx) => context = ctx;

        private Repository<Trip> trips;

        public Repository<Trip> Trips
        {
            get
            {
                if (trips == null) trips = new Repository<Trip>(context);
                return trips;
            }
        }

        private Repository<Destination> destinations;

        public Repository<Destination> Destinations
        {
            get
            {
                if (destinations == null) destinations = new Repository<Destination>(context);
                return destinations;
            }
        }

        private Repository<Accomodation> accomodations;

        public Repository<Accomodation> Accomodations
        {
            get
            {
                if (accomodations == null) accomodations = new Repository<Accomodation>(context);
                return accomodations;
            }
        }

        private Repository<Activity> activities;

        public Repository<Activity> Activities
        {
            get
            {
                if (activities == null) activities = new Repository<Activity>(context);
                return activities;
            }
        }
        public void Save() => context.SaveChanges();
    }
}
