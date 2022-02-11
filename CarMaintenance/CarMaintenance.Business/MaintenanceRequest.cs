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
        }

        public CarModel Car { get; set; }
        public List<ShopServices> SelectedServices { get; set; }
        public MaintenanceStatus Status { get; set; }
    }
}
