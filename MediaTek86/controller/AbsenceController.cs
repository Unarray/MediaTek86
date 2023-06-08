using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediaTek86.dal;
using MediaTek86.model;

namespace MediaTek86.controller
{
    class AbsenceController
    {
        private readonly AbsenceAccess absenceAccess;

        public AbsenceController()
        {
            absenceAccess = new AbsenceAccess();
        }

        public List<Absence> GetAbsences(int personnelId)
        {
            return absenceAccess.GetAbsences(personnelId);
        }

        public void DeleteAbsence(int personnelId, DateTime date)
        {
            absenceAccess.DeleteAbsence(personnelId, date);
        }

        public void DeleteAllAbsences(int personnelId)
        {
            absenceAccess.DeleteAllAbsences(personnelId);
        }

        public void CreateAbsence(Absence absence)
        {
            absenceAccess.CreateAbsence(absence);
        }

        public void UpdateAbsence(Absence absence)
        {
            absenceAccess.UpdateAbsence(absence);
        }
    }
}
