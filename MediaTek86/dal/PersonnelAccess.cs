using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediaTek86.model;

namespace MediaTek86.dal
{
    class PersonnelAccess
    {
        private readonly Access access = null;
        
        public PersonnelAccess()
        {
            access = Access.GetInstance();
        }

        public List<Personnel> GetPersonnels()
        {
            List<Personnel> personnels = new List<Personnel>();

            if (access == null) return personnels;

            string req = @"SELECT
                personnel.IDPERSONNEL,
                personnel.NOM,
                personnel.PRENOM,
                personnel.TEL,
                personnel.MAIL,
                service.IDSERVICE,
                service.NOM AS NOMSERVICE
            FROM
                personnel
            INNER JOIN service ON service.IDSERVICE = personnel.IDSERVICE";

            try
            {
                List<Dictionary<string, object>> records = access.Manager.ReqSelect(req);

                foreach(Dictionary<string, object> row in records)
                {
                    personnels.Add(new Personnel(
                        (int)row["IDPERSONNEL"],
                        new Service(
                            (int)row["IDSERVICE"],
                            (string)row["NOMSERVICE"]
                        ),
                        (string)row["NOM"],
                        (string)row["PRENOM"],
                        (string)row["TEL"],
                        (string)row["MAIL"]
                    ));
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                Environment.Exit(0);
            }

            return personnels;
        }
    }
}
