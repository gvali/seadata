using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain;

namespace DAL.Interfaces
{
    public interface IProjectPersonRepository : IEFRepository<ProjectPerson>
    {
        List<ProjectPerson> AllProjectPersons();
        List<ProjectPerson> AllforUser(int userId);
        //Project GetForUser(int contactId, int userId);
    }
}
