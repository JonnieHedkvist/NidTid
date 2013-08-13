using System.Linq;
using NidTid.Domain.Entities;

namespace NidTid.Domain.Abstract {
    public interface IProjectRepository {

        IQueryable<Project> Projects { get; }

        void SaveProject(Project project);
    }
}

