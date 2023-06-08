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

        public void DeleteAbsence(int personnelId, DateTime start)
        {
            if (access == null) return;

            string req = "DELETE FROM absence WHERE IDPERSONNEL=@id AND DATEDEBUT=@date;";
            Dictionary<string, object> parameters = new Dictionary<string, object> {
                {"@id", personnelId },
                {"@date", start }
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

        public void DeleteAllAbsences(int personnelId)
        {
            if (access == null) return;

            string req = "DELETE FROM absence WHERE IDPERSONNEL=@id;";
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

        public void CreateAbsence(Absence absence)
        {
            if (access == null) return;

            string req = "INSERT INTO absence (IDPERSONNEL, DATEDEBUT, DATEFIN, IDMOTIF) VALUES (@id, @debut, @fin, @motif)";
            Dictionary<string, object> parameters = new Dictionary<string, object> {
                {"@id", absence.personnel.id},
                {"@debut", absence.dateDebut},
                {"@fin", absence.dateFin},
                {"@motif", absence.motif.id}
            };

            access.Manager.ReqUpdate(req, parameters);
        }

        public void UpdateAbsence(Absence absence)
        {
            if (access == null) return;

            string req = "UPDATE absence SET DATEFIN=@fin, IDMOTIF=@motif WHERE IDPERSONNEL=@id AND DATEDEBUT=@debut";
            Dictionary<string, object> parameters = new Dictionary<string, object> {
                {"@id", absence.personnel.id},
                {"@debut", absence.dateDebut},
                {"@fin", absence.dateFin},
                {"@motif", absence.motif.id}
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
