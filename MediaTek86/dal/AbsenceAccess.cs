using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediaTek86.model;

namespace MediaTek86.dal
{
    class AbsenceAccess
    {
        private readonly Access access = null;

        public AbsenceAccess()
        {
            access = Access.GetInstance();
        }

        public List<Absence> GetAbsences(int personnelId)
        {
            List<Absence> absences = new List<Absence>();

            if (access == null) return absences;

            Personnel personnel = null;

            try
            {
                PersonnelAccess personnelAccess = new PersonnelAccess();
                personnel = personnelAccess.GetPersonnel(personnelId);
            }catch (Exception e)
            {
                Console.WriteLine(e.Message);
                Environment.Exit(0);
            }

            if (personnel == null) return absences;

            string req = @"SELECT
                absence.DATEDEBUT,
                absence.DATEFIN,
                motif.IDMOTIF,
                motif.LIBELLE
            FROM
                absence
            INNER JOIN motif ON motif.IDMOTIF = absence.IDMOTIF
            WHERE absence.IDPERSONNEL = @id;";
            Dictionary<string, object> parameters = new Dictionary<string, object> {
                {"@id", personnelId }
            };

            try
            {
                List<Dictionary<string, object>> records = access.Manager.ReqSelect(req, parameters);

                foreach (Dictionary<string, object> row in records)
                {
                    
                    absences.Add(new Absence(
                        personnel,
                        (DateTime)row["DATEDEBUT"],
                        (DateTime)row["DATEFIN"],
                        new Motif(
                            (int)row["IDMOTIF"],
                            (string)row["LIBELLE"]
                        )
                    ));
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                Environment.Exit(0);
            }

            return absences;
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
}
