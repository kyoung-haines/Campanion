using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;


namespace Campanion.Shared.DTOs.Trip
{
    public class CreateTripDto
    {
        [Required]
        public string TripName { get; set; }
        [Required]
        public DateOnly TripStartDate { get; set; }
        [Required]
        public DateOnly TripEndDate { get; set; }
        public DateTime TripCreatedAt { get; set; }


        // this will need to refactored
        // this should be a list of users (friends) that have been added
        // it won't be strings I don't think
        public List<string> TripAttendees { get; set; }
    }
}
