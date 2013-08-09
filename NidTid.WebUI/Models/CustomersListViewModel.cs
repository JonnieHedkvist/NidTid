using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NidTid.Domain.Entities;

namespace NidTid.WebUI.Models
{
    public class CustomersListViewModel {
        public IEnumerable<Customer> Customers { get; set; }
    }
}