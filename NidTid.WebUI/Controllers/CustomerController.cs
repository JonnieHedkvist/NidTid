using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NidTid.Domain.Abstract;
using NidTid.Domain.Entities;
using NidTid.WebUI.Models;
using System.Web.Services;


namespace NidTid.WebUI.Controllers {
    
    public class CustomerController : Controller {
        private ICustomerRepository repository;

        public CustomerController(ICustomerRepository customerRepository) {
            this.repository = customerRepository;
        }

        public ActionResult CustomerProjectSelect() {
            CustomersListViewModel model = new CustomersListViewModel {Customers = repository.Customers};
            return PartialView(model);
        }
        
        [HttpGet]
        [Authorize(Roles = "admin")]
        public ActionResult CustomerDetails(int? id) {
            Customer selectedCustomer = new Customer();
            if (id != null)
            {
                selectedCustomer = repository.Customers.FirstOrDefault(c => c.Id == id);
            }
            return View(selectedCustomer);
        }

        [HttpPost]
        [Authorize(Roles = "admin")]
        public ActionResult CustomerDetails(Customer currentCustomer) {
            var currentId = 0;
            if (ModelState.IsValid) {
                currentId = repository.SaveCustomer(currentCustomer);
                TempData["message"] = "Kunduppgifterna har uppdaterats!";
            }
            else { 
                //FIXA FELMEDDELANDE
            }
            return RedirectToAction(actionName: "CustomerDetails", routeValues: new { id = currentId });
        }

        [HttpPost]
        [Authorize(Roles = "admin")]
        public String DeleteCustomer(int id)
        {
            string msg = "";
            Customer customer = repository.Customers.FirstOrDefault(c => c.Id == id);
            if (customer.Project.Count() > 0)
            {
                msg = "Kan ej radera en kund med registrerade projekt!";
            }
            else {
                repository.DeleteCustomer(customer);
                msg = "Kunden har raderats ur databasen";
            }
            return msg;
        }


        [HttpGet]
        [Authorize(Roles = "admin")]
        public ActionResult Create() {
            Customer newCustomer = new Customer();
            return PartialView(newCustomer);
        }
        

        public ActionResult FilteredCustomers(string term)
        {
            var filteredCustomers = repository.Customers.Where(c => c.Name.StartsWith(term)).Select(c => new
            {
                label = c.Name,
                value = c.Id
            });
            return Json(filteredCustomers, JsonRequestBehavior.AllowGet);
        }

        
        public ActionResult FilteredProjects(int customerId) {
            Customer tempCustomer = repository.Customers.FirstOrDefault(c => c.Id == customerId);
            var filteredProjects = tempCustomer.Project.Where(proj => proj.Active == true).Select(p => new 
            {
                name = p.Name,
                id = p.Id
            });
            return Json(filteredProjects, JsonRequestBehavior.AllowGet);
        }

        public ActionResult FilteredProjectsAll(int customerId)
        {
            Customer tempCustomer = repository.Customers.FirstOrDefault(c => c.Id == customerId);
            var filteredProjects = tempCustomer.Project.Select(p => new
            {
                name = p.Name,
                id = p.Id
            });
            return Json(filteredProjects, JsonRequestBehavior.AllowGet);
        }


    }
}
