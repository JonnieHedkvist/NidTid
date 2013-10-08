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


        public void SaveVehicle(Vehicle vehicle)
        {
            if (vehicle.Id == 0)
            {
                context.Vehicles.Add(vehicle);
            }
            else
            {
                Vehicle dbPost = context.Vehicles.Find(vehicle.Id);
                if (dbPost != null)
                {
                    dbPost.RegNr = vehicle.RegNr;
                    dbPost.Description = vehicle.Description;
                }
            }
            context.SaveChanges();
        }
    }

}
