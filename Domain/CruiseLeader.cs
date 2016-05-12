using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class CruiseLeader
    {
        public int CruiseLeaderId { get; set; }
        public int  CruiseId { get; set; }
        public virtual Cruise Cruise { get; set; }

        public int PersonId { get; set; }
        public virtual Person Person { get; set; }

    }
}
