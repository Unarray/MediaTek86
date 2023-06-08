using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediaTek86.dal;
using MediaTek86.model;

namespace MediaTek86.controller
{
    public class ServiceController
    {
        private readonly ServiceAccess serviceAccess;

        public ServiceController()
        {
            serviceAccess = new ServiceAccess();
        }

        public List<Service> GetServices()
        {
            return serviceAccess.GetServices();
        }
    }
}
