using System.Linq;
using NidTid.Domain.Entities;

namespace NidTid.Domain.Abstract
{
    public interface IVehicleRepository
    {

        IQueryable<Vehicle> Vehicles { get; }

        int SaveVehicle(Vehicle vehicle);
    }
}