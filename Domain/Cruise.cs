using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Domain
{
    public class Cruise
    {
        public int CruiseId { get; set; }

        [Required(ErrorMessageResourceName = "FieldIsRequired", ErrorMessageResourceType = typeof(Resources.Common))]
        [MinLength(1, ErrorMessageResourceName = "FieldMinLength", ErrorMessageResourceType = typeof(Resources.Common))]
        [MaxLength(32, ErrorMessageResourceName = "FieldMaxLength", ErrorMessageResourceType = typeof(Resources.Common))]
        [Display(Name = nameof(Resources.Domain.CruiseName), ResourceType = typeof(Resources.Domain))]
        public string CruiseName { get; set; }

        [Display(Name = nameof(Resources.Domain.StartDate), ResourceType = typeof(Resources.Domain))]
        [DataType(DataType.DateTime, ErrorMessageResourceName = "FieldMustBeDataTypeDateTime", ErrorMessageResourceType = typeof(Resources.Common))]
        //[Required(ErrorMessageResourceName = "FieldIsRequired", ErrorMessageResourceType = typeof(Resources.Common))]
        public DateTime StartDate { get; set; }

        [Display(Name = nameof(Resources.Domain.EndDate), ResourceType = typeof(Resources.Domain))]
        [DataType(DataType.DateTime, ErrorMessageResourceName = "FieldMustBeDataTypeDateTime", ErrorMessageResourceType = typeof(Resources.Common))]
        //[Required(ErrorMessageResourceName = "FieldIsRequired", ErrorMessageResourceType = typeof(Resources.Common))]
        public DateTime EndDate { get; set; }

        [Required]
        public virtual List<CruiseLeader> CruiseLeaders { get; set; } = new List<CruiseLeader>();

        public virtual List<CruisePerson> CruisePersons { get; set; } = new List<CruisePerson>();

        [Required]
        public virtual List<CruiseStation> CruiseStations { get; set; } = new List<CruiseStation>();

    }
}