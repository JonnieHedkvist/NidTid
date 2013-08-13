using System.Linq;
using NidTid.Domain.Abstract;
using NidTid.Domain.Entities;
using System.Data.Entity;

namespace NidTid.Domain.Concrete {

        public class EFProjectRepository : IProjectRepository {
            private EFDbContext context = new EFDbContext();

            public IQueryable<Project> Projects {
                get { return context.Projects; }
            }

            public void SaveProject(Project project) {   
            }
        }
    }

