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

        [HttpGet]
        public ActionResult ProjectDetails(int? projectId) {
            Project selectedProject = new Project();
            if(projectId != null){
                selectedProject = repository.Projects.FirstOrDefault(p => p.Id == projectId);
            }
            
            return PartialView(selectedProject);
        }

        [HttpPost]
        public String ProjectDetails(Project currentProject)
        {
            String message ="";
            if (ModelState.IsValid)
            {
                repository.SaveProject(currentProject);
                message = "Projektet har uppdaterats!";
            }
            else
            {
                //FIXA FELMEDDELANDE
            }
            return message;
        }

        public ActionResult NewProject(int customerId) {
            Project newProject = new Project();
            newProject.CustomerId = customerId;
            newProject.Name = "Nytt Projekt";
            return PartialView(newProject);
        }
    }
}
