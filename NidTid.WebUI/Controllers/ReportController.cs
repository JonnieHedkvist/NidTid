using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NidTid.Domain.Abstract;
using NidTid.Domain.Entities;
using NidTid.WebUI.Models;
using System.Web.Services;
using NidTid.WebUI.Classes;


namespace NidTid.WebUI.Controllers
{
    public class ReportController : Controller
    {
        private IReportRepository repository;
        private ICustomerRepository customerRepo;

        public ReportController(IReportRepository reportRepository, ICustomerRepository customerRepository)
        {
            this.repository = reportRepository;
            this.customerRepo = customerRepository;
        }


        [HttpGet]
        public ActionResult NewReport()
        {
            ReportViewModel report = new ReportViewModel();
            report.Report = new Report();
            report.Customers = customerRepo.Customers;
            return View(report);
        }

        [HttpPost]
        public String SaveReport(ReportViewModel reportModel)
        {
            Report report = reportModel.Report;
            String message = "";
            if (ModelState.IsValid) {
                repository.SaveReport(report);
                message = "Rapporten har sparats!";
            }
            else {
                message = "Fel!";
            }
            return message;
        }


        public ActionResult List(ReportQuery query)
        {
            var filteredReports = query.Filter(this.repository.Reports);

            var k = filteredReports.ToList().Count;
            return PartialView(filteredReports);
        }


    }
}


