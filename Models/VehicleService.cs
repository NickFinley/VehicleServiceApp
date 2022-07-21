using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace VehicleServiceApp.Models
{
    public class VehicleService : IVehicleService
    {
        private readonly VehicleContext _context;

        public VehicleService(VehicleContext context)
        {
            _context = context;
        }

        public Vehicle? GetVehicle(int id)
        {
            if (_context.Vehicles == null)
            {
                return null;
            }
            else
            {
                return _context.Vehicles.Find(id);
            }
        }

        public List<Vehicle>? GetVehicles(QueryParameters parameters)
        {
            if (_context.Vehicles == null)
            {
                return null;
            }

            //filter by query parameters
            var vehicles = from vehicle in _context.Vehicles
                           where
                           (parameters.Models == null || parameters.Models.Contains(vehicle.Model, StringComparer.OrdinalIgnoreCase))
                           && (parameters.Makes == null || parameters.Makes.Contains(vehicle.Make, StringComparer.OrdinalIgnoreCase))
                           && (parameters.MinYear == 0 || vehicle.Year >= parameters.MinYear)
                           && (parameters.MaxYear == 0 || vehicle.Year <= parameters.MaxYear)
                           select vehicle;

            return vehicles.ToList();
        }

        public Vehicle? CreateVehicle(Vehicle? vehicle)
        {
            if (VehicleValidation.Validate(vehicle))
            {
                _context.Vehicles.Add(vehicle);
                _context.SaveChanges();
            }
            else
            {
                vehicle = null;
            }

            return vehicle;
        }

        public string UpdateVehicle(Vehicle vehicle)
        {
            string result = "";
            //check if the new values are valid before attempting update
            if (!VehicleValidation.Validate(vehicle))
            {
                result = "Invalid Properties";
            }

            _context.Entry(vehicle).State = EntityState.Modified;

            try
            {
                _context.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!VehicleExists(vehicle.Id))
                {
                    result = "Vehicle Not Found";
                }
                else
                {
                    throw;
                }
            }

            return result;
        }

        public string DeleteVehicle(int id)
        {
            string result;
            if (_context.Vehicles != null )
            {
                var vehicle = _context.Vehicles.Find(id);
                if (vehicle != null)
                {
                    _context.Vehicles.Remove(vehicle);
                    _context.SaveChanges();
                    result = "Deleted";
                }
                else
                {
                    result = "Vehicle Not Found";
                }
            }
            else
            {
                result = "Entity Set Empty";
            }

            return result;
        }

        private bool VehicleExists(int id)
        {
            return (_context.Vehicles?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
