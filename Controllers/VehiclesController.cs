using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VehicleServiceApp.Models;

namespace VehicleServiceApp.Controllers
{
    [Route("api/Vehicles")]
    [ApiController]
    public class VehiclesController : ControllerBase
    {
        private readonly IVehicleService _vehicleService;
        public VehiclesController(IVehicleService service)
        {
            _vehicleService = service;
        }

        // GET: api/Vehicles
        [HttpGet]
        public IActionResult GetVehicles([FromQuery]QueryParameters parameters)
        {
            var vehicles = _vehicleService.GetVehicles(parameters);
            if (vehicles == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(vehicles);
            }
        }

        // GET: api/Vehicles/{id}
        [HttpGet("{id}")]
        public IActionResult GetVehicle(int id)
        {
            var vehicle = _vehicleService.GetVehicle(id);
            if (vehicle == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(vehicle);
            }
        }

        // PUT: api/Vehicles
        [HttpPut]
        public IActionResult PutVehicle(Vehicle vehicle)
        {
            var result = _vehicleService.UpdateVehicle(vehicle);
            if (result != "" || _vehicleService.GetVehicle(vehicle.Id) == null)
            {
                return NotFound();
            }
            return Ok(result);
        }

        // POST: api/Vehicles
        [HttpPost]
        public IActionResult PostVehicle(Vehicle vehicle)
        {
            Vehicle result = _vehicleService.CreateVehicle(vehicle);
            if (result == null)
            {
                return BadRequest("Invalid Vehicle Properties");
            }

            return CreatedAtAction(nameof(GetVehicle), new { id = result.Id }, result);
        }

        // DELETE: api/Vehicles/{id}
        [HttpDelete("{id}")]
        public IActionResult DeleteVehicle(int id)
        {
            if (_vehicleService.GetVehicle(id) == null)
            {
                return NotFound();
            }

            string result = _vehicleService.DeleteVehicle(id);
            return Ok(result);
        }
    }
}
