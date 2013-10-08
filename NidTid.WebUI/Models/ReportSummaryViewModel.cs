using NidTid.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NidTid.WebUI.Models
{
    public class ReportSummaryViewModel
    {
        public decimal TotalToday { get; set; }
        public decimal TotalMonth { get; set; }
        public decimal DebToday { get; set; }
        public decimal EjDebToday { get; set; }
        public decimal DebMonth { get; set; }
        public decimal EjDebMonth { get; set; }

        public ReportSummaryViewModel(IQueryable<Report> reports, int userId) 
        {
            var today = DateTime.Today;
            this.DebToday = (from r in reports where(r.Date == today && r.UserId == userId)
                              select (decimal?)r.Deb).Sum() ?? 0;
            this.EjDebToday += (from r in reports
                               where (r.Date == today && r.UserId == userId)
                              select (decimal?)r.EjDeb).Sum() ?? 0;
            this.TotalToday = (this.DebToday + this.EjDebToday);


            
            var startOfMonth = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            this.DebMonth = (from r in reports
                             where (r.Date >= startOfMonth && r.Date <= today && r.UserId == userId)
                             select (decimal?)r.Deb).Sum() ?? 0;
            this.EjDebMonth += (from r in reports
                              where (r.Date >= startOfMonth && r.Date <= today && r.UserId == userId)
                              select (decimal?)r.EjDeb).Sum() ?? 0;

            this.TotalMonth = (this.EjDebMonth + this.DebMonth);
        }
    }
}