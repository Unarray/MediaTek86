using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediaTek86.dal;

namespace MediaTek86.controller
{
    public class AuthController
    {
        private readonly ResponsableAccess responsableAccess;

        public AuthController()
        {
            responsableAccess = new ResponsableAccess();
        }

        public Boolean isAuthValid(string login, string pwd)
        {
            return responsableAccess.AuthIsValid(login, pwd);
        }
    }
}
