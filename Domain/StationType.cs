using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class StationType:BaseEntity
    {
        public int StationTypeId { get; set; }

        [ForeignKey(nameof(StationTypeName))]
        public int StationTypeNameId { get; set; }

        [Required(ErrorMessageResourceName = "FieldIsRequired", ErrorMessageResourceType = typeof(Resources.Common))]
        [Display(Name = nameof(Resources.Domain.StationTypeName), ResourceType = typeof(Resources.Domain))]
        public virtual MultiLangString StationTypeName { get; set; }

        public virtual List<Station> Stations { get; set; } = new List<Station>();

    }
}
