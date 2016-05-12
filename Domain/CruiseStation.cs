using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class CruiseStation
    {
        public int CruiseStationId { get; set; }
        public int  CruiseId { get; set; }
        public virtual Cruise Cruise { get; set; }

        public int StationId { get; set; }
        public virtual Station Station { get; set; }
    }
}
