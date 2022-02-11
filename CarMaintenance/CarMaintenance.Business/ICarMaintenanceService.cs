using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarMaintenance.Business
{
    public interface ICarMaintenanceService
    {
        public CarModel GetCarDetails(string registrationNumber);

        public List<string> GetShopServices();

        public bool AddCarToMaintenance(string registartionNumber, List<ShopServices> shopServices);

        public List<MaintenanceRequestModel> GetMaintenanceRequests();

        public List<string> GetAllCar();

        bool IsCarAdded(string registrationNumber);

    }
}
