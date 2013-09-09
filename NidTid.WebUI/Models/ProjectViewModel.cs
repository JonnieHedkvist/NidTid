using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NidTid.Domain.Entities;

namespace NidTid.WebUI.Models
{
    public class ProjectViewModel
    {
        public Project Project { get; set; }
        public decimal TotalHours { get; set; }
        public decimal EjDebHours { get; set; }
        public decimal TotalKm { get; set; }
    }
}