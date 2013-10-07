using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NidTid.Domain.Abstract;
using NidTid.Domain.Entities;
using NidTid.WebUI.Models;
using System.Web.Services;
using NidTid.WebUI.Security;


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

        [HttpGet]
        [Authorize]
        public ActionResult EditReportModal(int reportId)
        {
            ReportViewModel report = new ReportViewModel();
            report.Report = repository.Reports.FirstOrDefault(r => r.Id == reportId);
            report.Customers = customerRepo.Customers;
            return PartialView(report);
        }

        [HttpPost]
        [Authorize]
        public String SaveReport(ReportViewModel reportModel)
        {
            Report report = reportModel.Report;
            var identity = ((CustomPrincipal)HttpContext.User).CustomIdentity;
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

        [HttpPost]
        [Authorize]
        public void DeleteReport(int reportId)
        {
            Report report = repository.Reports.FirstOrDefault(r => r.Id == reportId);
            repository.DeleteReport(report);
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

        public ActionResult UserSummary()
        {
            var identity = ((CustomPrincipal)HttpContext.User).CustomIdentity;
            ReportSummaryViewModel reportSummary = new ReportSummaryViewModel(this.repository.Reports, identity.UserId);
            return PartialView(reportSummary);
        }


        
    }
}


