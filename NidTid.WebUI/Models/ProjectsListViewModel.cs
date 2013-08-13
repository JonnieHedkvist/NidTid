using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NidTid.Domain.Entities;

namespace NidTid.WebUI.Models
{
    public class ProjectsListViewModel
    {
        public IEnumerable<Project> Projects { get; set; }
    }
}