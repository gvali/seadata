using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class StationMeasurement
    {
        public int StationMeasurementId { get; set; }

        public int StationId { get; set; }

        public virtual Station Station { get; set; }

        public int MeasurementId { get; set; }

        public virtual Measurement Measurement { get; set; }
    }
}
