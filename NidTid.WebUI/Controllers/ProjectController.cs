using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NidTid.Domain.Abstract;
using NidTid.Domain.Entities;
using NidTid.WebUI.Models;
using System.Web.Services;
using Newtonsoft.Json;

namespace NidTid.WebUI.Controllers
{
    public class ProjectController : Controller {
        private IProjectRepository repository;

        public ProjectController(IProjectRepository projectRepository) {
            this.repository = projectRepository;
        }

        public ActionResult List() {
            ProjectsListViewModel model = new ProjectsListViewModel { Projects = repository.Projects };
            return View(model);
        }

    }
}
