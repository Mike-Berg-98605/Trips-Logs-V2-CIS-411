using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace TripsLog_12_1.Models
{
    public interface IUnitOfWork
    {
        Repository<Trip> Trips { get; }

        Repository<Destination> Destinations { get; }

        Repository<Accomodation> Accomodations { get; }

        Repository<Activity> Activities { get; }

        void Save();
    }
}
