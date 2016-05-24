using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Domain
{
    public class Station:BaseEntity
    {
        public int StationId { get; set; }

        [Required(ErrorMessageResourceName = "FieldIsRequired", ErrorMessageResourceType = typeof(Resources.Common))]
        [MinLength(1, ErrorMessageResourceName = "FieldMinLength", ErrorMessageResourceType = typeof(Resources.Common))]
        [MaxLength(32, ErrorMessageResourceName = "FieldMaxLength", ErrorMessageResourceType = typeof(Resources.Common))]
        [Display(Name = nameof(Resources.Domain.StationName), ResourceType = typeof(Resources.Domain))]
        public string StationName { get; set; }

        public float StatLon { get; set; }
        public float StatLat { get; set; }
        public int StationTypeId { get; set; }
        public virtual StationType StationType { get; set; }

    }
}