using System.Linq;
using NidTid.Domain.Entities;

namespace NidTid.Domain.Abstract
{
    public interface IUserRepository
    {

        IQueryable<User> Users { get; }

        int SaveUser(User user);

        void DeleteUser(User user);
    }
}
