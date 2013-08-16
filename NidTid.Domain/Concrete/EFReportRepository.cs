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


        public void SaveReport(Report report)
        {

        }
    }

}
