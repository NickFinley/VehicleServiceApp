using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VehicleServiceApp;
using VehicleServiceApp.Models;

namespace VehicleControllerTest
{
    public class VehicleServiceDummy : IVehicleService
    {
        private readonly List<Vehicle> _vehicles;

        public VehicleServiceDummy()
        {
            _vehicles = new List<Vehicle>()
            {
                new() { Id = 1, Make = "GMC", Model = "Sierra", Year = 2002 },
                new() { Id = 2, Make = "Toyota", Model = "Camry", Year = 1999 },
                new() { Id = 3, Make = "Chevrolet", Model = "Silverado", Year = 2019 }
            };
        }
        public Vehicle? CreateVehicle(Vehicle vehicle)
        {
            if (VehicleValidation.Validate(vehicle))
            {
                _vehicles.Add(vehicle);
            }
            else
            {
                vehicle = null;
            }

            return vehicle;
        }

        public string DeleteVehicle(int id)
        {
            _vehicles.Remove(_vehicles.FirstOrDefault(v => v.Id == id));
            return "Deleted";
        }

        public Vehicle? GetVehicle(int id)
        {
            return _vehicles.FirstOrDefault(v => v.Id == id);
        }

        public List<Vehicle>? GetVehicles(QueryParameters parameters)
        {
            return _vehicles;
        }

        public string UpdateVehicle(Vehicle vehicle)
        {
            _vehicles.RemoveAt(_vehicles.FindIndex(v => v.Id == vehicle.Id));
            _vehicles.Add(vehicle);
            _vehicles.Sort();
            return "Updated";
        }
    }
}
