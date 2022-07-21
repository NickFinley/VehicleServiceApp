using Microsoft.AspNetCore.Mvc;

namespace VehicleServiceApp.Models
{
    public class QueryParameters
    {
        [FromQuery(Name = "makes")]
        public string[]? Makes { get; set; }

        [FromQuery(Name = "models")]
        public string[]? Models { get; set; }

        [FromQuery(Name = "minYear")]
        public int MinYear { get; set; }

        [FromQuery(Name = "maxYear")]
        public int MaxYear { get; set; }
    }
}
