﻿using System.Linq;
using NidTid.Domain.Entities;

namespace NidTid.Domain.Abstract {
    public interface ICustomerRepository {
        
        IQueryable<Customer> Customers { get; }
        IQueryable<Customer> ActiveCustomers { get; }

        int SaveCustomer(Customer customer);
        void DeleteCustomer(Customer customer);
    }   
}
