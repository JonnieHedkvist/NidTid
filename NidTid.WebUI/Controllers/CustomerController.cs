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
            return View(currentCustomer);
        }

        public ViewResult Create() {
            
            return View("CustomerDetails", new Customer());
        }

        [WebMethod(true)]
        public string FilteredCustomers(string term)
        {
            var filteredCustomers = repository.Customers.Where(c => c.Name.StartsWith(term)).Select(c => new
            {
                label = c.Name,
                value = c.Id
            });

            String json = JsonConvert.SerializeObject(filteredCustomers);
            return json;
        }

    }
}
