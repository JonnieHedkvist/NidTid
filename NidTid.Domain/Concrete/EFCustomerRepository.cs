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

       
        public void SaveCustomer(Customer customer) {
            if (customer.Id == 0) {
                context.Customers.Add(customer);
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
        }
    }

}
