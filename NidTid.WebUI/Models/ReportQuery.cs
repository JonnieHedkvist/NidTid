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
        public int Limit { get; set; }
        

        public IQueryable<Report> Filter(IQueryable<Report> reports) {

            if (this.fromDate != null) {
                reports = reports.Where(r => r.Date >= this.fromDate).OrderByDescending(r => r.Id);
            }

            if (this.toDate != null) {
                reports = reports.Where(r => r.Date <= this.toDate).OrderByDescending(r => r.Id);
            }

            if (this.ProjectId !=null) {
                reports = reports.Where(r => r.ProjectId == this.ProjectId).OrderByDescending(r => r.Id);
		    }

		    if (this.UserId != null) {
                reports = reports.Where(r => r.UserId == this.UserId).OrderByDescending(r => r.Id);
		    }

            if (this.Limit != null) {
                reports = reports.Take(Limit);
            }
            

            return reports;
        }

    
    }

   
}