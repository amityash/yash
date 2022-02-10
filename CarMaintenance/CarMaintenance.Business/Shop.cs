using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarMaintenance.Business
{
    public class Shop
    {
        public Shop()
        {
            MaintenanceRequests = new List<MaintenanceRequest>();
        }

        public List<MaintenanceRequest> MaintenanceRequests { get; set; }

        public const int maxRequests = 10;
    }
}
