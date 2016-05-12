using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Domain
{
    public class Cruise
    {
        public int CruiseId { get; set; }

        [MaxLength(64)]
        public string CruiseName { get; set; }

        [Required]
        public virtual List<CruiseLeader> CruiseLeaders { get; set; } = new List<CruiseLeader>();

        public virtual List<CruisePerson> CruisePersons { get; set; } = new List<CruisePerson>();

        public virtual List<CruiseStation> CruiseStations { get; set; } = new List<CruiseStation>();

    }
}