using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NidTid.Domain.Abstract;
using NidTid.Domain.Entities;
using NidTid.WebUI.Models;
using System.Web.Services;


namespace NidTid.WebUI.Controllers
{
    public class ReportController : Controller
    {
        private IReportRepository repository;
        private ICustomerRepository customerRepo;
        private IUserRepository userRepo;

        public ReportController(IReportRepository reportRepository, ICustomerRepository customerRepository, IUserRepository userRepository)
        {
            this.repository = reportRepository;
            this.customerRepo = customerRepository;
            this.userRepo = userRepository;
        }


        [HttpGet]
        [Authorize]
        public ActionResult NewReport()
        {
            ReportViewModel report = new ReportViewModel();
            report.Report = new Report();
            report.Customers = customerRepo.Customers;
            return View(report);
        }

        [HttpPost]
        [Authorize]
        public String SaveReport(ReportViewModel reportModel)
        {
            Report report = reportModel.Report;
            var identity = ((NidTid.WebUI.Security.CustomPrincipal)HttpContext.User).CustomIdentity;
            report.UserId = identity.UserId;
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

        [HttpGet]
        [Authorize]
        public ActionResult Spreadsheet()
        {
            SpreadsheetViewModel spreadsheet = new SpreadsheetViewModel();
            spreadsheet.Customers = customerRepo.Customers;
            spreadsheet.Users = userRepo.Users;
            return View(spreadsheet);
        }

        public ActionResult SpreadsheetResult(ReportQuery query)
        {
            var filteredReports = query.Filter(this.repository.Reports);

            var k = filteredReports.ToList().Count;
            return PartialView(filteredReports);
        }

        public ActionResult List(ReportQuery query)
        {
            var filteredReports = query.Filter(this.repository.Reports);

            var k = filteredReports.ToList().Count;
            return PartialView(filteredReports);
        }



    }
}


