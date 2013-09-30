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


        public int SaveUser(User user)
        {
            var currentId = user.Id;
            if (user.Id == 0)
            {
                context.Users.Add(user);
                context.SaveChanges();
                currentId = (from c in context.Users
                             orderby c.Id descending
                             select c.Id).First();
            }
            else
            {
                User dbUser = context.Users.Find(user.Id);
                if (dbUser != null)
                {
                    dbUser.Name = user.Name;
                    dbUser.Adress = user.Adress;
                    dbUser.Email = user.Email;
                    dbUser.Salary = user.Salary;
                    dbUser.PersNr = user.PersNr;
                    dbUser.Phone = user.Phone;
                    dbUser.Role = user.Role;
                    dbUser.PostNr = user.PostNr;
                    dbUser.PostOrt = user.PostOrt;
                    dbUser.Password = user.Password;
                    dbUser.Tax = user.Tax;
                }
            }
            context.SaveChanges();
            return currentId;
        }
    }

}
