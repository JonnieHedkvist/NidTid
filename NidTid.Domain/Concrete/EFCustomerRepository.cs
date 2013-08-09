using System.Linq;
using NidTid.Domain.Abstract;
using NidTid.Domain.Entities;

namespace NidTid.Domain.Concrete {
    
    public class EFCustomerRepository : ICustomerRepository {
        private EFDbContext context = new EFDbContext();

        public IQueryable<Customer> Customers {
            get { return context.Customers; }
        }
    }

}
