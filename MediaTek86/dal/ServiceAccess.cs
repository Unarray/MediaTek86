using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediaTek86.model;

namespace MediaTek86.dal
{
    public class ServiceAccess
    {
        private readonly Access access = null;

        public ServiceAccess()
        {
            access = Access.GetInstance();
        }

        public List<Service> GetServices()
        {
            List<Service> services = new List<Service>();

            if (access == null) return services;

            string req = "SELECT IDSERVICE, NOM FROM service;";

            try
            {
                List<Dictionary<string, object>> records = access.Manager.ReqSelect(req);

                foreach (Dictionary<string, object> row in records)
                {

                    services.Add(new Service(
                        (int)row["IDSERVICE"],
                        (string)row["NOM"]
                    ));
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                Environment.Exit(0);
            }

            return services;
        }
    }
}
