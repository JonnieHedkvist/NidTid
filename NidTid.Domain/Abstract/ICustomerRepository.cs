using System.Linq;
using NidTid.Domain.Entities;

namespace NidTid.Domain.Abstract {
    public interface ICustomerRepository {
        
        IQueryable<Customer> Customers { get; }
    }
}
