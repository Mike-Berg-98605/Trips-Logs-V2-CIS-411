using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace TripsLog_12_1.Models
{
    internal class TripConfig : IEntityTypeConfiguration<Trip>
    {
        public void Configure(EntityTypeBuilder<Trip> entity)
        {
            entity.HasOne(t => t.Destination)
                .WithMany(d => d.Trips)
                .OnDelete(DeleteBehavior.Restrict);


            entity.HasOne(t => t.Accomodation)
                .WithMany(d => d.Trips)
                .OnDelete(DeleteBehavior.SetNull);
        }
    }
}
