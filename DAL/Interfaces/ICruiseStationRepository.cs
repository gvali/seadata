using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain;

namespace DAL.Interfaces
{
    public interface ICruiseStationRepository : IEFRepository<CruiseStation>
    {
        List<CruiseStation> AllCruiseStations();
        //List<CruiseStation> AllforUser(int userId);
        //Project GetForUser(int contactId, int userId);
    }
}
