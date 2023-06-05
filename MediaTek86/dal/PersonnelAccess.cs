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

    }
}
