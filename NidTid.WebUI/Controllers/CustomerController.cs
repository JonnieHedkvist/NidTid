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

        public ActionResult List() {
            CustomersListViewModel model = new CustomersListViewModel {Customers = repository.Customers};
            return View(model);
        }
        
        [HttpGet]
        public ActionResult CustomerDetails(int id) {
            Customer selectedCustomer = repository.Customers.FirstOrDefault(c => c.Id == id);
            
            return View(selectedCustomer);
        }

        [HttpPost]
        public ActionResult CustomerDetails(Customer currentCustomer) {
            if (ModelState.IsValid) {
                repository.SaveCustomer(currentCustomer);
                TempData["message"] = "Kunduppgifterna har uppdaterats!";
            }
            else { 
                //FIXA FELMEDDELANDE
            }
            return RedirectToAction("CustomerDetails", "Customer");
        }

        public ViewResult Create() {
            
            return View("CustomerDetails", new Customer());
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

    }
}
