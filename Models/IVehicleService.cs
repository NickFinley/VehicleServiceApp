namespace VehicleServiceApp.Models
{
    public interface IVehicleService
    {
        public Vehicle? GetVehicle(int id);
        public List<Vehicle>? GetVehicles(QueryParameters parameters);
        public Vehicle? CreateVehicle(Vehicle vehicle);
        public string UpdateVehicle(Vehicle vehicle);
        public string DeleteVehicle(int id);
    }
}
