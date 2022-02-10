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
            throw new NotImplementedException();
        }

        public CarModel GetCarDetails(string registrationNumber)
        {
            var carDetails = DummyData.GetCars().FirstOrDefault(e => e.RegistrationNumber == registrationNumber);
            if(carDetails == null)
            {
                return new CarModel();
            }
            return MapCar(carDetails);
        }

        public List<MaintenanceRequestModel> GetMaintenanceRequests()
        {
            throw new NotImplementedException();
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
