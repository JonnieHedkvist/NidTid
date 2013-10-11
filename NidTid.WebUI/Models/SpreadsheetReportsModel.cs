using NidTid.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NidTid.WebUI.Models
{
    public class SpreadsheetReportsModel
    {
        public IEnumerable<Report> Reports { get; set; }
        public decimal DebTotal { get; set; }
        public decimal EjDebTotal { get; set; }
        public decimal TimeTotal { get; set; }
    }
}