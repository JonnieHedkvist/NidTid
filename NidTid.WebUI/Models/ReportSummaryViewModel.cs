using NidTid.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NidTid.WebUI.Models
{
    public class ReportSummaryViewModel
    {
        public decimal TimeToday { get; set; }
        public decimal TimeMonth { get; set; }

        public ReportSummaryViewModel(IQueryable<Report> reports, int userId) 
        {
            var today = DateTime.Today;
            this.TimeToday = (from r in reports where(r.Date == today && r.UserId == userId)
                              select (decimal?)r.Deb).Sum() ?? 0;
            this.TimeToday += (from r in reports
                               where (r.Date == today && r.UserId == userId)
                              select (decimal?)r.EjDeb).Sum() ?? 0;


            
            var startOfMonth = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            this.TimeMonth = (from r in reports
                             where (r.Date >= startOfMonth && r.Date <= today && r.UserId == userId)
                             select (decimal?)r.Deb).Sum() ?? 0;
            this.TimeMonth += (from r in reports
                              where (r.Date >= startOfMonth && r.Date <= today && r.UserId == userId)
                              select (decimal?)r.EjDeb).Sum() ?? 0;
        }
    }
}