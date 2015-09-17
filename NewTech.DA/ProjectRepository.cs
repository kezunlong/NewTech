using NewTech.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewTech.DA
{
    public class ProjectRepository : BaseRepository
    {
        public ProjectRepository(NewTechSqlDal dal) : base(dal) { }

        public List<Project> SelectProjects()
        {
            return null;
        }
    }
}
