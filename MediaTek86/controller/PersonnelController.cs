using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediaTek86.dal;
using MediaTek86.model;

namespace MediaTek86.controller
{
    class PersonnelController
    {
        private readonly PersonnelAccess personnelAccess;

        public PersonnelController()
        {
            personnelAccess = new PersonnelAccess();
        }

        public List<Personnel> GetPersonnels()
        {
            return personnelAccess.GetPersonnels();
        }

        public void DeletePersonnels(int personnelId)
        {
            personnelAccess.DeletePersonnel(personnelId);
        }

        public void CreatePersonnel(Personnel personnel)
        {
            personnelAccess.CreatePersonnel(personnel);
        }
    }
}
