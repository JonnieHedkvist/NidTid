using System.Linq;
using NidTid.Domain.Entities;
using System;

namespace NidTid.Domain.Abstract {
    public interface IProjectRepository {

        IQueryable<Project> Projects { get; }

        void SaveProject(Project project);

        void ToggleActive(Boolean active, int projectId);
    }
}

