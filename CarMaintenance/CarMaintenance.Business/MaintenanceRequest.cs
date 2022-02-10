using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarMaintenance.Business
{
    public class MaintenanceRequestModel
    {
        public MaintenanceRequestModel()
        {
            Car = new CarModel();
            SelectedService = new List<ShopServices>();
        }

        public CarModel Car { get; set; }
        public List<ShopServices> SelectedService { get; set; }
        public MaintenanceStatus Status { get; set; }
    }
}
