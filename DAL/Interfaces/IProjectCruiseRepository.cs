using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain;

namespace DAL.Interfaces
{
    public interface IProjectCruiseRepository : IEFRepository<ProjectCruise>
    {
        List<ProjectCruise> AllProjectCruises();
        //List<ProjectCruise> AllforUser(int userId);
        //Project GetForUser(int contactId, int userId);
    }
}
