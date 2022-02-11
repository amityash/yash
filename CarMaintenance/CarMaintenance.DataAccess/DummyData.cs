namespace CarMaintenance.DataAccess
{
    public static class DummyData
    {
        static DummyData()
        {
            Cars = new List<Car> {

              new Car {  make=1991, model="124", OwnerName="John1", RegistrationNumber= "134440"},
              new Car {  make=1991, model="125", OwnerName="John2", RegistrationNumber= "134441"},
              new Car {  make=1991, model="126", OwnerName="John3", RegistrationNumber= "134442"},
              new Car {  make=1991, model="127", OwnerName="Joh4", RegistrationNumber= "134443"},
              new Car {  make=1991, model="128", OwnerName="John5", RegistrationNumber= "134444"},
              new Car {  make=1991, model="129", OwnerName="John6", RegistrationNumber= "134445"},
              new Car {  make=1991, model="130", OwnerName="John7", RegistrationNumber= "134446"},
              new Car {  make=1991, model="131", OwnerName="John8", RegistrationNumber= "134447"},
              new Car {  make=1991, model="132", OwnerName="John9", RegistrationNumber= "134448"},
              new Car {  make=1991, model="133", OwnerName="John10", RegistrationNumber= "134449"},
              new Car {  make=1991, model="134", OwnerName="John11", RegistrationNumber= "134450"},
              new Car {  make=1991, model="135", OwnerName="John12", RegistrationNumber= "134451"},
            };
        }

        private static List<Car> Cars { get; set; }

        private static List<MaintenanceRequest> MaintenanceRequests { get; set; } = new List<MaintenanceRequest>();
        public static List<Car> GetCars()
        {
            return Cars;
        }


        public static List<MaintenanceRequest> GetMaintenanceRequests()
        {
            return MaintenanceRequests;
        }

    }
}