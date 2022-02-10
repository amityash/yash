using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarMaintenance.DataAccess
{
    public class MaintenanceRequest
    {
        public int RegistartionNumber { get; set; }
        public int SelectedService { get; set; }
        public int Status { get; set; }
    }
}
