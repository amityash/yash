using CarMaintenance.DataAccess;

namespace CarMaintenance.Business
{
    public class CarMaintenanceService : ICarMaintenanceService
    {
        public const int maxRequests = 10;

        public bool AddCarToMaintenance(string registrationNumber, List<ShopServices> shopServices)
        {
            try
            {

                var Dbrequests = DummyData.GetMaintenanceRequests();
                var maxSequence = 0;
                if (Dbrequests.Any())
                {
                    maxSequence = Dbrequests.Select(e => e.SequenceNumber).Max() + 1;
                }

                var request = new MaintenanceRequest
                {
                    RegistartionNumber = registrationNumber,
                    SelectedService = shopServices.Cast<int>().ToList(),
                    Status = (int)MaintenanceStatus.InProgress,
                    SequenceNumber = maxSequence++
                };
                Dbrequests.Add(request);

                var totalCars = Dbrequests.Select(e => e.RegistartionNumber).Distinct().Count();
                if (totalCars > maxRequests)
                {
                    var diff = totalCars - maxRequests;
                    var waitingrequests = Dbrequests.OrderByDescending(e => e.SequenceNumber).Take(diff);
                    foreach (var waitingRequest in waitingrequests)
                    {
                        waitingRequest.Status = (int)MaintenanceStatus.Waiting;
                    }
                }
            }
            catch (Exception)
            {
                return false;
            }


            return true;
        }

        public List<string> GetAllCar()
        {
            var cars = DummyData.GetCars().Select(e => e.RegistrationNumber).ToList();
            return cars;
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

        public List<string> GetShopServices()
        {
            List<string> shopServices = Enum.GetNames(typeof(ShopServices))
                            .ToList();

            return shopServices;
        }

        private CarModel MapCar(Car car)
        {
            var carmodel = new CarModel();
            carmodel.RegistrationNumber = car.RegistrationNumber;
            carmodel.OwnerName = car.OwnerName;
            carmodel.Make = car.make;
            carmodel.Model = car.model;
            return carmodel;
        }


        private MaintenanceRequestModel MapRequest(MaintenanceRequest maintenanceRequest)
        {
            var car = DummyData.GetCars().FirstOrDefault(e => e.RegistrationNumber == maintenanceRequest.RegistartionNumber);
            if (car != null)
            {
                var model = new MaintenanceRequestModel
                {
                    Car = MapCar(car),
                    SelectedServices = maintenanceRequest.SelectedService.Cast<ShopServices>().ToList(),
                    Status = (MaintenanceStatus)maintenanceRequest.Status,

                };
                return model;
            }
            return new MaintenanceRequestModel();

        }

        public bool IsCarAdded(string registrationNumber)
        {
            var Dbrequests = DummyData.GetMaintenanceRequests();
            var registrationNumbers = Dbrequests.Select(e => e.RegistartionNumber).ToList();
            return registrationNumbers.Contains(registrationNumber) ? true : false;
        }
    }
}
