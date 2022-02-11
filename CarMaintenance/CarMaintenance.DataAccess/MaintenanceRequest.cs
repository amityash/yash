using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarMaintenance.DataAccess
{
    public class MaintenanceRequest
    {
        public string RegistartionNumber { get; set; }
        public List<int> SelectedService { get; set; }
        public int Status { get; set; }
        public int SequenceNumber { get; set; }
    }
}
