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
    public class ReportQuery
    {
        public Nullable<int> ProjectId {get; set;}
        public Nullable<int> UserId { get; set; }
        public Nullable<DateTime> fromDate { get; set; }
        public Nullable<DateTime> toDate { get; set; }
        public Nullable<int> Limit { get; set; }
        

        public IQueryable<Report> Filter(IQueryable<Report> reports) {

            if (this.fromDate != null) {
                reports = reports.Where(r => r.Date >= this.fromDate);
            }

            if (this.toDate != null) {
                reports = reports.Where(r => r.Date <= this.toDate);
            }

            if (this.ProjectId !=null) {
                reports = reports.Where(r => r.ProjectId == this.ProjectId);
		    }

		    if (this.UserId != null) {
                reports = reports.Where(r => r.UserId == this.UserId);
		    }

            if (this.Limit != null) {
                reports = reports.Take(Limit.Value);
            }


            return reports.OrderByDescending(r => r.Id);
        }

    
    }

   
}