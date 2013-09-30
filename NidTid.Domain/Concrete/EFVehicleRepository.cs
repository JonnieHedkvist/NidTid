using System.Linq;
using NidTid.Domain.Abstract;
using NidTid.Domain.Entities;
using System.Data.Entity;

namespace NidTid.Domain.Concrete
{

    public class EFVehicleRepository : IVehicleRepository
    {
        private EFDbContext context = new EFDbContext();

        public IQueryable<Vehicle> Vehicles
        {
            get { return context.Vehicles; }
        }


        public int SaveVehicle(Vehicle vehicle)
        {
            return 1;
        }
    }

}
