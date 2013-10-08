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
        private IUserRepository userRepository;
        private ICustomerRepository customerRepository;

        public ProjectController(IProjectRepository projectRepository, IUserRepository userRepo, ICustomerRepository customRepo) {
            this.repository = projectRepository;
            this.userRepository = userRepo;
            this.customerRepository = customRepo;
        }

        [HttpGet]
        public ActionResult ProjectDetails(int? projectId) {
            Project selectedProject = new Project();
            ProjectViewModel projectModel = new ProjectViewModel();
            projectModel.Users = userRepository.Users;
            if(projectId != null){
                selectedProject = repository.Projects.FirstOrDefault(p => p.Id == projectId);
                projectModel.DebHours = (from r in selectedProject.Report
                                select r.Deb).Sum() ?? 0;
                projectModel.EjDebHours = (from r in selectedProject.Report
                                    select r.EjDeb).Sum() ?? 0;
                projectModel.TotalHours = projectModel.EjDebHours + projectModel.DebHours;
                
            }
            projectModel.Project = selectedProject;
            return PartialView(projectModel);
        }

        [HttpPost]
        public String ProjectDetails(ProjectViewModel currentProjectModel)
        {
            String message = "";
            if (ModelState.IsValid)
            {
                repository.SaveProject(currentProjectModel.Project);
                message = "Projektet har skapats";
            }
            else
            {
                message = "Ett fel uppstod och projektet kunde inte sparas, försök igen.";
            }
            return message;
        }

        [HttpPost]
        public String ToggleActive(Boolean active, int projectId)
        {
            String message = "";
            repository.ToggleActive(active, projectId);
            
            return message;
        }

        public ActionResult NewProject(int customerId) {
            Project newProject = new Project();
            newProject.CustomerId = customerId;
            newProject.Name = "Nytt Projekt";
            newProject.Customer = customerRepository.Customers.FirstOrDefault(c => c.Id == customerId);
            
            ProjectViewModel newProjectModel = new ProjectViewModel();
            newProjectModel.Users = userRepository.Users;
            newProjectModel.Project = newProject;
            return PartialView(newProjectModel);
        }
    }
}