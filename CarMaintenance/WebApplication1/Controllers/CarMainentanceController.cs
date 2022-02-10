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
        public CarModel GetCarDetails(string registrationNumber)
        {
            var carDetails = this.carMaintenanceService.GetCarDetails(registrationNumber);
            return carDetails;
        }

        [HttpGet("ShopServices")]
        public List<ShopServices> GetShopServices()
        {
            var shopServices = this.carMaintenanceService.GetShopServices();
            return shopServices;
        }


        [HttpGet("CarsInMaintenance")]
        public List<MaintenanceRequestModel> GetMaintenanceRequests()
        {
            var maintenanceRequests = this.carMaintenanceService.GetMaintenanceRequests();
            return maintenanceRequests;
        }

        [HttpPost("CarsInMaintenance")]
        public bool AddCarToMaintenance(string registartionNumber, List<ShopServices> shopServices)
        {
            var isAdded = this.carMaintenanceService.AddCarToMaintenance(registartionNumber, shopServices);
            return isAdded;
        }

    }
}
