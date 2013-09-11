using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NidTid.Domain.Abstract;
using NidTid.Domain.Entities;
using NidTid.WebUI.Models;
using System.Web.Services;
using System.Data.Entity;

namespace NidTid.WebUI.Models
{
    public class SpreadsheetViewModel 
    {
        public IEnumerable<Customer> Customers { get; set; }
        public IEnumerable<User> Users { get; set; }   
    }   
}
