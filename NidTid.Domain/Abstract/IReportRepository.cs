using System.Linq;
using NidTid.Domain.Entities;

namespace NidTid.Domain.Abstract
{
    public interface IReportRepository
    {

        IQueryable<Report> Reports { get; }

        void SaveReport(Report report);
    }
}
