using System.Linq;
using NidTid.Domain.Entities;

namespace NidTid.Domain.Abstract
{
    public interface IMeterPostRepository
    {

        IQueryable<MeterPost> MeterPosts { get; }

        void SaveMeterPost(MeterPost MeterPost);
    }
}
