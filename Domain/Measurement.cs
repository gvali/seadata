using System;
using System.ComponentModel.DataAnnotations;

namespace Domain
{
    public class Measurement
    {
        public int MeasurementId { get; set; }
        public int StationId { get; set; }
        public float MeasurementDepth { get; set; }
        public float MeasurementTemperature { get; set; }
        public float MeasurementSalinity { get; set; }
        public float MeasurementOxygen { get; set; }
        public float MeasurementChlorophyll { get; set; }
        [DataType(DataType.DateTime)]
        public DateTime MeasurementDateTime { get; set; }
        [DataType(DataType.Date)]
        public DateTime MeasurementDate { get; set; }
        [DataType(DataType.Time)]
        public DateTime MeasurementTime { get; set; }

    }
}