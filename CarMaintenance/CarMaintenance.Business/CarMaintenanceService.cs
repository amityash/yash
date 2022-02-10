using CarMaintenance.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarMaintenance.Business
{
    public class CarMaintenanceService : ICarMaintenanceService
    {

        public bool AddCarToMaintenance(string registartionNumber, List<ShopServices> shopServices)
        {
            var carDetails = DummyData.GetCars().FirstOrDefault(e => e.RegistrationNumber == registartionNumber);
            var requests = DummyData.GetMaintenanceRequests();
            throw new NotImplementedException();
        }

        public CarModel GetCarDetails(string registrationNumber)
        {
            var carDetails = DummyData.GetCars().FirstOrDefault(e => e.RegistrationNumber == registrationNumber);
            if (carDetails == null)
            {
                return new CarModel();
            }
            return MapCar(carDetails);
        }

        public List<MaintenanceRequestModel> GetMaintenanceRequests()
        {
            var requests = new List<MaintenanceRequestModel>();
            var maintenaceRequests = DummyData.GetMaintenanceRequests().Where(e => e.Status == (int)MaintenanceStatus.InProgress);
            foreach (var maintenaceRequest in maintenaceRequests)
            {
                requests.Add(MapRequest(maintenaceRequest));
            }
            return requests;
        }

        public List<ShopServices> GetShopServices()
        {
            List<ShopServices> shopServices = Enum.GetValues(typeof(ShopServices))
                            .Cast<ShopServices>()
                            .ToList();

            return shopServices;
        }

        private CarModel MapCar(Car car)
        {
            return new CarModel();
        }

        private MaintenanceRequestModel MapRequest(MaintenanceRequest maintenanceRequest)
        {
            return new MaintenanceRequestModel();
        }
    }
}
