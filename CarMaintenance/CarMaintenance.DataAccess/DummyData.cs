namespace CarMaintenance.DataAccess
{
    public static class DummyData
    {
        static DummyData()
        {
            Cars = new List<Car> {

              new Car {  make=1991, model="123", OwnerName="John", RegistrationNumber= "134444"},
              new Car {  make=1991, model="123", OwnerName="John", RegistrationNumber= "134444"},

            };
        }

        private static List<Car> Cars { get; set; }

        private static List<MaintenanceRequest> MaintenanceRequests { get; set; }   = new List<MaintenanceRequest>();
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