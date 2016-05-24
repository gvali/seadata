using System;
using System.ComponentModel.DataAnnotations;

namespace Domain
{
    public class Measurement
    {
        public int MeasurementId { get; set; }
        public int StationId { get; set; }
        public float MeasurementDepth { get; set; }
        public int MeasurementType { get; set; }
        public int MeasurementValue { get; set; }
        [DataType(DataType.DateTime)]
        public DateTime MeasurementDateTime { get; set; }
        [DataType(DataType.Date)]
        public DateTime MeasurementDate { get; set; }
        [DataType(DataType.Time)]
        public DateTime MeasurementTime { get; set; }

    }
}