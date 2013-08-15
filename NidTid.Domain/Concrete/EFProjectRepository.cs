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
                if (project.Id == 0)
                {
                    context.Projects.Add(project);
                }
                else
                {
                    Project dbProject = context.Projects.Find(project.Id);
                    if (dbProject != null)
                    {
                        dbProject.Name = project.Name;
                        dbProject.Active = project.Active;
                        dbProject.Description = project.Description;
                        dbProject.FastBtn = project.FastBtn;
                        dbProject.KontoStr = project.KontoStr;
                        dbProject.Referens = project.Referens;
                    }
                }
                context.SaveChanges();
            }
        }
    }

