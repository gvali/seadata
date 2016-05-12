using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain;

namespace DAL.Interfaces
{
    public interface ICruiseRepository : IEFRepository<Cruise>
    {
        List<Cruise> AllforUser(int userId);
        //Cruise GetForUser(int CruiseId, int userId);
    }
}
