using System.Linq;
using NidTid.Domain.Abstract;
using NidTid.Domain.Entities;
using System.Data.Entity;

namespace NidTid.Domain.Concrete
{

    public class EFUserRepository : IUserRepository
    {
        private EFDbContext context = new EFDbContext();

        public IQueryable<User> Users
        {
            get { return context.Users; }
        }


        public void SaveUser(User user) {
           
        }
    }

}
