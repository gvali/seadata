using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Domain
{
    public class Project
    {
        public int ProjectId { get; set; }
        [MaxLength(64)]
        public string ProjectName { get; set; }

        [Required]
        public virtual List<ProjectLeader> ProjectLeaders { get; set; } = new List<ProjectLeader>();

        public virtual List<ProjectPerson> ProjectPersons { get; set; } = new List<ProjectPerson>();

    }
}