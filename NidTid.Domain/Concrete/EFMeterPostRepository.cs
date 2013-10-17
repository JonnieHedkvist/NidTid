using System.Linq;
using NidTid.Domain.Abstract;
using NidTid.Domain.Entities;
using System.Data.Entity;

namespace NidTid.Domain.Concrete
{

    public class EFMeterPostRepository : IMeterPostRepository
    {
        private EFDbContext context = new EFDbContext();

        public IQueryable<MeterPost> MeterPosts
        {
            get { return context.MeterPosts; }
        }


        public void SaveMeterPost(MeterPost meterpost)
        {
            if (meterpost.Id == 0)
            {
                context.MeterPosts.Add(meterpost);
            }
            else
            {
                MeterPost dbPost = context.MeterPosts.Find(meterpost.Id);
                if (dbPost != null)
                {
                    dbPost.Date = meterpost.Date;
                    dbPost.CurrentMeter = meterpost.CurrentMeter;
                    dbPost.UserId = meterpost.UserId;
                    dbPost.VehicleId = meterpost.VehicleId;
                    dbPost.Notes = meterpost.Notes;
                }
            }
            context.SaveChanges();
        }

        public void DeletePost(MeterPost post)
        {
            context.MeterPosts.Remove(post);
            context.SaveChanges();
        }
    }

}
