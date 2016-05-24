using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class ProjectCruise
    {
        public int ProjectCruiseId { get; set; }
        public int  ProjectId { get; set; }
        public virtual Project Project { get; set; }

        public int CruiseId { get; set; }
        public virtual Cruise Cruise { get; set; }

    }
}
