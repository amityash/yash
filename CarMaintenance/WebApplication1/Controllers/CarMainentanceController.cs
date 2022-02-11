using CarMaintenance.Business;
using Microsoft.AspNetCore.Mvc;

namespace CarMaintenance.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarMainentanceController : ControllerBase
    {
        private readonly ICarMaintenanceService carMaintenanceService;

        public CarMainentanceController(ICarMaintenanceService carMaintenanceService)
        {
            this.carMaintenanceService = carMaintenanceService;
        }

        [HttpGet("CarDetails/{registrationNumber}")]
        public IActionResult GetCarDetails(string registrationNumber)
        {
            if (string.IsNullOrEmpty(registrationNumber))
            {
                return BadRequest();
            }
            var carDetails = this.carMaintenanceService.GetCarDetails(registrationNumber);
            return Ok(carDetails);
        }

        [HttpGet("Cars")]
        public IActionResult Cars()
        {
            var carDetails = this.carMaintenanceService.GetAllCar();
            return Ok(carDetails);
        }

        [HttpGet("ShopServices")]
        public IActionResult GetShopServices()
        {
            var shopServices = this.carMaintenanceService.GetShopServices();
            if(shopServices == null)
            {
                return NotFound();
            }
            return Ok(shopServices);
        }


        [HttpGet("GetMaintenanceRequests")]
        public IActionResult GetMaintenanceRequests()
        {
            var maintenanceRequests = this.carMaintenanceService.GetMaintenanceRequests();
            return Ok(maintenanceRequests);
        }

        [HttpPost("CarsInMaintenance")]
        public IActionResult AddCarToMaintenance(string registrationNumber, List<ShopServices> shopServices)
        {
            var isCarAdded= this.carMaintenanceService.IsCarAdded(registrationNumber);
            if (isCarAdded)
                return BadRequest("Car already Added");
            var isAdded = this.carMaintenanceService.AddCarToMaintenance(registrationNumber, shopServices);
            return Ok(isAdded);
        }

    }
}
