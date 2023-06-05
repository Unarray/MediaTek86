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

        public void DeletePersonnel(int personnelId)
        {
            if (access == null) return;

            string req = "DELETE FROM personnel WHERE IDPERSONNEL=@id";
            Dictionary<string, object> parameters = new Dictionary<string, object> {
                {"@id", personnelId }
            };

            try
            {
                access.Manager.ReqUpdate(req, parameters);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                Environment.Exit(0);
            }
        }

        public void CreatePersonnel(Personnel personnel)
        {
            if (access == null) return;

            string req = "INSERT INTO personnel (NOM, PRENOM, TEL, MAIL, IDSERVICE) VALUES (@nom, @prenom, @tel, @mail, @idservice)";
            Dictionary<string, object> parameters = new Dictionary<string, object> {
                {"@nom", personnel.nom},
                {"@prenom", personnel.prenom},
                {"@tel", personnel.tel},
                {"@mail", personnel.mail},
                {"@idservice", personnel.service.id}
            };

            try
            {
                access.Manager.ReqUpdate(req, parameters);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                Environment.Exit(0);
            }
        }

        public void UpdatePersonnel(Personnel personnel)
        {
            if (access == null) return;

            string req = "UPDATE personnel SET NOM=@nom, PRENOM=@prenom, TEL=@tel, MAIL=@mail, IDSERVICE=@idservice WHERE IDPERSONNEL=@id";
            Dictionary<string, object> parameters = new Dictionary<string, object> {
                {"@id", personnel.id},
                {"@nom", personnel.nom},
                {"@prenom", personnel.prenom},
                {"@tel", personnel.tel},
                {"@mail", personnel.mail},
                {"@idservice", personnel.service.id}
            };

            try
            {
                access.Manager.ReqUpdate(req, parameters);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                Environment.Exit(0);
            }
        }
}
