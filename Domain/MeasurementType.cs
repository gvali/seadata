using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class MeasurementType:BaseEntity
    {
        public int MeasurementTypeId { get; set; }

        [ForeignKey(nameof(MeasurementTypeName))]
        public int MeasurementTypeNameId { get; set; }
        public virtual MultiLangString MeasurementTypeName { get; set; }

        public virtual List<Measurement> Measurements { get; set; } = new List<Measurement>();

    }
}
