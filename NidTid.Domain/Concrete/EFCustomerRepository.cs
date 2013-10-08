using System.Linq;
using NidTid.Domain.Abstract;
using NidTid.Domain.Entities;
using System.Data.Entity;

namespace NidTid.Domain.Concrete {
    
    public class EFCustomerRepository : ICustomerRepository {
        private EFDbContext context = new EFDbContext();

        public IQueryable<Customer> Customers {
            get { return context.Customers; }
        }

        public IQueryable<Customer> ActiveCustomers
        {
            get {
                var activeCustomers = (from proj in context.Projects
                                        where proj.Active == true
                                        select proj.Customer).Distinct().OrderBy(c => c.Name);
                return activeCustomers;
            }
        }

        public void DeleteCustomer(Customer customer) {
            context.Customers.Remove(customer);
            context.SaveChanges();
        }

       
        public int SaveCustomer(Customer customer) 
        {
            var currentId = customer.Id;
            if (customer.Id == 0) {
                context.Customers.Add(customer);
                context.SaveChanges();
                currentId = (from c in context.Customers
                              orderby c.Id descending
                              select c.Id).First();
            }
            else {
                Customer dbCustomer = context.Customers.Find(customer.Id);
                if (dbCustomer != null) {
                    dbCustomer.Name = customer.Name;
                    dbCustomer.Adress = customer.Adress;
                    dbCustomer.Email = customer.Email;
                    dbCustomer.Moms = customer.Moms;
                    dbCustomer.OrgNr = customer.OrgNr;
                    dbCustomer.Phone1 = customer.Phone1;
                    dbCustomer.Phone2 = customer.Phone2;
                    dbCustomer.PostNr = customer.PostNr;
                    dbCustomer.PostOrt = customer.PostOrt;
                }
            }
            context.SaveChanges();
            return currentId;
        }
    }

}
