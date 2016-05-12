using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain;

namespace DAL.Interfaces
{
    public interface ICruisePersonRepository : IEFRepository<CruisePerson>
    {
        List<CruisePerson> AllCruisePersons();
        List<CruisePerson> AllforUser(int userId);
        //Project GetForUser(int contactId, int userId);
    }
}
