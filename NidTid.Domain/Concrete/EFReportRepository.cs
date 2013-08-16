using System.Linq;
using NidTid.Domain.Abstract;
using NidTid.Domain.Entities;
using System.Data.Entity;

namespace NidTid.Domain.Concrete
{

    public class EFReportRepository : IReportRepository
    {
        private EFDbContext context = new EFDbContext();

        public IQueryable<Report> Reports
        {
            get { return context.Reports; }
        }


        public void SaveReport(Report report) {
            if (report.Id == 0)
            {
                context.Reports.Add(report);
            }
            else
            {
                Report dbReport = context.Reports.Find(report.Id);
                if (dbReport != null)
                {
                    dbReport.Date = report.Date;
                    dbReport.Deb = report.Deb;
                    dbReport.EjDeb = report.EjDeb;
                    dbReport.Km = report.Km;
                    dbReport.Notes = report.Notes;
                    dbReport.ProjectId = report.ProjectId;
                    dbReport.UserId = report.UserId;
                }
            }
            context.SaveChanges();
        }
    }

}
