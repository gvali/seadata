using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Domain
{
    public class Project
    {
        public int ProjectId { get; set; }

        [Required(ErrorMessageResourceName = "FieldIsRequired", ErrorMessageResourceType = typeof(Resources.Common))]
        [MinLength(1, ErrorMessageResourceName = "FieldMinLength", ErrorMessageResourceType = typeof(Resources.Common))]
        [MaxLength(32, ErrorMessageResourceName = "FieldMaxLength", ErrorMessageResourceType = typeof(Resources.Common))]
        [Display(Name = nameof(Resources.Domain.ProjectName), ResourceType = typeof(Resources.Domain))]
        public string ProjectName { get; set; }

        [Display(Name = nameof(Resources.Domain.StartDate), ResourceType = typeof(Resources.Domain))]
        [DataType(DataType.DateTime, ErrorMessageResourceName = "FieldMustBeDataTypeDateTime", ErrorMessageResourceType = typeof(Resources.Common))]
        //[Required(ErrorMessageResourceName = "FieldIsRequired", ErrorMessageResourceType = typeof(Resources.Common))]
        public DateTime StartDate { get; set; }

        [Display(Name = nameof(Resources.Domain.EndDate), ResourceType = typeof(Resources.Domain))]
        [DataType(DataType.DateTime, ErrorMessageResourceName = "FieldMustBeDataTypeDateTime", ErrorMessageResourceType = typeof(Resources.Common))]
        //[Required(ErrorMessageResourceName = "FieldIsRequired", ErrorMessageResourceType = typeof(Resources.Common))]
        public DateTime EndDate { get; set; }

        [Required]
        public virtual List<ProjectLeader> ProjectLeaders { get; set; } = new List<ProjectLeader>();

        public virtual List<ProjectPerson> ProjectPersons { get; set; } = new List<ProjectPerson>();

    }
}